using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;
using GrantApp.Areas.Admin;
using GrantApp.Areas.Admin.Models;

namespace GrantApp.Services
{
    public class ReportServices
    {
        public List<ComplementryReportViewModel> PopulateReportSummary(int StatuaParamVal, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var StatusParamId = new SqlParameter { ParameterName = "@StatusParam", Value = StatuaParamVal };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("PopulateComplementryReport @StatusParam,@GrantTypeId", StatusParamId, GrantTypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }



        public List<ComplementryReportViewModel> PopulateNotSubmitedListReport(int StatuaParamVal, int GrantTypeId, int PhaseNumber, int ProvinceId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var StatusParamId = new SqlParameter { ParameterName = "@StatusParam", Value = StatuaParamVal };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("PopulateNotSubmitedListReport @StatusParam,@GrantTypeId,@PhaseNumber,@ProvinceId", StatusParamId, GrantTypeIdParam, PhaseNumberParam, ProvinceIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> PopulateNotSubmitedListReportForProvince(int GrantTypeId, int PhaseNumber, int ProvinceId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("PopulateNotSubmitedListReportForProvince @GrantTypeId,@PhaseNumber,@ProvinceId", GrantTypeIdParam, PhaseNumberParam, ProvinceIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> PopulateNotSubmitedListReportForLL(int GrantTypeId, int PhaseNumber, int ProvinceId, int DistrcitId, int VdcMunId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrcitId };
                var VdcMunIdParam = new SqlParameter { ParameterName = "@VdcMunId", Value = VdcMunId };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("PopulateNotSubmitedListReportForLL @GrantTypeId,@PhaseNumber,@ProvinceId,@DistrictId,@VdcMunId", GrantTypeIdParam, PhaseNumberParam, ProvinceIdParam, DistrictIdParam, VdcMunIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> PopulateNotSubmitedListReportByCode(int PhaseNumber, int SubProgramID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = PhaseNumber };
                var ProgramCodeNumberParam = new SqlParameter { ParameterName = "@ProgramCodeNumber", Value = SubProgramID };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("PopulateNotSubmitedListReportByCode @ProgramPhaseNumber,@ProgramCodeNumber", ProgramPhaseNumberParam, ProgramCodeNumberParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> PopulateSubProgramProvinceDistrictWise(int ProvinceId, int DistrictId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("PopulateSubProgramProvinceDistrictWise @ProvinceId,@DistrictId,@GrantTypeId", ProvinceIdParam, DistrictIdParam, GrantTypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ComplementryReportViewModel> ProvinceDistrictVDCMUNWiseReport(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int ProgramCountValId, int DocType2, int DocType5, int DoctType7, int DocType8, int DocType9, int DocType10, int DocType22, int DocType26, int DocType30, int DocType31, int DocType32, int TotalBudgetCalculation, int ProgramPhaseNumber, int MainSectionId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var CountValParam = new SqlParameter { ParameterName = "@CountVal", Value = ProgramCountValId };


                var Doc2Param = new SqlParameter { ParameterName = "@Doc2", Value = DocType2 };
                var Doc5Param = new SqlParameter { ParameterName = "@Doc5", Value = DocType5 };
                var Doc7Param = new SqlParameter { ParameterName = "@Doc7", Value = DoctType7 };
                var Doc8Param = new SqlParameter { ParameterName = "@Doc8", Value = DocType8 };
                var Doc9Param = new SqlParameter { ParameterName = "@Doc9", Value = DocType9 };
                var Doc10Param = new SqlParameter { ParameterName = "@Doc10", Value = DocType10 };


                var Doc22Param = new SqlParameter { ParameterName = "@Doc22", Value = DocType22 };
                var Doc26Param = new SqlParameter { ParameterName = "@Doc26", Value = DocType26 };
                var Doc30Param = new SqlParameter { ParameterName = "@Doc30", Value = DocType30 };
                var Doc31Param = new SqlParameter { ParameterName = "@Doc31", Value = DocType31 };
                var Doc32Param = new SqlParameter { ParameterName = "@Doc32", Value = DocType32 };
                var TotalBudgetCalculationParam = new SqlParameter { ParameterName = "@TotalBudgetCalculation", Value = TotalBudgetCalculation };
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = ProgramPhaseNumber };
                var MainSectionIdParam = new SqlParameter { ParameterName = "@MainSectionId", Value = MainSectionId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ReportProvinceDistrictVDCMUNWise @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@CountVal,@Doc2,@Doc5,@Doc7,@Doc8,@Doc9,@Doc10,@Doc22,@Doc26,@Doc30,@Doc31, @Doc32,@TotalBudgetCalculation,@ProgramPhaseNumber,@MainSectionId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, CountValParam, Doc2Param, Doc5Param, Doc7Param, Doc8Param, Doc9Param, Doc10Param, Doc22Param, Doc26Param, Doc30Param, Doc31Param, Doc32Param, TotalBudgetCalculationParam, ProgramPhaseNumberParam, MainSectionIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }
        //public List<ComplementryReportViewModel> ProvinceDistrictVDCMUNWiseReport(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId)
        //{
        //    using (GrantAppDBEntities db = new GrantAppDBEntities())
        //    {
        //        List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

        //        var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
        //        var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
        //        var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
        //        var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
        //        ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ReportProvinceDistrictVDCMUNWise @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam).ToList();
        //        return ReturnComplementryReportViewModelList;
        //    }
        //}


        public List<ComplementryReportViewModel> PopulateSubProgramProvincesWiseOnly(int ProvinceId, int GrantTypeId, int ProgramCountID, int DocType2, int DocType5, int DocType7, int DocType8, int DocType9, int DocType10, int DocType22, int DocType26, int DocType30, int DocType31, int DocType32, int TotalBudgetCalculation, int ProgramPhaseNumber, int MainSectionId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var CountvalParam = new SqlParameter { ParameterName = "@Countval", Value = ProgramCountID };

                var Doc2Param = new SqlParameter { ParameterName = "@Doc2", Value = DocType2 };
                var Doc5Param = new SqlParameter { ParameterName = "@Doc5", Value = DocType5 };
                var Doc7Param = new SqlParameter { ParameterName = "@Doc7", Value = DocType7 };
                var Doc8Param = new SqlParameter { ParameterName = "@Doc8", Value = DocType8 };
                var Doc9Param = new SqlParameter { ParameterName = "@Doc9", Value = DocType9 };
                var Doc10Param = new SqlParameter { ParameterName = "@Doc10", Value = DocType10 };


                var Doc22Param = new SqlParameter { ParameterName = "@Doc22", Value = DocType22 };
                var Doc26Param = new SqlParameter { ParameterName = "@Doc26", Value = DocType26 };
                var Doc30Param = new SqlParameter { ParameterName = "@Doc30", Value = DocType30 };
                var Doc31Param = new SqlParameter { ParameterName = "@Doc31", Value = DocType31 };
                var Doc32Param = new SqlParameter { ParameterName = "@Doc32", Value = DocType32 };
                var TotalBudgetCalculationParam = new SqlParameter { ParameterName = "@TotalBudetCalculation", Value = TotalBudgetCalculation };
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = ProgramPhaseNumber };
                var MainSectionIdParam = new SqlParameter { ParameterName = "@MainSectionId", Value = MainSectionId };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SubprogramListProvincesWiseOnly @ProvinceId,@GrantTypeId,@Countval,@Doc2,@Doc5,@Doc7,@Doc8,@Doc9,@Doc10,@Doc22,@Doc26,@Doc30,@Doc31, @Doc32,@TotalBudetCalculation,@ProgramPhaseNumber,@MainSectionId", ProvinceIdParam, GrantTypeIdParam, CountvalParam, Doc2Param, Doc5Param, Doc7Param, Doc8Param, Doc9Param, Doc10Param, Doc22Param, Doc26Param, Doc30Param, Doc31Param, Doc32Param, TotalBudgetCalculationParam, ProgramPhaseNumberParam, MainSectionIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }
        //public List<ComplementryReportViewModel> PopulateSubProgramProvincesWiseOnly(int ProvinceId, int GrantTypeId)
        //{
        //    using (GrantAppDBEntities db = new GrantAppDBEntities())
        //    {

        //        List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
        //        var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
        //        var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
        //        ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SubprogramListProvincesWiseOnly @ProvinceId,@GrantTypeId", ProvinceIdParam, GrantTypeIdParam).ToList();
        //        return ReturnComplementryReportViewModelList;
        //    }
        //}


        public List<OfficeDetailsViewModel> ListIdleUsers()
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetailsViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetailsViewModel>();


                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetailsViewModel>("PopulateIdleUserList").ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<OfficeDetailsViewModel> ListIdleUsersFYWise(int FyId, int RequestOrNotId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetailsViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetailsViewModel>();

                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = FyId };
                var SubmitedOrNotParam = new SqlParameter { ParameterName = "@SubmitedOrNot", Value = RequestOrNotId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetailsViewModel>("PopulateIdleUserListFYWise @PhaseNumber,@SubmitedOrNot,@GrantTypeId", PhaseNumberParam, SubmitedOrNotParam, GrantTypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<SubProgramDuplicateViewModel> PopulateDuplicateSubProgramList()
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SubProgramDuplicateViewModel> SubProgramDuplicateViewModelList = new List<SubProgramDuplicateViewModel>();

                SubProgramDuplicateViewModelList = db.Database.SqlQuery<SubProgramDuplicateViewModel>("PopulateDuplicateSubProgramList").ToList();
                return SubProgramDuplicateViewModelList;
            }
        }





        #region Program wise amount


        public List<ComplementryReportViewModel> PopulateSubProgramProvincesAmountOnly(int ProvinceId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SubprogramListProvincesAmountWiseOnly @ProvinceId,@GrantTypeId", ProvinceIdParam, GrantTypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> ProvinceDistrictVDCMUNAmountReport(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };




                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ReportProVdcMunProgramAmountWise @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }



        #endregion



        #region progress report details
        public List<ComplementryReportViewModel> PopulateProgressReportProvincesOnly(int ProvinceId, int GrantTypeId, int QuadrimesterId, int FyID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = QuadrimesterId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FyID };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ProgressReportProvincesWiseOnly @ProvinceId,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ComplementryReportViewModel> ProgressReportProvincesNotSubmitedOnly(int ProvinceId, int GrantTypeId, int QuadrimesterId, int FYID, int userTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = QuadrimesterId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                var UsertypeIdParam = new SqlParameter { ParameterName = "@UserType", Value = userTypeId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ListOfficeNotSubmitedProgressrpt @ProvinceId,@GrantTypeId,@QuadrimesterId,@FiscalYearId,@UserType", ProvinceIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam, UsertypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ComplementryReportViewModel> ProvinceDistrictVDCMUNProgressReport(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int QuadrimesterId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = QuadrimesterId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ProgressReportProVdcMunWise @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }
        //public List<ComplementryReportViewModel> ProvinceDistrictVDCMUNProgressReportForAdmin(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int QuadrimesterId, int FiscalYearId)
        //{
        //    using (GrantAppDBEntities db = new GrantAppDBEntities())
        //    {
        //        List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

        //        var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
        //        var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
        //        var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
        //        var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
        //        var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = QuadrimesterId };
        //        var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
        //        //ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ProgressReportProVdcMunWiseForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();
        //        ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_UPProgressReportProVdcMunWiseForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();

        //        return ReturnComplementryReportViewModelList;
        //    }
        //}

        public List<ComplementryReportViewModel> SP_UPProgressReportProvinceOnlyForAdmin(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int WorkActionStatus, int FiscalYearId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = WorkActionStatus };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                //ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ProgressReportProVdcMunWiseForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_UPProgressReportProvinceOnlyForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();

                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ComplementryReportViewModel> SP_PRProvinceOnlyForAdminDetails(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int WorkActionStatus, int FiscalYearId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = WorkActionStatus };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                //ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ProgressReportProVdcMunWiseForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_PRProvinceOnlyForAdminDetails @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();

                return ReturnComplementryReportViewModelList;
            }
        }




        public List<ComplementryReportViewModel> ProvinceDistrictVDCMUNProgressReportForAdmin(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int WorkActionStatus, int FiscalYearId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = WorkActionStatus };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                //ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ProgressReportProVdcMunWiseForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_UPProgressReportProVdcMunWiseForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();

                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ComplementryReportViewModel> SP_PRProVdcMunWiseForAdminAll(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int WorkActionStatus, int FiscalYearId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = WorkActionStatus };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                //ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("ProgressReportProVdcMunWiseForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_PRProVdcMunWiseForAdminAll @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@QuadrimesterId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, QuadrimesterIdParam, FiscalYearIdParam).ToList();

                return ReturnComplementryReportViewModelList;
            }
        }

        public List<QuadrimesterReportsDetailViewModel> PopulateProgressReportForAdmin(int SubProgramId, int QuadrimesterId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<QuadrimesterReportsDetailViewModel> ReturnComplementryReportViewModelList = new List<QuadrimesterReportsDetailViewModel>();
                var QuadrimesterIdParam = new SqlParameter { ParameterName = "@QuadrimesterId", Value = QuadrimesterId };
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<QuadrimesterReportsDetailViewModel>("SP_UPPopulateProgressReportForAdmin @QuadrimesterId,@SubProgramId", QuadrimesterIdParam, SubProgramIdParam).ToList();
                //ReturnComplementryReportViewModelList = db.Database.SqlQuery<QuadrimesterReportsDetailViewModel>("PopulateProgressReportForAdmin @QuadrimesterId,@SubProgramId", QuadrimesterIdParam, SubProgramIdParam).ToList();

                return ReturnComplementryReportViewModelList;
            }
        }

        #endregion

        #region Grant Request Amount
        public List<ComplementryReportViewModel> PopulateGrantReqAmountProvincesWiseOnly(int ProvinceId, int GrantTypeId, int FYID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("GrantReqAmountProvincesWiseOnly @ProvinceId,@GrantTypeId,@FiscalYearId", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> PopulateGrantReqAmountProVdcMunWise(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int FYID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("GrantReqAmountProVdcMunWise @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, FiscalYearIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> SP_GrantReqAmountForRunningAndNew(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int FYID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_GrantReqAmountForRunningAndNew @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, FiscalYearIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }



        public List<ComplementryReportViewModel> SubmitedProjectListForAdmin(int ProvinceId, int DistrictId, int VDCMUNID, int GrantTypeId, int FYID, int PhaseNumberId, int ProgramPriorityCountId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumberId };
                var PriorityIdParam = new SqlParameter { ParameterName = "@PriorityId", Value = ProgramPriorityCountId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SubmitedProjectListForAdmin @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@FiscalYearId,@PhaseNumber,@PriorityId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, FiscalYearIdParam, PhaseNumberParam, PriorityIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }



        public ViewRequestGrantAmountModel PopulateRequestGrantAmountDetail(int OfficeId, int ProgramId, int FYid, bool RequestStatus)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                ViewRequestGrantAmountModel model = new ViewRequestGrantAmountModel();
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = ProgramId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYid };
                var RequestStatusParam = new SqlParameter { ParameterName = "@RequestStatus", Value = RequestStatus };
                model = db.Database.SqlQuery<ViewRequestGrantAmountModel>("ViewRequestGrantAmountForAdmin @OfficeId,@ProgramId,@FiscalYearId,@RequestStatus", OfficeIdParam, ProgramIdParam, FiscalYearIdParam, RequestStatusParam).FirstOrDefault();
                if (model == null)
                {
                    model = new ViewRequestGrantAmountModel();
                }
                return model;
            }
        }


        public string UpdateGrantRequestAmountByAdmin(ViewRequestGrantAmountModel _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var ProgramIdParam = new SqlParameter { ParameterName = "ProgramId", Value = _Model.SubProgramId };
                var UpdateAmountParam = new SqlParameter { ParameterName = "@UpdateAmount", Value = _Model.AmountSecond };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec UpdateGrantedRequestAmountByAdmin @ProgramId,@UpdateAmount,@Message OUT",
                    ProgramIdParam, UpdateAmountParam, MessageParam);
                //int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;
            }
        }



        public string SP_UpdateProgramWiseAmount(ViewRequestGrantAmountModel _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var ProgramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = _Model.SubProgramId };
                var AmountSecondParam = new SqlParameter { ParameterName = "@GrantedAmount", Value = _Model.GrantedAmountByAdmin.HasValue ? _Model.GrantedAmountByAdmin : 0 };
                var UpdatedPhaseNumberParam = new SqlParameter { ParameterName = "@UpdatedPhaseNumber", Value = _Model.ProgramPhaseNumber };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@ReturnMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec SP_UpdateProgramWiseAmount @SubprogramId,@GrantedAmount,@UpdatedPhaseNumber,@ReturnMessage OUT",
                    ProgramIdParam, AmountSecondParam, UpdatedPhaseNumberParam, MessageParam);
                //int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;
            }
        }



        #endregion


        #region Anuschi

        public List<AnusuchiOneViewModel> GetAnusuchiOne(int ProvinceId, int FiscalYearId, int GrantTypeId, int FYIdForGrant)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModel> AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantParam = new SqlParameter { ParameterName = "@FiscalYearForGrant", Value = FYIdForGrant };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModel>("PopulateAnusuchiOneReport @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrant", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }

        public List<AnusuchiOneViewModelForFM> GetAnusuchiOneForArth(int ProvinceId, int FiscalYearId, int GrantTypeId, int FYIdForGrant)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModelForFM> AnusuchiOneViewModelList = new List<AnusuchiOneViewModelForFM>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantParam = new SqlParameter { ParameterName = "@FiscalYearForGrant", Value = FYIdForGrant };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModelForFM>("PopulateAnusuchiOneReportForFM @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrant", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }

        public List<AnusuchiOneViewModelForFM> GetAnusuchiOneForArth8283(int ProvinceId, int FiscalYearId, int GrantTypeId, int FYIdForGrant)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModelForFM> AnusuchiOneViewModelList = new List<AnusuchiOneViewModelForFM>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantParam = new SqlParameter { ParameterName = "@FiscalYearForGrant", Value = FYIdForGrant };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModelForFM>("PopulateAnusuchiOneReportForLL8283 @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrant", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }


        public List<AnusuchiComplementryViewModelForFM> GetAnusuchiOneForArthFM(int ProvinceId, int FiscalYearId, int GrantTypeId, int FYIdForGrant)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiComplementryViewModelForFM> AnusuchiOneViewModelList = new List<AnusuchiComplementryViewModelForFM>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantParam = new SqlParameter { ParameterName = "@FiscalYearForGrant", Value = FYIdForGrant };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiComplementryViewModelForFM>("PopulateAnusuchiOneReportForFM @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrant", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }
        //special

        public List<AnusuchiOneViewModelForFM> GetAnusuchiTwoForArth(int ProvinceId, int FiscalYearId, int GrantTypeId, int FYIdForGrant)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModelForFM> AnusuchiOneViewModelList = new List<AnusuchiOneViewModelForFM>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantParam = new SqlParameter { ParameterName = "@FiscalYearForGrant", Value = FYIdForGrant };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModelForFM>("PopulateAnusuchiTwoReportForFM @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrant", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }


        public List<AnusuchiOneViewModelForFM> GetAnusuchiTwoForArth8283(int ProvinceId, int FiscalYearId, int GrantTypeId, int FYIdForGrant)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModelForFM> AnusuchiOneViewModelList = new List<AnusuchiOneViewModelForFM>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantParam = new SqlParameter { ParameterName = "@FiscalYearForGrant", Value = FYIdForGrant };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModelForFM>("PopulateAnusuchiTwoReportForFM8283 @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrant", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }





        public List<AnusuchiOneViewModel> GetAnusuchiTwo(int ProvinceId, int FiscalYearId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModel> AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModel>("PopulateAnusuchiTwoReport @ProvinceId,@GrantTypeId,@FiscalYearId", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }

        public List<AnusuchiOneViewModel> GetAnusuchiThree(int ProvinceId, int FiscalYearId, int GrantTypeId, int FyIdForGrantAmount)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModel> AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantAmountParam = new SqlParameter { ParameterName = "@FiscalYearForGrantAmount", Value = FyIdForGrantAmount };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModel>("PopulateAnusuchiThreeReport @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrantAmount", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantAmountParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }

        public List<AnusuchiOneViewModel> GetAnusuchiFour(int ProvinceId, int FiscalYearId, int GrantTypeId, int FYIdForRequestGrant)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModel> AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantParam = new SqlParameter { ParameterName = "@FiscalYearForGrant", Value = FYIdForRequestGrant };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModel>("PopulateAnusuchiFourReport @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrant", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }


        public List<AnusuchiOneViewModel> GetAnusuchiFive(int ProvinceId, int FiscalYearId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModel> AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModel>("PopulateAnusuchiFiveReport @ProvinceId,@GrantTypeId,@FiscalYearId", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }


        public List<AnusuchiOneViewModel> GetAnusuchiSix(int ProvinceId, int FiscalYearId, int GrantTypeId, int FyIdForGrantAmount)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AnusuchiOneViewModel> AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var FiscalYearForGrantAmountParam = new SqlParameter { ParameterName = "@FiscalYearForGrantAmount", Value = FyIdForGrantAmount };
                AnusuchiOneViewModelList = db.Database.SqlQuery<AnusuchiOneViewModel>("PopulateAnusuchiSixReport @ProvinceId,@GrantTypeId,@FiscalYearId,@FiscalYearForGrantAmount", ProvinceIdParam, GrantTypeIdParam, FiscalYearIdParam, FiscalYearForGrantAmountParam).ToList();
                return AnusuchiOneViewModelList;
            }
        }



        public List<OfficeDetailsForAdminViewModel> GetOfficeDetailsProVDCWise(int ProvinceId, int DistrictCode, int VdcMunCode)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetailsForAdminViewModel> OfficeDetailsForAdminViewModelList = new List<OfficeDetailsForAdminViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictCodeParam = new SqlParameter { ParameterName = "@DistrictCode", Value = DistrictCode };
                var VDCMUNCodeParam = new SqlParameter { ParameterName = "@VDCMUNCode", Value = VdcMunCode };
                OfficeDetailsForAdminViewModelList = db.Database.SqlQuery<OfficeDetailsForAdminViewModel>("GetOfficeDetailsProVDCWise @ProvinceId,@DistrictCode,@VDCMUNCode", ProvinceIdParam, DistrictCodeParam, VDCMUNCodeParam).ToList();
                return OfficeDetailsForAdminViewModelList;
            }
        }
        #endregion


        public List<ComplementryReportViewModel> AdminReportApprovedAmountProWise(int ProvinceId, int GrantTypeId, int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("AdminReportApprovedAmountProWise @ProvinceId,@GrantTypeId,@PhaseNumber", ProvinceIdParam, GrantTypeIdParam, PhaseNumberParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ComplementryReportViewModel> AdminReportApprovedAmountVDCWise(int ProvinceId, int DistrictId, int VDCMUNId, int GrantTypeId, int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMunIdParam = new SqlParameter { ParameterName = "@VDCMunId", Value = VDCMUNId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("AdminReportApprovedAmountVDCWise @ProvinceId,@DistrictId,@VDCMunId,@GrantTypeId,@PhaseNumber", ProvinceIdParam, DistrictIdParam, VDCMunIdParam, GrantTypeIdParam, PhaseNumberParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<OfficeDetailsViewModel> GetRejectedOfficeTillDate()
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetailsViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetailsViewModel>();

                var PhaseIdParam = new SqlParameter { ParameterName = "@PhaseId", Value = 1 };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetailsViewModel>("GetRejectedOfficeTillDate @PhaseId", PhaseIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }



        public List<OfficeDetailsViewModel> GetNotGrantedAmountOfficeDetails()
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetailsViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetailsViewModel>();

                var PhaseIdParam = new SqlParameter { ParameterName = "@PhaseId", Value = 1 };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetailsViewModel>("GetNotGrantedAmountOfficeDetails @PhaseId", PhaseIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<OfficeDetailsViewModel> ApprovedOfficeListFYWise(int ProvinceId, int DistrictId, int VDCMUNID, int FYID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetailsViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetailsViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetailsViewModel>("ApprovedOfficeListFYWise @ProvinceId,@DistrictId,@VDCMUNID,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, FiscalYearIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }



        public List<OfficeDetailsViewModel> RejectedOfficeListFYWise(int ProvinceId, int DistrictId, int VDCMUNID, int FYID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetailsViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetailsViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCMUNID };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FYID };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetailsViewModel>("RejectedOfficeListFYWise @ProvinceId,@DistrictId,@VDCMUNID,@FiscalYearId", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, FiscalYearIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }




        public List<ViewApplicationDetailsByOfficeIdModel> GetApprovedApplicationByOfficeAndPhaseId(int OfficeId, int PhaseID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewApplicationDetailsByOfficeIdModel> ReturnComplementryReportViewModelList = new List<ViewApplicationDetailsByOfficeIdModel>();

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = PhaseID };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ViewApplicationDetailsByOfficeIdModel>("GetApprovedApplicationByOfficeAndPhaseId @OfficeId,@FiscalYearId", OfficeIdParam, FiscalYearIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<canceledApplicationListViewModel> AdminReport_GetCancelledApplication(int PhaseID, int FiscalYearID, int ProvinceId,int DistrcitIdVal, int VdcMunIdVal, int GrantTypeIdVal, int ProninceOrLocalLevelVal)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<canceledApplicationListViewModel> ReturnComplementryReportViewModelList = new List<canceledApplicationListViewModel>();

                var PhaseIDParam = new SqlParameter { ParameterName = "@PhaseId", Value = PhaseID };
                var FiscalYearIdparam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FiscalYearID };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };

                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrcitIdVal };
                var VdcmunIdParam = new SqlParameter { ParameterName = "@VdcmunId", Value = VdcMunIdVal };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeIdVal };
                var ProninceOrLocalLevelParam = new SqlParameter { ParameterName = "@ProninceOrLocalLevel", Value = ProninceOrLocalLevelVal };



                ReturnComplementryReportViewModelList = db.Database.SqlQuery<canceledApplicationListViewModel>("AdminReport_GetCancelledApplication @PhaseId,@FiscalYearId,@ProvinceId,@DistrictId,@VdcmunId,@GrantTypeId,@ProninceOrLocalLevel", PhaseIDParam, FiscalYearIdparam, ProvinceIdParam,DistrictIdParam,VdcmunIdParam,GrantTypeIdParam,ProninceOrLocalLevelParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ComplementryReportViewModel> GetSubprogramDetailsForEdit(int FYid, int ProvinceId, int DistrictId, int VDCId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ComplementryReportViewModel> model = new List<ComplementryReportViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = FYid };
                model = db.Database.SqlQuery<ComplementryReportViewModel>("GetApplicationDetailByVDCMun @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@ProgramPhaseNumber", ProvinceIdParam, DistrictIdParam, VDCMUNIdParam, GrantTypeIdParam, ProgramPhaseNumberParam).ToList();
                return model;


            }
        }


        public string ChangeSubProgramTimeDuration(SubProgramMaster _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = _Model.SubProgramId };
                var TimeDurationIdParam = new SqlParameter { ParameterName = "@TimeDurationId", Value = _Model.TimeDurationYear };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = _Model.OfficeId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec ChangeSubProgramTimeDuration @SubProgramId,@TimeDurationId,@OfficeId,@Message OUT",
                    SubProgramIdParam, TimeDurationIdParam, OfficeIdParam, MessageParam);
                //int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;
            }
        }

        public SubprogramDetailForPartialViewModel Partial_GetSubprogramBasicDetails(int SubProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                SubprogramDetailForPartialViewModel SubprogramDetailForPartialViewModelObj = new SubprogramDetailForPartialViewModel();

                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = SubProgramId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = 1 };
                SubprogramDetailForPartialViewModelObj = db.Database.SqlQuery<SubprogramDetailForPartialViewModel>("Partial_GetSubprogramBasicDetails @SubprogramId", SubprogramIdParam).FirstOrDefault();
                return SubprogramDetailForPartialViewModelObj;
            }
        }

        public string InsertApplicationPointFromAdmin(ReportModel _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                _Model.ObjApplicationPointsFromAdminViewModel.TotalPoints = 0;
                int TotalPoint = _Model.ObjApplicationPointsFromAdminViewModel.TotalPoints;
                if (_Model.GrantTypeIdSearch == 1)
                {
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId11 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId12 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId13 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId14 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId16 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId18 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId19 = 0;

                    TotalPoint = _Model.ObjApplicationPointsFromAdminViewModel.VariableId2 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId3 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId4 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId5 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId6;


                }
                else
                {
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId2 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId3 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId4 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId5 = 0;
                    _Model.ObjApplicationPointsFromAdminViewModel.VariableId6 = 0;

                    TotalPoint = _Model.ObjApplicationPointsFromAdminViewModel.VariableId11 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId12 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId13 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId14 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId16 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId18 + _Model.ObjApplicationPointsFromAdminViewModel.VariableId19;

                }

                _Model.ObjApplicationPointsFromAdminViewModel.TotalPoints = TotalPoint;

                _Model.ObjApplicationPointsFromAdminViewModel.PointsObtained = TotalPoint;


                string msg = string.Empty;
                var ApplicationPointByAdminIdParam = new SqlParameter { ParameterName = "@ApplicationPointByAdminId", Value = 0 };
                var ApplicationFormMasterIdParam = new SqlParameter { ParameterName = "@ApplicationFormMasterId", Value = _Model.ObjApplicationPointsFromAdminViewModel.SubprogramId };
                var VariableId2Param = new SqlParameter { ParameterName = "@VariableId2", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId2 };
                var VariableId3Param = new SqlParameter { ParameterName = "@VariableId3", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId3 };

                var VariableId4Param = new SqlParameter { ParameterName = "@VariableId4", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId4 };
                var VariableId5Param = new SqlParameter { ParameterName = "@VariableId5", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId5 };
                var VariableId6Param = new SqlParameter { ParameterName = "@VariableId6", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId6 };
                var VariableId11Param = new SqlParameter { ParameterName = "@VariableId11", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId11 };

                var VariableId12Param = new SqlParameter { ParameterName = "@VariableId12", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId12 };
                var VariableId13Param = new SqlParameter { ParameterName = "@VariableId13", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId13 };
                var VariableId14Param = new SqlParameter { ParameterName = "@VariableId14", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId14 };
                var VariableId16Param = new SqlParameter { ParameterName = "@VariableId16", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId16 };
                var VariableId18Param = new SqlParameter { ParameterName = "@VariableId18", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId18 };
                var VariableId19Param = new SqlParameter { ParameterName = "@VariableId19", Value = _Model.ObjApplicationPointsFromAdminViewModel.VariableId19 };


                var TotalNumberObtainedParam = new SqlParameter { ParameterName = "@TotalNumberObtained", Value = _Model.ObjApplicationPointsFromAdminViewModel.TotalPoints };

                var PointsObtainedParam = new SqlParameter { ParameterName = "@PointsObtained", Value = _Model.ObjApplicationPointsFromAdminViewModel.PointsObtained };

                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = _Model.GrantTypeIdSearch };


                var MessageParam = new SqlParameter
                {
                    ParameterName = "@MessagePoint",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var PrimaryIdParam = new SqlParameter
                {
                    ParameterName = "@PrimaryIdPoint",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec InsertApplicationPointFromAdmin @ApplicationPointByAdminId,@ApplicationFormMasterId,@VariableId2,@VariableId3,@VariableId4,@VariableId5,@VariableId6,@VariableId11,@VariableId12,@VariableId13,@VariableId14,@VariableId16,@VariableId18,@VariableId19,@TotalNumberObtained,@PointsObtained,@GrantTypeId,@MessagePoint OUT,@PrimaryIdPoint OUT",
                    ApplicationPointByAdminIdParam, ApplicationFormMasterIdParam, VariableId2Param, VariableId3Param, VariableId4Param, VariableId5Param, VariableId6Param, VariableId11Param, VariableId12Param, VariableId13Param, VariableId14Param, VariableId16Param, VariableId18Param, VariableId19Param, TotalNumberObtainedParam, PointsObtainedParam, GrantTypeIdParam, MessageParam, PrimaryIdParam);
                int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;

            }
        }

        public ApplicationPointsFromAdminViewModel PopulateApplicationPointFromAdmin(int SubProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                ApplicationPointsFromAdminViewModel ApplicationPointsFromAdminViewModelObj = new ApplicationPointsFromAdminViewModel();

                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = SubProgramId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = 1 };
                ApplicationPointsFromAdminViewModelObj = db.Database.SqlQuery<ApplicationPointsFromAdminViewModel>("PopulateApplicationPointFromAdmin @SubprogramId", SubprogramIdParam).FirstOrDefault();
                return ApplicationPointsFromAdminViewModelObj;
            }
        }

        public List<ComplementryReportViewModel> ProgressReportProvincesForDashBoard(int PhaseNumber, int UsertypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var UsertypeIdParam = new SqlParameter { ParameterName = "@UsertypeId", Value = UsertypeId };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("DashboardprogressreportList @PhaseNumber,@UsertypeId", PhaseNumberParam, UsertypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ProgressReportSubmitedByOfficeVM> SP_UPDashboardprogressreportList(int PhaseNumber, int UsertypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ProgressReportSubmitedByOfficeVM> ReturnComplementryReportViewModelList = new List<ProgressReportSubmitedByOfficeVM>();
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var UsertypeIdParam = new SqlParameter { ParameterName = "@UsertypeId", Value = UsertypeId };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ProgressReportSubmitedByOfficeVM>("SP_UPDashboardprogressreportList @PhaseNumber,@UsertypeId", PhaseNumberParam, UsertypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<OfficesNotSubmitedProgressRptviewModel> RPT_ProgressReportNotSubmitedOfficeList(int PhaseNumber, int UsertypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<OfficesNotSubmitedProgressRptviewModel> ReturnOfficesNotSubmitedProgressRptviewModel = new List<OfficesNotSubmitedProgressRptviewModel>();
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                var UsertypeIdParam = new SqlParameter { ParameterName = "@UsertypeId", Value = UsertypeId };

                ReturnOfficesNotSubmitedProgressRptviewModel = db.Database.SqlQuery<OfficesNotSubmitedProgressRptviewModel>("RPT_ProgressReportNotSubmitedOfficeList @PhaseNumber,@UsertypeId", PhaseNumberParam, UsertypeIdParam).ToList();
                return ReturnOfficesNotSubmitedProgressRptviewModel;
            }
        }

        #region Final Approved Result List


        public List<ComplementryReportViewModel> SP_GetFinalApprovedListByAdmin(int ProvinceId, int GrantTypeId, int ProgramPhaseNumber, int RunningOrNewId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = ProgramPhaseNumber };
                var RunningOrNewParam = new SqlParameter { ParameterName = "@RunningOrNew", Value = RunningOrNewId };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_GetFinalApprovedListByAdmin @PhaseNumber,@ProvinceId,@GrantTypeId,@RunningOrNew", ProvinceIdParam, GrantTypeIdParam, ProgramPhaseNumberParam, RunningOrNewParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ComplementryReportViewModel> SP_GetFinalApprovedListByAdminVDCWise(int ProvinceId, int DistrictId, int VDCId, int GrantTypeId, int ProgramPhaseNumber, int RunningOrNewId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                List<ComplementryReportViewModel> ReturnComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdparam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMUNIdParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = VDCId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = ProgramPhaseNumber };
                var RunningOrNewParam = new SqlParameter { ParameterName = "@RunningOrNew", Value = RunningOrNewId };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ComplementryReportViewModel>("SP_GetFinalApprovedListByAdminVDCWise @ProvinceId,@DistrictId,@VDCMUNID,@GrantTypeId,@PhaseNumber,@RunningOrNew", ProvinceIdParam, DistrictIdparam, VDCMUNIdParam, GrantTypeIdParam, ProgramPhaseNumberParam, RunningOrNewParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public string SP_RemoveProgramFromFinalList(int SubprogramId, int PhaseNumber)
        {

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = SubprogramId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };

                string msg = string.Empty;
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@MessagePoint",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var result = db.Database.ExecuteSqlCommand("exec SP_RemoveProgramFromFinalList @SubprogramId,@PhaseNumber, @MessagePoint OUT",
                     SubprogramIdParam, PhaseNumberParam, MessageParam);
                msg = MessageParam.SqlValue.ToString();
                return msg;


            }

        }


        #endregion


        public string ChangeSubmitStatusOfProgramByAdmin(int SubprogramId, int? PhaseNumber, int OfficeId)
        {

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = SubprogramId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };

                string msg = string.Empty;
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@OutputMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var result = db.Database.ExecuteSqlCommand("exec ChangeSubmitStatusOfProgramByAdmin @SubprogramId,@OfficeId,@PhaseNumber, @OutputMessage OUT",
                     SubprogramIdParam, OfficeIdParam, PhaseNumberParam, MessageParam);
                msg = MessageParam.SqlValue.ToString();
                return msg;


            }

        }

        public List<ViewAllDetailsOfOfficeViewModel> SP_GetAllDetailsOfIndividualOffice(int Phasenumber, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewAllDetailsOfOfficeViewModel> ReturnComplementryReportViewModelList = new List<ViewAllDetailsOfOfficeViewModel>();

                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = Phasenumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ViewAllDetailsOfOfficeViewModel>("SP_GetAllDetailsOfIndividualOffice @PhaseNumber,@OfficeId", PhaseNumberParam, OfficeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ViewAllDetailsOfOfficeViewModel> SP_GetAllDetailsOfIndividualOfficeUpdated(int Phasenumber, int OfficeId, int ApprovedOrRejected, int DistrictId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewAllDetailsOfOfficeViewModel> ReturnComplementryReportViewModelList = new List<ViewAllDetailsOfOfficeViewModel>();

                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = Phasenumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var ApprovedOrRejectdparam = new SqlParameter { ParameterName = "@ApprovedOrRejected", Value = ApprovedOrRejected };
                var DistrictIdparam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ViewAllDetailsOfOfficeViewModel>("SP_GetAllDetailsOfIndividualOfficeUpdated @PhaseNumber,@OfficeId,@ApprovedOrRejected,@DistrictId", PhaseNumberParam, OfficeIdParam, ApprovedOrRejectdparam, DistrictIdparam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }



        public List<ViewAllProgressReportDtlViewModel> SP_GetALlProgressReportDltByOfficeId(int Phasenumber, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewAllProgressReportDtlViewModel> ReturnComplementryReportViewModelList = new List<ViewAllProgressReportDtlViewModel>();

                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = Phasenumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ViewAllProgressReportDtlViewModel>("SP_GetALlProgressReportDltByOfficeId @OfficeId,@PhaseStatus", OfficeIdParam, PhaseNumberParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<SelectedListProvinceWiseViewModel> SP_PopulateSelectedProgramProvinceWise(int PhaseNumber, int UserType, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SelectedListProvinceWiseViewModel> PopulateProvinceWiseListForNew = new List<SelectedListProvinceWiseViewModel>();
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = PhaseNumber };//List All 
                var UserTypeParam = new SqlParameter { ParameterName = "@UserType", Value = UserType };
                var GrantTypeIdparam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };

                PopulateProvinceWiseListForNew = db.Database.SqlQuery<SelectedListProvinceWiseViewModel>("SP_PopulateSelectedProgramProvinceWise @ProgramPhaseNumber,@UserType,@GrantTypeId", ProgramPhaseNumberParam, UserTypeParam, GrantTypeIdparam).ToList();
                return PopulateProvinceWiseListForNew;
            }
        }

        public List<SelectedListProvinceWiseViewModel> SP_PopulateApprovedProgramProvinceWise(int PhaseNumber, int UserType, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SelectedListProvinceWiseViewModel> PopulateProvinceWiseListForNew = new List<SelectedListProvinceWiseViewModel>();
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = PhaseNumber };//List All 
                var UserTypeParam = new SqlParameter { ParameterName = "@UserType", Value = UserType };
                var GrantTypeIdparam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };

                PopulateProvinceWiseListForNew = db.Database.SqlQuery<SelectedListProvinceWiseViewModel>("SP_PopulateApprovedProgramProvinceWise @ProgramPhaseNumber,@UserType,@GrantTypeId", ProgramPhaseNumberParam, UserTypeParam, GrantTypeIdparam).ToList();
                return PopulateProvinceWiseListForNew;
            }
        }

        public int GetTotalSeletectedProgramCount(int PhaseNumber)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) From SubProgramMaster
                                where Status=2 and IsSelected=1 and ApprovedStatus=0 and PhaseStatus=@id", new SqlParameter("@id", PhaseNumber))
                             .FirstOrDefault();
            }
            return Total;

        }

        public string SP_UpdateFinalResultWithApprovedStatus(int ProgramPhaseNumber)
        {

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {

                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = ProgramPhaseNumber };

                string msg = string.Empty;
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@OutputMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var result = db.Database.ExecuteSqlCommand("exec SP_UpdateFinalResultWithApprovedStatus @PhaseNumber, @OutputMessage OUT",
                     PhaseNumberParam, MessageParam);
                msg = MessageParam.SqlValue.ToString();
                return msg;


            }

        }
        public List<OfficeDetalProvinceWiseViewModel> SP_GetofficeDetailProvincWise(int ProvinceId, int DistrictId, int VdcMunId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<OfficeDetalProvinceWiseViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetalProvinceWiseViewModel>();

                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictId };
                var VDCMunCodeParam = new SqlParameter { ParameterName = "@VDCMunCode", Value = VdcMunId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetalProvinceWiseViewModel>("SP_GetofficeDetailProvincWise @ProvinceId,@DistrictId,@VDCMunCode", ProvinceIdParam, DistrictIdParam, VDCMunCodeParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<ViewAllSifarishVM> SP_GetAllSifarishListByOfficeId(int OfficeId, int DistrictId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewAllSifarishVM> ReturnComplementryReportViewModelList = new List<ViewAllSifarishVM>();

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var DistrictIdparam = new SqlParameter { ParameterName = "@ProvinceId", Value = DistrictId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ViewAllSifarishVM>("SP_GetAllSifarishListByOfficeId @OfficeId,@ProvinceId", OfficeIdParam, DistrictIdparam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<ViewAllSifarishVM> SP_GetAllSifarishListByOfficeIdSpecial(int OfficeId, int DistrictId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewAllSifarishVM> ReturnComplementryReportViewModelList = new List<ViewAllSifarishVM>();

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var DistrictIdparam = new SqlParameter { ParameterName = "@ProvinceId", Value = DistrictId };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<ViewAllSifarishVM>("SP_GetAllSifarishListByOfficeIdSpecial @OfficeId,@ProvinceId", OfficeIdParam, DistrictIdparam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }

        public List<OfficeDetailsForAdminViewModel> SP_GetOfficeDetailsByParam(int userTypeId, int provinceId, int districtId, int VdcMunId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string UsertypeIdstr = string.Empty;
                string ProvinceIdStr = string.Empty;
                string DistrictIdStr = string.Empty;
                string VdcMunIdStr = string.Empty;

                if (userTypeId > 0)
                {
                    UsertypeIdstr = userTypeId.ToString();
                }
                if (provinceId > 0)
                {
                    ProvinceIdStr = provinceId.ToString();
                }
                if (districtId > 0)
                {
                    DistrictIdStr = districtId.ToString();
                }
                if (VdcMunId > 0)
                {
                    VdcMunIdStr = VdcMunId.ToString();
                }

                List<OfficeDetailsForAdminViewModel> ReturnComplementryReportViewModelList = new List<OfficeDetailsForAdminViewModel>();
                var UserTypeIdParam = new SqlParameter { ParameterName = "@UserTypeId", Value = UsertypeIdstr };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceIdStr };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictIdStr };
                var VdcMunIdParam = new SqlParameter { ParameterName = "@VdcMunId", Value = VdcMunIdStr };

                ReturnComplementryReportViewModelList = db.Database.SqlQuery<OfficeDetailsForAdminViewModel>("SP_GetOfficeDetailsByParam @UserTypeId,@ProvinceId,@DistrictId,@VdcMunId", UserTypeIdParam, ProvinceIdParam, DistrictIdParam, VdcMunIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }




        public List<GetSubprogramListForApprovedActionVM> SP_ChangeApprovedDisApprovedStatusSearch(int SubprogramId, int userTypeId,int fiscalYeraId, int provinceId, int districtId, int VdcMunId,int grantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string UsertypeIdstr = string.Empty;
                string ProvinceIdStr = string.Empty;
                string DistrictIdStr = string.Empty;
                string VdcMunIdStr = string.Empty;
                string SubprogramIdStr = string.Empty;
                string FiscalYearIdStr = string.Empty;
                string GrantTypeIdStr = string.Empty;

                if (userTypeId > 0)
                {
                    UsertypeIdstr = userTypeId.ToString();
                }
                if (provinceId > 0)
                {
                    ProvinceIdStr = provinceId.ToString();
                }
                if (districtId > 0)
                {
                    DistrictIdStr = districtId.ToString();
                }
                if (VdcMunId > 0)
                {
                    VdcMunIdStr = VdcMunId.ToString();
                }
                if (SubprogramId > 0)
                {
                    SubprogramIdStr = SubprogramId.ToString();
                }
                if (fiscalYeraId > 0)
                {
                    FiscalYearIdStr = fiscalYeraId.ToString();
                }
                if (grantTypeId > 0)
                {
                    GrantTypeIdStr = grantTypeId.ToString();
                }

                List<GetSubprogramListForApprovedActionVM> ReturnComplementryReportViewModelList = new List<GetSubprogramListForApprovedActionVM>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubprogramIdStr };

                var UserTypeIdParam = new SqlParameter { ParameterName = "@UserTypeId", Value = UsertypeIdstr };
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = FiscalYearIdStr };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceIdStr };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = DistrictIdStr };
                var VdcMunIdParam = new SqlParameter { ParameterName = "@VdcmunId", Value = VdcMunIdStr };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeIdStr };
                ReturnComplementryReportViewModelList = db.Database.SqlQuery<GetSubprogramListForApprovedActionVM>("SP_ChangeApprovedDisApprovedStatusSearch @SubProgramId,@UserTypeId,@ProgramPhaseNumber,@ProvinceId,@DistrictId,@VdcmunId,@GrantTypeId", SubProgramIdParam,UserTypeIdParam,ProgramPhaseNumberParam, ProvinceIdParam, DistrictIdParam, VdcMunIdParam,GrantTypeIdParam).ToList();
                return ReturnComplementryReportViewModelList;
            }
        }


        public List<AdhuroApuroReportViewModel> AdhuroApuroAppliedListReportForAdmin(ReportModel model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AdhuroApuroReportViewModel> AdhuroApuroViewModelList = new List<AdhuroApuroReportViewModel>();

                var SearchByParam = new SqlParameter { ParameterName = "@SearchBy", Value = model.SearchBy };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = model.ProvinceIdSearch };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = model.DistrictIdSearch };
                var VdcMunIdParam = new SqlParameter { ParameterName = "@VdcMunId", Value = model.VDCMUNIdSearch };
                AdhuroApuroViewModelList = db.Database.SqlQuery<AdhuroApuroReportViewModel>("PopulateAdhuroApuroReport @SearchBy,@ProvinceId,@DistrictId,@VdcMunId", SearchByParam,ProvinceIdParam, DistrictIdParam, VdcMunIdParam).ToList();
                return AdhuroApuroViewModelList;
            }
        }

        public List<RemainingBhuktaniGrantReportViewModel> RemainingBhuktaniRequestAppliedListReportForAdmin(ReportModel model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<RemainingBhuktaniGrantReportViewModel> AdhuroApuroViewModelList = new List<RemainingBhuktaniGrantReportViewModel>();

                var SearchByParam = new SqlParameter { ParameterName = "@SearchBy", Value = model.SearchBy };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = model.ProvinceIdSearch };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = model.DistrictIdSearch };
                var VdcMunIdParam = new SqlParameter { ParameterName = "@VdcMunId", Value = model.VDCMUNIdSearch };
                AdhuroApuroViewModelList = db.Database.SqlQuery<RemainingBhuktaniGrantReportViewModel>("PopulateRemainingBhuktaniRequestReport @SearchBy,@ProvinceId,@DistrictId,@VdcMunId", SearchByParam,ProvinceIdParam, DistrictIdParam, VdcMunIdParam).ToList();
                return AdhuroApuroViewModelList;
            }
        }


    }
}