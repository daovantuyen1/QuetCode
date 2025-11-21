using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
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

    public class HIPicApiController : HIBaseApiController
    {
        /// <summary>
        /// Get danh sach Pic (paging)
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="factoryBuilding"></param>
        /// <param name="floor"></param>
        /// <param name="factory"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<ElTableRes> GetPicLs(GetPicLsReq req)

        {
            return HIPicBusiness.Instance.GetPicLs(req.unit, req.factoryBuilding, req.floor, req.factory, req.search, req.page, req.pageSize);

        }

        /// <summary>
        /// them/sua/xoa pic
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        [HICustomAuthenApi(Role1 = "Admin;Quyền cài đặt người phụ trách xử lý thông báo")]
        public HIReturnMessageModel<object> ModifyPic(ModifyPicReq dat)
        {
            return HIPicBusiness.Instance.ModifyPic(dat);
        }

        /// <summary>
        /// lay ra danh sach pic emp cua khu vuc
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="unit"></param>
        /// <param name="factoryBuilding"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        [HttpGet]

        public HIReturnMessageModel<List<V_HI_C_PIC_ZONE>> GetPicEmpLsByWhere(string factory, string unit, string factoryBuilding, string floor)
        {
            return HIPicBusiness.Instance.GetPicEmpLsByWhere(factory, unit, factoryBuilding, floor);
        }

        /// <summary>
        /// lay ra danh sach pic boss emp cua khu vuc
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="unit"></param>
        /// <param name="factoryBuilding"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_PIC_ZONE>> GetPicBossEmpLsByWhere(string factory, string unit, string factoryBuilding, string floor)
        {
            return HIPicBusiness.Instance.GetPicBossEmpLsByWhere(factory, unit, factoryBuilding, floor);
        }

    }
}
