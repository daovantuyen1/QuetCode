using Dapper;
using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{


    public class HIMailBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIMailBusiness() { }
        private static HIMailBusiness _instance;
        public static HIMailBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIMailBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion




        public HI_C_MAIL_TEMPLATE GetMailTemplate(string templateName)
        {
            return HIDbHelper.Instance.GetDBCnn()
                 .QueryFirstOrDefault<HI_C_MAIL_TEMPLATE>(@"
                    SELECT [ROW_ID]
                          ,[TEMPLATE_NAME]
                          ,[MAIL_SUBJECT]
                          ,[MAIL_CONTENT]
                          ,[DATA1]
                          ,[DATA2]
                          ,[DATA3]
                          ,[CREATE_EMP]
                          ,[CREATE_TIME]
                          ,[UPDATE_EMP]
                          ,[UPDATE_TIME]
                      FROM [dbo].[C_MAIL_TEMPLATE]
                      WHERE TEMPLATE_NAME  = @TEMPLATE_NAME
                    ", new
                 {
                     TEMPLATE_NAME = templateName
                 });

        }



        public string SendMail(MailDataView maildata)
        {
            try
            {
                if (Constant.HI_IS_SEND_MAIL == "Y")
                {
                    if (maildata != null)
                    {
                        //bypass https
                        Commons.Util.ByPassHttps();
                        //
                        using (var httpClient = new HttpClient())
                        {
                            var json = JsonConvert.SerializeObject(maildata);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            var responseTask = httpClient.PostAsync(Commons.Constant.HI_API_SEND_MAIL, content);
                            Task.WaitAll(responseTask);
                            var response = responseTask.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var taskrs = response.Content.ReadAsStringAsync();
                                Task.WaitAll(taskrs);
                                string rs = taskrs.Result;
                                HILogging.Instance.SaveLog($"Call mail api success,call result:{rs},mailData:{JsonConvert.SerializeObject(maildata)}");
                                return rs;

                            }
                            else
                            {
                                HILogging.Instance.SaveLog($"Call mail api fail, mailData:{JsonConvert.SerializeObject(maildata)}");

                            }
                        }



                    }

                }

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"Send mail fail, exception:{ex.Message},mailData:{JsonConvert.SerializeObject(maildata)}");
                return ex.Message;
            }
            return "";

        }


        public void SendNotifyToIcivet(string toEmpNo,string message)
        {
            try
            {
                if (Constant.HI_IS_SEND_MAIL == "Y")
                {
                    if(toEmpNo.isNullStr()==false && message.isNullStr()==false)
                    {
                        //bypass https
                        Commons.Util.ByPassHttps();
                        //
                        using (var httpClient = new HttpClient())
                        {
                            var json = JsonConvert.SerializeObject(new RequestApiIcivet
                            {

                                Content = message,
                                ToCivetNo = toEmpNo,
                            });

                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            var responseTask = httpClient.PostAsync(Commons.Constant.HI_API_SEND_ALERT_ICIVET, content);
                            Task.WaitAll(responseTask);
                            var response = responseTask.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var taskrs = response.Content.ReadAsStringAsync();
                                Task.WaitAll(taskrs);
                                string rs = taskrs.Result;
                                var rsObj = JsonConvert.DeserializeObject<ResponseApiIcivet>(rs);
                                if (rsObj.Response == "true")
                                {
                                    HILogging.Instance.SaveLog($"SendNotifyToIcivet success, toEmpNo:{toEmpNo},content:{message} ");

                                }
                                else
                                {
                                    HILogging.Instance.SaveLog($"SendNotifyToIcivet fail error:{JsonConvert.SerializeObject(rsObj)}, toEmpNo:{toEmpNo},content:{message} ");

                                }


                            }
                            else
                            {

                                HILogging.Instance.SaveLog($"SendNotifyToIcivet fail, toEmpNo:{toEmpNo},content:{message} ");

                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"SendNotifyToIcivet fail, exception:{ex.Message},toEmpNo:{toEmpNo},content:{message} ");

            }
         
        }


        /// <summary>
        /// Gui mail nhac nho don thong bao moi nguy
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="signEmpNo"></param>
        /// <param name="signEmpName"></param>
        /// <param name="toMail"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> SendMailHazardNotifyAppWaitingSign(string docNo, string signEmpName, string toMail,string signEmpNo,string CCMail)
        {
            try
            {

                var template = GetMailTemplate("Đơn thông báo mối nguy đợi ký");
                if (template == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Chưa thiết lập mail template"), null);
                if (toMail.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("toMail trống"), null);

                var maildata = new MailDataView()
                {
                    MAIL_FROM = Commons.Constant.HI_MAIL_FROM,
                    MAIL_TO = toMail,
                    MAIL_CC = CCMail, 
                    IMPORTANT_LEVEL = "HIGH",
                    IS_HTML = "YES",
                    MAIL_SUBJECT = template.MAIL_SUBJECT.Replace("{docNo}", docNo),
                    MAIL_BODY = template.MAIL_CONTENT.Replace("{empName}", signEmpName)
                                 .Replace("{docNo}", docNo).Replace("{url}", Constant.HI_LINK_SIGN),
                    UserName = "Useraxjtbk",
                    PassWord = "P@ssWordx!!!"
                };
                SendMail(maildata);
                SendNotifyToIcivet(signEmpNo, $@"
                 [SAFETY AI]
                 Bạn có đơn thông báo mối nguy {docNo} chờ bạn ký – Vui lòng truy cập hệ thống kiểm tra.
                 您有一份危险通知单 {docNo} 等待您签署 - 请访问系统进行检查
                ");

                return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405131644] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnError<object>(ex.Message, null);


            }


        }


    }
}