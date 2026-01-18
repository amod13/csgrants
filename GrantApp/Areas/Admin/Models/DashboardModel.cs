using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Areas.Admin.Models
{
    public class DashboardModel
    {
        public string SectionName { get; set; }
        public int TotalProgram { get; set; }
        public int ProgramPhaseNumber { get; set; }
        public int RGAFiscalYearId { get; set; }
        public string ProgramPhaseNumberStr { get; set; }

        public List<DashboardModel> DashboardModelList { get; set; }
        public List<DashboardModel> DashboardModelListComplemnetryGrant { get; set; }
        public List<DashboardModel> DashboardModelListSpecialGrant { get; set; }

        public DashboardModel ObjDashboardModel { get; set; }
        public AdminDashboardNewprojectCountListViewModel ObjAdminDashboardNewprojectCountListViewModel { get; set; }
        public GetSubprogramDetailByIdViewModel ObjGetSubprogramDetailByIdViewModel { get; set; }
        public List<GetSubprogramDetailByIdViewModel> GetSubprogramDetailByIdViewModelList { get; set; }

        public List<SecionWiseProgramCountViewModel> SecionWiseProgramCountViewModelList { get; set; }

        public TotalProgramListPhaseWiseNewModel objTotalProgramListPhaseWiseNewModel { get; set; }
    }
    public class AdminDashboardNewprojectCountListViewModel
    {
        public int TotalApproved { get; set; }
        public int BisheshCount { get; set; }
        public int SampurakCount { get; set; }
        public int TotalBishesh { get; set; }
        public int TotalSampurak { get; set; }
        public int TotalSubmited { get; set; }
        public int TotalRunning { get; set; }
        public int NewAndRunning { get; set; }


    }

    public class GetSubprogramDetailByIdViewModel
    {
        public string PhaseTitle { get; set; }
        public string GrantTypeName { get; set; }
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public bool ApprovedStatus { get; set; }
        public int Status { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal AmountProvinceVdc { get; set; }
        public decimal NGOINGOAmount { get; set; }
        public string OfficeName { get; set; }
        public decimal GrantedAmountFirst { get; set; }
        public decimal? AmountSecondYear { get; set; }
        public decimal? GrantedAmountThird { get; set; }
        public int TimeDurationYear { get; set; }
        public decimal? GrantedAmountSecond { get; set; }
        [NotMapped]
        public decimal ApprovedAmountByAdmin { get; set; }
        [NotMapped]
        public decimal? BudgetForFirstYear { get; set; }
        [NotMapped]
        public decimal? BudgetForSecondYear { get; set; }
        [NotMapped]
        public decimal? BudgetForThirdYear { get; set; }
    }

    public class SecionWiseProgramCountViewModel
    {
        public int TotalProgram { get; set; }
        public string SectionName { get; set; }
        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public int BisheshApproved { get; set; }
        public int SampurakApproved { get; set; }
        public int BisheshApprovedLocalLevel { get; set; }
        public int SampurakApprovedLocalLevel { get; set; }
    }

    public class TotalProgramListPhaseWiseNewModel
    {
        public int TotalProgramCount { get; set; }
        public int TotalBisheshCount { get; set; }
        public int TotalSamapurakCount { get; set; }
    }


}