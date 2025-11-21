using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.Models;
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

    public class HIDocumentApiController : HIBaseApiController
    {

        /// <summary>
        /// Them/xoa file cua Bu
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HICustomAuthenApi(Role1 = "Admin;Quyền quản lý tài liệu cấp BU;Quyền quản lý tài liệu tham khảo")]
        [HttpPost]

        public HIReturnMessageModel<object> ModifyDocument(ModifyDocumentReq dat)
        {
            return HIDocumentBusiness.Instance.ModifyDocument(dat);

        }


        /// <summary>
        /// Lay ra danh sach cac tai lieu 
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<ElTableRes> GetDocumentLs(DocumentLsReq dat)
        {
            return HIDocumentBusiness.Instance.GetDocumentLs(dat);
        }
    }
}
