using Dapper;
using HazardIdentifySystemApi.Commons;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using HazardIdentifySystemApi.Models;
using HazardIdentifySystemApi.Models.HazardIdentifySystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace HazardIdentifySystemApi.Businesses.HazardIdentifySystem
{
    public class HIConfigBusiness
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIConfigBusiness() { }
        private static HIConfigBusiness _instance;
        public static HIConfigBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIConfigBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion


        public string GeneralDocNo(string prefix)
        {


            return HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault(@"
                               SELECT  @prefix +  replace(replace(replace(convert(varchar, getdate(), 120),':',''),'-',''),' ','')
			                            + CAST(NEXT VALUE FOR SEQ_TOTAL AS NVARCHAR(900)) AS DOC_NO
                                    ", new { prefix = prefix })?.DOC_NO;
        }



        public string GetNewId()
        {
            return HIDbHelper.Instance.GetDBCnn().QueryFirstOrDefault(@"
                                select dbo.GET_NEW_ROWID(NEWID()) as NEW_ID
                                    ")?.NEW_ID;
        }

        public List<V_HI_C_NODE_VALUE> GetCNodeValueLSOfFatherNode(string fatherNodeId)
        {
            return HIDbHelper.Instance.GetDBCnn().Query<V_HI_C_NODE_VALUE>(
                      @"
                            SELECT 
                                                        F_ROW_ID 
                                                      , F_NODE_NAME 
                                                      , F_NODE_VALUE 
                                                      , F_NODE_DESC 
                                                      , F_FATHER_NODE  
                                                      , F_SORT 
                                                      , F_DATA1 
                                                      , F_DATA2 
                                                      , F_DATA3 
                                                      ,F_CREATE_EMP
                                                      ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_CREATE_TIME
                                                      , F_UPDATE_EMP 
                                                      , FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_UPDATE_TIME
                                                     ,F_ALLOW_DELETE
                             
                            FROM C_NODE_VALUE  WHERE  F_FATHER_NODE =   @F_ROW_ID
                            ORDER BY F_SORT ASC

                            ", new { F_ROW_ID = fatherNodeId }).ToList();

        }


        public void GetAllChidrenNodeValueLS(List<V_HI_C_NODE_VALUE> rootLs)
        {
            rootLs.ForEach(item =>
            {
                item.children = GetCNodeValueLSOfFatherNode(item.F_ROW_ID);
                if (item.children != null && item.children.Count > 0)
                {
                    GetAllChidrenNodeValueLS(item.children);
                }
            });
        }

        public List<V_HI_C_NODE_VALUE> GetAllChidrenOfAparent(string fatherNodeID)
        {
            var ls = GetCNodeValueLSOfFatherNode(fatherNodeID);
            var finalLs = ls.ToList();
            if (ls != null && ls.Count > 0)
            {
                ls.ForEach(item =>
                {
                    finalLs.AddRange(GetAllChidrenOfAparent(item.F_ROW_ID));
                });
            }
            return finalLs;
        }

        public HIReturnMessageModel<ElTableRes> GetCNodeValueLS(string search, int page, int pageSize)
        {
            try
            {


                search = string.IsNullOrWhiteSpace(search) ? "" : search.Trim().ToLower();
                string columns = @"
                            F_ROW_ID 
                          , F_NODE_NAME 
                          , F_NODE_VALUE 
                          , F_NODE_DESC 
                          , F_FATHER_NODE 
                          , F_SORT 
                          , F_DATA1 
                          , F_DATA2 
                          , F_DATA3 
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_CREATE_TIME
                          , F_UPDATE_EMP 
                          , FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_UPDATE_TIME
                          , F_ALLOW_DELETE
                       ";


                string sql = @"
                        SELECT 
                        {0}
                        FROM 
                        C_NODE_VALUE
                        WHERE  (F_FATHER_NODE IS NULL OR F_FATHER_NODE ='') " +
                        $" AND ( {(string.IsNullOrWhiteSpace(search) ? " 1=1 " : " LOWER( F_NODE_NAME ) LIKE N'%" + search + "%' ")}   )"
                      ;



                int star_rownum = (page * pageSize) - pageSize + 1;
                int end_rownum = page * pageSize;

                string sql1 = $@"
                   SELECT 
                            F_ROW_ID 
                          , F_NODE_NAME 
                          , F_NODE_VALUE 
                          , F_NODE_DESC 
                          , F_FATHER_NODE 
                          , F_SORT 
                          , F_DATA1 
                          , F_DATA2 
                          , F_DATA3 
                          ,F_CREATE_EMP
                          , F_CREATE_TIME
                          , F_UPDATE_EMP 
                          ,F_UPDATE_TIME
                          ,F_ALLOW_DELETE

                    FROM (SELECT ROW_NUMBER() OVER (ORDER BY F_ROW_ID ASC ) AS ROW_NUMBER, K.*
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

                var items = HIDbHelper.Instance.GetDBCnn().Query<V_HI_C_NODE_VALUE>(
                  sql1
                 , new
                 {
                     SEARCH = search
                 }).ToList();
                if (items.Count > 0)
                {
                    GetAllChidrenNodeValueLS(items);
                }


                return HIReturnMessage.HIReturnSuccess<ElTableRes>(HIStatusType.success.ToString(), new ElTableRes()
                {
                    items = items,
                    totalCount = totalCount
                });
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112221] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<ElTableRes>(ex.Message);
            }


        }




        public HI_C_NODE_VALUE GetOneCNodeValue(object paramss, string where)
        {
            return HIDbHelper.Instance.GetDBCnn()
          .QueryFirstOrDefault<HI_C_NODE_VALUE>(
          $@"  SELECT 
                            F_ROW_ID 
                          , F_NODE_NAME 
                          , F_NODE_VALUE 
                          , F_NODE_DESC 
                          , F_FATHER_NODE 
                          , F_SORT 
                          , F_DATA1 
                          , F_DATA2 
                          , F_DATA3 
                          ,F_CREATE_EMP
                          ,FORMAT( F_CREATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_CREATE_TIME
                          , F_UPDATE_EMP 
                          , FORMAT( F_UPDATE_TIME  , 'yyyy/MM/dd HH:mm:ss') as F_UPDATE_TIME
                          , F_ALLOW_DELETE
                  FROM  C_NODE_VALUE WHERE  1=1  {  (where.isNullStr() ? "" : $" AND ( { where } ) ") }
                ", paramss);

        }




        public bool DeleteCNodeValue(string rowId, SqlTransaction trs)
        {
            return trs.Connection.Execute(
          @"
              DELETE FROM C_NODE_VALUE WHERE  F_ROW_ID = @F_ROW_ID 
            ", new { F_ROW_ID = rowId }, trs) > 0 ? true : false;
        }

        public bool AddNewCNodeValue(HI_C_NODE_VALUE dat)
        {
            return HIDbHelper.Instance.GetDBCnn().Execute(
               @"
                INSERT INTO C_NODE_VALUE
                           ([F_ROW_ID]
                           ,[F_NODE_NAME]
                           ,[F_NODE_VALUE]
                           ,[F_NODE_DESC]
                           ,[F_FATHER_NODE]
                           ,[F_SORT]
                           ,[F_DATA1]
                           ,[F_DATA2]
                           ,[F_DATA3]
                           ,[F_CREATE_EMP]
                           ,[F_CREATE_TIME]
                           ,[F_UPDATE_EMP]
                           ,[F_UPDATE_TIME])
                     VALUES
                           ( dbo.GET_NEW_ROWID(NEWID())
                           ,@F_NODE_NAME
                           ,@F_NODE_VALUE
                           ,@F_NODE_DESC
                           ,@F_FATHER_NODE
                           ,@F_SORT
                           ,@F_DATA1
                           ,@F_DATA2
                           ,@F_DATA3
                           ,@F_CREATE_EMP
                           ,GETDATE()
                           ,@F_CREATE_EMP
                           ,GETDATE()
		                   )
                ", dat) > 0 ? true : false;


        }


        public bool UpdateCNodeValue(HI_C_NODE_VALUE dat)
        {
            var rs = HIDbHelper.Instance.GetDBCnn()
                .Execute(
               @"
              UPDATE C_NODE_VALUE
              SET F_NODE_NAME= @F_NODE_NAME ,
                  F_NODE_VALUE = @F_NODE_VALUE,
	              F_NODE_DESC = @F_NODE_DESC ,
	              F_SORT = @F_SORT  ,
	              F_UPDATE_EMP= @F_UPDATE_EMP,
	              F_UPDATE_TIME =GETDATE()
              WHERE  F_ROW_ID = @F_ROW_ID

            ", new
               {
                   F_NODE_NAME = dat.F_NODE_NAME,
                   F_NODE_VALUE = dat.F_NODE_VALUE,
                   F_NODE_DESC = dat.F_NODE_DESC,
                   F_SORT = dat.F_SORT,
                   F_UPDATE_EMP = dat.F_UPDATE_EMP,
                   F_ROW_ID = dat.F_ROW_ID,
               });

            return rs > 0 ? true : false;

        }


        public HIReturnMessageModel<object> ModifyCNodeValue(ModifyCNodeValueReq dat)
        {
            // 1. them nut cha level cao nhat.
            //2. Them nut con
            //3.sua nut hien tai
            //4. xoa nut hien tai
            var acc = HIAccountBusiness.Instance.GetAccountFromSession();


            if (dat.modifyType == EnumModifyType.Delete.ToString())
            {  //xoa
                if (string.IsNullOrWhiteSpace(dat?.curNodeId))
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ"));
                var oldDat = GetOneCNodeValue(new { F_ROW_ID = dat.curNodeId }, "  F_ROW_ID = @F_ROW_ID ");
                if (oldDat == null)
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Nút hiện tại không tồn tại"));
                if (oldDat.F_ALLOW_DELETE == "N") // ko cho phep xoa-> cac nut config cua he thong
                {
                    return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Nút hiện tại không cho phép xóa"));
                }

                var allChidrenLs = GetAllChidrenOfAparent(dat.curNodeId);
                using (var sqlConn = HIDbHelper.Instance.GetDBCnn())
                {
                    SqlTransaction trs = null;
                    try
                    {
                        sqlConn.Open();
                        trs = sqlConn.BeginTransaction();
                        var rs = DeleteCNodeValue(dat.curNodeId, trs);
                        if (allChidrenLs != null && allChidrenLs.Count > 0)
                        {
                            allChidrenLs.ForEach(r =>
                            {
                                DeleteCNodeValue(r.F_ROW_ID, trs);
                            });
                        }
                        trs.Commit();
                        if (rs)
                        {
                            HILogging.Instance.SaveLog($@"Bạn {acc?.F_EMPNO} đã xoá dữ liệu cơ bản :cha: [{JsonConvert.SerializeObject(oldDat)}], con:[{JsonConvert.SerializeObject(allChidrenLs)}]");
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

                        HILogging.Instance.SaveLog($"[ERR-20250405112256] exception detail:[{ex1.Message + ex1.StackTrace}]");

                        return HIReturnMessage.HIReturnError<object>(ex1.Message);
                    }


                }


            }
            else  // them /sua
            {
                if (dat == null
                || dat.curNodeName.isNullStr()
                || dat.curNodeValue.isNullStr()
                || dat.curNodeDesc.isNullStr()
                ) return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Dữ liệu gửi lên không hợp lệ, vui lòng điền vào các trường bắt buộc"), null);


                if (dat.modifyType == EnumModifyType.Add.ToString())
                {
                    if (string.IsNullOrWhiteSpace(dat.fatherNodeId))
                    {  // 1. them nut cha level cao nhat.
                        var oldDat = GetOneCNodeValue(new { F_NODE_NAME = dat.curNodeName?.Trim() }, " F_NODE_NAME =@F_NODE_NAME AND  ( F_FATHER_NODE IS NULL OR F_FATHER_NODE='' ) ");
                        if (oldDat != null)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Đã có dữ liệu giống tồn tại trên hệ thống, vui lòng cài đặt giá trị khác"), null);
                        var rs = AddNewCNodeValue(new HI_C_NODE_VALUE
                        {
                            F_NODE_NAME = dat.curNodeName?.Trim(),
                            F_NODE_VALUE = dat.curNodeValue?.Trim(),
                            F_NODE_DESC = dat.curNodeDesc?.Trim(),
                            F_SORT = dat.curNodeSort,
                            F_CREATE_EMP = acc?.F_EMPNO,
                        });
                        if (rs)
                        {
                            HILogging.Instance.SaveLog($@"Bạn {acc?.F_EMPNO} đã thêm dữ liệu cơ bản (nút cha cao nhất) [{JsonConvert.SerializeObject(dat)}]");
                            return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                        }

                        else
                            return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);

                    }
                    else if (!string.IsNullOrWhiteSpace(dat.fatherNodeId) && string.IsNullOrWhiteSpace(dat.curNodeId))
                    {
                        //2. Them nut con
                        var parentDat = GetOneCNodeValue(new { F_ROW_ID = dat.fatherNodeId }, " F_ROW_ID = @F_ROW_ID  ");
                        if (parentDat == null)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Nút cha đã bị xóa trước đó"), null);
                        var othersameDat = GetOneCNodeValue(new { F_NODE_NAME = dat.curNodeName?.Trim(), F_FATHER_NODE = dat?.fatherNodeId }, "  F_NODE_NAME = @F_NODE_NAME AND F_FATHER_NODE= @F_FATHER_NODE ");
                        if (othersameDat != null)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Giá trị tương tự đã tồn tại, vui lòng nhập giá trị khác"), null);
                        var rs = AddNewCNodeValue(new HI_C_NODE_VALUE
                        {
                            F_NODE_NAME = dat.curNodeName?.Trim(),
                            F_NODE_VALUE = dat.curNodeValue?.Trim(),
                            F_NODE_DESC = dat.curNodeDesc?.Trim(),
                            F_SORT = dat.curNodeSort,
                            F_FATHER_NODE = dat.fatherNodeId,
                            F_CREATE_EMP = acc?.F_EMPNO,
                        });
                        if (rs)
                        {
                            HILogging.Instance.SaveLog($@"Bạn {acc?.F_EMPNO} đã thêm dữ liệu cơ bản (nút con) [{JsonConvert.SerializeObject(dat)}]");
                            return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);
                        }

                        else
                            return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);
                    }
                }

                if (dat.modifyType == EnumModifyType.Edit.ToString())
                    if (!string.IsNullOrWhiteSpace(dat.curNodeId))
                    {   //3.sua nut hien tai
                        var curDat = GetOneCNodeValue(new { F_ROW_ID = dat.curNodeId }, "  F_ROW_ID  = @F_ROW_ID ");
                        if (curDat == null)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Nút hiện tại không tồn tại"), null);

                        if (curDat.F_ALLOW_DELETE == "N") // ko cho phep xoa-> cac nut config cua he thong
                        {
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Nút hiện tại không cho phép sửa"), null);
                        }

                        var otherSameDat = GetOneCNodeValue(new { F_NODE_NAME = dat.curNodeName?.Trim(), F_FATHER_NODE = curDat.F_FATHER_NODE, F_ROW_ID = dat.curNodeId }, " F_NODE_NAME = @F_NODE_NAME  AND  ( @F_FATHER_NODE  IS NULL OR @F_FATHER_NODE ='' OR  F_FATHER_NODE= @F_FATHER_NODE  )       AND F_ROW_ID != @F_ROW_ID ");
                        if (otherSameDat != null)
                            return HIReturnMessage.HIReturnError<object>(LangHelper.Instance.Get("Giá trị tương tự đã tồn tại, vui lòng nhập giá trị khác"), null);
                        var rs = UpdateCNodeValue(new HI_C_NODE_VALUE
                        {
                            F_NODE_NAME = dat.curNodeName?.Trim(),
                            F_NODE_VALUE = dat.curNodeValue?.Trim(),
                            F_NODE_DESC = dat.curNodeDesc?.Trim(),
                            F_SORT = dat.curNodeSort,
                            F_ROW_ID = dat.curNodeId,
                            F_UPDATE_EMP = acc?.F_EMPNO

                        });
                        if (rs)
                        {
                            HILogging.Instance.SaveLog($@"Bạn {acc?.F_EMPNO} đã cập nhật dữ liệu cơ bản (nút con), dữ liệu mới: [{JsonConvert.SerializeObject(dat)}]");
                            return HIReturnMessage.HIReturnSuccess<object>(HIStatusType.success.ToString(), null);

                        }

                        else
                            return HIReturnMessage.HIReturnError<object>(HIStatusType.error.ToString(), null);

                    }
            }
            return null;

        }

        public HIReturnMessageModel<HI_C_NODE_VALUE> GetOneCNodeValueByRowID(string rowId)
        {
            try
            {
                var dat = GetOneCNodeValue(new { F_ROW_ID = rowId?.Trim() }, " F_ROW_ID  = @F_ROW_ID ");
                return HIReturnMessage.HIReturnSuccess<HI_C_NODE_VALUE>(HIStatusType.success.ToString(), dat);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112321] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<HI_C_NODE_VALUE>(ex.Message, null);

            }

        }


        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetSomeDropDownConfigByNodeName(string nodeName)
        {
            try
            {
                var fatherNode = GetOneCNodeValue(null, $" F_NODE_NAME= N'{nodeName}' AND ( F_FATHER_NODE IS NULL OR F_FATHER_NODE='' )  ");
                var childLs = GetCNodeValueLSOfFatherNode(fatherNode?.F_ROW_ID);
                return HIReturnMessage.HIReturnSuccess(HIStatusType.success.ToString(), childLs);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112336] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<List<V_HI_C_NODE_VALUE>>(ex.Message, null);

            }
        }


        /// <summary>
        /// Lay danh sach don vi
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetUnitLs()
        {
            return GetSomeDropDownConfigByNodeName("Đơn vị");
        }


        /// <summary>
        /// Lay danh sach toa xuong
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetFactoryBuildingLs()
        {
            return GetSomeDropDownConfigByNodeName("Tòa xưởng");
        }


        /// <summary>
        /// Lay danh sach tang
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetFloorLs()
        {
            return GetSomeDropDownConfigByNodeName("Tầng");
        }


        /// <summary>
        /// Lay danh sach loai hinh moi nguy
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetTypeOfHazardLs()
        {
            return GetSomeDropDownConfigByNodeName("Loại hình mối nguy");
        }


        /// <summary>
        /// Lay danh sach loai hinh moi nguy
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetDangerLevelLs()
        {
            return GetSomeDropDownConfigByNodeName("Mức độ nguy hiểm");
        }


        /// <summary>
        /// Lay danh sach nha xuong
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetFactoryLs()
        {
            return GetSomeDropDownConfigByNodeName("Nhà xưởng");
        }
        /// <summary>
        /// Lay danh sach tinh trang xu ly
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetHandleStatusLs()
        {
            return GetSomeDropDownConfigByNodeName("Tình trạng xử lý");
        }

        /// <summary>
        /// Lay danh sach muc do uu tien
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetPriorityLevel()
        {
            return GetSomeDropDownConfigByNodeName("Mức độ ưu tiên");
        }



        /// <summary>
        /// Lay danh sach cac ten quyen
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetPermissionNameLs()
        {
            return GetSomeDropDownConfigByNodeName("USER_PERMISSION");
        }

        /// <summary>
        /// Lay ra cai dat infor gop y
        /// </summary>
        /// <returns></returns>
        public HIReturnMessageModel<List<V_HI_C_NODE_VALUE>> GetCommentConfigInfor()
        {
            return GetSomeDropDownConfigByNodeName("Thông tin góp ý");
        }




        public HIReturnMessageModel<string> GetSysDate()
        {
            try
            {
                var dat = HIDbHelper.Instance.GetDBCnn()
              .QueryFirstOrDefault(
              $@"  
               select FORMAT( GETDATE()  , 'yyyy/MM/dd HH:mm:ss') as SYSDATE
                ")?.SYSDATE;
                return HIReturnMessage.HIReturnSuccess<string>(HIStatusType.success.ToString(), dat);
            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-20250405112405] exception detail:[{ex.Message + ex.StackTrace}]");

                return HIReturnMessage.HIReturnError<string>(ex.Message, null);

            }


        }


        public HIReturnMessageModel<List<HI_C_PIC_ZONE>> GetFactoryInforLs(string search)
        {
            try
            {

                var dat = HIDbHelper.Instance.GetDBCnn().Query<HI_C_PIC_ZONE>(
                      $@"
                select 
               DISTINCT
                F_FACTORY ,
                F_UNIT  ,
                F_FACTORY_BUILDING ,
                F_FLOOR
                from
                C_PIC_ZONE 
                WHERE  (  F_FACTORY LIKE  @SEARCH  OR F_UNIT LIKE   @SEARCH  OR F_FACTORY_BUILDING LIKE   @SEARCH OR F_FLOOR LIKE   @SEARCH ) 
                ORDER BY F_FACTORY  , F_UNIT,F_FACTORY_BUILDING ,F_FLOOR
                ", new { SEARCH = $"%{search?.Trim()?.ToLower()}%" }).ToList();
                return HIReturnMessage.HIReturnSuccess(HIStatusType.success.ToString(), dat);

            }
            catch (Exception ex)
            {
                HILogging.Instance.SaveLog($"[ERR-202504141610] exception detail:[{ex.Message + ex.StackTrace}]");
                return HIReturnMessage.HIReturnError<List<HI_C_PIC_ZONE>>(ex.Message, null);

            }

        }

        /// <summary>
        /// Lay ra thong tin Vị trí phát hiện mối nguy   
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public HIReturnMessageModel<List<string>> GetPositionInfor(GetPositionInforReq dat)
        {
            try
            {

                string where = "";
                object paramss = null;

                if (dat == null)
                    return HIReturnMessage.HIReturnError<List<string>>(LangHelper.Instance.Get("Không hợp lệ"), null);

                switch (dat.typePosition)
                {
                    case "F_FACTORY": where = "  1=1 "; paramss = null; break;
                    case "F_UNIT": where = "  F_FACTORY = @F_FACTORY  "; paramss = new { F_FACTORY = dat.factory?.Trim() }; break;
                    case "F_FACTORY_BUILDING": where = " F_UNIT= @F_UNIT AND   F_FACTORY = @F_FACTORY  "; paramss = new { F_UNIT = dat.unit?.Trim(), F_FACTORY = dat.factory?.Trim() }; break;
                    case "F_FLOOR": where = " F_UNIT= @F_UNIT AND   F_FACTORY = @F_FACTORY AND F_FACTORY_BUILDING = @F_FACTORY_BUILDING "; paramss = new { F_UNIT = dat.unit?.Trim(), F_FACTORY = dat.factory?.Trim(), F_FACTORY_BUILDING = dat.factoryBuilding?.Trim() }; break;
                    default: return HIReturnMessage.HIReturnError<List<string>>(LangHelper.Instance.Get("typePosition không hợp lệ"), null);
                }

                string sql = $@"
                select 
                DISTINCT
                F_FACTORY ,
                F_UNIT  ,
                F_FACTORY_BUILDING ,
                F_FLOOR
                from
                C_PIC_ZONE 
                WHERE  { where }
                ";
                var data = HIDbHelper.Instance.GetDBCnn().Query<HI_C_PIC_ZONE>(
                    sql, paramss).ToList();
                var returnDat = new List<string>();
                switch (dat.typePosition)
                {
                    case "F_FACTORY": returnDat = data.Select(r => r.F_FACTORY).Distinct().OrderBy(r => r).ToList(); break;
                    case "F_UNIT": returnDat = data.Select(r => r.F_UNIT).Distinct().OrderBy(r => r).ToList(); break;
                    case "F_FACTORY_BUILDING": returnDat = data.Select(r => r.F_FACTORY_BUILDING).Distinct().OrderBy(r => r).ToList(); break;
                    case "F_FLOOR": returnDat = data.Select(r => r.F_FLOOR).Distinct().OrderBy(r => r).ToList(); break;

                }
                return HIReturnMessage.HIReturnSuccess<List<string>>(HIStatusType.success.ToString(), returnDat);

            }
            catch (Exception ex)
            {
                return HIReturnMessage.HIReturnError<List<string>>(ex.Message, null);

            }

        }



    }
}