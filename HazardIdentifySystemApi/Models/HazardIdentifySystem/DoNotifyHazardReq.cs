using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.HazardIdentifySystem
{
    public class NotifyHazardTotalData
    {
        /// <summary>
        /// list content do AI phan tich
        /// </summary>
        public List<Analysis> AnalysisLs { set; get; }
        /// <summary>
        /// // ma the ng kiem tra
        /// </summary>
        public string checkEmpNo { set; get; }
        /// <summary>
        ///  ho ten ng kiem tra
        /// </summary>
        public string checkEmpName { set; get; }
        /// <summary>
        /// // ten bo phan cua ng kiem tra
        /// </summary>
        public string checkEmpDept { set; get; }

        /// <summary>
        /// // mail  cua ng kiem tra
        /// </summary>
        public string checkEmpMail { set; get; }

        /// <summary>
        /// khu nha xuong 
        /// </summary>
        public string factory { set; get; }
        /// <summary>
        /// // don vi phat hien moi nguy
        /// </summary>
        public string unit { set; get; }
        /// <summary>
        /// // toa xuong phat hien moi nguy
        /// </summary>
        public string factoryBuilding { set; get; }
        /// <summary>
        /// // tang phat hien moi nguy
        /// </summary>
        public string floor { set; get; }
        /// <summary>
        /// thoi gian phat hien moi nguy : mac dinh sysdate
        /// </summary>
        public string checkTime { set; get; }
        /// <summary>
        /// //loai hinh moi nguy
        /// </summary>
        public string dangerType { set; get; }
        /// <summary>
        ///  muc do nguy hiem
        /// </summary>
        public string dangerLevel { set; get; }
        /// <summary>
        /// // ghi chu cua ng kiem tra
        /// </summary>
        public string checkRemark { set; get; }
        /// <summary>
        /// //  moc thoi gian cai thien (do ng check yeu cau)
        /// </summary>
        public string improvementDay { set; get; }
        /// <summary>
        /// / // id hinh anh moi nguy.
        /// </summary>
        public string imageId { set; get; }
        /// <summary>
        /// // url hinh anh moi nguy
        /// </summary>
        public string imageUrl { set; get; }


        /// <summary>
        /// // id hinh anh khac phuc do ng xu ly up len (option)
        /// </summary>
        public string fixImgId { set; get; }

        /// <summary>
        /// url hinh anh khac phuc
        /// </summary>
        public string fixImgUrl { set; get; }



        /// <summary>
        /// // ma the ng phu trach giai quyet van de
        /// </summary>
        public string inchargeEmpNo { set; get; }
        /// <summary>
        ///  // ho ten ng phu trach giai quyet van de
        /// </summary>
        public string inchargeEmpName { set; get; }
        /// <summary>
        /// // dept cua nguoi phu trach giai quyet van de
        /// </summary>
        public string inchargeEmpDept { set; get; }

        /// <summary>
        ///  mail cua ng phu trch giai quyet van de 
        /// </summary>
        public string inchargeEmpMail { set; get; }


        /// <summary>
        /// // ghi chu cua nguoi phu trach giai quyet van de
        /// </summary>
        public string inchargeRemark { set; get; }


        /// <summary>
        ///  ma the nhan vien phe duyet tu bo phan EHS
        /// </summary>
        public string picBossEmpNo { set; get; }
        /// <summary>
        /// Ho ten nhan vien phe duyet tu bo phan EHS
        /// 
        /// </summary>
        public string picBossEmpName { set; get; }
        /// <summary>
        /// Mail nhan vien phe duyet tu bo phan EHS
        /// </summary>
        public string picBossEmpMail { set; get; }


       
        /// <summary>
        ///  // ma dau don
        /// </summary>
        public string docNo { set; get; }
        /// <summary>
        /// // trang thai dau don
        /// </summary>
        public string docStatus { set; get; }
        /// <summary>
        /// // ngay hoan thanh don.
        /// </summary>
        public string docCompletedDate { set; get; }

        /// <summary>
        ///  Phần kiểm tra này có thuộc dự án nào không
        /// </summary>
        public string docType { set; get; }

        /// <summary>
        /// Phần kiểm tra này có thuộc dự án cu the thi ten du an la truong nay
        /// </summary>
        public List<string>  projectNames { set; get; }

        
        /// <summary>
        /// // mo ta moi nguy
        /// </summary>
        public string hazardDesc { set; get; }
        /// <summary>
        /// // tieu chuan can cu
        /// </summary>
        public string hazardBasisStandard { set; get; }
        /// <summary>
        /// / doi sach cai thien
        /// </summary>
        public string improvecountermeasures { set; get; }

        /// <summary>
        ///  tinh trang xu ly : tinh trang xu ly  (hien thi trong form cua ng kiem tra ): -- chua xu ly , dang xu ly , da xu ly xong 

        /// </summary>
        public string handleStatus { set; get; }

        /// <summary>
        /// // muc do uu tien  : thap , trung binh ,cao
        /// </summary>
        public string priorityLevel { set; get; }

        /// <summary>
        ///full noi dung render do AI tra ve -> ng dung da luu
        /// </summary>

        public string hazardAiContent { set; get; }


        public string hazardAiContentTemp { set; get; }


        /// <summary>
        /// Chi tiet vi tri phat hien moi nguy
        /// </summary>
        public string positionDetail { set; get; }


        



    }

    public class DoNotifyHazardReq: NotifyHazardTotalData
    {

    }



    public class Analysis
    {

        /// <summary>
        /// // noi dung text phan tich
        /// </summary>
        public string content { set; get; }
        /// <summary>
        /// // noi dung text dk dich
        /// </summary>
        public string contentTranslate { set; get; }

        /// <summary>
        /// phan loai : KeyTypeAnalysis
        /// </summary>
        public string type { set; get; }
        /// <summary>
        /// // so thu tu de ghep vao danh sach cuoi cung
        /// </summary>
        public int sort { set; get; }
        /// <summary>
        /// //  la du lieu do user add thu cong ?
        /// </summary>
        public bool isUserAdd { set; get; }

    }


    public enum KeyTypeAnalysis
    {
        /// <summary>
        /// Mô tả mối nguy hiểm
        /// </summary>
        DescribeTheHazard = 0,
        /// <summary>
        /// Mức độ nguy hiểm
        /// </summary>
        RiskLevel = 1,
        /// <summary>
        /// Đối sách cải thiện
        /// </summary>
        SolutionAndProposedImplementation = 2,
    }
}