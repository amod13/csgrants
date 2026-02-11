using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Services;
using GrantApp.Areas.Admin.Models;
using GrantApp.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace GrantApp.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminHomeController : Controller
    {
        int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();//-1;

        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            DashboardServices services = new DashboardServices();
            DashboardModel model = new DashboardModel();
            model.DashboardModelList = services.PopulateSectionWiseProgramCount(0, CurrentPhaseNumber);
            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            DashboardServices services = new DashboardServices();
            DashboardModel model = new DashboardModel();
            model.ObjAdminDashboardNewprojectCountListViewModel = new AdminDashboardNewprojectCountListViewModel();

            //model.DashboardModelList = services.PopulateSectionWiseProgramCount(0, CurrentPhaseNumber);
            //model.DashboardModelListSpecialGrant = services.PopulateSectionWiseProgramCount(1, CurrentPhaseNumber);
            //model.DashboardModelListComplemnetryGrant = services.PopulateSectionWiseProgramCount(2, CurrentPhaseNumber);


            int PreviousPhaseNumber = CurrentPhaseNumber - 1;
            model.ObjAdminDashboardNewprojectCountListViewModel = services.AdminDashboardNewprojectCountList(PreviousPhaseNumber);
            model.SecionWiseProgramCountViewModelList = new List<SecionWiseProgramCountViewModel>();
            model.SecionWiseProgramCountViewModelList = services.AdminDashboardCountMainSectionWise(CurrentPhaseNumber);
            model.objTotalProgramListPhaseWiseNewModel = new TotalProgramListPhaseWiseNewModel();
            model.objTotalProgramListPhaseWiseNewModel = services.spCountProgramNumberWithPhase(CurrentPhaseNumber);

            return View(model);
        }
        public ActionResult DashboardDetails(int id, int id1)
        {
            DashboardServices services = new DashboardServices();
            DashboardModel model = new DashboardModel();
            model.ObjAdminDashboardNewprojectCountListViewModel = new AdminDashboardNewprojectCountListViewModel();
            model.DashboardModelList = services.PopulateSectionWiseProgramCount(0, CurrentPhaseNumber);
            model.DashboardModelListSpecialGrant = services.PopulateSectionWiseProgramCount(1, CurrentPhaseNumber);
            model.DashboardModelListComplemnetryGrant = services.PopulateSectionWiseProgramCount(2, CurrentPhaseNumber);
            int PreviousPhaseNumber = CurrentPhaseNumber-1;
            model.ObjAdminDashboardNewprojectCountListViewModel = services.AdminDashboardNewprojectCountList(PreviousPhaseNumber);
            model.SecionWiseProgramCountViewModelList = new List<SecionWiseProgramCountViewModel>();
            model.SecionWiseProgramCountViewModelList = services.AdminDashboardCountMainSectionWise(CurrentPhaseNumber);
            model.ProgramPhaseNumberStr = FunctionClass.GetPhaseTitleBYPhaseNumber(8);
            model.ProgramPhaseNumber = CurrentPhaseNumber;
            int RGAFYID = FunctionClass.GetRGAFiscalYearByPhaseNumber(CurrentPhaseNumber);
            model.RGAFiscalYearId = RGAFYID;
            return View(model);

        }
        [HttpGet]
        public ActionResult ViewReportFiscalYearWise()
        {
            DashboardModel model = new DashboardModel();
            return View(model);
        }


        [HttpPost]
        public ActionResult ViewReportFiscalYearWise(DashboardModel model)
        {
            DashboardServices services = new DashboardServices();
            model.DashboardModelList = services.PopulateSectionWiseProgramCount(0, model.ProgramPhaseNumber);
            model.DashboardModelListSpecialGrant = services.PopulateSectionWiseProgramCount(1, model.ProgramPhaseNumber);
            model.DashboardModelListComplemnetryGrant = services.PopulateSectionWiseProgramCount(2, model.ProgramPhaseNumber);
            return View(model);


        }


        public ActionResult ChangePassword()
        {
            DashboardModel model = new DashboardModel();
            return View(model);
        }



        [HttpPost]
        public ActionResult ChangePassword(DashboardModel model)
        {
            CommonProvider cp = new CommonProvider();
            if (cp.ChangeUserPassword(model.SectionName) == "Updated Successfully")
            {
                TempData["Notifications"] = "पासवर्ड परिवर्तन भयो ।";
            }
            //here sectionname is username...static code....

            return RedirectToAction("ChangePassword");
        }


        public ActionResult UpdateMunTitle()
        {
            VDCMUNVIewModel model = new VDCMUNVIewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMunTitle(VDCMUNVIewModel model)
        {
            if (model.VDCMUNCode <= 0)
            {
                ViewBag.ErrorMessage = "कृपया गापा/नपा छान्नुहोस ।";
                return View(model);
            }

            DashboardServices ds = new DashboardServices();
            string msg = ds.UpdateVDCMUNName(model);
            if (msg == "Updated Successfully")
            {
                TempData["Notifications"] = "गापा/नपा को नाम परिवर्तन भयो ।";
            }
            else
            {
                TempData["Notifications"] = "सिस्टममा समस्या आयो। कृपया पुनह् कोशिस गर्नुहोस ।";
            }

            return RedirectToAction("UpdateMunTitle");
        }


        public ActionResult ViewDashboardNewProgramList(int id, int id1, int id2, int? id3)//grant type, phasenumber
        {
            AdminReport model = new AdminReport();
            DashboardServices services = new DashboardServices();
            model.DashboardNewProgramListViewModelList = services.DashboradPopulateProgramList(id, id1, id2);
            model.ViewNewProgramProvinceWiseStatusVMList = services.SP_AdminDashboardNewProgramStatus(id1, 1, id);
            model.PageNumber = id3 ?? 1;
            model.TotalPageNumberToDisplay = 100;
            model.pageList = model.DashboardNewProgramListViewModelList.ToPagedList(model.PageNumber, model.TotalPageNumberToDisplay);
            model.FiscalYearID = id1;
            model.GrantTypeId = id;
            model.ProvinceId = id2;


            model.CurrentPhaseNumber = CurrentPhaseNumber;

            return View(model);
        }

        public ActionResult ViewDashboardRequestAmountList(int id, int id1, int id2)//id2=provinceid
        {
            AdminReport model = new AdminReport();
            DashboardServices services = new DashboardServices();
            model.DashboardNewProgramListViewModelList = services.DashboradPopulateRequestedAmountList(id, id1, id2);
            model.ViewNewProgramProvinceWiseStatusVMList = services.SP_AdminDashboardNewRequestedProgramStatus(18, 0);//rga fiscal yearid, granttypeid
            model.ProvinceId = id2;
            return View(model);
        }


        public ActionResult ListOffices()
        {

            return View();
        }


        public ActionResult CheckProgramDetails(string ProgramID)
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
                return Json("T");
            }
            else
            {
                return Json("F");
            }

            //if (result != null && result.Rows.Count > 0)
            //{
            //    return Json("T");
            //}
            //else
            //{
            //    return Json("F");
            //}

        }


        public ActionResult GetProgramDetailsByID(string id)
        {
            int SubprogramId = 0;
            if (!string.IsNullOrEmpty(id))
            {
                SubprogramId = Convert.ToInt32(id);
            }
            DashboardModel model = new DashboardModel();
            model.ObjGetSubprogramDetailByIdViewModel = new GetSubprogramDetailByIdViewModel();
            DashboardServices DS = new DashboardServices();
            model.ObjGetSubprogramDetailByIdViewModel = DS.SP_GetSubprogramDetailByIDForSearch(SubprogramId);
            //return PartialView("~/Views/Home/_ProgramDetails.cshtml", model);
            return PartialView("__ProgramDetails", model);
        }

        public ActionResult GetProgramDetailsByIDError(string id)
        {
            int SubprogramId = 0;
            if (!string.IsNullOrEmpty(id))
            {
                SubprogramId = Convert.ToInt32(id);
            }
            DashboardModel model = new DashboardModel();
            model.ObjGetSubprogramDetailByIdViewModel = new GetSubprogramDetailByIdViewModel();
            DashboardServices DS = new DashboardServices();
            model.ObjGetSubprogramDetailByIdViewModel = DS.SP_GetSubprogramDetailByIDForSearch(SubprogramId);
            //return PartialView("~/Views/Home/_ProgramDetails.cshtml", model);
            return PartialView("__ProgramDetailsError", model);
        }

        public ActionResult GetSectionWiseCountDetails(string id)
        {
            int Phasenumber = 0;
            if (!string.IsNullOrEmpty(id))
            {
                Phasenumber = Convert.ToInt32(id);
            }
            DashboardServices services = new DashboardServices();
            DashboardModel model = new DashboardModel();
            model.SecionWiseProgramCountViewModelList = new List<SecionWiseProgramCountViewModel>();
            model.SecionWiseProgramCountViewModelList = services.AdminDashboardCountMainSectionWise(Phasenumber);
            return PartialView("__SectionWiseCountDetails", model);
        }


        public ActionResult ViewDeatilsFromDashboard(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();

            model = SubProgramServices.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);

            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id, UserTypeId);
            model.ViewbagGrantTypeId = id1;
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            ViewBag.Mode = "Edit";
           
            model.ValuationResultViewModelList = ObjService.PopulateVariableBasisDetailForReport(id, model.ViewbagGrantTypeId).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = SubProgramServices.PopulateFifteenYojana(id).Take(10).ToList();
            model.ObjMakeApprovedFinalListViewModel = new MakeApprovedFinalListViewModel();
            model.ObjMakeApprovedFinalListViewModel.ApprovedAmount = ObjService.GetProgramWiseAmount(id);
            model.ViewbagGrantTypeId = id1;
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


        public ActionResult ShowProgramPhaseWiseDetails(string id)
        {
            int Phasenumber = 0;
            if (!string.IsNullOrEmpty(id))
            {
                Phasenumber = Convert.ToInt32(id);
            }
            DashboardServices services = new DashboardServices();
            DashboardModel model = new DashboardModel();
            model.ProgramPhaseNumber = Phasenumber;
            int RGAFYID = FunctionClass.GetRGAFiscalYearByPhaseNumber(Phasenumber);
            model.RGAFiscalYearId = RGAFYID;
            model.ProgramPhaseNumberStr = FunctionClass.GetPhaseTitleBYPhaseNumber(Phasenumber);
            return PartialView("__FiscalYearWiseDetails", model);

        }

        public ActionResult ListLetter()
        {
            DashboardServices services = new DashboardServices();
            LetterDetails model = new LetterDetails();
            model.LetterDetailsList = new List<LetterDetails>();
            model.LetterDetailsList = services.SP_PopulateLetterList(0);
            return View(model);
        }
        public ActionResult AddNewLetter()
        {
            LetterDetails model = new LetterDetails();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddNewLetter(LetterDetails model)
        {
            string letterFileUploadStr = model.LetterFileUploadFile == null ? string.Empty : model.LetterFileUploadFile.FileName;

            if (string.IsNullOrEmpty(letterFileUploadStr) == false)
            {
                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.UploadDoc = Path.GetFileName(PrifixLetter + "_" + model.LetterFileUploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/LetterDocs"), model.UploadDoc);
                model.LetterFileUploadFile.SaveAs(path);
            }

            DashboardServices sr = new DashboardServices();
            model.LetterDate = DateTime.Now;
            string Val = sr.SP_InsertLetter(model);
            if (Val == "Saved Successfully")
            {
                TempData["Notifications"] = @"विवरण पेश भयो । ";
                return RedirectToAction("ListLetter");
            }
            else
            {
                ViewBag.ErrorMessage = @"सिस्टममा केहि समस्या देखियो । पुनह कोशिस गर्नुहोस ।";
                return View(model);
            }

        }
        public ActionResult EditNewLetter(int id)
        {
            DashboardServices sr = new DashboardServices();
            LetterDetails model = new LetterDetails();
            model = sr.SP_PopulateLetterList(id).SingleOrDefault();
            return View(model);
        }


        [HttpPost]
        public ActionResult EditNewLetter(LetterDetails model)
        {
            string letterFileUploadStr = model.LetterFileUploadFile == null ? string.Empty : model.LetterFileUploadFile.FileName;
            if (string.IsNullOrEmpty(letterFileUploadStr) == false)
            {
                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.UploadDoc = Path.GetFileName(PrifixLetter + "_" + model.LetterFileUploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/LetterDocs"), model.UploadDoc);
                model.LetterFileUploadFile.SaveAs(path);
            }
            else if (string.IsNullOrEmpty(model.UploadDoc))
            {
                model.UploadDoc = string.Empty;
            }
            else
            {
                model.UploadDoc = model.UploadDoc;
            }
            DashboardServices sr = new DashboardServices();
            model.LetterDate = DateTime.Now;
            string Val = sr.SP_UpdateLetterDetail(model);
            if (Val == "Updated")
            {
                TempData["Notifications"] = @"विवरण परिवर्तन भयो । ";
                return RedirectToAction("ListLetter");
            }
            else
            {
                ViewBag.ErrorMessage = @"सिस्टममा केहि समस्या देखियो । पुनह कोशिस गर्नुहोस ।";
                return View(model);
            }

        }
        public ActionResult DeleteLetterDetail(int id)
        {
            DashboardServices sr = new DashboardServices();
            string val = sr.SP_DeleteLetterDetail(id);
            TempData["Notifications"] = @"विवरण सिस्टमबाट हटाईयो । ";
            return RedirectToAction("ListLetter");
        }
        public ActionResult ViewLetterDetail(int id)
        {
            DashboardServices sr = new DashboardServices();
            LetterDetails model = new LetterDetails();
            model = sr.SP_PopulateLetterList(id).SingleOrDefault();
            return View(model);
        }

        public ActionResult SearchOffice()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            return View(model);
        }

        [HttpPost]
        public PartialViewResult SearchOffice(ReportModel model)
        {
            ReportServices services = new ReportServices();

            model.OfficeDetalProvinceWiseViewModelList = new List<OfficeDetalProvinceWiseViewModel>();
            model.OfficeDetalProvinceWiseViewModelList = services.SP_GetofficeDetailProvincWise(model.ProvinceIdSearch, model.DistrictIdSearch, model.VDCMUNIdSearch);
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return PartialView("_DetailsSearchOfficeReports", model);

        }


    }



}