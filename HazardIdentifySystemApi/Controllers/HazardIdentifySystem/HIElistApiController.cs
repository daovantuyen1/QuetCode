using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO;
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


    public class HIElistApiController : HIBaseApiController
    {

        /// <summary>
        /// lay thong tin emp tu elist service
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<ELIST_USER_INFOR> GetEmpInfor(string empNo)
        {
            return HIElistQueryBusiness.Instance.GetEmpInfor(empNo);
        }
    }
}
