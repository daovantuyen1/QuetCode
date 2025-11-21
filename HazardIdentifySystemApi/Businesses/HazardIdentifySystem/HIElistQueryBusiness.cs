using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIElistQueryBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIElistQueryBusiness() { }
        private static HIElistQueryBusiness _instance;
        public static HIElistQueryBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIElistQueryBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public ELIST_USER_INFOR GetEmpInforFromSV(string empNo)
        {

            try
            {
                //   ElistQuerySV.ElistQuery sv = new ElistQuerySV.ElistQuery();
                // var ds = sv.ByUserID(empNo);
                PostmanSv.PostmanService sv1 = new PostmanSv.PostmanService();
                sv1.Url = Constant.POSTMAN_URL;
                // bypass https
                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                //
                var dt = sv1.GetEmpInfomation(empNo?.Trim()?.ToUpper());

                if (dt.CheckDtValid())
                {
                    var r = dt.Rows[0];
                    var empInfor = new ELIST_USER_INFOR
                    {
                        USER_ID = r["USER_ID"]?.ToString()?.Trim()?.ToUpper(),
                        USER_NAME = r["USER_NAME"]?.ToString()?.Trim(),
                        USER_NAME_EXT = r["USER_NAME_EXT"]?.ToString()?.Trim(),
                        SEX = r["SEX"]?.ToString()?.Trim(),
                        GRADE = r["GRADE"]?.ToString()?.Trim(),
                        JOB_TITLE = r["JOB_TITLE"]?.ToString()?.Trim(),
                        CURRENT_OU_CODE = r["CURRENT_OU_CODE"]?.ToString()?.Trim(),
                        CURRENT_OU_NAME = r["CURRENT_OU_NAME"]?.ToString()?.Trim(),
                        UPPER_OU_CODE = r["UPPER_OU_CODE"]?.ToString()?.Trim(),
                        NOTES_ID = r["NOTES_ID"]?.ToString()?.Trim(),
                        EMAIL = !string.IsNullOrWhiteSpace(r["NOTES_ID"]?.ToString()) ? r["NOTES_ID"]?.ToString()?.Trim() : (!string.IsNullOrWhiteSpace(r["EMAIL"]?.ToString()) ? r["EMAIL"]?.ToString()?.Trim() : ""),
                        ALL_MANAGERS = r["ALL_MANAGERS"]?.ToString()?.Trim(),
                        SITE_ALL_MANAGERS = r["SITE_ALL_MANAGERS"]?.ToString()?.Trim(),
                        BU_ALL_MANAGERS = r["BU_ALL_MANAGERS"]?.ToString()?.Trim(),
                        USER_LEVEL = r["USER_LEVEL"]?.ToString()?.Trim(),
                        HIREDATE = r["HIREDATE"]?.ToString()?.Trim(),
                        LEAVEDAY = r["LEAVEDAY"]?.ToString()?.Trim(),
                        JOB_TYPE = r["JOB_TYPE"]?.ToString()?.Trim(),
                        CL_ID = r["CL_ID"]?.ToString()?.Trim(),
                        CL_NAME = r["CL_NAME"]?.ToString()?.Trim(),
                        LOCATION = r["LOCATION"]?.ToString()?.Trim(),
                        CARD_ID = r["CARD_ID"]?.ToString()?.Trim(),
                        UPPER_OU_NAME = r["UPPER_OU_NAME"]?.ToString()?.Trim(),
                        NOTDUTY = r["NOTDUTY"]?.ToString()?.Trim(),
                        TRAVEL = r["TRAVEL"]?.ToString()?.Trim(),
                        USER_ID_EXT = r["USER_ID_EXT"]?.ToString()?.Trim(),
                        ASSISTANT_ID = r["ASSISTANT_ID"]?.ToString()?.Trim(),
                        USER_BU = r["USER_BU"]?.ToString()?.Trim(),
                        USER_NAME1 = r["USER_NAME1"]?.ToString()?.Trim(),
                        USER_IDCARD = r["USER_IDCARD"]?.ToString()?.Trim(),
                    };

                    if(empInfor.LEAVEDAY.StartsWith("9999")==false)  //emp da nghi viec
                    {
                        throw new Exception(LangHelper.Instance.Get("Nhân viên {0} đã nghỉ việc vào ngày {1} [Elist Service]", empInfor.USER_ID, empInfor.LEAVEDAY));
                    }

                   return empInfor;

                }
                else
                {
                    throw new Exception(LangHelper.Instance.Get("Không có dữ liệu nhân viên [Elist Service]"));
                }
              
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112613] exception detail:[{ex.Message + ex.StackTrace}]");

                throw new Exception(ex.Message);
            }

        }

   
        /// <summary>
        /// lay ra thong tin nguoi ky thay the hop le cua nguoi hien tai
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        public ELIST_USER_INFOR GetAgentOfEmp(string empNo)
        {

            try
            {
                var curempInfor = GetEmpInforFromSV(empNo);
                if (curempInfor.NOTDUTY?.ToUpper() == "YES" || curempInfor.TRAVEL?.ToUpper() == "YES") // nguoi ky chinh hien tai dang nghi-> hop le ng ky thay the
                {

                    //   ElistQuerySV.ElistQuery sv = new ElistQuerySV.ElistQuery();
                    // var ds = sv.ByUserID(empNo);
                    PostmanSv.PostmanService sv1 = new PostmanSv.PostmanService();
                    sv1.Url = Constant.POSTMAN_URL;
                    // bypass https
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    //
                    var dt = sv1.GetEmp_Agent(empNo);

                    if (dt.CheckDtValid())
                    {
                        var r = dt.Rows[0];
                        var empInfor = new AGENT_INFOR
                        {
                            AGENT_LINE = r["AGENT_LINE"]?.ToString()?.Trim(),
                            AGENT_OU = r["AGENT_OU"]?.ToString()?.Trim(),
                            AGENT_ORDER = r["AGENT_ORDER"]?.ToString()?.Trim(),
                            AGENT_WHO = r["AGENT_WHO"]?.ToString()?.Trim()?.ToUpper(),
                            NOTDUTY = r["NOTDUTY"]?.ToString()?.Trim()?.ToUpper(),
                            TRAVEL = r["TRAVEL"]?.ToString()?.Trim()?.ToUpper(),
                        };

                        if (!string.IsNullOrWhiteSpace(empInfor.AGENT_WHO))
                        {
                            var agentInfor = GetEmpInforFromSV(empInfor.AGENT_WHO);
                            return agentInfor;
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;

        }


        public HIReturnMessageModel<ELIST_USER_INFOR> GetEmpInfor(string empNo)
        {
            try
            {
                if (empNo.isNullStr())
                    HIReturnMessage.HIReturnError<ELIST_USER_INFOR>(LangHelper.Instance.Get("Không bỏ trống mã thẻ"), null);

                var empinforSV = GetEmpInforFromSV(empNo?.Trim()?.ToUpper());
                return HIReturnMessage.HIReturnSuccess<ELIST_USER_INFOR>(HIStatusType.success.ToString(), empinforSV);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112649] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<ELIST_USER_INFOR>(ex.Message, null);
            }
           

        }

    }
}