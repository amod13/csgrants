using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace GrantApp.Areas.Admin.Models
{
    public class AdminReport
    {

        public DashboardNewProgramListViewModel ObjDashboardNewProgramListViewModel { get; set; }
        public List<DashboardNewProgramListViewModel> DashboardNewProgramListViewModelList { get; set; }
        //public IPagedList<DashboardNewProgramListViewModel> DashboardNewProgramListViewModelListPL { get; set; }
        public IPagedList<DashboardNewProgramListViewModel> pageList;

        public AllProgramListOfClientViewModel ObjAllProgramListOfClientViewModel { get; set; }
        public List<AllProgramListOfClientViewModel> AllProgramListOfClientViewModelList { get; set; }

        public int FiscalYearID { get; set; }
        public int GrantTypeId { get; set; }
        public int SubmitOrNot { get; set; }
        public int OfficeId { get; set; }
        public int CurrentPhaseNumber { get; set; }
        public int ProvinceId { get; set; }

        public int PageNumber { get; set; }
        public int TotalPageNumberToDisplay { get; set; }

        public int DistrictIdSearch { get; set; }
        public int VDCMUNIdSearch { get; set; }
        public int GrantTypeIdSearch { get; set; }

        public int ProvinceOrLocalLevel { get; set; }



        public List<canceledApplicationListViewModel> canceledApplicationListViewModelList { get; set; }
        public List<ViewNewProgramProvinceWiseStatusVM> ViewNewProgramProvinceWiseStatusVMList { get; set; }
        public AdminReport()
        {
            ObjDashboardNewProgramListViewModel = new DashboardNewProgramListViewModel();
            DashboardNewProgramListViewModelList = new List<DashboardNewProgramListViewModel>();
            AllProgramListOfClientViewModelList = new List<AllProgramListOfClientViewModel>();
            canceledApplicationListViewModelList = new List<canceledApplicationListViewModel>();
            ViewNewProgramProvinceWiseStatusVMList = new List<ViewNewProgramProvinceWiseStatusVM>();
        }


    }

    public class DashboardNewProgramListViewModel
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int TimeDurationYear { get; set; }
        public string GrantTypeName { get; set; }
        public string SectionName { get; set; }
        public string OfficeName { get; set; }
        public string PhaseTitle { get; set; }
        public decimal? RequestedAmount { get; set; }
        public bool? ApprovedStatus { get; set; }
        public int? PhaseStatus { get; set; }
        public decimal? ContractAmount { get; set; }
    }


    public class AllProgramListOfClientViewModel
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int TimeDurationYear { get; set; }
        public string GrantTypeName { get; set; }
        public string SectionName { get; set; }
        public string OfficeName { get; set; }
        public string PhaseTitle { get; set; }
        public decimal? RequestedAmount { get; set; }
        public bool? ApprovedStatus { get; set; }
        public int SubmitStatus { get; set; }
        public int GrantTypeId { get; set; }
        public int ProgramPhaseNumberId { get; set; }


    }
    public class ViewNewProgramProvinceWiseStatusVM
    {
        public int TotalProgram { get; set; }
        public string PhaseTitle { get; set; }
        public int ProOneTotal { get; set; }
        public int ProTwoTotal { get; set; }
        public int ProThreeTotal { get; set; }
        public int ProFourTotal { get; set; }
        public int ProFiveTotal { get; set; }
        public int ProSixTotal { get; set; }
        public int ProSevenTotal { get; set; }
        //public int ProOneSelected { get; set; }
        //public int ProTwoSelected { get; set; }
        //public int ProThreeSelected { get; set; }
        //public int ProFourSelected { get; set; }
        //public int ProFiveSelected { get; set; }
        //public int ProSixSelected { get; set; }
        //public int ProSevenSelected { get; set; }
    }

    public class canceledApplicationListViewModel
    {
        public string ProvinceTitleEng { get; set; }
        public string DistrictNameEng { get; set; }
        public string VdcMunNameEng { get; set; }
        public string PhaseTitle { get; set; }
        public string GrantTypeName { get; set; }
        public string SectionName { get; set; }
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public int TimeDurationYear { get; set; }
        public string CancelledRemarks { get; set; }
        public string CancelledDocuments { get; set; }

        public string CancelledDocuments1 { get; set; }
        public DateTime? CancelledDate { get; set; }
        public int? PhaseStatus { get; set; }



    }

    public class GetSummaryMaxApprovedProgramWiseVM
    {
        [System.ComponentModel.DisplayName("कार्यालयको नाम")]
        public string OfficeName { get; set; }
        [System.ComponentModel.DisplayName("जम्मा -हालसम्म")]
        public decimal? Total { get; set; }

        [System.ComponentModel.DisplayName("विशेष जम्मा -२०७६/७७")]
        public decimal? TotalBisheshPhase1 { get; set; }
        [System.ComponentModel.DisplayName("समपूरक जम्मा -२०७६/७७")]
        public decimal? TotalSampurakPhase1 { get; set; }


        [System.ComponentModel.DisplayName("विशेष जम्मा -२०७७/७८")]
        public decimal? TotalBisheshPhase2 { get; set; }
        [System.ComponentModel.DisplayName("समपूरक जम्मा -२०७७/७८")]
        public decimal? TotalSampurakPhase2 { get; set; }


        [System.ComponentModel.DisplayName("विशेष जम्मा -२०७८/७९")]
        public decimal? TotalBisheshPhase3 { get; set; }
        [System.ComponentModel.DisplayName("समपूरक जम्मा -२०७८/७९")]
        public decimal? TotalSampurakPhase3 { get; set; }

        [System.ComponentModel.DisplayName("विशेष जम्मा -२०७९/८०")]
        public decimal? TotalBisheshPhase4 { get; set; }
        [System.ComponentModel.DisplayName("समपूरक जम्मा -२०७९/८०")]
        public decimal? TotalSampurakPhase4 { get; set; }
    }

    




}