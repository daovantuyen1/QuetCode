using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class ModifyAccountReq
    {
        public string empNo { set; get; }
        public string empName { set; get; }
        public string empMail { set; get; }
        public string empDept { set; get; }
        public string empPhone { set; get; }
        
        public List<string> empPermission { set; get; }

        public List<string> empBU { set; get; }



        public string modifyType { set; get; }
    }
}