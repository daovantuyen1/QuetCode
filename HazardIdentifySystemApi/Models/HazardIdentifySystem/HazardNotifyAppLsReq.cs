using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class HazardNotifyAppLsReq
    {

        public string searchType { set; get; }
        public string factory { set; get; }
        public string unit { set; get; }
        public string factoryBuilding { set; get; }
        public string floor { set; get; }
        public string docNo { set; get; }
        public string docStatus { set; get; }
        public string checkEmpNo { set; get; }
        public string checkEmpName { set; get; }
        public string startTime { set; get; }
        public string endTime { set; get; }

        public int page { set; get; }
        public int pageSize { set; get; }
    }
}