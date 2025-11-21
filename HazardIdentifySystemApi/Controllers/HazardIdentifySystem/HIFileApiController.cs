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
   
    public class HIFileApiController : HIBaseApiController
    {

        [HttpPost]
        public HIReturnMessageModel<string> UploadFile()
        {
            return HIFileBusiness.Instance.UploadFile();

        }
        [HttpGet]
        public HIReturnMessageModel<object> DeleteFile(string fileId)
        {
            return HIFileBusiness.Instance.DeleteFile(fileId);
        }

        [HttpGet]
        public HIReturnMessageModel<HI_R_FILE_DATA> GetFile(string fileId)
        {
            return HIFileBusiness.Instance.GetFile(fileId);
        }
    }
}
