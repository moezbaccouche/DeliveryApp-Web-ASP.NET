using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json;



namespace DeliveryApp.Extensions
{
    public static class PushNotificationsSender
    {
        public static void SendPushNotificationToAll(string msg, string title)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";


            var obj = new
            {
                app_id = "4d92a6e0-c0bb-42b6-8bf1-01be7bc90286",
                contents = new { en = msg, fr = msg },
                heading = new { en = title, fr = title },
                android_accent_color = "FF6E1C1C"
            };


            var param = JsonConvert.SerializeObject(obj, Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }

        public static void SendPushNotificationToSpecificUsers(string[] playersIds, string title, string msg)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";


            var obj = new
            {
                app_id = "4d92a6e0-c0bb-42b6-8bf1-01be7bc90286",
                contents = new { en = msg, fr = msg },
                headings = new { en = title, fr = title },
                include_player_ids = playersIds,
                android_accent_color = "FF6E1C1C"
            };


            var param = JsonConvert.SerializeObject(obj, Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
