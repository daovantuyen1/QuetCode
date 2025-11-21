using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// BANG LUU THONG TIN FILE DINH KEM OR REMARK DINH KEM CUA BANG R_APPLY_ORDER.
    /// </summary>
    [Table("R_APPLY_ORDER_ATTACH")]
    public class HI_R_APPLY_ORDER_ATTACH
    {


        public string F_SIGN_STATION_NO { set; get; }
        public string F_SIGN_STATION_NAME { set; get; }
        public string F_APPLY_NO { set; get; }
        public string F_FILE_ID { set; get; }
        public string F_REMARK { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }

    }
}