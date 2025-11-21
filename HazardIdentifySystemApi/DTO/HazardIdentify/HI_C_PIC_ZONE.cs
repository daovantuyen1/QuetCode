using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    [Table("C_PIC_ZONE")]
    public class HI_C_PIC_ZONE
    {

        public string F_ROW_ID { set; get; }
        public string  F_FACTORY { set; get; }
        public string F_UNIT { set; get; }
        public string F_FACTORY_BUILDING { set; get; }
        public string F_FLOOR { set; get; }
        public string F_PIC_EMPNO { set; get; }
        public string F_PIC_EMPNAME { set; get; }
        public string F_PIC_EMPMAIL { set; get; }
        public string F_PIC_PHONE { set; get; }


        public string F_PIC_BOSS_EMPNO { set; get; }
        public string F_PIC_BOSS_NAME { set; get; }
        public string F_PIC_BOSS_MAIL { set; get; }



        public string F_MAIL_CC { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }

        public string F_BIG_BOSS_MAIL { set; get; }
        

    }

    public class V_HI_C_PIC_ZONE : HI_C_PIC_ZONE
    {
        public string F_INCHARGE_EMPDEPT { set; get; }
    }
}