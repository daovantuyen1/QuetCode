using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace HazardIdentifySystemApi.Commons.HazardIdentifySystem
{

    /// <summary>
    /// Custom authen for web api
    /// </summary>
    public class HICustomAuthenApiAttribute : System.Web.Http.AuthorizeAttribute
    {
        private bool? isRoled = null;
        public string Role1 { set; get; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if ((HttpContext.Current?.Session != null && HttpContext.Current?.Session["Account"] != null)
                || HttpContext.Current.Request?.Headers?.GetValues("Token")?.Length > 0
                )
            {
                V_HI_TB_ACCOUNT acc = null;
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["Account"] != null)
                {
                    var acc1 = HttpContext.Current.Session["Account"] as V_HI_TB_ACCOUNT;
                    acc = HIAccountBusiness.Instance.GetAccountInfor(acc1.F_EMPNO);
                }
                else if (HttpContext.Current.Request?.Headers?.GetValues("Token")?.Length > 0)
                {
                    var token = HttpContext.Current.Request?.Headers?.GetValues("Token")?.FirstOrDefault();
                    if (token.isNullStr())
                        return false;
                    var plaintextToken = Aes128Encryption.Instance.Decrypt(token);
                    var tempArr = plaintextToken.Split(';').ToList();
                    string empNo = tempArr[0];
                    string createTime = tempArr[1];
                    var createTimeDate = DateTime.ParseExact(createTime, "yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"));
                    var sub = DateTime.Now - createTimeDate;

                    if (sub.Minutes <= 60 && sub.Minutes >= 0)  // thoi gian hop le cua token la 60 phut
                    {  // token hop le
                        acc = HIAccountBusiness.Instance.GetAccountInfor(empNo);
                    }
                    else
                    {
                        return false;
                    }
                }

                if (acc == null)
                {
                    isRoled = false;
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(Role1)) //need check role
                {
                    var roleNeedCheck = Role1?.Split(';').Where(r => !string.IsNullOrWhiteSpace(r)).Select(t => t.Trim()).ToList();
                    var roleAccount = acc.F_ROLES?.Select(r => r.F_ROLE?.Trim()).ToList();
                    //  acc.F_ROLE?.Split(';').Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim()).ToList();
                    if (roleAccount.Join(roleNeedCheck, x => x, y => y, (a, b) => a).Count() > 0)
                    {
                        isRoled = true;
                        return true;
                    }
                    else
                    {
                        isRoled = false;
                        return false;
                    }

                }
                else return true;  // no need check role
            }
            else
                return false;
        }

        //protected  bool OnAuthorization(HttpContextBase httpContext)
        //{
        //    if (HttpContext.Current.Session != null && HttpContext.Current.Session["Account"] != null)
        //    {
        //        var acc = HttpContext.Current.Session["Account"] as TB_ACCOUNT;
        //        if (!string.IsNullOrWhiteSpace(Role1)) //need check role
        //        {
        //            var roleNeedCheck = Role1?.Split(';').ToList();
        //            var roleAccount = acc.F_ROLE?.Split(';').ToList();
        //            if (roleAccount.Join(roleNeedCheck, x => x.ToUpper(), y => y.ToUpper(), (a, b) => a).Count() > 0)
        //            {
        //                isRoled = true;
        //                return true;
        //            }
        //            else
        //            {
        //                isRoled = false;
        //                return false;
        //            }

        //        }
        //        else return true;  // no need check role
        //    }
        //    else
        //        return false;
        //}


        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (isRoled == false)  // invalid role
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent(LangHelper.Instance.Get("Bạn không có quyền thao tác"))
                };
            }
            else
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("authorization has been denied for this request")
                };

            }

        }
    }

}