using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Routing.Request
{
    public class SaveNoteReceivedGoodsOfAppForIssuanceReq
    {
        public  string receiveDocNum { set; get; }
         public string Remark { set; get; }
    }
}