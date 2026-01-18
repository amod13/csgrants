using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using GrantApp.Models;

namespace GrantApp.Services
{
    public class CommonServices
    {

        public string ValidateFiles(IEnumerable<HttpPostedFileBase> files)
        {
            int maxFileSize = 5 * 1024 * 1024; // 5MB
            string[] allowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png", ".docx", ".xlsx" };
            HashSet<string> uploadedFileNames = new HashSet<string>(); // To track duplicates

            foreach (var file in files)
            {
                if (file == null || file.ContentLength == 0)
                {
                    return "Invalid file: No file uploaded.";
                }

                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return $"Invalid file type: {file.FileName}. Allowed types: {string.Join(", ", allowedExtensions)}";
                }

                if (file.ContentLength > maxFileSize)
                {
                    return $"File size too large: {file.FileName} exceeds {maxFileSize / (1024 * 1024)}MB.";
                }

                if (uploadedFileNames.Contains(file.FileName))
                {
                    return $"Duplicate file detected: {file.FileName} is uploaded more than once.";
                }

                uploadedFileNames.Add(file.FileName); // Store filename to check duplicates
            }

            return "Valid"; // If everything is okay
        }



        public OfficeDetails GetOfficeDetails(int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                OfficeDetails returnModel = new OfficeDetails();
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                returnModel = db.Database.SqlQuery<OfficeDetails>("GetOfficeDetails @OfficeId", OfficeIdParam).FirstOrDefault();
                return returnModel;
            }
        }
        public string UpdateOfficeDetails(OfficeDetails _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = _Model.OfficeId };
                var AuthorizedEmailParam = new SqlParameter { ParameterName = "@AuthorizedEmail", Value = _Model.AuthorizedEmail==null?string.Empty:_Model.AuthorizedEmail };
                var MobileNumberParam = new SqlParameter { ParameterName = "@MobileNumber", Value = _Model.MobileNumber == null ? string.Empty : _Model.MobileNumber };
                var ContactNumberParam = new SqlParameter { ParameterName = "@ContactNumber", Value = _Model.Phone == null ? string.Empty : _Model.Phone };
                var ContactPersonParam = new SqlParameter { ParameterName = "@ContactPerson", Value = _Model.ContactPerson == null ? string.Empty : _Model.ContactPerson };
                var ContactPersonPostParam = new SqlParameter { ParameterName = "@ContactPersonPost", Value = _Model.ContactPersonPost == null ? string.Empty : _Model.ContactPersonPost };
                var ContactPersonAccountParam = new SqlParameter { ParameterName = "@ContactPersonAccount", Value = _Model.ContactPersonAccount == null ? string.Empty : _Model.ContactPersonAccount };
                var ContactPersonMobileAccountParam = new SqlParameter { ParameterName = "@ContactPersonMobileAccount", Value = _Model.ContactPersonMobileAccount == null ? string.Empty : _Model.ContactPersonMobileAccount };

                var ContactPersonEmailAccount = new SqlParameter { ParameterName = "@ContactPersonEmailAccount", Value = _Model.ContactPersonEmailAccount == null ? string.Empty : _Model.ContactPersonEmailAccount };
                var UpMayerNameParam = new SqlParameter { ParameterName = "@UpMayerName", Value = _Model.UpMayerName == null ? string.Empty : _Model.UpMayerName };
                var UpMayerMobileParam = new SqlParameter { ParameterName = "@UpMayerMobile", Value = _Model.UpMayerMobile == null ? string.Empty : _Model.UpMayerMobile };
                var UpMayeraEmailParam = new SqlParameter { ParameterName = "@UpMayeraEmail", Value = _Model.UpMayeraEmail == null ? string.Empty : _Model.UpMayeraEmail };

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


                var result = db.Database.ExecuteSqlCommand("exec UpdateOfficeDetails @OfficeId,@AuthorizedEmail,@MobileNumber,@ContactNumber,@ContactPerson,@ContactPersonPost,@ContactPersonAccount,@ContactPersonMobileAccount,@ContactPersonEmailAccount,@UpMayerName,@UpMayerMobile,@UpMayeraEmail,@Message OUT,@PrimaryId OUT",
                    OfficeIdParam, AuthorizedEmailParam, MobileNumberParam, ContactNumberParam,ContactPersonParam,ContactPersonPostParam,ContactPersonAccountParam,ContactPersonMobileAccountParam,ContactPersonEmailAccount,UpMayerNameParam,UpMayerMobileParam,UpMayeraEmailParam, MessageParam, PrimaryIdParam);
                int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;

            }
        }

        

        public bool CheckIfEmailIdExist(string EmailId, int OfficeId)
        {
            if(string.IsNullOrEmpty(EmailId))
            {
                return false;
            }
            EmailId = EmailId.Trim();
            int totalEmailInOffice = 0;
            int totalEmailInUsers = 0;
            int TotalEmail = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                totalEmailInOffice = db.Database.SqlQuery<int>("select count(*) total From OfficeDetails where AuthorizedEmail='" + EmailId + "' and OfficeId!='" + OfficeId + "'").FirstOrDefault();
                totalEmailInUsers = db.Database.SqlQuery<int>("select count(*) total From AspNetUsers where Email='" + EmailId + "' and OfficeId!='" + OfficeId + "'").FirstOrDefault();


            }
            TotalEmail = totalEmailInOffice + totalEmailInUsers;
            if(TotalEmail>0)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }
        public bool InsertEmailFailureDetails(string Email, string Message, string FromAction)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;


                var fromActionParam = new SqlParameter { ParameterName = "@FromAction", Value = FromAction };
                var emailIdParam = new SqlParameter { ParameterName = "@EmailId", Value = Email };
                var ErrorMesageParam = new SqlParameter { ParameterName = "@ErrroMessage", Value = Message };

                var result = db.Database.ExecuteSqlCommand("exec InsertFailureEmail @FromAction,@EmailId,@ErrroMessage",
                    FromAction, emailIdParam, ErrorMesageParam);

                //msg = MessageParam.SqlValue.ToString();

                return true;

            }
        }


        public bool SP_PrepareFinalListFromAdmin(int SubProgramId, decimal? ApprovedAmount, int? PhaseNumber,int RunningOrNewID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;


                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var ApprovedAmountParam = new SqlParameter { ParameterName = "@ApprovedAmount", Value = ApprovedAmount.HasValue?ApprovedAmount:0 };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var RunningOrNewParam = new SqlParameter { ParameterName = "@RunningOrNew", Value = RunningOrNewID };
                try
                {
                    var result = db.Database.ExecuteSqlCommand("exec SP_PrepareFinalListFromAdmin @SubProgramId,@ApprovedAmount,@PhaseNumber,@RunningOrNew",
                    SubProgramIdParam, ApprovedAmountParam, PhaseNumberParam,RunningOrNewParam);
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
                

                

            }
        }


        public bool SP_UpdateProgramSubmitStatusByAdmin(int SubProgramId,string UpdatedBy, string Remarks, int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;


                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = SubProgramId };
                var UpdatedByParam = new SqlParameter { ParameterName = "@UpdatedBy", Value =  UpdatedBy};
                var RemarksParam = new SqlParameter { ParameterName = "@Remarks", Value = Remarks };
                var PhaseStatusParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseNumber };
                try
                {
                    var result = db.Database.ExecuteSqlCommand("exec SP_UpdateProgramSubmitStatusByAdmin @SubProgramId,@UpdatedBy,@Remarks,@PhaseStatus",
                    SubProgramIdParam, UpdatedByParam,RemarksParam, PhaseStatusParam);
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }




            }
        }



    }



    //public int InsertJVOfficialDetail(JointVentureOfficeDetails _Model)
    //{
    //    using (TourismMGrantAppDb db = new TourismMGrantAppDb())
    //    {
    //        string msg = string.Empty;


    //        var LetterSubjectParam = new SqlParameter { ParameterName = "@LetterSubject", Value = _Model.LetterSubject == null ? string.Empty : _Model.LetterSubject };
    //        var LetterDateParam = new SqlParameter { ParameterName = "@LetterDate", Value = _Model.LetterDate == null ? string.Empty : _Model.LetterDate };
    //        var LetterBodyParam = new SqlParameter { ParameterName = "@LetterBody", Value = _Model.LetterBody == null ? string.Empty : _Model.LetterBody };
    //        var UploadDocumentsParam = new SqlParameter { ParameterName = "@UploadDocuments", Value = _Model.UploadDocuments == null ? string.Empty : _Model.UploadDocuments };

    //        var MessageParam = new SqlParameter
    //        {
    //            ParameterName = "@Message",
    //            DbType = DbType.String,
    //            Size = 50,
    //            Direction = System.Data.ParameterDirection.Output
    //        };

    //        var PrimaryIdParam = new SqlParameter
    //        {
    //            ParameterName = "@PrimaryId",
    //            DbType = DbType.Int32,
    //            Direction = System.Data.ParameterDirection.Output
    //        };


    //        var result = db.Database.ExecuteSqlCommand("exec InsertOfficialLetter @LetterSubject,@LetterDate,@LetterBody,@UploadDocuments,@Message OUT,@PrimaryId OUT",
    //            LetterSubjectParam, LetterDateParam, LetterBodyParam, UploadDocumentsParam, MessageParam, PrimaryIdParam);
    //        int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
    //        //msg = MessageParam.SqlValue.ToString();

    //        return UserRegistrationIdValue;

    //    }
    //}






    public class AppCodeViewModel
    {
        public int MasterId { get; set; }
        public int HeadId { get; set; }
        public int SubHeadId { get; set; }
    }



}