using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using HazardIdentifySystemApi.Models.Account.Request;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HazardIdentifySystemApi.Controllers.HazardIdentifySystem
{


    public class HIAccountApiController : HIBaseApiController
    {

        [HttpGet, AllowAnonymous]
        public IHttpActionResult TestA()
        {
            return Ok(new { name1 = "tuyen", age = 12 });
        }

        [HttpGet, AllowAnonymous]
        public HttpResponseMessage Test1()
        {

            try
            {


                DataTable dt = new DataTable();
                dt.Columns.Add("EMP NO");
                dt.Columns.Add("EMP NAME");


                dt.Rows.Add("v1030398", "tuyen");
                dt.Rows.Add("v1021214", "hung");


                var workbook = NPOIExceLHelper.WriteDataTableToExcelFile(@"D:\Test2.xlsx", dt, 0, 4);



                using (var exportData = new MemoryStream())
                {
                    workbook.Write(exportData);
                    string fileName = Util.GetRandomString() + ".xlsx";
                    byte[] bytes = exportData.ToArray();
                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(bytes)
                    };
                    result.Content.Headers.ContentDisposition =
                        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                        {
                            FileName = fileName
                        };
                    result.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;

                }
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(
                             HttpStatusCode.BadRequest,
                             "Invalid operation: " + ex.Message
                         );
                return errorResponse;

            }
        }





        [HttpGet, AllowAnonymous]
        public HIReturnMessageModel<V_HI_TB_ACCOUNT> Test()
        {

            //  public static IWorkbook WriteDataTableToWorkbook(DataTable dataTable, IWorkbook workbook, int sheetIndex, int startRowIndex, bool includeHeader = true, bool autoSizeColumns = true)


            var rs = NPOIExceLHelper.ReadDataFromExcelToDataTable(@"D:\Test.xlsx", 0, 2, 2);


            return null;
        }

        [HttpPost, AllowAnonymous]
        public HIReturnMessageModel<V_HI_TB_ACCOUNT> CheckLogin(HILoginReq hILoginReq)
        {

            var rs = HIAccountBusiness.Instance.CheckLogin(hILoginReq);
            return rs;

        }

        [HttpPost, AllowAnonymous]
        public HIReturnMessageModel<V_HI_TB_ACCOUNT> CheckLogin1(HILoginReq hILoginReq)
        {
            var rs = HIAccountBusiness.Instance.CheckLogin1(hILoginReq);
            return rs;
        }

        [HttpGet]
        public HIReturnMessageModel<V_HI_TB_ACCOUNT> CheckSession()
        {
            return HIAccountBusiness.Instance.CheckSession();
        }

        [HttpGet, AllowAnonymous]
        public HIReturnMessageModel<object> Logout()
        {
            return HIAccountBusiness.Instance.Logout();
        }


        [HICustomAuthenApi(Role1 = "Admin;Quyền cài đặt tài khoản")]
        [HttpGet]
        public HIReturnMessageModel<ElTableRes> GetAllAccount(string search, int page, int pageSize)
        {
            return HIAccountBusiness.Instance.GetAllAccount(search, page, pageSize);
        }
        [HICustomAuthenApi(Role1 = "Admin;Quyền cài đặt tài khoản")]
        [HttpPost]
        public HIReturnMessageModel<object> ModifyAccount(ModifyAccountReq dat)
        {
            return HIAccountBusiness.Instance.ModifyAccount(dat);
        }

        [HttpPost]
        public HIReturnMessageModel<object> UpdateYourAccount(UpdateYourAccountReq dat)
        {
            return HIAccountBusiness.Instance.UpdateYourAccount(dat);
        }



        [HttpPost, AllowAnonymous]
        public HIReturnMessageModel<V_HI_TB_ACCOUNT> LoginSSO(LoginSSOReq req)
        {
            return HIAccountBusiness.Instance.LoginSSO(req?.code);
        }

    }
}
