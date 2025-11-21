using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
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
   
    public class HIAISaftyApiController : HIBaseApiController
    {

        [HttpPost]
        public HIReturnMessageModel<string> UploadImageOfNoSafeForAnalysis( )
        {
            return HIAISaftyBusiness.Instance.UploadImageOfNoSafeForAnalysis();
                
        }

    }
}


