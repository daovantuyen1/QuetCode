using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HRM
{
    public class HRMResponse<T>
    {
        public StatusType status { set; get; } = StatusType.success; // 0: normal , 1: error
        public string message { set; get; }
        public T data { set; get; }
    }

    public enum StatusType
    {
        success = 0,
        error = 1
    }

}