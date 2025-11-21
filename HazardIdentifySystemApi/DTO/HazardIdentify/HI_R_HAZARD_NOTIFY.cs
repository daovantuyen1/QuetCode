using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    [Table("R_HAZARD_NOTIFY")]
    public class HI_R_HAZARD_NOTIFY
    {
        public string F_DOCNO { set; get; }
        public string F_DOC_STATUS { set; get; }
        public string F_CHECK_EMPNO { set; get; }
        public string F_CHECK_EMPNAME { set; get; }
        public string F_CHECK_EMPDEPT { set; get; }
        public string F_CHECK_EMPMAIL { set; get; }
        
        public string F_CHECK_REMARK { set; get; }

        public string F_FACTORY { set; get; }
        public string F_UNIT { set; get; }
        public string F_FACTORY_BUILDING { set; get; }
        public string F_FLOOR { set; get; }
        public string F_CHECKTIME { set; get; }
        public string F_DANGER_TYPE { set; get; }
        public string F_DANGER_LEVEL { set; get; }
        public string F_IMPROVEMENT_DAY { set; get; }
        public string F_IMAGE_ID { set; get; }
        public string F_FIX_IMAGE_ID { set; get; }
        public string F_INCHARGE_EMPNO { set; get; }
        public string F_INCHARGE_EMPNAME { set; get; }
        public string F_INCHARGE_EMPMAIL { set; get; }
        
        public string F_INCHARGE_EMPDEPT { set; get; }

        public string F_INCHARGE_EMPREMARK { set; get; }
        public string F_PIC_BOSS_EMPNO { set; get; }
        public string F_PIC_BOSS_NAME { set; get; }

        public string F_PIC_BOSS_MAIL { set; get; }


        public string F_HAZARD_DESC { set; get; }
        public string F_HAZARD_BASIC_STANDARD { set; get; }
        public string F_IMPROVE_COUNTER_MEASURE { set; get; }
        public string F_HAZARD_AI_CONTENT { set; get; }

        
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }
        public string F_APPLY_NO { set; get; }
        public string F_DOC_TYPE { set; get; }
        public string F_PROJECT_NAMES { set; get; }


        public string F_HANDLE_STATUS { set; get; }
        public string F_PRIORITY_LEVEL { set; get; }
        public string F_SIGN_EMP { set; get; }
        public string F_SIGN_NAME { set; get; }
        public string F_SIGN_MAIL { set; get; }
        public string F_POSITION_DETAIL { set; get; }

    }
}