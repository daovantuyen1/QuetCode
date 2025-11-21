using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HazardIdentifySystemApi.Controllers.HazardIdentifySystem
{

    public class HIConfigApiController : HIBaseApiController
    {

        [HttpGet]
        public HIReturnMessageModel<ElTableRes> GetCNodeValueLS(string search, int page, int pageSize)
        {
            return HIConfigBusiness.Instance.GetCNodeValueLS(search, page, pageSize);
        }



        [HICustomAuthenApi(Role1 = "Admin;Quyền cài đặt dữ liệu cơ bản")]
        [HttpPost]
        public HIReturnMessageModel<object> ModifyCNodeValue(ModifyCNodeValueReq dat)
        {
            return HIConfigBusiness.Instance.ModifyCNodeValue(dat);

        }

        [HttpGet]
        public HIReturnMessageModel<HI_C_NODE_VALUE> GetOneCNodeValueByRowID(string rowId)
        {
            return HIConfigBusiness.Instance.GetOneCNodeValueByRowID(rowId);

        }


        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetUnitLs()
        {
            return HIConfigBusiness.Instance.GetUnitLs();
        }


        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetFactoryBuildingLs()
        {
           
            return HIConfigBusiness.Instance.GetFactoryBuildingLs();
        }

        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetTypeOfHazardLs()
        {
            return HIConfigBusiness.Instance.GetTypeOfHazardLs();
        }


        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetFloorLs()
        {
            return HIConfigBusiness.Instance.GetFloorLs();
        }

        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetDangerLevelLs()
        {
            return HIConfigBusiness.Instance.GetDangerLevelLs();
        }


        [HttpGet]
        public HIReturnMessageModel<string> GetSysDate()
        {
            return HIConfigBusiness.Instance.GetSysDate();

        }


        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetFactoryLs()
        {
            return HIConfigBusiness.Instance.GetFactoryLs();
        }

        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetHandleStatusLs()
        {
            return HIConfigBusiness.Instance.GetHandleStatusLs();
        }
        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetPriorityLevel()
        {
            return HIConfigBusiness.Instance.GetPriorityLevel();
        }


        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetPermissionNameLs()
        {
            return HIConfigBusiness.Instance.GetPermissionNameLs();
        }

        /// <summary>
        /// Tra ve danh sach cac Factory+building+unit+floor
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<List<HI_C_PIC_ZONE>> GetFactoryInforLs(string search)
        {
            return HIConfigBusiness.Instance.GetFactoryInforLs(search);

        }


    


        /// <summary>
        /// Lay ra thong tin Vị trí phát hiện mối nguy   
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<List<string>> GetPositionInfor(GetPositionInforReq dat)
        {
            return HIConfigBusiness.Instance.GetPositionInfor(dat);
        }


        /// <summary>
        /// Lay ra cai dat infor gop y
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetCommentConfigInfor()
        {
            return HIConfigBusiness.Instance.GetCommentConfigInfor();
        }
    }
}
