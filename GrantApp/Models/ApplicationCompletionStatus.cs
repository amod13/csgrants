using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class ApplicationCompletionStatus
    {
        [Key]

        public int ApplicationCompletionStatusId { get; set; }

        public int? ApplicationId { get; set; }
        public int? OfficeId { get; set; }

        public int? GrantTypeID { get; set; }


        public int? FiscalYearId { get; set; }

        [Display(Name = "आयोजनाको अवस्था")]
        [Required]
        public int? CompletionStatusId { get; set; }

        [Display(Name = "पूर्ण/आंशिक सम्पन्न")]
        [Required]
        public int? CompleteOrParitalCompleteStatusId { get; set; }




        public string Remarks { get; set; }

        public string UploadFileUrl { get; set; }

        public string PurnaAmsikSampanaMiti { get; set; }

        [Display(Name = "आयोजना सम्पन्न हुने नयाँ अवधि")]
       
        public int? YearNeededForCompleteId { get; set; }

        [Display(Name = "थप रकम आवश्यक भए (लाखमा)")]
        public decimal? AmountRequired { get; set; }

        
        public DateTime? CreatedDate { get; set; }

     
        public bool? Status { get; set; }

        
        public string WorkCompletionPhoto1 { get; set; }

        
        public string WorkCompletionPhoto2 { get; set; }
        public string NotCompletedFileTypeStr { get; set; }

        public string DroppedReason { get; set; }
        public string NotCompleteReason { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadFileUrlForCompletion { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadFileUrlForDroppedProjectFile { get; set; }

        [NotMapped]
        public HttpPostedFileBase NotCompletedFileTypeStrFile { get; set; }

    }


}