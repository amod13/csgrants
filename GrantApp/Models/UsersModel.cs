using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class UsersModel
    {

        public int UserType { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public int UserRoleId { get; set; }
        public UserRegistration UserRegistration { get; set; }
        public List<UserRegistration> UserRegistrationList { get; set; }
        public UserAction UserActionModel { get; set; }
        public List<UserAction> UserActionList { get; set; }
        public List<UserAction> UserControllerActionList { get; set; }


        public List<webAction> webActionList { get; set; }


        public UserLoginModelViewModel UserLoginModelViewModel { get; set; }
        public List<UserLoginModelViewModel> UserLoginModelListList { get; set; }
        public UsersModel()

        {
            UserRegistration = new UserRegistration();
            UserRegistrationList = new List<UserRegistration>();
            UserActionModel = new UserAction();
            UserControllerActionList = new List<Models.UserAction>();
            UserActionList = new List<Models.UserAction>();
            UserLoginModelViewModel = new UserLoginModelViewModel();
            UserLoginModelListList = new List<UserLoginModelViewModel>();



        }

    }

    public class UserRegistration
    {
        [Key]
        public int UserRegistrationId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateofBirth { get; set; }
        public int GenderId { get; set; }
        public string PhotoPath { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public System.Guid? UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

    public class UserLoginModelViewModel
    {
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        public int? ProvinceId { get; set; }
        public string DistrictCode { get; set; }
        public string VDCMUNCode { get; set; }
    }




    public class UserAction
    {
        public int SectionId { get; set; }
        public int ControllerId { get; set; }
        public int ActionId { get; set; }
        public string Label { get; set; }
        public bool CheckStatus { get; set; }
    }

    public class webAction
    {
        public int ActionId { get; set; }
        public int ControllerId { get; set; }
        public string ActionName { get; set; }
        public string DisplayTitle { get; set; }
        public bool Status { get; set; }
        public bool CheckStatus { get; set; }
    }

    public class AspNetCustomUserRoles
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserTypeId { get; set; }
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public int SectionId { get; set; }
        public bool Status { get; set; }
    }

    public class AgencyDetailViewModel
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string AgencyAddress { get; set; }
        public string AgencyEmail { get; set; }
        public string AgencyContact { get; set; }
        public bool? AgencyStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ContactPerson { get; set; }

        public List<AgencyDetailViewModel> AgencyDetailViewModelList { get; set; }
    }
}