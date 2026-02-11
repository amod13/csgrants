using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Web.Mvc;
using iText.StyledXmlParser.Jsoup.Select;
using static GrantApp.Models.SubProgramMaster;
using Microsoft.Ajax.Utilities;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;

namespace GrantApp
{
    public class CommontUtilities
    {

        //Get Total Program
        //Get Total Sub program

        public static int TotalProgramCount(int CurrentorPrevious)
        {
            int Total = 0;
            int Param = 0;
            int CurrentPhaseNumber = GetCurrentProgramPhaseNumber();
            int PreviousPhaseNumber = CurrentPhaseNumber - 1;
            if (CurrentorPrevious == 1)
            {
                Param = CurrentPhaseNumber;
            }
            else
            {
                Param = PreviousPhaseNumber;
            }
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.SubProgramMaster.Count(x => x.Status == 2 && x.PhaseStatus == Param);

                return Total;
            }

        }


        public static string TotalProgramCountStr(int CurrentorPrevious)
        {
            int Total = 0;
            int Param = 0;
            int CurrentPhaseNumber = GetCurrentProgramPhaseNumber();
            int PreviousPhaseNumber = CurrentPhaseNumber - 1;
            if (CurrentorPrevious == 1)
            {
                Param = CurrentPhaseNumber;
            }
            else
            {
                Param = PreviousPhaseNumber;
            }
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                //Total = db.SubProgramMaster.Count(x => x.Status == 2 && x.PhaseStatus == Param);
                Total = db.Database.SqlQuery<int>(@"select count(*) as Total From SubProgramMaster where Status=2 and PhaseStatus = @id", new SqlParameter("@id", Param)).FirstOrDefault();


                return Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string GetTotalProgramCountPhaseWise(int PhaseNumber)
        {
            int TotalProgram = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                TotalProgram = db.Database.SqlQuery<int>(@"select count(*) as Total From SubProgramMaster where Status=2 and PhaseStatus = @id", new SqlParameter("@id", PhaseNumber)).FirstOrDefault();


            }

            return Areas.Admin.FunctionClass.EnglishToNepaliNumber(TotalProgram.ToString());
        }

        public static string GetTotalProgramCountPhaseAndGrantTypeWise(int PhaseNumber, int GrantTypeId)
        {
            int TotalProgram = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                TotalProgram = db.Database.SqlQuery<int>(@"select count(*) as Total From SubProgramMaster where Status=2 and PhaseStatus='" + PhaseNumber + "' and GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();


            }

            return Areas.Admin.FunctionClass.EnglishToNepaliNumber(TotalProgram.ToString());
        }




        public static string TotalSpecialProgramCount(int CurrentorPrevious)
        {
            int Total = 0;
            int Param = 0;
            int CurrentPhaseNumber = GetCurrentProgramPhaseNumber();
            int PreviousPhaseNumber = CurrentPhaseNumber - 1;
            if (CurrentorPrevious == 1)
            {
                Param = CurrentPhaseNumber;
            }
            else
            {
                Param = PreviousPhaseNumber;
            }
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.SubProgramMaster.Count(x => x.GrantTypeId == 1 && x.Status == 2 && x.PhaseStatus == Param);

                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalComplementryProgramCount(int CurrentorPrevious)
        {
            int Total = 0;
            int Param = 0;
            int CurrentPhaseNumber = GetCurrentProgramPhaseNumber();
            int PreviousPhaseNumber = CurrentPhaseNumber - 1;
            if (CurrentorPrevious == 1)
            {
                Param = CurrentPhaseNumber;
            }
            else
            {
                Param = PreviousPhaseNumber;
            }
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.SubProgramMaster.Count(x => x.GrantTypeId == 2 && x.Status == 2 && x.PhaseStatus == Param);
                return Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalNewProgramOfficeTypeWise(int UserTypeId, int CurrentPhaseNumber)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) from subprogrammaster SPM
                    inner join OfficeDetails OD on SPM.OfficeId = OD.OfficeId
                    where SPM.PhaseStatus = '" + CurrentPhaseNumber + "' and SPM.Status = 2 AND OD.UserType= '" + UserTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalNewProgramBudgetOfficeTypeWise(int UserTypeId, int CurrentPhaseNumber)
        {
            decimal Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<decimal>(@"select isnull(sum(isnull(SPM.BudgetForFirstYear,0)),0) as TotalReqAmt from subprogrammaster SPM
                    inner join OfficeDetails OD on SPM.OfficeId = OD.OfficeId
                    where SPM.PhaseStatus = '" + CurrentPhaseNumber + "' and SPM.Status = 2 AND OD.UserType= '" + UserTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }


        public static string TotalProgramApprovedOfficeWise(int UserTypeId, int PhaseNumber)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) as TotalApproved From SubProgramMaster SPM
            inner join OfficeDetails OD on OD.OfficeId=SPM.OfficeId
            where SPM.ApprovedStatus=1 and SPM.PhaseStatus='" + PhaseNumber + "' and OD.UserType='" + UserTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalProgramApprovedOfficeGrantTypeWise(int UserTypeId, int PhaseNumber, int GrantTypeId)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) as TotalApproved From SubProgramMaster SPM
            inner join OfficeDetails OD on OD.OfficeId=SPM.OfficeId
            where SPM.ApprovedStatus=1 and SPM.PhaseStatus='" + PhaseNumber + "' and OD.UserType='" + UserTypeId + "' and SPM.GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }


        public static string TotalNewProgramBudgetOfficeTypeWise(int UserTypeId, int CurrentPhaseNumber, int GrantTypeId)
        {
            decimal Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<decimal>(@"select isnull(sum(isnull(SPM.BudgetForFirstYear,0)),0) as TotalReqAmt from subprogrammaster SPM
                    inner join OfficeDetails OD on SPM.OfficeId = OD.OfficeId
                    where SPM.PhaseStatus ='" + CurrentPhaseNumber + "' and SPM.Status = 2 AND OD.UserType='" + UserTypeId + "' and SPM.GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }



        public static string TotalNewProgramOfficeTypeWise(int UserTypeId, int CurrentPhaseNumber, int GrantTypeId)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) from subprogrammaster SPM
                    inner join OfficeDetails OD on SPM.OfficeId = OD.OfficeId
                    where SPM.PhaseStatus = '" + CurrentPhaseNumber + "' and SPM.Status = 2 AND OD.UserType= '" + UserTypeId + "' and SPM.GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalNewProgramOfficeTypeWiseRGA(int UserTypeId, int CurrentPhaseNumber, int RGAFYID)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select  count (*) as Total From RequestGrantAmount rga
                inner join OfficeDetails OD on OD.OfficeId=rga.OfficeId
                where rga.FiscalYearId='" + RGAFYID + "' and rga.AmountSecond>0 and OD.UserType='" + UserTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalNewProgramOfficeTypeWiseRGA(int UserTypeId, int CurrentPhaseNumber, int RGAFYID, int GrantTypeId)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select  count (*) as Total From RequestGrantAmount rga
                inner join OfficeDetails OD on OD.OfficeId=rga.OfficeId
				left join SubProgramMaster SPM on SPM.SubProgramId=rga.ProgramId
                where rga.FiscalYearId='" + RGAFYID + "' and rga.AmountSecond>0 and OD.UserType='" + UserTypeId + "' and SPM.GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalRequestAmountOfficeTypeWiseRGA(int UserTypeId, int CurrentPhaseNumber, int RGAFYID)
        {
            decimal Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<decimal>(@"select  isnull(sum(rga.AmountSecond),0) as Total From RequestGrantAmount rga
                inner join OfficeDetails OD on OD.OfficeId=rga.OfficeId				
                where rga.FiscalYearId='" + RGAFYID + "' and rga.AmountSecond>1 and OD.UserType='" + UserTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string TotalRequestAmountOfficeTypeWiseRGA(int UserTypeId, int CurrentPhaseNumber, int RGAFYID, int GrantTypeId)
        {
            decimal Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<decimal>(@"select  isnull(sum(rga.AmountSecond),0) as Total From RequestGrantAmount rga
                inner join SubProgramMaster spm on spm.SubProgramId=rga.ProgramId
                inner join OfficeDetails OD on OD.OfficeId=rga.OfficeId				
                where rga.FiscalYearId='" + RGAFYID + "' and rga.AmountSecond>1 and OD.UserType='" + UserTypeId + "' and spm.GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }


        public static int TotalRequestGrantAmountCount(int FiscalYearId)
        {
            int Total = 0;



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(*) as TotalRequestAmount From RequestGrantAmount where AmountSecond>1 and FiscalYearId='" + FiscalYearId + "'").FirstOrDefault();
                return Total;
            }

        }


        public static int TotalRequestGrantAmountCountByGrantTypeId(int FiscalYearId, int GrantTypeId)
        {
            int Total = 0;



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) as Total from RequestGrantAmount RGA
                                inner join SubProgramMaster SPM on SPM.SubProgramId = RGA.ProgramId
                                where SPM.ApprovedStatus = 1 AND RGA.AmountSecond>1 and RGA.FiscalYearId='" + FiscalYearId + "' and SPM.GrantTypeId = '" + GrantTypeId + "'").FirstOrDefault();
                return Total;
            }

        }
        public static string GetTotalProgramPreviousAndNew()
        {
            int TotalProgramCountVal = 0;
            int TotalNewProgram = TotalProgramCount(1);
            int TotalOldProgram = DashboradCountTotalRequestGrantAmountint(0, 0, 13);//amount request....fiscal yearid
            TotalProgramCountVal = TotalNewProgram + TotalOldProgram;

            return Areas.Admin.FunctionClass.EnglishToNepaliNumber(TotalProgramCountVal.ToString());
        }




        public static int TotalSpecialProgramCountByOfficeId(int OfficeId, int PhaseId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.SubProgramMaster.Count(x => x.GrantTypeId == 1 && x.Status == 2 && x.OfficeId == OfficeId && x.PhaseStatus == PhaseId);

                return Total;
            }

        }

        public static int TotalComplementryProgramCountByOfficeId(int OfficeId, int PhaseId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.SubProgramMaster.Count(x => x.GrantTypeId == 2 && x.Status == 2 && x.OfficeId == OfficeId && x.PhaseStatus == PhaseId);
                return Total;
            }

        }

        public static int TotalRequestAmountCountByOfficeId(int OfficeId, int RGAFYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) as Total From RequestGrantAmount
where AmountSecond>1 and officeid = '" + OfficeId + "' and FiscalYearId = '" + RGAFYID + "'").FirstOrDefault();
                return Total;
            }

        }



        public static int PreviousProgramCountByOfficeId(int OfficeId, int GrantTypeId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("Select count(*) as Total From SubProgramMaster where PhaseStatus in (1,2,3,4,5) and ApprovedStatus=1 and OfficeId='" + OfficeId + "' and GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                return Total;
            }

        }

        public static int PreviousComplementryProgramCountByOfficeId(int OfficeId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.SubProgramMaster.Count(x => x.GrantTypeId == 2 && x.Status == 2 && x.OfficeId == OfficeId && x.PhaseStatus == 2);
                return Total;
            }

        }








        public static int TotalUsers()
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.OfficeDetails.Count();
                return Total;
            }

        }


        public static int TotalActiveUsers(int CurrentOrPrevious)
        {
            int Total = 0;
            int Param = 0;
            int CurrnetPhaseNumber = GetCurrentProgramPhaseNumber();
            int PreviousPhaseNumber = CurrnetPhaseNumber - 1;
            if (CurrentOrPrevious == 1)//Current-1, previous -2
            {
                Param = CurrnetPhaseNumber;
            }
            else
            {
                Param = PreviousPhaseNumber;
            }


            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(distinct OfficeId) as TotalActiveUser From SubProgramMaster where PhaseStatus='" + Param + "'").FirstOrDefault();
                return Total;
            }

        }






        public static string GetProgramNameById(int ProgramId)
        {
            string ProgramName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Result = db.ProgramSetup.SingleOrDefault(x => x.ProgramId == ProgramId);
                if (Result != null)
                {
                    ProgramName = Result.ProgramName;
                }
                return ProgramName;
            }
        }

        public static string GetSubProgramNameById(int SubProgramId)
        {
            string SubProgramTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Result = db.SubProgramMaster.SingleOrDefault(x => x.SubProgramId == SubProgramId);
                if (Result != null)
                {
                    SubProgramTitle = Result.SubProgramTitle;
                }
                return SubProgramTitle;
            }
        }
        public static string GetSubProgramNameById(int? SubProgramId)
        {
            string SubProgramTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Result = db.SubProgramMaster.SingleOrDefault(x => x.SubProgramId == SubProgramId);
                if (Result != null)
                {
                    SubProgramTitle = Result.SubProgramTitle;
                }
                return SubProgramTitle;
            }
        }


        public static int GetTotalProgram(int GrantTypeId)
        {
            int TotalCount = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (GrantTypeId == 0)
                {
                    TotalCount = db.ProgramSetup.Count();
                }
                else
                {
                    TotalCount = db.ProgramSetup.Count(x => x.GrantTypeId == GrantTypeId);
                }

            }

            return TotalCount;
        }

        public static int GetTotalSubProgram(int GrantTypeId, int ProgramId)
        {
            int TotalCount = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (GrantTypeId == 0)
                {
                    if (ProgramId == 0)
                    {
                        TotalCount = db.SubProgramMaster.Count();
                    }
                    else
                    {
                        TotalCount = db.SubProgramMaster.Count(x => x.ProgramId == ProgramId);
                    }
                }
                else
                {
                    if (ProgramId == 0)
                    {
                        TotalCount = db.SubProgramMaster.Count(x => x.GrantTypeId == GrantTypeId);
                    }
                    else
                    {
                        TotalCount = db.SubProgramMaster.Count(x => x.ProgramId == ProgramId && x.GrantTypeId == GrantTypeId);
                    }
                }

            }


            return TotalCount;

        }


        public static string GetOfficeNameRegisterProcess(int UserType, int ProvincesId, string DistrictCode, string VDCMUNCode)
        {
            string OfficeNamestr = string.Empty;
            if (UserType == 1)
            {
                OfficeNamestr = "Admin";
            }
            else if (UserType == 2)
            {
                string ProvincesName = GrantApp.Areas.Admin.FunctionClass.GetProvinceNameByID(ProvincesId);
                OfficeNamestr = ProvincesName + " कार्यलय";
            }
            else if (UserType == 3)
            {
                // string ProvincesName = GrantApp.Areas.Admin.FunctionClass.GetProvinceNameByID(ProvincesId);
                string DistriceName = GrantApp.Areas.Admin.FunctionClass.GetDistrictNameByDistrctitCode(DistrictCode);
                //OfficeNamestr = ProvincesName + " ," + DistriceName + ", कार्यलय";
                OfficeNamestr = DistriceName + ", कार्यलय";
            }
            else if (UserType == 4)
            {
                //string ProvincesName = GrantApp.Areas.Admin.FunctionClass.GetProvinceNameByID(ProvincesId);
                //string DistriceName = GrantApp.Areas.Admin.FunctionClass.GetDistrictNameByDistrctitCode(DistrictCode);
                string VDCName = GrantApp.Areas.Admin.FunctionClass.GetVDCNPByVDCCode(VDCMUNCode);
                OfficeNamestr = VDCName + ",कार्यलय";

            }
            else
            {
                OfficeNamestr = "Super Admin";

            }

            return OfficeNamestr;


        }

        public static string GetMainSectionNameBySectionId(int MainSectionId)
        {
            string MainSectionName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

            }

            return MainSectionName;
        }

        public static int GetCurrentLoginUserUserTypeByOfficeId(int OfficeId)
        {
            int UserTypeId = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                UserTypeId = db.Database.SqlQuery<int>("select isnull(UserType,0) as UserTypeID From AspNetUsers where OfficeId=@id", new SqlParameter("@id", OfficeId))
                            .FirstOrDefault();


            }

            return UserTypeId;
        }


        public static int GetKramagatAppliedByGrantTypeId(int OfficeId, int GrantType)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                return db.Database.SqlQuery<int>(
                    @"SELECT COUNT(*) 
                      FROM dbo.RequestGrantAmount RG
                      INNER JOIN dbo.SubProgramMaster spm ON spm.SubProgramId = RG.ProgramId
                      WHERE spm.OfficeId = @OfficeId 
                      AND spm.GrantTypeId = @GrantType
                      AND spm.ApprovedStatus = 1 
                      AND spm.IsCancelled = 0 
                      AND RG.FiscalYearId = 18",
                    new SqlParameter("@OfficeId", OfficeId),
                    new SqlParameter("@GrantType", GrantType)
                ).FirstOrDefault();
            }
        }


        public static int GetWillApplyTotalForThisYear(int OfficeId, int GrantType)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                return db.Database.SqlQuery<int>(
                    @"SELECT TotalAppliedProject
                      FROM dbo.NewProgramInitiation NPI
                      WHERE NPI.OfficeId = @OfficeId 
                      AND NPI.GrantType = @GrantType
                      AND NPI.FiscalYearId = 18",
                    new SqlParameter("@OfficeId", OfficeId),
                    new SqlParameter("@GrantType", GrantType)
                ).FirstOrDefault();
            }
        }


        public static int TotalAppliedThisYear(int OfficeId, int GrantType)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                return db.Database.SqlQuery<int>(
                    @"SELECT Count(*)
                      FROM dbo.SubProgramMaster SPM
                      WHERE SPM.OfficeId = @OfficeId 
                      AND SPM.GrantTypeId = @GrantType
                      AND SPM.FiscalYearId = 18",
                    new SqlParameter("@OfficeId", OfficeId),
                    new SqlParameter("@GrantType", GrantType)
                ).FirstOrDefault();
            }
        }

        public static int MaxAllowedThisyear(int officeId, int grantType)
        {
         
            int officeTypeId = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(officeId);
            int maxForOffice = 0;
            int kramagatApplied = CommontUtilities.GetKramagatAppliedByGrantTypeId(officeId, grantType);
            if (officeTypeId == 4) { maxForOffice = 3; }
            if (officeTypeId == 3) { maxForOffice = 10; }
            int maxAllowed = Math.Max(maxForOffice - kramagatApplied, 0);
            return maxAllowed;
        }



        public static int RemainingNewApplicationThisyear(int officeId, int grantType)
        {


            int reminingProject = 0;
            int WillApplyTotal = CommontUtilities.GetWillApplyTotalForThisYear(officeId, grantType) ;
            int NewApplicationCount = CommontUtilities.TotalAppliedThisYear(officeId, grantType);
            reminingProject = Math.Max(WillApplyTotal - NewApplicationCount, 0);
            return reminingProject;
        }




        public static string GetUserTypeNameFromUserTypeId(int UserTypeId)
        {
            string UserTypeName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                UserTypeName = db.Database.SqlQuery<string>("select isnull(UserType,0) as UserTypeID From AspNetUsers where OfficeId=@id", new SqlParameter("@id", UserTypeId))
                            .FirstOrDefault();


            }

            return UserTypeName;
        }
        public static string GetUserTypeByUserTypeId(int UserTypeId)
        {
            string UserTypeName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                UserTypeName = db.Database.SqlQuery<string>("select isnull(UserTypeName,0) as UserTypeID From UserType where UserTypeId=@id", new SqlParameter("@id", UserTypeId))
                            .FirstOrDefault();


            }

            return UserTypeName;
        }


        public static string GetOfficeNameByOfficeId()
        {
            string OfficeName = string.Empty;
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                OfficeName = db.Database.SqlQuery<string>("select OfficeName From OfficeDetails where OfficeId=@id", new SqlParameter("@id", OfficeId))
                            .FirstOrDefault();


            }

            return OfficeName;
        }


        public static string GetOfficeAddressNameByOfficeId()
        {
            string OfficeName = string.Empty;
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                OfficeName = db.Database.SqlQuery<string>("select OfficeName From OfficeDetails where OfficeId=@id", new SqlParameter("@id", OfficeId))
                            .FirstOrDefault();


            }

            return OfficeName;
        }


        public static string GetUserTypeNameByID(int UserType)
        {
            string UsertypeName = string.Empty;
            if (UserType == 1)
            {
                UsertypeName = "सुपर एडमिन";
            }
            else if (UserType == 2)
            {

                UsertypeName = "प्रदेश";
            }
            else if (UserType == 3)
            {
                UsertypeName = "जिल्ला";
            }
            else if (UserType == 4)
            {
                UsertypeName = "गापा/नपा";

            }
            else
            {
                UsertypeName = "";

            }

            return UsertypeName;


        }


        public static IEnumerable<SelectListItem> CountVariableListInReportDD()
        {
            return new SelectList(new[]
            {
                new {Id="0",Value="--"},
                new {Id="1",Value="१"},
                new {Id="2",Value="२"},
                new {Id="3",Value="३"},
                new {Id="4",Value="३ भन्दा बढि"},
            }, "Id", "Value");

        }

        public static IEnumerable<SelectListItem> SubmitedOrNotSubmited()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="पठाउने"},
                new {Id="2",Value="नपठाउने"},

            }, "Id", "Value");

        }


        public static IEnumerable<SelectListItem> YesNoUploadFileDetails()
        {
            return new SelectList(new[]
            {   new {Id="0",Value="--छान्नुहोस--"},
                new {Id="1",Value="अपलोड भएको"},
                new {Id="2",Value="अपलोड नभएको"},

            }, "Id", "Value");

        }

        public static IEnumerable<SelectListItem> TotalBudgetSearchDD()
        {
            return new SelectList(new[]
            {
                new {Id="0",Value="--छान्नुहोस--"},
                new {Id="1",Value="५० % वा भन्दा धेरै"},
                new {Id="2",Value="५० % भन्दा कम"},

            }, "Id", "Value");

        }



        public static IEnumerable<SelectListItem> ProgressReportDurationDD()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="प्रथम चौमासिक"},
                new {Id="2",Value="दोस्रो चौमासिक"},
                new {Id="3",Value="तेस्रो चौमासिक"},

            }, "Id", "Value");

        }

        public static string GetProgressReportDurationById(int DurationId)
        {
            string DurationName = string.Empty;
            if (DurationId == 1)
            {
                DurationName = "प्रथम चौमासिक";
            }
            else if (DurationId == 2)
            {

                DurationName = "दोस्रो चौमासिक";
            }
            else if (DurationId == 3)
            {
                DurationName = "तेस्रो चौमासिक";
            }

            else
            {
                DurationName = "";

            }

            return DurationName;


        }



        //Program Phase Status Detail function
        public static int GetCurrentProgramPhaseNumber()
        {
            int PhaseNumber = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                PhaseNumber = db.Database.SqlQuery<int>("select PhaseNumber as CurrnetPhaseNumber From ProgramPhaseStatus where PhaseStatus=1")
                            .FirstOrDefault();

            }

            return PhaseNumber;
        }

        public static DateTime GetCurrentProgramPhaseEndDate()
        {
            DateTime PhaseEndDate = DateTime.Now;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                PhaseEndDate = db.Database.SqlQuery<DateTime>("select EndDate as PhaseEndDate From ProgramPhaseStatus where PhaseStatus=1")
                            .FirstOrDefault();

            }

            return PhaseEndDate;
        }



        //Progresss report dd or check list
        public static IEnumerable<SelectListItem> ProjectContractStatusDD()
        {
            return new SelectList(new[]
            {   new {Id="0",Value="--छान्नुहोस--"},
                new {Id="1",Value="ठेक्का सूचना जारी भएको"},
                new {Id="2",Value="ठेक्का सम्झैाता सम्पन्न भएको"},
                new {Id="3",Value="पहिलो किस्ता भुक्तानि भएको"},

            }, "Id", "Value");

        }



        #region Phase wise report-fiscal year wise

        public static int GetCurrentFiscalYearId()
        {
            int CurrentFyId = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                CurrentFyId = db.Database.SqlQuery<int>("Select FiscalYearRecordId From FiscalYearRecords where isActive=1")
                            .FirstOrDefault();
            }
            return CurrentFyId;
        }

        public static decimal GetNextYearRequestGrantAmount(int FYID, int OfficeId, int ProgramID)
        {
            decimal requestAmount = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                requestAmount = db.Database.SqlQuery<decimal>(@"select isnull(AmountSecond,0) as RequestedAmount From RequestGrantAmount
                                where OfficeId='" + OfficeId + "' and FiscalYearId='" + FYID + "' and ProgramId='" + ProgramID + "'")
                            .FirstOrDefault();
            }
            return requestAmount;
        }



        public static string GetFiscalYearTitelByFID(int FyId)
        {
            string FyTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                FyTitle = db.Database.SqlQuery<string>("Select Title as FiscalYearTitle From FiscalYearRecords where FiscalYearRecordId='" + FyId + "'")
                            .FirstOrDefault();
            }
            return FyTitle;
        }
        public static string GetFiscalYearTitelByFID(int? FyId)
        {
            string FyTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                FyTitle = db.Database.SqlQuery<string>("Select Title as FiscalYearTitle From FiscalYearRecords where FiscalYearRecordId='" + FyId + "'")
                            .FirstOrDefault();
            }
            return FyTitle;
        }

        public static int GetPreviousFiscalYearId()
        {
            int CurrentFyId = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                CurrentFyId = db.Database.SqlQuery<int>("Select PreviousFiscalYearRecordId From FiscalYearRecords where isActive=1")
                            .FirstOrDefault();
            }
            return CurrentFyId;
        }




        #endregion



        //Changed dec 14 2020


        public static int TotalApprovedMultiYearProgram(int OfficeId)
        {
            int Total = 0;



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(*) from SubProgramMaster where ApprovedStatus=1 and TimeDurationYear>=2 and IsCancelled=0 and PhaseStatus in (1,2) and OfficeId='" + OfficeId + "'").FirstOrDefault();
                return Total;
            }

        }


        public static int TotalApprovedMultiYearProgramByGrantType(int OfficeId, int GrantTypeId)
        {
            int TotalOld = 0;
            int TotalNew = 0;
            int GrantTotal = 0;



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                TotalOld = db.Database.SqlQuery<int>("select count(*) from SubProgramMaster where ApprovedStatus=1 and TimeDurationYear>2 and IsCancelled=0 and PhaseStatus in (4) and GrantTypeId='" + GrantTypeId + "' and OfficeId='" + OfficeId + "'").FirstOrDefault();
                TotalNew = db.Database.SqlQuery<int>("select count(*) from SubProgramMaster where ApprovedStatus=1 and TimeDurationYear>=2 and IsCancelled=0 and PhaseStatus in (5) and GrantTypeId='" + GrantTypeId + "' and OfficeId='" + OfficeId + "'").FirstOrDefault();
                GrantTotal = TotalOld + TotalNew;
                return GrantTotal;
            }

        }

        public static int TotalApprovedMultiYearProgramByGrantTypeForRPT(int OfficeId, int GrantTypeId)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(*) from SubProgramMaster where ApprovedStatus=1 and TimeDurationYear>=2 and IsCancelled=0 and PhaseStatus in (3) and GrantTypeId='" + GrantTypeId + "' and OfficeId='" + OfficeId + "'").FirstOrDefault();
                return Total;
            }

        }



        public static int TotalApprovedMultiYearProgramByGrantTypeForProRpt(int OfficeId, int GrantTypeId)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(*) from SubProgramMaster where ApprovedStatus=1 and TimeDurationYear>=1 and IsCancelled=0 and PhaseStatus in (4) and GrantTypeId='" + GrantTypeId + "' and OfficeId='" + OfficeId + "'").FirstOrDefault();
                return Total;
            }
        }



        public static decimal GetProgramWiseAmountByProgramId(int ProgramId, int InstallmentAmount)
        {
            decimal Total = 0;


            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (InstallmentAmount == 1)
                {
                    Total = db.Database.SqlQuery<decimal>("select ISNULL(Amount,0) as FirstInstallment From ProgramwiseAmount where ProgramId='" + ProgramId + "'").FirstOrDefault();

                }

                else if (InstallmentAmount == 2)
                {
                    Total = db.Database.SqlQuery<decimal>("select isnull(AmountSecondYear,0) as SecondInstallment From ProgramwiseAmount where ProgramId='" + ProgramId + "'").FirstOrDefault();
                }
                else
                {
                    Total = db.Database.SqlQuery<decimal>("select isnull(AmountSecondYear,0) as SecondInstallment From ProgramwiseAmount where ProgramId='" + ProgramId + "'").FirstOrDefault();
                }
                return Total;
            }

        }
        public static int TotalRequestedGrantAmountByOffice(int OfficeId, int GrantTypeId)
        {
            int Total = 0;

            //using (GrantAppDBEntities db = new GrantAppDBEntities())
            //{
            //    Total = db.Database.SqlQuery<int>(@"select count(distinct RGA.ProgramId) as Total from RequestGrantAmount RGA
            //    inner join SubProgramMaster SPM on SPM.SubProgramId = RGA.ProgramId
            //    where RGA.AmountSecond > 0 and RGA.FiscalYearId in (11,13,14) and RGA.OfficeId = '" + OfficeId + "' and SPM.GrantTypeId = '" + GrantTypeId + "'").FirstOrDefault();
            //    return Total;
            //}

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct RGA.ProgramId) as Total from RequestGrantAmount RGA
                inner join SubProgramMaster SPM on SPM.SubProgramId = RGA.ProgramId
                where RGA.AmountSecond > 0 and RGA.FiscalYearId in (16) and RGA.OfficeId = '" + OfficeId + "' and SPM.GrantTypeId = '" + GrantTypeId + "'").FirstOrDefault();
                return Total;
            }

        }

        public static decimal GetSumProgramWiseAmount(int ProgramId)
        {
            decimal Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<decimal>("select isnull(sum(isnull(Amount,0)+isnull(AmountSecondYear,0)),0)as TotalGrantedAmount From ProgramwiseAmount where ProgramId='" + ProgramId + "'").FirstOrDefault();
                return Total;
            }

        }

        public static int TotalSubmittedProgressReportByOffice(int OfficeId)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(distinct ProgramId) as Total from QuadrimesterReportsDetail where FiscalYearId in (1,11) and QuadrimesterId=1 and OfficeId='" + OfficeId + "'").FirstOrDefault();
                return Total;
            }

        }

        public static int TotalSubmittedProgressReportByOffice(int OfficeId, int grantTypeId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct QRD.ProgramId) as Total from QuadrimesterReportsDetail  QRD
                inner join SubProgramMaster SPM on SPM.SubProgramId = QRD.ProgramId
                where QRD.FiscalYearId in (13,14) and QRD.QuadrimesterId = 1 and SPM.GrantTypeId = '" + grantTypeId + "' and QRD.OfficeId = '" + OfficeId + "'").FirstOrDefault();
                return Total;
            }

        }

        //Get total year to be submited and count total year inserted......
        public static int TotalSubmitedProgressReportyBYFYID(int OfficeId, int grantTypeId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct QRD.ProgramId) as Total from QuadrimesterReportsDetail  QRD
                inner join SubProgramMaster SPM on SPM.SubProgramId = QRD.ProgramId
                where QRD.FiscalYearId in (13,14) and QRD.QuadrimesterId = 1 and SPM.GrantTypeId = '" + grantTypeId + "' and QRD.OfficeId = '" + OfficeId + "'").FirstOrDefault();
                return Total;
            }

        }











        public static string GetPhaseTitleFromPhaseNumber(int? PhaseNumber)
        {
            string PhaseTitle = "";

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                PhaseTitle = db.Database.SqlQuery<string>("Select PhaseTitle From ProgramPhaseStatus where PhaseNumber= '" + PhaseNumber + "'").FirstOrDefault();
                return PhaseTitle;
            }

        }

        public static string GetAnusuchiTwoExtraData(int SubProgramId, int ConditionId)
        {
            string strVal = string.Empty;
            //22 26 30 31 32

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                strVal = db.Database.SqlQuery<string>("select UploadFileUrl From ProgramConditionsDetail where SubProgramId='" + SubProgramId + "' and ProgramConditionId='" + ConditionId + "'").FirstOrDefault();
                if (string.IsNullOrEmpty(strVal))
                {
                    return "छैन";
                }
                else
                {
                    return "छ";
                }
            }

        }

        public static string GetOfficeAuthorizedEmailId(int OfficeId)
        {
            string strVal = string.Empty;
            //22 26 30 31 32

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                strVal = db.Database.SqlQuery<string>("select AuthorizedEmail from OfficeDetails where OfficeId='" + OfficeId + "'").FirstOrDefault();
                if (string.IsNullOrEmpty(strVal))
                {
                    return string.Empty;
                }
                else
                {
                    return strVal;
                }
            }

        }

        public static string SendSubmittedMessage(string Email, string GrantTypeName, int SubprogramId)
        {
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                    new System.Net.Mail.MailAddress("npcstartusnepal@gmail.com", "msgrant.gov.np"),
                    new System.Net.Mail.MailAddress(Email));
            m.Subject = "Application Submitted Successfully...";
            m.Body = @"Dear '" + Email + "' ,";
            m.Body += System.Environment.NewLine;
            m.Body += string.Format("Application Details :" + GrantTypeName + " - " + SubprogramId);
            m.Body += string.Format(". Your Application has been successfully Submitted in NCP System.Thank you.");
            m.Body += System.Environment.NewLine;
            m.IsBodyHtml = true;
            m.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //smtp.Credentials = new System.Net.NetworkCredential("startupsnpc@gmail.com", "Ncp@123nepal");
            //smtp.Credentials = new System.Net.NetworkCredential("startupsnpcnepal@gmail.com", "Visit@neP12l");
            smtp.Credentials = new System.Net.NetworkCredential("npcstartusnepal@gmail.com", "Npc@start@12pal");

            try
            {
                smtp.Send(m);
                return "Email Send Successfully";
            }
            catch (Exception)
            {

                //services.InsertFailureEmail(model.Email);
                return "Email Failure";
            }
        }

        public static string SendSubmittedMessage(string Email)
        {
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                    new System.Net.Mail.MailAddress("npcstartusnepal@gmail.com", "msgrant.gov.np"),
                    new System.Net.Mail.MailAddress(Email));
            m.Subject = "Application Submitted Successfully...";
            m.Body = @"Dear '" + Email + "' ,";
            m.Body += System.Environment.NewLine;
            m.Body += string.Format("Your Application has been successfully Submitted in NCP System.Thank you.");
            m.Body += System.Environment.NewLine;
            m.IsBodyHtml = true;
            m.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //smtp.Credentials = new System.Net.NetworkCredential("startupsnpc@gmail.com", "Ncp@123nepal");
            //smtp.Credentials = new System.Net.NetworkCredential("startupsnpcnepal@gmail.com", "Visit@neP12l");
            smtp.Credentials = new System.Net.NetworkCredential("npcstartusnepal@gmail.com", "Npc@start@12pal");

            try
            {
                smtp.Send(m);
                return "Email Send Successfully";
            }
            catch (Exception)
            {

                //services.InsertFailureEmail(model.Email);
                return "Email Failure";
            }
        }

        public static string DashboradCountTotalRequestGrantAmount(int GrantTypeId, int PhaseId, int FicalYearId)
        {
            int TotalApprovedProgram = 0;
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int PrePhaseNumber = CurrentPhaseNumber = 1;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseIdParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PrePhaseNumber };
                var FicalYearIdParam = new SqlParameter { ParameterName = "@FicalYearId", Value = FicalYearId };
                TotalApprovedProgram = db.Database.SqlQuery<int>("DashboradCountTotalRequestGrantAmount @GrantTypeId,@PhaseStatus,@FicalYearId", GrantTypeIdParam, PhaseIdParam, FicalYearIdParam).FirstOrDefault();

            }
            return Areas.Admin.FunctionClass.EnglishToNepaliNumber(TotalApprovedProgram.ToString());
        }

        public static int DashboradCountTotalRequestGrantAmountint(int GrantTypeId, int PhaseId, int FicalYearId)
        {
            int TotalApprovedProgram = 0;
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int PrePhaseNumber = CurrentPhaseNumber = 1;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseIdParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PrePhaseNumber };
                var FicalYearIdParam = new SqlParameter { ParameterName = "@FicalYearId", Value = FicalYearId };
                TotalApprovedProgram = db.Database.SqlQuery<int>("DashboradCountTotalRequestGrantAmount @GrantTypeId,@PhaseStatus,@FicalYearId", GrantTypeIdParam, PhaseIdParam, FicalYearIdParam).FirstOrDefault();

            }
            return TotalApprovedProgram;
        }


        public static string SP_GetAuthorizedEmailOfOffice(string UserId)
        {
            string AuthorizedEmail = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var UserIdParam = new SqlParameter { ParameterName = "@UserId", Value = UserId };
                AuthorizedEmail = db.Database.SqlQuery<string>("SP_GetAuthorizedEmailOfOffice @UserId", UserIdParam).FirstOrDefault();
            }
            return AuthorizedEmail;
        }

        public static string GetOrganizationName()
        {
            string OrganizationName = "";

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                OrganizationName = db.Database.SqlQuery<string>("select OrganizationName From OrganizationDetail").FirstOrDefault();
                return OrganizationName;
            }

        }

        public static string GetOrganizationHeading()
        {
            string OrganizationName = "";

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                OrganizationName = db.Database.SqlQuery<string>("select OrganiztionHeading From OrganizationDetail").FirstOrDefault();
                return OrganizationName;
            }

        }

        public static string GetOrganizationCopyRight()
        {
            string OrganizationName = "";

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                OrganizationName = db.Database.SqlQuery<string>("select OrganizationCopyRight From OrganizationDetail").FirstOrDefault();
                return OrganizationName;
            }

        }

        public static string GetOrganizationAddress()
        {
            string OrganizationName = "";

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                OrganizationName = db.Database.SqlQuery<string>("select OrganizationAddress From OrganizationDetail").FirstOrDefault();
                return OrganizationName;
            }

        }


        public static int GetTotalLetterCount()
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(*) totalLetter From LetterDetail").FirstOrDefault();
                return Total;
            }

        }

        public static int GetPhaseNumberFromProgramId(int programId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select isnull(PhaseStatus,0) as PhaseNumber From SubProgramMaster where SubProgramId='" + programId + "'").FirstOrDefault();
                return Total;
            }

        }


        public static int GetTotalProgressRptSubmitedInUserDashboard(int OfficeId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>("select count(*) as totalSubmited From QuadrimesterReportsDetail where OfficeId='" + OfficeId + "'").FirstOrDefault();
                return Total;
            }

        }

        public static GrantGroupInfo GetGrantGroupInfo(int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var result = db.Database.SqlQuery<GrantGroupInfo>(
                    "SELECT gg.ContributionPercent, gg.name FROM dbo.OfficeDetails od " +
                    "INNER JOIN dbo.GrantGroup gg ON gg.GroupId = od.GroupId " +
                    "WHERE od.OfficeId = @p0", OfficeId).FirstOrDefault();

                return result ?? new GrantGroupInfo { Name = "N/A", ContributionPercent = 0 };
            }
        }

        public static string TotalRequestAmountGrantTypeWise(int RGAFYID, int GrantTypeID)
        {
            int Total = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@" select count(*) as Total From RequestGrantAmount RGA
                inner join SubProgramMaster SPM on SPM.SubProgramId=RGA.ProgramId
                where RGA.FiscalYearId='" + RGAFYID + "' and SPM.GrantTypeId='" + GrantTypeID + "' and RGA.AmountSecond>1").FirstOrDefault();
                return GrantApp.Areas.Admin.FunctionClass.EnglishToNepaliNumber(Total.ToString());
            }

        }

        public static string GetProgressReportClassName(int ProgramId, int FYID, int ReportedOnFiscalYearEnd)
        {
            string className = "edits";
            //color: #4cb748;
            int Total = 0;
            //int totalRecordsRequired = CommontUtilities.GetTotalReportRequiredByProgramId(ProgramId);
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                Total = db.Database.SqlQuery<int>(
                            @"SELECT COUNT(*) 
                              FROM QuadrimesterReportsDetail 
                              WHERE ProgramId = @ProgramId 
                                AND FiscalYearId = @FYID 
                                AND QuadrimesterId = 0 
                                AND ReportOfFisalYearEnd = @ReportedonEnd",
                            new SqlParameter("@ProgramId", ProgramId),
                            new SqlParameter("@FYID", FYID),
                            new SqlParameter("@ReportedonEnd", ReportedOnFiscalYearEnd)
                        ).FirstOrDefault();

            }
            if (Total > 0)
            {
                className = "Update";
            }
            return className;
        }


        public static int GetProgressReportId(int ProgramId, int FYID, int ReportedOnFiscalYearEnd)
        {
            //string className = "edits";
            //color: #4cb748;
            int Total = 0;
            //int totalRecordsRequired = CommontUtilities.GetTotalReportRequiredByProgramId(ProgramId);
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                Total = db.Database.SqlQuery<int>(
                            @"SELECT QuadrimesterReportsDetailId
                              FROM QuadrimesterReportsDetail 
                              WHERE ProgramId = @ProgramId 
                                AND FiscalYearId = @FYID 
                                AND QuadrimesterId = 0 
                                AND ReportOfFisalYearEnd = @ReportedonEnd",
                            new SqlParameter("@ProgramId", ProgramId),
                            new SqlParameter("@FYID", FYID),
                            new SqlParameter("@ReportedonEnd", ReportedOnFiscalYearEnd)
                        ).FirstOrDefault();

            }
         
            return Total;
        }



        public static string GetFinalReportClassName(int ProgramId)
        {
            string className = "edits";
            //color: #4cb748;
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@" select COUNT(*) as Total From ApplicationCompletionStatus
                            where ApplicationId='" + ProgramId +"'").FirstOrDefault();


            }

            if (Total > 0)
            {
                className = "Update";
            }
            return className;
        }

        public static bool ShowHideApplicationCompletionStatus(int ProgramId, int TimeDuration)
        {
            bool returnVal = false;
            int currentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int CheckCondtionForCurrentPhase = currentPhaseNumber-1;
            int GetphaseNumberFromProgramId = GetPhaseNumberFromProgramId(ProgramId);
            int CheckCondtionGreaterThan2years = currentPhaseNumber - 2;
            int CheckCondtionGreaterThan3years = currentPhaseNumber - 3;




            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@" select COUNT(distinct FiscalYearId) as Total From QuadrimesterReportsDetail
                where ProgramId='" + ProgramId + "' and QuadrimesterId=0").FirstOrDefault();


            }

            if (TimeDuration == Total)
            {
                returnVal = true;
            }

            if (GetphaseNumberFromProgramId == CheckCondtionForCurrentPhase)
            {
                return false;
            }
            if (CheckCondtionGreaterThan2years == GetphaseNumberFromProgramId && TimeDuration >= 2)
            {
                return false;
            }
            if (CheckCondtionGreaterThan3years == GetphaseNumberFromProgramId && TimeDuration > 2)
            {
                return false;
            }
            return returnVal;
        }

        public static int ValidationRule_ProgressReport(int OfficeId)
        {
            bool returnVal = false;
            int Total = 0;
            int Max2TotalSingleProgram = 0;
            int Max2TotalMultipleProgram = 0;
            int Max1CurrentPhaseProgram = 0;
            int GrantTotal = 0;

            int CurrentPhaseNumber = GetCurrentProgramPhaseNumber();
            int CheckCondtionPhaseNumber1 = CurrentPhaseNumber - 3;//max 3
            int CheckCondtionPhaseNumber4 = CurrentPhaseNumber - 2;//max 2
            int CheckCondtionPhaseNumberFinal = CurrentPhaseNumber - 1;//max 1           


            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                //total time duration.......
                Total = db.Database.SqlQuery<int>(@" select isnull(sum(isnull(TimeDurationYear,0)),0) as TotalTimeDuration From SubProgramMaster
                    where PhaseStatus <='" + CheckCondtionPhaseNumber1 + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1").FirstOrDefault();

                //max 2 case
                Max2TotalSingleProgram = db.Database.SqlQuery<int>(@" select isnull(count(isnull(SubProgramId,0)),0) as totalProgram From SubProgramMaster
                where PhaseStatus ='" + CheckCondtionPhaseNumber4 + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1 and TimeDurationYear=1").FirstOrDefault();


                Max2TotalMultipleProgram = db.Database.SqlQuery<int>(@" select isnull(count(isnull(SubProgramId,0)),0) as totalProgram From SubProgramMaster
                where PhaseStatus ='" + CheckCondtionPhaseNumber4 + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1 and TimeDurationYear > 1").FirstOrDefault();

                Max2TotalMultipleProgram = Max2TotalMultipleProgram * 2;

                Max1CurrentPhaseProgram = db.Database.SqlQuery<int>(@" select isnull(count(isnull(SubProgramId,0)),0) as totalProgram From SubProgramMaster
                where PhaseStatus ='" + CheckCondtionPhaseNumberFinal + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1 and TimeDurationYear >= 1").FirstOrDefault();
                //if(Max1CurrentPhaseProgram>0)
                //{
                //    Max1CurrentPhaseProgram = 1;

                //}
                //else
                //{
                //    Max1CurrentPhaseProgram = 0;
                //}
                //Max1CurrentPhaseProgram = 1;

                GrantTotal = Total + Max2TotalSingleProgram + Max2TotalMultipleProgram + Max1CurrentPhaseProgram;


            }

            //if (TimeDuration == Total)
            //{
            //    returnVal = true;
            //}

            return GrantTotal;
        }
        public static int ValidationRule_ProgressReport(int OfficeId, int GrantTypID)
        {
            bool returnVal = false;
            int Total = 0;
            int Max2TotalSingleProgram = 0;
            int Max2TotalMultipleProgram = 0;
            int Max1CurrentPhaseProgram = 0;
            int GrantTotal = 0;

            int CurrentPhaseNumber = GetCurrentProgramPhaseNumber();
            int CheckCondtionPhaseNumber1 = CurrentPhaseNumber - 3;//max 3
            int CheckCondtionPhaseNumber4 = CurrentPhaseNumber - 2;//max 2
            int CheckCondtionPhaseNumberFinal = CurrentPhaseNumber - 1;//max 1           


            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                //total time duration.......
                Total = db.Database.SqlQuery<int>(@" select isnull(sum(isnull(TimeDurationYear,0)),0) as TotalTimeDuration From SubProgramMaster
                    where PhaseStatus <='" + CheckCondtionPhaseNumber1 + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1 and GrantTypeId='" + GrantTypID + "'").FirstOrDefault();

                //max 2 case
                Max2TotalSingleProgram = db.Database.SqlQuery<int>(@" select isnull(count(isnull(SubProgramId,0)),0) as totalProgram From SubProgramMaster
                where PhaseStatus ='" + CheckCondtionPhaseNumber4 + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1 and TimeDurationYear=1 and GrantTypeId='" + GrantTypID + "'").FirstOrDefault();


                Max2TotalMultipleProgram = db.Database.SqlQuery<int>(@" select isnull(count(isnull(SubProgramId,0)),0) as totalProgram From SubProgramMaster
                where PhaseStatus ='" + CheckCondtionPhaseNumber4 + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1 and TimeDurationYear > 1 and GrantTypeId='" + GrantTypID + "'").FirstOrDefault();

                Max2TotalMultipleProgram = Max2TotalMultipleProgram * 2;

                Max1CurrentPhaseProgram = db.Database.SqlQuery<int>(@" select isnull(count(isnull(SubProgramId,0)),0) as totalProgram From SubProgramMaster
                where PhaseStatus ='" + CheckCondtionPhaseNumberFinal + "' and OfficeId='" + OfficeId + "' and ApprovedStatus=1 and TimeDurationYear >= 1 and GrantTypeId='" + GrantTypID + "'").FirstOrDefault();
                //if(Max1CurrentPhaseProgram>0)
                //{
                //    Max1CurrentPhaseProgram = 1;

                //}
                //else
                //{
                //    Max1CurrentPhaseProgram = 0;
                //}
                //Max1CurrentPhaseProgram = 1;

                GrantTotal = Total + Max2TotalSingleProgram + Max2TotalMultipleProgram + Max1CurrentPhaseProgram;


            }

            //if (TimeDuration == Total)
            //{
            //    returnVal = true;
            //}

            return GrantTotal;
        }
        //method for retrive total progress report submited till date....by officeId and Grant Type
        public static int Validation_TotalSubmitdQrdTillDateByOfficeId(int OfficeId, int GrantTypeID)
        {
            int Result = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (GrantTypeID > 0)
                {
                    Result = db.Database.SqlQuery<int>(@" select isnull(COUNT(qrd.FiscalYearId),0) as Totalsubmitedtilldate From QuadrimesterReportsDetail qrd
                            inner join SubProgramMaster spm on qrd.ProgramId=spm.SubProgramId
                            where qrd.QuadrimesterId=0 and qrd.OfficeId='" + OfficeId + "' and spm.GrantTypeId='" + GrantTypeID + "'").FirstOrDefault();
                }
                else
                {
                    Result = db.Database.SqlQuery<int>(@" select isnull(COUNT(qrd.FiscalYearId),0) as Totalsubmitedtilldate From QuadrimesterReportsDetail qrd
                            inner join SubProgramMaster spm on qrd.ProgramId=spm.SubProgramId
                            where qrd.QuadrimesterId=0 and qrd.OfficeId='" + OfficeId + "'").FirstOrDefault();
                }
            }

            return Result;

        }


        public static int ValidationRule_TotalApprovedProgramCount(int OfficeId, int GrantTypID)
        {
            bool returnVal = false;
            int Total = 0;
            int totalCase1 = 0;
            int GrantTotal = 0;

            int CurrentPhaseNumber = GetCurrentProgramPhaseNumber();
            int CheckCondtionPhaseNumber1 = CurrentPhaseNumber - 2;//max 3



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (GrantTypID > 0)
                {
                    Total = db.Database.SqlQuery<int>(@" select COUNT(SubProgramId) as TotalProgram From SubProgramMaster spm
                    where OfficeId='" + OfficeId + "' and ApprovedStatus=1 and spm.PhaseStatus<'" + CheckCondtionPhaseNumber1 + "' and spm.GrantTypeId='" + GrantTypID + "'").FirstOrDefault();

                    totalCase1 = db.Database.SqlQuery<int>(@" select COUNT(SubProgramId) as TotalProgram From SubProgramMaster spm
                        where OfficeId='" + OfficeId + "' and ApprovedStatus=1 and spm.PhaseStatus='" + CheckCondtionPhaseNumber1 + "' and TimeDurationYear<3 and spm.GrantTypeId='" + GrantTypID + "'").FirstOrDefault();


                    GrantTotal = Total + totalCase1;
                }
                else
                {
                    Total = db.Database.SqlQuery<int>(@" select COUNT(SubProgramId) as TotalProgram From SubProgramMaster spm
                where OfficeId='" + OfficeId + "' and ApprovedStatus=1 and spm.PhaseStatus<'" + CheckCondtionPhaseNumber1 + "'").FirstOrDefault();

                    totalCase1 = db.Database.SqlQuery<int>(@" select COUNT(SubProgramId) as TotalProgram From SubProgramMaster spm
                        where OfficeId='" + OfficeId + "' and ApprovedStatus=1 and spm.PhaseStatus='" + CheckCondtionPhaseNumber1 + "' and TimeDurationYear<3").FirstOrDefault();


                    GrantTotal = Total + totalCase1;
                }
                //total time duration.......



            }



            return GrantTotal;
        }

        public static bool CheckUserProfile(int OfficeId)
        {
           

            using (var db = new GrantAppDBEntities())  // Replace with your actual DbContext
            {
                int count = db.Database.SqlQuery<int>(@"
            SELECT COUNT(*) 
            FROM ProfileUpdates
            WHERE OfficeId = @p0 
            AND UpdatedDate > @p1",
                    OfficeId,
                    GrantApp.StaticValue.ConstantValues.ProfileUpdateGreaterThan
                ).FirstOrDefault();

                return count > 0; // Return true if profile is updated, false otherwise
            }
        }

        public static bool CheckUserProgress(int officeId)
        {
            using ( var db = new GrantAppDBEntities())  // 
            {
                var subPrograms = db.Database.SqlQuery<SubProgramMasterDto>(
                                 @"SELECT SubProgramId, PhaseStatus, TimeDurationYear 
                                  FROM SubProgramMaster 
                                  WHERE ApprovedStatus = 1
                                  AND OfficeId = @officeId",
                                 new SqlParameter("@officeId", officeId)
                             ).ToList();

                foreach (var subProgram in subPrograms)
                {
                    int requiredRecords = 0;

                    if (subProgram.PhaseStatus <= 3)  //78/79 ra tyo vanda tala 76/77 samma
                    {
                        requiredRecords = subProgram.TimeDurationYear + 1;
                    
                    }
                    else if (subProgram.PhaseStatus == 4)  //79/80
                    {
                        if (subProgram.TimeDurationYear == 1)
                        {
                            requiredRecords = subProgram.TimeDurationYear + 1;
                        }
                        else if (subProgram.TimeDurationYear == 2)
                        {
                            requiredRecords = subProgram.TimeDurationYear + 1;
                        }
                        else if (subProgram.TimeDurationYear == 3)
                        {
                            requiredRecords = 4;
                        }


                    }
                    else if (subProgram.PhaseStatus == 5)  //80/81
                    {
                        if(subProgram.TimeDurationYear == 1 )
                        {
                            requiredRecords = subProgram.TimeDurationYear + 1; 
                        }
                        else if (subProgram.TimeDurationYear == 2)
                        {
                            requiredRecords = subProgram.TimeDurationYear + 2;
                        }
                        else if(subProgram.TimeDurationYear == 3)
                        {
                            requiredRecords = subProgram.TimeDurationYear + 1;
                        }

                    
                    }
                    else if (subProgram.PhaseStatus == 6)  //81/82
                    {

                        if (subProgram.TimeDurationYear == 1)
                        {
                            requiredRecords = subProgram.TimeDurationYear + 2;
                        }
                        else if (subProgram.TimeDurationYear == 2)
                        {
                            requiredRecords = 3;
                        }
                        else if (subProgram.TimeDurationYear == 3)
                        {
                            requiredRecords = 3;
                        }

                      
                    }
                    else if (subProgram.PhaseStatus == 7) //82/83 chalirakheko vara 1 report 
                    {
                        requiredRecords = 1;
                    }

                    if (requiredRecords > 0)
                    {
                        // Check if the required number of records exist in the Report table
                        var existingReports = db.Database.SqlQuery<int>(
                                              @"SELECT COUNT(*) 
                                              FROM dbo.QuadrimesterReportsDetail qr
                                              INNER JOIN dbo.SubProgramMaster spm
                                              ON spm.SubProgramId = qr.ProgramId
                                              WHERE qr.ProgramId = @subProgramId
                                              AND spm.PhaseStatus = @phaseNumber",
                                              new SqlParameter("@subProgramId", subProgram.SubProgramId),
                                              new SqlParameter("@phaseNumber", subProgram.PhaseStatus)
                                          ).FirstOrDefault();

                        var completionReport = db.Database.SqlQuery<int>(
                                            @"SELECT COUNT(*) 
                                              FROM dbo.ApplicationCompletionStatus acs
                                              INNER JOIN dbo.SubProgramMaster spm
                                              ON spm.SubProgramId = acs.ApplicationId
                                              WHERE acs.ApplicationId = @subProgramId
                                              AND spm.PhaseStatus = @phaseNumber",
                                            new SqlParameter("@subProgramId", subProgram.SubProgramId),
                                            new SqlParameter("@phaseNumber", subProgram.PhaseStatus)
                                        ).FirstOrDefault();

                        var totaleportsubmitted = existingReports + completionReport;

                        // If not enough reports exist, return false
                        if (totaleportsubmitted < requiredRecords)
                        {
                            return false;
                        }
                    }
                }
                // If all checks pass, return true
                return true;
            }
        }


        public static bool CheckUserKramagat(int OfficeId)
        {
            using (var db = new GrantAppDBEntities())  // 
            {
                //var subPrograms = db.Database.SqlQuery<SubProgramMasterDto>(
                //                 @"SELECT SPM.SubProgramId, SPM.PhaseStatus, SPM.TimeDurationYear 
                //                  FROM SubProgramMaster SPM
                //                  LEFT JOIN ProgramwiseAmount PWS ON PWS.ProgramId = SPM.SubProgramId
                //                  WHERE (SPM.PhaseStatus = 6 AND SPM.TimeDurationYear >= 2 AND PWS.Amount > 0) 
                //                  SPM.OfficeId = @officeId 
                //                  AND SPM.ApprovedStatus = 1
                //                  AND SPM.IsCancelled = 0
                //                  AND SPM.PhaseStatus IN (6,7) 
                //                  AND (
                //                      OR 
                //                      (SPM.PhaseStatus = 6 AND SPM.TimeDurationYear > 2 AND PWS.AmountSecondYear > 0)
                //                  )",
                //                 new SqlParameter("@officeId", OfficeId)
                //                 ).ToList();

                var subPrograms = db.Database.SqlQuery<SubProgramMasterDto>(
                                    @"
                                    SELECT 
                                        SPM.SubProgramId,
                                        SPM.PhaseStatus,
                                        SPM.TimeDurationYear
                                    FROM SubProgramMaster SPM
                                    LEFT JOIN ProgramwiseAmount PWS 
                                        ON PWS.ProgramId = SPM.SubProgramId
                                    WHERE 
                                        SPM.OfficeId = @officeId
                                        AND SPM.ApprovedStatus = 1
                                        AND SPM.IsCancelled = 0
                                        AND SPM.PhaseStatus IN (6,7)
                                        AND
                                        (
                                            (SPM.PhaseStatus = 7 
                                                AND SPM.TimeDurationYear >= 2 
                                                AND PWS.Amount > 0
                                            )
                                            OR
                                            (SPM.PhaseStatus = 6 
                                                AND SPM.TimeDurationYear > 2 
                                                AND PWS.AmountSecondYear > 0
                                            )
                                        )
                                    ",
                                    new SqlParameter("@officeId", OfficeId)
                                    ).ToList();

                int AllSubmitted = 1;
                foreach (var subProgram in subPrograms)
                {
                   AllSubmitted = CheckIfKramagatIsAppliedOrNot(subProgram.SubProgramId);
                }
                if(AllSubmitted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
        }


        public static int GetTotalReportRequiredByProgramId(int programId)
        {
            using (var db = new GrantAppDBEntities())  // 
            {

                var subProgram = db.Database.SqlQuery<SubProgramMasterDto>(
                                 @"SELECT SubProgramId, PhaseStatus, TimeDurationYear 
                                  FROM SubProgramMaster 
                                  WHERE ApprovedStatus = 1 
                                  AND IsCancelled = 0 
                                  AND SubprogramId = @programId",
                                 new SqlParameter("@programId", programId)
                             ).FirstOrDefault();

               
                int requiredRecords = 0;

                if (subProgram.PhaseStatus <= 3)
                {
                    requiredRecords = subProgram.TimeDurationYear;

                }
                else if (subProgram.PhaseStatus == 4)  //79/80
                {
                    if (subProgram.TimeDurationYear == 1)
                    {
                        requiredRecords = 1 ;
                    }
                    else if (subProgram.TimeDurationYear == 2)
                    {
                        requiredRecords = 3;
                    }
                    else if (subProgram.TimeDurationYear == 3)
                    {
                        requiredRecords = 4;
                    }


                }
                else if (subProgram.PhaseStatus == 5)  //80/81
                {

                    if (subProgram.TimeDurationYear == 1)
                    {
                        requiredRecords = 2;
                    }
                    else if (subProgram.TimeDurationYear == 2)
                    {
                        requiredRecords = 3;
                    }
                    else if (subProgram.TimeDurationYear == 3)
                    {
                        requiredRecords = 3;
                    }

                }
                else if (subProgram.PhaseStatus == 6) //81/82
                {
                    requiredRecords = 1;
                }

                return requiredRecords;

            }

            
        }


        public static int GetCurrentLoginUserMenuBarStatus()
        {
            
            int CurrentLoginOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            bool isProfileUpdated = CheckUserProfile(CurrentLoginOfficeId);  
            bool isProgressCompleted = isProfileUpdated?CheckUserProgress(CurrentLoginOfficeId):false;
            bool isKramagatCompleted = isProgressCompleted?CheckUserKramagat(CurrentLoginOfficeId):false;
            bool isNewYojanaCompleted = isKramagatCompleted ? true: false;

            if (!isProfileUpdated)
            {
                return 1; // Show only Profile
            }
            else if (!isProgressCompleted)
            {
                return 2; // Show Profile + Progress
            }
            else if (!isKramagatCompleted)
            {
                return 3; // Show Profile + Progress + Kramagat
            }
            else if (!isNewYojanaCompleted)
            {
                return 4; // Show all menus
            }
            else
            {
                return 5; // Show all menus
            }
        }


        //public static int GetCurrentLoginUserMenuBarStatus()
        //{
        //    int ReturnStatusId = 0;
        //    //if user updated their profile....
        //    //status =1;open progress report ...menu with profile

        //    //if users completed all progress report with status
        //    //status=2 open request amout details..

        //    //if users requested all demand
        //    //status=3....open all menu



            
        //   int CurrentLoginOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
        //    int profileUpdatedStatus = 0;
        //    int progressRptstatus = 0;
        //    int RequestAmount = 0;
        //    using (GrantAppDBEntities db = new GrantAppDBEntities())
        //    {
        //        //first check update status
        //        profileUpdatedStatus = db.Database.SqlQuery<int>(@"select COUNT(*) as CheckIfUpdated From ProfileUpdates
        //        where OfficeId='" + CurrentLoginOfficeId + "' and FiscalYearId=6").FirstOrDefault();

        //        progressRptstatus = db.Database.SqlQuery<int>(@"select COUNT(*) as totalsumbitedprogressrpt From QuadrimesterReportsDetail
        //                where OfficeId='" + CurrentLoginOfficeId + "' and QuadrimesterId=0").FirstOrDefault();


        //    }
        //    if (profileUpdatedStatus > 0)
        //    {
        //        ReturnStatusId = 1;
        //    }

        //    //check in progress report table

        //    int tosubmitAllProgressReport = ValidationRule_ProgressReport(CurrentLoginOfficeId);
        //    int totalTosubmitStatus = TotalProgramProgressStatusCount_validation(CurrentLoginOfficeId);

        //    if (tosubmitAllProgressReport <= progressRptstatus)
        //    {
        //        if (TotalProgramProgressStatusCount_validation(CurrentLoginOfficeId) == 0)
        //        {
        //            ReturnStatusId = 2;
        //        }

        //        if (TotalGrantRequestamountCount_Validation(CurrentLoginOfficeId) == 0)
        //        {
        //            ReturnStatusId = 3;
        //        }
        //        if (TotalGrantRequestamountCount_Validation(CurrentLoginOfficeId) < 0)
        //        {
        //            ReturnStatusId = 3;
        //        }
        //    }






        //    return ReturnStatusId;

        //}

        //check this one
        public static int TotalProgramProgressStatusCount_validation(int officeid)
        {
            int returnTotal = 0;
            int forOldApp = 0;//1,2
            int forRunningApp = 0;//3>2
            int forPreviousApp = 0;//4>=2
            int GrandTotal = 0;

            int TotalSubmitedStatusInAppRunning = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                //first check update status
                forOldApp = db.Database.SqlQuery<int>(@"select COUNT(*) as TotalCase1 From SubProgramMaster
                where OfficeId='" + officeid + "' and ApprovedStatus=1 and PhaseStatus in (1,2)").FirstOrDefault();

                forRunningApp = db.Database.SqlQuery<int>(@"select COUNT(*) as TotalCase1 From SubProgramMaster
                    where OfficeId='" + officeid + "' and ApprovedStatus=1 and TimeDurationYear<3 and PhaseStatus in (3)").FirstOrDefault();

                forPreviousApp = db.Database.SqlQuery<int>(@"select COUNT(*) as TotalCase1 From SubProgramMaster
                where OfficeId='" + officeid + "' and ApprovedStatus=1 and TimeDurationYear<2 and PhaseStatus in (4)").FirstOrDefault();

                GrandTotal = forOldApp + forRunningApp + forPreviousApp;

                //status runninng app
                TotalSubmitedStatusInAppRunning = db.Database.SqlQuery<int>(@"select COUNT(*) as Total From ApplicationCompletionStatus
                    where OfficeId='" + officeid + "'").FirstOrDefault();


                returnTotal = GrandTotal - TotalSubmitedStatusInAppRunning;
                if (returnTotal < 0)
                {
                    return 0;
                }


            }

            return returnTotal;
        }

        public static int TotalGrantRequestamountCount_Validation(int OfficeId)
        {
            int ReturnVal = 0;
            int greaterthen2 = 0;
            int greaterorlessthen2 = 0;
            int totallessandgreaterthen2 = 0;
            int notcompletedordroppedcount = 0;
            int totalRequestCount = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                //first check update status
                greaterthen2 = db.Database.SqlQuery<int>(@"select COUNT(*) as total From SubProgramMaster
                where OfficeId='" + OfficeId + "' and TimeDurationYear>2 and PhaseStatus=4 and ApprovedStatus=1").FirstOrDefault();


                greaterorlessthen2 = db.Database.SqlQuery<int>(@"select COUNT(*) as total From SubProgramMaster
                where OfficeId='" + OfficeId + "' and TimeDurationYear>=2 and PhaseStatus=5 and ApprovedStatus=1").FirstOrDefault();

                notcompletedordroppedcount = db.Database.SqlQuery<int>(@"select COUNT(*) as Total From ApplicationCompletionStatus
                where CompletionStatusId in (2,3) and OfficeId='" + OfficeId + "'").FirstOrDefault();


                totalRequestCount = db.Database.SqlQuery<int>(@"select COUNT(*) as TotalRequestedGrantAmount From RequestGrantAmount
                    where OfficeId='" + OfficeId + "' and FiscalYearId=16").FirstOrDefault();


                totallessandgreaterthen2 = greaterthen2 + greaterorlessthen2;

                ReturnVal = totallessandgreaterthen2 - notcompletedordroppedcount;
                ReturnVal = ReturnVal - totalRequestCount;
            }


            return ReturnVal;
        }


        public static string GetFiscalYearTitleFromApplicationId(int programId)
        {
            string phaseTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                phaseTitle = db.Database.SqlQuery<string>(@"select b.PhaseTitle From SubProgramMaster a
                    inner join ProgramPhaseStatus b on a.PhaseStatus=b.PhaseNumber
                    where a.SubProgramId='" + programId + "' and ApprovedStatus=1").FirstOrDefault();
                return phaseTitle;
            }

        }

        public static string GetCancelledDocumentsUrl(int programId)
        {
            string phaseTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                phaseTitle = db.Database.SqlQuery<string>(@"select CancelledDocuments From SubProgramMaster
                where SubProgramId='" + programId + "'").FirstOrDefault();
                return phaseTitle;
            }

        }

        public static string GetCancelledDocuments1Url(int programId)
        {
            string phaseTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                phaseTitle = db.Database.SqlQuery<string>(@"select CancelledDocuments1 From SubProgramMaster
                where SubProgramId='" + programId + "'").FirstOrDefault();
                return phaseTitle;
            }

        }

        public static string GetVDCMUNNameByOfficeIdForLocalLevel(int OfficeId)
        {
            string VdcMunName = string.Empty;
            string OfficeName = string.Empty;
            int LLOrProvince = GetCurrentLoginUserUserTypeByOfficeId(OfficeId);
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (LLOrProvince == 4)//localLevel
                {
                    VdcMunName = db.Database.SqlQuery<string>(@"select b.VdcMunNameNep From AspNetUsers a
                            inner join VdcMun b on a.VDCMUNCode=b.VdcMunCode
                            where OfficeId='" + OfficeId + "'").FirstOrDefault();
                }
                else
                {
                    VdcMunName = db.Database.SqlQuery<string>(@"select b.ProvinceTitleNep From AspNetUsers a
                                    inner join Province b on a.ProvinceId=b.ProvinceId
                                    where OfficeId='" + OfficeId + "'").FirstOrDefault();
                }


            }
            return VdcMunName;

        }

        public static int CheckIfAllReportSubmittedOrNot(int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var OfficeIdParam = new SqlParameter("@OfficeId", OfficeId);

                // Define the output parameter
                var ResultParam = new SqlParameter
                {
                    ParameterName = "@Result",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                // Execute the stored procedure
                db.Database.ExecuteSqlCommand("EXEC CheckReportSubmissionStatus @OfficeId, @Result OUTPUT",
                                               OfficeIdParam, ResultParam);

                // Retrieve the output value
                return (int)ResultParam.Value;
            }
        }


        public static int CheckIfKramagatIsAppliedOrNot(int SubProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                return db.Database.SqlQuery<int>(@"
            SELECT CASE 
                WHEN EXISTS (
                    SELECT 1 
                    FROM RequestGrantAmount rg 
                    WHERE rg.ProgramId = @SubProgramId
                    AND rg.FiscalYearId = 18
                ) 
                OR EXISTS (
                    SELECT 1 
                    FROM SubProgramMaster spm 
                    WHERE spm.SubProgramId = @SubProgramId
                    AND spm.IsCancelled = 1
                ) 
                THEN 1 
                ELSE 0 
            END AS Result",
                    new SqlParameter("@SubProgramId", SubProgramId)
                ).FirstOrDefault();
            }
        }





        public static decimal? RetrunRsInLakh(decimal? amount)
        {
            if(amount.HasValue)
            {
                if(amount>0)
                {
                    return amount / 100000;
                }
                else
                {
                    return 0m;
                }
            }
            else
            {
                return 0m;
            }

        }

        public static decimal RetrunRsInLakh(decimal amount)
        {
            if (amount > 0)
            {
                return amount / 100000;
            }
            else
            {
                return 0m;
            }

        }


        public static string GetNepaliOrdinal(int number)
        {
            switch (number)
            {
                case 1: return "पहिलो";
                case 2: return "दोस्रो";
                case 3: return "तेस्रो";
                case 4: return "चौथो";
                case 5: return "पाँचौ";
                case 6: return "छैठौ";
                case 7: return "सातौ";
                case 8: return "आठौ";
                case 9: return "नवौ";
                case 10: return "दशौ";
                default: return number + " औं";
            }
        }


        public static IEnumerable<SelectListItem> GetWardListDD()
        {
            return Enumerable.Range(1, 31).Select(i => new SelectListItem
            {
                Value = i.ToString(),
                Text = $"वार्ड नं {i}"
            });
        }


    }
}