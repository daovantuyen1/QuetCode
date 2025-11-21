using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class SignHazardNoityAppReq
    {
        public string docNo { set; get; }
        public string remark { set; get; }
        public string signType { set; get; }
        public string  rejectTo { set; get; }
    }

    public enum SignType
    {
        agree=0,
        reject=1,
        remove =2
    }

    public enum RejectTo
    {
        firstStation = 0,
        preStation =1,
    }

}