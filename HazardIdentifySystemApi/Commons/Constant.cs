using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Commons
{
    public class Constant
    {
      
        public static readonly string KEY_ENCODE = "$quan9fu$KuDa3632689@deptra!vkl$%&";
        public static readonly string POSTMAN_URL = ConfigurationManager.AppSettings["POSTMAN_URL"]?.ToString();
      
        

        #region HazardIdentifySystemApi
        public static readonly string SQL_SERVER_DB_SECURITY = ConfigurationManager.AppSettings["SQL_SERVER_DB_SECURITY"]?.ToString();
        public static readonly string API_UPLOAD_IMAGE_AI = ConfigurationManager.AppSettings["API_UPLOAD_IMAGE_AI"]?.ToString();
        public static readonly string API_ANALYSIS_IMAGE_AI = ConfigurationManager.AppSettings["API_ANALYSIS_IMAGE_AI"]?.ToString();
        public static readonly string API_AI_AUTHEN_TOKEN = ConfigurationManager.AppSettings["API_AI_AUTHEN_TOKEN"]?.ToString();
        public static readonly string API_TRANSLATE = ConfigurationManager.AppSettings["API_TRANSLATE"]?.ToString();
        public static readonly string FILE_URL = ConfigurationManager.AppSettings["FILE_URL"]?.ToString();
        public static readonly string HI_IS_SEND_MAIL = ConfigurationManager.AppSettings["HI_IS_SEND_MAIL"]?.ToString();
        public static readonly string HI_API_SEND_MAIL = ConfigurationManager.AppSettings["HI_API_SEND_MAIL"]?.ToString();
        public static readonly string HI_MAIL_FROM = ConfigurationManager.AppSettings["HI_MAIL_FROM"]?.ToString();
        public static readonly string HI_LINK_SIGN = ConfigurationManager.AppSettings["HI_LINK_SIGN"]?.ToString();
        public static readonly string HI_API_SEND_ALERT_ICIVET = ConfigurationManager.AppSettings["HI_API_SEND_ALERT_ICIVET"]?.ToString();

        




        #endregion HazardIdentifySystemApi


    }
}