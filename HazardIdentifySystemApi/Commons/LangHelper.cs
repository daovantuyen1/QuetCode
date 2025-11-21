using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace HazardIdentifySystemApi.Commons
{
    public class LangHelper
    {
        private JObject viObj;
        private JObject enObj;
        private JObject cnObj;
        #region SingelTon
        private static object lockObj = new object();
        private LangHelper()
        {
            try
            {
                string phisicPath = HttpContext.Current.Server.MapPath("~/Lang/");
                string viPath = phisicPath + "vn.json";
                if (System.IO.File.Exists(viPath))
                {
                    string viStr = System.IO.File.ReadAllText(viPath);
                    if (!string.IsNullOrWhiteSpace(viStr))
                        viObj = JObject.Parse(viStr);
                }

                string enPath = phisicPath + "en.json";
                if (System.IO.File.Exists(enPath))
                {
                    string enStr = System.IO.File.ReadAllText(enPath);
                    if (!string.IsNullOrWhiteSpace(enStr))
                        enObj = JObject.Parse(enStr);
                }

                string cnPath = phisicPath + "cn.json";
                if (System.IO.File.Exists(cnPath))
                {
                    string cnStr = System.IO.File.ReadAllText(cnPath);
                    if (!string.IsNullOrWhiteSpace(cnStr))
                        cnObj = JObject.Parse(cnStr);
                }

            }
            catch (Exception ex)
            {

            }

        }
        private static LangHelper _instance;
        public static LangHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LangHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion


        public string Get(string key, params object[] paramsLs)
        {
            try
            {
                string requestLang = HttpContext.Current.Request?.Cookies["language"]?.Value;
                if (string.IsNullOrWhiteSpace(requestLang)
                    || new List<string> { "vi-VN", "en-US", "zh-TW" }.Contains(requestLang) == false)
                    requestLang = "vi-VN";
                string result = "";
                switch (requestLang)
                {
                    case "vi-VN": result = viObj[key]?.ToString(); break;
                    case "en-US":
                        result = enObj[key]?.ToString(); break;
                    case "zh-TW":
                        result = cnObj[key]?.ToString(); break;
                    default:
                        result = viObj[key]?.ToString();
                        break;
                }
                if (string.IsNullOrWhiteSpace(result))
                    result = key;

                if (paramsLs != null && paramsLs.Count() > 0)
                {
                    result = string.Format(result, paramsLs);
                }

                return result;

            }
            catch (Exception ex)
            {
                string result = key;
                if (paramsLs != null && paramsLs.Count() > 0)
                {
                    result = string.Format(result, paramsLs);
                }
                return result;
            }



        }

        public string TranslateApi(string content, string keyLang,ref string err)
        {
            string contentRsTrs = content;
            err = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Base address of the API
                    client.BaseAddress = new Uri("https://hrm.cns.myfiinet.com:8019/oa_api/api/PGOpenAi/Seach");

                    // Parameters to be sent in the query string
                    var parameters = new Dictionary<string, string>
            {
                { "text", content?.Trim() },
                { "type", keyLang  },  // "中文繁體" 
                { "sysid", "670ce5f1901c8026c92de4ce" },
                { "user", "V1030398" },
            };

                    // Construct the query string
                    string queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));

                    // Make the GET request
                    var responseTask = client.GetAsync($"?{queryString}");
                    responseTask.Wait();
                    HttpResponseMessage response = responseTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Read and process the response
                        var responseDataTask = response.Content.ReadAsStringAsync();
                        responseDataTask.Wait();
                        string responseData = responseDataTask.Result;
                        var rsTranslate= JsonConvert.DeserializeObject<TranslateAIRes>(responseData);
                        contentRsTrs = rsTranslate.data;
                    }
                    else
                    {
                        err = response.StatusCode.ToString();

                    }
                   
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return contentRsTrs;
        }
    }

    public class TranslateAIRes
    {
        public string data { set; get; }
    }
}