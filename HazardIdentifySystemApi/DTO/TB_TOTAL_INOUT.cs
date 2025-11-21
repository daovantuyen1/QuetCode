using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class TB_TOTAL_INOUT
    {

        public string F_ROW_ID { set; get; }
        public string F_MEDICINE_ID { set; get; }
        public string F_MEDICINE_NAME_VI { set; get; }
        public string F_MEDICINE_NAME_EN { set; get; }
        public string F_MEDICINE_EXPIRED_DATE { set; get; }
        public string F_MEDICINE_OBJ_UNIT_VI { set; get; }
        public string F_MEDICINE_OBJ_UNIT_EN { set; get; }
        public int F_TOTAL_QTY { set; get; }
        public string F_IN_QTY_HISTORY { set; get; }
        public string F_OUT_QTY_HISTORY { set; get; }
        public string F_FACTORYZONE { set; get; }
        public string F_SYSDATE { set; get; }
        public string F_SYSID { set; get; }
        public string F_CREATEDATE { set; get; }
        public string F_CREATEUSER { set; get; }

    }
}