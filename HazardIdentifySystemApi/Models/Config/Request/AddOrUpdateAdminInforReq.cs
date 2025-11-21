using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Config.Request
{
    public class AddOrUpdateAdminInforReq
    {
        public string rowID { set; get; }
        public string factoryZone { set; get; }
        public string empName { set; get; }
        public string emPhone { set; get; }
        public string empMobile { set; get; }
        public string empEmail { set; get; }
        public bool isAddNew { set; get; }
    }
}