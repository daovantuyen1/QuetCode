using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class TB_FILE_MONTHLY
    {

        public string F_MONTH_YEAR { set; get; }
        public string F_EMPNO { set; get; }
        public string F_EMPNAME { set; get; }
        public string F_FACTORYZONE { set; get; }
        public string F_FILE_ID { set; get; }
        public string F_FILE_EX { set; get; }
        public string F_FILE_DESC { set; get; }
        public string F_CREATED_DATE { set; get; }
        public string F_CREATED_EMP { set; get; }
    }
}