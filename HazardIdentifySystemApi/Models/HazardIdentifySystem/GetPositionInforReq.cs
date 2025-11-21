using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class GetPositionInforReq
    {

        public string factory { set; get; }
        public string unit { set; get; }
        public string factoryBuilding { set; get; }
        public string typePosition { set; get; }
    }
}