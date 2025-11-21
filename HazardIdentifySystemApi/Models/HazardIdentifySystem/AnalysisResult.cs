using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class AnalysisResult
    {
        /// <summary>
        /// **隐患描述：** : Mô tả mối nguy hiểm
        /// </summary>
        public AnalysisResultKey HazardDescription { set; get; }

        /// <summary>
        /// **风险等级：**: Mức độ rủi ro 
        /// </summary>
        public AnalysisResultKey RiskLevel { set; get; }

        /// <summary>
        /// **解决方案：** :Giải pháp
        /// </summary>
        public AnalysisResultKey Solution { set; get; }

        /// <summary>
        /// **实施建议：** : Khuyến nghị thực hiện
        /// </summary>
        public AnalysisResultKey ImplementationRecommendations { set; get; }

        /// <summary>
        ///  **具体问题及潜在风险：**Các vấn đề cụ thể và rủi ro tiềm ẩn
        /// </summary>
        public AnalysisResultKey SpecificIssuesPotentialRisks { set; get; }




    }

    public class AnalysisResultKey
    {
        public string KeyMain { set; get; }
        public List<string> ItemLs { set; get; }
    }
}