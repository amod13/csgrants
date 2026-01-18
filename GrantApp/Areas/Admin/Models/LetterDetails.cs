using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Areas.Admin.Models
{
    public class LetterDetails
    {
        public int LetterDetailId { get; set; }
        public DateTime? LetterDate { get; set; }
        public string LetterDateEng { get; set; }
        public string LetterSubject { get; set; }
        public string LetterDescription { get; set; }
        public string UploadDoc { get; set; }
        public bool? LetterStatus { get; set; }

        [NotMapped]
        public HttpPostedFileBase LetterFileUploadFile { get; set; }

        public List<LetterDetails> LetterDetailsList { get; set; }
    }
}