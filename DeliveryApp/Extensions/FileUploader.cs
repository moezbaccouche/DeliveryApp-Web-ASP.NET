using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Extensions
{
    public class FileUploader
    {
        private readonly IProductImageService productImageService;

        public FileUploader(IProductImageService productImageService)
        {
            this.productImageService = productImageService;
        }

        public static string UploadImage(IFormFile file, string directoryName)
        {
            string path = "";
            if (file == null || file.Length == 0)
                return path;

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string pathToReturn = "~/Content/" + directoryName + "/" + fileName;

            path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot\\Content\\" + directoryName + "\\", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return pathToReturn;
        }

        public bool UploadImages(List<IFormFile> files, Product newProduct, string directoryName)
        {
            try
            {
                string path = "";
                if (files == null || files.Count == 0)
                    return false;
                else
                {
                    foreach (var file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string pathToReturn = "~/Content/" + directoryName + "/" + fileName;

                        path = Path.Combine(
                                        Directory.GetCurrentDirectory(), "wwwroot\\Content\\" + directoryName, fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        //Insert the base64 data
                        byte[] base64Bytes = FileToBase64(pathToReturn);

                        //Add the image in the table productImage
                        productImageService.AddProductImage(new ProductImage { ImagePath = pathToReturn, Product = newProduct, ImageBase64 = base64Bytes });

                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            //string convert = base64String.Replace("data:image/jpeg;base64,", string.Empty);
            string convert = base64String;
            if (base64String.Contains(','))
            {
                convert = base64String.Substring(base64String.IndexOf(",") + 1, base64String.Length - (base64String.IndexOf(",") + 1));

            }
            byte[] imageBytes = Convert.FromBase64String(convert);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public static byte[] FileToBase64(string path)
        {
            try
            {
                string editedPath = path.Remove(0, 2);
                editedPath = "wwwroot/" + editedPath;
                byte[] bytes = File.ReadAllBytes(editedPath);
                return bytes;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }

        public static string BytesToBase64String(byte[] base64Bytes)
        {
            string base64 = Convert.ToBase64String(base64Bytes);
            return base64;
        }

        //public static string UploadBase64Image(Image img, string directoryName)
        //{

        //    string path = "";
        //    if (img == null)
        //        return path;

        //    string fileName = "Picture-" + DateTime.Now.ToString("yymmssfff") + ".jpg";

        //    string pathToReturn = "~/Content/" + directoryName + "/" + fileName;

        //    path = Path.Combine(
        //            Directory.GetCurrentDirectory(), "wwwroot\\Content\\" + directoryName + "\\", fileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        img.CopyTo(stream);
        //    }
        //    return pathToReturn;
        //}

    }
}
