using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// bang luu cai dat he thong
    /// </summary>
    [Table("C_NODE_VALUE")]
    public class HI_C_NODE_VALUE
    {
        public string F_ROW_ID { set; get; }
        public string F_NODE_NAME { set; get; }
        public string F_NODE_VALUE { set; get; }
        public string F_NODE_DESC { set; get; }
        public string F_FATHER_NODE { set; get; }
        public int? F_SORT { set; get; }
        public string F_DATA1 { set; get; }
        public string F_DATA2 { set; get; }
        public string F_DATA3 { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }
        public string F_ALLOW_DELETE { set; get; }
        
    }

    public class V_HI_C_NODE_VALUE: HI_C_NODE_VALUE
    {
      
        public List<V_HI_C_NODE_VALUE> children { set; get; }
    }
}