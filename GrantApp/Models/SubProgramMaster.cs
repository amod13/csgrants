using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrantApp.Models
{
    public class SubProgramMaster
    {

        [Key]
        public int SubProgramId { get; set; }
        public int ProgramId { get; set; }
        public int OfficeId { get; set; }
        [Required(ErrorMessage = "कृपया आयोजना/कार्यक्रमको नाम लेख्नुहोस")]
        public string SubProgramTitle { get; set; }

        //[Range(1000000, int.MaxValue, ErrorMessage = "कुल लागत कम भयो, सच्चाउनुहोस ।")]
        [Remote("ValidateBudgetSamapurak", "Grant", ErrorMessage = "Budget must be between 100 and 700.")]
        public decimal TotalBudget { get; set; }

        [NotMapped]
        //[Remote("ValidateBudgetBisesh", "Grant", ErrorMessage = "Budget must be between 100 and 700.")]
        public decimal TotalBudgetBisesh { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "लाभान्वित सम्भाब्य जनसंख्याको नम्बर लेख्नुहोस ।")]
        public int ProbableBenefitedPopulation { get; set; }
        //[Range(1, 3, ErrorMessage = "अवधि (वर्ष) 1 देखी 3 सम्म मात्र मान्य हुन्छ ।")]
        public int TimeDurationYear { get; set; }


        public int GrantTypeId { get; set; }

        public decimal? AmountProvinceVdc { get; set; }
        public decimal? NGOINGOAmount { get; set; }
        public decimal? GovnNepalAmount { get; set; }
        public bool? ApprovedStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public int MainSectionId { get; set; }

        //[Required(ErrorMessage = "कम्तीमा एउटा कार्यक्रम चयन गर्नुहोस्।")]
        //public List<int> SelectedProgramIds { get; set; } = new List<int>(); // Store multiple IDs


        [NotMapped]
        public List<int> SelectedProgramIds
        {
            get
            {
                return string.IsNullOrEmpty(SelectedPrograms)
                    ? new List<int>()
                    : SelectedPrograms.Split(',').Select(int.Parse).ToList();
            }
            set
            {
                SelectedPrograms = string.Join(",", value);
            }
        }

        // List of available programs for selection (Not Mapped to DB)
        [NotMapped]
        public List<SelectListItem> AvailablePrograms { get; set; }

        [NotMapped]
        public List<SubProgramFileViewModel> Files { get; set; } = new List<SubProgramFileViewModel>(); // List of uploaded files

        public string SelectedPrograms { get; set; } // Stores comma-separated values

        public string OtherProgram { get; set; } // Stores custom input when "Others" is selected

        public string AyojanaAddress { get; set; } // Stores custom input when "Others" is selected

        [NotMapped]
        public int ViewbagGrantTypeId { get; set; }
        [NotMapped]
        public int ViewbagCurrentPhaseNumber { get; set; }
        [NotMapped]
        public int ViewBagCurrentOfficeType { get; set; }
        [NotMapped]
        public int ViewBagCurrentLoginUserUserTypeId { get; set; }
        [NotMapped]
        public bool ViewBagIfProgramNumberExceed { get; set; }
        [NotMapped]
        public int TotalApprovedProgramCount { get; set; }
        [NotMapped]
        public int TotalRequestGrantAmountCount { get; set; }
        [NotMapped]
        public int TotalSubmitedProgressReportCount { get; set; }

        [NotMapped]
        public int TotalApprovedProgramCountForRPT { get; set; }
        [NotMapped]
        public int TotalApprovedProgramCountForProgressRPT { get; set; }

        //[NotMapped]
        public bool TermsAndCondtions { get; set; }
        public string FinalDocumentsUrl { get; set; }
        public int? FiscalYearId { get; set; }
        public int? PhaseStatus { get; set; }

        public decimal? BudgetForFirstYear { get; set; }
        public decimal? BudgetForSecondYear { get; set; }
        public decimal? BudgetForThirdYear { get; set; }

        public int ProgramPirority { get; set; }

        //updated dec 14 2020
        public bool? IsCancelled { get; set; }
        public string CancelledRemarks { get; set; }
        public DateTime? CancelledDate { get; set; }


        ////For Previous Running Project
        //public decimal? RequestGrantAmount { get; set; }
        //public decimal? GrantedAmountByAdmin { get; set; }
        //public decimal? IIndYearAmount { get; set; }
        //public decimal? IIIrdYearAmount { get; set; }

        public decimal? PujiFirstYearAmount { get; set; } = 0m;
        public decimal? PujiSecondYearAmount { get; set; } = 0m;
        public decimal? PujiThirdYearAmount { get; set; } = 0m;

        [NotMapped]
        public decimal? ApprovedBudgetByNPC { get; set; }

        [NotMapped]
        public HttpPostedFileBase ConditionUploadFile { get; set; }

        [NotMapped]
        public List<CanceledProgramListForRGVM> CanceledProgramListForRGVMList { get; set; }



        //For Validation Rule in viewdetailspage
        [NotMapped]
        public bool IsPointInsertedIntoVarialbesTable { get; set; }
        [NotMapped]
        public int? IsRunning { get; set; }
        [NotMapped]
        public bool? IsSelected { get; set; }
        [NotMapped]
        public int? CanceledPhaseNumber { get; set; }

        [NotMapped]
        public string CanceledDocUrl { get; set; }


        [NotMapped]
        public string CanceledDocUrl1 { get; set; }
        [NotMapped]
        public string IPAddress { get; set; }

        //[NotMapped]
        //public bool Is { get; set; }

        [NotMapped]
        public List<SubProgramMaster> SubProgramMasterList { get; set; }

        [NotMapped]
        public List<SubProgramMaster> SubProgramMasterListNotSubmited { get; set; }


        [NotMapped]
        public List<SubProgramMaster> SubProgramMasterListForPreviousYear { get; set; }


        [NotMapped]
        public SupportingDocumentsModel ObjSupportingDocumentsModel { get; set; }
        [NotMapped]
        public List<SupportingDocumentsModel> SupportingDocumentsModelList { get; set; }
        [NotMapped]
        public SubProgramListModelForAdmin ObjSubProgramListModelForAdmin { get; set; }
        [NotMapped]
        public List<SubProgramListModelForAdmin> SubProgramListModelForAdminList { get; set; }

        [NotMapped]
        public PopulatePointsVariableListViewModel ObjPopulatePointsVariableListViewModel { get; set; }
        [NotMapped]
        public List<PopulatePointsVariableListViewModel> PopulatePointsVariableListViewModelList { get; set; }

        [NotMapped]
        public List<ValuationBasisModel> ValuationBasisModelList { get; set; }
        [NotMapped]
        public ValuationBasisModel ObjValuationBasisModel { get; set; }
        [NotMapped]
        public ProgramConditionsViewModel ObjProgramConditionsViewModel { get; set; }
        [NotMapped]
        public List<ProgramConditionsViewModel> ProgramConditionsViewModelList { get; set; }

        [NotMapped]
        public FifteenYojanaDetails ObjFifteenYojanaDetails { get; set; }
        [NotMapped]
        public List<FifteenYojanaDetails> FifteenYojanaDetailsList { get; set; }


        [NotMapped]
        public ValuationResultViewModel ObjValuationResultViewModel { get; set; }
        [NotMapped]
        public List<ValuationResultViewModel> ValuationResultViewModelList { get; set; }



        [NotMapped]
        public ProgramwiseAmountViewModel ObjProgramwiseAmountViewModel { get; set; }

        [NotMapped]

        public NewProgramInitiation NewProgramInitiationModel { get; set; }

        [NotMapped]
        public List<ProgramwiseAmountViewModel> ProgramwiseAmountViewModelList { get; set; }




        [NotMapped]
        public QuadrimesterReportsDetailViewModel ObjQuadrimesterReportsDetailViewModel { get; set; }
        [NotMapped]
        public List<QuadrimesterReportsDetailViewModel> QuadrimesterReportsDetailViewModelList { get; set; }

        [NotMapped]
        public RequestGrantAmountViewModel ObjRequestGrantAmountViewModel { get; set; }
        [NotMapped]
        public List<RequestGrantAmountViewModel> RequestGrantAmountViewModelList { get; set; }

        [NotMapped]
        public MakeApprovedFinalListViewModel ObjMakeApprovedFinalListViewModel { get; set; }

        [NotMapped]
        public List<YearlyWiseProgressDetailsListVM> YearlyWiseProgressDetailsListVMList { get; set; }

        //[NotMapped]
        //public List<DocumentRequirementsUpload> DocumentRequirementsUploadList { get; set; }

        [NotMapped]
        public List<DocumentsRequirementsViewModel> DocumentsRequirementsViewModel { get; set; }
        [NotMapped]
        public DocumentsRequirementsViewModel ObjDocumentsRequirementsViewModel { get; set; }


        [NotMapped]
        public List<CurrentYearGrantRequestProgramListVM> CurrentYearGrantRequestProgramListVMList { get; set; }



        public SubProgramMaster()
        {
            ObjSupportingDocumentsModel = new SupportingDocumentsModel();
            SupportingDocumentsModelList = new List<SupportingDocumentsModel>();

            ObjSubProgramListModelForAdmin = new SubProgramListModelForAdmin();
            SubProgramListModelForAdminList = new List<SubProgramListModelForAdmin>();

            ObjPopulatePointsVariableListViewModel = new PopulatePointsVariableListViewModel();
            PopulatePointsVariableListViewModelList = new List<PopulatePointsVariableListViewModel>();
            ValuationBasisModelList = new List<Models.ValuationBasisModel>();
            ObjValuationBasisModel = new Models.ValuationBasisModel();

            ObjProgramConditionsViewModel = new ProgramConditionsViewModel();
            ProgramConditionsViewModelList = new List<ProgramConditionsViewModel>();

            ObjFifteenYojanaDetails = new FifteenYojanaDetails();
            FifteenYojanaDetailsList = new List<FifteenYojanaDetails>();
            ObjValuationResultViewModel = new ValuationResultViewModel();
            ValuationResultViewModelList = new List<ValuationResultViewModel>();



            ObjProgramwiseAmountViewModel = new ProgramwiseAmountViewModel();
            ProgramwiseAmountViewModelList = new List<ProgramwiseAmountViewModel>();


            ObjQuadrimesterReportsDetailViewModel = new QuadrimesterReportsDetailViewModel();
            QuadrimesterReportsDetailViewModelList = new List<QuadrimesterReportsDetailViewModel>();

            ObjMakeApprovedFinalListViewModel = new MakeApprovedFinalListViewModel();

            YearlyWiseProgressDetailsListVMList = new List<YearlyWiseProgressDetailsListVM>();
            DocumentsRequirementsViewModel = new List<DocumentsRequirementsViewModel>();

            ObjDocumentsRequirementsViewModel = new DocumentsRequirementsViewModel();
            SubProgramMasterListNotSubmited = new List<SubProgramMaster>();
            //DocumentRequirementsUploadList = new List<DocumentRequirementsUpload>();

            CanceledProgramListForRGVMList = new List<CanceledProgramListForRGVM>();
            CurrentYearGrantRequestProgramListVMList = new List<CurrentYearGrantRequestProgramListVM>();
        }

       



    }


    public class SubProgramFileViewModel
    {
        public byte[] FileData { get; set; }  // Store file content
        public string FileName { get; set; }  // Store file name
        public string FileContentType { get; set; }  // Store MIME type
    }
    public class GrantGroupInfo
    {
        public string Name { get; set; }
        public decimal ContributionPercent { get; set; }
    }

    public class SubProgramMasterDto
    {
        public int SubProgramId { get; set; }
        public int PhaseStatus { get; set; }
        public int TimeDurationYear { get; set; }
    }


    public class SupportingDocumentsModel
    {
        public int ProgramSuppoertingDocId { get; set; }
        public int SubProgramId { get; set; }
        public int OfficeId { get; set; }
        public string FeasibilitiesStudyFile { get; set; }
        public string SDSDocFile { get; set; }
        public string DecisionDocFile { get; set; }
        //public string ProbableBefefitDocFile { get; set; }
        public string EnvironmentDocFile { get; set; }
        public string ExtraDocFile { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }


        public string NecessityForProgram { get; set; }
        public string PhysicalAndManPower { get; set; }
        public bool InfrastrucutreRelatedToProvinces { get; set; }
        public bool PovertyAlleviationAnLivingStandard { get; set; }
        public string YearlyProgramAndBudget { get; set; }
        public bool ResourceSkillAndSourceUsed { get; set; }
        public bool Minimum50PercentageAppropriation { get; set; }
        public bool TopPriorityInGovernment { get; set; }
        public bool InGovernmentPolicyAndProgram { get; set; }






    }

    public class SubProgramListModelForAdmin
    {
        public int SubProgramId { get; set; }
        public int ProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int ProbableBenefitedPopulation { get; set; }
        public int TimeDurationYear { get; set; }
        public string GrantTypeName { get; set; }
        public decimal AmountProvinceVdc { get; set; }
        public decimal NGOINGOAmount { get; set; }
        public decimal GovnNepalAmount { get; set; }
        public string ProvinceTitleNep { get; set; }
        public string OfficeName { get; set; }

    }


    public class PopulatePointsVariableListViewModel
    {
        public int PointsVariableSetupId { get; set; }
        public string VariableName { get; set; }
        public string ColumnName { get; set; }
        public bool Status { get; set; }
        public int DisplayOrder { get; set; }

        [NotMapped]
        public decimal PointsObtain { get; set; }

    }


    public class ProgramConditionsViewModel
    {
        [Key]
        public int ProgramConditionID { get; set; }
        public int GrantTypeId { get; set; }
        public string ConditionsTitle { get; set; }
        //[Range(typeof(bool), "true", "true", ErrorMessage = "चेक गर्नुहोस")]
        public bool IsCheck { get; set; }
        public bool IsUploadFile { get; set; }
        public string UploadFileUrl { get; set; }

        public HttpPostedFileBase SupportingDocFiles { get; set; }
    }

    public class ValuationBasisModel
    {
        public string VariableBasis { get; set; }
        public string SerialNep { get; set; }
        public string VariableDetailBasis { get; set; }
        public int GrantTypeId { get; set; }
        public int VariableId { get; set; }
        public int VariableDetailid { get; set; }
        public bool IsChecked { get; set; }

        public string MainSection { get; set; }
        public string ProgramName { get; set; }
        public string SubProgram { get; set; }
        public int SubProgramId { get; set; }
        public int OfficeId { get; set; }
        public bool IsSystemGenerated { get; set; }
        public int Valuation { get; set; }

        public int ProvinceIdSearch { get; set; }
        public int DistrictIdSearch { get; set; }
        public int VDCMUNIdSearch { get; set; }
        public string DistrictCode { get; set; }
        public string VDCMUNCode { get; set; }
        public string VdcMunNameEng { get; set; }
        public string DistrictNameEng { get; set; }
        public string OfficeName { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal ProvinceNGOINGOAmount { get; set; }
        public decimal AnudhanAmount { get; set; }
        public int TimeDurationYear { get; set; }

        public int ProgramPhaseNumber { get; set; }
        [NotMapped]
        public bool ApprovedStatus { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
    }



    public class FifteenYojanaDetails
    {
        public int FifteenYojanaDetailsId { get; set; }
        public int OptionValue { get; set; }
        public bool IsChecked { get; set; }
        public int SubProgramId { get; set; }
        public int RefGrantVariableId { get; set; }
        public string YojanaTitle { get; set; }

    }

    public class ValuationResultViewModel
    {
        public int VariableId { get; set; }
        public int VariableDetailId { get; set; }
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public string SerialNep { get; set; }
        public bool IsChecked { get; set; }
    }

    public class ProgramwiseAmountViewModel
    {
        public int ProgramwiseAmountId { get; set; }
        public int ProgramId { get; set; }
        public decimal Amount { get; set; }
        public decimal? AmountSecondYear { get; set; }
        public decimal? AmountThirdYear { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        public int ViewBagProgramID { get; set; }
        public int ViewBagReportViewID { get; set; }

        public string ViewBagProgramTitle { get; set; }

    }


    public class QuadrimesterReportsDetailViewModel
    {
        public int QuadrimesterReportsDetailId { get; set; }
        public int OfficeId { get; set; }
        public int ProgramId { get; set; }
        public decimal ApprovedBudget { get; set; }
        public string ProgramConductPlace { get; set; }
        public int QuadrimesterId { get; set; }
        public string TargetedMaterial { get; set; }
        public decimal TargetedFinance { get; set; }
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Invalid Number")]

        public string AchievementMaterial { get; set; }
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Invalid Number")]

        public decimal AchievementFinance { get; set; }
        public string Remarks { get; set; }
        public int PreparedBy { get; set; }
        public DateTime PreparedDate { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool Status { get; set; }
        public bool IsLocked { get; set; }
        public string ProgramTitle { get; set; }
        public decimal GrantedAmount { get; set; }

        public int FiscalYearId { get; set; }

        public bool IsContactNoticeIssued { get; set; }
        public bool IsContractDone { get; set; }
        public bool IsFirstInstallmentTaken { get; set; }


        public string ProjectFileUpload { get; set; }
        public string PictureOfProjectOne { get; set; }
        public string PictureOfProjectTwo { get; set; }
        public string PictureOfProjectThree { get; set; }
        public bool? IsNikashaMaag { get; set; }
        public string NikasaMaagFileUpload { get; set; }

        [Range(0, 999999.99, ErrorMessage = "कृपया रू लाखमा लेख्नुहोस")]
        public decimal? TotalAmountUsed { get; set; }
        [Range(0,999999.99,ErrorMessage ="कृपया रू लाखमा लेख्नुहोस")]
        public decimal? TotalContractAmount { get; set; }
        [Range(0, 999999.99, ErrorMessage = "कृपया रू लाखमा लेख्नुहोस")]
        public decimal? TotalNikashaRamam { get; set; }

        public string ContractNoticeFile { get; set; }
        public string ContractFile { get; set; }
        public string BhuktaniFile { get; set; }
        public string RunningBillFile { get; set; }
        public string TimeExtendedFile { get; set; }
        public int? AppRunningStatus { get; set; }
        public string NotRunningProofDoc { get; set; }

        public int? ReportOfFisalYearEnd { get; set; }

        [NotMapped]
        public bool IsNikashaMaagBool { get; set; }

        [NotMapped]
        public HttpPostedFileBase NikasaMaagFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase PictureOfProjectThreeFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase PictureOfProjectTwoFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase PictureOfProjectOneFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase ProjectFileUploadFile { get; set; }

        [NotMapped]
        public int GrantTypeId { get; set; }
        public string PhaseTitle { get; set; }

        [NotMapped]
        public string ProgramPhaseTitle { get; set; }

        [NotMapped]
        public int? ApplicationProgressStatusId { get; set; }

        [NotMapped]
        public HttpPostedFileBase AppContractNoticeFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase AppcontractFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase AppBhuktaniVoucherFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase AppRunningBillsDetailFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase AppTimeExtendedFile { get; set; }
        [NotMapped]
        public HttpPostedFileBase AppNotRunningProofFile { get; set; }



    }

    public class FiscalYearViewModel
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearTitle { get; set; }
        public int ProgramId { get; set; }
        public int GrantTypeId { get; set; }
        public int ViewBagOfficeId { get; set; }

        public List<FiscalYearViewModel> FiscalYearViewModelList { get; set; }
    }

    public class RequestGrantAmountViewModel
    {
        public int SubProgramId { get; set; }
        public int OfficeId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int TimeDurationYear { get; set; }
        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public int PhaseStatus { get; set; }
        public string PhaseTitle { get; set; }
        public decimal RequestedAmount { get; set; }

        public string RequestReasonDoc { get; set; }
        public decimal OfficeAmount { get; set; }
        public string ProgramPhaseTitle { get; set; }



    }
    public class MakeApprovedFinalListViewModel
    {
        public int SubprogramId { get; set; }
        public decimal ApprovedAmount { get; set; }
        public int ApprovedStatus { get; set; }
    }

    public class YearlyWiseProgressDetailsListVM
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public int OfficeId { get; set; }
        public string FY207677 { get; set; }
        public string FY207778 { get; set; }
        public string FY207879 { get; set; }
        public string FY207980 { get; set; }
        public string FY208081 { get; set; }
        public int PhaseStatus { get; set; }
        public int TimeDurationYear { get; set; }
        public string GrantTypeName { get; set; }
        public int GrantTypeId { get; set; }

      



    }

    public class CanceledProgramListForRGVM
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public int TimeDurationYear { get; set; }
        public decimal TotalBudget { get; set; }
        public string GrantTypeName { get; set; }

    }

    public class CurrentYearGrantRequestProgramListVM
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public int TimeDurationYear { get; set; }
        public decimal TotalBudget { get; set; }
        public string GrantTypeName { get; set; }
        public decimal? AmountSecond { get; set; }

    }

    public class NewProgramInitiation
    {

        [Key]
        public int Id { get; set; }

        public int OfficeId { get; set; }

        public int GrantType { get; set; }

        public int TotalAppliedProject { get; set; }

        public int ProjectPhase { get; set; }

        public int FiscalYearId { get; set; }

        public string RequestFileDoc { get; set; }


        public int Status { get; set; }

        public string FinalFileDoc { get; set; }


        [NotMapped]
        public int ViewbagGrantTypeId { get; set; }

     
        public DateTime CreatedDate { get; set; }

    
        public int CreatedBy { get; set; }

     
        public DateTime? UpdatedDate { get; set; }

       
        public int UpdatedBy { get; set; }


    }

    public class ApplicationStatusModel
    {
        public int Status { get; set; }
        public string FinalFileDoc { get; set; }
    }
    public class AnusuchiViewModel
    {
        public string AyojanaName { get; set; }  // Title of the SubProgram
        public string OfficeName { get; set; }  // Office Name from OfficeDetails
        public string PhaseTypeName { get; set; }  // 'क्रमागत' or 'नया' based on PhaseStatus
        public int SubProgramID { get; set; }  // Unique ID of the SubProgram
        public int Priority { get; set; }  // Priority (0 for RequestGrant, others from ProgramPirority)

        public List<AnusuchiViewModel> AnusuchiData { get; set;}
    }


}