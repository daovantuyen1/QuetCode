using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// BANG LUU THONG TIN NG KY CUA  NEU TRAM Y TAI BANG C_SIGN_STATION LA FIX MA THE NG KY CO DINH(F_CONFIG_FLAG= Y)
    /// </summary>
    [Table("C_SIGN_MANAGER")]
    public class HI_C_SIGN_MANAGER
    {

        public string F_ROW_ID { set; get; }
        public string F_SIGN_STATION_ID { set; get; }
        public string F_EMP_NO { set; get; }
        public string F_EMP_NAME { set; get; }
        public string F_MAIL { set; get; }
        public string F_POSITION { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }
    }
}