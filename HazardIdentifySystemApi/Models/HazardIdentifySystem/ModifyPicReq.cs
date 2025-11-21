using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class ModifyPicReq
    {
        public string id { set; get; }
        public string factory { set; get; }
        public string unit { set; get; }
        public string factoryBuilding { set; get; }
        public string floor { set; get; }
        public string picEmpNo { set; get; }
        public string picEmpName { set; get; }
        public string picEmpMail { set; get; }
        public string picEmpPhone { set; get; }

        public string picBossEmpNo { set; get; }
        public string picBossEmpName { set; get; }
        public string picBossEmpMail { set; get; }

        public string mailCC { set; get; }
        public string bigBossMails { set; get; }
        public string modifyType { set; get; }




    }
}