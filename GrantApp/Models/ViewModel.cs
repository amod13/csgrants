using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using iText.Forms.Form.Element;

namespace GrantApp.Models
{
    public class ViewModel
    {
    }

    public class GetNotCompletedProgramListByOfficeIdVM
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int? TimeDurationYear { get; set; }
        public int? YearNeededForCompleteId { get; set; }
        public decimal? AmountRequired { get; set; }
        public string GrantTypeName { get; set; }
        public int PhaseStatus { get; set; }
        public List<GetNotCompletedProgramListByOfficeIdVM> GetNotCompletedProgramListByOfficeIdVMList { get; set; }
        public GetNotCompletedProgramListByOfficeIdVM()
        {
            GetNotCompletedProgramListByOfficeIdVMList = new List<GetNotCompletedProgramListByOfficeIdVM>();
        }

    }

    public class ApplicationCompletionStatusVM
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public string GrantTypeName { get; set; }
        public int? CompleteOrParitalCompleteStatusId { get; set; }
        public string PhaseTitle { get; set; }
        public int? CompletionStatusId { get; set; }
        public string Remarks { get; set; }
        public string UploadFileUrl { get; set; }
        public int? YearNeededForCompleteId { get; set; }
        public decimal? AmountRequired { get; set; }
        public string DroppedReason { get; set; }
        public string ProvinceTitleNep { get; set; }


        public string DistrictNameNep { get; set; }
        public string OfficeName { get; set; }
        public string NotCompletedFileTypeStr { get; set; }
        public string WorkCompletionPhoto1 { get; set; }
        public decimal? TotalBudget { get; set; }

        public int ProvinceIdSearch { get; set; }
        public int DistrictIdSearch { get; set; }
        public int VdcmunIdSearch { get; set; }
        public int ViewBagGrantTypeId { get; set; }
        public int ViewFiscalYearId { get; set; }

        public List<ApplicationCompletionStatusVM> ApplicationCompletionStatusVMList { get; set; }
    }

    public class GetSubprogramListForApprovedActionVM
    {
        public int SubProgramId { get; set; }
        public string ProvinceTitleNep { get; set; }
        public string DistrictNameNep { get; set; }
        public string VdcMunNameNep { get; set; }
        public string PhaseTitle { get; set; }
        public string SubProgramTitle { get; set; }
        public int TimeDurationYear { get; set; }
        public int Status { get; set; }
        public bool ApprovedStatus { get; set; }
        public string GrantTypeName { get; set; }
        public int GrantTypeId { get; set; }

        public int ProvinceIdSearch { get; set; }
        public int DistrictIdSearch { get; set; }
        public int VdcmunIdSearch { get; set; }
        public int FiscalYearIdSearch { get; set; }
        public int GrantTypeIdSearch { get; set; }

        public List<GetSubprogramListForApprovedActionVM> GetSubprogramListForApprovedActionVMList { get; set; }




    }


    public class AdhuroApuroViewModel
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }
        public int? TimeDurationYear { get; set; }

        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public int PhaseStatus { get; set; }
        public List<AdhuroApuroViewModel> GetNotCompletedProgramListByOfficeIdVMList { get; set; }
        public AdhuroApuroViewModel()
        {
            GetNotCompletedProgramListByOfficeIdVMList = new List<AdhuroApuroViewModel>();
        }

    }

    public class PublicPrivateGrantViewModel
    {
        public int SubProgramId { get; set; }
        public string SubProgramTitle { get; set; }
        public decimal TotalBudget { get; set; }

        public decimal RequestedAmount { get; set; }
        public int? TimeDurationYear { get; set; }
        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public int PhaseStatus { get; set; }
        public List<PublicPrivateGrantViewModel> GetPublicPrivateGrantVMList { get; set; }
        public PublicPrivateGrantViewModel()
        {
            GetPublicPrivateGrantVMList = new List<PublicPrivateGrantViewModel>();
        }

    }

 

    public class AdhuroApuroGrantRequest
    {
            [Key]
        public int Id { get; set; }

        public int OfficeId { get; set; }

        public int ProgramId { get; set; }

        public int PhaseStatus { get; set; }
            public decimal AdditionalFundRequested { get; set; } // in lakh
            public decimal AmountCoveredByLocalLevel { get; set; } // in lakh
            public decimal PhysicalProgress { get; set; } // in percentage
            public decimal FinancialProgress { get; set; } // in percentage
            public decimal TotalAllocation { get; set; } // in lakh
            public decimal TotalExpenditure { get; set; } // in lakh

            // File Upload Fields
            public string ContractAgreementFilePath { get; set; }
            public string PaymentVoucherFilePath { get; set; }
            public string ProjectImagesFilePath { get; set; }
            public string ExtensionLetterFilePath { get; set; }
            public string CommitmentLetterFilePath { get; set; }
            public string ExecutiveDecisionFilePath { get; set; }

            public DateTime CreatedDate { get; set; } = DateTime.Now;
  

    }




    public class RemainingBhuktaniGrantRequest
    {

        [Key]
        public int Id { get; set; }

        public int OfficeId { get; set; }

        public int ProgramId { get; set; }

        public int PhaseStatus { get; set; }

        public decimal AdditionalAmountRequested { get; set; } // रु. लाखमा
        public decimal AmountToBeCoveredByLevel { get; set; } // रु. लाखमा
        public decimal PhysicalProgress { get; set; } // %
        public decimal FinancialProgress { get; set; } // %
        public decimal TotalAllocation { get; set; } // रु. लाखमा
        public decimal TotalExpenditure { get; set; } // रु. लाखमा

        public string ExtensionLetterPath { get; set; }
        public string ContractAgreementPath { get; set; }
        public string PaymentVouchersPath { get; set; }
        public string ProjectImagesPath { get; set; }
        public string LiabilityCertificationPath { get; set; }
        public string ExecutiveDecisionPath { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    //


    public class PublicPrivateGrantRequest
    {
        [Key]
        public int Id { get; set; }

        public int OfficeId { get; set; }
        public int PhaseStatus { get; set; }

        public int TimeDuration { get; set; }

        [Required]
        public string ProgramName { get; set; }  // आयोजना वा कार्यक्रमको नाम

        [Required]
        public string ProgramSector { get; set; }  // आयोजना वा कार्यक्रमको प्रमुख क्षेत्र

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total cost must be a positive number.")]
        public decimal TotalCost { get; set; }  // आयोजना वा कार्यक्रमको कूल लागत (रु. लाखमा)

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "State contribution must be a positive number.")]
        public decimal StateContribution { get; set; }  // प्रदेश वा स्थानीय तहले व्यहोर्ने रकम (रु. लाखमा)

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Private contribution must be a positive number.")]
        public decimal PrivateContribution { get; set; }  // निजी क्षेत्रले व्यहोर्ने रकम (रु. लाखमा)

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "VGF fund must be a positive number.")]
        public decimal VGFFund { get; set; }  // सम्भाव्यता न्यून परिपूरक कोष (VGF) रकम

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Requested amount must be a positive number.")]
        public decimal RequestedAmount { get; set; }  // संघ सरकारसँग माग गरिएको रकम

        public string FeasibilityStudy { get; set; }  // आयोजना सम्भाव्यता अध्ययन

        public string EnvironmentalReport { get; set; }  // वातावरणीय अध्ययन प्रतिवेदन

        public string PriorityDetails { get; set; }  // आयोजना प्राथमिकता सहितको विवरण

        public string LocalGovtDecision { get; set; }  // स्थानीय तह र प्रदेश सरकारका निर्णय

        public string DeclarationSchedule { get; set; }  // अनुसूची-२ बमोजिमको ढाँचामा शर्तनामा घोषणा

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    //



}