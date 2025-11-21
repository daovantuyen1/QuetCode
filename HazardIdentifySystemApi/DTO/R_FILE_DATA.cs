using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class R_FILE_DATA
    {
        public string F_ROW_ID { set; get; }
        public string F_FILE_NAME { set; get; }

        public string F_FILE_TYPE { set; get; }

        public string F_CREATEDATE { set; get; }

        public string F_CREATEUSER { set; get; }

        /// <summary>
        /// ip cua user up file
        /// </summary>
        public string F_IP { set; get; }

        /// <summary>
        /// Loai dau don 
        /// </summary>
        public string F_APP_TYPE { set; get; }
    }

    public class V_R_FILE_DATA : R_FILE_DATA
    {
        public string F_ISALLOW_DELETE { set; get; }

    }
}