using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// BANG LUU THONG TIN TAI KHOAN
    /// </summary>
    [Table("TB_ACCOUNT")]
    public class HI_TB_ACCOUNT
    {
        public string F_EMPNO { set; get; }
        public string F_EMPNAME { set; get; }
        public string F_MAIL { set; get; }
        public string F_DEPT { set; get; }
        public string F_PHONE { set; get; }
        
        public string F_ROLE { set; get; }
        public string F_DATA1 { set; get; }
        public string F_DATA2 { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }

    }

    public class V_HI_TB_ACCOUNT : HI_TB_ACCOUNT
    {
        public List<HI_R_ACCOUNT_PERMISSION> F_ROLES { set; get; } = new List<HI_R_ACCOUNT_PERMISSION>();

        public List<HI_R_ACCOUNT_BU> F_BUS { set; get; } = new List<HI_R_ACCOUNT_BU>();

        public string token { set; get; }

    }
}