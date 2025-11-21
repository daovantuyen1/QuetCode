using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class SaveSomeHazardInfor1Req
    {
        
         public string docNo { set; get; }
        /// <summary>
        ///  Loại hình mối nguy
        /// </summary>
        public string   dangerType { set; get; }
        /// <summary>
        ///   Mức độ nguy hiểm
        /// </summary>
        public string dangerLevel { set; get; }

        /// <summary>
        ///  Mức độ ưu tiên
        /// </summary>
        public string priorityLevel { set; get; }

        /// <summary>
        /// Phần kiểm tra này có thuộc dự án nào không
        /// </summary>
        public string docType { set; get; }
        /// <summary>
        /// ten cac du an
        /// </summary>

        public List<string>  projectNames { set; get; }

        /// <summary>
        ///  Thời gian phát hiện mối ngu
        /// </summary>
        public string checkTime { set; get; }

        /// <summary>
        ///  Mốc thời gian cải thien
        /// </summary>
        public string improvementDay { set; get; }
        /// <summary>
        /// noi dung do AI phan tich
        /// </summary>

        public string hazardAiContent { set; get; }

        /// <summary>
        /// tieu chuan can cu
        /// </summary>

        public string hazardBasisStandard { set; get; }


        /// <summary>
        ///  doi sach cai thien
        /// </summary>
        public string improvecountermeasures { set; get; }






    }
}