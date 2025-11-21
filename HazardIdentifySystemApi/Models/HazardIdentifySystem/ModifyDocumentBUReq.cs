using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class ModifyDocumentReq
    {
        public string fileID { set; get; }
        public string fileRemark { set; get; }
        public string BU { set; get; }
        /// <summary>
        //THUOC LOAI TAI LIEU NAO ?: 'BU',  'REFERENCES'
        /// </summary>
        public string type { set; get; }
        public string modifyType { set; get; }

    }
}