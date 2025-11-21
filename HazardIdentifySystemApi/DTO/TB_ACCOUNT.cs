using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class TB_ACCOUNT
    {
        public string F_EMPNO { set; get; }
        public string F_EMPNAME { set; get; }
        public string F_EMPNAME_VI { set; get; }
        public string F_EMPPHONE { set; get; }
        public string F_EMPMOBILE { set; get; }
        public string F_EMPMAIL { set; get; }
        public string F_DEPT { set; get; }
        public string F_DEPTNAME { set; get; }
        public string F_FACTORYZONE { set; get; }
        public List<string> F_FACTORYZONE_LS { set; get; } = new List<string>();
        public string F_LEGAL { set; get; }
        public string F_FEECODE { set; get; }
        public string F_PASS { set; get; }
        public string F_ROLE { set; get; }
        public List<string> F_ROLE_LS { set; get; } = new List<string>();
        public string F_SYSDATE { set; get; }
        public string F_SYSID { set; get; }

    }
}