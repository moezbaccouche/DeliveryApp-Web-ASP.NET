using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
       
        public static string UploadImage(IFormFile file)
        {
            string path = "";
            if (file == null || file.Length == 0)
                return path;

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string pathToReturn = "~/Content/CategoriesImages/" + fileName;

            path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot\\Content\\CategoriesImages\\", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return pathToReturn;
        }

        public bool UploadImages(List<IFormFile> files, Product newProduct )
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
                        string pathToReturn = "~/Content/CategoriesImages/" + fileName;

                        path = Path.Combine(
                                        Directory.GetCurrentDirectory(), "wwwroot\\Content\\CategoriesImages", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);

                            //Add the image in the table productImage
                            //AddAttachmentFile(pathToReturn, employee, request);
                            productImageService.AddProductImage(new ProductImage { ImagePath = pathToReturn, Product = newProduct });
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
