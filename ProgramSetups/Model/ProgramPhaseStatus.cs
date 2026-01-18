using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSetups.Model
{
    public class ProgramPhaseStatus
    {
        
        [Key]
        public int ProgramPhaseStatausId { get; set; }
        public int PhaseNumber { get; set; }
        public bool PhaseStatus { get; set; }
        public int FiscalYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PhaseTitle { get; set; }


    }
}
