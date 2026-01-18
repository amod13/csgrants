using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrantApp.Areas.Admin.Models
{
    public class ProfileUpdates
    {
        [Key]
        public int ProfileUpdateId { get; set; }


        public int? OfficeId { get; set; }



        public int? FiscalYearId { get; set; }



        public DateTime? UpdatedDate { get; set; }

    }

}