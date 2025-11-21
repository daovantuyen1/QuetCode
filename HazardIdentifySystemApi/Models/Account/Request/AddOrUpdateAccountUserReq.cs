using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Account.Request
{
    public class AddOrUpdateAccountUserReq
    {
        public string empNo { set; get; }
        public string empName { set; get; }
        public string empMail { set; get; }
        public bool managefilemonthly { set; get; }
        public List<string> factoryLs { set; get; }
        public bool isAddNew { set; get; }


    }
    public class AddOrUpdateAccountAdminReq
    {
        public string empNo { set; get; }
        public string empName { set; get; }
        public string empMail { set; get; }
        public List<string> factoryLs { set; get; }
        public bool isAddNew { set; get; }
    }
}