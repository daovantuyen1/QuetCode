using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace HazardIdentifySystemApi.Commons
{
    public static class Util
    {


        public static double ConvertBytesToMB(long bytes)
        {
            const double bytesPerMB = 1024 * 1024; // 1 MB = 1024 * 1024 bytes
            return  (double)Math.Round(bytes / bytesPerMB, 2);
            
        }

        /// <summary>
        /// Kiem tra string is null 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool isNullStr(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static void ByPassHttps()
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }
        public static string GetIp()
        {
            try
            {
                string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return ip;
            }
            catch
            {
               
            }
            return "";
           
        }

        public static  bool CheckDateStrIsValid(this string DateStr)
        {
            try
            {
                DateTime.ParseExact(DateStr, "yyyy/MM/dd", new CultureInfo("en-US"));
                return true;
            }
            catch 
            {

                return false;
            }
           
        }

        public static string GetRandomString()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string strRandom = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            return DateTime.Now.ToString("yyyyMMddHHmmssff") + strRandom;
        }


        public static bool CheckDateTimeStrIsValid(this string DateTimeStr)
        {
            try
            {
                DateTime.ParseExact(DateTimeStr, "yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"));
                return true;
            }
            catch
            {

                return false;
            }
        }

        public static string AppendNewLineHtml(this string input)
        {
            return $"<span> {input} </span> <br/>" ;

        }

        public static bool CheckDsValid(this DataSet ds)
        {
            if (ds == null) return false;
            if (ds.Tables == null) return false;
            if (ds.Tables[0].Rows == null) return false;
            if (ds.Tables[0].Rows.Count <= 0) return false;
            return true;
        }
        /// <summary>
        /// Check DataTable is Valid?
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool CheckDtValid(this DataTable dt)
        {
            if (dt == null) return false;
            if (dt.Rows == null) return false;
            if (dt.Rows.Count <= 0) return false;
            return true;
        }


        public static bool IsEmailValid(string emailAddress)
        {
            // Kiểm tra xem chuỗi có rỗng hoặc null không
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            try
            {
                // Sử dụng lớp MailAddress để phân tích và xác thực địa chỉ email
                // Nếu địa chỉ không hợp lệ, nó sẽ ném ra một FormatException
                MailAddress m = new MailAddress(emailAddress);

                // Nếu không có lỗi, tức là email có cú pháp hợp lệ
                return true;
            }
            catch (FormatException)
            {
                // Bắt ngoại lệ nếu chuỗi không phải là định dạng email hợp lệ
                return false;
            }
            catch (Exception ex)
            {
                // Bắt các ngoại lệ khác có thể xảy ra (ví dụ: ArgumentNullException nếu emailAddress là null,
                // nhưng chúng ta đã kiểm tra nó ở trên)
                // Trong thực tế, bạn có thể muốn log ex.Message ở đây.
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false;
            }
        }


    }
}