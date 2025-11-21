using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class SaveRemarkOfCheckerReq
    {
        public string docNo { set; get; }
        public string checkRemark { set; get; }
    }



    public class SaveRemarkOfHandlerReq
    {
        public string docNo { set; get; }
        public string inchargeRemark { set; get; }

    }

    public class SavePositionDetailOfCheckerReq
    {
        public string docNo { set; get; }
        public string positionDetail { set; get; }

    }


    public class UpdateHandlerForHazardAppReq
    {

        public string inchargeEmpNo { set; get; }
        public string inchargeEmpName { set; get; }
        public string inchargeEmpDept { set; get; }
        public string inchargeEmpMail { set; get; }
        public string docNo { set; get; }

    }
}