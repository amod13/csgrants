using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using GrantApp.Areas.Admin.Models;


namespace GrantApp
{
    public class CommonProvider
    {

        #region User List
        public List<UserLoginModelViewModel> PopulateRegisteredUserList()
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<UserLoginModelViewModel> RegisteredUserList = new List<UserLoginModelViewModel>();
                RegisteredUserList = db.Database.SqlQuery<UserLoginModelViewModel>("PopulateUserList").ToList();
                return RegisteredUserList;
            }
        }


        public int InsertUserRegistrationDetails(UsersModel _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var FullNameParam = new SqlParameter { ParameterName = "@FullName", Value = _Model.UserRegistration.FullName };
                var EmailParam = new SqlParameter { ParameterName = "@Email", Value = _Model.UserRegistration.Email };
                var AddressParam = new SqlParameter { ParameterName = "@Address", Value = _Model.UserRegistration.Address };
                var DateofBirthParam = new SqlParameter { ParameterName = "@DateofBirth", Value = _Model.UserRegistration.DateofBirth };
                var GenderIdParam = new SqlParameter { ParameterName = "@GenderId", Value = _Model.UserRegistration.GenderId };
                var PhoneNumberParam = new SqlParameter { ParameterName = "@PhoneNumber", Value = _Model.UserRegistration.PhoneNumber };
                var PhotoPathParam = new SqlParameter { ParameterName = "@PhotoPath", Value = _Model.UserRegistration.PhotoPath == null ? string.Empty : _Model.UserRegistration.PhotoPath };
                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = _Model.UserRegistration.Status ? false : _Model.UserRegistration.Status };
                var createdDateParam = new SqlParameter { ParameterName = "CreatedDate", Value = DateTime.Now };



                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var PrimaryIdParam = new SqlParameter
                {
                    ParameterName = "@PrimaryId",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec InsertUserRegistration @FullName,@Email,@Address,@DateofBirth,@GenderId,@PhoneNumber,@PhotoPath,@Status,@CreatedDate,@Message OUT,@PrimaryId OUT", FullNameParam, EmailParam, AddressParam, DateofBirthParam, GenderIdParam, PhoneNumberParam, PhotoPathParam, StatusParam, createdDateParam, MessageParam, PrimaryIdParam);
                int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                //msg = MessageParam.SqlValue.ToString();

                return UserRegistrationIdValue;

            }
        }



        public int InsertOfficeDetails(OfficeDetails _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                string Officename = CommontUtilities.GetOfficeNameRegisterProcess(_Model.UserType, _Model.ProvincesId, _Model.DistrictCode, _Model.VDCMUNCode);

                var OfficeNameParam = new SqlParameter { ParameterName = "@OfficeName", Value = Officename };
                var ProvincesIdParam = new SqlParameter { ParameterName = "@ProvincesId", Value = _Model.ProvincesId < 0 ? 0 : _Model.ProvincesId };
                var DistrictCodeParam = new SqlParameter { ParameterName = "@DistrictCode", Value = _Model.DistrictCode == null ? string.Empty : _Model.DistrictCode };
                var VDCMUNCodeParam = new SqlParameter { ParameterName = "@VDCMUNCode", Value = _Model.VDCMUNCode == null ? string.Empty : _Model.VDCMUNCode };
                var UserTypeParam = new SqlParameter { ParameterName = "@UserType", Value = _Model.UserType };
                var AddressParam = new SqlParameter { ParameterName = "@Address", Value = _Model.Address == null ? string.Empty : _Model.Address };
                var PhoneParam = new SqlParameter { ParameterName = "@Phone", Value = _Model.Phone == null ? string.Empty : _Model.Phone };
                var EmailIDParam = new SqlParameter { ParameterName = "@EmailID", Value = _Model.EmailID == null ? string.Empty : _Model.EmailID };
                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = _Model.Status ? false : _Model.Status };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var PrimaryIdParam = new SqlParameter
                {
                    ParameterName = "@PrimaryId",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec InsertOfficeDetails @OfficeName,@ProvincesId,@DistrictCode,@VDCMUNCode,@UserType,@Address,@Phone,@EmailID,@Status,@Message OUT,@PrimaryId OUT",
                    OfficeNameParam, ProvincesIdParam, DistrictCodeParam, VDCMUNCodeParam, UserTypeParam, AddressParam, PhoneParam, EmailIDParam, StatusParam, MessageParam, PrimaryIdParam);
                int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                //msg = MessageParam.SqlValue.ToString();

                return UserRegistrationIdValue;

            }
        }

        #endregion


        #region Change password

        public string ChangeUserPassword(string Username)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var UserNameParam = new SqlParameter { ParameterName = "@UserName", Value = Username.Trim() };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec ChangedUserPassword @UserName,@Message OUT",
                    UserNameParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;

            }
        }

        #endregion

        public List<AgencyDetailViewModel> SP_PopulateAgenciesList()
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AgencyDetailViewModel> RegisteredUserList = new List<AgencyDetailViewModel>();
                RegisteredUserList = db.Database.SqlQuery<AgencyDetailViewModel>("PopulateUserList").ToList();
                return RegisteredUserList;
            }
        }

        public int SP_InsertAgencyDetail(AgencyDetailViewModel _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var AgencyNameParam = new SqlParameter { ParameterName = "@AgencyName", Value = _Model.AgencyName == null ? string.Empty : _Model.AgencyName };
                var AgencyAddressParam = new SqlParameter { ParameterName = "@AgencyAddress", Value = _Model.AgencyAddress == null ? string.Empty : _Model.AgencyAddress };
                var AgencyEmailParam = new SqlParameter { ParameterName = "@AgencyEmail", Value = _Model.AgencyEmail == null ? string.Empty : _Model.AgencyEmail };
                var AgencyContactParam = new SqlParameter { ParameterName = "@AgencyContact", Value = _Model.AgencyContact == null ? string.Empty : _Model.AgencyContact };
                var AgencyStatusParam = new SqlParameter { ParameterName = "@AgencyStatus", Value = _Model.AgencyStatus };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = _Model.CreatedBy == null ? string.Empty : _Model.CreatedBy };
                var ContactPersonParam = new SqlParameter { ParameterName = "@ContactPerson", Value = _Model.ContactPerson == null ? string.Empty : _Model.ContactPerson };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var PrimaryIdParam = new SqlParameter
                {
                    ParameterName = "@PrimaryId",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec SP_InsertAgencyDetail @AgencyName,@AgencyAddress,@AgencyEmail,@AgencyContact,@AgencyStatus,@CreatedBy,@ContactPersons,@Message OUT,@PrimaryId OUT",
                    AgencyNameParam, AgencyAddressParam, AgencyEmailParam, AgencyContactParam, AgencyStatusParam, CreatedByParam, ContactPersonParam, MessageParam, PrimaryIdParam);
                int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                //msg = MessageParam.SqlValue.ToString();

                return UserRegistrationIdValue;

            }
        }

        public string SP_UpdateAgencyDetail(AgencyDetailViewModel _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var AgencyIdParamParam = new SqlParameter { ParameterName = "@AgencyId", Value = _Model.AgencyId };

                var AgencyNameParam = new SqlParameter { ParameterName = "@AgencyName", Value = _Model.AgencyName == null ? string.Empty : _Model.AgencyName };
                var AgencyAddressParam = new SqlParameter { ParameterName = "@AgencyAddress", Value = _Model.AgencyAddress == null ? string.Empty : _Model.AgencyAddress };
                var AgencyContactParam = new SqlParameter { ParameterName = "@AgencyContact", Value = _Model.AgencyContact == null ? string.Empty : _Model.AgencyContact };
                var AgencyStatusParam = new SqlParameter { ParameterName = "@AgencyStatus", Value = _Model.AgencyStatus };
                var ContactPersonParam = new SqlParameter { ParameterName = "@ContactPerson", Value = _Model.ContactPerson == null ? string.Empty : _Model.ContactPerson };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec SP_UpdateAgencyDetail @AgencyId,@AgencyName,@AgencyAddress,@AgencyContact,@AgencyStatus,@ContactPersons,@Message OUT",
                    AgencyNameParam, AgencyAddressParam, AgencyContactParam, AgencyStatusParam, ContactPersonParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();

                return msg;

            }
        }
        public string SP_DeleteAgencyDetail(int AgencyId)
        {
            string msg = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var AgencyIdParam = new SqlParameter { ParameterName = "@AgencyId", Value = AgencyId };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var result = db.Database.ExecuteSqlCommand("exec SP_DeleteAgencyDetail @AgencyId,@Message OUT",
                    AgencyIdParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();

                return msg;
            }


        }

        public List<GetSummaryMaxApprovedProgramWiseVM> GetSummaryMaxApprovedProgramWise(int? OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId.HasValue?OfficeId:0 };
                List<GetSummaryMaxApprovedProgramWiseVM> totalDetails = new List<GetSummaryMaxApprovedProgramWiseVM>();
                totalDetails = db.Database.SqlQuery<GetSummaryMaxApprovedProgramWiseVM>("GetSummaryMaxApprovedProgramWise @OfficeId", OfficeIdParam).ToList();
                return totalDetails;
            }
        }


        public List<ApplicationCompletionStatusVM> SPUP_GetApplicationCompletionStatus(int? CompletionStatusId, int? OfficeId, int? FiscalYearId,int ProvinceOrLocalLevelId,int? ProvinceId, int DistrictId,int VdcmunId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var CompletionStatusIdParam = new SqlParameter { ParameterName = "@CompletionStatusId", Value = CompletionStatusId.HasValue ? CompletionStatusId : 0 };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId.HasValue ? OfficeId : 0 };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId.HasValue ? FiscalYearId : 0 };
                var ProvinceOrLocalLevelParam = new SqlParameter { ParameterName = "@ProvinceOrLocalLevel", Value = ProvinceOrLocalLevelId };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId.HasValue?ProvinceId:0 };

                var DistrictIdSearchParam = new SqlParameter { ParameterName = "@DistrictIdSearch", Value = DistrictId };

                var VdcmunIdSearchParam = new SqlParameter { ParameterName = "@VdcmunIdSearch", Value = VdcmunId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@RetrunMessage",
                    DbType = DbType.String,
                    Size = 550,
                    Direction = System.Data.ParameterDirection.Output
                };

                List<ApplicationCompletionStatusVM> totalDetails = new List<ApplicationCompletionStatusVM>();
                totalDetails = db.Database.SqlQuery<ApplicationCompletionStatusVM>("SPUP_GetApplicationCompletionStatus @CompletionStatusId,@OfficeId,@FiscalYearId,@ProvinceOrLocalLevel,@ProvinceId,@DistrictIdSearch,@VdcmunIdSearch,@RetrunMessage OUT", CompletionStatusIdParam,OfficeIdParam,FiscalYearIdParam, ProvinceOrLocalLevelParam,ProvinceIdParam,DistrictIdSearchParam,VdcmunIdSearchParam, MessageParam).ToList();
                return totalDetails;
            }
        }
    }
}