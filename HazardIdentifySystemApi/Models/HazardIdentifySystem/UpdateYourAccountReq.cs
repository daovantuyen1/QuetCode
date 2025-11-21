using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class UpdateYourAccountReq
    {
        public string empNo { set; get; }
        public string empName { set; get; }
        public string empMail { set; get; }
        public string empDept { set; get; }
        public string empPhone { set; get; }
    }
}