using HazardIdentifySystemApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Routing
{
    public class SignApplicationForIssuanceReq
    {
        public string receiveDocNum { set; get; }
        public string receiveUnit { set; get; }
        public string FactoryZone { set; get; }
        public List<V_C_SIGN_STATION> signStationLs { set; get; }
        public string remark1 { set; get; }
        /// <summary>
        /// FirstStation , BeforeStation
        /// </summary>
        public string RejectStation { set; get; }
        public bool? isReject { set; get; } = null;
        public List<medicineExport> medicineExportLs { set; get; }

        public List<string> fileLs { set; get; }

    }

    public class medicineExport
    {
        public string F_MEDICINE_ID { set; get; }
        public string F_MEDICINE_NAME { set; get; }
        public string F_MEDICINE_NAME_VI { set; get; }
        public string F_MEDICINE_NAME_EN { set; get; }
        public string F_MEDICINE_OBJ_UNIT { set; get; }
        public string F_MEDICINE_OBJ_UNIT_VI { set; get; }
        public string F_MEDICINE_OBJ_UNIT_EN { set; get; }
        public int F_QTY_REQUEST { set; get; }
        public string F_REQUEST_DATE { set; get; }
        public int F_QTY_RECEIVED { set; get; }
        public string F_RECEIVE_DOC_NUM { set; get; }
        public string F_FACTORYZONE { set; get; }
        public string F_RECEIVED_DATE { set; get; }
        public string F_RECEIVED_EMP { set; get; }
    }


    public class SignApplicationForScrapReq
    {
        public string scrapDocNum { set; get; }
        public string FactoryZone { set; get; }
        public List<V_C_SIGN_STATION> signStationLs { set; get; }
        public string remark { set; get; }
        /// <summary>
        /// FirstStation , BeforeStation
        /// </summary>
        public string RejectStation { set; get; }
        public bool? isReject { set; get; } = null;
        public List<medicineExportForScrap> medicineExportLs { set; get; }

    }


    public class medicineExportForScrap
    {
        public string F_MEDICINE_ID { set; get; }
        public string F_MEDICINE_NAME { set; get; }
        public string F_MEDICINE_NAME_VI { set; get; }
        public string F_MEDICINE_NAME_EN { set; get; }
        public string F_MEDICINE_OBJ_UNIT { set; get; }
        public string F_MEDICINE_OBJ_UNIT_VI { set; get; }
        public string F_MEDICINE_OBJ_UNIT_EN { set; get; }
        public string F_MEDICINE_EXPIRED_DATE { set; get; }
        public int F_QTY_SCRAP { set; get; }
        public string F_REQUEST_DATE { set; get; }
        public string F_SYSDATE { set; get; }
        public string F_CSCRAP_DOC_NUM { set; get; }

    }



    public class SignApplicationForCSrapReq
    {
        public string scrapDocNum { set; get; }
        /// <summary>
        /// ma dau don xin xac nhan bao phe
        /// </summary>
        public string CscrapDocNum { set; get; }
        /// <summary>
        /// // danh sach cac thuoc can xin xac nhan bao phe
        /// </summary>
        public List<V_TB_APP_SCRAP_MEDICINE_DETAIL> scrapMedinceLs { set; get; }
        public List<V_C_SIGN_STATION> signStationLs { set; get; }
        public string FactoryZone { set; get; }
        public string remark { set; get; }
        /// <summary>
        /// FirstStation , BeforeStation
        /// </summary>
        public string RejectStation { set; get; }
        public bool? isReject { set; get; } = null;

        public List<string> fileLs { set; get; }

    }



    public class medicineExportForReturn
    {
        public string F_MEDICINE_ID { set; get; }
        public string F_MEDICINE_NAME { set; get; }
        public string F_MEDICINE_NAME_VI { set; get; }
        public string F_MEDICINE_NAME_EN { set; get; }
        public string F_MEDICINE_OBJ_UNIT { set; get; }
        public string F_MEDICINE_OBJ_UNIT_VI { set; get; }
        public string F_MEDICINE_OBJ_UNIT_EN { set; get; }
        public int F_QTY_RETURN { set; get; }
        public string F_MEDICINE_EXPIRED_DATE { set; get; }
        public string F_FACTORYZONE { set; get; }
        public string F_CREATEDATE { set; get; }
        public string F_CREATEUSER { set; get; }
        public string F_RETURN_DOC_NUM { set; get; }

    }


    public class SignApplicationForReturnWHReq
    {
        public string returnDocNum { set; get; }
        public string FactoryZone { set; get; }
        public List<V_C_SIGN_STATION> signStationLs { set; get; }
        public string remark1 { set; get; }
        /// <summary>
        /// FirstStation , BeforeStation
        /// </summary>
        public string RejectStation { set; get; }
        public bool? isReject { set; get; } = null;
        public List<medicineExportForReturn> medicineExportLs { set; get; }

    }

}