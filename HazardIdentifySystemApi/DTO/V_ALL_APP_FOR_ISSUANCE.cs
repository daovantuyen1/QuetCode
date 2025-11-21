using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{

    public class V_ALL_APP_FOR_RETURN 
    {
        public string F_RETURN_DOC_NUM { set; get; }
        public string APPLY_EMP { set; get; }
        public string APPLY_NAME { set; get; }
        public string APPLY_TIME { set; get; }
        public string F_STATUS { set; get; }
        public string F_SYSDATE { set; get; }
        public string F_SYSID { set; get; }
        public string F_FACTORYZONE { set; get; }
        
    }


    public class V_ALL_APP_FOR_ISSUANCE
    {
        public string F_RECEIVE_DOC_NUM { set; get; }
        public string APPLY_EMP { set; get; }
        public string APPLY_NAME { set; get; }
        public string F_RECEIVE_UNIT { set; get; }
        public string APPLY_TIME { set; get; }
        public string F_STATUS { set; get; }
        public string F_SYSDATE { set; get; }
        public string F_SYSID { set; get; }
        public string F_FACTORYZONE { set; get; }
    }

   

    public class V_ALL_APP_FOR_SCRAP
    {
        public string F_SCRAP_DOC_NUM { set; get; }
        public string APPLY_EMP { set; get; }
        public string APPLY_NAME { set; get; }

        public string APPLY_TIME { set; get; }

        public string F_STATUS { set; get; }

        public string F_SYSDATE { set; get; }

        public string F_SYSID { set; get; }
        public string F_FACTORYZONE { set; get; }

        
    }



    public class V_ALL_APP_FOR_CSCRAP
    {
        public string F_CSCRAP_DOC_NUM { set; get; }
        public string APPLY_EMP { set; get; }
        public string APPLY_NAME { set; get; }

        public string APPLY_TIME { set; get; }

        public string F_STATUS { set; get; }

        public string F_SYSDATE { set; get; }

        public string F_SYSID { set; get; }
        public string F_FACTORYZONE { set; get; }

        
    }
}