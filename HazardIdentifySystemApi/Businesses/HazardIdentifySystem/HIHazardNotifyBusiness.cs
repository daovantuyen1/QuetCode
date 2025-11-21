using Dapper;
using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIHazardNotifyBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIHazardNotifyBusiness() { }
        private static HIHazardNotifyBusiness _instance;
        public static HIHazardNotifyBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIHazardNotifyBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion



        public bool UpdateRApplyOrder(HI_R_APPLY_ORDER dat, string sqlSet, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection).Execute(
                    $@"
                        UPDATE R_APPLY_ORDER
                           SET 
                         {sqlSet}
                        WHERE F_APPLY_NO = @F_APPLY_NO

                ", dat, trs) > 0 ? true : false;

        }

        public bool UpdateRSignLinked(HI_R_SIGN_LINKED dat, string sqlSet, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection).Execute(
                    $@"
                        UPDATE R_SIGN_LINKED
                           SET 
                         {sqlSet}
                        WHERE F_ROW_ID = @F_ROW_ID

                ", dat, trs) > 0 ? true : false;

        }

        public bool UpdateRHazardNotify(HI_R_HAZARD_NOTIFY dat, string sqlSet, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection).Execute(
                  $@"
                UPDATE R_HAZARD_NOTIFY
                   SET
                     {sqlSet}
                 WHERE  F_DOCNO =  @F_DOCNO

                ", dat, trs) > 0 ? true : false;

        }


        public void AddRApplyOrder(HI_R_APPLY_ORDER dat, SqlTransaction trs = null)
        {
            (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection).Execute(
            @"
                INSERT INTO R_APPLY_ORDER
                           (
		                    F_APPLY_NO
                           ,F_APPLY_TYPE
                           ,F_APPLY_EMP
                           ,F_APPLY_NAME
                           ,F_APPLY_TIME
                           ,F_STATUS
                           ,F_SIGN_STATION_NAME
                           ,F_SIGN_STATION_NO
                           ,F_SIGN_EMP
                           ,F_FLOW_NAME
                           ,F_FLOW_ROW_ID
                           ,F_DATA2
                           ,F_DATA3
                           ,F_SIGN_AGENT_EMP
                           ,F_SIGN_AGENT_NAME
                           ,F_SIGN_AGENT_MAIL
                           ,F_CREATE_EMP
                           ,F_CREATE_TIME
		                   )
                     VALUES
	                 (
                            @F_APPLY_NO
                           ,@F_APPLY_TYPE
                           ,@F_APPLY_EMP
                           ,@F_APPLY_NAME
                           ,GETDATE()
                           ,@F_STATUS
                           ,@F_SIGN_STATION_NAME
                           ,@F_SIGN_STATION_NO
                           ,@F_SIGN_EMP
                           ,@F_FLOW_NAME
                           ,@F_FLOW_ROW_ID
                           ,@F_DATA2
                           ,@F_DATA3
                           ,@F_SIGN_AGENT_EMP
                           ,@F_SIGN_AGENT_NAME
                           ,@F_SIGN_AGENT_MAIL
                           ,@F_CREATE_EMP
                           ,GETDATE()
		   
		                   )
                ", dat, trs);

        }



        public void AddRSignLinked(HI_R_SIGN_LINKED dat, SqlTransaction trs = null)
        {
            (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
              .Execute(
            @"
                          INSERT INTO  R_SIGN_LINKED
                           (
		                    F_ROW_ID
                           ,F_APPLY_NO
                           ,F_FLOW_NAME
                           ,F_FLOW_ROW_ID
                           ,F_STATION_NAME
                           ,F_SIGN_SORT
                           ,F_RETURN_SORT
                           ,F_SIGN_EMP
                           ,F_SIGN_NAME
                           ,F_SIGN_MAIL
                           ,F_POSITION
                           ,F_CONFIG_FLAG
                           ,F_MAIL_FLAG
                           ,F_EMP_LEVEL
                           ,F_EMP_LEVEL_OF_SORT
                           ,F_NODE_INPUT_EMP
                           ,F_NODE_UP_FILE
                           ,F_NODE_REMARK
                           ,F_CREATE_EMP
                           ,F_CREATE_TIME
                        
                   )
                     VALUES
                          (
		                    dbo.GET_NEW_ROWID(NEWID())
                           ,@F_APPLY_NO
                           ,@F_FLOW_NAME
                           ,@F_FLOW_ROW_ID
                           ,@F_STATION_NAME
                           ,@F_SIGN_SORT
                           ,@F_RETURN_SORT
                           ,@F_SIGN_EMP
                           ,@F_SIGN_NAME
                           ,@F_SIGN_MAIL
                           ,@F_POSITION
                           ,@F_CONFIG_FLAG
                           ,@F_MAIL_FLAG
                           ,@F_EMP_LEVEL
                           ,@F_EMP_LEVEL_OF_SORT
                           ,@F_NODE_INPUT_EMP
                           ,@F_NODE_UP_FILE
                           ,@F_NODE_REMARK
                           ,@F_CREATE_EMP
                           ,GETDATE()
                         
		                  )
                                ", dat, trs);

        }



        public void AddRSignDetail(HI_R_SIGN_DETAIL dat, SqlTransaction trs = null)
        {
            (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection).Execute(
            @"
                          INSERT INTO R_SIGN_DETAIL
                               (
		                       F_ROW_ID
                               ,F_APPLY_NO
                               ,F_FLOW_NAME
                               ,F_FLOW_ROW_ID
                               ,F_STATION_NAME
                               ,F_SIGN_SORT
                               ,F_RETURN_SORT
                               ,F_SIGN_EMP
                               ,F_SIGN_NAME
                               ,F_SIGN_MAIL
                               ,F_POSITION
                               ,F_CONFIG_FLAG
                               ,F_MAIL_FLAG
                               ,F_EMP_LEVEL
                               ,F_EMP_LEVEL_OF_SORT
                               ,F_NODE_INPUT_EMP
                               ,F_NODE_UP_FILE
                               ,F_NODE_REMARK
                               ,F_CREATE_EMP
                               ,F_CREATE_TIME
                               , F_SIGN_STATUS
		                       )
                         VALUES
                                  (
		                        DBO.GET_NEW_ROWID(NEWID())
                               ,@F_APPLY_NO
                               ,@F_FLOW_NAME
                               ,@F_FLOW_ROW_ID
                               ,@F_STATION_NAME
                               ,@F_SIGN_SORT
                               ,@F_RETURN_SORT
                               ,@F_SIGN_EMP
                               ,@F_SIGN_NAME
                               ,@F_SIGN_MAIL
                               ,@F_POSITION
                               ,@F_CONFIG_FLAG
                               ,@F_MAIL_FLAG
                               ,@F_EMP_LEVEL
                               ,@F_EMP_LEVEL_OF_SORT
                               ,@F_NODE_INPUT_EMP
                               ,@F_NODE_UP_FILE
                               ,@F_NODE_REMARK
                               ,@F_CREATE_EMP
                               ,GETDATE() 
                               ,@F_SIGN_STATUS
		                       )

                                ", dat, trs);

        }

        public List<HI_R_SIGN_LINKED> GetRSignLinked(string applyNo, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                   .Query<HI_R_SIGN_LINKED>(@"
                    SELECT F_ROW_ID
                          ,F_APPLY_NO
                          ,F_FLOW_NAME
                          ,F_FLOW_ROW_ID
                          ,F_STATION_NAME
                          ,F_SIGN_SORT
                          ,F_RETURN_SORT
                          ,F_SIGN_EMP
                          ,F_SIGN_NAME
                          ,F_SIGN_MAIL
                          ,F_POSITION
                          ,F_CONFIG_FLAG
                          ,F_MAIL_FLAG
                          ,F_EMP_LEVEL
                          ,F_EMP_LEVEL_OF_SORT
                          ,F_NODE_INPUT_EMP
                          ,F_NODE_UP_FILE
                          ,F_NODE_REMARK
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss')   AS F_CREATE_TIME
                          ,F_UPDATE_EMP
                          , FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS F_UPDATE_TIME
                      FROM R_SIGN_LINKED WHERE F_APPLY_NO = @F_APPLY_NO
                      ORDER BY F_SIGN_SORT ASC

                    ", new
                   {
                       F_APPLY_NO = applyNo
                   }, trs).ToList();




        }


        public List<HI_R_SIGN_DETAIL> GetRSignDetail(string applyNo, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                   .Query<HI_R_SIGN_DETAIL>(@"
                 SELECT F_ROW_ID
                          ,F_APPLY_NO
                          ,F_FLOW_NAME
                          ,F_FLOW_ROW_ID
                          ,F_STATION_NAME
                          ,F_SIGN_SORT
                          ,F_RETURN_SORT
                          ,F_SIGN_EMP
                          ,F_SIGN_NAME
                          ,F_SIGN_MAIL
                          ,F_POSITION
                          ,F_CONFIG_FLAG
                          ,F_MAIL_FLAG
                          ,F_EMP_LEVEL
                          ,F_EMP_LEVEL_OF_SORT
                          ,F_NODE_INPUT_EMP
                          ,F_NODE_UP_FILE
                          ,F_NODE_REMARK
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') AS F_CREATE_TIME
                          , F_SIGN_STATUS
                      FROM R_SIGN_DETAIL  WHERE F_APPLY_NO= @F_APPLY_NO
                      ORDER BY F_CREATE_TIME ASC

                    ", new
                   {
                       F_APPLY_NO = applyNo
                   }, trs).ToList();




        }



        public HI_R_HAZARD_NOTIFY GetRHaZardNotify(string docNo, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                   .QueryFirstOrDefault<HI_R_HAZARD_NOTIFY>(@"
                 SELECT F_DOCNO
                          ,F_DOC_STATUS
                          ,F_CHECK_EMPNO
                          ,F_CHECK_EMPNAME
                          ,F_CHECK_EMPDEPT
                          ,F_CHECK_REMARK
                          ,F_UNIT
                          ,F_FACTORY_BUILDING
                          ,F_FLOOR
                          , FORMAT( F_CHECKTIME  , 'yyyy/MM/dd HH:mm:ss') AS  F_CHECKTIME
                          ,F_DANGER_TYPE
                          ,F_DANGER_LEVEL
                          ,FORMAT( F_IMPROVEMENT_DAY  , 'yyyy/MM/dd HH:mm:ss')  AS F_IMPROVEMENT_DAY
                          ,F_IMAGE_ID
                          ,F_INCHARGE_EMPNO
                          ,F_INCHARGE_EMPNAME
                          ,F_INCHARGE_EMPDEPT
                          ,F_INCHARGE_EMPREMARK
                          ,F_HAZARD_DESC
                          ,F_HAZARD_BASIC_STANDARD
                          ,F_IMPROVE_COUNTER_MEASURE
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss')   AS F_CREATE_TIME
                          ,F_UPDATE_EMP
                          ,FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS F_UPDATE_TIME
                          ,F_FACTORY
                          ,F_FIX_IMAGE_ID
                          ,F_INCHARGE_EMPMAIL
                          ,F_CHECK_EMPMAIL
                          ,F_PIC_BOSS_EMPNO
                          ,F_PIC_BOSS_NAME
                          ,F_PIC_BOSS_MAIL
                          ,F_DOC_TYPE
                          ,F_APPLY_NO
                          ,F_HANDLE_STATUS
                          ,F_PRIORITY_LEVEL
                          ,F_HAZARD_AI_CONTENT
                          ,F_PROJECT_NAMES 
                          ,F_POSITION_DETAIL
                      FROM R_HAZARD_NOTIFY
                      WHERE  F_DOCNO = @F_DOCNO

                    ", new
                   {
                       F_DOCNO = docNo
                   }, trs);




        }




        public HI_R_APPLY_ORDER GetRApplyOrder(string applyNo, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection)
                   .QueryFirstOrDefault<HI_R_APPLY_ORDER>(@"
                    SELECT F_APPLY_NO
                          ,F_APPLY_TYPE
                          ,F_APPLY_EMP
                          ,F_APPLY_NAME
                          ,FORMAT( F_APPLY_TIME  , 'yyyy/MM/dd HH:mm:ss') AS F_APPLY_TIME
                          ,F_STATUS
                          ,F_SIGN_STATION_NAME
                          ,F_SIGN_STATION_NO
                          ,F_SIGN_EMP
                          ,F_FLOW_NAME
                          ,F_FLOW_ROW_ID
                          ,F_DATA2
                          ,F_DATA3
                          ,F_SIGN_AGENT_EMP
                          ,F_SIGN_AGENT_NAME
                          ,F_SIGN_AGENT_MAIL
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss')  AS F_CREATE_TIME
                          ,F_UPDATE_EMP
                          ,FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss') AS F_UPDATE_TIME
                      FROM R_APPLY_ORDER WHERE F_APPLY_NO = @F_APPLY_NO

                    ", new
                   {
                       F_APPLY_NO = applyNo
                   }, trs);




        }







        public void AddHanZardAIContentLog(HI_R_HAZARD_AI_LOG_CONTENT dat, SqlTransaction trs = null)
        {
            (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection).Execute(
            @"
                    INSERT INTO R_HAZARD_AI_LOG_CONTENT
                               (
		                        F_ROW_ID
                               ,F_DOCNO
                               ,F_CONTENT
                               ,F_TYPE
                               ,F_SORT
                               ,F_IS_USERADD
                               ,F_CREATE_EMP
                               ,F_CREATE_TIME   )
                         VALUES
                               (
		                        dbo.GET_NEW_ROWID(NEWID())
                               ,@F_DOCNO
                               ,@F_CONTENT
                               ,@F_TYPE
                               ,@F_SORT
                               ,@F_IS_USERADD
                               ,@F_CREATE_EMP
                               ,GETDATE()
		                       )
                    ", dat, trs);

        }



        public bool AddNewNotifyHanzard(HI_R_HAZARD_NOTIFY dat, SqlTransaction trs = null)
        {
            return (trs == null ? HIDbHelper.Instance.GetDBCnn() : trs.Connection).Execute(@"
                INSERT INTO R_HAZARD_NOTIFY
                           (
                            F_DOCNO
                           ,F_DOC_STATUS
                           ,F_CHECK_EMPNO
                           ,F_CHECK_EMPNAME
                           ,F_CHECK_EMPDEPT
                           ,F_CHECK_REMARK
                           ,F_UNIT
                           ,F_FACTORY_BUILDING
                           ,F_FLOOR
                           ,F_CHECKTIME
                           ,F_DANGER_TYPE
                           ,F_DANGER_LEVEL
                           ,F_IMPROVEMENT_DAY
                           ,F_IMAGE_ID
                           ,F_INCHARGE_EMPNO
                           ,F_INCHARGE_EMPNAME
                           ,F_INCHARGE_EMPDEPT
                           ,F_INCHARGE_EMPREMARK
                           ,F_HAZARD_DESC
                           ,F_HAZARD_BASIC_STANDARD
                           ,F_IMPROVE_COUNTER_MEASURE
                           ,F_HAZARD_AI_CONTENT
                           ,F_CREATE_EMP
                           ,F_CREATE_TIME
                           ,F_FACTORY
                           ,F_FIX_IMAGE_ID
                           ,F_INCHARGE_EMPMAIL
                           ,F_CHECK_EMPMAIL
                           ,F_PIC_BOSS_EMPNO
                           ,F_PIC_BOSS_NAME
                           ,F_PIC_BOSS_MAIL
                           ,F_DOC_TYPE
                           ,F_APPLY_NO
                           ,F_HANDLE_STATUS
                           ,F_PRIORITY_LEVEL
                           ,F_PROJECT_NAMES 
                           ,F_POSITION_DETAIL

            )
                     VALUES
                           (@F_DOCNO
                           ,@F_DOC_STATUS
                           ,@F_CHECK_EMPNO
                           ,@F_CHECK_EMPNAME
                           ,@F_CHECK_EMPDEPT
                           ,@F_CHECK_REMARK
                           ,@F_UNIT
                           ,@F_FACTORY_BUILDING
                           ,@F_FLOOR
                           ,@F_CHECKTIME
                           ,@F_DANGER_TYPE
                           ,@F_DANGER_LEVEL
                           ,@F_IMPROVEMENT_DAY
                           ,@F_IMAGE_ID
                           ,@F_INCHARGE_EMPNO
                           ,@F_INCHARGE_EMPNAME
                           ,@F_INCHARGE_EMPDEPT
                           ,@F_INCHARGE_EMPREMARK
                           ,@F_HAZARD_DESC
                           ,@F_HAZARD_BASIC_STANDARD
                           ,@F_IMPROVE_COUNTER_MEASURE
                           ,@F_HAZARD_AI_CONTENT
                           ,@F_CREATE_EMP
                           ,GETDATE()
                           ,@F_FACTORY
                           , '' 
                           ,@F_INCHARGE_EMPMAIL
                           ,@F_CHECK_EMPMAIL
                           ,@F_PIC_BOSS_EMPNO
                           ,@F_PIC_BOSS_NAME
                           ,@F_PIC_BOSS_MAIL
                           ,@F_DOC_TYPE
                           ,@F_APPLY_NO
                           ,@F_HANDLE_STATUS
                           ,@F_PRIORITY_LEVEL
                           ,@F_PROJECT_NAMES 
                           ,@F_POSITION_DETAIL
                            )

                ", dat, trs) > 0 ? true : false;
        }


        /// <summary>
        /// Ng kiem tra nhan tao don 
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> DoNotifyHazard(DoNotifyHazardReq dat)
        {
            try
            {
                if (dat == null
                    //|| dat.AnalysisLs == null || dat.AnalysisLs.Count <= 0
                    || dat.checkEmpNo.isNullStr()
                    || dat.factory.isNullStr()
                    || dat.unit.isNullStr()
                    || dat.factoryBuilding.isNullStr()
                    || dat.floor.isNullStr()
                    || dat.dangerType.isNullStr()
                    || dat.dangerLevel.isNullStr()
                    //|| dat.checkRemark.isNullStr()
                    || dat.improvementDay.isNullStr()
                    || dat.imageId.isNullStr()
                      || dat.inchargeEmpNo.isNullStr()
                       || dat.inchargeEmpName.isNullStr()
                      // || dat.inchargeEmpMail.isNullStr()
                      || dat.docType.isNullStr()
                      //  || dat.handleStatus.isNullStr()
                      || dat.priorityLevel.isNullStr()
                       || dat.hazardAiContent.isNullStr()
                    //  || dat.hazardBasisStandard.isNullStr()

                    )
                {

                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Vui lòng nhập vào các trường bắt buộc"), null);
                }


                if (dat.checkEmpMail.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Người kiểm tra không có email, vui lòng vào phần thông tin cá nhân thiết lập"), null);

                if (dat.inchargeEmpMail.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Người xử lý không có email, vui lòng liên hệ họ vào phần thông tin cá nhân thiết lập"), null);


                var acc = HIAccountBusiness.Instance.GetAccountFromSession();


                //2. ngay cai thien hop le ko ?
                var improvementDay = DateTime.ParseExact(dat.improvementDay, "yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"));
                if (improvementDay <= DateTime.Now)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Ngày cải thiện không được nhỏ hơn thời gian hiện tại"), null);
                //3. kiem tra ng check phải là login user
                if (dat.checkEmpNo != acc?.F_EMPNO)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Người kiểm tra phải là người đăng nhập hệ thống"), null);
                //

                if (dat.docType == "Y")
                {
                    if (dat.projectNames == null || dat.projectNames.Count() <= 0)
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Vui lòng chọn tên dự án"), null);
                }
                else
                {
                    dat.projectNames = null;
                }


                var docNo = HIConfigBusiness.Instance.GeneralDocNo("NHZ");
                var applyNo = HIConfigBusiness.Instance.GetNewId();


                //var DescribeTheHazardLs = dat.AnalysisLs.Where(r => r.type == KeyTypeAnalysis.DescribeTheHazard.ToString() && (!r.content.isNullStr()))
                //        .OrderBy(r => r.sort).ToList();

                //var RiskLevelLs = dat.AnalysisLs.Where(r => r.type == KeyTypeAnalysis.RiskLevel.ToString() && (!r.content.isNullStr()))
                //     .OrderBy(r => r.sort).ToList();

                //var SolutionAndProposedImplementationLs = dat.AnalysisLs.Where(r => r.type == KeyTypeAnalysis.SolutionAndProposedImplementation.ToString() && (!r.content.isNullStr()))
                // .OrderBy(r => r.sort).ToList();



                var sysdate = HIConfigBusiness.Instance.GetSysDate();
                if (sysdate.status == HIStatusType.error.ToString())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Lỗi lấy thời gian"), null);

                //string totalContentAiRender =
                //    "Describe The Hazard:"+Environment.NewLine +
                //    string.Join(Environment.NewLine, DescribeTheHazardLs.Select(r => r.content)) +Environment.NewLine
                //  +  "Risk Level:"+Environment.NewLine
                //  + string.Join(Environment.NewLine, RiskLevelLs.Select(r => r.content))+Environment.NewLine
                //  + "Solution And Proposed Implementation:"
                //    + string.Join(Environment.NewLine, SolutionAndProposedImplementationLs.Select(r => r.content))+Environment.NewLine+
                //    "Hazard Basis Standard:"+Environment.NewLine
                //   + "****************" + Environment.NewLine + dat.hazardBasisStandard?.Trim()
                ;

                using (var cnn = HIDbHelper.Instance.GetDBCnn())
                {
                    SqlTransaction trs = null;
                    try
                    {
                        cnn.Open();
                        trs = cnn.BeginTransaction();

                        // 1. them du lieu vao bang chinh luu content cua don 
                        var rs1 = AddNewNotifyHanzard(new HI_R_HAZARD_NOTIFY
                        {
                            F_DOCNO = docNo
                           ,
                            F_DOC_STATUS = "Waiting/Đợi ký"
                           ,
                            F_CHECK_EMPNO = dat.checkEmpNo?.Trim()?.ToUpper()
                           ,
                            F_CHECK_EMPNAME = dat.checkEmpName?.Trim()
                           ,
                            F_CHECK_EMPDEPT = dat.checkEmpDept?.Trim()
                           ,
                            F_CHECK_EMPMAIL = dat.checkEmpMail?.Trim()
                           ,
                            F_CHECK_REMARK = dat.checkRemark?.Trim()
                           ,
                            F_UNIT = dat.unit
                           ,
                            F_FACTORY_BUILDING = dat.factoryBuilding
                           ,
                            F_FLOOR = dat.floor
                           ,
                            F_CHECKTIME = dat.checkTime.isNullStr() ? sysdate?.data : dat.checkTime
                           ,
                            F_DANGER_TYPE = dat.dangerType
                           ,
                            F_DANGER_LEVEL = dat.dangerLevel
                           ,
                            F_IMPROVEMENT_DAY = dat.improvementDay
                           ,
                            F_IMAGE_ID = dat.imageId
                           ,
                            F_INCHARGE_EMPNO = dat.inchargeEmpNo?.Trim()?.ToUpper()
                           ,
                            F_INCHARGE_EMPNAME = dat.inchargeEmpName
                           ,
                            F_INCHARGE_EMPDEPT = dat?.inchargeEmpDept
                           ,
                            F_INCHARGE_EMPREMARK = ""
                           ,
                            F_HAZARD_DESC = ""
                           ,
                            F_HAZARD_BASIC_STANDARD = dat?.hazardBasisStandard?.Trim()
                           ,
                            F_IMPROVE_COUNTER_MEASURE = dat?.improvecountermeasures?.Trim()
                           ,
                            F_HAZARD_AI_CONTENT = dat.hazardAiContent?.Trim()
                            ,
                            F_CREATE_EMP = acc?.F_EMPNO
                           ,
                            F_FACTORY = dat.factory
                           ,
                            F_FIX_IMAGE_ID = ""
                           ,
                            F_INCHARGE_EMPMAIL = dat.inchargeEmpMail
                           ,
                            F_PIC_BOSS_EMPNO = dat.picBossEmpNo?.Trim()?.ToUpper()
                           ,
                            F_PIC_BOSS_NAME = dat.picBossEmpName
                           ,
                            F_PIC_BOSS_MAIL = dat.picBossEmpMail?.Trim()
                           ,
                            F_DOC_TYPE = dat.docType
                           ,
                            F_APPLY_NO = applyNo
                           ,
                            F_HANDLE_STATUS = dat.handleStatus
                           ,
                            F_PRIORITY_LEVEL = dat.priorityLevel
                           ,
                            F_PROJECT_NAMES = dat.projectNames == null ? "" : string.Join(";", dat.projectNames),
                            F_POSITION_DETAIL = dat.positionDetail?.Trim(),


                        }, trs);


                        //2. Add log ai phan tich
                        //var AiResultLs = new List<Analysis>();
                        //AiResultLs.AddRange(DescribeTheHazardLs);
                        //AiResultLs.AddRange(RiskLevelLs);
                        //AiResultLs.AddRange(SolutionAndProposedImplementationLs);
                        //if (AiResultLs.Count > 0)
                        //{
                        //    AiResultLs.ForEach(r =>
                        //    {
                        //        AddHanZardAIContentLog(new HI_R_HAZARD_AI_LOG_CONTENT
                        //        {
                        //            F_DOCNO = docNo,
                        //            F_CONTENT = r.content,
                        //            F_TYPE = r.type,
                        //            F_SORT = r.sort.ToString(),
                        //            F_IS_USERADD = r.isUserAdd ? "Y" : "N",
                        //            F_CREATE_EMP = acc?.F_EMPNO

                        //        }, trs);
                        //    });

                        //}
                        //3. r_applyoder, r_sign_linked,r_sign_detail , 
                        var newSignLinkedLs = new List<HI_R_SIGN_LINKED>();

                        //3.1 add r_sign_linked
                        newSignLinkedLs.Add(new HI_R_SIGN_LINKED
                        {
                            F_APPLY_NO = applyNo,
                            F_STATION_NAME = "Người làm đơn/Applicant/申请人",
                            F_SIGN_SORT = 100,
                            F_RETURN_SORT = 0,
                            F_SIGN_EMP = dat.checkEmpNo?.Trim()?.ToUpper(),
                            F_SIGN_NAME = dat.checkEmpName?.Trim(),
                            F_SIGN_MAIL = dat.checkEmpMail?.Trim(),
                            F_MAIL_FLAG = "Y",
                            F_CREATE_EMP = acc?.F_EMPNO,
                        });
                        if (!dat.picBossEmpNo.isNullStr())  // neu co nhan vien phe duyet tu bo phan EHS
                        {
                            newSignLinkedLs.Add(new HI_R_SIGN_LINKED
                            {
                                F_APPLY_NO = applyNo,
                                F_STATION_NAME = "Phê duyệt từ bộ phận EHS hoặc Chủ quản xưởng/Approval from EHS department or Factory Manager/来自EHS部门或车间主管的批准",
                                F_SIGN_SORT = 200,
                                F_RETURN_SORT = 100,
                                F_SIGN_EMP = dat.picBossEmpNo?.Trim()?.ToUpper(),
                                F_SIGN_NAME = dat.picBossEmpName?.Trim(),
                                F_SIGN_MAIL = dat.picBossEmpMail?.Trim(),
                                F_MAIL_FLAG = "Y",
                                F_CREATE_EMP = acc?.F_EMPNO,
                            });
                        }

                        newSignLinkedLs.Add(new HI_R_SIGN_LINKED
                        {
                            F_APPLY_NO = applyNo,
                            F_STATION_NAME = "Người xử lý/Handler/处理程序",
                            F_SIGN_SORT = (!dat.picBossEmpNo.isNullStr()) ? 300 : 200,
                            F_RETURN_SORT = (!dat.picBossEmpNo.isNullStr()) ? 200 : 100,
                            F_SIGN_EMP = dat.inchargeEmpNo?.Trim()?.ToUpper(),
                            F_SIGN_NAME = dat.inchargeEmpName?.Trim(),
                            F_SIGN_MAIL = dat.inchargeEmpMail?.Trim(),
                            F_MAIL_FLAG = "Y",
                            F_CREATE_EMP = acc?.F_EMPNO,
                        });

                        // Người kết án chinh la ng lam don
                        newSignLinkedLs.Add(new HI_R_SIGN_LINKED
                        {
                            F_APPLY_NO = applyNo,
                            F_STATION_NAME = "Người kết án/Finisher/整理器",
                            F_SIGN_SORT = (!dat.picBossEmpNo.isNullStr()) ? 400 : 300,
                            F_RETURN_SORT = (!dat.picBossEmpNo.isNullStr()) ? 300 : 100,
                            F_SIGN_EMP = dat.checkEmpNo?.Trim()?.ToUpper(),
                            F_SIGN_NAME = dat.checkEmpName?.Trim(),
                            F_SIGN_MAIL = dat.checkEmpMail?.Trim(),
                            F_MAIL_FLAG = "Y",
                            F_CREATE_EMP = acc?.F_EMPNO,
                        });


                        foreach (var linked in newSignLinkedLs)
                        {
                            AddRSignLinked(linked, trs);
                        }


                        //3.2 add r_applyoder
                        var nexStation = new HI_R_SIGN_LINKED();

                        var rSignLinkedLs = GetRSignLinked(applyNo, trs);
                        if ((!dat.picBossEmpNo.isNullStr()))
                        {
                            var next = rSignLinkedLs.FirstOrDefault(r => r.F_STATION_NAME.Contains("Phê duyệt từ bộ phận EHS") == true);

                            nexStation.F_STATION_NAME = next.F_STATION_NAME;
                            nexStation.F_ROW_ID = next.F_ROW_ID;
                            nexStation.F_SIGN_EMP = next.F_SIGN_EMP;
                        }
                        else
                        {
                            var next = rSignLinkedLs.FirstOrDefault(r => r.F_STATION_NAME.Contains("Người xử lý"));
                            nexStation.F_STATION_NAME = next.F_STATION_NAME;
                            nexStation.F_ROW_ID = next.F_ROW_ID;
                            nexStation.F_SIGN_EMP = next.F_SIGN_EMP;
                        }


                        AddRApplyOrder(new HI_R_APPLY_ORDER
                        {
                            F_APPLY_NO = applyNo,
                            F_APPLY_EMP = dat.checkEmpNo?.Trim()?.ToUpper(),
                            F_APPLY_NAME = dat.checkEmpName?.Trim(),
                            F_STATUS = "Waiting/Đợi ký",
                            F_SIGN_STATION_NAME = nexStation.F_STATION_NAME,
                            F_SIGN_STATION_NO = nexStation.F_ROW_ID,
                            F_SIGN_EMP = nexStation.F_SIGN_EMP,
                            F_CREATE_EMP = acc?.F_EMPNO,
                        }, trs);


                        //
                        //3.3 add R_SIGN_DETAIL
                        var newRSignDetail = rSignLinkedLs[0];
                        AddRSignDetail(new HI_R_SIGN_DETAIL
                        {
                            F_APPLY_NO = applyNo,
                            F_STATION_NAME = newRSignDetail.F_STATION_NAME,
                            F_SIGN_SORT = newRSignDetail.F_SIGN_SORT,
                            F_RETURN_SORT = newRSignDetail.F_RETURN_SORT,
                            F_SIGN_EMP = newRSignDetail.F_SIGN_EMP,
                            F_SIGN_NAME = newRSignDetail.F_SIGN_NAME,
                            F_SIGN_MAIL = newRSignDetail.F_SIGN_MAIL,
                            F_MAIL_FLAG = newRSignDetail.F_MAIL_FLAG,
                            F_NODE_REMARK = "",
                            F_CREATE_EMP = acc?.F_EMPNO,
                            F_SIGN_STATUS = "Approved/Đồng ý",
                        }, trs);

                        //
                        trs.Commit();
                        // cho gui mail den ng doi ky
                        var tempRappyOrder = GetRApplyOrder(applyNo);
                        var tempRSignLinked = GetRSignLinked(applyNo);
                        var tempNextSign = tempRSignLinked.FirstOrDefault(r => r.F_ROW_ID == tempRappyOrder.F_SIGN_STATION_NO);
                        var tempRHazardNotify = GetRHaZardNotify(docNo);

                        var ccMailStr = "";
                        if (
                                tempRHazardNotify.F_DANGER_LEVEL?.Contains("Lỗi lớn") == true
                             || tempRHazardNotify.F_DANGER_LEVEL?.Contains("Lỗi nghiêm trọng") == true
                             || tempRHazardNotify.F_DANGER_LEVEL?.Contains("Lỗi cực kỳ nghiêm trọng") == true
                         )
                        {
                            var ccMailLs = GetCCMailLsOfHazardApp(docNo);
                             ccMailStr = ccMailLs?.Count <= 0 ? "" : string.Join(";", ccMailLs);
                        }

                      
                        HIMailBusiness.Instance
                            .SendMailHazardNotifyAppWaitingSign(docNo, tempNextSign.F_SIGN_NAME, tempNextSign.F_SIGN_MAIL, tempNextSign.F_SIGN_EMP, ccMailStr);
                        //


                        HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã làm đơn thông báo mối nguy, đầu đơn {docNo}");


                        return HIReturnMessage.HIReturnSuccess<object>(LangHelper.Instance.Get("Gửi thành công đầu đơn:") + docNo, null);


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
                        HIFileBusiness.Instance.DeleteFile(dat.imageId);


                        HILogging.Instance.SaveLog($"[ERR-2025040513121] exception detail:[{ex1.Message + ex1.StackTrace}]");

                        return HIReturnMessage.HIReturnError<object>(ex1.Message, null);
                    }

                }



            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-2025040513122] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);
            }


        }



        public List<string> GetCCMailLsOfHazardApp(string docNo)
        {
            try
            {
                var lsCC = HIDbHelper.Instance.GetDBCnn().Query<dynamic>(@"
               SELECT 
	                C.F_BIG_BOSS_MAIL ,
					C.F_MAIL_CC
                FROM 
                ( SELECT * FROM R_HAZARD_NOTIFY WHERE F_DOCNO = @F_DOCNO ) A
                LEFT JOIN 
                C_PIC_ZONE C 
                ON ( A.F_FACTORY= C.F_FACTORY   AND A.F_UNIT =C.F_UNIT  AND A.F_FACTORY_BUILDING=C.F_FACTORY_BUILDING AND A.F_FLOOR =C.F_FLOOR AND A.F_INCHARGE_EMPNO=C.F_PIC_EMPNO )
           ", new { F_DOCNO = docNo?.Trim() }).ToList();

                if (lsCC?.Count > 0)
                {
                    var ccLs = new List<string>();
                    lsCC?.ForEach(r =>
                    {
                        if (!string.IsNullOrWhiteSpace(r?.F_BIG_BOSS_MAIL))
                        {
                            ccLs.Add(r?.F_BIG_BOSS_MAIL);
                        }

                        if (!string.IsNullOrWhiteSpace(r?.F_MAIL_CC))
                        {
                            ccLs.Add(r?.F_MAIL_CC);
                        }

                    });
                    if (ccLs.Count > 0)
                    {
                        string strArrTemp = string.Join(",", ccLs);
                        var arr1 = strArrTemp.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)?.ToList();
                        var arr2 = arr1.Where(r => (!string.IsNullOrWhiteSpace(r)) && (new[] { ";", "," }.Contains(r?.Trim()) == false)).Distinct().ToList();
                        return arr2;
                    }

                }


            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-202511080858] exception detail:[{ex.Message + ex.StackTrace}]");

            }

            return new List<string>();

        }


        /// <summary>
        /// Update fix image id  vao bang R_HAZARD_NOTIFY ( nguoi xu ly  da upload anh khac phuc-> cap nhat fix image id vao bang R_HAZARD_NOTIFY)
        /// </summary>
        /// <param name="imgId"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> UpdateFixImageId(string imgId, string docNo)
        {
            try
            {
                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                if (docNo.isNullStr()
                    )
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"), null);

                //var fileRs= HIFileBusiness.Instance.GetFile(imgId);
                //if(fileRs.status== HIStatusType.error.ToString())
                //    return HIReturnMessage.HIReturnError<object>(fileRs.message, null);


                var rs = HIDbHelper.Instance.GetDBCnn().Execute(
                         @"
                          UPDATE R_HAZARD_NOTIFY
                          SET
                           F_FIX_IMAGE_ID= @F_FIX_IMAGE_ID
                          WHERE F_DOCNO = @F_DOCNO 
                        ", new
                         {
                             F_FIX_IMAGE_ID = imgId?.Trim(),
                             F_DOCNO = docNo?.Trim()
                         }) > 0 ? true : false;
                if (rs)
                {
                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật hình ảnh cải thiện cho đơn thông báo [{docNo}] , dữ liệu cập nhật imgId=[{imgId}]");

                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                }

                else
                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202504051159] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
        }



        /// <summary>
        /// ky don thong bao
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> DoSignHazardNoityApp(SignHazardNoityAppReq dat)
        {
            try
            {
                if (dat == null
                   || dat.docNo.isNullStr()
                   || dat.signType.isNullStr()
                   )
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"), null);

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();

                var rhazarNotify = GetRHaZardNotify(dat.docNo?.Trim());
                var rApplyOrder = GetRApplyOrder(rhazarNotify?.F_APPLY_NO);
                var rSignLinked = GetRSignLinked(rhazarNotify?.F_APPLY_NO);
                if (rhazarNotify == null
                    || rApplyOrder == null
                    || rSignLinked == null
                    )
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có thông tin đầu đơn " + dat.docNo), null);

                if (new[] { "Closed/Đã đóng", "Removed/Đã hủy" }.Contains(rApplyOrder.F_STATUS))
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Đơn này đã đóng hoặc hủy"), null);
                }

                //kiem tra ban co phai la ng doi ky cua don hien tai ko ?


                var curStationWaitSign = rSignLinked.Where(r => r.F_ROW_ID == rApplyOrder.F_SIGN_STATION_NO).FirstOrDefault();
                if (
                    rApplyOrder.F_SIGN_EMP == acc?.F_EMPNO
                    && curStationWaitSign.F_SIGN_EMP == acc?.F_EMPNO
                    )
                {
                    // dung ban la ng doi ky cua don
                }
                else
                {
                    if (dat.signType == SignType.remove.ToString())
                    {
                        bool isYouHaveRighsRemoveApp = acc?.F_ROLES?.Where(r => r.F_ROLE == "Admin" || r.F_ROLE == "Quyền hủy đơn thông báo")?.Count() > 0;
                        if (isYouHaveRighsRemoveApp)
                        {
                            goto aaPoint;
                        }
                    }

                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không phải là người đợi ký của đơn hiện tại"), null);


                }


            aaPoint:

                var nextStation = rSignLinked.Where(r => r.F_SIGN_SORT > curStationWaitSign.F_SIGN_SORT).OrderBy(r => r.F_SIGN_SORT)
                    .FirstOrDefault();


                if (dat.signType == SignType.agree.ToString())  // ky tiep
                {
                    string nextDocStatus = "Waiting/Đợi ký";
                    if (curStationWaitSign.F_ROW_ID == rSignLinked.Last().F_ROW_ID) // tram cuoi dung.
                    {
                        nextDocStatus = "Closed/Đã đóng";


                    }

                    if (curStationWaitSign.F_STATION_NAME.Contains("Người làm đơn"))
                    {
                        if (rhazarNotify.F_INCHARGE_EMPMAIL.isNullStr())
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Người xử lý không có email, vui lòng liên hệ họ vào phần thông tin cá nhân thiết lập"));

                        if (curStationWaitSign.F_SIGN_MAIL.isNullStr())
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Người kiểm tra không có email, vui lòng vào phần thông tin cá nhân thiết lập"));

                    }

                    if (curStationWaitSign.F_STATION_NAME.Contains("Người xử lý") == true)
                    {
                        // kiem tra tram Người xử lý  :da nhap ghi chú của nhân viên xử lý chua ?
                        if (string.IsNullOrWhiteSpace(rhazarNotify?.F_INCHARGE_EMPREMARK))
                        {
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn chưa nhập ghi chú của nhân viên xử lý"), null);

                        }
                        // kiem tra tram Người xử lý  : da upload anh cai thien chua?
                        if (string.IsNullOrWhiteSpace(rhazarNotify.F_FIX_IMAGE_ID))
                        {
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn chưa tải lên hình ảnh cải thiện"), null);

                        }

                    }


                    using (var cnn = HIDbHelper.Instance.GetDBCnn())
                    {
                        SqlTransaction trs = null;
                        try
                        {
                            cnn.Open();
                            trs = cnn.BeginTransaction();
                            UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                            {
                                F_DOCNO = dat.docNo?.Trim(),
                                F_DOC_STATUS = nextDocStatus,
                                F_UPDATE_EMP = acc?.F_EMPNO

                            }, @"
                                       F_DOC_STATUS = @F_DOC_STATUS ,
                                       F_UPDATE_EMP  = @F_UPDATE_EMP ,
                                       F_UPDATE_TIME  = GETDATE()
                                 ", trs);

                            UpdateRApplyOrder(new HI_R_APPLY_ORDER
                            {
                                F_APPLY_NO = rApplyOrder.F_APPLY_NO,
                                F_STATUS = nextDocStatus,
                                F_UPDATE_EMP = acc?.F_EMPNO,
                                F_SIGN_STATION_NAME = nextStation?.F_STATION_NAME,
                                F_SIGN_STATION_NO = nextStation?.F_ROW_ID,
                                F_SIGN_EMP = nextStation?.F_SIGN_EMP,
                            }, @"
                                     F_STATUS = @F_STATUS ,
                                     F_UPDATE_EMP= @F_UPDATE_EMP , 
	                                 F_UPDATE_TIME = GETDATE() ,
                                     F_SIGN_STATION_NAME  = @F_SIGN_STATION_NAME ,
                                     F_SIGN_STATION_NO= @F_SIGN_STATION_NO,
                                     F_SIGN_EMP  = @F_SIGN_EMP
                               ", trs);

                            AddRSignDetail(new HI_R_SIGN_DETAIL
                            {
                                F_APPLY_NO = curStationWaitSign.F_APPLY_NO,
                                F_STATION_NAME = curStationWaitSign.F_STATION_NAME,
                                F_SIGN_SORT = curStationWaitSign.F_SIGN_SORT,
                                F_RETURN_SORT = curStationWaitSign.F_RETURN_SORT,
                                F_SIGN_EMP = curStationWaitSign.F_SIGN_EMP,
                                F_SIGN_NAME = curStationWaitSign.F_SIGN_NAME,
                                F_SIGN_MAIL = curStationWaitSign.F_SIGN_MAIL,
                                F_MAIL_FLAG = curStationWaitSign.F_MAIL_FLAG,
                                F_NODE_REMARK = dat.remark?.Trim(),
                                F_CREATE_EMP = acc?.F_EMPNO,
                                F_SIGN_STATUS = "Approved/Đồng ý",

                            }, trs);
                            trs.Commit();

                            if (nextDocStatus == "Waiting/Đợi ký")
                            {
                                // gui mail den ng ky tiep theo
                                var tempRappyOrder = GetRApplyOrder(curStationWaitSign.F_APPLY_NO);
                                var tempRSignLinked = GetRSignLinked(curStationWaitSign.F_APPLY_NO);
                                var tempNextSign = tempRSignLinked.FirstOrDefault(r => r.F_ROW_ID == tempRappyOrder.F_SIGN_STATION_NO);


                                HIMailBusiness.Instance
                                    .SendMailHazardNotifyAppWaitingSign(dat.docNo, tempNextSign?.F_SIGN_NAME, tempNextSign?.F_SIGN_MAIL, tempNextSign?.F_SIGN_EMP, "");

                            }

                            return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);

                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                if (trs != null)
                                    trs.Rollback();
                            }
                            catch
                            {


                            }

                            HILogging.Instance.SaveLog($"[ERR-202507101050] exception detail:[{ex.Message + ex.StackTrace}]");

                            return HIReturnMessage.HIReturnError<object>(ex.Message);

                        }
                    }


                }

                if (dat.signType == SignType.reject.ToString())   //reject
                {
                    if (rApplyOrder.F_SIGN_STATION_NO == rSignLinked.First().F_ROW_ID) // tram tiep theo la tram dau tien trong luu trinh ky thi ko dk reject
                    {
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Hiện tại đang đợi trạm đầu tiên trong lưu trình ký ,không được phép reject"), null);

                    }

                    HI_R_SIGN_LINKED nextRejectStation = null;

                    if (dat.rejectTo.isNullStr() || dat.rejectTo == RejectTo.firstStation.ToString())
                    {
                        nextRejectStation = rSignLinked.First();

                    }
                    else if (dat.rejectTo == RejectTo.preStation.ToString())
                    {
                        nextRejectStation = rSignLinked.Where(r => r.F_SIGN_SORT < rSignLinked.FirstOrDefault(t => t.F_ROW_ID == rApplyOrder.F_SIGN_STATION_NO)?.F_SIGN_SORT)
                                       .OrderBy(r => r.F_SIGN_SORT).LastOrDefault();

                    }



                    using (var cnn = HIDbHelper.Instance.GetDBCnn())
                    {
                        SqlTransaction trs = null;
                        try
                        {

                            cnn.Open();
                            trs = cnn.BeginTransaction();
                            string nextDocStatus = "Rejected/Đã trả lại";
                            UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                            {
                                F_DOCNO = dat.docNo?.Trim(),
                                F_DOC_STATUS = nextDocStatus,
                                F_UPDATE_EMP = acc?.F_EMPNO

                            }, @"
                                       F_DOC_STATUS = @F_DOC_STATUS ,
                                       F_UPDATE_EMP  = @F_UPDATE_EMP ,
                                       F_UPDATE_TIME  = GETDATE()
                                 ", trs);

                            UpdateRApplyOrder(new HI_R_APPLY_ORDER
                            {
                                F_APPLY_NO = rApplyOrder.F_APPLY_NO,
                                F_STATUS = nextDocStatus,
                                F_UPDATE_EMP = acc?.F_EMPNO,
                                F_SIGN_STATION_NAME = nextRejectStation?.F_STATION_NAME,
                                F_SIGN_STATION_NO = nextRejectStation?.F_ROW_ID,
                                F_SIGN_EMP = nextRejectStation?.F_SIGN_EMP,
                            }, @"
                                     F_STATUS = @F_STATUS ,
                                     F_UPDATE_EMP= @F_UPDATE_EMP , 
	                                 F_UPDATE_TIME = GETDATE() ,
                                     F_SIGN_STATION_NAME  = @F_SIGN_STATION_NAME ,
                                     F_SIGN_STATION_NO= @F_SIGN_STATION_NO,
                                     F_SIGN_EMP  = @F_SIGN_EMP
                               ", trs);



                            AddRSignDetail(new HI_R_SIGN_DETAIL
                            {
                                F_APPLY_NO = curStationWaitSign.F_APPLY_NO,
                                F_STATION_NAME = curStationWaitSign.F_STATION_NAME,
                                F_SIGN_SORT = curStationWaitSign.F_SIGN_SORT,
                                F_RETURN_SORT = curStationWaitSign.F_RETURN_SORT,
                                F_SIGN_EMP = curStationWaitSign.F_SIGN_EMP,
                                F_SIGN_NAME = curStationWaitSign.F_SIGN_NAME,
                                F_SIGN_MAIL = curStationWaitSign.F_SIGN_MAIL,
                                F_MAIL_FLAG = curStationWaitSign.F_MAIL_FLAG,
                                F_NODE_REMARK = dat.remark?.Trim(),
                                F_CREATE_EMP = acc?.F_EMPNO,
                                F_SIGN_STATUS = "Rejected/Đã trả lại",

                            }, trs);
                            trs.Commit();

                            // gui mail den ng ky tiep theo 
                            var tempRappyOrder = GetRApplyOrder(curStationWaitSign.F_APPLY_NO);
                            var tempRSignLinked = GetRSignLinked(curStationWaitSign.F_APPLY_NO);
                            var tempNextSign = tempRSignLinked.FirstOrDefault(r => r.F_ROW_ID == tempRappyOrder.F_SIGN_STATION_NO);


                            HIMailBusiness.Instance.
                                SendMailHazardNotifyAppWaitingSign(dat.docNo, tempNextSign?.F_SIGN_NAME, tempNextSign?.F_SIGN_MAIL, tempNextSign?.F_SIGN_EMP, "");

                            //

                            return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);

                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                if (trs != null)
                                    trs.Rollback();
                            }
                            catch
                            {


                            }

                            HILogging.Instance.SaveLog($"[ERR-20250710105051] exception detail:[{ex.Message + ex.StackTrace}]");

                            return HIReturnMessage.HIReturnError<object>(ex.Message);


                        }
                    }



                }
                if (dat.signType == SignType.remove.ToString())  // huy don
                {

                    var firstStation = rSignLinked.First();

                    bool isWaitingAppCreater = (rApplyOrder.F_SIGN_STATION_NO == firstStation.F_ROW_ID
                        && rApplyOrder.F_SIGN_EMP == firstStation.F_SIGN_EMP && rApplyOrder.F_SIGN_EMP == acc?.F_EMPNO);
                    bool isYouHaveRighsRemoveApp = acc?.F_ROLES?.Where(r => r.F_ROLE == "Admin" || r.F_ROLE == "Quyền hủy đơn thông báo")?.Count() > 0;


                    //1. don phai dang doi ng tao don or ban co quyen huy don
                    if (
                        isWaitingAppCreater || isYouHaveRighsRemoveApp
                        )
                    {

                        using (var cnn = HIDbHelper.Instance.GetDBCnn())
                        {
                            SqlTransaction trs = null;
                            try
                            {
                                cnn.Open();
                                trs = cnn.BeginTransaction();
                                string nextDocStatus = "Removed/Đã hủy";
                                UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                                {
                                    F_DOCNO = dat.docNo?.Trim(),
                                    F_DOC_STATUS = nextDocStatus,
                                    F_UPDATE_EMP = acc?.F_EMPNO

                                }, @"
                                       F_DOC_STATUS = @F_DOC_STATUS ,
                                       F_UPDATE_EMP  = @F_UPDATE_EMP ,
                                       F_UPDATE_TIME  = GETDATE()
                                 ", trs);

                                UpdateRApplyOrder(new HI_R_APPLY_ORDER
                                {
                                    F_APPLY_NO = rApplyOrder.F_APPLY_NO,
                                    F_STATUS = nextDocStatus,
                                    F_UPDATE_EMP = acc?.F_EMPNO,
                                }, @"
                                     F_STATUS = @F_STATUS ,
                                     F_UPDATE_EMP= @F_UPDATE_EMP , 
	                                 F_UPDATE_TIME = GETDATE()
                               ", trs);
                                AddRSignDetail(new HI_R_SIGN_DETAIL
                                {
                                    F_APPLY_NO = firstStation.F_APPLY_NO,
                                    F_STATION_NAME = firstStation.F_STATION_NAME,
                                    F_SIGN_SORT = firstStation.F_SIGN_SORT,
                                    F_RETURN_SORT = firstStation.F_RETURN_SORT,
                                    F_SIGN_EMP = isWaitingAppCreater ? firstStation.F_SIGN_EMP : (isYouHaveRighsRemoveApp ? acc?.F_EMPNO : ""),
                                    F_SIGN_NAME = isWaitingAppCreater ? firstStation.F_SIGN_NAME : (isYouHaveRighsRemoveApp ? acc?.F_EMPNAME : ""),
                                    F_SIGN_MAIL = isWaitingAppCreater ? firstStation.F_SIGN_MAIL : (isYouHaveRighsRemoveApp ? acc?.F_MAIL : ""),
                                    F_MAIL_FLAG = firstStation.F_MAIL_FLAG,
                                    F_NODE_REMARK = isWaitingAppCreater ? dat.remark?.Trim() : (isYouHaveRighsRemoveApp ? dat.remark?.Trim() + $" | [{acc?.F_EMPNO}-{acc.F_EMPNAME} removed this DocNo]" : ""),
                                    F_CREATE_EMP = acc?.F_EMPNO,
                                    F_SIGN_STATUS = "Removed/Đã hủy",

                                }, trs);
                                trs.Commit();
                                return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);


                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    if (trs != null)
                                        trs.Rollback();
                                }
                                catch
                                {


                                }

                                HILogging.Instance.SaveLog($"[ERR-2025040513321] exception detail:[{ex.Message + ex.StackTrace}]");

                                return HIReturnMessage.HIReturnError<object>(ex.Message);
                            }
                        }


                    }
                    else
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không phải là người đợi ký tiếp theo của đơn này hoặc chỉ người tạo đơn mới có quyền hủy đơn"), null);

                }



            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405131418] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
            return null;
        }


        /// <summary>
        /// Nhac nho mail ky don thong bao
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> DoRemindToSignNotifyApp(string docNo)
        {
            try
            {

                if (docNo.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không tồn tại đơn này trên hệ thống"), null);
                var rApplyOrder = GetRapplyOrderByDocNo(docNo, "HazardNotify");
                if (rApplyOrder.status == StatusType.error.ToString())
                    return HIReturnMessage.HIReturnError<object>(rApplyOrder.message, null);

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();

                if (rApplyOrder.data?.F_APPLY_EMP != acc?.F_EMPNO)
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không có quyền giục ký"), null);

                }
                if ((rApplyOrder.data.F_STATUS.Contains("Waiting") || rApplyOrder.data.F_STATUS.Contains("Rejected")) == false)
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Đầu đơn hiện tại đã đóng hoặc hủy"), null);
                }

                var nextSignerInfor = HIElistQueryBusiness.Instance.GetEmpInfor(rApplyOrder.data.F_SIGN_EMP);
                if (nextSignerInfor.status == StatusType.error.ToString())
                    return HIReturnMessage.HIReturnError<object>(nextSignerInfor.message, null);

                var rsSendMail = HIMailBusiness.Instance.SendMailHazardNotifyAppWaitingSign(docNo, nextSignerInfor.data.USER_NAME, nextSignerInfor.data.EMAIL, rApplyOrder.data.F_SIGN_EMP, "");
                HIMailBusiness.Instance.SendNotifyToIcivet(rApplyOrder.data.F_SIGN_EMP, $@"
                 [SAFETY AI]
                 Bạn có đơn thông báo mối nguy {docNo} chờ bạn ký – Vui lòng truy cập hệ thống kiểm tra.
                 您有一份危险通知单 {docNo} 等待您签署 - 请访问系统进行检查
                ");

                if (rsSendMail.status == StatusType.error.ToString())
                    return HIReturnMessage.HIReturnError<object>(rsSendMail.message, null);
                return HIReturnMessage.HIReturnSuccess<object>(LangHelper.Instance.Get("Gửi mail thành công"), null);


            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-202507090916] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }

        }


        public HIReturnMessageModel<NotifyHazardTotalData> GetAllDataOfHazardNotifyByDocNo(string docNo)
        {
            try
            {

                var hanzardNotify = GetRHaZardNotify(docNo?.Trim());

                var finalDat = new NotifyHazardTotalData
                {
                    checkEmpNo = hanzardNotify?.F_CHECK_EMPNO,
                    checkEmpName = hanzardNotify?.F_CHECK_EMPNAME,
                    checkEmpDept = hanzardNotify?.F_CHECK_EMPDEPT,
                    checkEmpMail = hanzardNotify?.F_CHECK_EMPMAIL,
                    factory = hanzardNotify?.F_FACTORY,
                    unit = hanzardNotify?.F_UNIT,
                    factoryBuilding = hanzardNotify?.F_FACTORY_BUILDING,
                    floor = hanzardNotify?.F_FLOOR,
                    checkTime = hanzardNotify?.F_CHECKTIME,
                    dangerType = hanzardNotify?.F_DANGER_TYPE,
                    dangerLevel = hanzardNotify?.F_DANGER_LEVEL,
                    checkRemark = hanzardNotify?.F_CHECK_REMARK,
                    improvementDay = hanzardNotify?.F_IMPROVEMENT_DAY,
                    imageId = hanzardNotify?.F_IMAGE_ID,
                    imageUrl = HIFileBusiness.Instance.GetFile(hanzardNotify.F_IMAGE_ID)?.data?.F_FILE_PATH,
                    fixImgId = hanzardNotify?.F_FIX_IMAGE_ID,
                    fixImgUrl = HIFileBusiness.Instance.GetFile(hanzardNotify.F_FIX_IMAGE_ID)?.data?.F_FILE_PATH,
                    inchargeEmpNo = hanzardNotify?.F_INCHARGE_EMPNO,
                    inchargeEmpName = hanzardNotify?.F_INCHARGE_EMPNAME,
                    inchargeEmpDept = hanzardNotify?.F_INCHARGE_EMPDEPT,
                    inchargeEmpMail = hanzardNotify?.F_INCHARGE_EMPMAIL,
                    inchargeRemark = hanzardNotify?.F_INCHARGE_EMPREMARK,
                    picBossEmpNo = hanzardNotify?.F_PIC_BOSS_EMPNO,
                    picBossEmpName = hanzardNotify?.F_PIC_BOSS_NAME,
                    picBossEmpMail = hanzardNotify?.F_PIC_BOSS_MAIL,
                    docNo = hanzardNotify?.F_DOCNO,
                    docStatus = hanzardNotify?.F_DOC_STATUS,
                    docCompletedDate = hanzardNotify?.F_UPDATE_TIME,
                    docType = hanzardNotify?.F_DOC_TYPE,
                    projectNames = hanzardNotify?.F_DOC_TYPE == "Y" ? hanzardNotify.F_PROJECT_NAMES?.Split(';').ToList() : new List<string>(),
                    priorityLevel = hanzardNotify?.F_PRIORITY_LEVEL,
                    hazardAiContent = hanzardNotify?.F_HAZARD_AI_CONTENT,
                    hazardBasisStandard = hanzardNotify?.F_HAZARD_BASIC_STANDARD,
                    improvecountermeasures = hanzardNotify?.F_IMPROVE_COUNTER_MEASURE,
                    positionDetail = hanzardNotify?.F_POSITION_DETAIL
                };
                return HIReturnMessage.HIReturnSuccess<NotifyHazardTotalData>
                  (HIStatusType.success.ToString(), finalDat);


            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405130218] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<NotifyHazardTotalData>(ex.Message, null);
            }
        }


        public HIReturnMessageModel<HI_R_APPLY_ORDER> GetRapplyOrderByDocNo(string docNo, string docType)
        {
            try
            {

                string applyNo = "";
                if (docType == "HazardNotify")
                {
                    var hazardDat = GetRHaZardNotify(docNo?.Trim());
                    applyNo = hazardDat?.F_APPLY_NO;

                }
                var rApplyOrder = GetRApplyOrder(applyNo);

                return HIReturnMessage.HIReturnSuccess<HI_R_APPLY_ORDER>(HIStatusType.success.ToString(), rApplyOrder);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405130219] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<HI_R_APPLY_ORDER>(ex.Message, null);

            }


        }



        public HIReturnMessageModel<List<HI_R_SIGN_LINKED>> GetRSignLinkedByDocNo(string docNo, string docType)
        {
            try
            {

                string applyNo = "";
                if (docType == "HazardNotify")
                {
                    var hazardDat = GetRHaZardNotify(docNo?.Trim());
                    applyNo = hazardDat?.F_APPLY_NO;

                }
                var rSignLinked = GetRSignLinked(applyNo);


                return HIReturnMessage.HIReturnSuccess<List<HI_R_SIGN_LINKED>>(HIStatusType.success.ToString(), rSignLinked);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405130220] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<List<HI_R_SIGN_LINKED>>(ex.Message, null);

            }


        }






        /// <summary>
        ///  Lay ra lich su ky duyet + wating station 
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        public HIReturnMessageModel<List<HI_R_SIGN_DETAIL>> GetHistorySign(string docNo, string docType)
        {
            try
            {

                string applyNo = "";
                if (docType == "HazardNotify")
                {
                    var hazardDat = GetRHaZardNotify(docNo?.Trim());
                    applyNo = hazardDat?.F_APPLY_NO;

                }
                var totalHistorySign = new List<HI_R_SIGN_DETAIL>();
                var rSignDetail = GetRSignDetail(applyNo);
                var rSignLinked = GetRSignLinked(applyNo);

                totalHistorySign.AddRange(rSignDetail);

                var rApplyOrder = GetRApplyOrder(applyNo);
                if (new[] { "Closed/Đã đóng", "Removed/Đã hủy" }.Contains(rApplyOrder.F_STATUS) == false)
                // don dang doi ky
                {
                    var curNextStation = rSignLinked.FirstOrDefault(r => r.F_ROW_ID == rApplyOrder.F_SIGN_STATION_NO);

                    totalHistorySign.Add(new HI_R_SIGN_DETAIL
                    {
                        F_ROW_ID = "",
                        F_APPLY_NO = curNextStation?.F_APPLY_NO,
                        F_STATION_NAME = curNextStation?.F_STATION_NAME,
                        F_SIGN_SORT = curNextStation?.F_SIGN_SORT,
                        F_RETURN_SORT = curNextStation?.F_RETURN_SORT,
                        F_SIGN_EMP = curNextStation?.F_SIGN_EMP,
                        F_SIGN_NAME = curNextStation?.F_SIGN_NAME,
                        F_SIGN_MAIL = curNextStation?.F_SIGN_MAIL,
                        F_POSITION = curNextStation?.F_POSITION,
                        F_CONFIG_FLAG = curNextStation?.F_CONFIG_FLAG,
                        F_MAIL_FLAG = curNextStation?.F_MAIL_FLAG,
                        F_NODE_REMARK = curNextStation?.F_NODE_REMARK,
                        F_CREATE_EMP = curNextStation?.F_CREATE_EMP,
                        F_CREATE_TIME = curNextStation?.F_CREATE_TIME,
                        F_SIGN_STATUS = "Waiting/Đợi ký",

                    });

                }

                return HIReturnMessage.HIReturnSuccess<List<HI_R_SIGN_DETAIL>>(HIStatusType.success.ToString(), totalHistorySign);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405130221] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<List<HI_R_SIGN_DETAIL>>(ex.Message, null);

            }


        }

        /// <summary>
        /// update ghi chu cua ng kiem tra
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="checkRemark"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> SaveRemarkOfChecker(SaveRemarkOfCheckerReq dat)
        {
            try
            {

                string docNo = dat?.docNo;
                string checkRemark = dat?.checkRemark;

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                if (docNo.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"), null);
                if (checkRemark.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không được bỏ trống ghi chú"), null);
                var hazardInfor = GetRHaZardNotify(docNo?.Trim());
                if (hazardInfor == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có thông tin trên hệ thống của đầu đơn ") + docNo, null);
                if (hazardInfor.F_CHECK_EMPNO != acc?.F_EMPNO)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không phải là người tạo đơn"), null);

                var rs = UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                {
                    F_CHECK_REMARK = checkRemark?.Trim(),
                    F_DOCNO = docNo?.Trim(),
                    F_UPDATE_EMP = acc?.F_EMPNO,
                },
                      @"
                    F_CHECK_REMARK = @F_CHECK_REMARK ,
                    F_UPDATE_EMP = @F_UPDATE_EMP ,
                    F_UPDATE_TIME  = GETDATE()
                      ");
                if (rs)
                {
                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật ghi chú của ng kiểm tra cho đầu đơn {docNo?.Trim()}, với dữ liệu mới [{JsonConvert.SerializeObject(dat)}]");
                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                }
                else
                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405130222] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
        }



        /// <summary>
        /// update chi tiet vi tri cua ng kiem tra
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> SavePositionDetailOfChecker(SavePositionDetailOfCheckerReq dat)
        {
            try
            {

                string docNo = dat?.docNo;
                string positionDetail = dat?.positionDetail;

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                if (docNo.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"), null);
                if (positionDetail.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không được bỏ trống ghi chú"), null);
                var hazardInfor = GetRHaZardNotify(docNo?.Trim());
                if (hazardInfor == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có thông tin trên hệ thống của đầu đơn ") + docNo, null);
                if (hazardInfor.F_CHECK_EMPNO != acc?.F_EMPNO)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không phải là người tạo đơn"), null);

                var rs = UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                {
                    F_POSITION_DETAIL = positionDetail?.Trim(),
                    F_DOCNO = docNo?.Trim(),
                    F_UPDATE_EMP = acc?.F_EMPNO,
                },
                      @"
                    F_POSITION_DETAIL = @F_POSITION_DETAIL ,
                    F_UPDATE_EMP = @F_UPDATE_EMP ,
                    F_UPDATE_TIME  = GETDATE()
                      ");
                if (rs)
                {
                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật vị trí chi tiết của ng kiểm tra cho đầu đơn {docNo?.Trim()}, với dữ liệu mới [{JsonConvert.SerializeObject(dat)}]");
                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                }
                else
                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202507161728] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
        }



        /// <summary>
        /// Update handler cho Don thong bao
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> UpdateHandlerForHazardApp(UpdateHandlerForHazardAppReq dat)
        {

            try
            {



                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                if (dat == null
                    || dat.docNo.isNullStr()
                    || dat.inchargeEmpNo.isNullStr()
                    || dat.inchargeEmpName.isNullStr()
                    )
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"), null);

                if (dat.inchargeEmpMail.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Người xử lý không có email, vui lòng liên hệ họ vào phần thông tin cá nhân thiết lập"), null);


                var hazardInfor = GetRHaZardNotify(dat?.docNo?.Trim());
                if (hazardInfor == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có thông tin trên hệ thống của đầu đơn ") + dat?.docNo, null);
                if (hazardInfor.F_CHECK_EMPNO != acc?.F_EMPNO)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không phải là người tạo đơn"), null);

                var rApplyOrder = GetRapplyOrderByDocNo(dat.docNo?.Trim(), "HazardNotify");

                if (rApplyOrder.data.F_SIGN_STATION_NAME?.Contains("Người làm đơn") == false)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không phải là người tạo đơn"), null);

                var rSignLinked = GetRSignLinkedByDocNo(dat.docNo?.Trim(), "HazardNotify");
                var handlerStation = rSignLinked.data.Where(r => r.F_STATION_NAME.Contains("Người xử lý")).FirstOrDefault();

                using (var cnn = HIDbHelper.Instance.GetDBCnn())
                {
                    SqlTransaction trs = null;
                    try
                    {

                        cnn.Open();
                        trs = cnn.BeginTransaction();
                        UpdateRSignLinked(new HI_R_SIGN_LINKED
                        {
                            F_SIGN_EMP = dat.inchargeEmpNo?.Trim()?.ToUpper(),
                            F_SIGN_NAME = dat.inchargeEmpName?.Trim(),
                            F_SIGN_MAIL = dat.inchargeEmpMail?.Trim(),
                            F_UPDATE_EMP = acc?.F_EMPNO,
                            F_ROW_ID = handlerStation?.F_ROW_ID,

                        }, @"
                        F_SIGN_EMP	 = @F_SIGN_EMP ,
                        F_SIGN_NAME	 = @F_SIGN_NAME ,
                        F_SIGN_MAIL  = @F_SIGN_MAIL ,
                        F_UPDATE_EMP = @F_UPDATE_EMP ,
                        F_UPDATE_TIME = GETDATE()
                    ", trs);


                        UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                        {
                            F_INCHARGE_EMPNO = dat.inchargeEmpNo?.Trim()?.ToUpper(),
                            F_INCHARGE_EMPNAME = dat.inchargeEmpName?.Trim(),
                            F_INCHARGE_EMPDEPT = dat.inchargeEmpDept?.Trim(),
                            F_INCHARGE_EMPMAIL = dat.inchargeEmpMail?.Trim(),
                            F_UPDATE_EMP = acc?.F_EMPNO,
                            F_DOCNO = dat.docNo

                        }, @"
                        F_INCHARGE_EMPNO = @F_INCHARGE_EMPNO ,
                        F_INCHARGE_EMPNAME	 = @F_INCHARGE_EMPNAME,
                        F_INCHARGE_EMPDEPT  = @F_INCHARGE_EMPDEPT,
                        F_INCHARGE_EMPMAIL =@F_INCHARGE_EMPMAIL ,
                        F_UPDATE_EMP = @F_UPDATE_EMP,
                        F_UPDATE_TIME = GETDATE()
                    ", trs);

                        trs.Commit();
                        return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString());
                    }
                    catch (Exception ex)
                    {
                        try
                        {

                            if (trs != null)
                                trs.Rollback();
                        }
                        catch
                        {

                        }

                        HILogging.Instance.SaveLog($"[ERR-202507161900] exception detail:[{ex.Message + ex.StackTrace}]");
                        return HIReturnMessage.HIReturnError<object>(ex.Message);

                    }


                }





            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202507161906] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message);

            }
        }


        /// <summary>
        ///  luu ghi chu cua nguoi xu ly
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> SaveRemarkOfHandler(SaveRemarkOfHandlerReq dat)
        {
            try
            {

                string docNo = dat?.docNo;
                string inchargeRemark = dat?.inchargeRemark;

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                if (docNo.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"), null);
                if (inchargeRemark.isNullStr())
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không được bỏ trống ghi chú"), null);
                var hazardInfor = GetRHaZardNotify(docNo?.Trim());
                if (hazardInfor == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có thông tin trên hệ thống của đầu đơn ") + docNo, null);
                if (hazardInfor.F_INCHARGE_EMPNO != acc?.F_EMPNO)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không có quyền"), null);


                var rs = UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                {
                    F_INCHARGE_EMPREMARK = inchargeRemark?.Trim(),
                    F_DOCNO = docNo?.Trim(),
                    F_UPDATE_EMP = acc?.F_EMPNO,
                },
                      @"
                    F_INCHARGE_EMPREMARK = @F_INCHARGE_EMPREMARK ,
                    F_UPDATE_EMP = @F_UPDATE_EMP ,
                    F_UPDATE_TIME  = GETDATE()
                      ");
                if (rs)
                {
                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật ghi chú của người xử lý cho đầu đơn {docNo?.Trim()}, với dữ liệu mới [{JsonConvert.SerializeObject(dat)}]");
                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                }

                else
                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405130224] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
        }


        /// <summary>
        /// Update some inofr 1 of hazardNotify
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<object> SaveSomeHazardInfor1(SaveSomeHazardInfor1Req dat)
        {
            try
            {

                if (dat == null
                    || dat.docNo.isNullStr()
                     || dat.dangerType.isNullStr()
                      || dat.dangerLevel.isNullStr()
                       || dat.priorityLevel.isNullStr()
                        || dat.docType.isNullStr()
                         || dat.checkTime.isNullStr()
                             || dat.improvementDay.isNullStr()
                            || dat.hazardAiContent.isNullStr()
                    //  || dat.improvecountermeasures.isNullStr()


                    )
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Vui lòng điền vào các trường bắt buộc"), null);
                }

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();

                var hazardInfor = GetRHaZardNotify(dat.docNo);
                if (hazardInfor == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Không có thông tin trên hệ thống của đầu đơn ") + dat.docNo, null);
                if (hazardInfor.F_CHECK_EMPNO != acc?.F_EMPNO)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Bạn không có quyền"), null);

                if (DateTime.ParseExact(dat.improvementDay?.Trim().Replace("-", "/"), "yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"))
                  <= DateTime.ParseExact(dat.checkTime?.Trim().Replace("-", "/"), "yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"))
                  )
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Thời gian cải thiện không được nhỏ hơn thời gian phát hiện mối nguy"), null);
                }


                if (dat.docType == "Y")  // Phần kiểm tra này có thuộc dự án 
                {
                    if (dat.projectNames == null || dat.projectNames.Count <= 0)
                        return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Vui lòng chọn tên dự án"), null);

                }
                else
                {
                    dat.projectNames = null;
                }

                var rs = UpdateRHazardNotify(new HI_R_HAZARD_NOTIFY
                {
                    F_DOCNO = dat.docNo,
                    F_DANGER_TYPE = dat.dangerType,
                    F_DANGER_LEVEL = dat.dangerLevel,
                    F_PRIORITY_LEVEL = dat.priorityLevel,
                    F_DOC_TYPE = dat.docType,
                    F_CHECKTIME = dat.checkTime,
                    F_IMPROVEMENT_DAY = dat.improvementDay,
                    F_UPDATE_EMP = acc?.F_EMPNO,
                    F_HAZARD_AI_CONTENT = dat.hazardAiContent?.Trim(),
                    F_HAZARD_BASIC_STANDARD = dat.hazardBasisStandard?.Trim(),
                    F_IMPROVE_COUNTER_MEASURE = dat.improvecountermeasures?.Trim(),
                    F_PROJECT_NAMES = dat.projectNames == null ? "" : string.Join(";", dat.projectNames),
                },
                    @"
                    F_DANGER_TYPE  = @F_DANGER_TYPE ,
                    F_DANGER_LEVEL = @F_DANGER_LEVEL ,
                    F_PRIORITY_LEVEL  =@F_PRIORITY_LEVEL ,
                    F_DOC_TYPE = @F_DOC_TYPE,
                    F_CHECKTIME   = @F_CHECKTIME,
                    F_IMPROVEMENT_DAY = @F_IMPROVEMENT_DAY ,
                    F_UPDATE_EMP = @F_UPDATE_EMP ,
                    F_UPDATE_TIME = GETDATE() ,
                    F_HAZARD_AI_CONTENT = @F_HAZARD_AI_CONTENT ,
                    F_HAZARD_BASIC_STANDARD  = @F_HAZARD_BASIC_STANDARD  ,
                    F_IMPROVE_COUNTER_MEASURE  = @F_IMPROVE_COUNTER_MEASURE ,
                    F_PROJECT_NAMES  = @F_PROJECT_NAMES
                      ");
                if (rs)
                {
                    dat.hazardAiContent = "";
                    HILogging.Instance.SaveLog($"Bạn {acc?.F_EMPNO} đã cập nhật thông tin SaveSomeHazardInfor1 của đầu đơn {dat.docNo}, với dữ liệu mới là [{JsonConvert.SerializeObject(dat)}]");

                    return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                }

                else
                    return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);

            }
            catch (Exception ex)
            {

                HILogging.Instance.SaveLog($"[ERR-20250405130225] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }
        }





        /// <summary>
        /// get danh sach don hazardnotify
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public HIReturnMessageModel<ElTableRes> GetHazardNotifyAppLs(
                                HazardNotifyAppLsReq dat, bool isExcel = false
            )
        {
            try
            {
                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                string columns = $@" 
                            AA.F_DOCNO	,
                            AA.F_DOC_STATUS ,
                            AA.F_FACTORY ,
                            AA.F_UNIT ,
                            AA.F_FACTORY_BUILDING ,
                            AA.F_FLOOR ,
                            AA.F_CHECK_EMPNO	 ,
	                        AA.F_CHECK_EMPNAME ,
	                        AA.F_DOC_TYPE  ,
	                        AA.F_PRIORITY_LEVEL ,
	                        AA.F_CREATE_EMP ,
	                        FORMAT( AA.F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_CREATE_TIME ,
                            AA.F_CHECK_EMPDEPT  ,
                            AA.F_CHECK_EMPMAIL  ,
                            FORMAT( AA.F_CHECKTIME  , 'yyyy/MM/dd HH:mm:ss') AS  F_CHECKTIME ,
                            AA.F_DANGER_TYPE,
                            AA.F_DANGER_LEVEL ,
                             FORMAT( AA.F_IMPROVEMENT_DAY  , 'yyyy/MM/dd HH:mm:ss') AS F_IMPROVEMENT_DAY, 
                            AA.F_INCHARGE_EMPNO  ,
                            AA.F_INCHARGE_EMPNAME  ,
                            AA.F_INCHARGE_EMPMAIL ,
                            AA.F_INCHARGE_EMPDEPT  ,
                            AA.F_PIC_BOSS_EMPNO  ,
                            AA.F_PIC_BOSS_NAME  ,
                            AA.F_PIC_BOSS_MAIL ,
                            AA.F_UPDATE_EMP  ,
                            FORMAT( AA.F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss') AS F_UPDATE_TIME  , 
                            AA.F_APPLY_NO  ,
                            AA.F_PROJECT_NAMES ,
                            AA.F_HANDLE_STATUS  ,
                            CC1.F_SIGN_EMP,
                            CC1.F_SIGN_NAME,
                            CC1.F_SIGN_MAIL ,
                            AA.F_CHECK_REMARK,
                            AA.F_INCHARGE_EMPREMARK, 
                            AA.F_POSITION_DETAIL 
                            {(isExcel ? ", AA.F_HAZARD_AI_CONTENT  " : "")} 
 
                                 ";


                string sql = @"
                        SELECT 
                      DISTINCT
                        {0}
                        FROM 
                        R_HAZARD_NOTIFY AA
                        LEFT JOIN R_APPLY_ORDER BB1 ON AA.F_APPLY_NO = BB1.F_APPLY_NO
                        LEFT JOIN R_SIGN_LINKED CC1 ON ( BB1.F_APPLY_NO = CC1.F_APPLY_NO AND BB1.F_SIGN_STATION_NO = CC1.F_ROW_ID )
                        WHERE  1=1  
                     " +
                     $@" {(dat.factory.isNullStr() ? " AND 1=1 " : $" AND AA.F_FACTORY =  N'{dat.factory?.Trim()}' ")} 
                         {(dat.unit.isNullStr() ? " AND 1=1 " : $" AND AA.F_UNIT = N'{dat.unit?.Trim()}' ")} 
                         {(dat.factoryBuilding.isNullStr() ? " AND 1=1 " : $" AND AA.F_FACTORY_BUILDING = N'{dat.factoryBuilding?.Trim()}' ")} 
                          {(dat.floor.isNullStr() ? " AND 1=1 " : $" AND AA.F_FLOOR = N'{dat.floor?.Trim()}' ")} 
                          {(dat.docNo.isNullStr() ? " AND 1=1 " : $" AND AA.F_DOCNO LIKE N'%{dat.docNo?.Trim()}%' ")} 
                        {(dat.docStatus.isNullStr() ? " AND 1=1 " : $" AND AA.F_DOC_STATUS LIKE N'%{dat.docStatus?.Trim()}%' ")} 
                        {(dat.checkEmpNo.isNullStr() ? " AND 1=1 " : $" AND AA.F_CHECK_EMPNO LIKE N'%{dat.checkEmpNo?.Trim()}%' ")} 
                        {(dat.checkEmpName.isNullStr() ? " AND 1=1 " : $" AND AA.F_CHECK_EMPNAME LIKE N'%{dat.checkEmpName?.Trim()}%' ")} 
                        {((dat.startTime.isNullStr() == false && dat.endTime.isNullStr() == false) ? $" AND (  CAST(AA.F_CHECKTIME AS DATE)  BETWEEN    CAST('{dat.startTime?.Trim()}' AS DATE)   AND    CAST('{dat.endTime?.Trim()}' AS DATE)     )  " : " AND 1=1 ")  }
                         "
                      ;


                if (dat.searchType.isNullStr() || dat.searchType == "All")
                {

                }
                else if (dat.searchType == "WaitingYouSign")
                {
                    sql += $@"
                            AND AA.F_APPLY_NO IN (
	                            select  BB.F_APPLY_NO  from 
	                            R_APPLY_ORDER BB
	                            WHERE 
	                            BB.F_STATUS NOT IN (  N'Removed/Đã hủy' ,N'Closed/Đã đóng' )
	                            AND  BB.F_SIGN_EMP ='{acc?.F_EMPNO}'
	                            )

                            ";
                }
                else if (dat.searchType == "YourApp")
                {
                    sql += $@"
                            AND AA.F_APPLY_NO IN (
	                            select DISTINCT BB.F_APPLY_NO  from 
	                            R_SIGN_LINKED BB
	                            WHERE 
                                  BB.F_SIGN_EMP ='{acc?.F_EMPNO}'
	                            )
                            ";
                }

                dat.page = dat.page <= 0 ? 1 : dat.page;
                dat.pageSize = dat.pageSize <= 0 ? 5 : dat.pageSize;
                int star_rownum = (dat.page * dat.pageSize) - dat.pageSize + 1;
                int end_rownum = dat.page * dat.pageSize;

                string sql1 = $@"
                
                    SELECT 
                                             F_DOCNO	,
                                              F_DOC_STATUS ,
                                              F_FACTORY ,
                                              F_UNIT ,
                                              F_FACTORY_BUILDING ,
                                              F_FLOOR ,
                                            F_CHECK_EMPNO	 ,
	                                        F_CHECK_EMPNAME ,
	                                        F_DOC_TYPE  ,
	                                        F_PRIORITY_LEVEL ,
	                                        F_CREATE_EMP ,
	                                        F_CREATE_TIME ,
                                            F_CHECK_EMPDEPT  ,
                                            F_CHECK_EMPMAIL  ,
                                            F_CHECKTIME ,
                                            F_DANGER_TYPE,
                                            F_DANGER_LEVEL ,
                                            F_IMPROVEMENT_DAY, 
                                            F_INCHARGE_EMPNO  ,
                                            F_INCHARGE_EMPNAME  ,
                                            F_INCHARGE_EMPMAIL ,
                                            F_INCHARGE_EMPDEPT  ,
                                            F_PIC_BOSS_EMPNO  ,
                                            F_PIC_BOSS_NAME  ,
                                            F_PIC_BOSS_MAIL ,
                                            F_UPDATE_EMP  ,
                                             F_UPDATE_TIME  , 
                                            F_APPLY_NO  ,
                                            F_PROJECT_NAMES ,
                                            F_HANDLE_STATUS  ,
                                            F_SIGN_EMP,
                                            F_SIGN_NAME,
                                            F_SIGN_MAIL ,
                                            F_CHECK_REMARK,
                                            F_INCHARGE_EMPREMARK, 
                                            F_POSITION_DETAIL 
                                            {(isExcel ? ", F_HAZARD_AI_CONTENT  " : "")} 
                                    FROM (SELECT ROW_NUMBER() OVER (ORDER BY F_DOCNO DESC ) AS ROW_NUMBER, K.*
                                          FROM (
						                      {string.Format(sql, columns)}
						                  )  K ) E
                                    WHERE   {(isExcel == false ? $" E.ROW_NUMBER BETWEEN {star_rownum} AND {end_rownum} " : " 1=1 ")}     
                
               
              ";
                int totalCount = 0;
                var tempCount = HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault(string.Format(sql, " COUNT(1) AS CC"));
                if (tempCount != null)
                {

                    totalCount = (int)tempCount.CC;
                }

                var items = HIDbHelper.Instance.GetDBCnn().Query<HI_R_HAZARD_NOTIFY>(
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

                HILogging.Instance.SaveLog($"[ERR-2025040513051] exception detail:[{ex.Message + ex.StackTrace}]");


                return HIReturnMessage.HIReturnError<ElTableRes>(ex.Message, null);
            }


        }



        /// <summary>
        /// Get total count HIapp waiting you sign
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<int?> CountHIAppWaitingYouSign()
        {
            try
            {

                var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                var count = HIDbHelper.Instance.GetDBCnn().Query<HI_R_APPLY_ORDER>(@"
                SELECT * FROM R_APPLY_ORDER
                WHERE 
                F_SIGN_EMP = @F_SIGN_EMP
                AND F_APPLY_NO IN (
                select F_APPLY_NO from 
                R_HAZARD_NOTIFY WHERE F_DOC_STATUS NOT IN (N'Closed/Đã đóng', N'Removed/Đã hủy')
                )
                ", new { F_SIGN_EMP = acc?.F_EMPNO })?.Count();
                return HIReturnMessage.HIReturnSuccess<int?>(HIStatusType.success.ToString(), count);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-2025040513052] exception detail:[{ex.Message + ex.StackTrace}]");


                return HIReturnMessage.HIReturnError<int?>(ex.Message, null);
            }



        }


        /// <summary>
        /// Get total count app waiting you sign
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<int?> GetToTalCountAppWaitingSign()
        {
            try
            {

                int? totalCount = 0;
                var rs1 = CountHIAppWaitingYouSign();
                if (rs1.status == HIStatusType.error.ToString())
                    return HIReturnMessage.HIReturnError<int?>(rs1.message, null);
                totalCount += rs1.data;

                return HIReturnMessage.HIReturnSuccess<int?>(HIStatusType.success.ToString(), totalCount);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-2025040513052] exception detail:[{ex.Message + ex.StackTrace}]");


                return HIReturnMessage.HIReturnError<int?>(ex.Message, null);
            }
        }


        /// <summary>
        /// Get some indexes of notify app
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<object> GetIndexesNotifyApp()
        {
            try
            {

                int totalCountApp = HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault<int>(
                @"
            SELECT COUNT(1) AS CC
              FROM [R_HAZARD_NOTIFY] WHERE F_DOC_STATUS != N'Removed/Đã hủy'
            ");
                int totalCountNotFinish = HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault<int>(
                 @"
            SELECT COUNT(1) AS CC
              FROM [R_HAZARD_NOTIFY] WHERE F_DOC_STATUS NOT IN ( N'Removed/Đã hủy' ,N'Closed/Đã đóng' )
            ");
                int totalCountFinish = totalCountApp - totalCountNotFinish;
                var percentFinish = Math.Round((float)(totalCountFinish * 100.0) / (float)totalCountApp, 2);
                return HIReturnMessage.HIReturnSuccess<object>("", new
                {
                    totalCountApp,
                    totalCountNotFinish,
                    totalCountFinish,
                    percentFinish
                });
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202511041649] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<object>(ex.Message, null);

            }

        }


    }
}