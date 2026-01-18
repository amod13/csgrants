using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class ProgramSetup
    {
        [Key]
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool Status { get; set; }
        public int GrantTypeId { get; set; }
        public int MainSectionId { get; set; }

        [NotMapped]
        public int ViewBagGrantTypeId { get; set; }
        [NotMapped]
        public List<ProgramSetup> ProgramSetupList { get; set; }
    }

    public class GrantTypeViewModel
    {
        public int GrantTypeId { get; set; }
        public string GrantTypeName { get; set; }
        public bool Status { get; set; }

        public List<GrantTypeViewModel> GrantTypeViewModelList { get; set; }
    }
}