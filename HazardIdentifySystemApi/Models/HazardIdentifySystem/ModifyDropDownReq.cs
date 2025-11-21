using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class ModifyCNodeValueReq
    {
        /// <summary>
        /// id node cha
        /// </summary>
        public string fatherNodeId { set; get; }
        /// <summary>
        /// // ten node cha
        /// </summary>
        public string fatherNodeName { set; get; }
        /// <summary>
        ///  id node hien tai
        /// </summary>
        public string curNodeId { set; get; }
        /// <summary>
        /// ten node hien tai
        /// </summary>
        public string curNodeName { set; get; }
        /// <summary>
        /// gia tri node hien tai
        /// </summary>
        public string curNodeValue { set; get; }
        /// <summary>
        /// mo ta chua nang node hien tai
        /// </summary>
        public string curNodeDesc { set; get; }
        /// <summary>
        /// so thu tu node hien tai
        /// </summary>
        public int? curNodeSort { set; get; }
        /// <summary>
        /// loai hinh modify la gi : EnumModifyType
        /// 
        /// </summary>
        public string modifyType { set; get; }
    }
    public enum EnumModifyType
    {
        Delete = 4,
        Edit=5,
        Add=6,
    }

}