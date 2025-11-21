using Dapper;
using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIAccountBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIAccountBusiness() { }
        private static HIAccountBusiness _instance;
        public static HIAccountBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIAccountBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion



        public bool DeleteRAccountBU(string empNo, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                 .Execute(@"
                   DELETE FROM R_ACCOUNT_BU WHERE  F_EMPNO = @F_EMPNO
                    ", new { F_EMPNO = empNo?.Trim()?.ToUpper() }, trs) > 0 ? true : false;
        }


        public bool AddNewRAccountBU(HI_R_ACCOUNT_BU dat, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                 .Execute(@"
                  INSERT INTO R_ACCOUNT_BU
                               (
		                       ROW_ID
                               ,F_EMPNO
                               ,F_BU_NAME
                               ,CREATE_EMP
                               ,CREATE_TIME
		                       )
                         VALUES
                             (
		                        dbo.GET_NEW_ROWID(NEWID())
                               ,@F_EMPNO
                               ,@F_BU_NAME
                               ,@CREATE_EMP
                               ,GETDATE()
		                     )
                    ", dat, trs) > 0 ? true : false;
        }





        public bool AddNewAccountPermission(HI_R_ACCOUNT_PERMISSION dat, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                 .Execute(@"
                    INSERT INTO R_ACCOUNT_PERMISSION
                               (
		                       F_ROW_ID
                               ,F_EMPNO
                               ,F_ROLE
                               ,F_CREATE_EMP
                               ,F_CREATE_TIME
		                       )
                         VALUES
                                 ( 
		                        dbo.GET_NEW_ROWID(NEWID())
                               ,@F_EMPNO
                               ,@F_ROLE
                               ,@F_CREATE_EMP
                               ,GETDATE()
		                       )
                    ", dat, trs) > 0 ? true : false;
        }




        public bool DeleteAccountPermission(string empNo, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                 .Execute(@"
                    DELETE FROM R_ACCOUNT_PERMISSION  WHERE F_EMPNO =  @F_EMPNO
                    ", new { F_EMPNO = empNo?.Trim()?.ToUpper() }, trs) > 0 ? true : false;
        }


        public bool UpdateAccount(HI_TB_ACCOUNT dat, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                 .Execute(@"
                    UPDATE TB_ACCOUNT
                   SET 
                       F_EMPNAME= @F_EMPNAME
                      ,F_MAIL = @F_MAIL
                      ,F_DEPT= @F_DEPT
                      , F_UPDATE_EMP = @F_UPDATE_EMP
                      ,F_UPDATE_TIME = getdate()
                      ,F_PHONE =@F_PHONE
                 WHERE
                 F_EMPNO = @F_EMPNO

                    ", dat, trs) > 0 ? true : false;
        }



        public bool AddNewAccount(HI_TB_ACCOUNT dat, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                 .Execute(@"
                    INSERT INTO TB_ACCOUNT
                               ( 
		                        F_EMPNO
                               ,F_EMPNAME
                               ,F_MAIL
                               ,F_DEPT
                               ,F_ROLE
                               ,F_DATA1
                               ,F_DATA2
                               ,F_CREATE_EMP
                               ,F_CREATE_TIME
                               ,F_PHONE
		                       )
                         VALUES
	                       ( 
		                        @F_EMPNO
                               ,@F_EMPNAME
                               ,@F_MAIL
                               ,@F_DEPT
                               ,''
                               ,''
                               ,''
                               ,@F_CREATE_EMP
                               ,GETDATE()
                               ,@F_PHONE
		                       )
                    ", dat, trs) > 0 ? true : false;
        }

        public List<HI_R_ACCOUNT_PERMISSION> GetRAccountPermission(string empNo)
        {
            return HIDbHelper.Instance.GetDBCnn().Query<HI_R_ACCOUNT_PERMISSION>(@"
                        SELECT F_ROW_ID
                              ,F_EMPNO
                              ,F_ROLE
                              ,F_CREATE_EMP
	                         , FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_CREATE_TIME
                          FROM  R_ACCOUNT_PERMISSION
                             WHERE F_EMPNO =  @F_EMPNO
                             ORDER BY F_ROLE ASC   ",
                               new { F_EMPNO = empNo?.Trim()?.ToUpper() })
                               .ToList();

        }


        public List<HI_R_ACCOUNT_BU> GetRAccountBU(string empNo)
        {
            return HIDbHelper.Instance.GetDBCnn().Query<HI_R_ACCOUNT_BU>(@"
                                SELECT ROW_ID
                                      ,F_EMPNO
                                      ,F_BU_NAME
                                      ,CREATE_EMP
                                      , FORMAT( CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as CREATE_TIME  
                                  FROM R_ACCOUNT_BU
                                  WHERE F_EMPNO = @F_EMPNO
                                  ORDER BY F_BU_NAME ASC  
                                ",
                               new { F_EMPNO = empNo?.Trim()?.ToUpper() })
                               .ToList();

        }



        public V_HI_TB_ACCOUNT GetAccountByEmpNo(string empNo)
        {
            var acc = HIDbHelper.Instance.GetDBCnn()
              .QueryFirstOrDefault<V_HI_TB_ACCOUNT>(@"
                    SELECT [F_EMPNO]
                          ,[F_EMPNAME]
                          ,[F_MAIL]
                          ,[F_DEPT]
                          , F_PHONE
                          ,[F_ROLE]
                          ,[F_DATA1]
                          ,[F_DATA2]
                          ,[F_CREATE_EMP]
                          ,[F_CREATE_TIME]
                          ,[F_UPDATE_EMP]
                          ,[F_UPDATE_TIME]
                      FROM  [TB_ACCOUNT]  WHERE   [F_EMPNO] = @F_EMPNO
                     ", new
              {
                  F_EMPNO = empNo?.Trim()?.ToUpper()
              });
            return acc;

        }

        public V_HI_TB_ACCOUNT GetAccountInfor(string empNo)
        {
            var acc = GetAccountByEmpNo(empNo);

            var roleLs = GetRAccountPermission(empNo);
            var buLs = GetRAccountBU(empNo);

            ELIST_USER_INFOR empsvInfor = null;
            try
            {
                empsvInfor = HIElistQueryBusiness.Instance.GetEmpInforFromSV(empNo);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405111230] exception detail:[{ex.Message + ex.StackTrace}]");
            }

            if (acc != null)
            {
                if (empsvInfor != null)
                {
                    if (!string.IsNullOrWhiteSpace(empsvInfor?.EMAIL))
                    {
                        acc.F_MAIL = empsvInfor?.EMAIL;
                    }
                    if (!string.IsNullOrWhiteSpace(empsvInfor?.CURRENT_OU_NAME))
                    {
                        acc.F_DEPT = empsvInfor?.CURRENT_OU_NAME;
                    }
                    if (!string.IsNullOrWhiteSpace(empsvInfor?.USER_NAME))
                    {
                        acc.F_EMPNAME = empsvInfor?.USER_NAME;
                    }

                }

            }

            if (acc == null && empsvInfor != null)
            {

                acc = new V_HI_TB_ACCOUNT
                {
                    F_EMPNO = empsvInfor?.USER_ID,
                    F_EMPNAME = empsvInfor?.USER_NAME,
                    F_MAIL = empsvInfor?.EMAIL,
                    F_DEPT = empsvInfor?.CURRENT_OU_NAME,
                };

                AddNewAccount(acc);

            }

            var acc1 = GetAccountByEmpNo(empNo);

            if (empsvInfor != null)
            {

                UpdateAccount(new HI_TB_ACCOUNT
                {
                    F_EMPNO = empsvInfor?.USER_ID,
                    F_EMPNAME = !string.IsNullOrWhiteSpace(empsvInfor?.USER_NAME) ? empsvInfor?.USER_NAME : acc1?.F_EMPNAME,
                    F_MAIL = !string.IsNullOrWhiteSpace(empsvInfor?.EMAIL) ? empsvInfor?.EMAIL : acc1?.F_MAIL,
                    F_DEPT = !string.IsNullOrWhiteSpace(empsvInfor?.CURRENT_OU_NAME) ? empsvInfor?.CURRENT_OU_NAME : acc1?.F_DEPT,
                    F_PHONE = acc1?.F_PHONE,
                    F_UPDATE_EMP = acc1?.F_UPDATE_EMP,
                });

            }


            if (roleLs.Count > 0)
                acc.F_ROLES = roleLs;
            if (buLs.Count > 0)
                acc.F_BUS = buLs;
            return acc;

        }

        public V_HI_TB_ACCOUNT GetAccountFromSession()
        {
            //dang nhap tu dmz
            if (HttpContext.Current.Request?.Headers?.GetValues("Token")?.Length > 0)
            {
                var token = HttpContext.Current?.Request?.Headers?.GetValues("Token")?.FirstOrDefault();

                var plaintextToken = Aes128Encryption.Instance.Decrypt(token);
                var tempArr = plaintextToken.Split(';').ToList();
                string empNo = tempArr[0];
                var acc = GetAccountByEmpNo(empNo);
                return acc;
            }
            else
            {

                var acc = HttpContext.Current.Session["Account"] as V_HI_TB_ACCOUNT;
                return acc;
            }

        }

        /// <summary>
        /// Kiem tra login 
        /// </summary>
        /// <param name="hILoginReq"></param>
        /// <returns></returns>
        public HIReturnMessageModel<V_HI_TB_ACCOUNT> CheckLogin(HILoginReq hILoginReq)
        {
            try
            {
                if (hILoginReq == null
                  || string.IsNullOrWhiteSpace(hILoginReq.UserName)
                  || string.IsNullOrWhiteSpace(hILoginReq.PassWord))
                {
                    return HIReturnMessage
                         .HIReturnError<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Vui lòng nhập tài khoản và mật khẩu"));
                }
                var acc = GetAccountInfor(hILoginReq.UserName);
                if (acc != null)
                {

                    // token cho dang nhap tu dmz : mac dinh thoi gian song la 60 phut.
                    acc.token = Aes128Encryption.Instance.Encrypt(
                        $"{acc.F_EMPNO};{DateTime.Now.ToString("yyyy_MM_dd HH:mm:ss").Replace("_", "/")}");
                    HttpContext.Current.Session.Add("Account", acc);

                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã đăng nhập");
                    return HIReturnMessage.HIReturnSuccess<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Đăng nhập thành công"), acc);
                }
                else
                    return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Đăng nhập thất bại"));
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405111440] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(ex.Message);

            }


        }


        /// <summary>
        /// Kiem tra login : su dung cho test env
        /// </summary>
        /// <param name="hILoginReq"></param>
        /// <returns></returns>
        public HIReturnMessageModel<V_HI_TB_ACCOUNT> CheckLogin1(HILoginReq hILoginReq)
        {
            try
            {
                if (hILoginReq == null
                  || string.IsNullOrWhiteSpace(hILoginReq.UserName)
                  || string.IsNullOrWhiteSpace(hILoginReq.PassWord))
                {
                    return HIReturnMessage
                         .HIReturnError<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Vui lòng nhập tài khoản và mật khẩu"));
                }
                var acc = GetAccountInfor(hILoginReq.UserName);
                if (acc != null)
                {
                    var accSvInfor = HIElistQueryBusiness.Instance.GetEmpInforFromSV(acc.F_EMPNO);
                    var arrHirDate = accSvInfor.HIREDATE?.Split('/');
                    string yearHireDate = arrHirDate[0];
                    string monthHireDate = arrHirDate[1];
                    string dayHireDate = arrHirDate[2];

                    string mmDDSv = (monthHireDate.Length == 1 ? "0" + monthHireDate : monthHireDate)
                                   + (dayHireDate.Length == 1 ? "0" + dayHireDate : dayHireDate);
                    if (mmDDSv != hILoginReq.PassWord?.Trim())
                    {
                        return HIReturnMessage
                            .HIReturnError<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Đăng nhập thất bại"));

                    }

                    // token cho dang nhap tu dmz : mac dinh thoi gian song la 60 phut.
                    acc.token = Aes128Encryption.Instance.Encrypt(
                        $"{acc.F_EMPNO};{DateTime.Now.ToString("yyyy_MM_dd HH:mm:ss").Replace("_", "/")}");
                    HttpContext.Current.Session.Add("Account", acc);

                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã đăng nhập");
                    return HIReturnMessage.HIReturnSuccess<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Đăng nhập thành công"), acc);
                }
                else
                    return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Đăng nhập thất bại"));
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405111440] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(ex.Message);

            }


        }



        public HIReturnMessageModel<V_HI_TB_ACCOUNT> CheckSession()
        {
            try
            {
                var accSession = GetAccountFromSession();
                var acc = GetAccountInfor(accSession.F_EMPNO);
                return HIReturnMessage.HIReturnSuccess<V_HI_TB_ACCOUNT>(HIStatusType.success.ToString(), acc);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202504051115] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(ex.Message, null);
            }

        }

        public HIReturnMessageModel<object> Logout()
        {
            HttpContext.Current.Session.Remove("Account");
            return HIReturnMessage.HIReturnSuccess<object>(LangHelper.Instance.Get("Đăng xuất thành công"), null);
        }



        public HIReturnMessageModel<ElTableRes> GetAllAccount(string search, int page, int pageSize)
        {
            try
            {

                search = search.isNullStr() ? "" : search.Trim().ToLower();
                string columns = @"
                           F_EMPNO
                          ,F_EMPNAME
                          ,F_MAIL
                          ,F_DEPT
                          ,F_ROLE
                          ,F_DATA1
                          ,F_DATA2
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_CREATE_TIME 
                          ,F_PHONE
                          ,F_UPDATE_EMP
                          , FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_UPDATE_TIME 
                       ";
                string sql = @"
                        SELECT 
                        {0}
                        FROM 
                        TB_ACCOUNT
                        WHERE  1=1   
                        " +
                        $@"
                        {(search.isNullStr() ? "" : $" AND ( F_EMPNO LIKE N'%{search}%'  OR  F_EMPNAME LIKE N'%{search}%' OR  F_MAIL LIKE N'%{search}%'   )  ")}
                       "
                        ;
                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 5 : pageSize;

                int star_rownum = (page * pageSize) - pageSize + 1;
                int end_rownum = page * pageSize;

                string sql1 = $@"
                   SELECT 
                          F_EMPNO
                          ,F_EMPNAME
                          ,F_MAIL
                          ,F_DEPT
                          ,F_ROLE
                          ,F_DATA1
                          ,F_DATA2
                          ,F_CREATE_EMP
                          ,F_CREATE_TIME
                          ,F_PHONE
                            ,F_UPDATE_EMP
                            ,F_UPDATE_TIME
                    FROM (SELECT ROW_NUMBER() OVER (ORDER BY F_EMPNO ASC ) AS ROW_NUMBER, K.*
                          FROM (
						      {string.Format(sql, columns)}
						  )  K ) E
                    WHERE E.ROW_NUMBER BETWEEN {star_rownum} AND {end_rownum} 
              ";
                int totalCount = 0;
                var tempCount = HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault(string.Format(sql, " COUNT(1) AS CC"));
                if (tempCount != null)
                {

                    totalCount = (int)tempCount.CC;
                }

                var items = HIDbHelper.Instance.GetDBCnn().Query<V_HI_TB_ACCOUNT>(
                  sql1
                 ).ToList();

                if (items != null && items.Count > 0)
                {
                    items.ForEach(r =>
                    {
                        r.F_ROLES = GetRAccountPermission(r.F_EMPNO);
                        r.F_BUS = GetRAccountBU(r.F_EMPNO);
                    });


                }


                var dat = new ElTableRes()
                {
                    items = items,
                    totalCount = totalCount
                };

                return HIReturnMessage.HIReturnSuccess<ElTableRes>(HIStatusType.success.ToString(), dat);

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202504051115] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<ElTableRes>(ex.Message);

            }



        }


        public HIReturnMessageModel<object> DeleteAccount(string empNo)
        {
            try
            {
                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                var curEmpInfor = GetAccountByEmpNo(empNo);
                if (curEmpInfor == null)
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Tài khoản hiện tại không tồn tại"));
                }
                HIDbHelper.Instance.GetDBCnn().Execute(
                @"
                  BEGIN
                  DELETE FROM TB_ACCOUNT WHERE F_EMPNO = @F_EMPNO ;
                  DELETE FROM  R_ACCOUNT_PERMISSION WHERE  F_EMPNO = @F_EMPNO ;
                  DELETE FROM R_ACCOUNT_BU WHERE  F_EMPNO = @F_EMPNO ;

                  END;
                ", new { F_EMPNO = empNo?.Trim()?.ToUpper() });

                HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã xóa tài khoản [{empNo}] với dữ liệu cũ [{JsonConvert.SerializeObject(curEmpInfor)}]");

                return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202504051117] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message);

            }

        }

        public HIReturnMessageModel<object> ModifyAccount(ModifyAccountReq dat)
        {
            try
            {
                var acc = GetAccountFromSession();
                if (dat == null
                    || dat.empNo.isNullStr()
                       || dat.modifyType.isNullStr()
                    )
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"));
                }

                if (dat.modifyType == EnumModifyType.Delete.ToString())
                {

                    return DeleteAccount(dat.empNo);
                }
                else
                {
                    if (dat.empName.isNullStr()
                           || dat.empDept.isNullStr()
                                || dat.empMail.isNullStr())
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Vui lòng nhập vào các trường bắt buộc"));

                    var oldAcc = GetAccountByEmpNo(dat.empNo);

                    var rsPermissionLs = HIConfigBusiness.Instance.GetPermissionNameLs();
                    if (rsPermissionLs.status == HIStatusType.error.ToString())
                        return HIReturnMessage.HIReturnError<object>(rsPermissionLs.message);
                    if (dat.empPermission != null && dat.empPermission.Count > 0)
                    {
                        int checkCount = dat.empPermission.Where(r => rsPermissionLs.data.Select(t => t.F_NODE_NAME).Contains(r) == false).Count();
                        if (checkCount > 0)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Danh sách quyền bạn gửi lên không hợp lệ"));

                    }

                    //var rsUnitLs = HIConfigBusiness.Instance.GetUnitLs();
                    //if (rsUnitLs.status == HIStatusType.error.ToString())
                    //    return HIReturnMessage.HIReturnError<object>(rsUnitLs.message, null);
                    //if (dat.empBU != null && dat.empBU.Count > 0)
                    //{
                    //    int checkCount = dat.empBU.Where(r => rsUnitLs.data.Select(t => t.F_NODE_NAME).Contains(r) == false).Count();
                    //    if (checkCount > 0)
                    //        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Danh sách BU bạn gửi lên không hợp lệ"), null);

                    //}


                    if (Util.IsEmailValid(dat.empMail?.Trim()) == false)
                    {
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Email không hợp lệ"));

                    }


                    if (dat.modifyType == EnumModifyType.Add.ToString())
                    {

                        if (oldAcc != null)
                        {
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Tài khoản này đã tồn tại trên hệ thống trước đó"));
                        }

                        using (var cnn = HIDbHelper.Instance.GetDBCnn())
                        {
                            SqlTransaction trs = null;
                            try
                            {
                                cnn.Open();
                                trs = cnn.BeginTransaction();
                                var rs = AddNewAccount(new HI_TB_ACCOUNT
                                {
                                    F_EMPNO = dat.empNo?.Trim()?.ToUpper(),
                                    F_EMPNAME = dat.empName?.Trim(),
                                    F_MAIL = dat.empMail?.Trim(),
                                    F_DEPT = dat.empDept?.Trim(),
                                    F_PHONE = dat.empPhone?.Trim(),
                                    F_CREATE_EMP = acc?.F_EMPNO,

                                }, trs);
                                if (dat.empPermission != null && dat.empPermission.Count > 0)
                                {
                                    dat.empPermission.ForEach(role =>
                                    {
                                        AddNewAccountPermission(new HI_R_ACCOUNT_PERMISSION
                                        {
                                            F_EMPNO = dat.empNo?.Trim()?.ToUpper(),
                                            F_ROLE = role,
                                            F_CREATE_EMP = acc?.F_EMPNO
                                        }, trs);
                                    });
                                }


                                if (dat.empBU != null && dat.empBU.Count > 0)
                                {
                                    dat.empBU.ForEach(bu =>
                                    {
                                        AddNewRAccountBU(new HI_R_ACCOUNT_BU
                                        {
                                            F_EMPNO = dat.empNo?.Trim()?.ToUpper(),
                                            F_BU_NAME = bu?.Trim(),
                                            CREATE_EMP = acc?.F_EMPNO
                                        }, trs);
                                    });
                                }

                                trs.Commit();
                                if (rs)
                                {

                                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã thêm tài khoản [{JsonConvert.SerializeObject(dat)}]");
                                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                                }
                                else
                                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString());
                            }
                            catch (Exception ex1)
                            {
                                try
                                {
                                    if (trs != null)
                                        trs.Rollback();
                                }
                                catch
                                {
                                }
                                HILogging.Instance.SaveLog($"[ERR-202504051119] exception detail:[{ex1.Message + ex1.StackTrace}]");

                                return HIReturnMessage.HIReturnError<object>(ex1.Message);

                            }

                        }


                    }
                    if (dat.modifyType == EnumModifyType.Edit.ToString())
                    {
                        if (oldAcc == null)
                        {
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Tài khoản này không tồn tại trên hệ thống"));
                        }

                        using (var cnn = HIDbHelper.Instance.GetDBCnn())
                        {
                            SqlTransaction trs = null;
                            try
                            {
                                cnn.Open();
                                trs = cnn.BeginTransaction();
                                var rs = UpdateAccount(new HI_TB_ACCOUNT
                                {
                                    F_EMPNO = dat.empNo?.Trim()?.ToUpper(),
                                    F_EMPNAME = dat.empName?.Trim(),
                                    F_MAIL = dat.empMail?.Trim(),
                                    F_DEPT = dat.empDept?.Trim(),
                                    F_PHONE = dat.empPhone?.Trim(),
                                    F_UPDATE_EMP = acc?.F_EMPNO,
                                }, trs);

                                DeleteAccountPermission(dat.empNo, trs);
                                DeleteRAccountBU(dat.empNo, trs);

                                if (dat.empPermission != null && dat.empPermission.Count > 0)
                                {
                                    dat.empPermission.ForEach(role =>
                                    {
                                        AddNewAccountPermission(new HI_R_ACCOUNT_PERMISSION
                                        {
                                            F_EMPNO = dat.empNo?.Trim()?.ToUpper(),
                                            F_ROLE = role,
                                            F_CREATE_EMP = acc?.F_EMPNO
                                        }, trs);
                                    });
                                }

                                if (dat.empBU != null && dat.empBU.Count > 0)
                                {
                                    dat.empBU.ForEach(bu =>
                                    {
                                        AddNewRAccountBU(new HI_R_ACCOUNT_BU
                                        {
                                            F_EMPNO = dat.empNo?.Trim()?.ToUpper(),
                                            F_BU_NAME = bu?.Trim(),
                                            CREATE_EMP = acc?.F_EMPNO
                                        }, trs);
                                    });
                                }


                                trs.Commit();
                                if (rs)
                                {
                                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật tài khoản với dữ liệu mới là [{JsonConvert.SerializeObject(dat)}]");
                                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                                }
                                else
                                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString());
                            }
                            catch (Exception ex1)
                            {
                                try
                                {
                                    if (trs != null)
                                        trs.Rollback();
                                }
                                catch
                                {
                                }
                                HILogging.Instance.SaveLog($"[ERR-20250405111856] exception detail:[{ex1.Message + ex1.StackTrace}]");

                                return HIReturnMessage.HIReturnError<object>(ex1.Message);

                            }



                        }
                    }




                }
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405111831] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message);
            }
            return null;
        }



        public HIReturnMessageModel<object> UpdateYourAccount(UpdateYourAccountReq dat)
        {
            try
            {
                var acc = GetAccountFromSession();
                if (dat == null
                  || dat.empNo.isNullStr()
                   || dat.empName.isNullStr()
                    || dat.empMail.isNullStr()
                     || dat.empDept.isNullStr()

                    )
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Vui lòng nhập vào các trường bắt buộc"));
                }

                if (dat.empNo != acc?.F_EMPNO)
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Chỉ bạn mới sửa được thông tin của mình"));

                }

                var curAcc = GetAccountByEmpNo(dat?.empNo?.Trim());
                if (curAcc == null)
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Tài khoản của bạn không tồn tại"));
                }
                if (Util.IsEmailValid(dat.empMail?.Trim()) == false)
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Email không hợp lệ"));

                }

                var rs = UpdateAccount(new HI_TB_ACCOUNT
                {
                    F_EMPNO = acc?.F_EMPNO,
                    F_EMPNAME = dat.empName?.Trim(),
                    F_MAIL = dat.empMail?.Trim(),
                    F_DEPT = dat.empDept?.Trim(),
                    F_PHONE = dat.empPhone?.Trim(),
                    F_UPDATE_EMP = acc?.F_PHONE,
                });
                if (rs)
                {
                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật tài khoản với dữ liệu mới là [{JsonConvert.SerializeObject(dat)}]");

                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                }
                else
                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString());

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112014] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message);
            }
        }



        public HIReturnMessageModel<V_HI_TB_ACCOUNT> LoginSSO(string code)
        {
            try
            {

                if (code.isNullStr())
                    return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Vui lòng nhập code"));
                string empNo = LoginSSOs.Instance.LoginSSO(code?.Trim());
                if (empNo.isNullStr())
                    return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(LangHelper.Instance.Get("Không có thông tin mã thẻ"));

                var checkLoginRs = CheckLogin(new HILoginReq { UserName = empNo?.Trim()?.ToUpper(), PassWord = "ddd" });
                return checkLoginRs;

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202511131324] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnError<V_HI_TB_ACCOUNT>(ex.Message);
            }



        }
    }
}