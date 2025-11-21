using HazardIdentifySystemApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Routing.Response
{
    public class GetTotalInforOfAnApplicationForIssuanceRes
    {
        public string receiveDocNum { set; get; }
        public string receiveUnit { set; get; }
        public string FactoryZone { set; get; }
        /// <summary>
        /// Ghi chu thong tin da nhan thuoc cua ng lam don
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        ///  Cho phep hien thi giao dien nhap Ghi chu thong tin da nhan thuoc cua ng lam don  : 
        /// </summary>
        public bool isAllowRemark { set; get; }

        public List<medicineExport> medicineExportLs { set; get; }

        /// <summary>
        /// node don dang doi ky la tram dau tien trong luu trinh ky
        /// </summary>
        public bool isFirstStation { set; get; }

        /// <summary>
        /// Danh sach tat ca cac tram ky cua dau don
        /// </summary>
        public List<V_C_SIGN_STATION> signStationLs { set; get; }
        /// <summary>
        /// trang thai hien tai cua dau don
        /// </summary>

        public R_APPLY_ORDER rApplyOrder { set; get; }
        /// <summary>
        /// Danh sach cac file  dinh kiem cua app
        /// </summary>
        public List<string> fileLs { set; get; }
    }



    public class GetTotalInforOfAnApplicationForScrapRes
    {
        public string scrapDocNum { set; get; }
        public string FactoryZone { set; get; }

        public List<medicineExportForScrap> medicineExportLs { set; get; }

        /// <summary>
        /// node don dang doi ky la tram dau tien trong luu trinh ky
        /// </summary>
        public bool isFirstStation { set; get; }

        /// <summary>
        /// Danh sach tat ca cac tram ky cua dau don
        /// </summary>
        public List<V_C_SIGN_STATION> signStationLs { set; get; }

        /// <summary>
        /// trang thai hien tai cua don
        /// </summary>
        public R_APPLY_ORDER rApplyOrder { set; get; }

    }





    public class GetTotalInforOfAnApplicationForReturnWH
    {
        public string returnDocNum { set; get; }
        public string FactoryZone { set; get; }
      


        public List<medicineExportForReturn> medicineExportLs { set; get; }

        /// <summary>
        /// node don dang doi ky la tram dau tien trong luu trinh ky
        /// </summary>
        public bool isFirstStation { set; get; }

        /// <summary>
        /// Danh sach tat ca cac tram ky cua dau don
        /// </summary>
        public List<V_C_SIGN_STATION> signStationLs { set; get; }
        /// <summary>
        /// trang thai hien tai cua dau don
        /// </summary>

        public R_APPLY_ORDER rApplyOrder { set; get; }

    }


}