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
using System.Web;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIPicBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIPicBusiness() { }
        private static HIPicBusiness _instance;
        public static HIPicBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIPicBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion


        public HIReturnMessageModel<ElTableRes> GetPicLs(string unit,string factoryBuilding, string floor,string factory , string search, int page, int pageSize)
        {
            try
            {


                search = search.isNullStr() ? "" : search.Trim().ToLower();
                string columns = @"
                              F_ROW_ID
                              ,F_UNIT
                              ,F_FACTORY_BUILDING
                              ,F_FLOOR
                              ,F_PIC_EMPNO
                              ,F_PIC_EMPNAME
                              ,F_PIC_EMPMAIL
                              ,F_PIC_PHONE
                              ,F_MAIL_CC
                              ,F_CREATE_EMP
                              ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS F_CREATE_TIME
                              ,F_UPDATE_EMP
                              ,FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS F_UPDATE_TIME
                              ,F_FACTORY
                              ,F_PIC_BOSS_EMPNO
                              ,F_PIC_BOSS_NAME
                              ,F_PIC_BOSS_MAIL
                              ,F_BIG_BOSS_MAIL
                       ";


                string sql = @"
                        SELECT 
                        {0}
                        FROM 
                        C_PIC_ZONE
                        WHERE   " +
                        $@"   
                            (     
                             {(unit.isNullStr()?" 1=1 ": $" F_UNIT LIKE N'%{unit?.Trim()}%'  ")} AND 
                             {(factoryBuilding.isNullStr() ? " 1=1 " : $" F_FACTORY_BUILDING LIKE N'%{factoryBuilding?.Trim()}%'  ")} AND 
                             {(floor.isNullStr() ? " 1=1 " : $" F_FLOOR LIKE N'%{floor?.Trim()}%'  ")} AND 
                             {(factory.isNullStr() ? " 1=1 " : $" F_FACTORY LIKE N'%{factory?.Trim()}%'  ")}  
                            ) 
                           AND
                            ( 
                                F_PIC_EMPNO LIKE N'%{search?.Trim()}%' OR
                                    F_PIC_EMPNAME LIKE N'%{search?.Trim()}%' OR
                                  F_PIC_EMPMAIL LIKE N'%{search?.Trim()}%' OR
                                 F_PIC_PHONE LIKE N'%{search?.Trim()}%' OR
                                F_MAIL_CC LIKE N'%{search?.Trim()}%' OR
                                F_PIC_BOSS_EMPNO LIKE N'%{search?.Trim()}%' OR
                                F_PIC_BOSS_NAME LIKE N'%{search?.Trim()}%' OR
                                F_PIC_BOSS_MAIL LIKE N'%{search?.Trim()}%' OR
                                F_BIG_BOSS_MAIL LIKE N'%{search?.Trim()}%' 
                            )
                        "
                      ;


                page = page <= 0 ? 1 : page;
                pageSize = pageSize <= 0 ? 5 : pageSize;
                int star_rownum = (page * pageSize) - pageSize + 1;
                int end_rownum = page * pageSize;

                string sql1 = $@"
                   SELECT 
                              F_ROW_ID
                              ,F_UNIT
                              ,F_FACTORY_BUILDING
                              ,F_FLOOR
                              ,F_PIC_EMPNO
                              ,F_PIC_EMPNAME
                              ,F_PIC_EMPMAIL
                              ,F_PIC_PHONE
                              ,F_MAIL_CC
                              ,F_CREATE_EMP
                              , F_CREATE_TIME
                              ,F_UPDATE_EMP
                              ,  F_UPDATE_TIME
                              ,F_FACTORY
                              ,F_PIC_BOSS_EMPNO
                              ,F_PIC_BOSS_NAME
                              ,F_PIC_BOSS_MAIL
                              , F_BIG_BOSS_MAIL

                    FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL) ) AS ROW_NUMBER, K.*
                          FROM (
						      {string.Format(sql, columns)}
						  )  K ) E
                    WHERE E.ROW_NUMBER BETWEEN {star_rownum} AND {end_rownum}  ORDER BY F_FACTORY , F_UNIT ,  F_FACTORY_BUILDING , F_FLOOR ,F_ROW_ID
              ";
                int totalCount = 0;
                var tempCount = HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault(string.Format(sql, " COUNT(1) AS CC"));
                if (tempCount != null)
                {

                    totalCount = (int)tempCount.CC;
                }

                var items = HIDbHelper.Instance.GetDBCnn().Query<HI_C_PIC_ZONE>(
                  sql1
                 , new
                 {
                     SEARCH = search
                 }).ToList();

                return HIReturnMessage.HIReturnSuccess<ElTableRes>(HIStatusType.success.ToString(), new ElTableRes()
                {
                    items = items,
                    totalCount = totalCount
                });
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405131801] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<ElTableRes>(ex.Message, null);
            }


        }


        public List<HI_C_PIC_ZONE> GetPic(object paramss, string where)
        {
            return HIDbHelper.Instance.GetDBCnn()
                  .Query<HI_C_PIC_ZONE>($@"
                SELECT 
                              F_ROW_ID
                              ,F_UNIT
                              ,F_FACTORY_BUILDING
                              ,F_FLOOR
                              ,F_PIC_EMPNO
                              ,F_PIC_EMPNAME
                              ,F_PIC_EMPMAIL
                              ,F_PIC_PHONE
                              ,F_MAIL_CC
                              ,F_CREATE_EMP
                              ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS F_CREATE_TIME
                              ,F_UPDATE_EMP
                              ,FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS F_UPDATE_TIME
                              ,F_FACTORY
                              ,F_PIC_BOSS_EMPNO
                              ,F_PIC_BOSS_NAME
                              ,F_PIC_BOSS_MAIL 
                              ,F_BIG_BOSS_MAIL
                  FROM C_PIC_ZONE
                 WHERE 1=1   { (where.isNullStr() ? "" : $"AND {where}  ")  }
                ", paramss).ToList();

        }

        public bool AddNewPic(HI_C_PIC_ZONE dat)
        {
            return HIDbHelper.Instance.GetDBCnn()
                  .Execute(@"
                    INSERT INTO C_PIC_ZONE
                               (F_ROW_ID
                               ,F_UNIT
                               ,F_FACTORY_BUILDING
                               ,F_FLOOR
                               ,F_PIC_EMPNO
                               ,F_PIC_EMPNAME
                               ,F_PIC_EMPMAIL
                               ,F_PIC_PHONE
                               ,F_MAIL_CC
                               ,F_CREATE_EMP
                               ,F_CREATE_TIME
                               ,F_UPDATE_EMP
                               ,F_UPDATE_TIME
                                  ,F_FACTORY
                                  ,F_PIC_BOSS_EMPNO
                                  ,F_PIC_BOSS_NAME
                                  ,F_PIC_BOSS_MAIL
                                  ,F_BIG_BOSS_MAIL
                            )
                         VALUES
                               ( dbo.GET_NEW_ROWID(NEWID())
                               , @F_UNIT
                               , @F_FACTORY_BUILDING
                               , @F_FLOOR
                               , @F_PIC_EMPNO
                               , @F_PIC_EMPNAME
                               , @F_PIC_EMPMAIL
                               , @F_PIC_PHONE
                               , @F_MAIL_CC
                               , @F_CREATE_EMP
                               , getdate()
                               , @F_CREATE_EMP
                               , getdate() 
                                  ,@F_FACTORY
                                  ,@F_PIC_BOSS_EMPNO
                                  ,@F_PIC_BOSS_NAME
                                  ,@F_PIC_BOSS_MAIL
                                  ,@F_BIG_BOSS_MAIL
		                       )
                    ", dat) > 0 ? true : false;
        }

        public bool UpdatePic(HI_C_PIC_ZONE dat)
        {
            return HIDbHelper.Instance.GetDBCnn().Execute(
             @"
            UPDATE C_PIC_ZONE
               SET
                  F_UNIT = @F_UNIT
                  ,F_FACTORY_BUILDING = @F_FACTORY_BUILDING
                  ,F_FLOOR = @F_FLOOR
                  ,F_PIC_EMPNO = @F_PIC_EMPNO
                  ,F_PIC_EMPNAME = @F_PIC_EMPNAME
                  ,F_PIC_EMPMAIL = @F_PIC_EMPMAIL
                  ,F_PIC_PHONE =  @F_PIC_PHONE
                  ,F_MAIL_CC = @F_MAIL_CC
                  ,F_UPDATE_EMP =  @F_UPDATE_EMP
                  ,F_UPDATE_TIME = getdate() 
                  ,F_FACTORY  = @F_FACTORY
                      ,F_PIC_BOSS_EMPNO  = @F_PIC_BOSS_EMPNO
                      ,F_PIC_BOSS_NAME  = @F_PIC_BOSS_NAME
                      ,F_PIC_BOSS_MAIL  = @F_PIC_BOSS_MAIL
                      ,F_BIG_BOSS_MAIL = @F_BIG_BOSS_MAIL
             WHERE 
             F_ROW_ID  =@F_ROW_ID
            ", dat) > 0 ? true : false;
        }

        public bool DeletePic(string rowId)
        {
            return HIDbHelper.Instance.GetDBCnn().Execute(@" 	DELETE FROM C_PIC_ZONE  WHERE  F_ROW_ID = @F_ROW_ID ", new { F_ROW_ID = rowId })
                 > 0 ? true : false;
        }

        /// <summary>
        /// Modify Pic 
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> ModifyPic(ModifyPicReq dat)
        {
            try
            {

                if (dat == null || dat.modifyType.isNullStr())
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ, vui lòng điền vào các trường bắt buộc"), null);
                }

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                if (dat.modifyType == EnumModifyType.Add.ToString() || dat.modifyType == EnumModifyType.Edit.ToString())
                {
                    if (
                        dat.factory.isNullStr()
                         || dat.unit.isNullStr()
                               || dat.floor.isNullStr()
                                 || dat.factoryBuilding.isNullStr()
                                   || dat.picEmpNo.isNullStr()
                                     || dat.picEmpName.isNullStr()
                                          || dat.picEmpMail.isNullStr()
                              )
                    {
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ, vui lòng điền vào các trường bắt buộc"), null);
                    }


                    var lsEmailCC = new List<string>();
                    if (!dat.mailCC.isNullStr())
                    {
                        lsEmailCC = dat.mailCC.Split(';', ',').Where(r => (!string.IsNullOrWhiteSpace(r)) && new[] { ";", "," }.Contains(r?.Trim()) == false)
                            .Select(r => r?.Trim()).ToList();
                        foreach (var mail in lsEmailCC)
                        {
                            if (Util.IsEmailValid(mail) == false)
                                return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Giá trị mail CC không hợp lệ, vui lòng kiểm tra lại"), null);
                        }

                    }
                    lsEmailCC = lsEmailCC?.Distinct()?.ToList();


                    var lsEmailBigBoss = new List<string>();
                    if (!dat.bigBossMails.isNullStr())
                    {
                        lsEmailBigBoss = dat.bigBossMails.Split(';', ',').Where(r => (!string.IsNullOrWhiteSpace(r)) && new[] { ";", "," }.Contains(r?.Trim()) == false)
                            .Select(r => r?.Trim()).ToList();
                        foreach (var mail in lsEmailBigBoss)
                        {
                            if (Util.IsEmailValid(mail) == false)
                                return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Giá trị mail của chủ quản cấp cao không hợp lệ"), null);
                        }

                    }
                    lsEmailBigBoss = lsEmailBigBoss?.Distinct()?.ToList();


                    //var empSv = HIElistQueryBusiness.Instance.GetEmpInforFromSV(dat.picEmpNo);
                    //if (empSv?.LEAVEDAY?.StartsWith("9999") == false)
                    //    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Mã thẻ hiện tại đã nghỉ việc hoặc không có thông tin trên hệ thống Elist"), null);


                    // kiem tra picMail 
                    if (Util.IsEmailValid(dat.picEmpMail) == false)
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Giá trị mail của người phụ trách không hợp lệ"), null);

                    //


                    if (!dat.picBossEmpNo.isNullStr())
                    {
                        if (dat.picBossEmpMail.isNullStr() || Util.IsEmailValid(dat.picBossEmpMail?.Trim()) == false)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Mail của chủ quản không được bỏ trống"), null);

                    }


                    if (dat.modifyType == EnumModifyType.Add.ToString())
                    {
                        var oldDatLS = GetPic(new { F_FACTORY = dat?.factory?.Trim(), F_UNIT = dat.unit?.Trim(), F_FACTORY_BUILDING = dat.factoryBuilding?.Trim(), F_FLOOR = dat.floor?.Trim(), F_PIC_EMPNO = dat.picEmpNo?.Trim()?.ToUpper(), F_PIC_BOSS_EMPNO = dat.picBossEmpNo?.Trim()?.ToUpper() },
                         "     F_FACTORY= @F_FACTORY AND   F_UNIT= @F_UNIT AND F_FACTORY_BUILDING  = @F_FACTORY_BUILDING AND F_FLOOR = @F_FLOOR AND F_PIC_EMPNO= @F_PIC_EMPNO  AND  ( @F_PIC_BOSS_EMPNO IS NULL OR  @F_PIC_BOSS_EMPNO =''  OR F_PIC_BOSS_EMPNO = @F_PIC_BOSS_EMPNO    )   ");

                        if (oldDatLS != null && oldDatLS.Count > 0)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu tương tự đã tồn tại, không được thêm mới"), null);
                        var rs = AddNewPic(new HI_C_PIC_ZONE
                        {
                            F_UNIT = dat.unit?.Trim(),
                            F_FACTORY_BUILDING = dat.factoryBuilding?.Trim(),
                            F_FLOOR = dat.floor?.Trim(),
                            F_PIC_EMPNO = dat.picEmpNo?.Trim()?.ToUpper(),
                            F_PIC_EMPNAME = dat.picEmpName?.Trim(),
                            F_PIC_EMPMAIL = dat.picEmpMail?.Trim(),
                            F_PIC_PHONE = dat.picEmpPhone?.Trim(),
                            F_MAIL_CC = lsEmailCC?.Count <= 0 ? "" : string.Join(";", lsEmailCC),
                            F_CREATE_EMP = acc?.F_EMPNO,
                            F_FACTORY = dat.factory?.Trim(),
                            F_PIC_BOSS_EMPNO = dat?.picBossEmpNo?.Trim()?.ToUpper(),
                            F_PIC_BOSS_NAME = dat?.picBossEmpName?.Trim(),
                            F_PIC_BOSS_MAIL = dat?.picBossEmpMail?.Trim(),
                            F_BIG_BOSS_MAIL = lsEmailBigBoss?.Count <= 0 ? "" : string.Join(";", lsEmailBigBoss),
                            
                        });

                        if (rs)
                        {
                            HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã thêm người phụ trách xử lý vấn đề, dữ liệu [{JsonConvert.SerializeObject(dat)}]");

                            return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                        }
                           
                        else
                            return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);
                    }
                    else if (dat.modifyType == EnumModifyType.Edit.ToString())
                    {
                        var oldDatLs = GetPic(new { F_ROW_ID = dat.id }, "   F_ROW_ID = @F_ROW_ID  ");
                        if (oldDatLs == null || oldDatLs.Count <= 0)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu không tồn tại trên hệ thống"), null);
                        var oldOtherDatLs = GetPic(new
                        {
                            F_FACTORY = dat?.factory?.Trim(),
                            F_UNIT = dat.unit?.Trim(),
                            F_FACTORY_BUILDING = dat.factoryBuilding?.Trim(),
                            F_FLOOR = dat.floor?.Trim(),
                            F_PIC_EMPNO = dat.picEmpNo?.Trim()?.ToUpper(),
                            F_ROW_ID = dat.id ,
                            F_PIC_BOSS_EMPNO= dat?.picBossEmpNo?.Trim()?.ToUpper()
                        }
                         , " F_FACTORY = @F_FACTORY AND    F_UNIT =@F_UNIT AND F_FACTORY_BUILDING =@F_FACTORY_BUILDING AND F_FLOOR= @F_FLOOR AND  F_PIC_EMPNO= @F_PIC_EMPNO  AND F_ROW_ID !=@F_ROW_ID  AND  ( @F_PIC_BOSS_EMPNO IS NULL OR  @F_PIC_BOSS_EMPNO =''  OR F_PIC_BOSS_EMPNO = @F_PIC_BOSS_EMPNO    )     ");
                        if (oldOtherDatLs != null && oldOtherDatLs.Count > 0)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu tương tự đã tồn tại"), null);
                        var rs = UpdatePic(new HI_C_PIC_ZONE
                        {
                            F_UNIT = dat.unit?.Trim(),
                            F_FACTORY_BUILDING = dat.factoryBuilding?.Trim(),
                            F_FLOOR = dat.floor?.Trim(),
                            F_PIC_EMPNO = dat.picEmpNo?.Trim(),
                            F_PIC_EMPNAME = dat.picEmpName?.Trim(),
                            F_PIC_EMPMAIL = dat.picEmpMail ,
                            F_PIC_PHONE = dat.picEmpPhone?.Trim(),
                            F_MAIL_CC = lsEmailCC?.Count <= 0 ? "" : string.Join(";", lsEmailCC),
                            F_UPDATE_EMP = acc?.F_EMPNO,
                            F_ROW_ID = dat?.id,
                            F_FACTORY = dat.factory?.Trim(),
                            F_PIC_BOSS_EMPNO = dat?.picBossEmpNo?.Trim()?.ToUpper(),
                            F_PIC_BOSS_NAME = dat?.picBossEmpName?.Trim(),
                            F_PIC_BOSS_MAIL = dat?.picBossEmpMail?.Trim(),
                            F_BIG_BOSS_MAIL= lsEmailBigBoss?.Count <= 0 ? "" : string.Join(";", lsEmailBigBoss),
                        });

                        if (rs)
                        {
                            HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật  người phụ trách xử lý vấn đề, dữ liệu mới [{JsonConvert.SerializeObject(dat)}]");

                            return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                        }
                          
                        else
                            return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);
                    }

                }

                if (dat.modifyType == EnumModifyType.Delete.ToString())
                {
                    var oldDatLs = GetPic(new { F_ROW_ID = dat.id }, "   F_ROW_ID = @F_ROW_ID  ");
                    if (oldDatLs == null || oldDatLs.Count <= 0)
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu không tồn tại trên hệ thống"), null);
                    var rs = DeletePic(dat.id);
                    if (rs)
                    {
                        HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã xóa người phụ trách xử lý vấn đề, dữ liệu cũ [{JsonConvert.SerializeObject(oldDatLs)}]");

                        return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                    }
                      
                    else
                        return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);
                }
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250105132201] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
            return null;
        }


        /// <summary>
        /// Lay ra danh sach Pic emp cua nha xuong hien tai
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="unit"></param>
        /// <param name="factoryBuilding"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_PIC_ZONE>> GetPicEmpLsByWhere(string factory, string unit, string factoryBuilding, string floor)
        {
            try
            {
                var picEmpLs = GetPic(new
                {
                    F_FACTORY = factory?.Trim(),
                    F_UNIT = unit?.Trim(),
                    F_FACTORY_BUILDING = factoryBuilding?.Trim(),
                    F_FLOOR = floor?.Trim()
                },
              " F_FACTORY  = @F_FACTORY AND   F_UNIT = @F_UNIT AND  F_FACTORY_BUILDING = @F_FACTORY_BUILDING AND F_FLOOR = @F_FLOOR ")
             ;

                var finalPicEmpLs = picEmpLs.Select(r => {
                    var empSv = HIElistQueryBusiness.Instance.GetEmpInfor(r.F_PIC_EMPNO?.Trim()?.ToUpper());
                    return new V_HI_C_PIC_ZONE
                    {
                        F_PIC_EMPNO = r.F_PIC_EMPNO,
                        F_PIC_EMPNAME = r.F_PIC_EMPNAME,
                        F_PIC_EMPMAIL = r.F_PIC_EMPMAIL,
                        F_INCHARGE_EMPDEPT = empSv?.data?.CURRENT_OU_NAME,
                    };
                }).ToList();

                return HIReturnMessage.HIReturnSuccess(HIStatusType.success.ToString(), finalPicEmpLs);
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405132202] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnSuccess<List<V_HI_C_PIC_ZONE>>(ex.Message, null);
            }
         

        }



        /// <summary>
        /// Lay ra danh sach Pic boss emp cua nha xuong hien tai
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="unit"></param>
        /// <param name="factoryBuilding"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_PIC_ZONE>> GetPicBossEmpLsByWhere(string factory, string unit, string factoryBuilding, string floor)
        {
            try
            {
                var picEmpLs = GetPic(new
                {
                    F_FACTORY = factory?.Trim(),
                    F_UNIT = unit?.Trim(),
                    F_FACTORY_BUILDING = factoryBuilding?.Trim(),
                    F_FLOOR = floor?.Trim()
                },
              " F_FACTORY  = @F_FACTORY AND   F_UNIT = @F_UNIT AND  F_FACTORY_BUILDING = @F_FACTORY_BUILDING AND F_FLOOR = @F_FLOOR ")
             ;

                var finalPicBossEmpLs = picEmpLs.Select(r => {
                    return new V_HI_C_PIC_ZONE
                    {
                        F_PIC_BOSS_EMPNO = r.F_PIC_BOSS_EMPNO,
                        F_PIC_BOSS_NAME = r.F_PIC_BOSS_NAME,
                        F_PIC_BOSS_MAIL = r.F_PIC_BOSS_MAIL,
                    };
                }).ToList();

                return HIReturnMessage.HIReturnSuccess(HIStatusType.success.ToString(), finalPicBossEmpLs);
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405132204] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnError<List<V_HI_C_PIC_ZONE>>(ex.Message, null);
            }


        }

    }
}