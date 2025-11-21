using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class GetPicLsReq
    {
        public string unit { set; get; }
        public string factoryBuilding { set; get; }
        public string floor { set; get; }
        public string factory { set; get; }
        public string search { set; get; }
        public int page { set; get; }
        public int pageSize { set; get; }
    }
}