using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{

    public class HITranslatrAIBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HITranslatrAIBusiness() { }
        private static HITranslatrAIBusiness _instance;
        public static HITranslatrAIBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HITranslatrAIBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public HIReturnMessageModel<string> TranslateContent(TranslateContentReq data)
        {
            try
            {
                string type = data.type;
                string text = data.text;
                // type in : English  中文繁體 Tiếng Việt
               if (new[] { "English", "中文繁體", "Tiếng Việt" }.Contains(type) == false)
                 return HIReturnMessage.HIReturnError<string>("type param invalid, it only belongs English, 中文繁體, Tiếng Việt", null);
               if(string.IsNullOrWhiteSpace(text))
                 return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Vui lòng nhập văn bản cần dịch"), null);

                Util.ByPassHttps();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Important: Accept JSON
                    client.Timeout = TimeSpan.FromSeconds(15);
                    var requestBody = new
                    {
                        text= text,
                        type= type,
                        sysid= "670ce5f1901c8026c92de4ce",
                        user= "V1030398"
                    };

                    string json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var responseTask = client.PostAsync(Constant.API_TRANSLATE, content);
                    responseTask.Wait();
                    var response = responseTask.Result;
                    var responseContentTask = response.Content.ReadAsStringAsync();
                    responseContentTask.Wait();
                    var responseContent = responseContentTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var rtDat = JObject.Parse(responseContent);
                        if (!string.IsNullOrWhiteSpace(rtDat["data"]?.ToString()))
                        {
                           
                           return  HIReturnMessage.HIReturnSuccess<string>(HIStatusType.success.ToString(), rtDat["data"].ToString());
                        }
                        else
                        { // error 
                            return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Không thể dịch") , null);
                        }

                    }
                    else
                    {
                        return HIReturnMessage.HIReturnError<string>(response.Content.ToString(), null);
                    }
                }
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-2025040513012301] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnError<string>(ex.Message, null);
            }

        }


    }
}

