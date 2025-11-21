using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Dapper;
using Newtonsoft.Json;



namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIFileBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIFileBusiness() { }
        private static HIFileBusiness _instance;
        public static HIFileBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIFileBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion


        public HI_R_FILE_DATA GetFileData(string fileId)
        {
            return HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault<HI_R_FILE_DATA>(
                  @"
                    SELECT F_FILE_ID
                          ,F_FILE_NAME
                          ,F_FILE_EXT
                          ,F_FILE_SIZE
                          ,F_FILE_PATH
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_CREATE_TIME
                      FROM R_FILE_DATA WHERE F_FILE_ID = @F_FILE_ID
                    ", new { F_FILE_ID = fileId?.Trim() });
        }

        public bool DeleteFileData(string fileId)
        {
            return HIDbHelper.Instance.GetDBCnn().Execute(@" DELETE FROM R_FILE_DATA WHERE F_FILE_ID = @F_FILE_ID  "
                , new { F_FILE_ID = fileId?.Trim() }) > 0 ? true : false;

        }



        public bool AddNewFileData(HI_R_FILE_DATA dat)
        {
            return HIDbHelper.Instance.GetDBCnn()
                 .Execute(@"
                        INSERT INTO R_FILE_DATA
                                   (F_FILE_ID
                                   ,F_FILE_NAME
                                   ,F_FILE_EXT
                                   ,F_FILE_SIZE
                                   ,F_FILE_PATH
                                   ,F_CREATE_EMP
                                   ,F_CREATE_TIME)
                             VALUES
                                   ( @F_FILE_ID
                                   , @F_FILE_NAME
                                   , @F_FILE_EXT
                                   , @F_FILE_SIZE
                                   , @F_FILE_PATH
                                   , @F_CREATE_EMP
                                   , GETDATE()
		                           )
                        ", dat) > 0 ? true : false;

        }

        public HIReturnMessageModel<string> UploadFile()
        {
            try
            {

                string errMess = "";
                string filePath = "";
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Form != null)
                {

                    var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                    if ((httpRequest.Files != null && httpRequest.Files.Count > 0) == false)
                        return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Không có file gửi lên"), null);

                    try
                    {
                        string fileId = HIConfigBusiness.Instance.GetNewId();  // Commons.Util.GetRandomString();
                        var curFile = httpRequest.Files[0];
                        if (curFile.ContentLength > 209715200)  //200mb
                        {
                            return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Kích thước file tối đa tải lên là 200mb"), null);

                        }
                        if (new[]
                            {   
                            "image/jpeg",
                            "image/png" ,
                            "image/gif",
                            "application/pdf",
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ,
                            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                            "application/vnd.openxmlformats-officedocument.presentationml.presentation" ,
                        }.Contains(curFile.ContentType?.ToLower()) == false)
                        {
                         
                            return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Định dạng file không hợp lệ"), null);

                        }
                      


                        string extension = curFile.FileName.Substring(curFile.FileName.LastIndexOf(".")).ToLower();
                        string virtualPath = "~/UploadFilez/" + fileId + extension;
                        string imageSize = Util.ConvertBytesToMB(curFile.ContentLength) + " MB";
                        filePath = HttpContext.Current.Server.MapPath(virtualPath);

                        curFile.SaveAs(filePath);

                        // check actually is valid file ?
                        //if (
                        //    (curFile.ContentType == "image/jpeg" && CheckFileHelper.IsValidJpegFile(filePath) == false)
                        //    || (curFile.ContentType == "image/png" && CheckFileHelper.IsValidPngFile(filePath) == false)
                        //    || (curFile.ContentType == "image/gif" && CheckFileHelper.IsValidGifFile(filePath) == false)
                        //    || (curFile.ContentType == "application/pdf" && CheckFileHelper.IsValidPdfFile(filePath) == false)
                        //    || (curFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && CheckFileHelper.IsValidXlsxFile(filePath) == false)
                        //    || (curFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" && CheckFileHelper.IsValidDocXFile(filePath) == false)
                        //    || (curFile.ContentType == "application/vnd.openxmlformats-officedocument.presentationml.presentation" && CheckFileHelper.IsValidPptXFile(filePath) == false)

                        //    )
                        //{
                        //    if (File.Exists(filePath))
                        //        File.Delete(filePath);
                        //    return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Định dạng file không hợp lệ"), null);
                        //}


                        // if file is image:
                        if (new [] {
                            "image/jpeg",
                            "image/png" ,
                            "image/gif", }.Contains(curFile.ContentType)
                            )
                        {
                            if(curFile.ContentLength > 5 * 1024 * 1024) // image's bigger 5mb-> reduce size to 5mb
                            {
                                byte[] OutImageByteArr = null;
                                var rsResize = ResizeImageHelper.Instance.ReduceImageSize(filePath, out imageSize, out OutImageByteArr);
                                if(rsResize && OutImageByteArr!=null)
                                {
                                    if (File.Exists(filePath))
                                        File.Delete(filePath);
                                    File.WriteAllBytes(filePath, OutImageByteArr);
                                   
                                }
                                else
                                {     // can not reduce size image 
                                    goto aPoint;
                                }
                            }
                        }

                        aPoint:

                        var fileDat = new HI_R_FILE_DATA
                        {
                            F_FILE_ID = fileId,
                            F_FILE_NAME = curFile.FileName,
                            F_FILE_EXT = extension,
                            F_FILE_SIZE = imageSize,
                            F_FILE_PATH = "/UploadFilez/" + fileId + extension,
                            F_CREATE_EMP = acc?.F_EMPNO,
                        };

                        var rs = AddNewFileData(fileDat);
                        if (rs)
                        {
                            HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã thêm file với dữ liệu sau [{JsonConvert.SerializeObject(fileDat)}]");
                            return HIReturnMessage.HIReturnSuccess<string>(HIStatusType.success.ToString(), fileId);
                        }

                        else
                        {
                            errMess = HIStatusType.error.ToString();
                            goto deleteFilePoint;

                        }

                    }
                    catch (Exception ex)
                    {
                        HILogging.Instance.SaveLog($"[ERR-2025040513301] exception detail:[{ex.Message + ex.StackTrace}]");
                        errMess = ex.Message;
                        goto deleteFilePoint;
                    }


                }
                else
                    return HIReturnMessage.HIReturnError<string>(LangHelper.Instance.Get("Không có file được tải lên"), null);


                deleteFilePoint:
                try
                {

                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                }
                catch
                {

                }

                return HIReturnMessage.HIReturnError<string>(errMess, null);
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405112815] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<string>(ex.Message, null);

            }

        }

        public HIReturnMessageModel<object> DeleteFile(string fileId)
        {
            try
            {
                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                var fileInfor = GetFileData(fileId?.Trim());
                if (fileInfor == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có thông tin file này trên hệ thống"), null);

                var fullPath = HttpContext.Current.Server.MapPath("~" + fileInfor.F_FILE_PATH);
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
                DeleteFileData(fileInfor.F_FILE_ID);
                HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã xóa file với dữ liệu cũ [{JsonConvert.SerializeObject(fileInfor)}]");

                return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405112830] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
           

        }


        public HIReturnMessageModel<HI_R_FILE_DATA> GetFile(string fileId)
        {
            try
            {
                var fileInfor = GetFileData(fileId?.Trim());
                if (fileInfor == null)
                    return HIReturnMessage.HIReturnError<HI_R_FILE_DATA>(LangHelper.Instance.Get("Không có thông tin file này trên hệ thống"), null);
                fileInfor.F_FILE_PATH = Constant.FILE_URL + fileInfor.F_FILE_PATH;
                return HIReturnMessage.HIReturnSuccess(HIStatusType.success.ToString(), fileInfor);
            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405112911] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<HI_R_FILE_DATA>(ex.Message, null);
            }
        

        }
    }
}