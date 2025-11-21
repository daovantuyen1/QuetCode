using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class HILoginReq
    {
        public string UserName { set; get; }
        public string PassWord { set; get; }
    }
}