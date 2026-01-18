using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class ReportModel
    {
        public int SearchBy {  get; set; } 
        public int ProvinceIdSearch { get; set; }
        public int DistrictIdSearch { get; set; }
        public int VDCMUNIdSearch { get; set; }

        public int GrantTypeIdSearch { get; set; }
        public int ProgramCountIdSearch { get; set; }


        public int ViewBagGrantTypeId { get; set; }
        public int ApprovedOrRejectedStatusId { get; set; }


        public int GrantType1Two { get; set; }
        public int GrantType1Five { get; set; }
        public int GrantType1Seven { get; set; }
        public int GrantType1Eight { get; set; }
        public int GrantType1Nine { get; set; }
        public int GrantType1Ten { get; set; }


        public int GrantType2TwentyTwo { get; set; }
        public int GrantType2TwentySix { get; set; }
        public int GrantType2Thirty { get; set; }
        public int GrantType2ThirtyOne { get; set; }
        public int GrantType2ThirtyTwo { get; set; }
        public int ProvinceVDCAmountTotalBudget { get; set; }

        public int ProgressReportViewBagId { get; set; }
        public int ProgramPhaseNumber { get; set; }
        public int QuirdId { get; set; }
        public int SubmitedOrNotSubmited { get; set; }

        public int OfficeIdSearch { get; set; }

        public int MainSectionIDForOne { get; set; }
        public int MainSectionIDForTwo { get; set; }
        public int TotalSelectedProgramCount { get; set; }
        public int ViewBagProgramPhaseNumber { get; set; }
        public int RunningOrNewProgramInt { get; set; }

        public ComplementryReportViewModel ObjComplementryReportViewModel { get; set; }
        public List<ComplementryReportViewModel> ComplementryReportViewModelList { get; set; }

        public List<ComplementryReportViewModel> RunningApplicationListViewModelList { get; set; }
        public List<SubProgramDuplicateViewModel> SubProgramDuplicateViewModelList { get; set; }

        public List<AnusuchiOneViewModel> AnusuchiOneViewModelList { get; set; }

        public ApplicationPointsFromAdminViewModel ObjApplicationPointsFromAdminViewModel { get; set; }
        public List<ApplicationPointsFromAdminViewModel> ApplicationPointsFromAdminViewModelList { get; set; }

        public SubprogramDetailForPartialViewModel ObjSubprogramDetailForPartialViewModel { get; set; }
        public List<SubprogramDetailForPartialViewModel> SubprogramDetailForPartialViewModelList { get; set; }

        public List<OfficesNotSubmitedProgressRptviewModel> OfficesNotSubmitedProgressRptviewModelList { get; set; }

        public MakeApprovedFinalListViewModel ObjMakeApprovedFinalListViewModel { get; set; }
        public List<MakeApprovedFinalListViewModel> MakeApprovedFinalListViewModelList { get; set; }

        public List<ViewAllDetailsOfOfficeViewModel> ViewAllDetailsOfOfficeViewModelList { get; set; }
        public List<ViewAllProgressReportDtlViewModel> ViewAllProgressReportDtlViewModelList { get; set; }

        public List<SelectedListProvinceWiseViewModel> SelectedListProvinceWiseViewModelList { get; set; }
        public List<SelectedListProvinceWiseViewModel> SelectedApprovedListProvinceWiseViewModelList { get; set; }
        public List<OfficeDetalProvinceWiseViewModel> OfficeDetalProvinceWiseViewModelList { get; set; }

        public List<ViewAllSifarishVM> ViewAllSifarishVMList { get; set; }

        public List<ProgressReportSubmitedByOfficeVM> ProgressReportSubmitedByOfficeVMList { get; set; }

        public List<AnusuchiOneViewModelForFM> AnusuchiOneViewModelForFMList { get; set; }
        public List<AnusuchiComplementryViewModelForFM> AnusuchiComplementryViewModelForFMList { get; set; }

        public List<AdhuroApuroReportViewModel> AdhuroApuroGrantRequestListVM { get; set; }

        public List<RemainingBhuktaniGrantReportViewModel> RemainingBhuktaniGrantRequestListVM { get; set; }

        //public List<RemainingBhuktaniGrantReportViewModel> RemainingBhuktaniGrantRequestListVM { get; set; }

    }


    public class AdhuroApuroReportViewModel:AdhuroApuroGrantRequest
    {

        public int Id { get; set; }
        
        public string FiscalYearName { get; set; }
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public string OfficeName { get; set; }

        public string DistrictName { get;set; }

        public decimal TotalBudget { get; set; }
        public int? TimeDurationYear { get; set; }

        public decimal TotalRequestBudget { get; set; }

        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public int PhaseStatus { get; set; }

        public string ContractAgreementFilePath { get; set; }
        
        public string PaymentVoucherFilePath { get; set; }

        public string ProjectImagesFilePath { get; set; }

       public string ExtensionLetterFilePath { get; set; }
       
        public string CommitmentLetterFilePath { get; set; }
       
        public string ExecutiveDecisionFilePath { get; set; }



    }


    public class RemainingBhuktaniGrantReportViewModel : RemainingBhuktaniGrantRequest
    {

        public int Id { get; set; }

        public string FiscalYearName { get; set; }
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public string OfficeName { get; set; }

        public string DistrictName { get; set; }

        public decimal TotalBudget { get; set; }
        public int? TimeDurationYear { get; set; }

        public decimal TotalRequestBudget { get; set; }

        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public int PhaseStatus { get; set; }


    }


    public class PublicGrantReportViewModel : PublicPrivateGrantRequest
    {

        public int Id { get; set; }

        public string FiscalYearName { get; set; }
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public string OfficeName { get; set; }

        public string DistrictName { get; set; }

        public decimal TotalBudget { get; set; }
        public int? TimeDurationYear { get; set; }

        public decimal TotalRequestBudget { get; set; }

        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public int PhaseStatus { get; set; }


    }




    public class ComplementryReportViewModel
    {
        public int SubProgramId { get; set; }
        public int OfficeId { get; set; }

 
        public string SubProgramTitle { get; set; }

        public int PhaseStatus { get; set; }
        public string AyojanaAddress { get; set; }

        public int ProbableBenefitedPopulation { get; set; }
        public string GroupName { get; set; }
        public decimal ContributionPercent { get; set; }
      //  public string SubProgramTitle { get; set; }

        public int SpecialGrantRanking { get; set; }

        public decimal TotalBudget { get; set; }
        public int TimeDurationYear { get; set; }
        public decimal AmountProvinceVdc { get; set; }
        public decimal NGOINGOAmount { get; set; }
        public string OfficeName { get; set; }

        public string LMBISCode { get; set; }
        public int UserType { get; set; }

        public int ProvinceCode { get; set; }
        public string ProvinceName { get; set; }

        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public int GrantTypeId { get; set; }
        public string SectionName { get; set; }

        public string Programnames { get; set; }
        public string VDCMUNCode { get; set; }


        public decimal? ContractAmount { get; set; }


        //For Grant Type 1
        public string Doc2 { get; set; }
        public string Doc5 { get; set; }
        public string Doc7 { get; set; }
        public string Doc8 { get; set; }
        public string Doc9 { get; set; }
        public string Doc10 { get; set; }

        //For Grant Type 2
        public string Doc22 { get; set; }
        public string Doc26 { get; set; }
        public string Doc30 { get; set; }
        public string Doc31 { get; set; }
        public string Doc32 { get; set; }


        //after phase 7
         
        public string RequestFileDoc { get; set; }
        public string FinalDocumentsUrl { get; set; }


        public string EstimateDoc { get; set; }

        public string EiaDoc { get; set; }

        public string AnusuchiDoc { get; set; }



        public decimal GrantedAmount { get; set; }


        public int QuadrimesterId { get; set; }
        public int QuadrimesterReportsDetailId { get; set; }

        public int ProgramPhaseNumber { get; set; }

        public decimal AmountSecond { get; set; }

        public string ProjectFileUpload { get; set; }
        public string PictureOfProjectOne { get; set; }
        public string PictureOfProjectTwo { get; set; }
        public string PictureOfProjectThree { get; set; }

        public string PhaseTitle { get; set; }
        public decimal FirstInstallment { get; set; }
        public decimal SecondInstallment { get; set; }
        public decimal ThirdInstallment { get; set; }
        public int? ProgramPirority { get; set; }
        //public int QuadrimesterId { get; set; }
        public string GrantTypeName { get; set; }
        public int ProvinceIDSearch { get; set; }
        public int DistrictIdSearch { get; set; }
        public int VdcMunIdSearch { get; set; }



        public decimal? BudgetForFirstYear { get; set; }
        public decimal? BudgetForSecondYear { get; set; }
        public decimal? BudgetForThirdYear { get; set; }

        public decimal? TotalAmountUsed { get; set; }
        public decimal? TotalNikashaRamam { get; set; }
        public decimal? TotalContractAmount { get; set; }
        public string ProgramPhaseNumberStr { get; set; }
        public int ProgramCodeNumber { get; set; }

        public decimal? ChaluAmount { get; set; }
        public decimal? PujigatAmount { get; set; }
        public int? AppRunningStatus { get; set; }

        public int? FY207677 { get; set; }
        public int? FY207778 { get; set; }
        public int? FY207879 { get; set; }
        public int? FY207980 { get; set; }
        public int? FY208081 { get; set; }
        public string CompletionStatus { get; set; }

        public decimal? PujiFirstYearAmount { get; set; }
        public decimal? PujiSecondYearAmount { get; set; }
        public decimal? PujiThirdYearAmount { get; set; }



        public List<ComplementryReportViewModel> ComplementryReportViewModelList { get; set; }
        public List<ComplementryReportViewModel> RunningApplicationListViewModelList { get; set; }

    }

    public class OfficeDetailsViewModel
    {
        //public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string Address { get; set; }
        public string ProvinceTitleEng { get; set; }
        public string DistrictNameEng { get; set; }
        public string VdcMunNameEng { get; set; }
        public string VDCMUNCode { get; set; }
        [NotMapped]
        public int ProgramPhaseNumber { get; set; }
        [NotMapped]
        public int RequestOrNotDDID { get; set; }

        [NotMapped]
        public int GrantTypeSearchID { get; set; }
        [NotMapped]
        public int OfficeId { get; set; }
        [NotMapped]
        public int ProvincesId { get; set; }

        [NotMapped]
        public int ProvinesIdSearch { get; set; }
        [NotMapped]
        public int DistrictIdSearch { get; set; }
        [NotMapped]
        public int VDCMUNIdSearch { get; set; }
        [NotMapped]
        public int ApprovedOrRejectedIdSearch { get; set; }
        [NotMapped]
        public int Bishesh { get; set; }
        [NotMapped]
        public int Sampurak { get; set; }
        [NotMapped]
        public int TotalGrant { get; set; }
        [NotMapped]
        public string PhaseTitle { get; set; }
        public List<OfficeDetailsViewModel> OfficeDetailsViewModelList { get; set; }

    }


    public class SubProgramDuplicateViewModel
    {
        public int SubProgramId { get; set; }
        public string SectionName { get; set; }
        public string ProgramName { get; set; }
        public string OfficeName { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProvinceTitleEng { get; set; }
        public string DistrictNameEng { get; set; }
        public string VdcMunNameEng { get; set; }
        public string OfficeType { get; set; }
        public string VDCMUNCode { get; set; }
    }

    public class AnusuchiOneViewModel
    {
        public string SectionName { get; set; }
        public string SubProgramTitle { get; set; }
        public int SubProgramId { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal AmountProvinceVdc { get; set; }
        public int TimeDurationYear { get; set; }
        public string ProvinceTitleNep { get; set; }
        public string DistrictNameNep { get; set; }
        public string VdcMunNameNep { get; set; }
        public int? PhaseStatus { get; set; }
        public decimal FirstInstallMent { get; set; }
        public decimal? SecondInstallMent { get; set; }
        public decimal? ThirdInstallMent { get; set; }
        public string PhaseTitle { get; set; }
        public string GrantTypeName { get; set; }

    }

    public class ViewApplicationDetailsByOfficeIdModel
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }

        public int TimeDurationYear { get; set; }
        public decimal TotalBudget { get; set; }
        public string PhaseTitle { get; set; }
        public string GrantTypeName { get; set; }
        public string SectionName { get; set; }
        public string OfficeName { get; set; }
        public int GrantTypeId { get; set; }

        public List<ViewApplicationDetailsByOfficeIdModel> ViewApplicationDetailsByOfficeIdModelList { get; set; }
    }


    public class ApplicationPointsFromAdminViewModel
    {
        public int PointsByAdminId { get; set; }
        public int SubprogramId { get; set; }
        public int VariableId2 { get; set; }
        public int VariableId3 { get; set; }
        public int VariableId4 { get; set; }
        public int VariableId5 { get; set; }
        public int VariableId6 { get; set; }
        public int VariableId11 { get; set; }
        public int VariableId12 { get; set; }
        public int VariableId13 { get; set; }
        public int VariableId14 { get; set; }
        public int VariableId16 { get; set; }
        public int VariableId18 { get; set; }
        public int VariableId19 { get; set; }
        public int PointsObtained { get; set; }
        public int TotalPoints { get; set; }
        public int GrantTypeId { get; set; }

    }
    public class SubprogramDetailForPartialViewModel
    {
        public string OfficeName { get; set; }
        public string GrantTypeName { get; set; }
        public string SectionName { get; set; }
        public string ProgramName { get; set; }
        public string SubProgramTitle { get; set; }
        public int TimeDurationYear { get; set; }
        public decimal TotalBudget { get; set; }

    }

    public class OfficesNotSubmitedProgressRptviewModel
    {
        public string OfficeName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
    }
    public class ViewAllDetailsOfOfficeViewModel
    {
        public string OfficeName { get; set; }
        public string PhaseTitle { get; set; }
        public int SubProgramID { get; set; }
        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public string SubProgramTitle { get; set; }
        public int TimeDurationYear { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal? BudgetForFirstYear { get; set; }
        public decimal? BudgetForSecondYear { get; set; }
        public decimal? BudgetForThirdYear { get; set; }
        public decimal Ist { get; set; }
        public decimal? IInd { get; set; }
        public decimal? IIIrd { get; set; }
        public decimal? RequestAmountIst { get; set; }
        public decimal? RequestAmountIInd { get; set; }
        public decimal? RequestAmountIIIrd { get; set; }
        public bool? IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public bool ApprovedStatus { get; set; }
        public int ViewBagPhaseNumber { get; set; }
        public decimal? ContractAmount { get; set; }


    }

    public class ViewAllProgressReportDtlViewModel
    {
        public string PhaseTitle { get; set; }
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public int TimeDurationYear { get; set; }
        public string SubmitedYear { get; set; }
        public int QuadrimesterId { get; set; }
    }
    public class SelectedListProvinceWiseViewModel
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
        public int ProOneSelected { get; set; }
        public int ProTwoSelected { get; set; }
        public int ProThreeSelected { get; set; }
        public int ProFourSelected { get; set; }
        public int ProFiveSelected { get; set; }
        public int ProSixSelected { get; set; }
        public int ProSevenSelected { get; set; }
        public int TotalSelected { get; set; }
    }
    public class OfficeDetalProvinceWiseViewModel
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string AuthorizedEmail { get; set; }
        public string DistrictNameNep { get; set; }
        public string VdcMunNameNep { get; set; }
    }

    public class ViewAllSifarishVM
    {
        public string ProvinceTitleNep { get; set; }
        public string DistrictNameNep { get; set; }
        public string VdcMunNameNep { get; set; }
        public int OfficeId { get; set; }
        public int? Total20762077 { get; set; }
        public int? Total20772078 { get; set; }
        public int? Total20782079 { get; set; }
        public int? Total20792080 { get; set; }
        public int? Total20802081 { get; set; }
        public int? TotalTillDate { get; set; }

        public decimal? Total20762077Sum { get; set; }
        public decimal? Total20772078Sum { get; set; }
        public decimal? Total20782079Sum { get; set; }
        public decimal? Total20792080Sum { get; set; }
        public decimal? Total20802081Sum { get; set; }
        public decimal? GrantTotalSum { get; set; }

        public int? TotalRequestedProgram { get; set; }
    }


    public class ProgressReportSubmitedByOfficeVM
    {
        public int SubProgramId { get; set; }
        public int OfficeId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int TimeDurationYear { get; set; }
        public string OfficeName { get; set; }
        public int UserType { get; set; }
        public int GrantTypeID { get; set; }

        public string ProvinceTitleNep { get; set; }
        public string DistrictNameNep { get; set; }     


    }

    public class AnusuchiOneViewModelForFM
    {
        public string PhaseTitle { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceTitleNep { get; set; }
        public string DistrcitCode { get; set; }
        public string VdcMunCode { get; set; }
        public string DistrictNameNep { get; set; }
        public string VdcMunNameNep { get; set; }
        public string SectionName { get; set; }
        public string SubProgramTitle { get; set; }
        public int? TimeDurationYear { get; set; }
        public decimal? TotalBudget { get; set; }
        public decimal? ApprovedBudget { get; set; }
        public string Remarks { get; set; }
        public int SubProgramId { get; set; }
        public string LMBISCODE { get; set; }
        public decimal? ChaluTotal { get; set; }
        public decimal? PujigatTotal { get; set; }


    }


    public class AnusuchiComplementryViewModelForFM
    {
        public string PhaseTitle { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceTitleNep { get; set; }
        public string DistrcitCode { get; set; }
        public string VdcMunCode { get; set; }
        public string DistrictNameNep { get; set; }
        public string VdcMunNameNep { get; set; }
        public string SectionName { get; set; }
        public string SubProgramTitle { get; set; }
        public int? TimeDurationYear { get; set; }
        public decimal? TotalBudget { get; set; }
        public decimal? ApprovedBudget { get; set; }
        public string Remarks { get; set; }
        public int SubProgramId { get; set; }
        public string LMBISCODE { get; set; }
      


    }
}