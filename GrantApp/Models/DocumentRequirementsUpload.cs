using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class DocumentRequirementsUpload
    {
        [Key]        
        public int DocumentRequirementsUploadId { get; set; }

       
        public int? SubprogramId { get; set; }

      
        public int? DocumentsRequirementsId { get; set; }

        
        public string UploadUrl { get; set; }

       
        public int? PhaseStatus { get; set; }

    }



    public class DocumentsRequirements
    {
        [Key]
        
        public int DocumentsRequirementsId { get; set; }

        
        public int? GrantTypeID { get; set; }

       
        public bool? IsForProvince { get; set; }

       
        public bool? IsForLocalLevel { get; set; }

       
        public string DocTitleFileStr { get; set; }

        [NotMapped]
        public List<DocumentsRequirements> DocumentsRequirementsList { get; set; }


    }

    public class DocumentsRequirementsViewModel
    {
        [Key]
        public int RequiredDocID { get; set; }
        public int GrantTypeId { get; set; }
        public string DocTitleFileStr { get; set; }
        public string UploadFileUrl { get; set; }

        public string UploaderName { get; set; }
        public string UploaderPosition { get; set; }
        public string FromMissing { get; set; }


        public bool FileExists { get; set; }
        public HttpPostedFileBase SupportingDocFiles { get; set; }
    }


    public class FileCheckResult
    {
        public int SubProgramId { get; set; }
        public string FileName { get; set; }
    }


}