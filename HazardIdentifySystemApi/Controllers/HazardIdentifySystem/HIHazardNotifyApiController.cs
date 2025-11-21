using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HazardIdentifySystemApi.Controllers.HazardIdentifySystem
{

    public class HIHazardNotifyApiController : HIBaseApiController
    {
        /// <summary>
        ///  ng lam don gui thong bao
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<object> DoNotifyHazard(DoNotifyHazardReq dat)
        {

            return HIHazardNotifyBusiness.Instance.DoNotifyHazard(dat);

        }
        /// <summary>
        /// Update fix image id  vao bang R_HAZARD_NOTIFY ( nguoi xu ly  da upload anh khac phuc-> cap nhat fix image id vao bang R_HAZARD_NOTIFY)
        /// </summary>
        /// <param name="imgId"></param>
        /// <param name="docNo"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<object> UpdateFixImageId(string imgId, string docNo)
        {
            return HIHazardNotifyBusiness.Instance.UpdateFixImageId(imgId, docNo);
        }


        /// <summary>
        /// ky don thong bao
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<object> DoSignHazardNoityApp(SignHazardNoityAppReq dat)
        {
            return HIHazardNotifyBusiness.Instance.DoSignHazardNoityApp(dat);
        }


        /// <summary>
        ///  Nhac nho mail ky don thong bao
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<object> DoRemindToSignNotifyApp(string docNo)
        {
            return HIHazardNotifyBusiness.Instance.DoRemindToSignNotifyApp(docNo);
        }
        /// <summary>
        /// Lay ra tat ca du lieu cua don thong bao.
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<NotifyHazardTotalData> GetAllDataOfHazardNotifyByDocNo(string docNo)
        {
            return HIHazardNotifyBusiness.Instance.GetAllDataOfHazardNotifyByDocNo(docNo?.Trim());

        }


        /// <summary>
        ///  Lay thong tin trang thai hien tai cua don
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<HI_R_APPLY_ORDER> GetRapplyOrderByDocNo(string docNo, string docType)
        {
            return HIHazardNotifyBusiness.Instance.GetRapplyOrderByDocNo(docNo?.Trim(), docType?.Trim());

        }

        [HttpGet]
        public HIReturnMessageModel<List<HI_R_SIGN_LINKED>> GetRSignLinkedByDocNo(string docNo, string docType)
        {
            return HIHazardNotifyBusiness.Instance.GetRSignLinkedByDocNo(docNo?.Trim(), docType?.Trim());

        }

        /// <summary>
        ///   Lay ra lich su ky duyet + wating station 
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<List<HI_R_SIGN_DETAIL>> GetHistorySign(string docNo, string docType)
        {
            return HIHazardNotifyBusiness.Instance.GetHistorySign(docNo?.Trim(), docType?.Trim());

        }
        /// <summary>
        /// luu ghi chu cua ng kiem tra
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<object> SaveRemarkOfChecker(SaveRemarkOfCheckerReq dat)
        {
            return HIHazardNotifyBusiness.Instance.SaveRemarkOfChecker(dat);

        }


        /// <summary>
        /// update chi tiet vi tri cua ng kiem tra
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<object> SavePositionDetailOfChecker(SavePositionDetailOfCheckerReq dat)
        {
            return HIHazardNotifyBusiness.Instance.SavePositionDetailOfChecker(dat);
        }


        /// <summary>
        /// Update handler cho Don thong bao
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<object> UpdateHandlerForHazardApp(UpdateHandlerForHazardAppReq dat)
        {
            return HIHazardNotifyBusiness.Instance.UpdateHandlerForHazardApp(dat);
        }

        /// <summary>
        ///  luu  ghi chu cua ng xu ly
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<object> SaveRemarkOfHandler(SaveRemarkOfHandlerReq dat)
        {
            return HIHazardNotifyBusiness.Instance.SaveRemarkOfHandler(dat);
        }

        /// <summary>
        /// Update some inofr 1 of hazardNotify
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HIReturnMessageModel<object> SaveSomeHazardInfor1(SaveSomeHazardInfor1Req dat)
        {
            return HIHazardNotifyBusiness.Instance.SaveSomeHazardInfor1(dat);
        }

        [HttpPost]
        public HIReturnMessageModel<ElTableRes> GetHazardNotifyAppLs(HazardNotifyAppLsReq dat
            )
        {
            return HIHazardNotifyBusiness.Instance.GetHazardNotifyAppLs(dat);
        }




        /// <summary>
        /// Download notify app ls in excel file
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DownLoadExcelForGetHazardNotifyAppLs(HazardNotifyAppLsReq dat)
        {

            try
            {
                dat.page = 1;
                dat.pageSize = 100;
                var rsData = HIHazardNotifyBusiness.Instance.GetHazardNotifyAppLs(dat, true);

                var datLs = rsData.data.items as List<HI_R_HAZARD_NOTIFY>;
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Danh sách đơn thông báo");
                // set header 
                IRow row0 = sheet.CreateRow(0);
                ExcelHelper.CreateCell(row0, 0, "Số thứ tự");
                ExcelHelper.CreateCell(row0, 1, "Mã đầu đơn");
                ExcelHelper.CreateCell(row0, 2, "Trạng thái");
                ExcelHelper.CreateCell(row0, 3, "Mã thẻ nhân viên kiểm tra");
                ExcelHelper.CreateCell(row0, 4, "Họ tên nhân viên kiểm tra");
                ExcelHelper.CreateCell(row0, 5, "Tên bộ phận nhân viên kiểm tra");
                ExcelHelper.CreateCell(row0, 6, "Mail nhân viên kiểm tra");
                ExcelHelper.CreateCell(row0, 7, "Nhà xưởng");
                ExcelHelper.CreateCell(row0, 8, "Đơn vị phát hiện mối nguy");
                ExcelHelper.CreateCell(row0, 9, "Tòa xưởng");
                ExcelHelper.CreateCell(row0, 10, "Tầng");
                ExcelHelper.CreateCell(row0, 11, "Mã thẻ nhân viên xử lý");
                ExcelHelper.CreateCell(row0, 12, "Họ tên nhân viên xử lý");
                ExcelHelper.CreateCell(row0, 13, "Tên bộ phận nhân viên xử lý");
                ExcelHelper.CreateCell(row0, 14, "Email nhân viên xử lý");
                ExcelHelper.CreateCell(row0, 15, "Loại hình mối nguy");
                ExcelHelper.CreateCell(row0, 16, "Mức độ nguy hiểm");
                ExcelHelper.CreateCell(row0, 17, "Mức độ ưu tiên");
                ExcelHelper.CreateCell(row0, 18, "Phần kiểm tra này có thuộc dự án nào không?");
                ExcelHelper.CreateCell(row0, 19, "Danh sách dự án");
                ExcelHelper.CreateCell(row0, 20, "Thời gian phát hiện mối nguy");
                ExcelHelper.CreateCell(row0, 21, "Mốc thời gian cải thiện");
                ExcelHelper.CreateCell(row0, 22, "Mã thẻ người ký tiếp theo");
                ExcelHelper.CreateCell(row0, 23, "Họ tên người ký tiếp theo");
                ExcelHelper.CreateCell(row0, 24, "Email người ký tiếp theo");
                ExcelHelper.CreateCell(row0, 25, "Ghi chú của người kiểm tra");
                ExcelHelper.CreateCell(row0, 26, "Ghi chú của người xử lý");
                ExcelHelper.CreateCell(row0, 27, "Vị trí chi tiết");
                ExcelHelper.CreateCell(row0, 28, "Mô tả mối nguy");

                if (datLs != null && datLs.Count > 0)
                {
                    for (int i = 0; i < datLs.Count; i++)
                    {
                        var medicine = datLs[i];
                        IRow row1 = sheet.CreateRow(i + 1);
                        var docIsWaiting = medicine.F_DOC_STATUS.Contains("Waiting") || medicine.F_DOC_STATUS.Contains("Rejected");

                        ExcelHelper.CreateCell(row1, 0, (i + 1).ToString());
                        ExcelHelper.CreateCell(row1, 1, medicine.F_DOCNO);
                        ExcelHelper.CreateCell(row1, 2, medicine.F_DOC_STATUS);
                        ExcelHelper.CreateCell(row1, 3, medicine.F_CHECK_EMPNO);
                        ExcelHelper.CreateCell(row1, 4, medicine.F_CHECK_EMPNAME);
                        ExcelHelper.CreateCell(row1, 5, medicine.F_CHECK_EMPDEPT);
                        ExcelHelper.CreateCell(row1, 6, medicine.F_CHECK_EMPMAIL);
                        ExcelHelper.CreateCell(row1, 7, medicine.F_FACTORY);
                        ExcelHelper.CreateCell(row1, 8, medicine.F_UNIT);
                        ExcelHelper.CreateCell(row1, 9, medicine.F_FACTORY_BUILDING);
                        ExcelHelper.CreateCell(row1, 10, medicine.F_FLOOR);
                        ExcelHelper.CreateCell(row1, 11, medicine.F_INCHARGE_EMPNO);
                        ExcelHelper.CreateCell(row1, 12, medicine.F_INCHARGE_EMPNAME);
                        ExcelHelper.CreateCell(row1, 13, medicine.F_INCHARGE_EMPDEPT);
                        ExcelHelper.CreateCell(row1, 14, medicine.F_INCHARGE_EMPMAIL);
                        ExcelHelper.CreateCell(row1, 15, medicine.F_DANGER_TYPE);
                        ExcelHelper.CreateCell(row1, 16, medicine.F_DANGER_LEVEL);
                        ExcelHelper.CreateCell(row1, 17, medicine.F_PRIORITY_LEVEL);
                        ExcelHelper.CreateCell(row1, 18, medicine.F_DOC_TYPE == "Y" ? "Có" : "Không");
                        ExcelHelper.CreateCell(row1, 19, medicine.F_PROJECT_NAMES);
                        ExcelHelper.CreateCell(row1, 20, medicine.F_CHECKTIME);
                        ExcelHelper.CreateCell(row1, 21, medicine.F_IMPROVEMENT_DAY);
                        ExcelHelper.CreateCell(row1, 22, docIsWaiting ? medicine.F_SIGN_EMP : "");
                        ExcelHelper.CreateCell(row1, 23, docIsWaiting ? medicine.F_SIGN_NAME : "");
                        ExcelHelper.CreateCell(row1, 24, docIsWaiting ? medicine.F_SIGN_MAIL : "");
                        ExcelHelper.CreateCell(row1, 25, medicine.F_CHECK_REMARK);
                        ExcelHelper.CreateCell(row1, 26, medicine.F_INCHARGE_EMPREMARK);
                        ExcelHelper.CreateCell(row1, 27, medicine.F_POSITION_DETAIL);
                        ExcelHelper.CreateCell(row1, 28, medicine.F_HAZARD_AI_CONTENT);

                    }
                }

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





        /// <summary>
        ///  Get total count HIapp waiting you sign
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<int?> CountHIAppWaitingYouSign()
        {
            return HIHazardNotifyBusiness.Instance.CountHIAppWaitingYouSign();
        }

        /// <summary>
        /// /// Get total count app waiting you sign 
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public HIReturnMessageModel<int?> GetToTalCountAppWaitingSign()
        {
            return HIHazardNotifyBusiness.Instance.GetToTalCountAppWaitingSign();
        }

        /// <summary>
        /// Get some indexes of notify app
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HIReturnMessageModel<object> GetIndexesNotifyApp()
        {
            return HIHazardNotifyBusiness.Instance.GetIndexesNotifyApp();
        }
    }
}
