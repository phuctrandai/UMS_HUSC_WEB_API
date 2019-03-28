using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using UMS_HUSC_WEB_API.Daos;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class FCMController : ApiController
    {
        private const string SERVER_KEY= "AAAA47Pl2e0:APA91bHr-LTCt3PdHBeP-YaNHy4ytNf8GLcr6KJRcBCSN0zV7B1h3VoCpCDU_p7ctUd2cTRwfz6IdSBXXHT7Y0c3qFQ7zOD-VMipSDGNG8jr5YaOD1_WU1v6aSxe1IxHjUT6FPBnbk4o";
        private const string SENDER_ID = "977975761389";

        public const string MESSAGE_NOTIFICATION = "message_notification";
        public const string NEWS_NOTIFICATION = "news_notification";

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Controller run ok");
        }

        [HttpPost]
        public IHttpActionResult Post(string order, string maSinhVien, string token)
        {
            if (string.IsNullOrEmpty(order))
                return NotFound();

            else if (order.Equals("save"))
            {
                var result = FireBaseDao.AddFireBase(maSinhVien, token);
                if (result)
                    return Ok("Save token success");
                return NotFound();
            }
            else if (order.Equals("delete"))
            {
                var result = FireBaseDao.DeleteFireBase(maSinhVien, token);
                if (result)
                    return Ok("Delete token success");
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        
        public string CreateNewsNotification(THONGBAO tHONGBAO)
        {
            string title = tHONGBAO.TieuDe;
            string body = tHONGBAO.NoiDung;
            string postTime = tHONGBAO.ThoiGianDang.Value.ToString();

            string[] arrRegid = FireBaseDao.GetAllFireBase().Select(x => x.Token).Distinct().ToArray();
            string RegArr = string.Empty;
            RegArr = string.Join("\",\"", arrRegid);
            
            string postData = 
                "{ \"registration_ids\": [ \"" + RegArr + "\" ]" +
                ",\"data\": {" +
                    "\"type\": \"" + NEWS_NOTIFICATION + "\"" +
                    ",\"body\": \"" + body + "\"" +
                    ",\"title\": \"" + title + "\"" +
                    ",\"postTime\":\"" + postTime + 
                "\"}" +
                ",\"notification\": {" +
                    ",\"body\": \"" + body + "\"" +
                    ",\"title\": \"" + title + "\"" +
                    "}" +
                "}";
            return postData;
        }

        public string SendMessage(string postData)
        {
            WebRequest tRequest;
            //thiết lập FCM send
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "POST";
            tRequest.UseDefaultCredentials = true;
            tRequest.PreAuthenticate = true;
            tRequest.Credentials = CredentialCache.DefaultNetworkCredentials;

            //định dạng JSON
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_KEY));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();
            string response = sResponseFromServer; //Lấy thông báo kết quả từ FCM server.

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return response;
        }
    }
}
