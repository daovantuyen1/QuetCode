using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class ResponseApiIcivet
    {
        public int StatusCode { set; get; }
        public string Response { set; get; }

    }
    public class RequestApiIcivet
    {
        public string Content { set; get; }
        public string  ToCivetNo { set; get; }
    }
}