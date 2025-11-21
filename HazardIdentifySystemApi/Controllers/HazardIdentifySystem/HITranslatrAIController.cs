using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HazardIdentifySystemApi.Controllers.HazardIdentifySystem
{

 
    public class HITranslatrAIController : HIBaseApiController
    {
        [HttpPost]
        public HIReturnMessageModel<string> TranslateContent(TranslateContentReq req)
        {
            return HITranslatrAIBusiness.Instance.TranslateContent(req);
        }
    }
}
