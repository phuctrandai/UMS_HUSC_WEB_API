using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS_HUSC_WEB_API.ViewModels
{
    public class PushNotification
    {
        public string[] registration_ids { get; set; }
        public Data data { get; set; }
        //public Notification notification { get; set; }
    }

    public class Data
    {
        public string title { get; set; }
        public string body { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public string postTime { get; set; }
    }

    public class Notification
    {
        public string title { get; set; }
        public string body { get; set; }
    }
}