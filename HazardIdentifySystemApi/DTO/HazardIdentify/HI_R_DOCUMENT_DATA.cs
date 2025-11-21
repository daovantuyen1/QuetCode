using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    [Table("R_DOCUMENT_DATA")]
    public class HI_R_DOCUMENT_DATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string F_ROW_ID { set; get; }
        public string F_EMPNO { set; get; }
        public string F_EMPNAME { set; get; }
        public string F_EMPDEPT { set; get; }
        /// <summary>
        /// ,  -- THUOC LOAI TAI LIEU NAO ?: 'BU',  'REFERENCES'
        /// </summary>
        public string F_TYPE { set; get; }
        /// <summary>
        ///  -- NEU  F_TYPE= BU THI F_BU LA BU CU THE 
        /// </summary>
        public string F_BU { set; get; }
        /// <summary>
        /// -- GHI CHU TAI LIEU UP LEN
        /// </summary>
        public string F_REMARK { set; get; }
        /// <summary>
        /// tham chieu bang R_FILE_DATA
        /// </summary>
        public string F_FILE_ID { set; get; }
        public string CREATE_EMP { set; get; }
        public string CREATE_TIME { set; get; }
    }

    public class V_HI_R_DOCUMENT_DATA : HI_R_DOCUMENT_DATA
    {
        public string F_FILE_NAME { set; get; }
        public string F_FILE_EXT { set; get; }
        public string F_FILE_SIZE { set; get; }
    }
}