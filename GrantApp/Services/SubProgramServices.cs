using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using DocumentFormat.OpenXml.EMMA;
using System.Web.Mvc;
using System.IO;

namespace GrantApp.Services
{
    public class SubProgramServices
    {
        private readonly string _folderPath;

        public SubProgramServices() 
        {

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            _folderPath = Path.Combine(basePath, "RequiredDocs");
        }

        #region Subprogram Details
        public List<SubProgramMaster> PopulateSubProgram(int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramMaster> SubProgramSetupList = new List<SubProgramMaster>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateSubProgram @GrantTypeId", GrantTypeIdParam).ToList();
                return SubProgramSetupList;
            }
        }


        public SubProgramMaster PopulateSubProgramBySupprogramAndGrantTypeId(int GrantTypeId, int SubprogramID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                SubProgramMaster SubProgramSetupList = new SubProgramMaster();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = SubprogramID };
                SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateSubProgramBySupprogramAndGrantTypeId @GrantTypeId,@SubprogramId", GrantTypeIdParam, SubprogramIdParam).FirstOrDefault();
                if (SubProgramSetupList == null)
                {
                    SubProgramSetupList = new SubProgramMaster();
                }

                return SubProgramSetupList;
            }
        }


        public List<SubProgramMaster> PopulateSubProgramForClient(int GrantTypeId, int PreviusOrcurrent)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramMaster> SubProgramSetupList = new List<SubProgramMaster>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PreviousOrCurrentParam = new SqlParameter { ParameterName = "@PreviousOrCurrent", Value = PreviusOrcurrent };
                SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateSubProgramListForClient @GrantTypeId,@PreviousOrCurrent", GrantTypeIdParam, PreviousOrCurrentParam).ToList();
                return SubProgramSetupList;
            }
        }

        public List<SubProgramMaster> PopulateSubProgramListForClientWithOfficeId(int GrantTypeId, int PreviusOrcurrent, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramMaster> SubProgramSetupList = new List<SubProgramMaster>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PreviousOrCurrentParam = new SqlParameter { ParameterName = "@PreviousOrCurrent", Value = PreviusOrcurrent };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateSubProgramListForClientWithOfficeId @GrantTypeId,@PreviousOrCurrent,@OfficeId", GrantTypeIdParam, PreviousOrCurrentParam, OfficeIdParam).ToList();
                return SubProgramSetupList;
            }
        }



        public List<ViewRequestGrantAmountModel> PopulateRunninProjectListForClient(int GrantTypeId, int PhaseNumber, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewRequestGrantAmountModel> SubProgramSetupList = new List<ViewRequestGrantAmountModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                try
                {
                    SubProgramSetupList = db.Database.SqlQuery<ViewRequestGrantAmountModel>("PopulateRunninProjectListForClient @PhaseNumber,@GrantTypeId,@OfficeId", PhaseNumberParam, GrantTypeIdParam, OfficeIdParam).ToList();
                }
                catch (Exception)
                {

                    SubProgramSetupList = new List<ViewRequestGrantAmountModel>();
                }

                return SubProgramSetupList;
            }
        }

        public List<RequestGrantAmountViewModel> PopulateRequestGrantAmountOfficeWise(int GrantTypeId, int PhaseNumber, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<RequestGrantAmountViewModel> RequestGrantAmountList = new List<RequestGrantAmountViewModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                try
                {
                    RequestGrantAmountList = db.Database.SqlQuery<RequestGrantAmountViewModel>("PopulateRequestGrantAmountOfficeWise @PhaseNumber,@GrantTypeId,@OfficeId", PhaseNumberParam, GrantTypeIdParam, OfficeIdParam).ToList();
                }
                catch (Exception)
                {

                    RequestGrantAmountList = new List<RequestGrantAmountViewModel>();
                }

                return RequestGrantAmountList;
            }
        }


        public List<SupportingDocumentsModel> PopulateSubProgramSupportingDoc(int SubProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SupportingDocumentsModel> SupportingDocList = new List<SupportingDocumentsModel>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                SupportingDocList = db.Database.SqlQuery<SupportingDocumentsModel>("PopulateSubProgramSupportingDoc @SubProgramId", SubProgramIdParam).ToList();
                return SupportingDocList;
            }
        }


        public NewProgramInitiation PopulateNewProgramInitiationDoc(int Phase, int OfficeId, int GrantType)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                
                var data = db.NewProgramInitiation.Where(x=>x.OfficeId == OfficeId && x.GrantType == GrantType && x.ProjectPhase == Phase ).FirstOrDefault();
                return data;
            }
        }


        public List<AnusuchiViewModel> PopulateAnusuchi(int GrantTypeId, int PhaseNumber, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<AnusuchiViewModel> AnusuchiDataList = new List<AnusuchiViewModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = 18 };
            try
            {
                    AnusuchiDataList = db.Database.SqlQuery<AnusuchiViewModel>(
                        "GetAnusuchiData @GrantTypeId,@PhaseNumber,@OfficeId,@FiscalYearId",
                        GrantTypeIdParam, PhaseNumberParam, OfficeIdParam, FiscalYearIdParam
                    ).ToList();
                }
            catch (Exception)
            {

                    AnusuchiDataList = new List<AnusuchiViewModel>();
            }

            return AnusuchiDataList;
            }
        }


        public bool IsApplicationEditable(int OfficeId, int GrantType, int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                return db.NewProgramInitiation
                         .Any(x => x.OfficeId == OfficeId
                                   && x.GrantType == GrantType
                                   && x.ProjectPhase == PhaseNumber
                                   && x.Status == 1
                                   && x.FinalFileDoc == null);
            }
        }

        public ApplicationStatusModel GetApplicationStatus(int OfficeId, int GrantType, int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                return db.NewProgramInitiation
                         .Where(x => x.OfficeId == OfficeId
                                  && x.GrantType == GrantType
                                  && x.ProjectPhase == PhaseNumber)
                         .Select(x => new ApplicationStatusModel
                         {
                             Status = x.Status,
                             FinalFileDoc = x.FinalFileDoc
                         })
                         .FirstOrDefault();
            }
        }


        public int InsertSubProgram(SubProgramMaster _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                //_Model.OfficeId = 1;
                //_Model.GrantTypeId = 1;
                _Model.Status = 1;

                if (_Model.TimeDurationYear == 1 && _Model.GrantTypeId == 2)
                {
                    
                    _Model.BudgetForSecondYear = 0;
                    _Model.BudgetForThirdYear = 0;
                }
                //else
                //{
                //    //_Model.BudgetForFirstYear = _Model.TotalBudget;
                //    _Model.BudgetForSecondYear = 0;
                //    _Model.BudgetForThirdYear = 0;
                //}
                var MainSectionIdParam = new SqlParameter { ParameterName = "@MainSectionId", Value = _Model.MainSectionId };
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = _Model.ProgramId };

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = _Model.OfficeId };
                var SubProgramTitleParam = new SqlParameter { ParameterName = "@SubProgramTitle", Value = _Model.SubProgramTitle };
                var TotalBudgetParam = new SqlParameter { ParameterName = "@TotalBudget", Value = _Model.TotalBudget < 0 ? 0 : _Model.TotalBudget };
                var ProbableBenefitedPopulationParam = new SqlParameter { ParameterName = "@ProbableBenefitedPopulation", Value = _Model.ProbableBenefitedPopulation < 0 ? 0 : _Model.ProbableBenefitedPopulation };
                var TimeDurationYearParam = new SqlParameter { ParameterName = "@TimeDurationYear", Value = _Model.TimeDurationYear < 0 ? 0 : _Model.TimeDurationYear };

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = _Model.GrantTypeId };
                var createdDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };

                //this is for complementary grant-provdc amount, ngoingo amount, govn of nepal amount....
                var AmountProvinceVdcParam = new SqlParameter { ParameterName = "@AmountProvinceVdc", Value = _Model.AmountProvinceVdc.HasValue ? _Model.AmountProvinceVdc : 0 };
                var NGOINGOAmountParam = new SqlParameter { ParameterName = "@NGOINGOAmount", Value = _Model.NGOINGOAmount.HasValue ? _Model.NGOINGOAmount : 0 };
                var GovnNepalAmountParam = new SqlParameter { ParameterName = "@GovnNepalAmount", Value = _Model.GovnNepalAmount.HasValue ? _Model.GovnNepalAmount : 0 };

                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = 1 };
                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = _Model.Status };
                var ProgramPriorityParam = new SqlParameter { ParameterName = "@ProgramPriority", Value = _Model.ProgramPirority };

                var TermsAndCondtionsParam = new SqlParameter { ParameterName = "@TermsAndCondtions", Value = false };
                var FinalDocumentsUrlParam = new SqlParameter { ParameterName = "@FinalDocumentsUrl", Value = _Model.FinalDocumentsUrl == null ? string.Empty : _Model.FinalDocumentsUrl };
                _Model.FiscalYearId = 18;
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = _Model.FiscalYearId };
                //int PhaseStatus = 2;
                //int PhaseStatus = 3;
                int PhaseStatus = 8;
                var PhaseStatusParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseStatus };

                var BudgetForFirstYearParam = new SqlParameter { ParameterName = "@BudgetForFirstYear", Value = _Model.BudgetForFirstYear.HasValue ? _Model.BudgetForFirstYear : 0 };
                var BudgetForSecondYearParam = new SqlParameter { ParameterName = "@BudgetForSecondYear", Value = _Model.BudgetForSecondYear.HasValue ? _Model.BudgetForSecondYear : 0 };
                var BudgetForThirdYearParam = new SqlParameter { ParameterName = "@BudgetForThirdYear", Value = _Model.BudgetForThirdYear.HasValue ? _Model.BudgetForThirdYear : 0 };

                var BudgetForFirstYearPujiParam = new SqlParameter { ParameterName = "@BudgetForFirstYearPuji", Value = _Model.PujiFirstYearAmount.HasValue ? _Model.PujiFirstYearAmount : 0 };
                var BudgetForSecondYearPujuParam = new SqlParameter { ParameterName = "@BudgetForSecondYearPuji", Value = _Model.PujiSecondYearAmount.HasValue ? _Model.PujiSecondYearAmount : 0 };
                var BudgetForThirdYearPujiParam = new SqlParameter { ParameterName = "@BudgetForThirdYearPuji", Value = _Model.PujiThirdYearAmount.HasValue ? _Model.PujiThirdYearAmount : 0 };

                var SelectedProgramsParam = new SqlParameter { ParameterName = "@SelectedPrograms", Value = _Model.SelectedPrograms };

                var OtherProgramParam = new SqlParameter { ParameterName = "@OtherProgram", Value = _Model.OtherProgram == null ? string.Empty : _Model.OtherProgram };

                var AyojanaAddressParam = new SqlParameter { ParameterName = "@AyojanaAddress", Value = _Model.AyojanaAddress };

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


                var result = db.Database.ExecuteSqlCommand("exec InsertSubProgram @MainSectionId,@ProgramId,@OfficeId,@SubProgramTitle,@TotalBudget,@ProbableBenefitedPopulation,@TimeDurationYear,@GrantTypeId,@AmountProvinceVdc,@NGOINGOAmount,@GovnNepalAmount,@CreatedDate,@CreatedBy,@Status,@ProgramPriority,@TermsAndCondtions,@FinalDocumentsUrl,@FiscalYearId,@PhaseStatus,@BudgetForFirstYear,@BudgetForSecondYear,@BudgetForThirdYear,@BudgetForFirstYearPuji,@BudgetForSecondYearPuji,@BudgetForThirdYearPuji,@SelectedPrograms, @OtherProgram, @AyojanaAddress ,@Message OUT,@PrimaryId OUT",
                   MainSectionIdParam, ProgramIdParam, OfficeIdParam, SubProgramTitleParam, TotalBudgetParam, ProbableBenefitedPopulationParam, TimeDurationYearParam, GrantTypeIdParam, AmountProvinceVdcParam, NGOINGOAmountParam, GovnNepalAmountParam, createdDateParam, CreatedByParam, StatusParam, ProgramPriorityParam,TermsAndCondtionsParam, FinalDocumentsUrlParam, FiscalYearIdParam, PhaseStatusParam, BudgetForFirstYearParam, BudgetForSecondYearParam, BudgetForThirdYearParam, BudgetForFirstYearPujiParam, BudgetForSecondYearPujuParam, BudgetForThirdYearPujiParam, SelectedProgramsParam,OtherProgramParam,AyojanaAddressParam, MessageParam, PrimaryIdParam);
                int SubProgramId = (int)PrimaryIdParam.Value;


                //if (SubProgramId > 0)
                //{
                //    //insert into supporting doc
                //    var SubProgramIdDtlParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };

                //    var OfficeIdDtlParam = new SqlParameter { ParameterName = "@OfficeId", Value = _Model.OfficeId };


                //    //Added New filed
                //    var NecessityForProgramParam = new SqlParameter { ParameterName = "@NecessityForProgram", Value = _Model.ObjSupportingDocumentsModel.NecessityForProgram == null ? string.Empty : _Model.ObjSupportingDocumentsModel.NecessityForProgram };
                //    var PhysicalAndManPowerParam = new SqlParameter { ParameterName = "@PhysicalAndManPower", Value = _Model.ObjSupportingDocumentsModel.PhysicalAndManPower == null ? string.Empty : _Model.ObjSupportingDocumentsModel.PhysicalAndManPower };


                //    var InfrastrucutreRelatedToProvinces = new SqlParameter { ParameterName = "@InfrastrucutreRelatedToProvinces", Value = _Model.ObjSupportingDocumentsModel.InfrastrucutreRelatedToProvinces };
                //    var PovertyAlleviationAnLivingStandardparam = new SqlParameter { ParameterName = "@PovertyAlleviationAnLivingStandard", Value = _Model.ObjSupportingDocumentsModel.PovertyAlleviationAnLivingStandard };
                //    var YearlyProgramAndBudgetParam = new SqlParameter { ParameterName = "@YearlyProgramAndBudget", Value = _Model.ObjSupportingDocumentsModel.YearlyProgramAndBudget == null ? string.Empty : _Model.ObjSupportingDocumentsModel.YearlyProgramAndBudget };
                //    var ResourceSkillAndSourceUsedParam = new SqlParameter { ParameterName = "@ResourceSkillAndSourceUsed", Value = _Model.ObjSupportingDocumentsModel.ResourceSkillAndSourceUsed };
                //    var Minimum50PercentageAppropriationParam = new SqlParameter { ParameterName = "@Minimum50PercentageAppropriation", Value = _Model.ObjSupportingDocumentsModel.Minimum50PercentageAppropriation };
                //    var TopPriorityInGovernmentParam = new SqlParameter { ParameterName = "@TopPriorityInGovernment", Value = _Model.ObjSupportingDocumentsModel.TopPriorityInGovernment };
                //    var InGovernmentPolicyAndProgramParam = new SqlParameter { ParameterName = "@InGovernmentPolicyAndProgram", Value = _Model.ObjSupportingDocumentsModel.InGovernmentPolicyAndProgram };

                //    var FeasibilitiesStudyFileParam = new SqlParameter { ParameterName = "@FeasibilitiesStudyFile", Value = _Model.ObjSupportingDocumentsModel.FeasibilitiesStudyFile == null ? string.Empty : _Model.ObjSupportingDocumentsModel.FeasibilitiesStudyFile };
                //    var SDSDocFileParam = new SqlParameter { ParameterName = "@SDSDocFile", Value = _Model.ObjSupportingDocumentsModel.SDSDocFile == null ? string.Empty : _Model.ObjSupportingDocumentsModel.SDSDocFile };
                //    var DecisionDocFileParam = new SqlParameter { ParameterName = "@DecisionDocFile", Value = _Model.ObjSupportingDocumentsModel.DecisionDocFile == null ? string.Empty : _Model.ObjSupportingDocumentsModel.DecisionDocFile };
                //    var EnvironmentDocFileParam = new SqlParameter { ParameterName = "@EnvironmentDocFile", Value = _Model.ObjSupportingDocumentsModel.EnvironmentDocFile == null ? string.Empty : _Model.ObjSupportingDocumentsModel.EnvironmentDocFile };
                //    var ExtraDocFileDtlParam = new SqlParameter { ParameterName = "@ExtraDocFile", Value = _Model.ObjSupportingDocumentsModel.ExtraDocFile == null ? string.Empty : _Model.ObjSupportingDocumentsModel.ExtraDocFile };
                //    var createdDateDtlParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };
                //    var StatusDtlParam = new SqlParameter { ParameterName = "@Status", Value = true };

                //    var MessageDtlParam = new SqlParameter
                //    {
                //        ParameterName = "@Message",
                //        DbType = DbType.String,
                //        Size = 50,
                //        Direction = System.Data.ParameterDirection.Output
                //    };
                //    var resultDtl = db.Database.ExecuteSqlCommand("exec InsertSupportingDocumentsDetails @SubProgramId,@OfficeId,@NecessityForProgram,@PhysicalAndManPower,@InfrastrucutreRelatedToProvinces,@PovertyAlleviationAnLivingStandard,@YearlyProgramAndBudget,@ResourceSkillAndSourceUsed,@Minimum50PercentageAppropriation,@TopPriorityInGovernment,@InGovernmentPolicyAndProgram,@FeasibilitiesStudyFile,@SDSDocFile,@DecisionDocFile,@EnvironmentDocFile,@ExtraDocFile,@Status,@CreatedDate,@Message OUT",
                //    SubProgramIdDtlParam, OfficeIdDtlParam, NecessityForProgramParam, PhysicalAndManPowerParam, InfrastrucutreRelatedToProvinces, PovertyAlleviationAnLivingStandardparam, YearlyProgramAndBudgetParam, ResourceSkillAndSourceUsedParam, Minimum50PercentageAppropriationParam, TopPriorityInGovernmentParam, InGovernmentPolicyAndProgramParam, FeasibilitiesStudyFileParam, SDSDocFileParam, DecisionDocFileParam, EnvironmentDocFileParam, ExtraDocFileDtlParam, StatusDtlParam, createdDateDtlParam, MessageDtlParam);
                //    msg = MessageDtlParam.SqlValue.ToString();




                //}
                //msg = MessageParam.SqlValue.ToString();

                return SubProgramId;

            }
        }



        //public string UpdateSubProgram(SubProgramMaster _Model)
        //{
        //    using (GrantAppDBEntities db = new GrantAppDBEntities())
        //    {
        //        string msg = string.Empty;
        //        string msgdtl = string.Empty;
        //        //_Model.OfficeId = 1;
        //        //_Model.GrantTypeId = 1;

        //        _Model.Status = 1;



        //        if (_Model.TimeDurationYear == 1)
        //        {

        //            _Model.BudgetForSecondYear = 0;
        //            _Model.BudgetForThirdYear = 0;
        //        }
        //        if (_Model.TimeDurationYear == 2)
        //        {
        //            _Model.BudgetForThirdYear = 0;
        //        }

        //        var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = _Model.SubProgramId };
        //        var MainSectionIdParam = new SqlParameter { ParameterName = "@MainSectionId", Value = _Model.MainSectionId };
        //        var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = _Model.ProgramId };
        //        var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = _Model.OfficeId };
        //        var SubProgramTitleParam = new SqlParameter { ParameterName = "@SubProgramTitle", Value = _Model.SubProgramTitle };
        //        var TotalBudgetParam = new SqlParameter { ParameterName = "@TotalBudget", Value = _Model.TotalBudget < 0 ? 0 : _Model.TotalBudget };
        //        var ProbableBenefitedPopulationParam = new SqlParameter { ParameterName = "@ProbableBenefitedPopulation", Value = _Model.ProbableBenefitedPopulation < 0 ? 0 : _Model.ProbableBenefitedPopulation };
        //        var TimeDurationYearParam = new SqlParameter { ParameterName = "@TimeDurationYear", Value = _Model.TimeDurationYear < 0 ? 0 : _Model.TimeDurationYear };

        //        var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = _Model.GrantTypeId };
        //        var createdDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };

        //        //this is for complementary grant-provdc amount, ngoingo amount, govn of nepal amount....
        //        var AmountProvinceVdcParam = new SqlParameter { ParameterName = "@AmountProvinceVdc", Value = _Model.AmountProvinceVdc.HasValue ? _Model.AmountProvinceVdc : 0 };
        //        var NGOINGOAmountParam = new SqlParameter { ParameterName = "@NGOINGOAmount", Value = _Model.NGOINGOAmount.HasValue ? _Model.NGOINGOAmount : 0 };
        //        var GovnNepalAmountParam = new SqlParameter { ParameterName = "@GovnNepalAmount", Value = _Model.GovnNepalAmount.HasValue ? _Model.GovnNepalAmount : 0 };

        //        var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = 1 };
        //        var StatusParam = new SqlParameter { ParameterName = "@Status", Value = _Model.Status };
        //        var ProgramPriorityParam = new SqlParameter { ParameterName = "@ProgramPriority", Value = _Model.ProgramPirority };
        //        _Model.TermsAndCondtions = false;
        //        if (!string.IsNullOrEmpty(_Model.FinalDocumentsUrl))
        //        {
        //            _Model.TermsAndCondtions = true;
        //        }

        //        var TermsAndCondtionsParam = new SqlParameter { ParameterName = "@TermsAndCondtions", Value = _Model.TermsAndCondtions };

        //        var FinalDocumentsUrlParam = new SqlParameter { ParameterName = "@FinalDocumentsUrl", Value = _Model.FinalDocumentsUrl == null ? string.Empty : _Model.FinalDocumentsUrl };


        //        var BudgetForFirstYearParam = new SqlParameter { ParameterName = "@BudgetForFirstYear", Value = _Model.BudgetForFirstYear < 0 ? 0 : _Model.BudgetForFirstYear };
        //        var BudgetForSecondYearParam = new SqlParameter { ParameterName = "@BudgetForSecondYear", Value = _Model.BudgetForSecondYear < 0 ? 0 : _Model.BudgetForSecondYear };
        //        var BudgetForThirdYearParam = new SqlParameter { ParameterName = "@BudgetForThirdYear", Value = _Model.BudgetForThirdYear < 0 ? 0 : _Model.BudgetForThirdYear };

        //        var BudgetForFirstYearPujiParam = new SqlParameter { ParameterName = "@BudgetForFirstYearPuji", Value = _Model.PujiFirstYearAmount.HasValue ? _Model.PujiFirstYearAmount : 0 };
        //        var BudgetForSecondYearPujuParam = new SqlParameter { ParameterName = "@BudgetForSecondYearPuji", Value = _Model.PujiSecondYearAmount.HasValue ? _Model.PujiSecondYearAmount : 0 };
        //        var BudgetForThirdYearPujiParam = new SqlParameter { ParameterName = "@BudgetForThirdYearPuji", Value = _Model.PujiThirdYearAmount.HasValue ? _Model.PujiThirdYearAmount : 0 };

        //        var SelectedProgramsParam = new SqlParameter { ParameterName = "@SelectedPrograms", Value = _Model.SelectedPrograms };

        //        var OtherProgramParam = new SqlParameter { ParameterName = "@OtherProgram", Value = _Model.OtherProgram == null ? string.Empty : _Model.OtherProgram };

        //        var AyojanaAddressParam = new SqlParameter { ParameterName = "@AyojanaAddress", Value = _Model.AyojanaAddress };



        //        var MessageParam = new SqlParameter
        //        {
        //            ParameterName = "@Message",
        //            DbType = DbType.String,
        //            Size = 50,
        //            Direction = System.Data.ParameterDirection.Output
        //        };

        //        var result = db.Database.ExecuteSqlCommand("exec UpdateSubProgram @SubProgramId,@MainSectionId,@ProgramId,@OfficeId,@SubProgramTitle,@TotalBudget,@ProbableBenefitedPopulation,@TimeDurationYear,@GrantTypeId,@AmountProvinceVdc,@NGOINGOAmount,@GovnNepalAmount,@CreatedDate,@CreatedBy,@Status,@ProgramPriority,@TermsAndCondtions,@FinalDocumentsUrl,@BudgetForFirstYear,@BudgetForSecondYear,@BudgetForThirdYear,@BudgetForFirstYearPuji,@BudgetForSecondYearPuji,@BudgetForThirdYearPuji,@SelectedPrograms,@OtherProgram,@AyojanaAddress,@Message OUT",

        //        msg = MessageParam.SqlValue.ToString();



        //        return msg;

        //    }
        //}


        public string UpdateSubProgram(SubProgramMaster _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                _Model.Status = 1;

                if (_Model.TimeDurationYear == 1)
                {
                    _Model.BudgetForSecondYear = 0;
                    _Model.BudgetForThirdYear = 0;
                }
                if (_Model.TimeDurationYear == 2)
                {
                    _Model.BudgetForThirdYear = 0;
                }

                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@SubProgramId", _Model.SubProgramId),
            new SqlParameter("@MainSectionId", _Model.MainSectionId),
            new SqlParameter("@ProgramId", _Model.ProgramId),
            new SqlParameter("@OfficeId", _Model.OfficeId),
            new SqlParameter("@SubProgramTitle", _Model.SubProgramTitle),
            new SqlParameter("@TotalBudget", _Model.TotalBudget < 0 ? 0 : _Model.TotalBudget),
            new SqlParameter("@ProbableBenefitedPopulation", _Model.ProbableBenefitedPopulation < 0 ? 0 : _Model.ProbableBenefitedPopulation),
            new SqlParameter("@TimeDurationYear", _Model.TimeDurationYear < 0 ? 0 : _Model.TimeDurationYear),
            new SqlParameter("@GrantTypeId", _Model.GrantTypeId),

            new SqlParameter("@AmountProvinceVdc", _Model.AmountProvinceVdc ?? 0),
            new SqlParameter("@NGOINGOAmount", _Model.NGOINGOAmount ?? 0),
            new SqlParameter("@GovnNepalAmount", _Model.GovnNepalAmount ?? 0),

            new SqlParameter("@CreatedDate", DateTime.Now),
         
         

            new SqlParameter("@CreatedBy", 1),
               new SqlParameter("@ProgramPriority", _Model.ProgramPirority),
            new SqlParameter("@Status", _Model.Status),
         
            new SqlParameter("@TermsAndCondtions", !string.IsNullOrEmpty(_Model.FinalDocumentsUrl)), // Simplified boolean condition
            new SqlParameter("@FinalDocumentsUrl", _Model.FinalDocumentsUrl ?? string.Empty),
            new SqlParameter("@BudgetForFirstYear", _Model.BudgetForFirstYear < 0 ? 0 : _Model.BudgetForFirstYear),
            new SqlParameter("@BudgetForSecondYear", _Model.BudgetForSecondYear < 0 ? 0 : _Model.BudgetForSecondYear),
            new SqlParameter("@BudgetForThirdYear", _Model.BudgetForThirdYear < 0 ? 0 : _Model.BudgetForThirdYear),
            new SqlParameter("@BudgetForFirstYearPuji", _Model.PujiFirstYearAmount ?? 0),
            new SqlParameter("@BudgetForSecondYearPuji", _Model.PujiSecondYearAmount ?? 0),
            new SqlParameter("@BudgetForThirdYearPuji", _Model.PujiThirdYearAmount ?? 0),
            new SqlParameter("@SelectedPrograms", _Model.SelectedPrograms),
            new SqlParameter("@OtherProgram", _Model.OtherProgram ?? string.Empty),
            new SqlParameter("@AyojanaAddress", _Model.AyojanaAddress),
            new SqlParameter
            {
                ParameterName = "@Message",
                DbType = DbType.String,
                Size = 50,
                Direction = ParameterDirection.Output
            }
        };

                // Execute the stored procedure
                db.Database.ExecuteSqlCommand(
                    "exec UpdateSubProgram @SubProgramId, @MainSectionId, @ProgramId, @OfficeId, @SubProgramTitle, @TotalBudget, " +
                    "@ProbableBenefitedPopulation, @TimeDurationYear, @GrantTypeId, @AmountProvinceVdc, @NGOINGOAmount, " +
                    "@GovnNepalAmount, @CreatedDate, @CreatedBy,@ProgramPriority, @Status, @TermsAndCondtions, @FinalDocumentsUrl, " +
                    "@BudgetForFirstYear, @BudgetForSecondYear, @BudgetForThirdYear, @BudgetForFirstYearPuji, @BudgetForSecondYearPuji, " +
                    "@BudgetForThirdYearPuji, @SelectedPrograms, @OtherProgram, @AyojanaAddress, @Message OUT",
                    parameters.ToArray()
                );

                // Retrieve the output message
                msg = parameters.Last().Value.ToString();

                return msg;
            }
        }


        public bool CheckIfFileExists(int[] subProgramIds)
        {
            bool returnStatus = false;
            string folderPath = _folderPath;
            try
            {
               
                    // Fetch file names from the database based on SubProgramIds
                    List<string> fileNames = GetFilesBySubProgramIds(subProgramIds);

                    // If no file names are found, return false
                    if (fileNames == null || fileNames.Count == 0)
                    {
                        return false;
                    }

                    // Check if all files exist
                    foreach (var fileName in fileNames)
                    {
                        string filePath = System.IO.Path.Combine(folderPath, fileName);
                        if (!File.Exists(filePath))
                        {
                            return false; // Return false immediately if any file is missing
                        }
                    }

                    // If all files exist, return true
                    returnStatus = true;
                
            }
            catch (Exception ex)
            {
               
                returnStatus = false;
            }

            return returnStatus;
        }


        public bool FinalDocUrlAndPeshToNpc(int[] SubProgramIds, SubProgramMaster _Model)
        {
            bool ReturnStatus = false;

            try
            {
                using (GrantAppDBEntities db = new GrantAppDBEntities())
                {
                    // Update NewProgramInitiation
                    var pu = db.NewProgramInitiation
                                .FirstOrDefault(x => x.OfficeId == _Model.OfficeId
                                                  && x.GrantType == _Model.GrantTypeId
                                                  && x.ProjectPhase == _Model.PhaseStatus);
                    if (pu != null)
                    {
                        pu.Status = 2;  //submitted to NPC
                        pu.FinalFileDoc = _Model.FinalDocumentsUrl;
                        db.Entry(pu).State = EntityState.Modified;
                    }

                    // Process SubProgramIds in bulk, collect the entities that need to be updated
                    var subProgramsToUpdate = db.SubProgramMaster
                                                 .Where(x => SubProgramIds.Contains(x.SubProgramId))
                                                 .ToList();

                    foreach (var subProgram in subProgramsToUpdate)
                    {
                        subProgram.Status = 2;
                        subProgram.FinalDocumentsUrl = _Model.FinalDocumentsUrl;
                        db.Entry(subProgram).State = EntityState.Modified;
                    }

                    // Save all changes at once
                    int changes = db.SaveChanges();
                    ReturnStatus = changes > 0; // Returns true if changes were made, false otherwise
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Example: Log.Error(ex, "Error in FinalDocUrlAndPeshToNpc");
                ReturnStatus = false;
            }

            return ReturnStatus;
        }


        public bool CancelSubmission(int[] SubProgramIds, SubProgramMaster _Model)
        {
            bool ReturnStatus = false;

            try
            {
                using (GrantAppDBEntities db = new GrantAppDBEntities())
                {
                    // Update NewProgramInitiation
                    var pu = db.NewProgramInitiation
                                .FirstOrDefault(x => x.OfficeId == _Model.OfficeId
                                                  && x.GrantType == _Model.GrantTypeId
                                                  && x.ProjectPhase == _Model.PhaseStatus);
                    if (pu != null)
                    {
                        pu.Status = 1;  //submitted to NPC
                        pu.FinalFileDoc = null;
                        db.Entry(pu).State = EntityState.Modified;
                    }

                    // Process SubProgramIds in bulk, collect the entities that need to be updated
                    var subProgramsToUpdate = db.SubProgramMaster
                                                 .Where(x => SubProgramIds.Contains(x.SubProgramId))
                                                 .ToList();

                    foreach (var subProgram in subProgramsToUpdate)
                    {
                        subProgram.Status = 1;
                        subProgram.FinalDocumentsUrl = null;
                        db.Entry(subProgram).State = EntityState.Modified;
                    }

                    // Save all changes at once
                    int changes = db.SaveChanges();
                    ReturnStatus = changes > 0; // Returns true if changes were made, false otherwise
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Example: Log.Error(ex, "Error in FinalDocUrlAndPeshToNpc");
                ReturnStatus = false;
            }

            return ReturnStatus;
        }


        public bool UpdateSubProgramStatus(int SubProgramId, int ProgramId)
        {
            //bool ReturnStatus = false;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Data = db.SubProgramMaster.SingleOrDefault(x => x.SubProgramId == SubProgramId && x.ProgramId == ProgramId);
                if (Data != null)
                {
                    Data.Status = 2;

                }

                db.Entry(Data).State = EntityState.Modified;
                int i = db.SaveChanges();

                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }

        }

        public string UpdateSubProgramStatusSP(int SubProgramId, int ProgramId, string FileNameUrl, bool TermsAndCondtion, int ChangeStatus)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = ProgramId };
                var FileNameUrlParam = new SqlParameter { ParameterName = "@FileNameUrl", Value = FileNameUrl == null ? string.Empty : FileNameUrl };
                var TermsAndCondtionParam = new SqlParameter { ParameterName = "@TermsAndCondtion", Value = TermsAndCondtion };
                var ChangeStatusParam = new SqlParameter { ParameterName = "@ChangeStatus", Value = ChangeStatus };


                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec ChangeSubProgramStatus @SubProgramId,@ProgramId,@FileNameUrl,@TermsAndCondtion,@ChangeStatus,@Message OUT",
                    SubProgramIdParam, ProgramIdParam, FileNameUrlParam, TermsAndCondtionParam, ChangeStatusParam, MessageParam);
                //int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;
            }

        }


        public string UpdateSubProgramCancelStatus(int SubProgramId, int OfficeId, string Remarks, int? FYID, string CancelledDocuments, string CancelledDocuments1)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var FYIDParam = new SqlParameter { ParameterName = "@FYID", Value = FYID };
                var CancelledRemarksParam = new SqlParameter { ParameterName = "@CancelledRemarks", Value = Remarks == string.Empty ? string.Empty : Remarks };
                var CancelledDocumentsParam = new SqlParameter { ParameterName = "@CancelledDocuments", Value = CancelledDocuments == string.Empty ? string.Empty : CancelledDocuments };
                var CancelledDocumentsParam1 = new SqlParameter { ParameterName = "@CancelledDocuments1", Value = CancelledDocuments1 == string.Empty ? string.Empty : CancelledDocuments1 };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec ChangeIsCancelledStatus @SubProgramId,@OfficeId,@FYID,@CancelledRemarks,@CancelledDocuments,@CancelledDocuments1,@Message OUT",
                    SubProgramIdParam, OfficeIdParam, FYIDParam, CancelledRemarksParam, CancelledDocumentsParam, CancelledDocumentsParam1, MessageParam);
                //int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;
            }

        }



        public bool GetSubmitStatusOfSubProgramMaster(int SubProgramMasterId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Result = db.SubProgramMaster.SingleOrDefault(x => x.SubProgramId == SubProgramMasterId);
                if (Result != null)
                {
                    int Status = Result.Status;
                    if (Status > 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public int GetCurrentStatusOfSubProgram(int SubProgramId)
        {
            int ReturnVal = 1;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Result = db.SubProgramMaster.SingleOrDefault(x => x.SubProgramId == SubProgramId);
                if (Result != null)
                {
                    ReturnVal = Result.Status;
                }


            }

            return ReturnVal;
        }
        public string GetFinalFileNameBySubProgramID(int SubProgramId)
        {
            string FinalFileName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                FinalFileName = db.Database.SqlQuery<string>("select isnull(FinalDocumentsUrl,'Empty') as UploadedFile From SubProgramMaster where SubProgramId=@id", new SqlParameter("@id", SubProgramId)).FirstOrDefault();

            }

            return FinalFileName;
        }


        public List<string> GetFilesBySubProgramIds(int[] subProgramIds)
        {
            if (subProgramIds == null || subProgramIds.Length == 0)
            {
                return new List<string>(); // Return empty list if no IDs provided
            }

            // Create dynamic parameter names (@id0, @id1, ...)
            string paramPlaceholders = string.Join(",", subProgramIds.Select((id, index) => $"@id{index}"));

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                string query = $"SELECT UploadUrl FROM dbo.DocumentRequirementsUpload WHERE SubprogramId IN ({paramPlaceholders})";

           
                List<SqlParameter> parameters = new List<SqlParameter>();
                for (int i = 0; i < subProgramIds.Length; i++)
                {
                    parameters.Add(new SqlParameter($"@id{i}", subProgramIds[i]));
                }

                // Execute query
                List<string> fileNames = db.Database.SqlQuery<string>(query, parameters.ToArray()).ToList();

                return fileNames;
            }
            return new List<string>();



        }

        #endregion

        #region for admin

        public List<SubProgramListModelForAdmin> PopulateSubProgramListForAdmin(int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramListModelForAdmin> SubProgramSetupList = new List<SubProgramListModelForAdmin>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                SubProgramSetupList = db.Database.SqlQuery<SubProgramListModelForAdmin>("PopulateSubProgramForAdmin @GrantTypeId", GrantTypeIdParam).ToList();
                return SubProgramSetupList;
            }
        }

        public string DeleteSubProgramMaster(SubProgramMaster model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = model.SubProgramId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };




                var result = db.Database.ExecuteSqlCommand("exec DeleteSubProgramMater @SubProgramId,@Message OUT",
                    SubProgramIdParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;



            }
        }


        #endregion


        //point 
        public List<PopulatePointsVariableListViewModel> PopulatePointsVariableList(int SubProgramMasterId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<PopulatePointsVariableListViewModel> PopulatePointsVariableListViewModelList = new List<PopulatePointsVariableListViewModel>();
                var SubProgramMasterIdParam = new SqlParameter { ParameterName = "@SubProgramMasterId", Value = SubProgramMasterId };
                PopulatePointsVariableListViewModelList = db.Database.SqlQuery<PopulatePointsVariableListViewModel>("PopulatePointsVariableList @SubProgramMasterId", SubProgramMasterIdParam).ToList();
                return PopulatePointsVariableListViewModelList;
            }
        }
        public int GetVariableDetailsID(int TotalCountVal)
        {
            int GetVariableDetailsID = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                int TotalCount = db.Database.SqlQuery<int>("select VariableDetailid as VariableDetailid from GrantVariablesDetail where '" + TotalCountVal + "' between MinValue and MaxValue").FirstOrDefault();
                if (TotalCount > 0)
                {
                    GetVariableDetailsID = TotalCount;
                }
            }

            return GetVariableDetailsID;
        }


        #region Program Condtion sharts
        public List<ProgramConditionsViewModel> PopulateProgramConditionsList(int GrantTypeId, int CurrentLoginUserType)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (CurrentLoginUserType == 4)//for local level
                {
                    List<ProgramConditionsViewModel> ProgramConditionlist = new List<ProgramConditionsViewModel>();
                    var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                    ProgramConditionlist = db.Database.SqlQuery<ProgramConditionsViewModel>("PopulateProgramConditionsList @GrantTypeId", GrantTypeIdParam).ToList();
                    return ProgramConditionlist;
                }
                else//For Province
                {
                    List<ProgramConditionsViewModel> ProgramConditionlist = new List<ProgramConditionsViewModel>();
                    var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                    ProgramConditionlist = db.Database.SqlQuery<ProgramConditionsViewModel>("PopulateProgramConditionsListForProvince @GrantTypeId", GrantTypeIdParam).ToList();
                    return ProgramConditionlist;
                }

            }
        }

        public List<ProgramConditionsViewModel> PopulateProgramConditionsListForEdit(int SubProgramId, int OfficeTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (OfficeTypeId == 2)
                {
                    List<ProgramConditionsViewModel> ProgramConditionlist = new List<ProgramConditionsViewModel>();
                    var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                    ProgramConditionlist = db.Database.SqlQuery<ProgramConditionsViewModel>("PopulateProgramConditionsListForEditForProvince @SubProgramId", SubProgramIdParam).ToList();
                    return ProgramConditionlist;
                }
                else
                {
                    List<ProgramConditionsViewModel> ProgramConditionlist = new List<ProgramConditionsViewModel>();
                    var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                    ProgramConditionlist = db.Database.SqlQuery<ProgramConditionsViewModel>("PopulateProgramConditionsListForEdit @SubProgramId", SubProgramIdParam).ToList();
                    return ProgramConditionlist;
                }

            }
        }
        public List<DocumentsRequirementsViewModel> SPUP_PopulateDocRequirementList(int GrantTypeId, int UserType)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<DocumentsRequirementsViewModel> ProgramConditionlist = new List<DocumentsRequirementsViewModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var UserTypeIdParam = new SqlParameter { ParameterName = "@UserTypeId", Value = UserType };

                ProgramConditionlist = db.Database.SqlQuery<DocumentsRequirementsViewModel>("SPUP_PopulateDocRequirementList @GrantTypeId,@UserTypeId", GrantTypeIdParam, UserTypeIdParam).ToList();
                return ProgramConditionlist;
            }
        }
        public List<DocumentsRequirementsViewModel> SPUP_PopulateRequiredDocForEdit(int SubProgramId, int UserType)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<DocumentsRequirementsViewModel> ProgramConditionlist = new List<DocumentsRequirementsViewModel>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var UserTypeIdParam = new SqlParameter { ParameterName = "@UserTypeId", Value = UserType };

                ProgramConditionlist = db.Database.SqlQuery<DocumentsRequirementsViewModel>("SPUP_PopulateRequiredDocForEdit @SubProgramId,@UserTypeId", SubProgramIdParam, UserTypeIdParam).ToList();
                return ProgramConditionlist;
            }
        }


        public string InsertProgramConditionDetail(SubProgramMaster _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                if (_Model.ProgramConditionsViewModelList.Count > 0)
                {
                    foreach (var item in _Model.ProgramConditionsViewModelList)
                    {

                        var ProgramConditionIdParam = new SqlParameter { ParameterName = "@ProgramConditionId", Value = item.ProgramConditionID };
                        var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = _Model.SubProgramId };
                        var IsCheckedParam = new SqlParameter { ParameterName = "@IsChecked", Value = item.IsCheck };
                        var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadFileUrl", Value = item.UploadFileUrl == null ? string.Empty : item.UploadFileUrl };


                        var MessageParam = new SqlParameter
                        {
                            ParameterName = "@Message",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };


                        var result = db.Database.ExecuteSqlCommand("exec InsertProgramConditionDetails @ProgramConditionId, @SubProgramId,@IsChecked,@UploadFileUrl,@Message OUT",

                           ProgramConditionIdParam, SubProgramIdParam, IsCheckedParam, UploadFileUrlParam, MessageParam);

                        msg = MessageParam.SqlValue.ToString();



                    }
                }

                return msg;


            }
        }


        public string InsertProgramConditionDetailList(int ProgramConditionId, int SubProgramId, bool IsChecked, string UploadFileUrl)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;



                var ProgramConditionIdParam = new SqlParameter { ParameterName = "@ProgramConditionId", Value = ProgramConditionId };
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var IsCheckedParam = new SqlParameter { ParameterName = "@IsChecked", Value = IsChecked };
                var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadFileUrl", Value = UploadFileUrl == null ? string.Empty : UploadFileUrl };


                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec InsertProgramConditionDetails @ProgramConditionId, @SubProgramId,@IsChecked,@UploadFileUrl,@Message OUT",

                   ProgramConditionIdParam, SubProgramIdParam, IsCheckedParam, UploadFileUrlParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;

            }
        }

        public string SPUP_InsertProgramConditionDetails(int ProgramConditionId, int SubProgramId, string UploadFileUrl, int PhaseStatus)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var RequiredDocumentsIdParam = new SqlParameter { ParameterName = "@RequiredDocumentsId", Value = ProgramConditionId };
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadFileUrl", Value = UploadFileUrl == null ? string.Empty : UploadFileUrl };
                var PhaseStatusParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseStatus };


                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec SPUP_InsertProgramConditionDetails @RequiredDocumentsId, @SubProgramId,@UploadFileUrl,@PhaseStatus,@Message OUT",

                   RequiredDocumentsIdParam, SubProgramIdParam, UploadFileUrlParam, PhaseStatusParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;

            }
        }

        public string SPUP_InsertMissingDocumnet(int ProgramConditionId, int SubProgramId, string UploadFileUrl, int PhaseStatus, string UploaderName , string UploaderPosition)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var RequiredDocumentsIdParam = new SqlParameter { ParameterName = "@RequiredDocumentsId", Value = ProgramConditionId };
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadFileUrl", Value = UploadFileUrl == null ? string.Empty : UploadFileUrl };
                var PhaseStatusParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseStatus };
                var UploaderNameParam = new SqlParameter { ParameterName = "@UploaderOfficerName", Value = UploaderName };
                var UploaderPositionParam = new SqlParameter { ParameterName = "@UploadderOfficerPosition", Value = UploaderPosition };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec SPUP_InsertMissingDocumnet @RequiredDocumentsId, @SubProgramId,@UploadFileUrl,@PhaseStatus,@UploaderOfficerName,@UploadderOfficerPosition,@Message OUT",

                   RequiredDocumentsIdParam, SubProgramIdParam, UploadFileUrlParam, PhaseStatusParam,UploaderNameParam,UploaderPositionParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;

            }
        }


        

        public bool CheckIfAlreadyInsertedInCondictionDetails(int SubProgramId)
        {
            bool AlreadyInserted = false;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                int AlreadyIns = db.Database.SqlQuery<int>("select count(*) From ProgramConditionsDetail where SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                            .FirstOrDefault();
                if (AlreadyIns > 0)
                {
                    AlreadyInserted = true;
                }
            }

            return AlreadyInserted;
        }

        public bool CheckIfUploadFileInsertedOrNot(int SubProgramId, int GrantTypeId)
        {
            bool AlreadyInserted = false;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (GrantTypeId == 1)
                {
                    int AlreadyIns = db.Database.SqlQuery<int>("select count(*) as total from ProgramConditionsDetail where ISNULL(LTRIM(RTRIM(UploadFileUrl)),'')<>'' and ProgramConditionId=7 and SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                            .FirstOrDefault();
                    if (AlreadyIns > 0)
                    {
                        AlreadyInserted = true;
                    }
                }
                else
                {
                    int AlreadyIns = db.Database.SqlQuery<int>("select count(*) as total from ProgramConditionsDetail where ISNULL(LTRIM(RTRIM(UploadFileUrl)),'')<>'' and ProgramConditionId=30 and SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                            .FirstOrDefault();
                    if (AlreadyIns > 0)
                    {
                        AlreadyInserted = true;
                    }
                }

            }

            return AlreadyInserted;
        }


        public bool CheckIfUploadFileInsertedOrNotUpdated(int SubProgramId, int GrantTypeId, int UserTypeId)
        {
            bool AlreadyInserted = false;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (GrantTypeId == 1)
                {
                    int AlreadyIns = 0;
                    if (UserTypeId == 2)//province

                    {
                        AlreadyIns = db.Database.SqlQuery<int>(@"select count(*) as total from DocumentRequirementsUpload  where ISNULL(LTRIM(RTRIM(UploadUrl)),'')<>'' and SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                       .FirstOrDefault();
                        if (AlreadyIns >= 4)
                        {
                            AlreadyInserted = true;
                        }
                    }
                    else
                    {
                        AlreadyIns = db.Database.SqlQuery<int>(@"select count(*) as total from DocumentRequirementsUpload  where ISNULL(LTRIM(RTRIM(UploadUrl)),'')<>'' and SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                       .FirstOrDefault();
                        if (AlreadyIns >= 3)
                        {
                            AlreadyInserted = true;
                        }
                    }




                }
                else
                {
                    int AlreadyIns = 0;
                    if (UserTypeId == 2)//province

                    {
                        AlreadyIns = db.Database.SqlQuery<int>(@"select count(*) as total from DocumentRequirementsUpload  where ISNULL(LTRIM(RTRIM(UploadUrl)),'')<>'' and SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                       .FirstOrDefault();
                        if (AlreadyIns >= 4)
                        {
                            AlreadyInserted = true;
                        }
                    }
                    else
                    {
                        AlreadyIns = db.Database.SqlQuery<int>(@"select count(*) as total from DocumentRequirementsUpload  where ISNULL(LTRIM(RTRIM(UploadUrl)),'')<>'' and SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                       .FirstOrDefault();
                        if (AlreadyIns >= 3)
                        {
                            AlreadyInserted = true;
                        }
                    }
                }

            }

            return AlreadyInserted;
        }





        public string DeleteProgramConditionDetails(int SubProgramID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var DelSubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramID };
                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var delresult = db.Database.ExecuteSqlCommand("exec DeleteProgramConditionDetails @SubProgramId,@Message OUT",
                        DelSubProgramIdParam, DelMessageParam);

                msg = DelMessageParam.SqlValue.ToString();

                return msg;

            }
        }


        public string SP_UPDeleteRequiredDocumentsUpload(int SubProgramID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var DelSubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramID };
                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var delresult = db.Database.ExecuteSqlCommand("exec SP_UPDeleteRequiredDocumentsUpload @SubProgramId,@Message OUT",
                        DelSubProgramIdParam, DelMessageParam);
                msg = DelMessageParam.SqlValue.ToString();

                return msg;

            }
        }

        #endregion

        #region Fifteen Yojana
        public List<FifteenYojanaDetails> PopulateFifteenYojanaTitle(int SubProgramID, int RefGrantVarialbeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<FifteenYojanaDetails> FifteenYojanaDetailsList = new List<FifteenYojanaDetails>();
                var SubProgramIDParam = new SqlParameter { ParameterName = "@SubProgramID", Value = SubProgramID };
                var RefGrantVarialbeIdParam = new SqlParameter { ParameterName = "@RefGrantVarialbeId", Value = RefGrantVarialbeId };
                FifteenYojanaDetailsList = db.Database.SqlQuery<FifteenYojanaDetails>("PopulateFifteenYojanaTitle @SubProgramID,@RefGrantVarialbeId", SubProgramIDParam, RefGrantVarialbeIdParam).ToList();
                return FifteenYojanaDetailsList;
            }
        }

        public List<FifteenYojanaDetails> PopulateFifteenYojana(int SubProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<FifteenYojanaDetails> FifteenYojanaDetailsList = new List<FifteenYojanaDetails>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                FifteenYojanaDetailsList = db.Database.SqlQuery<FifteenYojanaDetails>("PopulateFifteenYojana @SubProgramId", SubProgramIdParam).ToList();
                return FifteenYojanaDetailsList;
            }
        }

        public bool IsAlreadyInsertedInFifteenYojanaDetails(int SubProgramId)
        {
            bool Returnval = false;
            SubProgramMaster model = new SubProgramMaster();
            model.FifteenYojanaDetailsList = PopulateFifteenYojana(SubProgramId);
            int Count = model.FifteenYojanaDetailsList.Count;
            if (Count > 0)
            {
                Returnval = true;
            }
            else
            {
                Returnval = false;
            }
            return Returnval;

        }

        public string InsertFifteenYojanaDetails(SubProgramMaster _Model)
        {
            DeleteFifteenYojanaDetails(_Model);

            int TotalTrueValue = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                string msg = string.Empty;
                string msgSub = string.Empty;
                if (_Model.FifteenYojanaDetailsList.Count > 0)
                {
                    foreach (var item in _Model.FifteenYojanaDetailsList)
                    {
                        //if(item.IsChecked==true)
                        //{
                        //    TotalTrueValue += 1;
                        //}

                        var OptionValueParam = new SqlParameter { ParameterName = "@OptionValue", Value = item.OptionValue };
                        var IsCheckedParam = new SqlParameter { ParameterName = "@IsChecked", Value = item.IsChecked };

                        var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = item.SubProgramId };
                        var RefGrantVariableIdParam = new SqlParameter { ParameterName = "@RefGrantVariableId", Value = item.RefGrantVariableId };

                        var MessageParam = new SqlParameter
                        {
                            ParameterName = "@Message",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        var result = db.Database.ExecuteSqlCommand("exec InsertFifteenYojanaDetails @OptionValue, @IsChecked,@SubProgramId,@RefGrantVariableId,@Message OUT",
                            OptionValueParam, IsCheckedParam, SubProgramIdParam, RefGrantVariableIdParam, MessageParam);

                        msg = MessageParam.SqlValue.ToString();



                    }

                    var SubProgramIdVarParam = new SqlParameter { ParameterName = "@SubProgramIdVar", Value = _Model.SubProgramId };
                    var RefGrantVariableIdvarParam = new SqlParameter { ParameterName = "@RefGrantVariableIdvar", Value = _Model.ObjFifteenYojanaDetails.RefGrantVariableId };


                    var MessageParamSubVariable = new SqlParameter
                    {
                        ParameterName = "@MessageSubVar",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    var resultSub = db.Database.ExecuteSqlCommand("exec InsertSubProgramVariableByFifteenYojana @SubProgramIdVar, @RefGrantVariableIdvar,@MessageSubVar OUT",
                      SubProgramIdVarParam, RefGrantVariableIdvarParam, MessageParamSubVariable);

                    msgSub = MessageParamSubVariable.SqlValue.ToString();

                }

                return msg;



            }
        }

        public string DeleteFifteenYojanaDetails(SubProgramMaster model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = model.SubProgramId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteFifteenYojanaDetails @SubProgramId,@Message OUT",
                    SubProgramIdParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;



            }
        }


        public int CountTotalNumberOfFifteenYojana(int SubProgramId)
        {
            int Totalnumber = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                int TotalCount = db.Database.SqlQuery<int>("select count(*) total From FifteenYojanaDetails where IsChecked=1 and SubProgramId=@id", new SqlParameter("@id", SubProgramId))
                            .FirstOrDefault();
                if (TotalCount > 0)
                {
                    Totalnumber = TotalCount;
                }
            }

            return Totalnumber;
        }

        #endregion



        #region Program Wise Amount Details
        public string InsertProgramWiseAmountDetails(ProgramwiseAmountViewModel model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = model.ProgramId };
                var AmountParam = new SqlParameter { ParameterName = "@Amount", Value = model.Amount };
                var AmountSecondYearParam = new SqlParameter { ParameterName = "@AmountSecondYear ", Value = model.AmountSecondYear.HasValue ? model.AmountSecondYear : 0 };
                var AmountThirdYearParam = new SqlParameter { ParameterName = "@AmountThirdYear ", Value = model.AmountThirdYear.HasValue ? model.AmountThirdYear : 0 };

                var RemarksParam = new SqlParameter { ParameterName = "@Remarks", Value = model.Remarks == null ? string.Empty : model.Remarks };
                var CreatedDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };
                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = true };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec InsertProgramwiseAmountValue @ProgramId, @Amount, @AmountSecondYear, @AmountThirdYear, @Remarks,@CreatedDate,@Status,@Message OUT",

                   ProgramIdParam, AmountParam, AmountSecondYearParam, AmountThirdYearParam, RemarksParam, CreatedDateParam, StatusParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;

            }
        }

        public int IsAlreadyInsertedIntoProgramWiseAmount(int ProgramId)
        {
            int TotalCountValue = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                int TotalCount = db.Database.SqlQuery<int>("select count(*) as TotalCount from ProgramwiseAmount where ProgramId='" + ProgramId + "'").FirstOrDefault();
                if (TotalCount > 0)
                {
                    TotalCountValue = TotalCount;
                }
            }

            return TotalCountValue;
        }


        public decimal AmountDemandByProgram(int ProgramId)
        {
            decimal TotalDemandAmount = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                decimal DemandAmount = db.Database.SqlQuery<decimal>("select TotalBudget as DemandAmount from SubProgramMaster where SubProgramId='" + ProgramId + "'").FirstOrDefault();
                if (DemandAmount > 0)
                {
                    TotalDemandAmount = DemandAmount;
                }
            }

            return TotalDemandAmount;
        }


        public string ProgramTitleByProgramId(int ProgramId)
        {
            string Title = "";
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                string ProgramTitle = db.Database.SqlQuery<string>("select SubProgramTitle as ProgramTitle from SubProgramMaster where SubProgramId='" + ProgramId + "'").FirstOrDefault();
                Title = ProgramTitle;
            }

            return Title;
        }






        public ProgramwiseAmountViewModel PopulateProgramwiseAmountByProgramId(int ProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                ProgramwiseAmountViewModel model = new ProgramwiseAmountViewModel();
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = ProgramId };
                model = db.Database.SqlQuery<ProgramwiseAmountViewModel>("PopulateProgramwiseAmountByProgramId @ProgramId", ProgramIdParam).FirstOrDefault();
                return model;
            }
        }

        #endregion



        #region progress detail
        //public string InsertQuadrimesterReportDetail(QuadrimesterReportsDetailViewModel model)
        //{
        //    using (GrantAppDBEntities db = new GrantAppDBEntities())
        //    {
        //        model.TargetedMaterial = "0";
        //        model.TargetedFinance = 0m;

        //        string msg = string.Empty;
        //        var QuadrimesterReportsDetailIdParam = new SqlParameter { ParameterName = "@QuadrimesterReportsDetailId", Value = model.QuadrimesterReportsDetailId };
        //        var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
        //        var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = model.ProgramId };
        //        var ApprovedBudgetParam = new SqlParameter { ParameterName = "@ApprovedBudget", Value = model.ApprovedBudget };
        //        var ProgramConductPlaceParam = new SqlParameter { ParameterName = "@ProgramConductPlace", Value = model.ProgramConductPlace };

        //        var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = model.QuadrimesterId };
        //        var TargetedMaterialParam = new SqlParameter { ParameterName = "@TargetedMaterial", Value = model.TargetedMaterial };
        //        var TargetedFinanceParam = new SqlParameter { ParameterName = "@TargetedFinance", Value = model.TargetedFinance };
        //        var AchievementMaterialParam = new SqlParameter { ParameterName = "@AchievementMaterial", Value = model.AchievementMaterial };

        //        var AchievementFinanceParam = new SqlParameter { ParameterName = "@AchievementFinance", Value = model.AchievementFinance };
        //        var RemarksParam = new SqlParameter { ParameterName = "@Remarks", Value = model.Remarks == null ? string.Empty : model.Remarks };
        //        var PreparedByParam = new SqlParameter { ParameterName = "@PreparedBy", Value = model.PreparedBy };
        //        var PreparedDateParam = new SqlParameter { ParameterName = "@PreparedDate", Value = DateTime.Now };

        //        var ApprovedByParam = new SqlParameter { ParameterName = "@ApprovedBy", Value = model.ApprovedBy };
        //        var ApprovedDateParam = new SqlParameter { ParameterName = "@ApprovedDate", Value = DateTime.Now };

        //        var StatusParam = new SqlParameter { ParameterName = "@Status", Value = true };
        //        var IsLockedParam = new SqlParameter { ParameterName = "@IsLocked", Value = false };
        //        var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
        //        var IsContactNoticeIssuedParam = new SqlParameter { ParameterName = "@IsContactNoticeIssued", Value = model.IsContactNoticeIssued };
        //        var IsContractDoneParam = new SqlParameter { ParameterName = "@IsContractDone", Value = model.IsContractDone };
        //        var IsFirstInstallmentTakenParam = new SqlParameter { ParameterName = "@IsFirstInstallmentTaken", Value = model.IsFirstInstallmentTaken };
        //        var ProjectFileUploadParam = new SqlParameter { ParameterName = "@ProjectFileUpload", Value = model.ProjectFileUpload == null ? string.Empty : model.ProjectFileUpload };
        //        var PictureOfProjectOneParam = new SqlParameter { ParameterName = "@PictureOfProjectOne", Value = model.PictureOfProjectOne == null ? string.Empty : model.PictureOfProjectOne };
        //        var PictureOfProjectTwoParam = new SqlParameter { ParameterName = "@PictureOfProjectTwo", Value = model.PictureOfProjectTwo == null ? string.Empty : model.PictureOfProjectTwo };
        //        var PictureOfProjectThreeParam = new SqlParameter { ParameterName = "@PictureOfProjectThree", Value = model.PictureOfProjectThree == null ? string.Empty : model.PictureOfProjectThree };
        //        var IsNikashaMaagParam = new SqlParameter { ParameterName = "@IsNikashaMaag", Value = model.IsNikashaMaag.HasValue ? model.IsNikashaMaag : false };
        //        var NikasaMaagFileUploadParam = new SqlParameter { ParameterName = "@NikasaMaagFileUpload", Value = model.NikasaMaagFileUpload == null ? string.Empty : model.NikasaMaagFileUpload };

        //        var ContractNoticeFileParam = new SqlParameter { ParameterName = "@ContractNoticeFile", Value = model.ContractNoticeFile == null ? string.Empty : model.ContractNoticeFile };
        //        var ContractFileParam = new SqlParameter { ParameterName = "@ContractFile", Value = model.ContractFile == null ? string.Empty : model.ContractFile };
        //        var BhuktaniFileParam = new SqlParameter { ParameterName = "@BhuktaniFile", Value = model.BhuktaniFile == null ? string.Empty : model.BhuktaniFile };
        //        var RunningBillFileParam = new SqlParameter { ParameterName = "@RunningBillFile", Value = model.RunningBillFile == null ? string.Empty : model.RunningBillFile };
        //        var TimeExtendedFileParam = new SqlParameter { ParameterName = "@TimeExtendedFile", Value = model.TimeExtendedFile == null ? string.Empty : model.TimeExtendedFile };
        //        var AppRunningStatusParam = new SqlParameter { ParameterName = "@AppRunningStatus", Value = model.AppRunningStatus.HasValue ? model.AppRunningStatus : 0 };
        //        var NotRunningProofDocParam = new SqlParameter { ParameterName = "@NotRunningProofDoc", Value = model.NotRunningProofDoc == null ? string.Empty : model.NotRunningProofDoc };
        //        var ReportOfFisalYearEndParam = new SqlParameter { ParameterName = "@ReportOfFisalYearEnd", Value = model.ReportOfFisalYearEnd.HasValue ? model.ReportOfFisalYearEnd : 0 };

        //        var SuchanaPatiFileParam = new SqlParameter { ParameterName = "@SuchanaPatiFile", Value = model.SuchanaPatiUpload == null ? string.Empty : model.SuchanaPatiUpload };

        //        var SarbajanikParikchenFileParam = new SqlParameter { ParameterName = "@SarbajanikParikchenFile", Value = model.SarbajanikParikchen == null ? string.Empty : model.SarbajanikParikchen };

        //        var SarbajanikSunuwaiFileParam = new SqlParameter { ParameterName = "@SarbajanikSunuwaiFile", Value = model.SarbajanikSunuwai == null ? string.Empty : model.SarbajanikSunuwai };

        //        var SamapurakAnusuchi5FileParam = new SqlParameter { ParameterName = "@SamapurakAnusuchi5File", Value = model.SamapurakAnusuchi5 == null ? string.Empty : model.SamapurakAnusuchi5 };

        //        var BisehsAnusuchi7FileParam = new SqlParameter { ParameterName = "@BisehsAnusuchi7File", Value = model.BiseshAnusuchi7 == null ? string.Empty : model.BiseshAnusuchi7 };


        //        if (!model.TotalAmountUsed.HasValue)
        //        {
        //            model.TotalAmountUsed = 0;
        //        }
        //        if (!model.TotalNikashaRamam.HasValue)
        //        {
        //            model.TotalNikashaRamam = 0;
        //        }
        //        if (!model.TotalContractAmount.HasValue)
        //        {
        //            model.TotalContractAmount = 0;
        //        }

        //        var TotalAmountUsedParam = new SqlParameter { ParameterName = "@TotalAmountUsed", Value = model.TotalAmountUsed };
        //        var TotalContractAmountParam = new SqlParameter { ParameterName = "@TotalContractAmount", Value = model.TotalContractAmount };
        //        var TotalNikashaRamamParam = new SqlParameter { ParameterName = "@TotalNikashaRamam", Value = model.TotalNikashaRamam };


        //        var MessageParam = new SqlParameter
        //        {
        //            ParameterName = "@Message",
        //            DbType = DbType.String,
        //            Size = 50,
        //            Direction = System.Data.ParameterDirection.Output
        //        };

        //        var result = db.Database.ExecuteSqlCommand("exec InsertQuadrimesterReportDetail @QuadrimesterReportsDetailId,@OfficeId,@ProgramId,@ApprovedBudget,@ProgramConductPlace,@QuadrimesterId,@TargetedMaterial,@TargetedFinance,@AchievementMaterial,@AchievementFinance,@Remarks,@PreparedBy,@PreparedDate,@ApprovedBy,@ApprovedDate,@Status,@IsLocked,@FiscalYearId,@IsContactNoticeIssued,@IsContractDone,@IsFirstInstallmentTaken,@ProjectFileUpload,@PictureOfProjectOne,@PictureOfProjectTwo,@PictureOfProjectThree,@IsNikashaMaag,@NikasaMaagFileUpload,@TotalAmountUsed,@TotalContractAmount,@TotalNikashaRamam,@ContractNoticeFile,@ContractFile,@BhuktaniFile,@RunningBillFile,@TimeExtendedFile,@AppRunningStatus,@NotRunningProofDoc,@ReportOfFisalYearEnd,@SuchanaPatiFile,@SarbajanikSunuwaiFile,@SamapurakAnusuchi5File,@BisehsAnusuchi7File,@Message OUT",
        //           QuadrimesterReportsDetailIdParam, OfficeIdParam, ProgramIdParam, ApprovedBudgetParam, ProgramConductPlaceParam, QuadrimesterIdParam, TargetedMaterialParam, TargetedFinanceParam, AchievementMaterialParam, AchievementFinanceParam, RemarksParam, PreparedByParam, PreparedDateParam, ApprovedByParam, ApprovedDateParam, StatusParam, IsLockedParam, FiscalYearIdParam, IsContactNoticeIssuedParam, IsContractDoneParam, IsFirstInstallmentTakenParam, ProjectFileUploadParam, PictureOfProjectOneParam, PictureOfProjectTwoParam, PictureOfProjectThreeParam, IsNikashaMaagParam, NikasaMaagFileUploadParam, TotalAmountUsedParam, TotalContractAmountParam, TotalNikashaRamamParam, ContractNoticeFileParam, ContractFileParam, BhuktaniFileParam, RunningBillFileParam, TimeExtendedFileParam, AppRunningStatusParam, NotRunningProofDocParam,ReportOfFisalYearEndParam,SuchanaPatiFileParam,SarbajanikSunuwaiFileParam,SarbajanikParikchenFileParam,SamapurakAnusuchi5FileParam,BisehsAnusuchi7FileParam,MessageParam);

        //        msg = MessageParam.SqlValue.ToString();
        //        return msg;

        //    }
        //}


        public string InsertQuadrimesterReportDetail(QuadrimesterReportsDetailViewModel model)
        {
            using (var db = new GrantAppDBEntities())
            {
                model.TargetedMaterial = "0";
                model.TargetedFinance = 0m;

                if (!model.TotalAmountUsed.HasValue)
                {
                    model.TotalAmountUsed = 0;
                }
                if (!model.TotalNikashaRamam.HasValue)
                {
                    model.TotalNikashaRamam = 0;
                }
                if (!model.TotalContractAmount.HasValue)
                {
                    model.TotalContractAmount = 0;
                }

                var parameters = new[]
                {
            new SqlParameter("@QuadrimesterReportsDetailId", model.QuadrimesterReportsDetailId),
            new SqlParameter("@OfficeId", model.OfficeId),
            new SqlParameter("@ProgramId", model.ProgramId),
            new SqlParameter("@ApprovedBudget", model.ApprovedBudget),
            new SqlParameter("@ProgramConductPlace", model.ProgramConductPlace),
            new SqlParameter("@QuadrimesterId", model.QuadrimesterId),
            new SqlParameter("@TargetedMaterial", model.TargetedMaterial),
            new SqlParameter("@TargetedFinance", model.TargetedFinance),
            new SqlParameter("@AchievementMaterial", model.AchievementMaterial),
            new SqlParameter("@AchievementFinance", model.AchievementFinance),
            new SqlParameter("@Remarks", model.Remarks ?? string.Empty),
            new SqlParameter("@PreparedBy", model.PreparedBy),
            new SqlParameter("@PreparedDate", DateTime.Now),
            new SqlParameter("@ApprovedBy", model.ApprovedBy),
            new SqlParameter("@ApprovedDate", DateTime.Now),
            new SqlParameter("@Status", true),
            new SqlParameter("@IsLocked", false),
            new SqlParameter("@FiscalYearId", model.FiscalYearId),
            new SqlParameter("@IsContactNoticeIssued", model.IsContactNoticeIssued),
            new SqlParameter("@IsContractDone", model.IsContractDone),
            new SqlParameter("@IsFirstInstallmentTaken", model.IsFirstInstallmentTaken),
            new SqlParameter("@ProjectFileUpload", model.ProjectFileUpload ?? string.Empty),
            new SqlParameter("@PictureOfProjectOne", model.PictureOfProjectOne ?? string.Empty),
            new SqlParameter("@PictureOfProjectTwo", model.PictureOfProjectTwo ?? string.Empty),
            new SqlParameter("@PictureOfProjectThree", model.PictureOfProjectThree ?? string.Empty),
            new SqlParameter("@IsNikashaMaag", model.IsNikashaMaag ?? false),
            new SqlParameter("@NikasaMaagFileUpload", model.NikasaMaagFileUpload ?? string.Empty),
            new SqlParameter("@TotalAmountUsed", model.TotalAmountUsed),
            new SqlParameter("@TotalContractAmount", model.TotalContractAmount),
            new SqlParameter("@TotalNikashaRamam", model.TotalNikashaRamam),
            new SqlParameter("@ContractNoticeFile", model.ContractNoticeFile ?? string.Empty),
            new SqlParameter("@ContractFile", model.ContractFile ?? string.Empty),
            new SqlParameter("@BhuktaniFile", model.BhuktaniFile ?? string.Empty),
            new SqlParameter("@RunningBillFile", model.RunningBillFile ?? string.Empty),
            new SqlParameter("@TimeExtendedFile", model.TimeExtendedFile ?? string.Empty),
            new SqlParameter("@AppRunningStatus", model.AppRunningStatus ?? 0),
            new SqlParameter("@NotRunningProofDoc", model.NotRunningProofDoc ?? string.Empty),
            new SqlParameter("@ReportOfFisalYearEnd", model.ReportOfFisalYearEnd ?? 0),
            new SqlParameter("@SuchanaPatiFile", model.SuchanaPatiUpload ?? string.Empty),
            new SqlParameter("@SarbajanikParikchenFile", model.SarbajanikParikchen ?? string.Empty),
            new SqlParameter("@SarbajanikSunuwaiFile", model.SarbajanikSunuwai ?? string.Empty),
            new SqlParameter("@SamapurakAnusuchi5File", model.SamapurakAnusuchi5 ?? string.Empty),
            new SqlParameter("@BisehsAnusuchi7File", model.BiseshAnusuchi7 ?? string.Empty),
              new SqlParameter("@ConductingCompanyName", model.ConductingCompanyName ?? string.Empty),
               new SqlParameter("@ConductingCompanyPan", model.ConductingCompanyPan ?? string.Empty),

            new SqlParameter
            {
                ParameterName = "@Message",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Direction = ParameterDirection.Output
            }
        };

                db.Database.ExecuteSqlCommand(
                    @"EXEC dbo.InsertQuadrimesterReportDetail
                @QuadrimesterReportsDetailId = @QuadrimesterReportsDetailId,
                @OfficeId = @OfficeId,
                @ProgramId = @ProgramId,
                @ApprovedBudget = @ApprovedBudget,
                @ProgramConductPlace = @ProgramConductPlace,
                @QuadrimesterId = @QuadrimesterId,
                @TargetedMaterial = @TargetedMaterial,
                @TargetedFinance = @TargetedFinance,
                @AchievementMaterial = @AchievementMaterial,
                @AchievementFinance = @AchievementFinance,
                @Remarks = @Remarks,
                @PreparedBy = @PreparedBy,
                @PreparedDate = @PreparedDate,
                @ApprovedBy = @ApprovedBy,
                @ApprovedDate = @ApprovedDate,
                @Status = @Status,
                @IsLocked = @IsLocked,
                @FiscalYearId = @FiscalYearId,
                @IsContactNoticeIssued = @IsContactNoticeIssued,
                @IsContractDone = @IsContractDone,
                @IsFirstInstallmentTaken = @IsFirstInstallmentTaken,
                @ProjectFileUpload = @ProjectFileUpload,
                @PictureOfProjectOne = @PictureOfProjectOne,
                @PictureOfProjectTwo = @PictureOfProjectTwo,
                @PictureOfProjectThree = @PictureOfProjectThree,
                @IsNikashaMaag = @IsNikashaMaag,
                @NikasaMaagFileUpload = @NikasaMaagFileUpload,
                @TotalAmountUsed = @TotalAmountUsed,
                @TotalContractAmount = @TotalContractAmount,
                @TotalNikashaRamam = @TotalNikashaRamam,
                @ContractNoticeFile = @ContractNoticeFile,
                @ContractFile = @ContractFile,
                @BhuktaniFile = @BhuktaniFile,
                @RunningBillFile = @RunningBillFile,
                @TimeExtendedFile = @TimeExtendedFile,
                @AppRunningStatus = @AppRunningStatus,
                @NotRunningProofDoc = @NotRunningProofDoc,
                @ReportOfFisalYearEnd = @ReportOfFisalYearEnd,
                @SuchanaPatiFile = @SuchanaPatiFile,
                @SarbajanikParikchenFile = @SarbajanikParikchenFile,
                @SarbajanikSunuwaiFile = @SarbajanikSunuwaiFile,
                @SamapurakAnusuchi5File = @SamapurakAnusuchi5File,
                @BisehsAnusuchi7File = @BisehsAnusuchi7File,
                @ConductingCompanyName = @ConductingCompanyName,
                @ConductingCompanyPan = @ConductingCompanyPan,
                @Message = @Message OUTPUT",
                    parameters
                );

                return parameters.Last(p => p.ParameterName == "@Message").Value?.ToString();
            }
        }



        public List<QuadrimesterReportsDetailViewModel> PopulateQuadrimesterReports(int QuadrimesterId, int OfficeId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<QuadrimesterReportsDetailViewModel> QuadrimesterlList = new List<QuadrimesterReportsDetailViewModel>();
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = QuadrimesterId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                QuadrimesterlList = db.Database.SqlQuery<QuadrimesterReportsDetailViewModel>("PopulateQuadrimesterReports @QuadrimesterId,@OfficeId,@GrantTypeId", QuadrimesterIdParam, OfficeIdParam, GrantTypeIdParam).ToList();
                return QuadrimesterlList;
            }
        }

        public List<QuadrimesterReportsDetailViewModel> PopulateProgressReportsByFYID(int FYID, int OfficeId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<QuadrimesterReportsDetailViewModel> QuadrimesterlList = new List<QuadrimesterReportsDetailViewModel>();
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                QuadrimesterlList = db.Database.SqlQuery<QuadrimesterReportsDetailViewModel>("PopulateProgressReportsByFYID @FiscalYearId,@OfficeId,@GrantTypeId", QuadrimesterIdParam, OfficeIdParam, GrantTypeIdParam).ToList();
                return QuadrimesterlList;
            }
        }


        public List<FiscalYearViewModel> PopulateFiscalYearForProgessReport(int ProgramId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<FiscalYearViewModel> FiscalyearList = new List<FiscalYearViewModel>();

                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = ProgramId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                FiscalyearList = db.Database.SqlQuery<FiscalYearViewModel>("PopulateFiscalYearForProgessReport @ProgramId,@GrantTypeId", ProgramIdParam, GrantTypeIdParam).ToList();
                return FiscalyearList;
            }
        }

        #endregion

        #region Phase II Validation

        public int GetTotalApprovedProgramCount(int GrantTypeId, int PhaseId, int OfficeId)
        {
            int TotalApprovedProgram = 0;
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int PrePhaseNumber = CurrentPhaseNumber = 1;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseIdParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PrePhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                TotalApprovedProgram = db.Database.SqlQuery<int>("CheckTotalProgramConditionPhaseWise @GrantTypeId,@PhaseStatus,@OfficeId", GrantTypeIdParam, PhaseIdParam, OfficeIdParam).FirstOrDefault();

            }
            return TotalApprovedProgram;
        }

        public int GetTotalKramagatAaayojanaCountByOfficeAndType(int GrantTypeId, int Officeid)
        {
            int CountCond1 = 0;
            int CountCond2 = 0;

            int SampurakCount = 0;
            int TotalCount = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                CountCond1 = db.Database.SqlQuery<int>(@"select COUNT(*) as Total From SubProgramMaster
                            where PhaseStatus=4 and TimeDurationYear>2
                            and Status=2 and ApprovedStatus=1 and OfficeId='" + Officeid + "' and GrantTypeId='" + GrantTypeId + "' and IsCancelled=0").FirstOrDefault();

                CountCond2 = db.Database.SqlQuery<int>(@"select COUNT(*) as Total From SubProgramMaster
                            where PhaseStatus=5 and TimeDurationYear>1
                            and Status=2 and ApprovedStatus=1 and OfficeId='" + Officeid + "' and GrantTypeId='" + GrantTypeId + "' and IsCancelled=0").FirstOrDefault();

                TotalCount = CountCond1 + CountCond2;

            }


            return TotalCount;
        }



        public bool CountTotalNumberOfProjectOfficeWise(int GrantTypeId, int PhaseId, int OfficeId)
        {
            int CurrentloginUserType = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserType();
            bool CheckNumberTrueFalse = false;
            int TotalCount = 0;
            int TotalCountBothNewAndRunning = 0;
            int TotalCountForLocalLevel = 0;

            //int TotalApprovedProgramCount = GetTotalApprovedProgramCount(GrantTypeId, PhaseId, OfficeId);
            int TotalApprovedProgramCount = GetTotalKramagatAaayojanaCountByOfficeAndType(GrantTypeId, OfficeId);

            //int TotalApprovedProgramCount = 0;


            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseIdParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                TotalCountBothNewAndRunning = db.Database.SqlQuery<int>("CheckTotalProgramConditionForClient @GrantTypeId,@PhaseStatus,@OfficeId", GrantTypeIdParam, PhaseIdParam, OfficeIdParam).FirstOrDefault();

            }

            TotalCount = TotalCountBothNewAndRunning + TotalApprovedProgramCount;

            if (CurrentloginUserType == 4)//local level
            {
                if (TotalCount < 5)//changed 3 to 5-12th jan 2020---changed 5 to 2 dec082020
                {
                    CheckNumberTrueFalse = true;
                }
                else
                {
                    CheckNumberTrueFalse = false;
                }
            }

            else//province level
            {
                if (GrantTypeId == 1)
                {
                    if (TotalCount < 10) //changed 15 to 2 dec82020
                    {
                        CheckNumberTrueFalse = true;
                    }
                    else
                    {
                        CheckNumberTrueFalse = false;
                    }
                }
                else
                {
                    if (TotalCount < 15)//changed 15 to 2
                    {
                        CheckNumberTrueFalse = true;
                    }
                    else
                    {
                        CheckNumberTrueFalse = false;
                    }
                }

            }

            return CheckNumberTrueFalse;
        }


        public string ChangeSubProgramPriority(int SubProgramId, int ProgramPriority, int OfficeId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var ProgramPriorityParam = new SqlParameter { ParameterName = "@ProgramPriority", Value = ProgramPriority };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                //var ChangeStatusParam = new SqlParameter { ParameterName = "@ChangeStatus", Value = ChangeStatus };


                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec ChangeSubProgramPriority @SubProgramId,@ProgramPriority,@OfficeId,@GrantTypeId,@Message OUT",
                    SubProgramIdParam, ProgramPriorityParam, OfficeIdParam, GrantTypeIdParam, MessageParam);
                //int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;
            }

        }

        public int CheckProgramNameAlreadyInserted(int ProgramId, int GrantTypeId, int officeId, string ProgramTitle)
        {
            int TotalCountValue = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                int TotalCount = db.Database.SqlQuery<int>("select count(SubProgramId) as Total From SubProgramMaster where OfficeId='" + officeId + "' and ProgramId='" + ProgramId + "' and SubProgramTitle=N'" + ProgramTitle + "' and GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                if (TotalCount > 0)
                {
                    TotalCountValue = TotalCount;
                }
            }

            return TotalCountValue;
        }


        public string GetProgramNameByProgramId(int SubProgramId, int OfficeId)
        {
            string SubProgramTitleVal = "";
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                string SubProgramTitle = db.Database.SqlQuery<string>("select SubProgramTitle as Total From SubProgramMaster where OfficeId='" + OfficeId + "' and SubProgramId='" + SubProgramId + "'").FirstOrDefault();
                if (SubProgramTitle != "")
                {
                    SubProgramTitleVal = SubProgramTitle;
                }
            }

            return SubProgramTitleVal;
        }


        public bool  CheckProgramPriorityAlreadyInserted( int GrantTypeId, int officeId,int phaseStatus, int Priority)
        {
            int TotalCountValue = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                int TotalCount = db.Database.SqlQuery<int>("select count(SubProgramId) as Total From SubProgramMaster where OfficeId='" + officeId + "' and PhaseStatus='" + phaseStatus + "' and ProgramPirority='" + Priority + "'and GrantTypeId='" + GrantTypeId + "'").FirstOrDefault();
                if (TotalCount > 0)
                {
                    TotalCountValue = TotalCount;
                }
            }

            if (TotalCountValue > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public int GetPriorityofProgramById(int programId, int officeId, int phaseStatus)
        {
            int ProgramPriorityValue = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                int ProgramPriority = db.Database.SqlQuery<int>("select ProgramPirority as Total From SubProgramMaster where OfficeId='" + officeId + "' and PhaseStatus='" + phaseStatus + "' and SubProgramId='" + programId + "'").FirstOrDefault();
                if (ProgramPriority > 0)
                {
                    ProgramPriorityValue = ProgramPriority;
                }
            }

            if (ProgramPriorityValue > 0)
            {
                return ProgramPriorityValue;
            }
            else
            {
                return ProgramPriorityValue;
            }

        }
        public string DeleteSubProgramDetails(SubProgramMaster model)
        {
            //check already approved or not



            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = model.SubProgramId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };




                var result = db.Database.ExecuteSqlCommand("exec DeleteSubProgramDetails @SubProgramId,@Message OUT",
                    SubProgramIdParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;



            }
        }


        public bool CheckTotalYearlyValueMatchOrNot(int YearDurationId, decimal TotalBudget, decimal? IstYearBudget, decimal? IIndYearBudget, decimal? IIIrdYearBudget)
        {
            bool returnVal = false;
            decimal? SumAmount = 0;
            if (YearDurationId == 1)
            {
                SumAmount = IstYearBudget;
                if (SumAmount <= TotalBudget)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
                returnVal = true;
            }
            if (YearDurationId == 2)
            {
                SumAmount = IstYearBudget + IIndYearBudget;
                if (SumAmount <= TotalBudget)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }
            if (YearDurationId == 3)
            {
                SumAmount = IstYearBudget + IIndYearBudget + IIIrdYearBudget;
                if (SumAmount <= TotalBudget)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }

            return returnVal;
        }


        public bool CheckTotalYearlyValueMatchOrNotComplementary(int YearDurationId, decimal TotalBudget, decimal? IstYearBudget, decimal? IIndYearBudget, decimal? IIIrdYearBudget, decimal? AmountFromPro)
        {
            bool returnVal = false;
            decimal? SumAmount = 0;
            if (YearDurationId == 1)
            {
                SumAmount = AmountFromPro + IstYearBudget;
                if (SumAmount <= TotalBudget)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }
            if (YearDurationId == 2)
            {
                SumAmount = IstYearBudget + IIndYearBudget + AmountFromPro;
                if (SumAmount <= TotalBudget)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }
            if (YearDurationId == 3)
            {
                SumAmount = IstYearBudget + IIndYearBudget + IIIrdYearBudget + AmountFromPro;
                if (SumAmount <= TotalBudget)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }

            return returnVal;
        }


        public bool CheckTotalYearlyValueMatchOrNot(int YearDurationId, decimal TotalBudget, decimal? IstYearBudget, decimal? IIndYearBudget, decimal? IIIrdYearBudget, decimal? IstYearBudgetPuji, decimal? IIndYearBudgetPuji, decimal? IIIrdYearBudgetPuji)
        {
            bool returnVal = false;
            decimal? SumAmount = 0;
            if (!IstYearBudget.HasValue)
            {
                IstYearBudget = 0;
            }
            if (!IIndYearBudget.HasValue)
            {
                IIndYearBudget = 0;
            }
            if (!IIIrdYearBudget.HasValue)
            {
                IIIrdYearBudget = 0;
            }


            if (YearDurationId == 1)
            {
                SumAmount = IstYearBudget + +IstYearBudgetPuji;

                if (TotalBudget == SumAmount)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }


            if (YearDurationId == 2)
            {


                SumAmount = IstYearBudget + IIndYearBudget + IstYearBudgetPuji + IIndYearBudgetPuji;

                if (TotalBudget == SumAmount)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }
            if (YearDurationId == 3)
            {
                SumAmount = IstYearBudget + IIndYearBudget + IIIrdYearBudget + IstYearBudgetPuji + IIndYearBudgetPuji + IIIrdYearBudgetPuji;
                if (TotalBudget == SumAmount)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }

            return returnVal;
        }

        public bool CheckTotalYearlyValueMatchOrNotForComplementry( decimal TotalBudget,  decimal? AmountByPro, int OfficeId)
        {
            bool returnVal = false;
            GrantGroupInfo groupInfo = new GrantGroupInfo();
            groupInfo = GrantApp.CommontUtilities.GetGrantGroupInfo(OfficeId);
            int contributionpercent = (int)(groupInfo?.ContributionPercent ?? 100);
            var MinimumContributionAmountByOffice = contributionpercent * TotalBudget / 100;
            if(AmountByPro>=MinimumContributionAmountByOffice)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool CheckTotalYearlyValueMatchOrNotForComplementryFirstYear(int YearDurationId, decimal TotalBudget, decimal? IstYearBudget,  decimal? AmountByPro)
        {


            // amount by palika check garne ratio anusar >= ratio
            bool returnVal = false;
            decimal? SumAmount = 0;
            decimal? checkAmount = 0;
            decimal? checkAmount2 = 0;
            if (YearDurationId == 1)
            {
                SumAmount = TotalBudget - AmountByPro;



                if (IstYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IstYearBudget >= 0.9m * SumAmount.Value)
                    {
                        returnVal= true;
                    }
                    else
                    {
                        returnVal= false;
                    }

                }
                else
                {
                    returnVal = false;
                }


            }
          

            return returnVal;
        }

        public bool CheckTotalYearlyValueMatchOrNotForComplementrySecondYear(int YearDurationId, decimal TotalBudget, decimal? IstYearBudget, decimal? IIndYearBudget, decimal? AmountByPro)
        {
            // amount by palika check garne ratio anusar >= ratio
            bool returnVal = false;
            decimal? SumAmount = 0;
            decimal? checkAmount = 0;
            decimal? checkAmount2 = 0;
            if (YearDurationId == 2)
            {
                //0.35 dekhi 0.4
                SumAmount = TotalBudget - AmountByPro;
                if (IstYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IstYearBudget > 0.4m * SumAmount.Value && IstYearBudget  <= 0.5m * SumAmount.Value)
                    {
                        returnVal= true;
                    }
                    else
                    {
                        returnVal= false;
                    }
                }
                else
                {
                    returnVal = false;
                }
                if (IIndYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IIndYearBudget >= 0.5m * SumAmount.Value && IIndYearBudget <= 0.6m * SumAmount.Value)
                    {
                        returnVal=  true;
                    }
                    else
                    {
                        returnVal= false;
                    }

                }
                else
                {
                    returnVal= false;
                }

            }
            return returnVal;
        }


        public bool CheckTotalYearlyValueMatchOrNotForComplementryThirdYear(int YearDurationId, decimal TotalBudget, decimal? IstYearBudget, decimal? IIndYearBudget, decimal? IIIrdYearBudget, decimal? AmountByPro, decimal? AmountByNGO)
        {
            // amount by palika check garne ratio anusar >= ratio
            bool returnVal = false;
            decimal? SumAmount = 0;
            decimal? checkAmount = 0;
            decimal? checkAmount2 = 0;
            if (YearDurationId == 3)
            {

                SumAmount = TotalBudget - AmountByPro;

                if (IstYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IstYearBudget > 0.25m * SumAmount.Value && IstYearBudget <= 0.35m * SumAmount.Value)
                    {
                        returnVal = true;
                    }
                    else
                    {
                        returnVal = true;
                    }

                }
                else
                {
                    returnVal = false;
                }

                if (IIndYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IIndYearBudget >= 0.35m * SumAmount.Value && IIndYearBudget <= 0.4m * SumAmount.Value)
                    { 
                        returnVal = true; 
                    }
                    else 
                    { 
                        returnVal = false; 
                    }
                }
                else
                {
                    returnVal = false;
                }

                if (IIIrdYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IIIrdYearBudget == TotalBudget - AmountByPro - IstYearBudget - IIndYearBudget)
                    {
                        returnVal = true;
                    }
                    else
                    {
                        returnVal = false;
                    }

                }
                else
                {
                    returnVal = false;
                }

            }
            return returnVal;
        }


        public bool CheckTotalYearlyValueMatchOrNotForComplementry(int YearDurationId, decimal TotalBudget, decimal? IstYearBudget, decimal? IIndYearBudget, decimal? IIIrdYearBudget, decimal? AmountByPro, decimal? AmountByNGO)
        {

          
            // amount by palika check garne ratio anusar >= ratio
            bool returnVal = false;
            decimal? SumAmount = 0;
            decimal? checkAmount= 0;
            decimal? checkAmount2 = 0;
            if (YearDurationId == 1)
            {
                SumAmount = TotalBudget - AmountByPro;
              
              

                if (IstYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IstYearBudget >= 0.9m * SumAmount.Value )
                        returnVal = true;
                }
                else
                {
                    returnVal = false;
                }

              
            }
            if (YearDurationId == 2)
            {
                //0.35 dekhi 0.4

                SumAmount = TotalBudget - AmountByPro;

                if (IstYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IstYearBudget >= 0.4m * SumAmount.Value && IstYearBudget <= 0.5m * SumAmount.Value)
                    {
                        returnVal = true;
                    }
                    {
                        returnVal = false;
                    }
                }
                else
                {
                    returnVal = false;
                }
                if (IIndYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IIndYearBudget > 0.5m * SumAmount.Value && IIndYearBudget <= 0.6m * SumAmount.Value)
                    {
                        returnVal = true;
                    }
                    else
                    {
                        returnVal = false;
                    }
                   
                }
                else
                {
                    returnVal = false;
                }

            }
            if (YearDurationId == 3)
            {

                SumAmount = TotalBudget - AmountByPro;

                if (IstYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IstYearBudget >= 0.25m * SumAmount.Value && IstYearBudget <= 0.35m * SumAmount.Value)
                    {
                        returnVal = true;
                    }
                    {
                        returnVal = true;
                    }
                       
                }
                else
                {
                    returnVal = false;
                }

                if (IIndYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IIndYearBudget > 0.35m * SumAmount.Value && IIndYearBudget <= 0.4m * SumAmount.Value)
                        returnVal = true;
                }
                else
                {
                    returnVal = false;
                }

                if (IIIrdYearBudget.HasValue && SumAmount.HasValue)
                {
                    if (IIIrdYearBudget == TotalBudget - AmountByPro - IstYearBudget- IIndYearBudget)
                    {
                        returnVal = true;
                    }
                    else
                    {
                        returnVal = false;
                    }
                        
                }
                else
                {
                    returnVal = false;
                }

            }

            return returnVal;
        }


        public List<SubProgramMaster> PopulateSubProgramPhaseWiseList(int GrantTypeId, int OfficeId, int PhaseNumber)

        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramMaster> SubProgramSetupList = new List<SubProgramMaster>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                //SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateSubProgramPhaseWise @GrantTypeId,@PhaseNumber,@OfficeId", GrantTypeIdParam, PhaseNumberParam, OfficeIdParam).ToList();
                SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateOldSubProgramPhaseWiseList @GrantTypeId,@PhaseNumber,@OfficeId", GrantTypeIdParam, PhaseNumberParam, OfficeIdParam).ToList();
                return SubProgramSetupList;
            }
        }
        public List<SubProgramMaster> PopulateOldSubProgramPhaseWiseListForProgressRpt(int GrantTypeId, int OfficeId, int PhaseNumber)

        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramMaster> SubProgramSetupList = new List<SubProgramMaster>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                //SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateSubProgramPhaseWise @GrantTypeId,@PhaseNumber,@OfficeId", GrantTypeIdParam, PhaseNumberParam, OfficeIdParam).ToList();
                SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("PopulateOldSubProgramPhaseWiseListForProgressRpt @GrantTypeId,@PhaseNumber,@OfficeId", GrantTypeIdParam, PhaseNumberParam, OfficeIdParam).ToList();
                return SubProgramSetupList;
            }
        }


        public List<YearlyWiseProgressDetailsListVM> SP_GetFYWiseProgressSubmissionList(int OfficeId, int PhaseNumber)

        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<YearlyWiseProgressDetailsListVM> SubProgramSetupList = new List<YearlyWiseProgressDetailsListVM>();
                //var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseStatus", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                SubProgramSetupList = db.Database.SqlQuery<YearlyWiseProgressDetailsListVM>("SP_GetFYWiseProgressSubmissionList @OfficeId,@ProgramPhaseStatus", OfficeIdParam, PhaseNumberParam).ToList();
                return SubProgramSetupList;
            }
        }



        #endregion




        public List<SubProgramMaster> SP_GetSubmitedProgramListbyType(int GrantTypeId, int ProgramStatus, int PhaseNumber, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramMaster> SubProgramSetupList = new List<SubProgramMaster>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var ProgramStatusParam = new SqlParameter { ParameterName = "@ProgramStatus", Value = ProgramStatus };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                SubProgramSetupList = db.Database.SqlQuery<SubProgramMaster>("SP_GetSubmitedProgramListbyType @GrantTypeId,@ProgramStatus,@PhaseNumber,@OfficeId", GrantTypeIdParam, ProgramStatusParam, PhaseNumberParam, OfficeIdParam).ToList();
                return SubProgramSetupList;
            }
        }


        public List<CurrentYearGrantRequestProgramListVM> SPUP_GetCurrentYearRequestProgramList(int PhaseNumber, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<CurrentYearGrantRequestProgramListVM> SubProgramSetupList = new List<CurrentYearGrantRequestProgramListVM>();
                var PhaseNumberParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                SubProgramSetupList = db.Database.SqlQuery<CurrentYearGrantRequestProgramListVM>("SPUP_GetCurrentYearRequestProgramList @OfficeId,@FiscalYearId", OfficeIdParam, PhaseNumberParam).ToList();
                return SubProgramSetupList;
            }
        }

        public List<CanceledProgramListForRGVM> SPUP_GetCanceledProgramListForClient(int GrantTypeId, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<CanceledProgramListForRGVM> RequestGrantAmountList = new List<CanceledProgramListForRGVM>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                try
                {
                    RequestGrantAmountList = db.Database.SqlQuery<CanceledProgramListForRGVM>("SPUP_GetCanceledProgramListForClient @GrantTypeId,@OfficeId", GrantTypeIdParam, OfficeIdParam).ToList();
                }
                catch (Exception)
                {

                    RequestGrantAmountList = new List<CanceledProgramListForRGVM>();
                }

                return RequestGrantAmountList;
            }
        }

        public string SP_MakeProgramApprovedByAdmin(SubProgramMaster model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string ReturnMessage = string.Empty;
                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = model.SubProgramId };
                var AmountParam = new SqlParameter { ParameterName = "@Amount", Value = model.ApprovedBudgetByNPC.HasValue?model.ApprovedBudgetByNPC:0m };

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var IPAddressParam = new SqlParameter { ParameterName = "@IPAddress", Value = model.IPAddress };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@ReturnMessage",
                    DbType = DbType.String,
                    Size = 150,
                    Direction = System.Data.ParameterDirection.Output
                };
                try
                {
                    var outPut = db.Database.ExecuteSqlCommand("exec SP_MakeProgramApprovedByAdmin @SubprogramId,@Amount,@OfficeId,@IPAddress,@ReturnMessage OUT", SubprogramIdParam, AmountParam,OfficeIdParam,IPAddressParam,MessageParam);
                    ReturnMessage = MessageParam.SqlValue.ToString();
                }
                catch (Exception e)
                {

                    ReturnMessage = e.ToString();
                }

                return string.Empty;
            }
        }

        public string SP_SP_MakeProgramDisApprovedByAdmin(SubProgramMaster model)
        {


            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string ReturnMessage = string.Empty;
                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = model.SubProgramId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var IPAddressParam = new SqlParameter { ParameterName = "@IPAddress", Value = model.IPAddress };
                try
                {
                    var outPut = db.Database.ExecuteSqlCommand("exec SP_SP_MakeProgramDisApprovedByAdmin @SubprogramId,@OfficeId,@IPAddress", SubprogramIdParam,OfficeIdParam,IPAddressParam);
                }
                catch (Exception e)
                {

                    ReturnMessage = e.ToString();
                }

                return string.Empty;
            }
        }



    }
}