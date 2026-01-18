using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class OfficeDetails
    {

        [Key]
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int ProvincesId { get; set; }
        public string DistrictCode { get; set; }
        public string VDCMUNCode { get; set; }
        public int UserType { get; set; }
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
       
        //[Range(8, 10, ErrorMessage = "Invalid Phone Number")]
        //[RegularExpression(@"^[0-9]$", ErrorMessage = "Invalid Phone Number")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Phone Number")]

        public string Phone { get; set; }

        public string EmailID { get; set; }
        public bool Status { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string AuthorizedEmail { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^[9][0-9]{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPost { get; set; }
        public string ContactPersonAccount { get; set; }
        [StringLength(10)]
        [RegularExpression(@"^[9][0-9]{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string ContactPersonMobileAccount { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]       
        public string ContactPersonEmailAccount { get; set; }
        public string UpMayerName { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^[9][0-9]{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string UpMayerMobile { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]        
        public string UpMayeraEmail { get; set; }
        [NotMapped]
        public int ViewbagGrantTypeId { get; set; }

     



    }

    public class OfficeDetailsForAdminViewModel
    {
        public int ProvinceIdSearch { get; set; }
        public int DistrictIdSearch { get; set; }
        public int VDCMUNIdSearch { get; set; }

        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int ProvincesId { get; set; }
        public string DistrictCode { get; set; }
        public string VDCMUNCode { get; set; }
        public int UserType { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmailID { get; set; }
        public bool Status { get; set; }
        public string AuthorizedEmail { get; set; }
        [StringLength(10)]
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPost { get; set; }
        public string ContactPersonAccount { get; set; }
        public string ContactPersonMobileAccount { get; set; }
        public string ContactPersonEmailAccount { get; set; }
        public string UpMayerName { get; set; }
        public string UpMayerMobile { get; set; }
        public string UpMayeraEmail { get; set; }

        public string ProvinceTitleEng { get; set; }
        public string DistrictNameEng { get; set; }
        public string VdcMunNameEng { get; set; }

        public List<OfficeDetailsForAdminViewModel> OfficeDetailsForAdminViewModelList { get; set; }


    }
}