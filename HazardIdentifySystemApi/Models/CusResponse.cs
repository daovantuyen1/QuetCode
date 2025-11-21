using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models
{
    public class CusResponse
    {
        public string status { set; get; }
        public string message { set; get; }
        public dynamic data { set; get; }
    }
    public enum StatusType
    {
        success = 0,
        error = 1
    }
}