using HazardIdentifySystemApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Routing.Response
{
    public class GetTotalInforOfAnApplicationForCScrapRes
    {
        public string CscrapDocNum { set; get; }
        public string FactoryZone { set; get; }

        public List<V_TB_APP_SCRAP_MEDICINE_DETAIL> scrapMedinceLs { set; get; }

        /// <summary>
        /// node don dang doi ky la tram dau tien trong luu trinh ky
        /// </summary>
        public bool isFirstStation { set; get; }

        /// <summary>
        /// Danh sach tat ca cac tram ky cua dau don
        /// </summary>
        public List<V_C_SIGN_STATION> signStationLs { set; get; }

        /// <summary>
        /// thong tin hien tai cua don
        /// </summary>
        public R_APPLY_ORDER rApplyOrder { set; get; }

        public List<string> fileLs { set; get; }
        
    }
}