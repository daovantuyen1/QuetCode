using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class DocumentLsReq
    {

        public string empNo { set; get; }
        public string empName { set; get; }
        public string fileName { set; get; }
        public string searchType { set; get; }
        public string BU { set; get; }
        public int page { set; get; }
        public int pageSize { set; get; }

    }
}