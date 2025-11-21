using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Routing.Request
{
    public class ConfirmReceivedMedicineForAppIssuanceReq
    {
        public string rowID { set; get; }
        public string Remark { set; get; }

    }
}