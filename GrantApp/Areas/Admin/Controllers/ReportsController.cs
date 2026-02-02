using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;
using GrantApp.Services;
using GrantApp.Areas.Admin.Models;
using System.Net;

namespace GrantApp.Areas.Admin.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        ReportServices services = new ReportServices();


        // GET: Admin/Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SpecialGrantIndex()
        {
            ComplementryReportViewModel model = new ComplementryReportViewModel();
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.ComplementryReportViewModelList = services.PopulateReportSummary(2, 1);//1 is not submited, 2 is submited
            return View(model);
        }
        public ActionResult ComplementryIndex()
        {
            ComplementryReportViewModel model = new ComplementryReportViewModel();
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.ComplementryReportViewModelList = services.PopulateReportSummary(2, 2);//1 is not submited, 2 is submited
            return View(model);
        }

        public ActionResult DetailsSearchReport()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult DetailsSearchReport(ReportModel model)
        {
            int MainSectionId = 0;
            if (model.GrantTypeIdSearch == 1)
            {
                MainSectionId = model.MainSectionIDForOne;
            }
            else
            {
                MainSectionId = model.MainSectionIDForTwo;
            }
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNWiseReport(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.ProgramCountIdSearch, model.GrantType1Two, model.GrantType1Five, model.GrantType1Seven, model.GrantType1Eight, model.GrantType1Nine, model.GrantType1Ten, model.GrantType2ThirtyTwo, model.GrantType2TwentySix, model.GrantType2Thirty, model.GrantType2ThirtyOne, model.GrantType2ThirtyTwo, model.ProvinceVDCAmountTotalBudget, model.ProgramPhaseNumber, MainSectionId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgramPhaseNumber = model.ProgramPhaseNumber;
            return PartialView("_DetailsReports", model);

        }

        public ActionResult SearchReportProvinceWise()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            return View(model);
        }

        [HttpPost]
        public PartialViewResult SearchReportProvinceWise(ReportModel model)
        {
            int MainSectionId = 0;
            if (model.GrantTypeIdSearch == 1)
            {
                MainSectionId = model.MainSectionIDForOne;
            }
            else
            {
                MainSectionId = model.MainSectionIDForTwo;
            }
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch,model.ProgramCountIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.ProgramCountIdSearch, model.GrantType1Two, model.GrantType1Five, model.GrantType1Seven, model.GrantType1Eight, model.GrantType1Nine, model.GrantType1Ten, model.GrantType2ThirtyTwo, model.GrantType2TwentySix, model.GrantType2Thirty, model.GrantType2ThirtyOne, model.GrantType2ThirtyTwo, model.ProvinceVDCAmountTotalBudget, model.ProgramPhaseNumber, MainSectionId);
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgramPhaseNumber = model.ProgramPhaseNumber;
            return PartialView("_DetailsReportsProvincesWise", model);
        }


        public ActionResult NotSubmitedList()
        {

            ComplementryReportViewModel model = new ComplementryReportViewModel();
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.ProgramPhaseNumber = CurrentPhaseNumber;
            model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReport(1, model.GrantTypeId, CurrentPhaseNumber, model.ProvinceIDSearch);//1 is not submited, 2 is submited
            return View(model);
        }

        [HttpPost]
        public ActionResult NotSubmitedList(ComplementryReportViewModel model)
        {
            if (model.ProgramCodeNumber > 0)
            {
                model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReportByCode(model.ProgramPhaseNumber, model.ProgramCodeNumber);//1 is not submited, 2 is submited
                model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            }
            else
            {
                model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReport(1, model.GrantTypeId, model.ProgramPhaseNumber, model.ProvinceIDSearch);//1 is not submited, 2 is submited
                model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            }
            return PartialView("_NotSubmitedListForProvince", model);

        }


        public ActionResult NotSubmitedListForProvince()
        {

            ComplementryReportViewModel model = new ComplementryReportViewModel();
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.ProgramPhaseNumber = CurrentPhaseNumber;
            model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReport(1, model.GrantTypeId, CurrentPhaseNumber, model.ProvinceIDSearch);//1 is not submited, 2 is submited
            return View(model);
        }

        [HttpPost]
        public ActionResult NotSubmitedListForProvince(ComplementryReportViewModel model)
        {
            if (model.ProgramCodeNumber > 0)
            {
                model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReportByCode(model.ProgramPhaseNumber, model.ProgramCodeNumber);//1 is not submited, 2 is submited
                model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            }
            else
            {
                model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
                model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReportForProvince(model.GrantTypeId, model.ProgramPhaseNumber, model.ProvinceIDSearch);//1 is not submited, 2 is submited
                model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            }
            return PartialView("_NotSubmitedListForProvince", model);

        }


        public ActionResult NotSubmitedListForLocalLevel()
        {

            ComplementryReportViewModel model = new ComplementryReportViewModel();
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.ProgramPhaseNumber = CurrentPhaseNumber;
            model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReport(1, model.GrantTypeId, CurrentPhaseNumber, model.ProvinceIDSearch);//1 is not submited, 2 is submited
            return View(model);
        }

        [HttpPost]
        public ActionResult NotSubmitedListForLocalLevel(ComplementryReportViewModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReportForLL(model.GrantTypeId, model.ProgramPhaseNumber, model.ProvinceIDSearch, model.DistrictIdSearch, model.VdcMunIdSearch);//1 is not submited, 2 is submited
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            //if (model.ProgramCodeNumber > 0)
            //{
            //    model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //    model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReportForLL(model.GrantTypeId, model.ProgramPhaseNumber, model.ProvinceIDSearch,model.DistrictIdSearch,model.VdcMunIdSearch);//1 is not submited, 2 is submited
            //    model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            //}
            //else
            //{
            //    model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //    model.ComplementryReportViewModelList = services.PopulateNotSubmitedListReportForLL(model.GrantTypeId, model.ProgramPhaseNumber, model.ProvinceIDSearch,model.DistrictIdSearch,model.VdcMunIdSearch);//1 is not submited, 2 is submited
            //    model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            //}
            return PartialView("_NotSubmitedListForLocalLevel", model);

        }









        //This is for not submited only save grant
        public ActionResult ViewDeatilsList(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);
            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id);
            int currentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(OfficeId);

            try
            {
                model.DocumentsRequirementsViewModel = SubProgramServices.SPUP_PopulateRequiredDocForEdit(id, currentLoginUserType);
            }
            catch (Exception)
            {

                model.DocumentsRequirementsViewModel = new List<DocumentsRequirementsViewModel>();
            }
            return View(model);
        }
        public ActionResult ViewDeatilsListForSubmited(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);
            int phaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int AppliedOfficeId= model.OfficeId;
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.NewProgramInitiationModel = SubProgramServices.PopulateNewProgramInitiationDoc(phaseNumber, AppliedOfficeId, id1);
            ProgramPointsServices ObjService = new ProgramPointsServices();
            model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);
            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            //model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            model.ValuationResultViewModelList = ObjService.PopulateVariableBasisDetailForReport(id, model.ViewbagGrantTypeId).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id).Take(10).ToList();

            try
            {
                model.DocumentsRequirementsViewModel = SubProgramServices.SPUP_PopulateRequiredDocForEdit(id, 1);
            }
            catch (Exception)
            {

                model.DocumentsRequirementsViewModel = new List<DocumentsRequirementsViewModel>();
            }

            return View(model);



        }


        public ActionResult ViewDeatilsListForComplementryGrant(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            int CurrentLoginUserType = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserType();

            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);
            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);
            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id);
            return View(model);
        }


        public ActionResult ViewDeatilsListForSpecialGrant(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);
            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);
            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id);
            return View(model);
        }

        public ActionResult IdleUserList()
        {
            OfficeDetailsViewModel Model = new OfficeDetailsViewModel();
            Model.OfficeDetailsViewModelList = new List<OfficeDetailsViewModel>();
            Model.OfficeDetailsViewModelList = services.ListIdleUsers();
            return View(Model);
        }

        public ActionResult IdleUserListFYWise()
        {
            OfficeDetailsViewModel Model = new OfficeDetailsViewModel();
            Model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            return View(Model);
        }


        [HttpPost]
        public ActionResult IdleUserListFYWise(OfficeDetailsViewModel model)
        {

            model.OfficeDetailsViewModelList = new List<OfficeDetailsViewModel>();
            model.OfficeDetailsViewModelList = services.ListIdleUsersFYWise(model.ProgramPhaseNumber, model.RequestOrNotDDID, model.GrantTypeSearchID);
            return PartialView("_IdleUserFiscalYearWise", model);
        }




        public ActionResult ViewDeatilsListForResult(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();

            //model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model = SubProgramServices.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            // bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            //model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);
            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            //model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            model.ValuationResultViewModelList = ObjService.PopulateVariableBasisDetailForReport(id, model.ViewbagGrantTypeId).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id).Take(10).ToList();
            model.ObjMakeApprovedFinalListViewModel = new MakeApprovedFinalListViewModel();
            model.ObjMakeApprovedFinalListViewModel.ApprovedAmount = ObjService.GetProgramWiseAmount(id);
            return View(model);
        }



        public ActionResult DuplicateSubProgramList()
        {
            ReportModel _Model = new GrantApp.Models.ReportModel();
            ReportServices ObjService = new Services.ReportServices();
            _Model.SubProgramDuplicateViewModelList = ObjService.PopulateDuplicateSubProgramList();
            return View(_Model);
        }



        #region Program Wise Amount Insert

        public ActionResult ProgramwiseAmount(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();
            model.ObjProgramwiseAmountViewModel.ProgramId = id;
            model.ObjProgramwiseAmountViewModel.ViewBagReportViewID = id1;
            model.ObjProgramwiseAmountViewModel.ViewBagProgramTitle = services.ProgramTitleByProgramId(id);



            if (services.IsAlreadyInsertedIntoProgramWiseAmount(id) > 0)
            {
                model.ObjProgramwiseAmountViewModel = services.PopulateProgramwiseAmountByProgramId(id);
                model.ObjProgramwiseAmountViewModel.ProgramId = id;
                model.ObjProgramwiseAmountViewModel.ViewBagReportViewID = id1;
                model.ObjProgramwiseAmountViewModel.ViewBagProgramTitle = services.ProgramTitleByProgramId(id);
                return View(model);
            }
            else
            {
                return View(model);
            }


        }


        [HttpPost]
        public ActionResult ProgramwiseAmount(SubProgramMaster model)
        {
            //Check Condition ..is inserted amount greater then demand....
            SubProgramServices services = new SubProgramServices();

            if (services.AmountDemandByProgram(model.ObjProgramwiseAmountViewModel.ProgramId) < model.ObjProgramwiseAmountViewModel.Amount)
            {
                ViewBag.ErroMessage = "स्वीकृत गरिएको रकम माग गरिएको रकम भन्दा धेरै भयो ।";
                return View(model);
            }

            services.InsertProgramWiseAmountDetails(model.ObjProgramwiseAmountViewModel);

            if (model.ObjProgramwiseAmountViewModel.ViewBagReportViewID == 1)//provinces Wise
            {
                return RedirectToAction("SearchReportProvinceWise");
            }
            else
            {
                return RedirectToAction("DetailsSearchReport");
            }

        }

        public ActionResult DetailsSearchReportAmountWise()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }


        [HttpPost]
        public PartialViewResult DetailsSearchReportAmountWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNAmountReport(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_DetailsReportsAmountWise", model);

        }



        public ActionResult AdminReportGrantedAmountVDCWise()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }


        [HttpPost]
        public PartialViewResult AdminReportGrantedAmountVDCWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.AdminReportApprovedAmountVDCWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_GrantedAmountVDCWise", model);

        }


        public ActionResult SearchReportProvinceAmountWise()
        {
            ReportModel model = new ReportModel();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult SearchReportProvinceAmountWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesAmountOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch);
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_DetailsReportsProvincesAmountWise", model);
        }



        public ActionResult AdminReportGrantedAmountProWise()
        {
            ReportModel model = new ReportModel();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult AdminReportGrantedAmountProWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.ComplementryReportViewModelList = services.AdminReportApprovedAmountProWise(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber);
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_GrantedAmountProvincesWise", model);
        }

        #endregion

        #region Progress report

        public ActionResult SearchProgressRptProvinceWise()
        {
            ReportModel model = new ReportModel();
            return View(model);
        }


        [HttpPost]
        public PartialViewResult SearchProgressRptProvinceWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch,model.ProgramCountIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.SP_UPProgressReportProvinceOnlyForAdmin(model.ProvinceIdSearch, 0, 0, model.GrantTypeIdSearch, model.SubmitedOrNotSubmited, model.ProgramPhaseNumber);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgressReportViewBagId = 1;
            return PartialView("_DetailsProgressReportProvinceWise", model);
            //if (model.SubmitedOrNotSubmited == 1)
            //{
            //    model.ComplementryReportViewModelList = services.PopulateProgressReportProvincesOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, 0, model.ProgramPhaseNumber);

            //    //model.ComplementryReportViewModelList = services.PopulateProgressReportProvincesOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber);
            //    model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            //    model.ProgressReportViewBagId = 1;
            //    return PartialView("_DetailsProgressReportProvinceWise", model);
            //}
            //else
            //{
            //    model.ComplementryReportViewModelList = services.ProgressReportProvincesNotSubmitedOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, 0, model.ProgramPhaseNumber, 2);

            //    //model.ComplementryReportViewModelList = services.ProgressReportProvincesNotSubmitedOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber, 2);
            //    model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            //    model.ProgressReportViewBagId = 1;
            //    return PartialView("_DetailsProgressReportProvinceWiseNotSubmited", model);
            //}





        }


        public ActionResult SearchProgressRptProvinceWiseDetails()
        {
            ReportModel model = new ReportModel();
            return View(model);
        }


        [HttpPost]
        public PartialViewResult SearchProgressRptProvinceWiseDetails(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.ComplementryReportViewModelList = services.SP_PRProvinceOnlyForAdminDetails(model.ProvinceIdSearch, 0, 0, model.GrantTypeIdSearch, model.SubmitedOrNotSubmited, 0);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgressReportViewBagId = 1;
            return PartialView("_DetailsProgressReportProvinceWiseDtl", model);

        }


        //public ActionResult DetailsSearchProgressReport()
        //{
        //    ReportModel model = new ReportModel();
        //    model.ProvinceIdSearch = 1;
        //    model.DistrictIdSearch = 0;
        //    return View(model);
        //}

        //[HttpPost]
        //public PartialViewResult DetailsSearchProgressReport(ReportModel model)
        //{

        //    model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
        //    //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
        //    //model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNProgressReport(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, 0);//1 is not submited, 2 is submited
        //    if (model.SubmitedOrNotSubmited == 1)
        //    {
        //        model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNProgressReportForAdmin(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber);//1 is not submited, 2 is submited
        //        model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
        //        model.ProgressReportViewBagId = 2;
        //        return PartialView("_DetailsProgressReportsVDCWise", model);
        //    }
        //    else
        //    {
        //        model.ComplementryReportViewModelList = services.ProgressReportProvincesNotSubmitedOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber, 4);
        //        model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
        //        model.ProgressReportViewBagId = 2;
        //        return PartialView("_DetailsProgressReportsVDCWiseNotSubmited", model);

        //    }
        //}



        public ActionResult DetailsSearchProgressReport()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult DetailsSearchProgressReport(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNProgressReport(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, 0);//1 is not submited, 2 is submited
            //model.SubmitedOrNotSubmited = 1;//Default Assign varialbe;

            model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNProgressReportForAdmin(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.SubmitedOrNotSubmited, model.ProgramPhaseNumber);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgressReportViewBagId = 2;
            return PartialView("_DetailsProgressReportsVDCWise", model);

        }

        public ActionResult DetailsSearchProgressReportAll()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult DetailsSearchProgressReportAll(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNProgressReport(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, 0);//1 is not submited, 2 is submited
            //model.SubmitedOrNotSubmited = 1;//Default Assign varialbe;

            model.ComplementryReportViewModelList = services.SP_PRProVdcMunWiseForAdminAll(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.SubmitedOrNotSubmited, 0);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgressReportViewBagId = 2;
            return PartialView("_DetailsProgressReportsVDCWiseAll", model);

        }

        public ActionResult ViewDetailProgressReport(int id, int id1, int id2, int id3)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            //model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);

            ReportServices rs = new ReportServices();
            model.QuadrimesterReportsDetailViewModelList = rs.PopulateProgressReportForAdmin(id, 0);//subprogram id and quardid


            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            //model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            //model.ValuationResultViewModelList = ObjService.PopulateVariableBasisDetailForReport(id, model.ViewbagGrantTypeId).ToList();
            ViewBag.Mode = "Edit";
            //model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id).Take(10).ToList();
            ViewBag.ProgressReportSearchId = id3;
            return View(model);
        }
        public ActionResult ViewDetailProgressReportForAll(int id, int id1, int id2, int id3)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            //model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);

            ReportServices rs = new ReportServices();
            model.QuadrimesterReportsDetailViewModelList = rs.PopulateProgressReportForAdmin(id, 0);//subprogram id and quardid


            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            //model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            //model.ValuationResultViewModelList = ObjService.PopulateVariableBasisDetailForReport(id, model.ViewbagGrantTypeId).ToList();
            ViewBag.Mode = "Edit";
            //model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id).Take(10).ToList();
            ViewBag.ProgressReportSearchId = id3;
            return View(model);
        }
        public ActionResult AddProgressDetailByFY(int id, int id1, int id2)//subprogramid, grantypeid, fiscalyearId
        {

            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();

            model.ObjQuadrimesterReportsDetailViewModel.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetOfficeIdFromSubProgramId(id);

            model.ObjQuadrimesterReportsDetailViewModel = services.PopulateProgressReportsByFYID(id2, model.ObjQuadrimesterReportsDetailViewModel.OfficeId, id1).SingleOrDefault(x => x.ProgramId == id && x.FiscalYearId == id2);
            ViewBag.Mode = "Edit";
            if (model.ObjQuadrimesterReportsDetailViewModel == null)
            {
                model.ObjQuadrimesterReportsDetailViewModel = new QuadrimesterReportsDetailViewModel();
                model.ObjQuadrimesterReportsDetailViewModel.IsContactNoticeIssued = false;
                model.ObjQuadrimesterReportsDetailViewModel.IsContractDone = false;
                model.ObjQuadrimesterReportsDetailViewModel.IsFirstInstallmentTaken = false;
                model.ObjQuadrimesterReportsDetailViewModel.QuadrimesterReportsDetailId = 0;
                ViewBag.Mode = "Create";
            }
            if (model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount.HasValue)
            {
                if (model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount > 0)
                {
                    model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount = model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount / 100000;
                }
            }
            if (model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam.HasValue)
            {
                if (model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam > 0)
                {
                    model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam = model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam / 100000;
                }
            }

            if (model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed.HasValue)
            {
                if (model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed > 0)
                {
                    model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed = model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed / 100000;
                }
            }


            model.ObjQuadrimesterReportsDetailViewModel.ProgramId = id;
            model.ViewbagGrantTypeId = id1;
            model.ObjQuadrimesterReportsDetailViewModel.FiscalYearId = id2;
            model.ObjQuadrimesterReportsDetailViewModel.ApplicationProgressStatusId = model.ObjQuadrimesterReportsDetailViewModel.AppRunningStatus;
            return View(model);
        }

        public ActionResult ApplicationStatusDetails(int id, int id1)//appid, grantTypeId
        {

            GrantAppDBEntities db = new GrantAppDBEntities();
            int FYID = CommontUtilities.GetCurrentFiscalYearId();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetOfficeIdFromSubProgramId(id);
            ApplicationCompletionStatus acs = new ApplicationCompletionStatus();
            //first check if this applcation all progress report submited or not....

            acs = db.ApplicationCompletionStatus.Where(x => x.ApplicationId == id && x.FiscalYearId == FYID && x.OfficeId == OfficeId).FirstOrDefault();

            if (acs == null)
            {
                acs = new ApplicationCompletionStatus();
                acs.ApplicationCompletionStatusId = 0;

            }

            acs.ApplicationId = id;
            acs.GrantTypeID = id1;
            acs.FiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
            return View(acs);
        }

        public ActionResult ViewDetailProgressReportForAnusuchi(int id, int id1, int id2, int id3)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            //model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);

            ReportServices rs = new ReportServices();
            model.QuadrimesterReportsDetailViewModelList = rs.PopulateProgressReportForAdmin(id, 0);//subprogram id and quardid


            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            //model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            //model.ValuationResultViewModelList = ObjService.PopulateVariableBasisDetailForReport(id, model.ViewbagGrantTypeId).ToList();
            ViewBag.Mode = "Edit";
            //model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id).Take(10).ToList();
            ViewBag.ProgressReportSearchId = id3;
            return View(model);
        }


        #endregion


        #region Request Grant Amount Details

        public ActionResult SearchGrantAmountRequestProvinceWise()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentFiscalYearId();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult SearchGrantAmountRequestProvinceWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch,model.ProgramCountIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.PopulateGrantReqAmountProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber);
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgressReportViewBagId = 1;
            model.ProgramPhaseNumber = model.ProgramPhaseNumber;
            return PartialView("_DetailsGrantRequestAmountProvinceWise", model);
        }



        public ActionResult DetailsSearchGrantRequestAmount()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult DetailsSearchGrantRequestAmount(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited

            model.ComplementryReportViewModelList = services.PopulateGrantReqAmountProVdcMunWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber);//1 is not submited, 2 is submited

            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgressReportViewBagId = 2;
            model.ProgramPhaseNumber = model.ProgramPhaseNumber;
            return PartialView("_DetailsGrantRequestAmountVDCWise", model);

        }


        public ActionResult ViewDetailGrantRequestAmount(int id, int id1)//sub, gt, fyid
        {
            ViewRequestGrantAmountModel model = new ViewRequestGrantAmountModel();
            model = services.PopulateRequestGrantAmountDetail(1, id, id1, false);
            model.GrantedAmountByAdmin = FunctionClass.GetGrantedAmountApprovedByAdmin(id, model.ProgramPhaseNumber);
            model.ViewBagFiscalYearId = id1;
            return View(model);
        }

        [HttpPost]
        public ActionResult ApprovedRequestGrantAmount(ViewRequestGrantAmountModel model)
        {

            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            //string Resultstr = services.UpdateGrantRequestAmountByAdmin(model);
            string Resultstr = services.SP_UpdateProgramWiseAmount(model);

            return RedirectToAction("ViewDetailGrantRequestAmount", new { @id = model.SubProgramId, @id1 = model.ViewBagFiscalYearId });
        }

        #endregion


        public ActionResult AnusuchiOne()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = 3;//static code
            return View(model);

        }

        [HttpPost]
        public ActionResult AnusuchiOne(ReportModel model)
        {
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);

            model.AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();
            model.AnusuchiOneViewModelList = services.GetAnusuchiOne(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_AnusuchiOne", model);
        }



        public ActionResult AnusuchiTwo()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = 4;//static code
            return View(model);

        }



        [HttpPost]
        public ActionResult AnusuchiTwo(ReportModel model)
        {
            model.AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();
            model.AnusuchiOneViewModelList = services.GetAnusuchiTwo(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_AnusuchiTwo", model);
        }

        public ActionResult AnusuchiThree()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = 2;//static code
            return View(model);

        }
        [HttpPost]
        public ActionResult AnusuchiThree(ReportModel model)
        {
            int FYearIdForGrantAmount = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.ProgramPhaseNumber = model.ProgramPhaseNumber - 1;
            model.AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();
            model.AnusuchiOneViewModelList = services.GetAnusuchiThree(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearIdForGrantAmount);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_AnusuchiThree", model);
        }

        public ActionResult AnusuchiFour()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = 2;//static code
            return View(model);

        }
        [HttpPost]
        public ActionResult AnusuchiFour(ReportModel model)
        {
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);

            model.AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();
            model.AnusuchiOneViewModelList = services.GetAnusuchiFour(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_AnusuchiFour", model);
        }

        public ActionResult AnusuchiFive()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = 2;//static code
            return View(model);

        }
        [HttpPost]
        public ActionResult AnusuchiFive(ReportModel model)
        {
            model.AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();
            model.AnusuchiOneViewModelList = services.GetAnusuchiFive(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_AnusuchiFive", model);
        }

        public ActionResult AnusuchiSix()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = 2;//static code
            return View(model);

        }
        [HttpPost]
        public ActionResult AnusuchiSix(ReportModel model)
        {
            int FYearIdForGrantAmount = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.ProgramPhaseNumber = model.ProgramPhaseNumber - 1;
            model.AnusuchiOneViewModelList = new List<AnusuchiOneViewModel>();
            model.AnusuchiOneViewModelList = services.GetAnusuchiSix(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearIdForGrantAmount);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_AnusuchiSix", model);
        }
        public ActionResult SearchOffices()
        {
            OfficeDetailsForAdminViewModel model = new OfficeDetailsForAdminViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult SearchOffices(OfficeDetailsForAdminViewModel model)
        {
            model.OfficeDetailsForAdminViewModelList = new List<OfficeDetailsForAdminViewModel>();
            model.OfficeDetailsForAdminViewModelList = services.GetOfficeDetailsProVDCWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch);
            return PartialView("_OfficeSearchDetails", model);
        }


        //Reports to display both submited well as request grant amount---

        public ActionResult GetAllRunningApplicationList()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetAllRunningApplicationList(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.RunningApplicationListViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateGrantReqAmountProVdcMunWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber);//1 is not submited, 2 is submited

            model.ComplementryReportViewModelList = services.SP_GrantReqAmountForRunningAndNew(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber);//1 is not submited, 2 is submited
            int PhaseNumberFromFY = FunctionClass.GetPhaseNumberFromFiscalYearPhaseTable(model.ProgramPhaseNumber);
            model.RunningApplicationListViewModelList = services.SubmitedProjectListForAdmin(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber, PhaseNumberFromFY, model.ProgramCountIdSearch);
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            model.ProgressReportViewBagId = 2;
            return PartialView("_RunningApplicationList", model);

        }



        public ActionResult OfficesNotApprovedTill()
        {
            OfficeDetailsViewModel Model = new OfficeDetailsViewModel();
            Model.OfficeDetailsViewModelList = new List<OfficeDetailsViewModel>();
            Model.OfficeDetailsViewModelList = services.GetRejectedOfficeTillDate();
            return View(Model);
        }


        public ActionResult GetNotGrantedOfficeDetails()
        {
            OfficeDetailsViewModel Model = new OfficeDetailsViewModel();
            Model.OfficeDetailsViewModelList = new List<OfficeDetailsViewModel>();
            Model.OfficeDetailsViewModelList = services.GetNotGrantedAmountOfficeDetails();
            return View(Model);
        }

        public ActionResult ApprovedRejectedOfficeFYWise()
        {
            OfficeDetailsViewModel model = new OfficeDetailsViewModel();
            model.ProvinesIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult ApprovedRejectedOfficeFYWise(OfficeDetailsViewModel model)
        {
            model.OfficeDetailsViewModelList = new List<OfficeDetailsViewModel>();
            if (model.ApprovedOrRejectedIdSearch == 1)//approved
            {
                int FYID = model.ProgramPhaseNumber;
                model.OfficeDetailsViewModelList = services.ApprovedOfficeListFYWise(model.ProvinesIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.ProgramPhaseNumber);
                model.ProgramPhaseNumber = FYID;
                return PartialView("_DetailsApprovedOffice", model);
            }
            else//Rejected
            {
                model.OfficeDetailsViewModelList = services.RejectedOfficeListFYWise(model.ProvinesIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.ProgramPhaseNumber);
                return PartialView("_DetailsRejectedOffice", model);
            }
        }



        public ActionResult GetApplicationDetailsByOfficePhaseId(int id, int id1)//officeid, phaseid
        {
            ViewApplicationDetailsByOfficeIdModel model = new ViewApplicationDetailsByOfficeIdModel();
            model.ViewApplicationDetailsByOfficeIdModelList = new List<ViewApplicationDetailsByOfficeIdModel>();
            model.ViewApplicationDetailsByOfficeIdModelList = services.GetApprovedApplicationByOfficeAndPhaseId(id, id1);
            return View(model);

        }

        public ActionResult ViewApplicationDetailByOfficeId(int id, int id1, int id2)//officeid, phaseid, subprogramid
        {
            SubProgramMaster model = new SubProgramMaster();
            return View();
        }


        public ActionResult GetCanceledApplicationList()
        {
            AdminReport model = new AdminReport();
            //model.canceledApplicationListViewModelList = services.AdminReport_GetCancelledApplication(1,model.FiscalYearID);
            return View(model);

        }
        [HttpPost]
        public ActionResult GetCanceledApplicationList(AdminReport model)
        {

            model.canceledApplicationListViewModelList = services.AdminReport_GetCancelledApplication(1, model.FiscalYearID, model.ProvinceId,model.DistrictIdSearch,model.VDCMUNIdSearch,model.GrantTypeIdSearch,model.ProvinceOrLocalLevel);
            return PartialView("_GetCanceledApplicationList", model);

        }

        //application time duration change action
        public ActionResult GetApplicationDetailForEdit()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 0;
            model.DistrictIdSearch = 0;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetApplicationDetailForEdit(ReportModel model)
        {
            model.ObjComplementryReportViewModel = new ComplementryReportViewModel();
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.GetSubprogramDetailsForEdit(model.ProgramPhaseNumber, model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_ApplicationRecordForEdit", model);
        }

        public ActionResult ChangeSubProgramDetailById(int id, int id1, int id2)//subprogramid, officeid, granttypeid
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();
            model = services.PopulateSubProgram(id2).SingleOrDefault(x => x.SubProgramId == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeSubProgramDetailById(SubProgramMaster model)//subprogramid, officeid, granttypeid
        {
            //update time duration.....
            string msg = services.ChangeSubProgramTimeDuration(model);
            if (msg == "Updated Successfully")
            {
                TempData["Notifications"] = "विवरण सम्पादन भयो ।";
            }
            else
            {
                TempData["Notifications"] = msg;
            }
            return RedirectToAction("ChangeSubProgramDetailById", new { id = model.SubProgramId, id1 = model.OfficeId, id2 = model.GrantTypeId });
        }



        public ActionResult ViewNotSubmitedProgressReportList()
        {
            return View();
        }

        public ActionResult PointsFromAdmin(int id)
        {
            ReportModel model = new ReportModel();
            //model.ApplicationPointsFromAdminViewModelList = new List<ApplicationPointsFromAdminViewModel>();
            model.ObjApplicationPointsFromAdminViewModel = new ApplicationPointsFromAdminViewModel();
            return View(model);
        }

        public ActionResult SpecialPointsFromAdmin(int id, int id1)//grant type, subprogramid
        {
            ReportModel model = new ReportModel();
            //model.ApplicationPointsFromAdminViewModelList = new List<ApplicationPointsFromAdminViewModel>();
            model.ObjApplicationPointsFromAdminViewModel = new ApplicationPointsFromAdminViewModel();
            model.ObjApplicationPointsFromAdminViewModel = services.PopulateApplicationPointFromAdmin(id1);
            if (model.ObjApplicationPointsFromAdminViewModel == null)
            {
                model.ObjApplicationPointsFromAdminViewModel = new ApplicationPointsFromAdminViewModel();
            }

            model.ObjSubprogramDetailForPartialViewModel = new SubprogramDetailForPartialViewModel();
            model.ObjSubprogramDetailForPartialViewModel = services.Partial_GetSubprogramBasicDetails(id1);
            model.GrantTypeIdSearch = id;
            model.ObjApplicationPointsFromAdminViewModel.SubprogramId = id1;
            return View(model);
        }


        [HttpPost]
        public ActionResult SpecialPointsFromAdmin(ReportModel model)
        {
            services.InsertApplicationPointFromAdmin(model);
            TempData["Notifications"] = "विवरण सुरक्छित भयो । ";
            return RedirectToAction("SpecialPointsFromAdmin", new { @id = model.GrantTypeIdSearch, @id1 = model.ObjApplicationPointsFromAdminViewModel.SubprogramId });

        }

        public ActionResult ComplementryPointsFromAdmin(int id, int id1)//grant type, subprogramid
        {
            ReportModel model = new ReportModel();
            //model.ApplicationPointsFromAdminViewModelList = new List<ApplicationPointsFromAdminViewModel>();
            model.ObjApplicationPointsFromAdminViewModel = new ApplicationPointsFromAdminViewModel();
            model.ObjApplicationPointsFromAdminViewModel = services.PopulateApplicationPointFromAdmin(id1);
            if (model.ObjApplicationPointsFromAdminViewModel == null)
            {
                model.ObjApplicationPointsFromAdminViewModel = new ApplicationPointsFromAdminViewModel();
            }

            model.ObjSubprogramDetailForPartialViewModel = new SubprogramDetailForPartialViewModel();


            model.ObjSubprogramDetailForPartialViewModel = services.Partial_GetSubprogramBasicDetails(id1);
            model.GrantTypeIdSearch = id;
            model.ObjApplicationPointsFromAdminViewModel.SubprogramId = id1;
            return View(model);
        }
        [HttpPost]
        public ActionResult ComplementryPointsFromAdmin(ReportModel model)
        {
            services.InsertApplicationPointFromAdmin(model);
            TempData["Notifications"] = "विवरण सुरक्छित भयो । ";
            return RedirectToAction("ComplementryPointsFromAdmin", new { @id = model.GrantTypeIdSearch, @id1 = model.ObjApplicationPointsFromAdminViewModel.SubprogramId });
        }



        [HttpPost]
        public ActionResult PointsFromAdmin(ReportModel model)
        {
            return RedirectToAction("PointsFromAdmin", new { id = model.ObjApplicationPointsFromAdminViewModel.SubprogramId });
        }




        public ActionResult DashBoardProgressRptProvinceWise(int id, int id1)//phasenunber usertype
        {
            ReportModel model = new ReportModel();
            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            model.ProgressReportSubmitedByOfficeVMList = new List<ProgressReportSubmitedByOfficeVM>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch,model.ProgramCountIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.ProgressReportProvincesForDashBoard(id, id1);
            model.ProgressReportSubmitedByOfficeVMList = services.SP_UPDashboardprogressreportList(id, id1);
            model.ObjComplementryReportViewModel = new ComplementryReportViewModel();
            model.ObjComplementryReportViewModel.UserType = id1;
            return View(model);

        }



        public ActionResult DashBoardProgressNotSubmitedList(int id, int id1)//phasenunber usertype
        {
            ReportModel model = new ReportModel();
            model.OfficesNotSubmitedProgressRptviewModelList = new List<OfficesNotSubmitedProgressRptviewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch,model.ProgramCountIdSearch);//1 is not submited, 2 is submited
            model.OfficesNotSubmitedProgressRptviewModelList = services.RPT_ProgressReportNotSubmitedOfficeList(id, id1);
            return View(model);
        }


        [HttpPost]
        public ActionResult AddApplicationResult(SubProgramMaster model)
        {
            if (model.ObjMakeApprovedFinalListViewModel.ApprovedAmount <= 0)
            {
                TempData["Notifications"] = "अनुमोदित रकम ० भन्दा धेरै हुनु पर्दछ ।";
                return RedirectToAction("ViewDeatilsListForResult", new { @id = model.SubProgramId, @id1 = model.GrantTypeId });
            }
            CommonServices cs = new CommonServices();
            bool isSuccess = cs.SP_PrepareFinalListFromAdmin(model.SubProgramId, model.ObjMakeApprovedFinalListViewModel.ApprovedAmount, model.PhaseStatus, 1);
            if (isSuccess)
            {
                TempData["Notifications"] = "प्रमाणितको प्रक्रिया पुरा भयो  ।";
            }
            else
            {
                TempData["Notifications"] = "सिस्टममा केहि समस्या देखियो । पुनह् कोशिस गर्नुहोस् ।";
            }
            return RedirectToAction("ViewDeatilsListForResult", new { @id = model.SubProgramId, @id1 = model.GrantTypeId });
        }


        public ActionResult ListFinalResultProvinceWise()
        {
            ReportModel model = new ReportModel();
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            return View(model);

        }

        [HttpPost]
        public PartialViewResult ListFinalResultProvinceWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch,model.ProgramCountIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.SP_GetFinalApprovedListByAdmin(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber, model.RunningOrNewProgramInt);
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_ListFinalResultProvinceWise", model);
        }


        public ActionResult ListFinalResultVDCWise()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            return View(model);
            //SP_GetFinalApprovedListByAdminVDCWise

        }

        [HttpPost]
        public PartialViewResult ListFinalResultVDCWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            model.ComplementryReportViewModelList = services.SP_GetFinalApprovedListByAdminVDCWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.ProgramPhaseNumber, model.RunningOrNewProgramInt);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_ListFinalResultVDCWise", model);

        }

        [HttpPost]
        public JsonResult RemoveProgramFromFinalList(int id)
        {
            int currentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            services.SP_RemoveProgramFromFinalList(id, currentPhaseNumber);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAllSelectedProgram()
        {
            ReportModel model = new ReportModel();
            model.SelectedListProvinceWiseViewModelList = new List<SelectedListProvinceWiseViewModel>();
            model.SelectedApprovedListProvinceWiseViewModelList = new List<SelectedListProvinceWiseViewModel>();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.SelectedListProvinceWiseViewModelList = services.SP_PopulateSelectedProgramProvinceWise(CurrentPhaseNumber, 1, 1);
            model.SelectedApprovedListProvinceWiseViewModelList = services.SP_PopulateApprovedProgramProvinceWise(CurrentPhaseNumber, 1, 1);

            model.TotalSelectedProgramCount = services.GetTotalSeletectedProgramCount(CurrentPhaseNumber);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateFinalApprovedStatus()
        {
            string rtrMsg = string.Empty;
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int totalSelectedProgram = services.GetTotalSeletectedProgramCount(CurrentPhaseNumber);
            rtrMsg = services.SP_UpdateFinalResultWithApprovedStatus(CurrentPhaseNumber);
            if (rtrMsg == "Updated Successfully")
            {


                TempData["Notifications"] = "चयन गरिएका '" + totalSelectedProgram + "' आयोजना/कार्यक्रम(हरु) स्वीकृत सूचीमा राखियो ।";

            }
            else
            {
                TempData["Notifications"] = "सिस्टममा त्रुटि देखियो । पुनह कोशिस गर्नुहोस ।";

            }
            return RedirectToAction("ListAllSelectedProgram");

        }

        public ActionResult UpdateProgramSelectedToApproved()
        {

            DashboardModel model = new DashboardModel();
            model.ObjGetSubprogramDetailByIdViewModel = new GetSubprogramDetailByIdViewModel();
            DashboardServices DS = new DashboardServices();
            model.ObjGetSubprogramDetailByIdViewModel = DS.SP_GetSubprogramDetailByIDForSearch(1);
            if (model.ObjGetSubprogramDetailByIdViewModel.SubProgramId > 0)
            {
                int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
                CommonServices CS = new CommonServices();
                CS.SP_PrepareFinalListFromAdmin(1, 0, CurrentPhaseNumber, 1);//Current PhaseNumber
                //Change ProgramStatus
                return Json("T");
            }
            else
            {
                //Error
                return Json("F");
            }



        }



        [HttpPost]
        public ActionResult UpdateSubprogramSubmitStatus(SubProgramMaster model)
        {
            string rtrMessage = services.ChangeSubmitStatusOfProgramByAdmin(model.SubProgramId, model.PhaseStatus, model.OfficeId);
            if (rtrMessage == "Updated Successfully")
            {
                TempData["Notifications"] = "विवरण सम्पादन भयो ।";
            }
            else
            {
                TempData["Notifications"] = "सिस्टममा केहि समस्या देखियो । पुनह् कोशिस गर्नुहोस् ।";
            }
            return RedirectToAction("ViewDeatilsList", new { @id = model.SubProgramId, @id1 = model.ViewbagGrantTypeId });

        }


        public ActionResult ViewDeatilsApprovedRejectedOfficeFYWise(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = SubProgramServices.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);
            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            //model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            model.ValuationResultViewModelList = ObjService.PopulateVariableBasisDetailForReport(id, model.ViewbagGrantTypeId).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id).Take(10).ToList();
            return View(model);



        }

        public ActionResult AddProgramWiseAmount(int id)
        {
            return View();
        }

        public ActionResult AddApprovedAmountByAdmin(int id)
        {

            DashboardModel model = new DashboardModel();
            model.ObjGetSubprogramDetailByIdViewModel = new GetSubprogramDetailByIdViewModel();
            DashboardServices DS = new DashboardServices();
            model.ObjGetSubprogramDetailByIdViewModel = DS.SP_GetSubprogramDetailByIDForSearch(id);
            //return PartialView("~/Views/Home/_ProgramDetails.cshtml", model);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddApprovedAmountByAdmin(DashboardModel model)
        {
            DashboardServices DS = new DashboardServices();
            if (model.ObjGetSubprogramDetailByIdViewModel.ApprovedAmountByAdmin < 1)
            {

                model.ObjGetSubprogramDetailByIdViewModel = DS.SP_GetSubprogramDetailByIDForSearch(model.ObjGetSubprogramDetailByIdViewModel.SubProgramId);
                ViewBag.ErrorMessage = "रकम कम भयो ";
                return View(model);

            }
            CommonServices cs = new CommonServices();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            bool isSuccess = cs.SP_PrepareFinalListFromAdmin(model.ObjGetSubprogramDetailByIdViewModel.SubProgramId, model.ObjGetSubprogramDetailByIdViewModel.ApprovedAmountByAdmin, CurrentPhaseNumber, 1);
            if (isSuccess)
            {
                ViewBag.ErrorMessage = "अनुमोदित रकम सुरक्छित भयो  ।";
            }
            else
            {
                ViewBag.ErrorMessage = "सिस्टममा केहि समस्या देखियो । पुनह् कोशिस गर्नुहोस् ।";
            }
            model.ObjGetSubprogramDetailByIdViewModel = DS.SP_GetSubprogramDetailByIDForSearch(model.ObjGetSubprogramDetailByIdViewModel.SubProgramId);
            return View(model);
        }



        public ActionResult ViewAllDetailsProvinceWise()
        {

            ReportModel model = new ReportModel();
            model.ViewAllDetailsOfOfficeViewModelList = new List<ViewAllDetailsOfOfficeViewModel>();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.ProgramPhaseNumber = CurrentPhaseNumber;

            return View(model);
        }





        //[HttpPost]
        //public ActionResult ViewAllDetailsProvinceWise(ReportModel model)
        //{
        //    // Get the full list from SP
        //    var allRecords = services.SP_GetAllDetailsOfIndividualOffice(model.ProgramPhaseNumber, model.ProvinceIdSearch);



        //    model.ViewAllDetailsOfOfficeViewModelList = services.SP_GetAllDetailsOfIndividualOffice(model.ProgramPhaseNumber, model.ProvinceIdSearch);//1 is not submited, 2 is submited
        //    model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
        //    return PartialView("_ViewAllDetailsProvinceWise", model);

        //}

        [HttpPost]
        public ActionResult ViewAllDetailsProvinceWise(ReportModel model)
        {
            var allRecords = services.SP_GetAllDetailsOfIndividualOffice(model.ProgramPhaseNumber, model.ProvinceIdSearch);

            // Filter based on selection, skip if value is 0
            var filteredRecords = allRecords.AsQueryable();

            if (model.ViewBagGrantTypeId > 0)
            {
                filteredRecords = filteredRecords.Where(x => x.GrantTypeId == model.ViewBagGrantTypeId);
            }

            if (model.ApprovedOrRejectedStatusId > 0)
            {
                bool status = model.ApprovedOrRejectedStatusId == 1; // 1 = Approved(true), 2 = Rejected(false)
                filteredRecords = filteredRecords.Where(x => x.ApprovedStatus == status);
            }

            model.ViewAllDetailsOfOfficeViewModelList = filteredRecords.ToList();
            model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            return PartialView("_ViewAllDetailsProvinceWise", model);
        }










        public ActionResult ViewAllDetailsVDCWise()
        {
            ReportModel model = new ReportModel();
            model.ViewAllDetailsOfOfficeViewModelList = new List<ViewAllDetailsOfOfficeViewModel>();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.ProgramPhaseNumber = CurrentPhaseNumber;
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 101;
            return View(model);
        }

        [HttpPost]
        public ActionResult ViewAllDetailsVDCWise(ReportModel model)
        {
            if (model.DistrictIdSearch > 0)
            {

                model.OfficeIdSearch = FunctionClass.GetOfficeIdFromVDCMUNCODE(model.VDCMUNIdSearch);
                //model.ViewAllDetailsOfOfficeViewModelList = services.SP_GetAllDetailsOfIndividualOfficeUpdated(model.ProgramPhaseNumber, model.OfficeIdSearch, model.ApprovedOrRejectedStatusId, model.DistrictIdSearch);//1 is not submited, 2 is submited
                var allRecords = services.SP_GetAllDetailsOfIndividualOfficeUpdated(model.ProgramPhaseNumber, model.OfficeIdSearch, model.ApprovedOrRejectedStatusId, model.DistrictIdSearch);
                var filteredRecords = allRecords.AsQueryable();

                if (model.ViewBagGrantTypeId > 0)
                {
                    filteredRecords = filteredRecords.Where(x => x.GrantTypeId == model.ViewBagGrantTypeId);
                }
                model.ViewAllDetailsOfOfficeViewModelList = filteredRecords.ToList();
                model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

                return PartialView("_ViewAllDetailsVDCWise", model);

            }
            else
            {
                model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
                ViewBag.ErrorMessage = "कृपया जिल्ला छान्नुहोस ।";
                return PartialView("_ViewAllDetailsVDCWiseError", model);
            }



        }

        [HttpPost]
        public ActionResult ViewAllDetailsVDCWise123(ReportModel model)
        {
            if (model.DistrictIdSearch > 0)
            {
                if (model.VDCMUNIdSearch > 0)
                {
                    model.OfficeIdSearch = FunctionClass.GetOfficeIdFromVDCMUNCODE(model.VDCMUNIdSearch);
                    model.ViewAllDetailsOfOfficeViewModelList = services.SP_GetAllDetailsOfIndividualOffice(model.ProgramPhaseNumber, model.OfficeIdSearch);//1 is not submited, 2 is submited
                    model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

                    return PartialView("_ViewAllDetailsVDCWise", model);
                }
                else
                {
                    model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

                    return PartialView("_ViewAllDetailsVDCWiseError", model);
                }
            }
            else
            {
                model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
                ViewBag.ErrorMessage = "कृपया जिल्ला छान्नुहोस ।";
                return PartialView("_ViewAllDetailsVDCWiseError", model);
            }



        }

        public ActionResult ChangeProgramSubmitStatus(string ProgramID)
        {
            int SubprogramId = 0;
            if (!string.IsNullOrEmpty(ProgramID))
            {
                SubprogramId = Convert.ToInt32(ProgramID);
            }

            DashboardModel model = new DashboardModel();
            model.ObjGetSubprogramDetailByIdViewModel = new GetSubprogramDetailByIdViewModel();
            DashboardServices DS = new DashboardServices();
            model.ObjGetSubprogramDetailByIdViewModel = DS.SP_GetSubprogramDetailByIDForSearch(SubprogramId);
            if (model.ObjGetSubprogramDetailByIdViewModel.SubProgramId > 0)
            {
                int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
                CommonServices CS = new CommonServices();
                CS.SP_UpdateProgramSubmitStatusByAdmin(SubprogramId, "Admin", "UpdatedSubmitStatus", CurrentPhaseNumber);//Current PhaseNumber
                //Change ProgramStatus
                return Json("T");
            }
            else
            {
                //Error
                return Json("F");
            }



        }


        public ActionResult ViewAllSifarish()
        {
            ReportModel model = new ReportModel();
            model.ViewAllDetailsOfOfficeViewModelList = new List<ViewAllDetailsOfOfficeViewModel>();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.ProgramPhaseNumber = CurrentPhaseNumber;
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 101;
            return View(model);
        }



        [HttpPost]
        public ActionResult ViewAllSifarish(ReportModel model)
        {

            model.OfficeIdSearch = FunctionClass.GetOfficeIdFromVDCMUNCODE(model.VDCMUNIdSearch);
            model.ViewAllSifarishVMList = new List<ViewAllSifarishVM>();
            if (model.ViewBagGrantTypeId == 2)
            {
                model.ViewAllSifarishVMList = services.SP_GetAllSifarishListByOfficeId(model.OfficeIdSearch, model.ProvinceIdSearch);//1 is not submited, 2 is submited
                model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
                return PartialView("_ViewAllSifarishVdcMunWise", model);
            }
            else
            {
                model.ViewAllSifarishVMList = services.SP_GetAllSifarishListByOfficeIdSpecial(model.OfficeIdSearch, model.ProvinceIdSearch);//1 is not submited, 2 is submited
                model.ViewBagProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
                return PartialView("_ViewAllSifarishVdcMunWiseBishesh", model);
            }



        }
        //province Level
        public ActionResult ListWorkCompletedAayojana()
        {
            CommonProvider cp = new CommonProvider();
            ApplicationCompletionStatusVM model = new ApplicationCompletionStatusVM();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            return View(model);
        }
        //province Level
        [HttpPost]
        public ActionResult ListWorkCompletedAayojana(ApplicationCompletionStatusVM model)
        {
            CommonProvider cp = new CommonProvider();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int provinceLevel = (int)ProvinceOrLocalLevel.provinceLevel;
            int completeApplication = (int)AayojanaCompletionStatusEnum.complete;
            int officeId = 0;
            int FiscalYearId = 0;
            model.ApplicationCompletionStatusVMList = cp.SPUP_GetApplicationCompletionStatus(completeApplication, officeId, FiscalYearId, provinceLevel, model.ProvinceIdSearch, model.DistrictIdSearch, model.VdcmunIdSearch);//1 is complete
            return PartialView("_ListWorkCompletedAayojana", model);
        }
        //VDC
        public ActionResult ListWorkCompletedAayojanaVDC()
        {
            CommonProvider cp = new CommonProvider();
            ApplicationCompletionStatusVM model = new ApplicationCompletionStatusVM();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            return View(model);
        }
        //VDC
        [HttpPost]
        public ActionResult ListWorkCompletedAayojanaVDC(ApplicationCompletionStatusVM model)
        {
            CommonProvider cp = new CommonProvider();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int provinceLevel = (int)ProvinceOrLocalLevel.localLevel;
            int completeApplication = (int)AayojanaCompletionStatusEnum.complete;
            int officeId = 0;
            int FiscalYearId = 0;
            model.ApplicationCompletionStatusVMList = cp.SPUP_GetApplicationCompletionStatus(completeApplication, officeId, FiscalYearId, provinceLevel, model.ProvinceIdSearch, model.DistrictIdSearch, model.VdcmunIdSearch);//1 is complete
            return PartialView("_ListWorkCompletedAayojanaVDC", model);
        }



        //Province Wise
        public ActionResult ListWorkNotCompletedAayojana()
        {
            CommonProvider cp = new CommonProvider();
            ApplicationCompletionStatusVM model = new ApplicationCompletionStatusVM();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int notCompletedRequestAgain = 2;
            int provinceLevel = 2;
            return View(model);
        }

        [HttpPost]
        public ActionResult ListWorkNotCompletedAayojana(ApplicationCompletionStatusVM model)
        {
            CommonProvider cp = new CommonProvider();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int notCompletedRequestAgain = (int)AayojanaCompletionStatusEnum.notCompleted;
            int provinceLevel = (int)ProvinceOrLocalLevel.provinceLevel;
            model.ApplicationCompletionStatusVMList = cp.SPUP_GetApplicationCompletionStatus(notCompletedRequestAgain, 0, 0, provinceLevel, model.ProvinceIdSearch, model.DistrictIdSearch, model.VdcmunIdSearch);//1 is complete
            return PartialView("_ListWorkNotCompletedAayojanaProvince", model);
        }


        public ActionResult ListWorkNotCompletedAayojanaVDCWise()
        {
            CommonProvider cp = new CommonProvider();
            ApplicationCompletionStatusVM model = new ApplicationCompletionStatusVM();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int notCompletedRequestAgain = 2;
            return View(model);
        }

        [HttpPost]
        public ActionResult ListWorkNotCompletedAayojanaVDCWise(ApplicationCompletionStatusVM model)
        {
            CommonProvider cp = new CommonProvider();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int notCompletedRequestAgain = (int)AayojanaCompletionStatusEnum.notCompleted;
            int localLevel = (int)ProvinceOrLocalLevel.localLevel;
            model.ApplicationCompletionStatusVMList = cp.SPUP_GetApplicationCompletionStatus(notCompletedRequestAgain, 0, model.ViewFiscalYearId, localLevel, model.ProvinceIdSearch,model.DistrictIdSearch, model.VdcmunIdSearch);//1 is complete
            return PartialView("_ListWorkNotCompletedAayojanaVDCWise", model);
        }


        //Province wise
        public ActionResult ListDroppedAayojana()
        {
            CommonProvider cp = new CommonProvider();
            ApplicationCompletionStatusVM model = new ApplicationCompletionStatusVM();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int provinceOrLocalLevel = 0;
            return View(model);
        }
        //Province wise

        [HttpPost]
        public ActionResult ListDroppedAayojana(ApplicationCompletionStatusVM model)
        {
            CommonProvider cp = new CommonProvider();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int provinceOrLocalLevel = 2;
            int droppedAayojana = (int)AayojanaCompletionStatusEnum.dropped;
            model.ApplicationCompletionStatusVMList = cp.SPUP_GetApplicationCompletionStatus(droppedAayojana, 0, 0, provinceOrLocalLevel, model.ProvinceIdSearch, model.DistrictIdSearch, model.VdcmunIdSearch);//1 is complete
            return PartialView("_ListDroppedAayojanaProvince", model);
        }




        //VDC wise
        public ActionResult ListDroppedAayojanaVDC()
        {
            CommonProvider cp = new CommonProvider();
            ApplicationCompletionStatusVM model = new ApplicationCompletionStatusVM();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int provinceOrLocalLevel = 0;
            return View(model);
        }
        //VDC wise

        [HttpPost]
        public ActionResult ListDroppedAayojanaVDC(ApplicationCompletionStatusVM model)
        {
            CommonProvider cp = new CommonProvider();
            model.ApplicationCompletionStatusVMList = new List<ApplicationCompletionStatusVM>();
            int provinceOrLocalLevel = 4;
            int droppedAayojana = (int)AayojanaCompletionStatusEnum.dropped;
            model.ApplicationCompletionStatusVMList = cp.SPUP_GetApplicationCompletionStatus(droppedAayojana, 0, 0, provinceOrLocalLevel, model.ProvinceIdSearch, model.DistrictIdSearch, model.VdcmunIdSearch);//1 is complete
            return PartialView("_ListDroppedAayojanaVDC", model);
        }





        public ActionResult OfficeDetailsByProvince()
        {

            OfficeDetailsForAdminViewModel model = new OfficeDetailsForAdminViewModel();
            model.OfficeDetailsForAdminViewModelList = services.SP_GetOfficeDetailsByParam(2, model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch);//1 is not submited, 2 is submited

            return View(model);
        }

        [HttpPost]
        public ActionResult OfficeDetailsByProvince(OfficeDetailsForAdminViewModel model)
        {
            model.OfficeDetailsForAdminViewModelList = services.SP_GetOfficeDetailsByParam(2, model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch);//1 is not submited, 2 is submited

            return PartialView("_OfficeDetailsByProvinceId", model);

        }


        public ActionResult OfficeDetailsByLocalLevel()
        {

            OfficeDetailsForAdminViewModel model = new OfficeDetailsForAdminViewModel();
            model.ProvinceIdSearch = 1;
            model.OfficeDetailsForAdminViewModelList = services.SP_GetOfficeDetailsByParam(4, model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch);//1 is not submited, 2 is submited

            return View(model);

        }

        [HttpPost]
        public ActionResult OfficeDetailsByLocalLevel(OfficeDetailsForAdminViewModel model)
        {

            model.OfficeDetailsForAdminViewModelList = services.SP_GetOfficeDetailsByParam(4, model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch);//1 is not submited, 2 is submited

            return PartialView("_OfficeDetailsByLocalLevelId", model);

        }



        public ActionResult ProvicneApprovedActionList()
        {

            GetSubprogramListForApprovedActionVM model = new GetSubprogramListForApprovedActionVM();
            model.ProvinceIdSearch = 1;
            return View(model);

        }

        [HttpPost]
        public ActionResult ProvicneApprovedActionList(GetSubprogramListForApprovedActionVM model)
        {

            model.GetSubprogramListForApprovedActionVMList = services.SP_ChangeApprovedDisApprovedStatusSearch(0, 2, model.FiscalYearIdSearch, model.ProvinceIdSearch, model.DistrictIdSearch, model.VdcmunIdSearch, model.GrantTypeIdSearch);

            return PartialView("_ProvicneApprovedActionList", model);

        }

        public ActionResult DoAction(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DoAction(SubProgramMaster model)
        {
            if (model.ApprovedStatus.HasValue)
            {
                if (model.ApprovedStatus == true)
                {
                    SubProgramServices SubProgramServices = new SubProgramServices();
                    SubProgramServices.SP_SP_MakeProgramDisApprovedByAdmin(model);

                }
                else
                {
                    if (!model.ApprovedBudgetByNPC.HasValue)
                    {
                        SubProgramServices SubProgramServices = new SubProgramServices();
                        model.IPAddress = GetIPAddress();
                        model = SubProgramServices.PopulateSubProgramBySupprogramAndGrantTypeId(model.SubProgramId, model.GrantTypeId);
                        return View(model);
                    }
                    else
                    {
                        if (model.ApprovedBudgetByNPC > 1)
                        {
                            model.ApprovedBudgetByNPC = model.ApprovedBudgetByNPC * 100000;
                            SubProgramServices subProgramServices = new SubProgramServices();
                            model.IPAddress = GetIPAddress();
                            string outputMessage = subProgramServices.SP_MakeProgramApprovedByAdmin(model);
                            if (outputMessage == "Successfully")
                            {
                                TempData["Success"] = "Approved Successfully";
                            }
                            else
                            {
                                TempData["Success"] = "Error Please try again";
                            }

                        }
                    }
                }
            }



            return RedirectToAction("DoAction", new { @id = model.SubProgramId, id1 = model.GrantTypeId });
        }


        public ActionResult LocalevelApprovedActionList()
        {

            GetSubprogramListForApprovedActionVM model = new GetSubprogramListForApprovedActionVM();
            model.ProvinceIdSearch = 1;
            return View(model);

        }

        [HttpPost]
        public ActionResult LocalevelApprovedActionList(GetSubprogramListForApprovedActionVM model)
        {

            model.GetSubprogramListForApprovedActionVMList = services.SP_ChangeApprovedDisApprovedStatusSearch(0, 4, model.FiscalYearIdSearch, model.ProvinceIdSearch, model.DistrictIdSearch, model.VdcmunIdSearch, model.GrantTypeIdSearch);

            return PartialView("_LocalLevelApprovedActionList", model);

        }

        public ActionResult DoActionForLocalLevel(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DoActionForLocalLevel(SubProgramMaster model)
        {
            if (model.ApprovedStatus.HasValue)
            {
                if (model.ApprovedStatus == true)
                {
                    SubProgramServices SubProgramServices = new SubProgramServices();
                    model.IPAddress = GetIPAddress();
                    SubProgramServices.SP_SP_MakeProgramDisApprovedByAdmin(model);

                }
                else
                {
                    if (!model.ApprovedBudgetByNPC.HasValue)
                    {
                        SubProgramServices SubProgramServices = new SubProgramServices();
                        model = SubProgramServices.PopulateSubProgramBySupprogramAndGrantTypeId(model.SubProgramId, model.GrantTypeId);
                        return View(model);
                    }
                    else
                    {
                        if (model.ApprovedBudgetByNPC > 1)
                        {
                            model.ApprovedBudgetByNPC = model.ApprovedBudgetByNPC * 100000;
                            SubProgramServices subProgramServices = new SubProgramServices();
                            model.IPAddress = GetIPAddress();
                            string outputMessage = subProgramServices.SP_MakeProgramApprovedByAdmin(model);
                            if (outputMessage == "Successfully")
                            {
                                TempData["Success"] = "Approved Successfully";
                            }
                            else
                            {
                                TempData["Success"] = "Error Please try again";
                            }

                        }
                    }
                }
            }



            return RedirectToAction("DoActionForLocalLevel", new { @id = model.SubProgramId, id1 = model.GrantTypeId });
        }




        public string GetIPAddress()
        {
            try
            {
                string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                string host = Dns.GetHostName();
                return $"{host}-{ipAddress}";

            }
            catch (Exception)
            {

                return "192.168.1.1";
            }



        }


        public ActionResult ChangeSuccessfully()
        {
            return View();
        }


        public ActionResult ProgressRptProvinceQuardWise()
        {
            ReportModel model = new ReportModel();
            return View(model);
        }


        [HttpPost]
        public PartialViewResult ProgressRptProvinceQuardWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvincesWiseOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch,model.ProgramCountIdSearch);//1 is not submited, 2 is submited
            if (model.SubmitedOrNotSubmited == 1)
            {
                model.ComplementryReportViewModelList = services.PopulateProgressReportProvincesOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber);
                model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
                model.ProgressReportViewBagId = 1;
                return PartialView("_ProgressReportProvinceQuardWise", model);
            }
            else
            {
                model.ComplementryReportViewModelList = services.ProgressReportProvincesNotSubmitedOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber, 2);
                model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
                model.ProgressReportViewBagId = 1;
                return PartialView("_ProgressReportProvinceQuardWiseNotSubmited", model);
            }





        }


        public ActionResult DetailsSearchProgressReportQuardWise()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult DetailsSearchProgressReportQuardWise(ReportModel model)
        {

            model.ComplementryReportViewModelList = new List<ComplementryReportViewModel>();
            //model.ComplementryReportViewModelList = services.PopulateSubProgramProvinceDistrictWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.GrantTypeIdSearch);//1 is not submited, 2 is submited
            //model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNProgressReport(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, 0);//1 is not submited, 2 is submited
            if (model.SubmitedOrNotSubmited == 1)
            {
                model.ComplementryReportViewModelList = services.ProvinceDistrictVDCMUNProgressReportForAdmin(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber);//1 is not submited, 2 is submited
                model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
                model.ProgressReportViewBagId = 2;
                return PartialView("_ProgressReportsVDCQuardWise", model);
            }
            else
            {
                model.ComplementryReportViewModelList = services.ProgressReportProvincesNotSubmitedOnly(model.ProvinceIdSearch, model.GrantTypeIdSearch, model.QuirdId, model.ProgramPhaseNumber, 4);
                model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
                model.ProgressReportViewBagId = 2;
                return PartialView("_ProgressReportsVDCQuardWiseNotSubmited", model);

            }
        }

        public ActionResult ArthaAnusuchiOne()//this is four
        {
            ReportModel model = new ReportModel();
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.AnusuchiOneViewModelForFMList = new List<AnusuchiOneViewModelForFM>();
            model.AnusuchiOneViewModelForFMList = services.GetAnusuchiOneForArth(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return View(model);

        }

        public ActionResult ArthaAnusuchiOne8283()//this is four
        {
            ReportModel model = new ReportModel();
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.AnusuchiOneViewModelForFMList = new List<AnusuchiOneViewModelForFM>();
            model.AnusuchiOneViewModelForFMList = services.GetAnusuchiOneForArth8283(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return View(model);

        }

        public ActionResult ArthaAnusuchiTwo()
        {
            ReportModel model = new ReportModel();
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.AnusuchiOneViewModelForFMList = new List<AnusuchiOneViewModelForFM>();
            model.AnusuchiOneViewModelForFMList = services.GetAnusuchiTwoForArth(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return View(model);

        }

        public ActionResult ArthaAnusuchiTwo8283()
        {
            ReportModel model = new ReportModel();
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.AnusuchiOneViewModelForFMList = new List<AnusuchiOneViewModelForFM>();
            model.AnusuchiOneViewModelForFMList = services.GetAnusuchiTwoForArth8283(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return View(model);

        }


        public ActionResult AdhuroApuroList()
        {

            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            return View(model);
    

        }

        [HttpPost]
        public ActionResult AdhuroApuroList(ReportModel model)
        {

            model.AdhuroApuroGrantRequestListVM = new List<AdhuroApuroReportViewModel>();
            model.AdhuroApuroGrantRequestListVM = services.AdhuroApuroAppliedListReportForAdmin(model);
            
            return PartialView("_AdhuroApuroList", model);

        }


        public ActionResult GetAdhuroApuroDetailById(int id)
        {
            AdhuroApuroReportViewModel model = new AdhuroApuroReportViewModel();
            AdhuroApuroServices serv = new AdhuroApuroServices();
            model = serv.SUP_GetAdhuroApuroDetailById(id);
            return View(model);
        }


        public ActionResult RemainingBhuktaniList()
        {

            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 0;
            model.DistrictIdSearch = 0;
            return View(model);


        }

        [HttpPost]
        public ActionResult RemainingBhuktaniList(ReportModel model)
        {

            model.RemainingBhuktaniGrantRequestListVM = new List<RemainingBhuktaniGrantReportViewModel>();
            model.RemainingBhuktaniGrantRequestListVM = services.RemainingBhuktaniRequestAppliedListReportForAdmin(model);
            return PartialView("_RemainingBhuktaniList", model);

        }


        public ActionResult GetRemainingBhuktaniDetailById(int id)
        {
            RemainingBhuktaniGrantReportViewModel model = new RemainingBhuktaniGrantReportViewModel();
            AdhuroApuroServices serv = new AdhuroApuroServices();
            model = serv.SUP_GetRemainingBhuktaniDetailById(id);
            return View(model);
        }


        //public ActionResult PublicGrantRequestList()
        //{

        //    ReportModel model = new ReportModel();
        //    model.ProvinceIdSearch = 0;
        //    model.DistrictIdSearch = 0;
        //    return View(model);


        //}

        //[HttpPost]
        //public ActionResult PublicGrantRequestList(ReportModel model)
        //{

        //    model.RemainingBhuktaniGrantRequestListVM = new List<RemainingBhuktaniGrantReportViewModel>();
        //    model.RemainingBhuktaniGrantRequestListVM = services.RemainingBhuktaniRequestAppliedListReportForAdmin(model);
        //    return PartialView("_PublicGrantRequestList", model);

        //}


        //public ActionResult GetPublicGrantRequestDetailById(int id)
        //{
        //    RemainingBhuktaniGrantReportViewModel model = new RemainingBhuktaniGrantReportViewModel();
        //    AdhuroApuroServices serv = new AdhuroApuroServices();
        //    model = serv.SUP_GetRemainingBhuktaniDetailById(id);
        //    return View(model);
        //}



        public FileResult Download(string FileName)
        {
            var FileVirtualPath = "~/RequiredDocs/" + FileName;
            return File(FileVirtualPath, "application/force-download", System.IO.Path.GetFileName(FileVirtualPath));
        }



    }
}