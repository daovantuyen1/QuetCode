using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class HIReturnMessageModel<T>
    {
        public string status { set; get; } = HIStatusType.success.ToString();
        public string message { set; get; }
        public int count { set; get; } = 0;
        public T data;
    }

    public class HIReturnMessage
    {
        public static HIReturnMessageModel<T> HIReturnSuccess<T>(string message,T data= default, int count =0)
        {
            return new HIReturnMessageModel<T>
            {
                message= message,
                data = data,
                status= HIStatusType.success.ToString(),
                count= count,

            };
        }

        public static HIReturnMessageModel<T> HIReturnError<T>(string message, T data= default , int count = 0)
        {
            return new HIReturnMessageModel<T>
            {
                message = message,
                data = data,
                status = HIStatusType.error.ToString(),
                count = count,
            };
        }
    }

    public enum HIStatusType
    {
        success=0,
        error=1,
    }

}