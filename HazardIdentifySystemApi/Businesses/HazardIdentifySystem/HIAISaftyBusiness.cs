using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIAISaftyBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIAISaftyBusiness() { }
        private static HIAISaftyBusiness _instance;
        public static HIAISaftyBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIAISaftyBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public HIReturnMessageModel<string> UploadImageOfNoSafeForAnalysis()
        {

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Form != null)
            {

                string lang = httpRequest.Form["lang"]?.ToString();

                if ((httpRequest.Files != null && httpRequest.Files.Count > 0) == false)
                    return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Không có file gửi lên"));
                string filePath = "";
                try
                {
                    string fileId = Commons.Util.GetRandomString();
                    var curFile = httpRequest.Files[0];
                    if (curFile.ContentLength > 9437184)  // 9mb
                    {
                        return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Kích thước file tối đa tải lên là 9mb"));

                    }
                    if (new[]
                        {
                            "image/jpeg",
                            "image/png" ,
                            "image/gif",
                        }.Contains(curFile.ContentType) == false)
                    {
                        return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Chỉ được tải lên file ảnh"));

                    }

                    string extension = curFile.FileName.Substring(curFile.FileName.LastIndexOf(".")).ToLower();
                    filePath = HttpContext.Current.Server.MapPath("~/TempFiles/" + fileId + extension);
                    curFile.SaveAs(filePath);
                    var returnUloadImg = UploadImageToAI(filePath);
                    if (returnUloadImg.status == HIStatusType.error.ToString())
                    {
                        return HIReturnMessage.HIReturnError<string>(returnUloadImg.message);
                    }
                    string upload_file_id = returnUloadImg.data;
                    if (string.IsNullOrWhiteSpace(upload_file_id))
                        return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Có lỗi trong quá trình phân tích ảnh"));

                    var returnAnalysisImage = AnalysisImageFromAI(upload_file_id, lang);
                    if (returnAnalysisImage.status == HIStatusType.error.ToString())
                    {
                        return HIReturnMessage.HIReturnError<string>(returnAnalysisImage.message);
                    }
                    string textAnalysis = returnAnalysisImage.data;
                    //var returnPareAnalysisResult =  PareAnalysisResult(textAnalysis);
                    //if (returnPareAnalysisResult.status == HIStatusType.error.ToString())
                    //{
                    //    return HIReturnMessage.HIReturnError<AnalysisResult>(returnPareAnalysisResult.message, null);
                    //}

                    //var rsallLine = new List<string>(textAnalysis.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    //                 .Where(r => !string.IsNullOrWhiteSpace(r)).ToList();


                    return HIReturnMessage.HIReturnSuccess<string>("success", textAnalysis);

                }
                catch (Exception ex)
                {
                    HILogging.Instance.SaveLog($"[ERR-20250405112052] exception detail:[{ex.Message + ex.StackTrace}]");

                    return HIReturnMessage.HIReturnError<string>(ex.Message);
                }
                finally
                {

                    try
                    {
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                    catch
                    {

                    }


                }


            }
            else
                return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Không có file ảnh được tải lên"));
        }

        public HIReturnMessageModel<string> UploadImageToAI(string filePath)
        {
            try
            {
                Util.ByPassHttps();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constant.API_AI_AUTHEN_TOKEN);
                    client.Timeout = TimeSpan.FromMinutes(5);
                    using (var formData = new MultipartFormDataContent())
                    {
                        // Add the username to the form data
                        formData.Add(new StringContent("1"), "user"); // Add user as a string content

                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg"); // Adjust content type

                        formData.Add(fileContent, "file", Path.GetFileName(filePath));

                        var responseTask = client.PostAsync(Constant.API_UPLOAD_IMAGE_AI, formData);
                        responseTask.Wait();
                        var response = responseTask.Result;
                        var responseContentTask = response.Content.ReadAsStringAsync();
                        responseContentTask.Wait();
                        var responseContent = responseContentTask.Result;

                        if (response.IsSuccessStatusCode)
                        {

                            //sample return data:
                            //{
                            //    "id": "fb08b636-e151-4444-9554-814b2e3b1b42",
                            //    "name": "2025032013381838BXF8U.jpg",
                            //    "size": 11574,
                            //    "extension": "jpg",
                            //    "mime_type": "image/jpeg",
                            //    "created_by": "8c4de695-4a14-4a43-88fc-1f4f74ddbb6a",
                            //    "created_at": 1742423908
                            //}

                            var rtDat = JObject.Parse(responseContent);
                            return HIReturnMessage.HIReturnSuccess<string>(HIStatusType.success.ToString(), rtDat["id"].ToString());

                        }
                        else
                        {
                            return HIReturnMessage.HIReturnError<string>(response.Content.ToString());

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112115] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<string>(ex.Message);
            }

        }



        public HIReturnMessageModel<string> AnalysisImageFromAI(string upload_file_id, string lang)
        {
            try
            {
                Util.ByPassHttps();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constant.API_AI_AUTHEN_TOKEN);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Important: Accept JSON
                    client.Timeout = TimeSpan.FromMinutes(5);

                    //string requestLang = HttpContext.Current.Request?.Cookies["language"]?.Value;
                    if (string.IsNullOrWhiteSpace(lang)
                        || new List<string> { "vi-VN", "en-US", "zh-TW" }.Contains(lang) == false)
                        lang = "vi-VN";
                    string promtAi = "What is the risk of this photo and under what terms?";
                    switch (lang)
                    {
                        case "vi-VN":
                            promtAi = "ảnh trên có mối nguy nào và dựa theo điều khoản nào?"; break;
                        case "en-US":
                            promtAi = "What is the risk of this photo and under what terms?"; break;
                        case "zh-TW":
                            promtAi = "張照片的風險是什麼？在什麼條件下?"; break;
                    }


                    var requestBody = new
                    {
                        inputs = new
                        {
                            input = promtAi
                        }, // Empty object
                        response_mode = "blocking",
                        user = "1",
                        files = new[]
                        {
                        new
                        {
                            type = "image",
                            transfer_method = "local_file",
                            upload_file_id = upload_file_id
                        }
                    }
                    };


                    string json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var responseTask = client.PostAsync(Constant.API_ANALYSIS_IMAGE_AI, content);
                    responseTask.Wait();
                    var response = responseTask.Result;
                    var responseContentTask = response.Content.ReadAsStringAsync();
                    responseContentTask.Wait();
                    var responseContent = responseContentTask.Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var rtDat = JObject.Parse(responseContent);
                        if (rtDat["data"]["error"].HasValues == false)
                        {
                            var AnalysisTextResult = rtDat["data"]["outputs"]["text"].ToString();
                            return HIReturnMessage.HIReturnSuccess<string>(HIStatusType.success.ToString(), AnalysisTextResult);
                        }
                        else
                        { // error 
                            return HIReturnMessage.HIReturnError<string>(rtDat["data"]["error"].ToString());
                        }

                    }
                    else
                    {
                        return HIReturnMessage.HIReturnError<string>(response.Content.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112144] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<string>(ex.Message);
            }

        }


        public HIReturnMessageModel<AnalysisResult> PareAnalysisResult(string textResult)
        {
            string fileId = Commons.Util.GetRandomString();
            var filePath = HttpContext.Current.Server.MapPath("~/TempFiles/" + fileId + ".txt");
            try
            {
                File.WriteAllText(filePath, textResult);
                var allLine = File.ReadAllLines(filePath);

                var totalLS = new Dictionary<string, List<string>>();
                AnalysisResult analysisResult = new AnalysisResult();
                List<string> MainKeyLs = new List<string> {
                "**隐患描述：**",
                "**风险等级：**",
                "**解决方案：**",
                "**实施建议：**",
                "**具体问题及潜在风险：**",
            };
                //var beforeIsMainKey = false;
                var curMainKey = "";
                var curlsItempOfMainKey = new List<string>();

                StringBuilder tempStrBuilder = new StringBuilder();
                for (var i = 0; i < allLine.Length; i++)
                {
                    var item = allLine[i];
                    if (MainKeyLs.Contains(item))  // la de muc chinh
                    {
                        if (!string.IsNullOrWhiteSpace(curMainKey))  // mainkey before co du lieu -> add vao list tong
                        {

                            totalLS.Add(curMainKey.ToString(), curlsItempOfMainKey.ToArray().ToList());
                            curMainKey = "";
                            curlsItempOfMainKey.Clear();
                            tempStrBuilder.Clear();
                        }
                        //  beforeIsMainKey = true;
                        curMainKey = item;
                    }
                    else if (!string.IsNullOrWhiteSpace(curMainKey))
                    //cac muc nho
                    {

                        if (string.IsNullOrWhiteSpace(item))
                        {
                            if (!string.IsNullOrWhiteSpace(tempStrBuilder.ToString()))
                            {
                                curlsItempOfMainKey.Add(tempStrBuilder.ToString());
                                tempStrBuilder.Clear();
                            }
                            if (curlsItempOfMainKey.Count <= 0)
                                continue;

                        }
                        else
                        {   // co noi dung muc nho
                            tempStrBuilder.Append(item + Environment.NewLine);

                        }
                    }
                    if (i == allLine.Length - 2)  // main key cuoi
                    {
                        totalLS.Add(curMainKey.ToString(), curlsItempOfMainKey.ToArray().ToList());
                        curMainKey = "";
                        curlsItempOfMainKey.Clear();
                        tempStrBuilder.Clear();
                        break;
                    }


                }

                if (totalLS.Count <= 0)
                {
                    return HIReturnMessage.HIReturnError<AnalysisResult>("Error pare result");
                }

                if (totalLS.Keys.Contains("**隐患描述：**"))
                {
                    var HazardDescriptionLs = totalLS["**隐患描述：**"].ToList();
                    analysisResult.HazardDescription = new AnalysisResultKey
                    {
                        KeyMain = "隐患描述",
                        ItemLs = HazardDescriptionLs.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToList(),
                    };
                }

                if (totalLS.Keys.Contains("**风险等级：**"))
                {
                    var RiskLevelLs = totalLS["**风险等级：**"].ToList();
                    analysisResult.RiskLevel = new AnalysisResultKey
                    {
                        KeyMain = "风险等级",
                        ItemLs = RiskLevelLs.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToList(),
                    };
                }


                if (totalLS.Keys.Contains("**解决方案：**"))
                {
                    var SolutionLS = totalLS["**解决方案：**"].ToList();
                    analysisResult.Solution = new AnalysisResultKey
                    {
                        KeyMain = "解决方案",
                        ItemLs = SolutionLS.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToList(),
                    };
                }



                if (totalLS.Keys.Contains("**实施建议：**"))
                {
                    var lsImplementRecomment = totalLS["**实施建议：**"].ToList();
                    if (lsImplementRecomment.Count > 0)
                    {
                        var finalLsImplementRecomment = lsImplementRecomment[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                             .ToList();
                        analysisResult.ImplementationRecommendations = new AnalysisResultKey
                        {
                            KeyMain = "实施建议",
                            ItemLs = finalLsImplementRecomment.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToList(),
                        };

                    }
                }


                if (totalLS.Keys.Contains("**具体问题及潜在风险：**"))
                {
                    var SpecificIssuesPotentialRisksLS = totalLS["**具体问题及潜在风险：**"].ToList();
                    if (SpecificIssuesPotentialRisksLS.Count > 0)
                    {
                        var finalSpecificIssuesPotentialRisksLS = SpecificIssuesPotentialRisksLS[0].Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                             .ToList();
                        analysisResult.SpecificIssuesPotentialRisks = new AnalysisResultKey
                        {
                            KeyMain = "具体问题及潜在风险",
                            ItemLs = finalSpecificIssuesPotentialRisksLS.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToList(),
                        };

                    }
                }

                return HIReturnMessage.HIReturnSuccess<AnalysisResult>(HIStatusType.success.ToString(), analysisResult);




            }
            catch (Exception ex)
            {


                return HIReturnMessage.HIReturnError<AnalysisResult>(ex.Message);
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    {


                    }
                }
            }
            return null;


        }
    }
}
