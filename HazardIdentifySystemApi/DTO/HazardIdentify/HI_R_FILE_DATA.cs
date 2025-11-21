using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// LUU THONG TIN FILE UPLOAD
    /// </summary>
    [Table("R_FILE_DATA")]
    public class HI_R_FILE_DATA
    {
        public string F_FILE_ID { set; get; }
        public string F_FILE_NAME { set; get; }
        public string F_FILE_EXT { set; get; }
        public string F_FILE_SIZE { set; get; }
        public string F_FILE_PATH { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
    }
}