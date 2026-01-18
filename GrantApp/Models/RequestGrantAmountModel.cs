using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class RequestGrantAmountModel
    {
        [Key]
        public int RequestGrantAmountId { get; set; }
        public int ProgramId { get; set; }
        public int OfficeId { get; set; }
        public int FiscalYearId { get; set; }
        public int ProgramTimeDuration { get; set; }
        public decimal? AmountFirst { get; set; }
        public decimal? AmountSecond { get; set; }
        public decimal? AmountThird { get; set; }
        public decimal? ChaluAmount { get; set; }
        public decimal? PujiAmount { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage = "ठेक्का रकम (रु. लाखमा)  लेख्नुहोस ।")]
        public decimal? ContractAmount { get; set; }

        [Required(ErrorMessage = "आगामी आ.व.का लागि सम्बन्धित स्थानीय तह/प्रदेश सरकारले बेहोर्ने रकम (रु. लाखमा) लेख्नुहोस ।")]
        public decimal? OfficeAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public int ViewBagGrantTypeId { get; set; }

        [NotMapped]
        public string ProgramTitle { get; set; }
        [NotMapped]
        public decimal TotalBudgetForProgram { get; set; }
        [NotMapped]
        public int? ProgramPhaseNumber { get; set; }
        [NotMapped]
        public bool? ApprovedStatusBool { get; set; }
        [NotMapped]
        public decimal? BudgetForFirstYear { get; set; }
        [NotMapped]
        public decimal? BudgetForSecondYear { get; set; }
        [NotMapped]
        public decimal? BudgetForThirdYear { get; set; }

        public bool? IsCanceled { get; set; }

        [NotMapped]
        public string RequestReasonDoc { get; set; }

        public RequestGrantAmountModel ObjRequestGrantAmountModel { get; set; }
        public List<RequestGrantAmountModel> RequestGrantAmountModelList { get; set; }


        public ViewRequestGrantAmountModel ObjViewRequestGrantAmountModel { get; set; }
        public List<ViewRequestGrantAmountModel> ViewRequestGrantAmountModelList { get; set; }

        public RequestGrantAmountModel()
        {
            ObjViewRequestGrantAmountModel = new ViewRequestGrantAmountModel();
            ViewRequestGrantAmountModelList = new List<ViewRequestGrantAmountModel>();
        }
    }

    public class ViewRequestGrantAmountModel
    {
        public int SubProgramId { get; set; }
        public int OfficeId { get; set; }
        public string SectionName { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int TimeDurationYear { get; set; }
        public decimal AmountProvinceVdc { get; set; }
        public decimal NGOINGOAmount { get; set; }
        public string OfficeName { get; set; }
        public int UserType { get; set; }
        public int GrantTypeId { get; set; }
        public decimal AmountFirst { get; set; }
        public decimal AmountSecond { get; set; }
        public decimal AmountThird { get; set; }
        public bool GrantAmountStatus { get; set; }

        public decimal? RequestGrantAmount { get; set; }
        public int FiscalYearId { get; set; }
        public decimal? GrantedAmountByAdmin { get; set; }
        public int ProgramPhaseNumber { get; set; }
        public int ViewBagFiscalYearId { get; set; }

        public decimal? BudgetForFirstYear { get; set; }
        public decimal? BudgetForSecondYear { get; set; }
        public decimal? BudgetForThirdYear { get; set; }




    }
}