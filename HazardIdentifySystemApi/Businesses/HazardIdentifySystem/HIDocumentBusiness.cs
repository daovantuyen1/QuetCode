using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using Newtonsoft.Json;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIDocumentBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIDocumentBusiness() { }
        private static HIDocumentBusiness _instance;
        public static HIDocumentBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIDocumentBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public HI_R_DOCUMENT_DATA GetRDocumentDataByFileId(string fileID)
        {
            return HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault<HI_R_DOCUMENT_DATA>(@"
                SELECT F_ROW_ID
                      ,F_EMPNO
                      ,F_EMPNAME
                      ,F_EMPDEPT
                      ,F_TYPE
                      ,F_BU
                      ,F_REMARK
                      ,F_FILE_ID
                      ,CREATE_EMP
                      , FORMAT( CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS CREATE_TIME  
                  FROM R_DOCUMENT_DATA WHERE F_FILE_ID = @F_FILE_ID
                ", new
            {
                F_FILE_ID = fileID?.Trim()
            });
        }


        public bool DeleteRDocumentDataByFileId(string fileID)
        {
            return  HIDbHelper.Instance.GetDBCnn().Execute(@"
                DELETE FROM R_DOCUMENT_DATA
                      WHERE F_FILE_ID =  @F_FILE_ID
                ", new
            {
                F_FILE_ID = fileID?.Trim()
            }) > 0 ? true : false;
           
        }


        public bool AddNewRDocumentData(HI_R_DOCUMENT_DATA dat)
        {
            return HIDbHelper.Instance.GetDBCnn().Execute(@"
                        INSERT INTO R_DOCUMENT_DATA
                                   (
		                            F_ROW_ID
                                   ,F_EMPNO
                                   ,F_EMPNAME
                                   ,F_EMPDEPT
                                   ,F_TYPE
                                   ,F_BU
                                   ,F_REMARK
                                   ,F_FILE_ID
                                   ,CREATE_EMP
                                   ,CREATE_TIME
		                           )
                             VALUES
	                            (
		                            dbo.GET_NEW_ROWID(NEWID())
                                   ,@F_EMPNO
                                   ,@F_EMPNAME
                                   ,@F_EMPDEPT
                                   ,@F_TYPE
                                   ,@F_BU
                                   ,@F_REMARK
                                   ,@F_FILE_ID
                                   ,@CREATE_EMP
                                   ,GETDATE()
		                           )
         
                ", dat) > 0 ? true : false;
        }



        /// <summary>
        /// Them/xoa file tai lieu
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> ModifyDocument(ModifyDocumentReq dat)
        {
            try
            {
                
                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                if (dat == null
                    || dat.modifyType.isNullStr()
                    || dat.fileID.isNullStr()
                    )
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"), null);
                }
                if (dat.modifyType == EnumModifyType.Add.ToString())
                {
                    if (dat.type == "BU")
                    {
                        if (dat.BU.isNullStr())
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("BU trống"), null);
                    }
                    if (dat.type == "REFERENCES")
                    {
                        dat.BU = "";

                    }

                    if (dat.fileRemark.isNullStr())
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Ghi chú trống"), null);
                    var fileDat = HIFileBusiness.Instance.GetFileData(dat.fileID);
                    if (fileDat == null)
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có dữ liệu trên hệ thống của file tải lên"), null);

                    var rs = AddNewRDocumentData(new HI_R_DOCUMENT_DATA
                    {
                        F_EMPNO = acc?.F_EMPNO?.Trim()?.ToUpper(),
                        F_EMPNAME = acc?.F_EMPNAME,
                        F_EMPDEPT = acc?.F_DEPT,
                        F_TYPE = dat.type?.Trim(),
                        F_BU = dat.BU?.Trim(),
                        F_REMARK = dat.fileRemark?.Trim(),
                        F_FILE_ID = dat.fileID?.Trim(),
                        CREATE_EMP = acc?.F_EMPNO
                    });
                    if (rs)
                    {
                        HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã thêm tài liệu với dữ liệu [{JsonConvert.SerializeObject(dat)}]");

                        return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                    }
                  
                    else
                        return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);

                }
                if (dat.modifyType == EnumModifyType.Delete.ToString())
                {
                    var oldDat = GetRDocumentDataByFileId(dat.fileID);
                    if (oldDat == null)
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu không tồn tại trên hệ thống"), null);

                    var rs = DeleteRDocumentDataByFileId(dat.fileID);
                    HIFileBusiness.Instance.DeleteFile(dat.fileID);

                    if (rs)
                    {
                        HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã xóa tài liệu với dữ liệu cũ [{JsonConvert.SerializeObject(oldDat)}]");
                        return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                    }
                      
                    else
                        return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);


                }

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112504] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);
            }
            return null;
        }

      
        public HIReturnMessageModel<ElTableRes> GetDocumentLs(
                              DocumentLsReq dat
          )
        {
            
            try
            {
                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                string columns = @"
                               AA.F_ROW_ID
                              ,AA.F_EMPNO
                              ,AA.F_EMPNAME
                              ,AA.F_EMPDEPT
                              ,AA.F_TYPE
                              ,AA.F_BU
                              ,AA.F_REMARK
                              ,AA.F_FILE_ID
                              ,AA.CREATE_EMP
	                          , FORMAT( AA.CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as CREATE_TIME  
                               , BB.F_FILE_NAME	
                              ,  BB.F_FILE_EXT
                               , BB.F_FILE_SIZE

                       ";


                string sql = @"
                        SELECT 
                        {0}
                        FROM 
                        R_DOCUMENT_DATA AA ,  R_FILE_DATA BB
                        WHERE AA.F_FILE_ID = BB.F_FILE_ID
                     " +
                     $@" 
                       {(dat.empNo.isNullStr() ? "" : $" AND AA.F_EMPNO LIKE N'%{dat.empNo.Trim()}%' ")}
                       {(dat.empName.isNullStr() ? "" : $" AND AA.F_EMPNAME LIKE N'%{dat.empName.Trim()}%' ")}
                       {(dat.fileName.isNullStr() ? "" : $" AND BB.F_FILE_NAME LIKE N'%{dat.fileName.Trim()}%' ")}
                      
                    
                       "
                      ;


                if (dat.searchType.isNullStr())
                {

                }
                else if (dat.searchType == "BU")
                {
                    sql += $@"
                                AND   AA.F_TYPE='BU'  {  (dat.BU.isNullStr() ? "" : $" AND AA.F_BU LIKE N'%{dat.BU.Trim()}%' ")}
                            ";
                }
                else if (dat.searchType == "REFERENCES")
                {
                    sql += $@"
                                AND   AA.F_TYPE='REFERENCES'  
                            ";

                }

                dat.page = dat.page <= 0 ? 1 : dat.page;
                dat.pageSize = dat.pageSize <= 0 ? 5 : dat.pageSize;
                int star_rownum = (dat.page * dat.pageSize) - dat.pageSize + 1;
                int end_rownum = dat.page * dat.pageSize;

                string sql1 = $@"
                   SELECT 
                              F_ROW_ID
                              ,F_EMPNO
                              ,F_EMPNAME
                              ,F_EMPDEPT
                              ,F_TYPE
                              ,F_BU
                              ,F_REMARK
                              ,F_FILE_ID
                              ,CREATE_EMP
	                          ,  CREATE_TIME  
                               , F_FILE_NAME	
                              ,  F_FILE_EXT
                               , F_FILE_SIZE

                    FROM (SELECT ROW_NUMBER() OVER (ORDER BY CREATE_TIME DESC ) AS ROW_NUMBER, K.*
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

                var items = HIDbHelper.Instance.GetDBCnn().Query<V_HI_R_DOCUMENT_DATA>(
                  sql1
                 ).ToList();


                return HIReturnMessage.HIReturnSuccess<ElTableRes>(HIStatusType.success.ToString(), new ElTableRes()
                {
                    items = items,
                    totalCount = totalCount
                });
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202504051125] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<ElTableRes>(ex.Message, null);
            }


        }


    }
}