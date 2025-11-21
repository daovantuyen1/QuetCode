using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class TB_APP_SCRAP_MEDICINE_DETAIL
    {
        public string F_ROW_ID { set; get; }
        public string F_SCRAP_DOC_NUM { set; get; }
        public string F_FACTORYZONE { set; get; }
        public string F_MEDICINE_ID { set; get; }
        public string F_MEDICINE_NAME_VI { set; get; }
        public string F_MEDICINE_NAME_EN { set; get; }
        public string F_MEDICINE_OBJ_UNIT_VI { set; get; }
        public string F_MEDICINE_OBJ_UNIT_EN { set; get; }
        public string F_MEDICINE_EXPIRED_DATE { set; get; }
        public int F_QTY_SCRAP { set; get; }
        public string F_REQUEST_DATE { set; get; }
        public string F_CREATEDATE { set; get; }
        public string F_CREATEUSER { set; get; }
        public string F_SYSDATE { set; get; }
        public string F_SYSID { set; get; }
        public string F_CSCRAP_DOC_NUM { set; get; }
        
    }

    public class V_TB_APP_SCRAP_MEDICINE_DETAIL : TB_APP_SCRAP_MEDICINE_DETAIL
    {

        public string F_MEDICINE_NAME { set; get; }
        public string F_MEDICINE_OBJ_UNIT { set; get; }

    }
}