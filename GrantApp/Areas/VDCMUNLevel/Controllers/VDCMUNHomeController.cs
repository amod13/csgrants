using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;
using GrantApp.Services;
using GrantApp.Areas.Admin.Models;
using System.Data.Entity;
using DocumentFormat.OpenXml.EMMA;

namespace GrantApp.Areas.VDCMUNLevel.Controllers
{
    [Authorize]
    public class VDCMUNHomeController : BaseController
    {
        GrantAppDBEntities db = new GrantAppDBEntities();
        public DateTime GetSubmissionDate()
        {

            DateTime ReturnDate = GrantApp.StaticValue.ConstantValues.SubmissionDateForProduction;
            return ReturnDate;
        }
        // GET: VDCMUNLevel/VDCMUNHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int tap = CommontUtilities.ValidationRule_ProgressReport(OfficeId);
            int totalApprovedProgram = CommontUtilities.ValidationRule_TotalApprovedProgramCount(OfficeId, 1);
            OfficeDetails od = new OfficeDetails();
            CommonServices cs = new CommonServices();
            int CurrentFYID = 6;
            od = cs.GetOfficeDetails(OfficeId);
            int count = CommontUtilities.GetCurrentLoginUserMenuBarStatus();
            //int count = db.ProfileUpdates.Count(x => x.OfficeId == OfficeId && x.FiscalYearId == CurrentFYID);
            //if (count > 2)
            //{
                return View();

            //}


            //else if (count == 1)
            //{
            //    return RedirectToAction("ProgressDetailList", "SubProgramSetup", new { area = "VDCMUNLevel" });


            //}

            //else if (count == 2)
            //{
            //    return RedirectToAction("PreviousSubProgramList", "SubProgramSetup", new { id = 0 });
            //}
        //    else
        //    {
        //        return RedirectToAction("ViewProfile");

        //    }
        }


        public ActionResult ViewProfile()
        {
            
            OfficeDetails model = new OfficeDetails();
            CommonServices CS = new CommonServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model = CS.GetOfficeDetails(OfficeId);
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewProfile(OfficeDetails model)
        {
            //check valid mobile number or not


            //check if email already exist in system or not



            CommonServices CS = new CommonServices();

            if (CS.CheckIfEmailIdExist(model.AuthorizedEmail, model.OfficeId))
            {
                ViewBag.ReturnMessage = "तपाँईले राख्नुभएको ईमेल सिस्टममा पहिले देखिनै प्रयोगमा छ । कृपया भिन्नै ईमेल राख्नुहोस ।";
            }
            else
            {
                string msg = CS.UpdateOfficeDetails(model);
                if (msg == "Update Successfully")
                {
                    //ProfileUpdates pu = new ProfileUpdates();
                    ProfileUpdates pu = db.ProfileUpdates.FirstOrDefault(x => x.OfficeId == model.OfficeId && x.FiscalYearId == 6);
                    //pu = db.ProfileUpdates.FirstOrDefault(x => x.OfficeId == model.OfficeId && x.FiscalYearId == 6);
                    if (pu != null)
                    {
                        pu.UpdatedDate = DateTime.Now;
                        db.Entry(pu).State = EntityState.Modified;
                        db.SaveChanges();

                        //update
                    }
                    else
                    {


                        pu = new ProfileUpdates();
                        //save
                        pu.FiscalYearId = 7;
                        pu.OfficeId = model.OfficeId;
                        pu.UpdatedDate = DateTime.Now;
                        db.ProfileUpdates.Add(pu);
                        try
                        {
                            db.SaveChanges();
                            return RedirectToAction("ProgressDetailList", "SubProgramSetup");

                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }


                    ViewBag.ReturnMessage = "प्रयोगकर्ता कार्यालयको प्रोफाईल अध्यावधिक भयो । अब प्रगति विवरण भर्न सक्नुहुनेछ ।";
                }
                else
                {
                    ViewBag.ReturnMessage = "सिस्टममा समस्या आयो । पुनह् प्रयास गर्नुहोस ।";
                }
            }


            return View(model);
        }


        public ActionResult ViewReports()
        {
            AdminReport armodel = new AdminReport();
            DashboardServices DS = new DashboardServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            armodel.AllProgramListOfClientViewModelList = DS.AllProgramListOfClientByOfficeId(0, 0, OfficeId);
            armodel.FiscalYearID = CurrentPhaseNumber;
            return View(armodel);
        }

        public ActionResult ViewReportsSubmitwise()
        {
            AdminReport armodel = new AdminReport();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            armodel.FiscalYearID = CurrentPhaseNumber;
            armodel.OfficeId = OfficeId;
            return View(armodel);
        }
        [HttpPost]
        public ActionResult ViewReportsSubmitwise(AdminReport model)
        {
            DashboardServices DS = new DashboardServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.AllProgramListOfClientViewModelList = DS.SP_PopulateProgramSubmitStatusWise(model.GrantTypeId, model.FiscalYearID, OfficeId, model.SubmitOrNot);
            return PartialView("_SubmitedNotSubmitedList", model);
        }


        public ActionResult ReportViewDetailById(int id, int id1)
        {

            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();
            model = services.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);
            model.ObjSupportingDocumentsModel = services.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = services.PopulateProgramConditionsListForEdit(id,UserTypeId);
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = services.CheckIfAlreadyInsertedInCondictionDetails(id);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            model.IsPointInsertedIntoVarialbesTable = ObjService.CheckIfProgramValuationBasisExist(id, id1, OfficeId);
            ViewBag.Mode = "Edit";

            //prastap mulyankan
            // ProgramPointsServices ObjService = new ProgramPointsServices();

            model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(id, model.ViewbagGrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            ViewBag.Mode = "Edit";
            model.FifteenYojanaDetailsList = services.PopulateFifteenYojana(id);
            return View(model);

        }


        public ActionResult viewAllDetailByOffice(int id, int id1)
        {
            ReportModel model = new ReportModel();
            ReportServices services = new ReportServices();
            model.ProgramPhaseNumber = 0;
            model.ProvinceIdSearch = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.ViewAllDetailsOfOfficeViewModelList = services.SP_GetAllDetailsOfIndividualOffice(model.ProgramPhaseNumber, model.ProvinceIdSearch);//1 is not submited, 2 is submited
                                                                                                                                                      //model.ViewAllProgressReportDtlViewModelList = services.SP_GetALlProgressReportDltByOfficeId(model.ProgramPhaseNumber, model.ProvinceIdSearch);
            return View(model);
        }
        public ActionResult ViewDetailProgressReportForAll(int id, int id1, int id2, int id3)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices SubProgramServices = new SubProgramServices();
            model = SubProgramServices.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = SubProgramServices.PopulateProgramConditionsListForEdit(id,UserTypeId);
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

        public ActionResult ListLetter()
        {
            DashboardServices services = new DashboardServices();
            LetterDetails model = new LetterDetails();
            model.LetterDetailsList = new List<LetterDetails>();
            model.LetterDetailsList = services.SP_PopulateLetterList(0);
            return View(model);
        }

        public ActionResult ViewLetterDetail(int id)
        {
            DashboardServices sr = new DashboardServices();
            LetterDetails model = new LetterDetails();
            model = sr.SP_PopulateLetterList(id).SingleOrDefault();
            return View(model);
        }


        public ActionResult ViewProgramDetailStatusWise(int id)
        {
            AdminReport armodel = new AdminReport();
            DashboardServices DS = new DashboardServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            armodel.AllProgramListOfClientViewModelList = DS.AllProgramListOfClientByOfficeId(0, 0, OfficeId);
            armodel.FiscalYearID = CurrentPhaseNumber;
            armodel.GrantTypeId = id;
            return View(armodel);
        }

        public ActionResult GetNotCompletedProjectList()
        {


            GetNotCompletedProgramListByOfficeIdVM  model= new GetNotCompletedProgramListByOfficeIdVM();
            DashboardServices DS = new DashboardServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            model.GetNotCompletedProgramListByOfficeIdVMList = DS.SPUP_GetNotCompletedProgramListByOfficeId(OfficeId);

            return View(model);
        }



        public ActionResult GetNotCompletedProjectList2()
        {


            GetNotCompletedProgramListByOfficeIdVM model = new GetNotCompletedProgramListByOfficeIdVM();
            DashboardServices DS = new DashboardServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            model.GetNotCompletedProgramListByOfficeIdVMList = DS.SPUP_GetNotCompletedProgramListByOfficeId(OfficeId);

            return View(model);
        }

        public ActionResult ViabilityFundProject()
        {
            return View();
        }

    }
}