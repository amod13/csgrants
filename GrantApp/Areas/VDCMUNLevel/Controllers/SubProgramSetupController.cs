using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;
using GrantApp.Services;
using System.IO;
using System.Globalization;
using System.Data.Entity;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNet.Identity;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace GrantApp.Areas.VDCMUNLevel.Controllers
{
    [Authorize]
    public class SubProgramSetupController : BaseController
    {

        GrantAppDBEntities db = new GrantAppDBEntities();
        private string OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId().ToString();

        SubProgramServices services = new SubProgramServices();

        // GET: VDCMUNLevel/SubProgramSetup

        public DateTime GetSubmissionStartDate()
        {
            DateTime ReturnDate = DateTime.Now;
            int CurrentLoginUserType = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserType();
            if (CurrentLoginUserType == 2)
            {
                ReturnDate = GrantApp.StaticValue.ConstantValues.SubmissionDateForProvinceOnly;
            }
            else
            {
                ReturnDate = GrantApp.StaticValue.ConstantValues.SubmissionDateForProduction;
            }
            return ReturnDate;
        }

        public DateTime GetSubmissionDate()
        {

            DateTime ReturnDate = DateTime.Now;
            int CurrentLoginUserType = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserType();
            if (CurrentLoginUserType == 2)
            {
                ReturnDate = GrantApp.StaticValue.ConstantValues.SubmissionDateForProvinceOnly;
            }
            else
            {
                ReturnDate = GrantApp.StaticValue.ConstantValues.SubmissionDateForProduction;
            }

            return ReturnDate;
        }

        //public string GetSubmissionDateErrorMessage()
        //{
        //    string ReturnStr = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
        //    return ReturnStr;
        //}

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckValidation(int id)
        {
            OfficeDetails od = new OfficeDetails();
            od.ViewbagGrantTypeId = id;
            CommonServices cs = new CommonServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            od = cs.GetOfficeDetails(OfficeId);
            od.ViewbagGrantTypeId = id;
            if ((string.IsNullOrEmpty(od.AuthorizedEmail)) || (string.IsNullOrEmpty(od.Phone)))
            {
                return RedirectToAction("ListSubProgram", new { id = @od.ViewbagGrantTypeId });
            }
            else
            {
                return RedirectToAction("ListSubProgram", new { id = @od.ViewbagGrantTypeId });
            }
        }


        public ActionResult CheckValidationForPrevious(int id)
        {
            OfficeDetails od = new OfficeDetails();
            od.ViewbagGrantTypeId = id;
            CommonServices cs = new CommonServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            od = cs.GetOfficeDetails(OfficeId);
            od.ViewbagGrantTypeId = id;
            var lastUpdatedDatedForProfile = db.ProfileUpdates
                   .Where(p => p.OfficeId == od.OfficeId)
                   .Select(p => p.UpdatedDate)
                   .FirstOrDefault();
            od.ViewbagGrantTypeId = id;

            if (string.IsNullOrEmpty(od.AuthorizedEmail) || string.IsNullOrEmpty(od.Phone) || lastUpdatedDatedForProfile < GrantApp.StaticValue.ConstantValues.ProfileUpdateGreaterThan)
            {
                // ViewBag.ReturnMessage = "सर्वप्रथम रयोगकताको प्रोफाईल अध्यावधिक गर्नुहोस ।";
                return RedirectToAction("ViewProfile", "VDCMUNHome");
            }
            var prgressDataInput = GrantApp.CommontUtilities.CheckIfAllReportSubmittedOrNot(OfficeId);
            if (prgressDataInput==0)
            {
                return RedirectToAction("CheckValidationForProgress", new { id = 0 });
            }
            else
            {
                return RedirectToAction("PreviousSubProgramList", new { id = 0 });
            }
        }
        public ActionResult CheckValidationForProgress(int id)
        {
            OfficeDetails od = new OfficeDetails();
            od.ViewbagGrantTypeId = id;
            CommonServices cs = new CommonServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            od = cs.GetOfficeDetails(OfficeId);
            var lastUpdatedDatedForProfile = db.ProfileUpdates
                    .Where(p => p.OfficeId == od.OfficeId)
                    .Select(p => p.UpdatedDate)
                    .FirstOrDefault();
            od.ViewbagGrantTypeId = id;
         
            if (string.IsNullOrEmpty(od.AuthorizedEmail) || string.IsNullOrEmpty(od.Phone) || lastUpdatedDatedForProfile < GrantApp.StaticValue.ConstantValues.ProfileUpdateGreaterThan) 
            {
               // ViewBag.ReturnMessage = "सर्वप्रथम रयोगकताको प्रोफाईल अध्यावधिक गर्नुहोस ।";
                return RedirectToAction("ViewProfile", "VDCMUNHome");
            }
            else
            {
                return RedirectToAction("ProgressDetailList");
            }
        }



        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]//This is for removing tempdata if back button pressed
        public ActionResult ListSubProgramOld(int id)
        {
            SubProgramMaster model = new SubProgramMaster
            {
                OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId()
            };
            model.SubProgramMasterList = services.PopulateSubProgramListForClientWithOfficeId(id, 1, model.OfficeId).ToList();

            model.ViewbagGrantTypeId = id;
            model.ViewBagIfProgramNumberExceed = services.CountTotalNumberOfProjectOfficeWise(id, 6, model.OfficeId);

            //this validation is for request grant amount
            model.TotalApprovedProgramCount = CommontUtilities.TotalApprovedMultiYearProgramByGrantType(model.OfficeId, model.ViewbagGrantTypeId);
            model.TotalRequestGrantAmountCount = CommontUtilities.TotalRequestedGrantAmountByOffice(model.OfficeId, model.ViewbagGrantTypeId);
            //This validation is for progress report count
            model.TotalSubmitedProgressReportCount = CommontUtilities.TotalSubmittedProgressReportByOffice(model.OfficeId, model.ViewbagGrantTypeId);
            model.SubProgramMasterListForPreviousYear = services.PopulateSubProgramPhaseWiseList(model.ViewbagGrantTypeId, model.OfficeId, 1).ToList();


            model.TotalApprovedProgramCountForRPT = CommontUtilities.TotalApprovedMultiYearProgramByGrantTypeForRPT(model.OfficeId, id);
            model.TotalApprovedProgramCountForProgressRPT = CommontUtilities.TotalApprovedMultiYearProgramByGrantTypeForProRpt(model.OfficeId, id);
            model.TotalApprovedProgramCountForProgressRPT = model.TotalApprovedProgramCountForProgressRPT + model.TotalApprovedProgramCountForRPT;

            model.ViewbagCurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            return View(model);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]//This is for removing tempdata if back button pressed
        public ActionResult ListSubProgram(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.SubProgramMasterList = services.SP_GetSubmitedProgramListbyType(id, 2, CurrentPhaseNumber, model.OfficeId).ToList();
            model.SubProgramMasterListNotSubmited = services.SP_GetSubmitedProgramListbyType(id, 1, CurrentPhaseNumber, model.OfficeId).ToList();
                       

            model.ViewbagGrantTypeId = id;
            model.ViewBagIfProgramNumberExceed = services.CountTotalNumberOfProjectOfficeWise(id, 6, model.OfficeId);

            //this validation is for request grant amount
            model.TotalApprovedProgramCount = CommontUtilities.TotalApprovedMultiYearProgramByGrantType(model.OfficeId, model.ViewbagGrantTypeId);
            model.TotalRequestGrantAmountCount = CommontUtilities.TotalRequestedGrantAmountByOffice(model.OfficeId, model.ViewbagGrantTypeId);
            //This validation is for progress report count
            //model.TotalSubmitedProgressReportCount = CommontUtilities.TotalSubmittedProgressReportByOffice(model.OfficeId, model.ViewbagGrantTypeId);
            model.TotalSubmitedProgressReportCount = CommontUtilities.Validation_TotalSubmitdQrdTillDateByOfficeId(model.OfficeId, model.ViewbagGrantTypeId);
            model.SubProgramMasterListForPreviousYear = services.PopulateSubProgramPhaseWiseList(model.ViewbagGrantTypeId, model.OfficeId, 1).ToList();
                       
            //model.TotalApprovedProgramCountForRPT = CommontUtilities.TotalApprovedMultiYearProgramByGrantTypeForRPT(model.OfficeId, id);
            //model.TotalApprovedProgramCountForProgressRPT = CommontUtilities.TotalApprovedMultiYearProgramByGrantTypeForProRpt(model.OfficeId, id);
            //model.TotalApprovedProgramCountForProgressRPT = model.TotalApprovedProgramCountForProgressRPT + model.TotalApprovedProgramCountForRPT;

            model.TotalApprovedProgramCountForProgressRPT = CommontUtilities.ValidationRule_ProgressReport(model.OfficeId, id);
            model.ViewbagCurrentPhaseNumber = CurrentPhaseNumber;
            return View(model);
        }


        public ActionResult PreviousSubProgramList(int id)
        {
            //This is for old program list...
            //check weather program approve or not, only approved program has progress report button
            //check program approve status-1 or 0, if 1 approved else not approved.
            
            SubProgramMaster model = new SubProgramMaster();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            //Populate dynamically phase number . Currnelty static value added
            int CurrentProgramPhaseStatus = CommontUtilities.GetCurrentProgramPhaseNumber();
            //Get only previous program list status not equal to running project
            // model.SubProgramMasterList = services.PopulateSubProgram(0).Where(x => x.OfficeId == model.OfficeId && x.PhaseStatus!=CurrentProgramPhaseStatus).ToList();
            //model.SubProgramMasterList = services.PopulateSubProgram(0).Where(x => x.OfficeId == model.OfficeId && x.PhaseStatus == 1).ToList();
            model.SubProgramMasterList = services.PopulateSubProgramPhaseWiseList(model.ViewbagGrantTypeId, model.OfficeId, 1).ToList();
            model.RequestGrantAmountViewModelList = new List<RequestGrantAmountViewModel>();
            model.RequestGrantAmountViewModelList = services.PopulateRequestGrantAmountOfficeWise(0, 1, model.OfficeId);
            model.CanceledProgramListForRGVMList = services.SPUP_GetCanceledProgramListForClient(1, model.OfficeId);
            return View(model);
        }

        public ActionResult Create(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.ViewbagGrantTypeId = id;
            model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
            model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id });
            }
            else
            {
                return View(model);
            }


        }

        public ActionResult LodgeNewSamapurak(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.ViewbagGrantTypeId = id;
            model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
            model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id });
            }
            else
            {
                return View(model);
            }


        }




        public JsonResult ValidateBudget(decimal TotalBudget)
        {
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            int officeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro

            if (officeType == 1 || officeType == 4)
            {
                if (TotalBudget >= 100 && TotalBudget <= 700)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json("कुल लागत १०० देखि ७०० सम्म राख्नुहोला ।", JsonRequestBehavior.AllowGet);
            }
            else if (officeType == 2 || officeType == 3)
            {
                if (TotalBudget >= 300 && TotalBudget <= 1500)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json("कुल लागत ३०० देखि १५०० सम्म राख्नुहोला ।", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (TotalBudget >= 1000 && TotalBudget <= 3000)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json("कुल लागत १००० देखि ३००० सम्म राख्नुहोला ।", JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult CreateSpecial(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.ViewbagGrantTypeId = id;
            model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
            model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            var SubmissionDate = GetSubmissionDate();
            //var SubmissionDate = DateTime.Today.AddDays(365);

            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id });
            }
            else
            {
                return View(model);
            }


        }

        [HttpPost]
        public ActionResult Create(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            //check if program already inserted or not.....
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int MaxAnsiCode = 255;
            bool checkUnicode = model.SubProgramTitle.Any(c => c > MaxAnsiCode);
            if (!checkUnicode)
            {
                ViewBag.ErrorMessage = "कृपया आयोजना/कार्यक्रमको नाम नेपालि युनिकोडमा लेख्नुहोस ।";
                ViewBag.ErrMode = "On";
                return View(model);
            }
            model.TotalBudget = model.TotalBudget * 100000;
            if (model.BudgetForFirstYear.HasValue)

            {
                model.BudgetForFirstYear = model.BudgetForFirstYear * 100000;
            }
            else
            {
                model.BudgetForFirstYear = 0m;
            }

            if (model.BudgetForSecondYear.HasValue)
            {
                model.BudgetForSecondYear = model.BudgetForSecondYear * 100000;
            }
            if (model.BudgetForThirdYear.HasValue)
            {
                model.BudgetForThirdYear = model.BudgetForThirdYear * 100000;
            }
            if (model.AmountProvinceVdc.HasValue)
            {
                model.AmountProvinceVdc = model.AmountProvinceVdc * 100000;
            }
            if (model.NGOINGOAmount.HasValue)
            {
                model.NGOINGOAmount = model.NGOINGOAmount * 100000;
            }
            if (model.ViewbagGrantTypeId == 1)
            {


                if (services.CheckTotalYearlyValueMatchOrNot(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear) == false)
                {
                    ViewBag.ErrorMessage = "पहिलो, दोस्रो र तेस्रो लागतको रकम कुल लागत रकम संग मिलेन ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }
            }

            else
            {


                if (services.CheckTotalYearlyValueMatchOrNotForComplementry(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear, model.AmountProvinceVdc, model.NGOINGOAmount) == false)
                {
                    ViewBag.ErrorMessage = "पहिलो, दोस्रो र तेस्रो लागतको रकम कुल लागत रकम संग मिलेन ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }

            }


            if (services.CheckProgramNameAlreadyInserted(model.ProgramId, model.ViewbagGrantTypeId, model.OfficeId, model.SubProgramTitle) > 0)
            {
                ViewBag.ErrorMessage = "यो आयोजना/कार्यक्रम पहिले नै सिस्टममा छ ।";
                ViewBag.ErrMode = "On";
                return View(model);
            }

            //check total value year1 year2 year3=total amount

            //check total amount validation....For gapanapa 1-10 crore, upmanapa-5-25 crore....pradesh 20-1 arba 1000000000

            //check total amount validation....For gapanapa 1-10 crore, upmanapa-5-25 crore....pradesh 10-30 karod
            if (model.ViewbagGrantTypeId == 1)
            {
                if (model.ViewBagCurrentLoginUserUserTypeId == 2)
                {
                    if (model.TotalBudget < 100000000 && model.TotalBudget > 300000000)
                    {
                        //ViewBag.ErrorMessage = "कुल लागत १० करोड वा सो भन्दा धेरै हुनु पर्दछ ।";
                        ViewBag.ErrorMessage = "कुल लागत कम्तिमा १० करोड वा बढीमा रू ३० करोड हुनु पर्दछ ।";
                        return View(model);
                    }
                }
                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा
                    {
                        if (model.TotalBudget > 50000000 || model.TotalBudget < 5000000)//गापा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा 5 करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)//nagarpalika
                    {
                        if (model.TotalBudget > 100000000 || model.TotalBudget < 5000000)//नपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा १० करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }

                    }
                    else//upmanapa ra mahanagarpaliak
                    {
                        if (model.TotalBudget > 150000000 || model.TotalBudget < 5000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा १५ करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }
                    }
                }

            }
            else
            {


                decimal FiftyPercentageBugdet = model.TotalBudget / 2;
                if (model.AmountProvinceVdc < FiftyPercentageBugdet)
                {
                    ViewBag.ErrorMessage = "प्रदेश वा स्थानिय तहले व्यहोर्ने रकम कुल लागत को ५०% वा सो भन्दा धेरै हुनु पर्दछ ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }



                if (model.ViewBagCurrentLoginUserUserTypeId == 2)
                {
                    if (model.TotalBudget > 1000000000 || model.TotalBudget < 200000000)
                    {
                        ViewBag.ErrorMessage = "कुल लागत २० करोड देखि १ अर्ब सम्म हुनु पर्दछ ।";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }
                }

                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा नपा
                    {
                        if (model.TotalBudget > 100000000 || model.TotalBudget < 10000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि १० करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)//गापा नपा
                    {
                        if (model.TotalBudget > 100000000 || model.TotalBudget < 10000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि १० करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }

                    else
                    {
                        if (model.TotalBudget > 250000000 || model.TotalBudget < 50000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत ५ करोड देखि २५ करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }
                    }
                }


            }

            model.GrantTypeId = model.ViewbagGrantTypeId;
            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;

            string PrifixLetter = model.OfficeId + "-" + model.ProgramId;

            model.GovnNepalAmount = Convert.ToDecimal(0);
            int Primarid = services.InsertSubProgram(model);
            //int Primarid = 0;
            if (Primarid > 0)
            {
                TempData["Notifications"] = "डाटा सुरक्छित भयो ।";
            }
            else
            {
                TempData["Notifications"] = "सिस्टममा केही समस्या देखियो । पुनह प्रयास गर्नुहोस ।";
            }


            return RedirectToAction("ReqDocUploads", new { @id = Primarid, @id1 = model.ViewbagGrantTypeId });

            //return RedirectToAction("ListSubProgram", new { @id = model.ViewbagGrantTypeId });
            //return RedirectToAction("CheckListAction", new { @id = Primarid, @id1 = model.ViewbagGrantTypeId });
        }

        [HttpPost]
        public ActionResult CreateSpecial(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            //check if program already inserted or not.....
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int MaxAnsiCode = 255;
            bool checkUnicode = model.SubProgramTitle.Any(c => c > MaxAnsiCode);
            if (!checkUnicode)
            {
                ViewBag.ErrorMessage = "कृपया आयोजना/कार्यक्रमको नाम नेपालि युनिकोडमा लेख्नुहोस ।";
                ViewBag.ErrMode = "On";
                return View(model);
            }
            model.TotalBudget = model.TotalBudget * 100000;
            if (model.BudgetForFirstYear.HasValue)

            {
                model.BudgetForFirstYear = model.BudgetForFirstYear * 100000;
            }
            else
            {
                model.BudgetForFirstYear = 0m;
            }

            if (model.BudgetForSecondYear.HasValue)
            {
                model.BudgetForSecondYear = model.BudgetForSecondYear * 100000;
            }
            if (model.BudgetForThirdYear.HasValue)
            {
                model.BudgetForThirdYear = model.BudgetForThirdYear * 100000;
            }
            if (model.AmountProvinceVdc.HasValue)
            {
                model.AmountProvinceVdc = model.AmountProvinceVdc * 100000;
            }
            if (model.NGOINGOAmount.HasValue)
            {
                model.NGOINGOAmount = model.NGOINGOAmount * 100000;
            }
            if (model.PujiFirstYearAmount.HasValue)
            {
                model.PujiFirstYearAmount = model.PujiFirstYearAmount * 100000;
            }
            if (model.PujiSecondYearAmount.HasValue)
            {
                model.PujiSecondYearAmount = model.PujiSecondYearAmount * 100000;
            }

            if (model.PujiThirdYearAmount.HasValue)
            {
                model.PujiThirdYearAmount = model.PujiThirdYearAmount * 100000;
            }


            if (model.ViewbagGrantTypeId == 1)
            {
                if (services.CheckTotalYearlyValueMatchOrNot(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear, model.PujiFirstYearAmount, model.PujiSecondYearAmount, model.PujiThirdYearAmount) == false)
                {
                    string MessageStr = @"पहिलो, दोस्रो र तेस्रो लागतको रकम कुल लागत रकम संग मिलेन ।";
                    if (model.TimeDurationYear == 1)
                    {
                        MessageStr = @"प्रथम वर्षको माग रकम (चालु ‌+ पुँजीगत) को रकम कुल लागत रकम संग मिलेन ।";
                    }
                    else if (model.TimeDurationYear == 2)
                    {
                        MessageStr = @"प्रथम र दोस्रो वर्षको माग रकम (चालु ‌+ पुँजीगत) को रकम कुल लागत रकम संग मिलेन ।";
                    }
                    else
                    {
                        MessageStr = @"प्रथम,दोस्रो र तेस्रो वर्षको माग रकम (चालु ‌+ पुँजीगत) को रकम कुल लागत रकम संग मिलेन ।";
                    }
                    ViewBag.ErrorMessage = MessageStr;
                    ViewBag.ErrMode = "On";
                    return View(model);
                }
            }

            else
            {
                if (services.CheckTotalYearlyValueMatchOrNotForComplementry(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear, model.AmountProvinceVdc, model.NGOINGOAmount) == false)
                {
                    ViewBag.ErrorMessage = "पहिलो, दोस्रो र तेस्रो लागतको रकम कुल लागत रकम संग मिलेन ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }

            }


            if (services.CheckProgramNameAlreadyInserted(model.ProgramId, model.ViewbagGrantTypeId, model.OfficeId, model.SubProgramTitle) > 0)
            {
                ViewBag.ErrorMessage = "यो आयोजना/कार्यक्रम पहिले नै सिस्टममा छ ।";
                ViewBag.ErrMode = "On";
                return View(model);
            }

            //check total value year1 year2 year3=total amount

            //check total amount validation....For gapanapa 1-10 crore, upmanapa-5-25 crore....pradesh 20-1 arba 1000000000

            //check total amount validation....For gapanapa 1-10 crore, upmanapa-5-25 crore....pradesh 10-30 karod
            if (model.ViewbagGrantTypeId == 1)
            {
                if (model.ViewBagCurrentLoginUserUserTypeId == 2)
                {
                    if (model.TotalBudget < 100000000 && model.TotalBudget > 300000000)
                    {
                        //ViewBag.ErrorMessage = "कुल लागत १० करोड वा सो भन्दा धेरै हुनु पर्दछ ।";
                        ViewBag.ErrorMessage = "कुल लागत कम्तिमा १० करोड वा बढीमा रू ३० करोड हुनु पर्दछ ।";
                        return View(model);
                    }
                }
                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा
                    {
                        if (model.TotalBudget > 50000000 || model.TotalBudget < 5000000)//गापा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा 5 करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)//nagarpalika
                    {
                        if (model.TotalBudget > 100000000 || model.TotalBudget < 5000000)//नपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा १० करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }

                    }
                    else//upmanapa ra mahanagarpaliak
                    {
                        if (model.TotalBudget > 150000000 || model.TotalBudget < 5000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा १५ करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }
                    }
                }

            }
            else
            {


                decimal FiftyPercentageBugdet = model.TotalBudget / 2;
                if (model.AmountProvinceVdc < FiftyPercentageBugdet)
                {
                    ViewBag.ErrorMessage = "प्रदेश वा स्थानिय तहले व्यहोर्ने रकम कुल लागत को ५०% वा सो भन्दा धेरै हुनु पर्दछ ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }



                if (model.ViewBagCurrentLoginUserUserTypeId == 2)
                {
                    if (model.TotalBudget > 1000000000 || model.TotalBudget < 200000000)
                    {
                        ViewBag.ErrorMessage = "कुल लागत २० करोड देखि १ अर्ब सम्म हुनु पर्दछ ।";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }
                }

                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा नपा
                    {
                        if (model.TotalBudget > 100000000 || model.TotalBudget < 10000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि १० करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)//गापा नपा
                    {
                        if (model.TotalBudget > 100000000 || model.TotalBudget < 10000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि १० करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }

                    else
                    {
                        if (model.TotalBudget > 250000000 || model.TotalBudget < 50000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत ५ करोड देखि २५ करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }
                    }
                }


            }

            model.GrantTypeId = model.ViewbagGrantTypeId;
            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;

            string PrifixLetter = model.OfficeId + "-" + model.ProgramId;

            model.GovnNepalAmount = Convert.ToDecimal(0);
            int Primarid = services.InsertSubProgram(model);
            //int Primarid = 0;
            if (Primarid > 0)
            {
                TempData["Notifications"] = "डाटा सुरक्छित भयो ।";
            }
            else
            {
                TempData["Notifications"] = "सिस्टममा केही समस्या देखियो । पुनह प्रयास गर्नुहोस ।";
            }


            //return RedirectToAction("ListSubProgram", new { @id = model.ViewbagGrantTypeId });
            //return RedirectToAction("CheckListAction", new { @id = Primarid, @id1 = model.ViewbagGrantTypeId });
            return RedirectToAction("ReqDocUploads", new { @id = Primarid, @id1 = model.ViewbagGrantTypeId });

        }



        public ActionResult Edit(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = services.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            model.ViewbagGrantTypeId = id1;
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
            model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            ViewBag.Mode = "Edit";


            model.TotalBudget = model.TotalBudget / 100000;
            if (model.BudgetForFirstYear.HasValue)
            {
                model.BudgetForFirstYear = model.BudgetForFirstYear / 100000;
            }
            if (model.BudgetForSecondYear.HasValue)
            {
                model.BudgetForSecondYear = model.BudgetForSecondYear / 100000;
            }
            if (model.BudgetForThirdYear.HasValue)
            {
                model.BudgetForThirdYear = model.BudgetForThirdYear / 100000;
            }

            if (model.AmountProvinceVdc.HasValue)
            {
                model.AmountProvinceVdc = model.AmountProvinceVdc / 100000;
            }
            if (model.NGOINGOAmount.HasValue)
            {
                model.NGOINGOAmount = model.NGOINGOAmount / 100000;
            }
            if (model.PujiFirstYearAmount.HasValue)
            {
                model.PujiFirstYearAmount = model.PujiFirstYearAmount / 100000;
            }
            if (model.PujiSecondYearAmount.HasValue)
            {
                model.PujiSecondYearAmount = model.PujiSecondYearAmount / 100000;
            }
            if (model.PujiThirdYearAmount.HasValue)
            {
                model.PujiThirdYearAmount = model.PujiThirdYearAmount / 100000;
            }


            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate >= SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id1 });
            }
            else
            {
                return View(model);
            }

        }


        [HttpPost]
        public ActionResult Edit(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {


            int MaxAnsiCode = 255;
            bool checkUnicode = model.SubProgramTitle.Any(c => c > MaxAnsiCode);


            if (!checkUnicode)
            {
                ViewBag.ErrorMessage = "कृपया आयोजना/कार्यक्रमको नाम नेपालि युनिकोडमा लेख्नुहोस ।";
                ViewBag.ErrMode = "On";
                return View(model);
            }

            //if (model.ViewbagGrantTypeId == 1)
            //{
            //    if (services.CheckTotalYearlyValueMatchOrNot(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear) == false)
            //    {
            //        ViewBag.ErrorMessage = "पहिलो, दोस्रो र तेस्रो लागतको रकम कुल लागत रकम संग मिलेन ।";
            //        ViewBag.ErrMode = "On";
            //        return View(model);
            //    }
            //}

            model.TotalBudget = model.TotalBudget * 100000;
            if (model.BudgetForFirstYear.HasValue)

            {
                model.BudgetForFirstYear = model.BudgetForFirstYear * 100000;
            }
            else
            {
                model.BudgetForFirstYear = 0m;
            }

            if (model.BudgetForSecondYear.HasValue)
            {
                model.BudgetForSecondYear = model.BudgetForSecondYear * 100000;
            }
            if (model.BudgetForThirdYear.HasValue)
            {
                model.BudgetForThirdYear = model.BudgetForThirdYear * 100000;
            }
            if (model.AmountProvinceVdc.HasValue)
            {
                model.AmountProvinceVdc = model.AmountProvinceVdc * 100000;
            }
            if (model.NGOINGOAmount.HasValue)
            {
                model.NGOINGOAmount = model.NGOINGOAmount * 100000;
            }

            if (model.PujiFirstYearAmount.HasValue)
            {
                model.PujiFirstYearAmount = model.PujiFirstYearAmount * 100000;
            }
            if (model.PujiSecondYearAmount.HasValue)
            {
                model.PujiSecondYearAmount = model.PujiSecondYearAmount * 100000;
            }

            if (model.PujiThirdYearAmount.HasValue)
            {
                model.PujiThirdYearAmount = model.PujiThirdYearAmount * 100000;
            }

            if (model.ViewbagGrantTypeId == 1)
            {
                if (services.CheckTotalYearlyValueMatchOrNot(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear, model.PujiFirstYearAmount, model.PujiSecondYearAmount, model.PujiThirdYearAmount) == false)
                {
                    ViewBag.ErrorMessage = "पहिलो, दोस्रो र तेस्रो लागतको रकम कुल लागत रकम संग मिलेन ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }
            }

            else
            {
                if (services.CheckTotalYearlyValueMatchOrNotForComplementry(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear, model.AmountProvinceVdc, model.NGOINGOAmount) == false)
                {
                    ViewBag.ErrorMessage = "पहिलो, दोस्रो र तेस्रो लागतको रकम कुल लागत रकम संग मिलेन ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }

            }

            if (model.ViewbagGrantTypeId == 1)
            {
                if (model.ViewBagCurrentLoginUserUserTypeId == 2)
                {
                    if (model.TotalBudget < 100000000 || model.TotalBudget > 300000000)
                    {
                        ViewBag.ErrorMessage = "कुल लागत कम्तिमा १० करोड र बढीमा ३० करोड सम्मको हुनु पर्दछ ।";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }
                }
                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा नपा
                    {
                        if (model.TotalBudget > 50000000 || model.TotalBudget < 5000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा ५ करोड सम्म हुनु पर्दछ ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)
                    {
                        if (model.TotalBudget > 100000000 || model.TotalBudget < 5000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा १० करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }
                    }
                    else
                    {
                        if (model.TotalBudget > 150000000 || model.TotalBudget < 5000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५० लाख र बढीमा १५ करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }
                    }
                }

            }
            else
            {
                //if(model.AmountProvinceVdc>=model.TotalBudget/2)
                //{
                //    ViewBag.ErrorMessage = "कुल लागत को ५०% वा भन्दा धेरै हुनु पर्दछ ।";
                //    return View(model);
                //}
                decimal FiftyPercentageBugdet = model.TotalBudget / 2;
                if (model.AmountProvinceVdc < FiftyPercentageBugdet)
                {
                    ViewBag.ErrorMessage = "प्रदेश वा स्थानिय तहले व्यहोर्ने रकम कुल लागत को ५०% वा सो भन्दा धेरै हुनु पर्दछ ।";
                    return View(model);
                }

                if (model.ViewBagCurrentLoginUserUserTypeId == 2)
                {
                    if (model.TotalBudget > 1000000000 && model.TotalBudget < 200000000)
                    {
                        ViewBag.ErrorMessage = "कुल लागत २० करोड देखि १ अर्ब सम्म हुनु पर्दछ ।";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }
                }
                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा नपा
                    {
                        if (model.TotalBudget > 100000000 && model.TotalBudget < 10000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि १० करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)//गापा नपा
                    {
                        if (model.TotalBudget > 100000000 && model.TotalBudget < 10000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि १० करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }
                    else
                    {
                        if (model.TotalBudget > 250000000 && model.TotalBudget < 50000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत ५ करोड देखि २५ करोड सम्म हुनु पर्दछ  ।";
                            return View(model);
                        }
                    }
                }

            }




            model.GrantTypeId = model.ViewbagGrantTypeId;
            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            string PrifixLetter = model.OfficeId + "-" + model.ProgramId;

            model.GovnNepalAmount = Convert.ToDecimal(0);

            //Check Validation....... or comment update part
            string ReturnMessage = services.UpdateSubProgram(model);


            return RedirectToAction("ListSubProgram", new { @id = model.ViewbagGrantTypeId });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]//This is for removing tempdata if back button pressed
        public ActionResult CheckListAction(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();
            var SubmissionDate = GetSubmissionDate();
            int CurrentLoginUserType = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserType();

            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id1 });
            }
            else
            {

                if (services.CheckIfAlreadyInsertedInCondictionDetails(id))
                {
                    model.ProgramConditionsViewModelList = services.PopulateProgramConditionsListForEdit(id, CurrentLoginUserType);

                }
                else
                {

                    model.ProgramConditionsViewModelList = services.PopulateProgramConditionsList(id1, CurrentLoginUserType);
                }

                ViewBag.Mode = "Edit";
                model.ViewbagGrantTypeId = id1;
                model.ViewBagCurrentOfficeType = CurrentLoginUserType;

                model.SubProgramId = id;
                model.Status = services.GetCurrentStatusOfSubProgram(model.SubProgramId);

                //model.Status = 2;//static variable....under process
                model.TermsAndCondtions = true;
                return View(model);
            }
        }



        public ActionResult ReqDocUploads(int id, int id1)
        {
            GrantAppDBEntities db = new GrantAppDBEntities();
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id1 });
            }
            else
            {

                if (db.DocumentRequirementsUpload.Where(x => x.SubprogramId == id).Count() > 0)

                {
                    model.DocumentsRequirementsViewModel = services.SPUP_PopulateRequiredDocForEdit(id, CurrentLoginUserType);

                }
                else
                {
                    model.DocumentsRequirementsViewModel = services.SPUP_PopulateDocRequirementList(id1, CurrentLoginUserType);
                }

                ViewBag.Mode = "Edit";
                model.ViewbagGrantTypeId = id1;
                model.SubProgramId = id;
                model.Status = services.GetCurrentStatusOfSubProgram(model.SubProgramId);

                model.TermsAndCondtions = true;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ReqDocUploads(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            SubProgramServices services = new SubProgramServices();
            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            string PrifixLetter = model.OfficeId + "-" + model.ProgramId;

            //check condition already same file exist in system and size of file...
            //bool CheckfileNameAndSize = false;
            var myList = new List<string>();
            var duplicates = new List<string>();
            int FileContentLength = 0;
            foreach (var item1 in model.DocumentsRequirementsViewModel)
            {
                //check for required file....
                int reqdocfileId = item1.RequiredDocID;



                string FileNameVal = item1.SupportingDocFiles == null ? string.Empty : item1.SupportingDocFiles.FileName;
                if (string.IsNullOrEmpty(FileNameVal) == false)
                {
                    if (!myList.Contains(FileNameVal))
                    {
                        myList.Add(FileNameVal);
                        FileContentLength = item1.SupportingDocFiles.ContentLength;
                    }

                    else
                    {
                        duplicates.Add(FileNameVal);
                        FileContentLength = item1.SupportingDocFiles.ContentLength;
                    }

                }



                int totalCountdup = duplicates.Count;
                int totalcountnondup = myList.Count;


                if (FileContentLength > 25000000)//file size more than 1mb
                {
                    TempData["Notifications"] = "अपलोड गरिएका फाईल(हरु) को साईज 25 MB भन्दा धेरै भयो ।";
                    return RedirectToAction("ReqDocUploads", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
                }

                if (totalCountdup > 0)
                {
                    TempData["Notifications"] = "अपलोड गरिएका फाईलहरु बेग्ला बेग्लै हुनुपर्दछ । एउटै फाईल एक भन्दा बढि शिर्षकमा राख्न मिल्दैन ।";
                    return RedirectToAction("ReqDocUploads", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
                }
            }



            //Comment this....
            services.SP_UPDeleteRequiredDocumentsUpload(model.SubProgramId);
            //var duplicateExists = model.ProgramConditionsViewModelList.GroupBy(n => n.SupportingDocFiles.FileName).Any(g => g.Count() > 1);

            foreach (var item in model.DocumentsRequirementsViewModel)
            {

                string FileNameVal = item.SupportingDocFiles == null ? string.Empty : item.SupportingDocFiles.FileName;
                if (string.IsNullOrEmpty(FileNameVal) == false)
                {
                    model.ObjDocumentsRequirementsViewModel.UploadFileUrl = Path.GetFileName(PrifixLetter + "_" + item.SupportingDocFiles.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjDocumentsRequirementsViewModel.UploadFileUrl);
                    item.SupportingDocFiles.SaveAs(path);
                }
                else
                {
                    if (string.IsNullOrEmpty(item.UploadFileUrl))
                    {
                        model.ObjDocumentsRequirementsViewModel.UploadFileUrl = string.Empty;
                    }
                    else
                    {
                        model.ObjDocumentsRequirementsViewModel.UploadFileUrl = item.UploadFileUrl;
                    }
                }


                services.SPUP_InsertProgramConditionDetails(item.RequiredDocID, model.SubProgramId, model.ObjDocumentsRequirementsViewModel.UploadFileUrl, 6);
                //only terms And Condition make True.....don't upload File.......
                string FinalFileNameStr = services.GetFinalFileNameBySubProgramID(model.SubProgramId);
                if (FinalFileNameStr == "Empty")
                {
                    model.FinalDocumentsUrl = string.Empty;
                }
                else
                {
                    model.FinalDocumentsUrl = FinalFileNameStr;
                }

                if (services.UpdateSubProgramStatusSP(model.SubProgramId, model.ProgramId, model.FinalDocumentsUrl, true, 1) == "Updated Successfully")
                {
                    TempData["RequestGrantNotifications"] = "तपाँईको विवरण पेश भयो ।";
                }
                else
                {
                    TempData["RequestGrantNotifications"] = "सिस्टममा केही समस्या देखियो । पुनह प्रयास गर्नुहोस ।";
                }

            }


            model.ViewbagGrantTypeId = model.ViewbagGrantTypeId;
            //return RedirectToAction("PrintPreview", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
            return RedirectToAction("CheckListAction", "SubProgramSetup", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
        }
        [HttpPost]
        public ActionResult CheckListAction(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            SubProgramServices services = new SubProgramServices();
            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            string PrifixLetter = model.OfficeId + "-" + model.ProgramId;

            //check condition already same file exist in system and size of file...
            //bool CheckfileNameAndSize = false;
            var myList = new List<string>();
            var duplicates = new List<string>();
            int FileContentLength = 0;
            foreach (var item1 in model.ProgramConditionsViewModelList)
            {
                //check for required file....
                int ProgramCondId = item1.ProgramConditionID;
                bool UploadFileCondtion = false;

                if (item1.IsUploadFile == true)
                {


                    string FileNameVal = item1.SupportingDocFiles == null ? string.Empty : item1.SupportingDocFiles.FileName;
                    if (string.IsNullOrEmpty(FileNameVal) == false)
                    {
                        if (!myList.Contains(FileNameVal))
                        {
                            myList.Add(FileNameVal);
                            FileContentLength = item1.SupportingDocFiles.ContentLength;
                        }

                        else
                        {
                            duplicates.Add(FileNameVal);
                            FileContentLength = item1.SupportingDocFiles.ContentLength;
                        }

                    }

                }

                int totalCountdup = duplicates.Count;
                int totalcountnondup = myList.Count;


                if (FileContentLength > 25000000)//file size more than 1mb
                {
                    TempData["Notifications"] = "अपलोड गरिएका फाईल(हरु) को साईज 25 MB भन्दा धेरै भयो ।";
                    return RedirectToAction("CheckListAction", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
                }

                if (totalCountdup > 0)
                {
                    TempData["Notifications"] = "अपलोड गरिएका फाईलहरु बेग्ला बेग्लै हुनुपर्दछ । एउटै फाईल एक भन्दा बढि शिर्षकमा राख्न मिल्दैन ।";
                    return RedirectToAction("CheckListAction", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
                }
            }



            //Comment this....
            services.DeleteProgramConditionDetails(model.SubProgramId);

            //var duplicateExists = model.ProgramConditionsViewModelList.GroupBy(n => n.SupportingDocFiles.FileName).Any(g => g.Count() > 1);

            foreach (var item in model.ProgramConditionsViewModelList)
            {

                if (item.IsUploadFile == true)
                {

                    string FileNameVal = item.SupportingDocFiles == null ? string.Empty : item.SupportingDocFiles.FileName;
                    if (string.IsNullOrEmpty(FileNameVal) == false)
                    {
                        model.ObjProgramConditionsViewModel.UploadFileUrl = Path.GetFileName(PrifixLetter + "_" + item.SupportingDocFiles.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjProgramConditionsViewModel.UploadFileUrl);
                        item.SupportingDocFiles.SaveAs(path);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(item.UploadFileUrl))
                        {
                            model.ObjProgramConditionsViewModel.UploadFileUrl = string.Empty;
                        }
                        else
                        {
                            model.ObjProgramConditionsViewModel.UploadFileUrl = item.UploadFileUrl;
                        }
                    }

                    //HttpPostedFileBase file = filesCollection[i];

                }
                else
                {
                    model.ObjProgramConditionsViewModel.UploadFileUrl = string.Empty;
                }

                services.InsertProgramConditionDetailList(item.ProgramConditionID, model.SubProgramId, item.IsCheck, model.ObjProgramConditionsViewModel.UploadFileUrl);
                //only terms And Condition make True.....don't upload File.......
                string FinalFileNameStr = services.GetFinalFileNameBySubProgramID(model.SubProgramId);
                if (FinalFileNameStr == "Empty")
                {
                    model.FinalDocumentsUrl = string.Empty;
                }
                else
                {
                    model.FinalDocumentsUrl = FinalFileNameStr;
                }

                if (services.UpdateSubProgramStatusSP(model.SubProgramId, model.ProgramId, model.FinalDocumentsUrl, true, 1) == "Updated Successfully")
                {
                    TempData["RequestGrantNotifications"] = "तपाँईको विवरण पेश भयो ।";
                }
                else
                {
                    TempData["RequestGrantNotifications"] = "सिस्टममा केही समस्या देखियो । पुनह प्रयास गर्नुहोस ।";
                }

            }


            model.ViewbagGrantTypeId = model.ViewbagGrantTypeId;
            //return RedirectToAction("PrintPreview", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
            return RedirectToAction("CheckListActionUpload", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
        }


        public ActionResult CheckListActionUpload(int id, int id1)
        {
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id1 });
            }
            else
            {

                SubProgramMaster model = new SubProgramMaster();
                model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
                model.ViewbagGrantTypeId = id1;
                ViewBag.Mode = "Edit";
                int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

                model.ProgramConditionsViewModelList = services.PopulateProgramConditionsListForEdit(id,UserTypeId);
                string FinalFileNameStr = services.GetFinalFileNameBySubProgramID(model.SubProgramId);

                if (FinalFileNameStr == "Empty")
                {
                    model.TermsAndCondtions = false;
                }
                else
                {
                    model.TermsAndCondtions = true;
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult CheckListActionUpload(SubProgramMaster model)
        {
            string FileNameVal = model.ConditionUploadFile == null ? string.Empty : model.ConditionUploadFile.FileName;
            bool IfCheckListInserted = services.CheckIfAlreadyInsertedInCondictionDetails(model.SubProgramId);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            bool IfPointsInserted = ObjService.CheckIfProgramValuationBasisExist(model.SubProgramId, model.ViewbagGrantTypeId, OfficeId);

            if (string.IsNullOrEmpty(FileNameVal) == false)
            {
                model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                string PrifixLetter = model.OfficeId + "-" + model.ProgramId;
                model.FinalDocumentsUrl = Path.GetFileName(PrifixLetter + "_" + model.ConditionUploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.FinalDocumentsUrl);
                model.ConditionUploadFile.SaveAs(path);
            }

            else
            {
                model.FinalDocumentsUrl = string.Empty;
            }


            if (services.UpdateSubProgramStatusSP(model.SubProgramId, model.ProgramId, model.FinalDocumentsUrl, true, 1) == "Updated Successfully")
            {
                TempData["RequestGrantNotifications"] = "तपाँईको विवरण पेश भयो  ।";
            }
            else
            {
                TempData["RequestGrantNotifications"] = "सिस्टममा केही समस्या देखियो । पुनह प्रयास गर्नुहोस ।";
            }


            //return RedirectToAction("ViewDetails", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
            return RedirectToAction("ProposalValuation", "GrantRequestForm", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
        }

        public ActionResult PrintPreview(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            return View(model);
        }

        public ActionResult FifteenYojana(int id, int id1)
        {
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id1 });
            }
            else
            {


                SubProgramMaster model = new SubProgramMaster();
                model.ViewbagGrantTypeId = id1;
                model.SubProgramId = id;
                int RefGrantVariable = 11;
                if (model.ViewbagGrantTypeId == 1)
                {
                    RefGrantVariable = 20;
                }


                if (services.IsAlreadyInsertedInFifteenYojanaDetails(id))
                {
                    model.FifteenYojanaDetailsList = services.PopulateFifteenYojana(id);
                    ViewBag.Mode = "Edit";
                }

                else
                {
                    model.FifteenYojanaDetailsList = services.PopulateFifteenYojanaTitle(model.SubProgramId, RefGrantVariable);
                    //for (int i = 1; i < 11; i++)
                    //{
                    //    model.FifteenYojanaDetailsList.Add(new FifteenYojanaDetails { OptionValue = i, IsChecked = false, SubProgramId = model.SubProgramId, RefGrantVariableId = RefGrantVariable });
                    //}

                }

                model.Status = services.GetCurrentStatusOfSubProgram(model.SubProgramId);
                model.ObjFifteenYojanaDetails.RefGrantVariableId = RefGrantVariable;
                ViewBag.Mode = "Edit";
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult FifteenYojana(SubProgramMaster model)
        {

            string msg = services.InsertFifteenYojanaDetails(model);
            TempData["RequestGrantNotifications"] = "डाटा सुरक्छित भयो ।";
            return RedirectToAction("ProgramPriority", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });

        }

        public ActionResult ViewDetails(int id, int id1)//id sub program id, id1 grant type id
        {
            SubProgramMaster model = new SubProgramMaster();
            //model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model = services.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);
            if(!string.IsNullOrEmpty(model.FinalDocumentsUrl))
            {
                model.TermsAndCondtions = true;
            }
            model.ObjSupportingDocumentsModel = services.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            int UserTypeId = GrantApp.Areas.Admin.FunctionClass.GetUsertypeFromOfficeId(model.OfficeId);

            model.ProgramConditionsViewModelList = services.PopulateProgramConditionsListForEdit(id,UserTypeId);
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            int currentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(OfficeId);
            try
            {
                model.DocumentsRequirementsViewModel = services.SPUP_PopulateRequiredDocForEdit(id, currentLoginUserType);
            }
            catch (Exception)
            {

                model.DocumentsRequirementsViewModel = new List<DocumentsRequirementsViewModel>();
            }
            model.ViewbagGrantTypeId = id1;
            bool IfCheckListInserted = services.CheckIfAlreadyInsertedInCondictionDetails(id);
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

        [HttpPost]
        public ActionResult RequestGrant(SubProgramMaster model)
        {
           

            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            model.ViewBagIfProgramNumberExceed =
                services.CountTotalNumberOfProjectOfficeWise(model.ViewbagGrantTypeId, CurrentPhaseNumber,
                    model.OfficeId);
            if (model.ViewBagIfProgramNumberExceed == false)
            {
                TempData["RequestGrantNotifications"] = "आयोजना /कार्यक्रमको संख्या धेरै भयो ।";
                return RedirectToAction("ViewDetails", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
            }

            string FileNameVal = model.ConditionUploadFile == null ? string.Empty : model.ConditionUploadFile.FileName;
            bool IfCheckListInserted = services.CheckIfAlreadyInsertedInCondictionDetails(model.SubProgramId);
            bool IfRequiredFileUploaded = false;

            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            bool IfPointsInserted = ObjService.CheckIfProgramValuationBasisExist(model.SubProgramId, model.ViewbagGrantTypeId, OfficeId);
            bool IfFIfteenYojanaAlreadyInserted = services.IsAlreadyInsertedInFifteenYojanaDetails(model.SubProgramId);
            model.FinalDocumentsUrl = string.Empty;


            if (IfCheckListInserted == true && IfPointsInserted == true && IfFIfteenYojanaAlreadyInserted == true && model.ProgramPirority >= 0)
            {
                int Usertype = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(OfficeId);
                string ErrorMessage = "पेश गर्नुपर्ने अनिवार्य कागजातहरू अपलोड गर्नुहोस ।";
                if (model.ViewbagGrantTypeId == 1)
                {
                    //IfRequiredFileUploaded = services.CheckIfUploadFileInsertedOrNot(model.SubProgramId, 1);
                    IfRequiredFileUploaded = services.CheckIfUploadFileInsertedOrNotUpdated(model.SubProgramId, 1, Usertype);

                }
                else
                {
                    //IfRequiredFileUploaded = services.CheckIfUploadFileInsertedOrNot(model.SubProgramId, 2);
                    IfRequiredFileUploaded = services.CheckIfUploadFileInsertedOrNotUpdated(model.SubProgramId, 1, Usertype);

                }

                if (IfRequiredFileUploaded == false)
                {

                    TempData["Notifications"] = ErrorMessage;
                    return RedirectToAction("ReqDocUploads", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
                }
                else
                {
                    var SubmissionDate = GetSubmissionDate();
                    DateTime TodayDate = DateTime.Today;

                    if (TodayDate > SubmissionDate)
                    {
                        TempData["Notification"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                        return RedirectToAction("LogOff", "Account", new { Area = "" });
                    }

                    else
                    {
                        if (services.UpdateSubProgramStatus(model.SubProgramId, model.ProgramId))
                        {
                            string AuthEmailId = CommontUtilities.GetOfficeAuthorizedEmailId(OfficeId);
                            string GrantTypeName = "Complementary";
                            if (model.GrantTypeId == 1)
                            {
                                GrantTypeName = "Special";
                            }
                            if (!string.IsNullOrEmpty(AuthEmailId))
                            {
                                CommontUtilities.SendSubmittedMessage(AuthEmailId, GrantTypeName, model.SubProgramId);
                            }

                            TempData["RequestGrantNotifications"] = "आ.व.२०८१/८२ का लागि तपाँईको प्रस्ताव राष्ट्रिय योजना आयोग मा पेश भयो ।";
                        }
                        else
                        {
                            TempData["RequestGrantNotifications"] = "सिस्टममा केही समस्या देखियो । पुनह प्रयास गर्नुहोस ।";
                        }
                    }

                }


            }

            else
            {
                TempData["RequestGrantNotifications"] = "कृपया सम्पुर्ण विवरण भर्नुहोस । शर्त घोषणा, थप विवरण वा राष्ट्रिय उदेश्यसँग सामञ्जस्यता वा आयोजना/कार्यक्रमको प्राथमिकता क्रम भर्न बाँकि छ ।";
            }



            return RedirectToAction("ViewDetails", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
        }

        public ActionResult RequestForApproved(int id, int id1)
        {
            //Update status ......

            return View();
        }

        public ActionResult DeleteSubProgramMaster(int id, int id1)
        {
            var SubmissionDate = DateTime.ParseExact("12/02/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", "SubProgramSetup", new { @id = id1 });
            }
            else
            {
                //check if already request ....

                SubProgramMaster model = new SubProgramMaster();
                model.SubProgramId = id;
                model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
                int Status = model.Status;
                if (model.ApprovedStatus == true)
                {
                    TempData["Notifications"] = "प्रमाणिकरण गर्न पठाईसकेको वा प्रमाणिकरण को प्रकियामा रहेको आयोजना/कार्यक्रम हटाउन मिल्दैन ।";
                }
                else
                {
                    services.DeleteSubProgramDetails(model);
                    TempData["Notifications"] = "आयोजना / कार्यक्रमको विवरण सिस्टम बाट हटाईयो ।";
                }

                return RedirectToAction("ListSubProgram", new { @id = id1 });
            }
        }

        #region Progress Details

        public ActionResult ProgressDetailView(int id, int id1)
        {
            FiscalYearViewModel model = new FiscalYearViewModel();
            model.FiscalYearViewModelList = new List<FiscalYearViewModel>();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            SubProgramMaster subpromodel = new SubProgramMaster();
            subpromodel.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            subpromodel = services.PopulateSubProgram(id1).Where(x => x.SubProgramId == id).FirstOrDefault();
            int YearDuration = subpromodel.TimeDurationYear;
            int? FiscalYearId = subpromodel.FiscalYearId;
            model.FiscalYearViewModelList = services.PopulateFiscalYearForProgessReport(id, id1).Where(x => x.FiscalYearId >= subpromodel.FiscalYearId).Take(YearDuration).ToList();
            model.GrantTypeId = id1;
            model.ProgramId = id;
            model.ViewBagOfficeId = OfficeId;

            return View(model);
        }

        public ActionResult AddProgressDetail(int id, int id1, int id2, int id3)//subprogramid, grantypeid, quartmesterid,fiscalyearId
        {
            //add validation rule....user can add only first quarter report...check....
            //change yearly id3 currently 14 next time 15.....
            //if (id3 >= 14 && id2 > 1)
            //{

            //    TempData["ProgresReportNtf"] = "दोस्रो र तेस्रो  चौमासिक अबधिको प्रगति विवरण बुझाउन मिल्दैन ।";
            //    return RedirectToAction("ProgressDetailView", new { id = id, id1 = id1 });
            //}
            //this is changed by dharan municipalities........
            if (id3 >= 14 && id2 > 2)
            {

                TempData["ProgresReportNtf"] = "दोस्रो र तेस्रो  चौमासिक अबधिको प्रगति विवरण बुझाउन मिल्दैन ।";
                return RedirectToAction("ProgressDetailView", new { id = id, id1 = id1 });
            }
            if (id3 > 14)
            {
                TempData["ProgresReportNtf"] = "यो अबधिको प्रगति विवरण बुझाउन मिल्दैन ।";
                return RedirectToAction("ProgressDetailView", new { id = id, id1 = id1 });
            }


            SubProgramMaster model = new SubProgramMaster();

            model.ObjQuadrimesterReportsDetailViewModel.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            model.ObjQuadrimesterReportsDetailViewModel = services.PopulateQuadrimesterReports(id2, model.ObjQuadrimesterReportsDetailViewModel.OfficeId, id1).SingleOrDefault(x => x.ProgramId == id && x.FiscalYearId == id3);
            if (model.ObjQuadrimesterReportsDetailViewModel == null)
            {
                model.ObjQuadrimesterReportsDetailViewModel = new QuadrimesterReportsDetailViewModel();
                model.ObjQuadrimesterReportsDetailViewModel.IsContactNoticeIssued = false;
                model.ObjQuadrimesterReportsDetailViewModel.IsContractDone = false;
                model.ObjQuadrimesterReportsDetailViewModel.IsFirstInstallmentTaken = false;
            }

            model.ObjQuadrimesterReportsDetailViewModel.ProgramId = id;
            model.ObjQuadrimesterReportsDetailViewModel.QuadrimesterId = id2;
            model.ViewbagGrantTypeId = id1;
            model.ObjQuadrimesterReportsDetailViewModel.FiscalYearId = id3;
            return View(model);
        }
        public ActionResult AddProgressDetailByFY(int id, int id1, int id2, int id3)//subprogramid, grantypeid, fiscalyearId, id3= checkforyearendornot
        {

            SubProgramMaster model = new SubProgramMaster();
            model.ObjQuadrimesterReportsDetailViewModel.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
          
            model.ObjQuadrimesterReportsDetailViewModel = services.PopulateProgressReportsByFYID(id2, model.ObjQuadrimesterReportsDetailViewModel.OfficeId, id1).SingleOrDefault(x => x.ProgramId == id && x.FiscalYearId == id2 && x.ReportOfFisalYearEnd==id3);
            ViewBag.Mode = "Edit";
            if (model.ObjQuadrimesterReportsDetailViewModel == null)
            {
                model.ObjQuadrimesterReportsDetailViewModel = new QuadrimesterReportsDetailViewModel();
                model.ObjQuadrimesterReportsDetailViewModel.IsContactNoticeIssued = false;
                model.ObjQuadrimesterReportsDetailViewModel.IsContractDone = false;
                model.ObjQuadrimesterReportsDetailViewModel.IsFirstInstallmentTaken = false;
                model.ObjQuadrimesterReportsDetailViewModel.QuadrimesterReportsDetailId = 0;
                model.ObjQuadrimesterReportsDetailViewModel.ReportOfFisalYearEnd = id3;
              
                ViewBag.Mode = "Create";
            }
            else 
            {
                TempData["ProgresReportNtf"] = "प्रगति विवरण अद्यावधिक भईसकेको छ";
                return RedirectToAction("ProgressDetailList");
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

        [HttpPost]
        public ActionResult AddProgressDetailByFY(SubProgramMaster model)
        {

            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ProgressDetailList");
            }



            SubProgramServices services = new SubProgramServices();

            //check application status running or terminated by users
            int? AppRunningStatus = model.ObjQuadrimesterReportsDetailViewModel.ApplicationProgressStatusId;
            if (AppRunningStatus == 2)
            {
                string NotRunningProofDocStr = model.ObjQuadrimesterReportsDetailViewModel.AppNotRunningProofFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.AppNotRunningProofFile.FileName;
                if (string.IsNullOrEmpty(NotRunningProofDocStr) == false || (string.IsNullOrEmpty(model.ObjQuadrimesterReportsDetailViewModel.NotRunningProofDoc) == false))
                {
                    if (string.IsNullOrEmpty(NotRunningProofDocStr) == false)
                    {
                        string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        model.ObjQuadrimesterReportsDetailViewModel.NotRunningProofDoc = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.AppNotRunningProofFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.NotRunningProofDoc);
                        model.ObjQuadrimesterReportsDetailViewModel.AppNotRunningProofFile.SaveAs(path);
                    }

                    //Default Value for not null field
                    model.ObjQuadrimesterReportsDetailViewModel.ProgramConductPlace = string.Empty;
                    model.ObjQuadrimesterReportsDetailViewModel.AchievementFinance = 0;
                    model.ObjQuadrimesterReportsDetailViewModel.AchievementMaterial = string.Empty;
                    model.ObjQuadrimesterReportsDetailViewModel.TargetedFinance = 0;
                    model.ObjQuadrimesterReportsDetailViewModel.TargetedMaterial = string.Empty;
                    model.ObjQuadrimesterReportsDetailViewModel.IsNikashaMaag = false;
                    model.ObjQuadrimesterReportsDetailViewModel.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                  


                    if (services.InsertQuadrimesterReportDetail(model.ObjQuadrimesterReportsDetailViewModel) == "Saved Successfully")
                    {

                        TempData["ProgresReportNtf"] = "तपाँईको विवरण पेश भयो  ।";
                        return RedirectToAction("ProgressDetailList");
                    }
                    else
                    {
                        TempData["ProgresReportNtf"] = "सिस्टममा समस्या देखियो । पुनह् कोशिस गर्नुहोस् ।";
                        return RedirectToAction("ProgressDetailList");

                    }



                }
                else
                {
                    if (model.ObjQuadrimesterReportsDetailViewModel.QuadrimesterReportsDetailId > 0)
                    {
                        ViewBag.Mode = "Edit";
                    }
                    else
                    {
                        ViewBag.Mode = "Create";
                    }
                    ViewBag.ErrorMessage = "कार्यान्वयन नभएको भए आधिकारिक पत्र अपलोड गर्नुहोस । ";
                    model.ObjQuadrimesterReportsDetailViewModel.ApplicationProgressStatusId = 2;
                    return View(model);
                }
            }
            else
            {
                decimal? nikahamagrakam = 0m;
                decimal? thekkaram = 0m;
                if (model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam > model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount)
                {
                    if (model.ObjQuadrimesterReportsDetailViewModel.QuadrimesterReportsDetailId > 0)
                    {
                        ViewBag.Mode = "Edit";
                    }
                    else
                    {
                        ViewBag.Mode = "Create";
                    }
                    ViewBag.ErrorMessage = "निकाशा रकम ठेक्का रकम भन्दा धेरै भयो । ";
                    return View(model);
                }


                model.ObjQuadrimesterReportsDetailViewModel.AppRunningStatus = 1;

                string ProjectFileUploadStr = model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile.FileName;
                string PictureOfProjectOneFileStr = model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile.FileName;
                string PictureOfProjectTwoFileStr = model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile.FileName;
                string PictureOfProjectThreeFileStr = model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile.FileName;
                string NikasaMaagFileStr = string.Empty;
                if (string.IsNullOrEmpty(ProjectFileUploadStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUpload = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUpload);
                    model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile.SaveAs(path);
                }
                if (string.IsNullOrEmpty(PictureOfProjectOneFileStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOne = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOne);
                    model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile.SaveAs(path);
                }
                if (string.IsNullOrEmpty(PictureOfProjectTwoFileStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwo = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwo);
                    model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile.SaveAs(path);
                }

                if (string.IsNullOrEmpty(PictureOfProjectThreeFileStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThree = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThree);
                    model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile.SaveAs(path);
                }

                //contract notice file
                string ContractNoticeFileStr = model.ObjQuadrimesterReportsDetailViewModel.AppContractNoticeFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.AppContractNoticeFile.FileName;
                if (string.IsNullOrEmpty(ContractNoticeFileStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.ContractNoticeFile = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.AppContractNoticeFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.ContractNoticeFile);
                    model.ObjQuadrimesterReportsDetailViewModel.AppContractNoticeFile.SaveAs(path);
                }

                //contract file
                string ContractDoneFileStr = model.ObjQuadrimesterReportsDetailViewModel.AppcontractFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.AppcontractFile.FileName;
                if (string.IsNullOrEmpty(ContractDoneFileStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.ContractFile = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.AppcontractFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.ContractFile);
                    model.ObjQuadrimesterReportsDetailViewModel.AppcontractFile.SaveAs(path);
                }
                //running bill

                string RunningbillFileStr = model.ObjQuadrimesterReportsDetailViewModel.AppRunningBillsDetailFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.AppRunningBillsDetailFile.FileName;
                if (string.IsNullOrEmpty(RunningbillFileStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.RunningBillFile = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.AppRunningBillsDetailFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.RunningBillFile);
                    model.ObjQuadrimesterReportsDetailViewModel.AppRunningBillsDetailFile.SaveAs(path);
                }

                //bhukatni voucher bill

                string VuktaniVoucharFileStr = model.ObjQuadrimesterReportsDetailViewModel.AppBhuktaniVoucherFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.AppBhuktaniVoucherFile.FileName;
                if (string.IsNullOrEmpty(VuktaniVoucharFileStr) == false)
                {

                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.BhuktaniFile = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.AppBhuktaniVoucherFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.BhuktaniFile);
                    model.ObjQuadrimesterReportsDetailViewModel.AppBhuktaniVoucherFile.SaveAs(path);
                }


                //myadthap File
                string MyadthapFileStr = model.ObjQuadrimesterReportsDetailViewModel.AppTimeExtendedFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.AppTimeExtendedFile.FileName;
                if (string.IsNullOrEmpty(MyadthapFileStr) == false)
                {
                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.TimeExtendedFile = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.AppTimeExtendedFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.TimeExtendedFile);
                    model.ObjQuadrimesterReportsDetailViewModel.AppTimeExtendedFile.SaveAs(path);
                }

                // suchana pati
                string SuchanaPatiFileStr = model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUploadFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUploadFile.FileName;
                if (string.IsNullOrEmpty(SuchanaPatiFileStr) == false)
                {
                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff"); 
                    model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUpload = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUploadFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUpload);
                    model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUploadFile.SaveAs(path);
                }

                // sarbajanik parickchen file
                string SarbajanikParikchenStr = model.ObjQuadrimesterReportsDetailViewModel.SarbajanikParikchenFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUploadFile.FileName;
                if (string.IsNullOrEmpty(SarbajanikParikchenStr) == false)
                {
                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.SarbajanikParikchen = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUploadFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.SuchanaPatiUpload);
                    model.ObjQuadrimesterReportsDetailViewModel.SarbajanikParikchenFile.SaveAs(path);
                }

                // sunuwai file
                string SarbajanikSunuwaiStr = model.ObjQuadrimesterReportsDetailViewModel.SarbajanikSunuwaiFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.SarbajanikSunuwaiFile.FileName;
                if (string.IsNullOrEmpty(SarbajanikSunuwaiStr) == false)
                {
                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.SarbajanikSunuwai = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.SarbajanikSunuwaiFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.SarbajanikSunuwai);
                    model.ObjQuadrimesterReportsDetailViewModel.SarbajanikSunuwaiFile.SaveAs(path);
                }

                // samapurak file
                string SamapurakAnusuchi5FileStr = model.ObjQuadrimesterReportsDetailViewModel.SamapurakAnusuchi5File == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.SamapurakAnusuchi5File.FileName;
                if (string.IsNullOrEmpty(SamapurakAnusuchi5FileStr) == false)
                {
                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.SamapurakAnusuchi5 = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.SamapurakAnusuchi5File.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.SamapurakAnusuchi5);
                    model.ObjQuadrimesterReportsDetailViewModel.SamapurakAnusuchi5File.SaveAs(path);
                }

                // Bisehs file
                string BisehsAnusuchi5FileStr = model.ObjQuadrimesterReportsDetailViewModel.BiseshAnusuchi7File == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.BiseshAnusuchi7File.FileName;
                if (string.IsNullOrEmpty(BisehsAnusuchi5FileStr) == false)
                {
                    string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    model.ObjQuadrimesterReportsDetailViewModel.BiseshAnusuchi7 = Path.GetFileName(PrifixLetter + "_" + OfficeId + "_" + model.ObjQuadrimesterReportsDetailViewModel.BiseshAnusuchi7File.FileName);
                    var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.BiseshAnusuchi7);
                    model.ObjQuadrimesterReportsDetailViewModel.BiseshAnusuchi7File.SaveAs(path);
                }


                if (model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount.HasValue)
                {
                    if (model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount > 0)
                    {
                        model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount = model.ObjQuadrimesterReportsDetailViewModel.TotalContractAmount * 100000;
                    }
                }

                if (model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam.HasValue)
                {
                    if (model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam > 0)
                    {
                        model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam = model.ObjQuadrimesterReportsDetailViewModel.TotalNikashaRamam * 100000;
                    }
                }

                if (model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed.HasValue)
                {
                    if (model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed > 0)
                    {
                        model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed = model.ObjQuadrimesterReportsDetailViewModel.TotalAmountUsed * 100000;
                    }
                }



                model.ObjQuadrimesterReportsDetailViewModel.IsNikashaMaag = false;
                model.ObjQuadrimesterReportsDetailViewModel.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                if (services.InsertQuadrimesterReportDetail(model.ObjQuadrimesterReportsDetailViewModel) == "Saved Successfully")
                {

                    TempData["ProgresReportNtf"] = "तपाँईको विवरण पेश भयो  ।";
                    return RedirectToAction("ProgressDetailList");
                }
                ViewBag.ErrorMessage = "सिस्टममा केहि त्रुटि भयो । कृपया पुनह प्रयास गर्नुहोस ।";
                return View(model);

            }





        }


        [HttpPost]
        public ActionResult AddProgressDetail(SubProgramMaster model)
        {
            string ProjectFileUploadStr = model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile.FileName;
            string PictureOfProjectOneFileStr = model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile.FileName;
            string PictureOfProjectTwoFileStr = model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile.FileName;
            string PictureOfProjectThreeFileStr = model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile == null ? string.Empty : model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile.FileName;
            string NikasaMaagFileStr = string.Empty;
            if (string.IsNullOrEmpty(ProjectFileUploadStr) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUpload = Path.GetFileName(PrifixLetter + "_" + model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUpload);
                model.ObjQuadrimesterReportsDetailViewModel.ProjectFileUploadFile.SaveAs(path);
            }
            if (string.IsNullOrEmpty(PictureOfProjectOneFileStr) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOne = Path.GetFileName(PrifixLetter + "_" + model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOne);
                model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectOneFile.SaveAs(path);
            }
            if (string.IsNullOrEmpty(PictureOfProjectTwoFileStr) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwo = Path.GetFileName(PrifixLetter + "_" + model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwo);
                model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectTwoFile.SaveAs(path);
            }

            if (string.IsNullOrEmpty(PictureOfProjectThreeFileStr) == false)
            {

                string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThree = Path.GetFileName(PrifixLetter + "_" + model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile.FileName);
                var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThree);
                model.ObjQuadrimesterReportsDetailViewModel.PictureOfProjectThreeFile.SaveAs(path);
            }


            model.ObjQuadrimesterReportsDetailViewModel.IsNikashaMaag = false;
            SubProgramServices services = new SubProgramServices();
            model.ObjQuadrimesterReportsDetailViewModel.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            if (services.InsertQuadrimesterReportDetail(model.ObjQuadrimesterReportsDetailViewModel) == "Saved Successfully")
            {

                TempData["ProgresReportNtf"] = "तपाँईको विवरण पेश भयो  ।";
                return RedirectToAction("ProgressDetailView", new { @id = model.ObjQuadrimesterReportsDetailViewModel.ProgramId, @id1 = model.ViewbagGrantTypeId });
            }
            ViewBag.ErrorMessage = "सिस्टममा केहि त्रुटि भयो । कृपया पुनः प्रयास गर्नुहोस ।";
            return View(model);
        }

        public ActionResult ProgressDetailList()
        {
            
            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.QuadrimesterReportsDetailViewModelList = services.PopulateQuadrimesterReports(0, OfficeId, 0);//qua id, officeid, granttyp id
            //model.SubProgramMasterList = services.PopulateOldSubProgramPhaseWiseListForProgressRpt(0, OfficeId, 1).ToList();
            model.YearlyWiseProgressDetailsListVMList = services.SP_GetFYWiseProgressSubmissionList(OfficeId, 0);
            return View(model);

        }


        public ActionResult ViewDetailProgressReportForClient(int id, int id1, int id2, int id3)
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
            model.QuadrimesterReportsDetailViewModelList = rs.PopulateProgressReportForAdmin(id, 0).Where(x => x.QuadrimesterReportsDetailId == id2).ToList();//subprogram id and quardid


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

        #region Document Managemnt For Missing

        public ActionResult MissingDocumentManagement()
        {

            SubProgramMaster model = new SubProgramMaster();
            SubProgramServices services = new SubProgramServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            //model.QuadrimesterReportsDetailViewModelList = services.PopulateQuadrimesterReports(0, OfficeId, 0);//qua id, officeid, granttyp id
            //model.SubProgramMasterList = services.PopulateOldSubProgramPhaseWiseListForProgressRpt(0, OfficeId, 1).ToList();
            model.YearlyWiseProgressDetailsListVMList = services.SP_GetFYWiseProgressSubmissionList(OfficeId, 0);
            return View(model);

        }


        public ActionResult ViewDocuments(int subProgramId, int grantType)
        {
            
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            SubProgramMaster model = new SubProgramMaster();

            model = services.PopulateSubProgram(grantType).SingleOrDefault(x => x.SubProgramId == subProgramId && x.OfficeId == CurrentLoginUserOfficeId);
            if (model != null)
            {
                model.OnlyDocumentsRequirementsViewModel = services.SPUP_PopulateDocRequirementList(grantType, CurrentLoginUserType);
                
                model.DocumentsRequirementsViewModel = services.SPUP_PopulateRequiredDocForEdit(subProgramId, 1);

                foreach (var item in model.DocumentsRequirementsViewModel)
                {
                    if (!string.IsNullOrEmpty(item.UploadFileUrl))
                    {
                        var physicalPath = Server.MapPath(
                                                 Path.Combine("~/RequiredDocs", item.UploadFileUrl)
                                             );
                        item.FileExists = System.IO.File.Exists(physicalPath);
                    }
                    else
                    {
                        item.FileExists = false;
                    }
                }

            }

            return View(model);
        }

        public ActionResult AddMissingDocument(int subProgramId, int grantType)
        {
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            SubProgramMaster model = new SubProgramMaster();
            model.OnlyDocumentsRequirementsViewModel = services.SPUP_PopulateDocRequirementList(grantType, CurrentLoginUserType);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMissingDocument(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            if (files != null)
            {
                int index = 0;
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Generate unique filename
                        string fileName = $"{model.SubProgramId}_{System.IO.Path.GetFileName(file.FileName)}";
                        string path = System.IO.Path.Combine(Server.MapPath("~/RequiredDocs"), fileName);
                        // Save file
                        file.SaveAs(path);
                        // Assign file name to corresponding model object
                        model.OnlyDocumentsRequirementsViewModel[index].UploadFileUrl = fileName;
                    }

                    index++;
                }
            }

            foreach (var item in model.OnlyDocumentsRequirementsViewModel)
            {
                services.SPUP_InsertMissingDocumnet(item.RequiredDocID, model.SubProgramId, item.UploadFileUrl, CurrentPhaseNumber,model.UploaderName,model.UploaderPosition);
            }

            return RedirectToAction("ViewDocuments", new { subProgramId = model.SubProgramId, grantType=model.GrantTypeId });
        }

            #endregion


            #region Approved Program Detail section

            [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]//This is for removing tempdata if back button pressed
        public ActionResult ApprovedListSubProgram(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.SubProgramMasterList = services.PopulateSubProgram(id).Where(x => x.OfficeId == model.OfficeId).ToList();
            model.ViewbagGrantTypeId = id;
            return View(model);
        }


        public ActionResult ApprovedProgramViewDetails(int id, int id1)//id sub program id, id1 grant type id
        {
            SubProgramMaster model = new SubProgramMaster();
            model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
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
        #endregion

        public ActionResult CancleForThisFY(int id, int id1)
        {
            var SubmissionDate = GetSubmissionDate();
            //var SubmissionDate = DateTime.Now.AddDays(365);
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("PreviousSubProgramList", "SubProgramSetup", new { @id = id1 });
            }
            else
            {
                SubProgramMaster model = new SubProgramMaster();
                model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
                model.ViewbagGrantTypeId = id1;
                model.SubProgramId = id;
                model.CanceledDocUrl = CommontUtilities.GetCancelledDocumentsUrl(id);
                model.CanceledDocUrl1 = CommontUtilities.GetCancelledDocuments1Url(id);
                ViewBag.Mode = "New";
                if (!string.IsNullOrEmpty(model.CanceledDocUrl))
                {
                    ViewBag.Mode = "Old";
                }

                return View(model);
            }
        }



        [HttpPost]
        public ActionResult CancleForThisFY(SubProgramMaster model, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            string CancelledDocParam = string.Empty;
            string CancelledDocParam1 = string.Empty;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{System.IO.Path.GetFileName(file.FileName)}";
                    string path = System.IO.Path.Combine(Server.MapPath("~/RequiredDocs"), uniqueFileName);
                    file.SaveAs(path);
                    CancelledDocParam = uniqueFileName;
                    //ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    //ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                CancelledDocParam = model.CanceledDocUrl;
            }

            if (file1 != null && file1.ContentLength > 0)
            {
                try
                {
                    string uniqueFileName1 = $"{Guid.NewGuid()}_{System.IO.Path.GetFileName(file1.FileName)}";
                    string path = System.IO.Path.Combine(Server.MapPath("~/RequiredDocs"), uniqueFileName1);
                    file1.SaveAs(path);
                    CancelledDocParam1 = uniqueFileName1;
                    //ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    //ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                CancelledDocParam1 = model.CanceledDocUrl1;
            }


            SubProgramServices services = new SubProgramServices();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.FiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
            string message = services.UpdateSubProgramCancelStatus(model.SubProgramId, model.OfficeId, model.CancelledRemarks, model.FiscalYearId, CancelledDocParam,CancelledDocParam1);
            if (message.ToString() == "Updated Successfully")
            {
                RequestGrantAmountService rgservices = new RequestGrantAmountService();

                RequestGrantAmountModel rgm = new RequestGrantAmountModel();
                rgm = rgservices.PopulateGrantRequestAmountDetail(model.OfficeId, model.SubProgramId, 16).SingleOrDefault(x => x.ProgramId == model.SubProgramId);
                if (rgm == null)
                {
                    rgm = new RequestGrantAmountModel();
                }
                rgm.ProgramId = model.SubProgramId;
                rgm.OfficeId = model.OfficeId;
                rgm.FiscalYearId = 17;//request amount fiscal year id
                rgm.ProgramTimeDuration = 3;
                rgm.AmountFirst = 0;
                rgm.AmountSecond = 0;
                rgm.AmountThird = 0;
                rgm.Status = false;
                rgm.CreatedDate = DateTime.Now;
                rgm.ContractAmount = 0;
                rgm.ChaluAmount = 0;
                rgm.PujiAmount = 0;
                rgm.IsCanceled = true;
                rgm.RequestReasonDoc = model.CancelledRemarks;
                rgservices.InsertRequestDemandAmount(rgm);

                TempData["Notifications"] = "यो आयोजना वा कार्यक्रम आगामी आ.व. का लागि अनुदान रकम माग नगर्ने निर्णय भई रद्द भयो । ";

            }
            else
            {
                TempData["Notifications"] = "सिस्टममा समस्या देखियो । कृपया पुनह प्रयास गर्नुहोस । ";
            }
            //return View(model);
            return RedirectToAction("PreviousSubProgramList", new { id = model.ViewbagGrantTypeId });
        }

        //public ActionResult CancleForThisFY(int id, int id1)
        //{
        //    var SubmissionDate = GetSubmissionDate();
        //    //var SubmissionDate = DateTime.Now.AddDays(365);
        //    DateTime TodayDate = DateTime.Today;
        //    if (TodayDate > SubmissionDate)
        //    {
        //        TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
        //        return RedirectToAction("ListSubProgram", "SubProgramSetup", new { @id = id1 });
        //    }
        //    else
        //    {
        //        SubProgramMaster model = new SubProgramMaster();
        //        model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
        //        model.ViewbagGrantTypeId = id1;
        //        model.SubProgramId = id;
        //        return View(model);
        //    }
        //}


        //[HttpPost]
        //public ActionResult CancleForThisFY(SubProgramMaster model, HttpPostedFileBase file)
        //{
        //    string CancelledDocParam = string.Empty;

        //    if (file != null && file.ContentLength > 0)
        //        try
        //        {
        //            string path = Path.Combine(Server.MapPath("~/RequiredDocs"),
        //                                       Path.GetFileName(file.FileName));
        //            file.SaveAs(path);
        //            CancelledDocParam = file.FileName;
        //            //ViewBag.Message = "File uploaded successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            //ViewBag.Message = "ERROR:" + ex.Message.ToString();
        //        }

        //    SubProgramServices services = new SubProgramServices();
        //    model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
        //    model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
        //    model.FiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
        //    string message = services.UpdateSubProgramCancelStatus(model.SubProgramId, model.OfficeId, model.CancelledRemarks, model.FiscalYearId, CancelledDocParam);
        //    if (message.ToString() == "Updated Successfully")
        //    {
        //        TempData["Notifications"] = "यो आयोजना/कार्यक्रम यस वर्षको लागी रद्द भयो । ";


        //    }
        //    else
        //    {
        //        TempData["Notifications"] = "सिस्टममा समस्या देखियो । कृपया पुनह प्रयास गर्नुहोस । ";
        //    }
        //    //return View(model);
        //    return RedirectToAction("ListSubProgram", new { id = model.ViewbagGrantTypeId });
        //}







        public ActionResult ChangeProgramPriority(int id, int id1)
        {
            SubProgramMaster model = new SubProgramMaster();
            model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ViewbagGrantTypeId = id1;
            model.SubProgramId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeProgramPriority(SubProgramMaster model)
        {

            SubProgramServices services = new SubProgramServices();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            string message = services.ChangeSubProgramPriority(model.SubProgramId, model.ProgramPirority, model.OfficeId, model.ViewbagGrantTypeId);
            if (message.ToString() == "Already Inserted")
            {
                TempData["PriorityMessage"] = "यो प्राथमिकीकरणको संख्या पहिले देखीनै सिस्टममा छ । कृपया पुनह प्रयास गर्नुहोस । ";

            }
            else
            {
                TempData["PriorityMessage"] = "यो आयोजना/कार्यक्रमको प्राथमिकीकरणको संख्या परिवर्तन भयो । ";
            }
            //return View(model);
            return RedirectToAction("ChangeProgramPriority", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
        }



        public ActionResult ProgramPriority(int id, int id1)
        {
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", new { @id = id1 });
            }
            else
            {
                SubProgramMaster model = new SubProgramMaster();
                model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
                model.ViewbagGrantTypeId = id1;
                model.SubProgramId = id;
                ViewBag.Mode = "Edit";
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ProgramPriority(SubProgramMaster model)
        {

            SubProgramServices services = new SubProgramServices();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            string message = services.ChangeSubProgramPriority(model.SubProgramId, model.ProgramPirority, model.OfficeId, model.ViewbagGrantTypeId);

            if (message.ToString() == "Already Inserted")
            {
                TempData["RequestGrantNotifications"] = "यो प्राथमिकीकरणको संख्या पहिले देखीनै सिस्टममा छ । कृपया पुनह प्रयास गर्नुहोस । ";
            }
            else
            {
                TempData["RequestGrantNotifications"] = "यो आयोजना/कार्यक्रमको प्राथमिकीकरणको संख्या परिवर्तन भयो । ";
            }


            //return View(model);
            return RedirectToAction("ViewDetails", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
        }


        public ActionResult ViewDetailPrevious(int id, int id1)//id sub program id, id1 grant type id
        {
            SubProgramMaster model = new SubProgramMaster();
            model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            //model.ObjSupportingDocumentsModel = services.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
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


        public ActionResult ReqestGrantAmount(int id, int id1)
        {



            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", "SubProgramSetup", new { @id = id1 });
            }
            else
            {

                RequestGrantAmountModel model = new RequestGrantAmountModel();
                SubProgramMaster Submodel = new SubProgramMaster();
                Submodel = services.PopulateSubProgramBySupprogramAndGrantTypeId(id1, id);
                int TimeDureation = Submodel.TimeDurationYear;
                int? FiscalYearId = Submodel.FiscalYearId;
                model.FiscalYearId = Convert.ToInt32(FiscalYearId);
                model.ProgramTimeDuration = TimeDureation;
                model.ProgramId = id;
                model.ViewBagGrantTypeId = id1;
                model.ProgramTitle = Submodel.SubProgramTitle;
                model.TotalBudgetForProgram = Submodel.TotalBudget;
                model.ProgramPhaseNumber = Submodel.PhaseStatus;
                model.ApprovedStatusBool = Submodel.ApprovedStatus;
                model.BudgetForFirstYear = Submodel.BudgetForFirstYear;
                model.BudgetForSecondYear = Submodel.BudgetForSecondYear;
                model.BudgetForThirdYear = Submodel.BudgetForThirdYear;
                RequestGrantAmountService Reqservice = new RequestGrantAmountService();
                int CurrentFiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
                CurrentFiscalYearId = 18;
                int OfficeIdParam = Submodel.OfficeId;
                model = Reqservice.PopulateGrantRequestAmountDetail(model.OfficeId, model.ProgramId, CurrentFiscalYearId).SingleOrDefault(x => x.ProgramId == id);
                //get next year request amount here;
                int CurrentFyID = CommontUtilities.GetCurrentFiscalYearId();


                if (model == null)
                {
                    model = new RequestGrantAmountModel();
                    model.ViewBagGrantTypeId = id1;
                    model.FiscalYearId = Convert.ToInt32(FiscalYearId);
                    model.ProgramTimeDuration = TimeDureation;
                    model.ProgramId = id;
                    model.ProgramTitle = Submodel.SubProgramTitle;
                    model.TotalBudgetForProgram = Submodel.TotalBudget;
                    model.ProgramPhaseNumber = Submodel.PhaseStatus;
                    model.ApprovedStatusBool = Submodel.ApprovedStatus;
                    model.AmountSecond = CommontUtilities.GetNextYearRequestGrantAmount(CurrentFiscalYearId, OfficeIdParam, id);

                }
                else
                {

                    model.AmountSecond = CommontUtilities.GetNextYearRequestGrantAmount(CurrentFiscalYearId, Submodel.OfficeId, id);
                    if (model.PujiAmount.HasValue)
                    {
                        if (model.PujiAmount > 0)
                        {
                            model.PujiAmount = model.PujiAmount / 100000;
                        }
                    }

                    if (model.ChaluAmount.HasValue)
                    {
                        if (model.ChaluAmount > 0)
                        {
                            model.ChaluAmount = model.ChaluAmount / 100000;
                        }
                    }

                    if (model.AmountSecond.HasValue)
                    {
                        if (model.AmountSecond > 0)
                        {
                            model.AmountSecond = model.AmountSecond / 100000;
                        }
                    }

                    if (model.ContractAmount.HasValue)
                    {
                        if (model.ContractAmount > 0)
                        {
                            model.ContractAmount = model.ContractAmount / 100000;
                        }
                    }

                    if (model.OfficeAmount.HasValue)
                    {
                        if (model.OfficeAmount > 0)
                        {
                            model.OfficeAmount = model.OfficeAmount / 100000;
                        }
                    }

                  

                }

                model.FiscalYearId = Convert.ToInt32(FiscalYearId);
                model.ProgramTimeDuration = TimeDureation;
                model.ProgramId = id;
                model.ViewBagGrantTypeId = id1;
                model.ProgramTitle = Submodel.SubProgramTitle;
                model.TotalBudgetForProgram = Submodel.TotalBudget;
                model.ProgramPhaseNumber = Submodel.PhaseStatus;
                model.ApprovedStatusBool = Submodel.ApprovedStatus;
                model.BudgetForFirstYear = Submodel.BudgetForFirstYear;
                model.BudgetForSecondYear = Submodel.BudgetForSecondYear;
                model.BudgetForThirdYear = Submodel.BudgetForThirdYear;
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult ReqestGrantAmount(RequestGrantAmountModel model, HttpPostedFileBase file)
        {

            string RequestDocParam = string.Empty;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/RequiredDocs"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    RequestDocParam = file.FileName;
                    model.RequestReasonDoc = RequestDocParam;
                    //ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    //ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                RequestDocParam = model.RequestReasonDoc;
            }

            //check if program is not approved by admin

            RequestGrantAmountService services = new RequestGrantAmountService();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            //int FiscalYerId = CommontUtilities.GetCurrentFiscalYearId();
            int FiscalYerId = 18;
            model.FiscalYearId = FiscalYerId;
            model.FiscalYearId = model.FiscalYearId;


            if (model.ViewBagGrantTypeId == 2)
            {
                if (!model.AmountSecond.HasValue)
                {
                    TempData["Notifications"] = "राख्नुभएको रकम मिलेन  ।";
                }
                else if (model.AmountSecond <= 0)
                {
                    TempData["Notifications"] = "राख्नुभएको रकम मिलेन  ।";
                }
                else
                {
                    decimal TotalApprovedAmount = CommontUtilities.GetSumProgramWiseAmount(model.ProgramId);
                    if (TotalApprovedAmount > 0)
                    {
                        TotalApprovedAmount = TotalApprovedAmount / 100000;
                    }

                    decimal RequestedAmount = 0;
                    RequestedAmount = Convert.ToDecimal(model.AmountSecond);//rs lakh ma                    
                    decimal BudgetForProgram = decimal.Round(model.TotalBudgetForProgram);
                    if (BudgetForProgram > 0)
                    {
                        BudgetForProgram = BudgetForProgram / 100000;
                    }


                    decimal ToCheckAmount = decimal.Round(TotalApprovedAmount + RequestedAmount);
                    if ((ToCheckAmount > BudgetForProgram) == true)
                    {

                        TempData["Notifications"] = "अनुदान माग गर्न लागिएको रकम कुल लागत रकम भन्दा धेरै भयो ।";

                    }
                    else
                    {
                        if (model.AmountSecond > 0)
                        {
                            model.AmountSecond = model.AmountSecond * 100000;
                        }
                        if (model.ContractAmount.HasValue)
                        {
                            if (model.ContractAmount > 0)
                            {
                                model.ContractAmount = model.ContractAmount * 100000;
                            }
                        }

                        if (model.OfficeAmount.HasValue)
                        {
                            if (model.OfficeAmount > 0)
                            {
                                model.OfficeAmount = model.OfficeAmount * 100000;
                            }
                        }

                        if (model.RequestGrantAmountId > 0)
                        {
                            string ReturnMessage = services.UpdateRequestDemandAmount(model);
                            TempData["Notifications"] = "अनुदान माग गरिएको रकम पेश भयो ।";
                        }
                        else
                        {
                            string ReturnMessage = services.InsertRequestDemandAmount(model);
                            TempData["Notifications"] = "अनुदान माग गरिएको रकम पेश भयो ।";
                        }


                 

                    }


                }
            }
            else
            {
                if (!model.PujiAmount.HasValue && !model.ChaluAmount.HasValue)
                {
                    TempData["Notifications"] = "राख्नुभएको रकम मिलेन  ।";
                }
                else
                {

                    decimal TotalApprovedAmount = CommontUtilities.GetSumProgramWiseAmount(model.ProgramId);
                    if (TotalApprovedAmount > 0)
                    {
                        TotalApprovedAmount = TotalApprovedAmount / 100000;
                    }
                    decimal RequestedAmount = 0;
                    RequestedAmount = Convert.ToDecimal(model.AmountSecond);
                    decimal BudgetForProgram = decimal.Round(model.TotalBudgetForProgram);
                    if (BudgetForProgram > 0)
                    {
                        BudgetForProgram = BudgetForProgram / 100000;
                    }
                    decimal ToCheckAmount = decimal.Round(TotalApprovedAmount + RequestedAmount);
                    if ((ToCheckAmount > BudgetForProgram) == true)
                    {

                        TempData["Notifications"] = "अनुदान माग गर्न लागिएको रकम कुल लागत रकम भन्दा धेरै भयो ।";

                    }
                    else
                    {
                        if (model.ChaluAmount.HasValue)
                        {
                            if (model.ChaluAmount > 0)
                            {
                                model.ChaluAmount = model.ChaluAmount * 100000;
                            }
                        }
                        if (model.PujiAmount.HasValue)
                        {
                            if (model.PujiAmount > 0)
                            {
                                model.PujiAmount = model.PujiAmount * 100000;
                            }
                        }
                        if (model.ContractAmount.HasValue)
                        {
                            if (model.ContractAmount > 0)
                            {
                                model.ContractAmount = model.ContractAmount * 100000;
                            }
                        }

                        model.AmountSecond = model.ChaluAmount + model.PujiAmount;//rs in lakh


                        if(model.RequestGrantAmountId > 0)
                        {
                            string ReturnMessage = services.UpdateRequestDemandAmount(model);
                            TempData["Notifications"] = "अनुदान माग गरिएको रकम पेश भयो ।";
                        }
                        else
                        {
                            string ReturnMessage = services.InsertRequestDemandAmount(model);
                            TempData["Notifications"] = "अनुदान माग गरिएको रकम पेश भयो ।";
                        }

                      

                    }


                }
            }


            return RedirectToAction("PreviousSubProgramList", new { id = model.ViewBagGrantTypeId });
        }


        public ActionResult populateRequestedGrantAmountList(int id)
        {
            RequestGrantAmountModel model = new RequestGrantAmountModel();
            //SubProgramMaster model = new SubProgramMaster();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.ViewRequestGrantAmountModelList = services.PopulateRunninProjectListForClient(0, 1, model.OfficeId);
            return View(model);
        }


        public ActionResult ViewDetailsForGrantRequestAmount(int id, int id1)//id sub program id, id1 grant type id
        {
            SubProgramMaster model = new SubProgramMaster();
            model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
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


        public ActionResult DisplayValidationRuleOld(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.TotalApprovedProgramCount = CommontUtilities.TotalApprovedMultiYearProgramByGrantType(model.OfficeId, id);
            model.TotalRequestGrantAmountCount = CommontUtilities.TotalRequestedGrantAmountByOffice(model.OfficeId, id);
            model.TotalSubmitedProgressReportCount = CommontUtilities.TotalSubmittedProgressReportByOffice(model.OfficeId, id);
            model.ViewbagGrantTypeId = id;


            model.TotalApprovedProgramCountForRPT = CommontUtilities.TotalApprovedMultiYearProgramByGrantTypeForRPT(model.OfficeId, id);
            model.TotalApprovedProgramCountForProgressRPT = CommontUtilities.TotalApprovedMultiYearProgramByGrantTypeForProRpt(model.OfficeId, id);
            model.TotalApprovedProgramCountForProgressRPT = model.TotalApprovedProgramCountForProgressRPT + model.TotalApprovedProgramCountForRPT;



            if (model.TotalRequestGrantAmountCount < model.TotalApprovedProgramCount)
            {
                return RedirectToAction("PreviousSubProgramList", new { @id = 0 });
            }

            else if (model.TotalSubmitedProgressReportCount < model.TotalApprovedProgramCountForProgressRPT)
            {
                return RedirectToAction("ProgressDetailList");
            }

            else
            {
                return RedirectToAction("ListSupProgram");
            }


        }

        public ActionResult DisplayValidationRule(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.TotalApprovedProgramCount = CommontUtilities.TotalApprovedMultiYearProgramByGrantType(model.OfficeId, id);
            model.TotalRequestGrantAmountCount = CommontUtilities.TotalRequestedGrantAmountByOffice(model.OfficeId, id);
            model.TotalSubmitedProgressReportCount = CommontUtilities.Validation_TotalSubmitdQrdTillDateByOfficeId(model.OfficeId, id);
            model.ViewbagGrantTypeId = id;

            model.TotalApprovedProgramCountForProgressRPT = CommontUtilities.ValidationRule_ProgressReport(model.OfficeId, id);





            if (model.TotalSubmitedProgressReportCount < model.TotalApprovedProgramCountForProgressRPT)
            {
                return RedirectToAction("ProgressDetailList");
            }
            else if (model.TotalRequestGrantAmountCount < model.TotalApprovedProgramCount)
            {
                return RedirectToAction("PreviousSubProgramList", new { @id = 0 });
            }

            else
            {
                return RedirectToAction("ListSupProgram");
            }


        }


        public ActionResult ReqestGrantAmountFromList(int id, int id1)
        {
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ListSubProgram", "SubProgramSetup", new { @id = id1 });
            }
            else
            {
                RequestGrantAmountModel model = new RequestGrantAmountModel();
                SubProgramMaster Submodel = new SubProgramMaster();
                Submodel = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
                int TimeDureation = Submodel.TimeDurationYear;
                int? FiscalYearId = Submodel.FiscalYearId;

                model.FiscalYearId = Convert.ToInt32(FiscalYearId);
                model.ProgramTimeDuration = TimeDureation;
                model.ProgramId = id;
                model.ViewBagGrantTypeId = id1;
                model.ProgramTitle = Submodel.SubProgramTitle;
                model.TotalBudgetForProgram = Submodel.TotalBudget;
                model.ProgramPhaseNumber = Submodel.PhaseStatus;
                model.ApprovedStatusBool = Submodel.ApprovedStatus;


                RequestGrantAmountService Reqservice = new RequestGrantAmountService();
                int CurrentFiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
                //CurrentFiscalYearId = CurrentFiscalYearId + 1;//New code 12 dec 2021 
                //CurrentFiscalYearId = CurrentFiscalYearId;//New code 12 dec 2021 
                CurrentFiscalYearId = 16;
                model = Reqservice.PopulateGrantRequestAmountDetail(model.OfficeId, model.ProgramId, CurrentFiscalYearId).SingleOrDefault(x => x.ProgramId == id);
                if (model == null)
                {
                    model = new RequestGrantAmountModel();
                    model.ViewBagGrantTypeId = id1;
                    model.FiscalYearId = Convert.ToInt32(FiscalYearId);
                    model.ProgramTimeDuration = TimeDureation;
                    model.ProgramId = id;
                    model.ProgramTitle = Submodel.SubProgramTitle;
                    model.TotalBudgetForProgram = Submodel.TotalBudget;
                    model.ProgramPhaseNumber = Submodel.PhaseStatus;
                    model.ApprovedStatusBool = Submodel.ApprovedStatus;

                }
                else
                {
                    if (model.AmountSecond.HasValue)
                    {
                        if (model.AmountSecond > 0)
                        {
                            model.AmountSecond = model.AmountSecond / 100000;
                        }
                    }


                    if (model.ChaluAmount.HasValue)
                    {
                        if (model.ChaluAmount > 0)
                        {
                            model.ChaluAmount = model.ChaluAmount / 100000;
                        }
                    }

                    if (model.PujiAmount.HasValue)
                    {
                        if (model.PujiAmount > 0)
                        {
                            model.PujiAmount = model.PujiAmount / 100000;
                        }
                    }

                    if (model.ContractAmount.HasValue)
                    {
                        if (model.ContractAmount > 0)
                        {
                            model.ContractAmount = model.ContractAmount / 100000;
                        }
                    }
                }
                model.FiscalYearId = Convert.ToInt32(FiscalYearId);
                model.ProgramTimeDuration = TimeDureation;
                model.ProgramId = id;
                model.ViewBagGrantTypeId = id1;
                model.ProgramTitle = Submodel.SubProgramTitle;
                model.TotalBudgetForProgram = Submodel.TotalBudget;
                model.ProgramPhaseNumber = Submodel.PhaseStatus;
                model.ApprovedStatusBool = Submodel.ApprovedStatus;

                return View(model);
            }
        }


        [HttpPost]
        public ActionResult ReqestGrantAmountFromList(RequestGrantAmountModel model)
        {

            //check if program is not approved by admin

            RequestGrantAmountService services = new RequestGrantAmountService();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int FiscalYerId = CommontUtilities.GetCurrentFiscalYearId();
            //model.FiscalYearId = FiscalYerId;
            //new code 20
            //model.FiscalYearId = FiscalYe21 dec 12rId + 1;
            model.FiscalYearId = 16;

            if (model.ViewBagGrantTypeId == 2)
            {
                if (!model.AmountSecond.HasValue)
                {

                    TempData["Notifications"] = "राख्नुभएको रकम मिलेन  ।";
                }
                else
                {
                    decimal TotalApprovedAmount = CommontUtilities.GetSumProgramWiseAmount(model.ProgramId);
                    if (TotalApprovedAmount > 0)
                    {
                        TotalApprovedAmount = TotalApprovedAmount / 100000;
                    }
                    decimal RequestedAmount = 0;
                    RequestedAmount = Convert.ToDecimal(model.AmountSecond);
                    decimal BudgetForProgram = decimal.Round(model.TotalBudgetForProgram);
                    if (BudgetForProgram > 0)
                    {
                        BudgetForProgram = BudgetForProgram / 100000;
                    }
                    decimal ToCheckAmount = decimal.Round(TotalApprovedAmount + RequestedAmount);
                    if ((ToCheckAmount > BudgetForProgram) == true)
                    {

                        TempData["Notifications"] = "अनुदान माग गर्न लागिएको रकम कुल लागत रकम भन्दा धेरै भयो ।";

                    }
                    else
                    {
                        if (model.AmountSecond > 0)
                        {
                            model.AmountSecond = model.AmountSecond * 100000;
                        }
                        string ReturnMessage = services.InsertRequestDemandAmount(model);
                        TempData["Notifications"] = "अनुदान माग गरिएको रकम पेश भयो ।";

                    }


                }
            }
            else
            {
                if (!model.ChaluAmount.HasValue && !model.PujiAmount.HasValue)
                {

                    TempData["Notifications"] = "राख्नुभएको रकम मिलेन  ।";
                }
                else
                {
                    decimal TotalApprovedAmount = CommontUtilities.GetSumProgramWiseAmount(model.ProgramId);
                    if (TotalApprovedAmount > 0)
                    {
                        TotalApprovedAmount = TotalApprovedAmount / 100000;
                    }
                    decimal RequestedAmount = 0;
                    RequestedAmount = Convert.ToDecimal(model.AmountSecond);
                    decimal BudgetForProgram = decimal.Round(model.TotalBudgetForProgram);
                    if (BudgetForProgram > 0)
                    {
                        BudgetForProgram = BudgetForProgram / 100000;
                    }
                    decimal ToCheckAmount = decimal.Round(TotalApprovedAmount + RequestedAmount);
                    if ((ToCheckAmount > BudgetForProgram) == true)
                    {

                        TempData["Notifications"] = "अनुदान माग गर्न लागिएको रकम कुल लागत रकम भन्दा धेरै भयो ।";

                    }
                    else
                    {
                        if (model.ChaluAmount.HasValue)
                        {
                            if (model.ChaluAmount > 0)
                            {
                                model.ChaluAmount = model.ChaluAmount * 100000;
                            }
                        }
                        if (model.PujiAmount.HasValue)
                        {
                            if (model.PujiAmount > 0)
                            {
                                model.PujiAmount = model.PujiAmount * 100000;
                            }
                        }
                        if (model.ContractAmount.HasValue)
                        {
                            if (model.ContractAmount > 0)
                            {
                                model.ContractAmount = model.ContractAmount * 100000;
                            }
                        }
                        model.AmountSecond = model.ChaluAmount + model.PujiAmount;

                        string ReturnMessage = services.InsertRequestDemandAmount(model);
                        TempData["Notifications"] = "अनुदान माग गरिएको रकम पेश भयो ।";

                    }


                }
            }



            return RedirectToAction("ListSubProgram", new { id = model.ViewBagGrantTypeId });
        }




        public ActionResult ApplicationStatusDetails(int id, int id1)//appid, grantTypeId
        {
            var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("ProgressDetailList");
            }
            



            GrantAppDBEntities db = new GrantAppDBEntities();
            int FYID = CommontUtilities.GetCurrentFiscalYearId();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            ApplicationCompletionStatus acs = new ApplicationCompletionStatus();

            acs = db.ApplicationCompletionStatus.Where(x => x.ApplicationId == id && x.OfficeId == OfficeId).FirstOrDefault();

            if(acs != null)
            {
                TempData["ProgresReportNtf"] = @"प्रगति विवरण अद्यावधिक भईसकेको छ । ";
                return View("ApplicationStatusView",acs);
               // return RedirectToAction("ProgressDetailList");
            }

            //first check if this applcation all progress report submited or not....
            int TotalReportsToSubmit = 0; //CommontUtilities.ValidationRule_ProgressReport(OfficeId);
            int duration = 0;//db.SubProgramMaster.Where(x => x.SubProgramId == id && x.OfficeId == OfficeId).FirstOrDefault().TimeDurationYear;
            bool checkVal = true;//CommontUtilities.ShowHideApplicationCompletionStatus(id, duration);
           // checkVal = true;

            if (checkVal == false)
            {

                int PhaseNumberFromAppId = CommontUtilities.GetPhaseNumberFromProgramId(id);
                int CheckCondtionForPhase = CommontUtilities.GetCurrentProgramPhaseNumber() - 1;
                int checkcondtionForPhasePrevious = CommontUtilities.GetCurrentProgramPhaseNumber() - 2;//time duration greater than 2
                int checkcondtionFor3Years = CommontUtilities.GetCurrentProgramPhaseNumber() - 3;//time duration greater than 2

                if (PhaseNumberFromAppId == CheckCondtionForPhase)
                {


                    TempData["ProgresReportNtf"] = @"तपाँईले भर्नुभएको विवरण हाल मसान्त सम्मको मात्र डाटा भएको ले सम्पन्नको अवस्था भर्न मिल्दैन । ";


                }
                else if (checkcondtionForPhasePrevious == PhaseNumberFromAppId && duration >= 2)
                {
                    TempData["ProgresReportNtf"] = @"तपाँईले भर्नुभएको विवरण हाल मसान्त सम्मको मात्र डाटा भएको ले सम्पन्नको अवस्था भर्न मिल्दैन । ";

                }
                else if (checkcondtionFor3Years == PhaseNumberFromAppId && duration > 2)
                {
                    TempData["ProgresReportNtf"] = @"तपाँईले भर्नुभएको विवरण हाल मसान्त सम्मको मात्र डाटा भएको ले सम्पन्नको अवस्था भर्न मिल्दैन । ";

                }
                else
                {
                    TempData["ProgresReportNtf"] = @"कृपया यो आयोजनाको सबै आ.व. को प्रगति विवरण भर्नुहोस । ";

                }
                return RedirectToAction("ProgressDetailList");
            }

           // acs = db.ApplicationCompletionStatus.Where(x => x.ApplicationId == id && x.FiscalYearId == FYID && x.OfficeId == OfficeId).FirstOrDefault();
   

            ViewBag.Mode = "Edit";
            if (acs == null)
            {
                acs = new ApplicationCompletionStatus();
                acs.ApplicationCompletionStatusId = 0;
                ViewBag.Mode = "Create";
            }

            acs.ApplicationId = id;
            acs.GrantTypeID = id1;
            acs.FiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
            return View(acs);
        }

        [HttpPost]
        public ActionResult ApplicationStatusDetails(ApplicationCompletionStatus model)
        {
            model.CreatedDate = DateTime.Now;
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.Status = false;
            if (model.ApplicationCompletionStatusId > 0)
            {
                ViewBag.Mode = "Edit";
            }
            else
            {
                ViewBag.Mode = "Create";
            }

            GrantAppDBEntities db = new GrantAppDBEntities();
            model.FiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
            //if (db.ApplicationCompletionStatus.Any(x => x.FiscalYearId == model.FiscalYearId && x.ApplicationId == model.ApplicationId))
            //{
            //    ViewBag.ErrorMessage = "विवरण पेश भैसकेको छ । ";
            //    return View(model);
            //}


            if (model.CompletionStatusId == 0)
            {
                ViewBag.ErrorMessage = "कृपया सम्पन्नताको अवस्था छनोट गर्नुहोस। ";
                return View(model);
            }
            else if (model.CompletionStatusId == 1)//सम्पन्न
            {
                if (model.ApplicationCompletionStatusId > 0)
                {
                    string UploadFileUrlForCompletionStr = model.UploadFileUrlForCompletion == null ? string.Empty : model.UploadFileUrlForCompletion.FileName;
                    if (string.IsNullOrEmpty(UploadFileUrlForCompletionStr) == false)
                    {
                        string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        model.UploadFileUrl = Path.GetFileName(PrifixLetter + "_" + model.UploadFileUrlForCompletion.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.UploadFileUrl);
                        model.UploadFileUrlForCompletion.SaveAs(path);

                    }
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Notifications"] = "आयोजनाको सम्पन्नताको अवस्था सुरक्षित भयो ।";
                    return RedirectToAction("ProgressDetailList");


                }
                else
                {
                    string UploadFileUrlForCompletionStr = model.UploadFileUrlForCompletion == null ? string.Empty : model.UploadFileUrlForCompletion.FileName;
                    if (string.IsNullOrEmpty(UploadFileUrlForCompletionStr) == false)
                    {

                        string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        model.UploadFileUrl = Path.GetFileName(PrifixLetter + "_" + model.UploadFileUrlForCompletion.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.UploadFileUrl);
                        model.UploadFileUrlForCompletion.SaveAs(path);

                        db.ApplicationCompletionStatus.Add(model);
                        db.SaveChanges();
                        TempData["Notifications"] = "आयोजनाको सम्पन्नताको अवस्था सुरक्षित भयो ।";
                        return RedirectToAction("ProgressDetailList");
                    }
                    else
                    {
                        int? AppId = model.ApplicationId;
                        int? GrantTypeId = model.GrantTypeID;
                        model.CompletionStatusId = 0;

                        ViewBag.ErrorMessage = "कृपया फाईल अपलोड गर्नुहोस् । फाईल अपलोड विना फाराम सुरक्षित हुदैन ।";
                        return View(model);
                    }
                }

            }
            else if (model.CompletionStatusId == 2)//अपुरो माग रकम र वर्ष
            {
                if (model.ApplicationCompletionStatusId > 0)
                {
                    string NotCompletedStr = model.NotCompletedFileTypeStrFile == null ? string.Empty : model.NotCompletedFileTypeStrFile.FileName;
                    if (string.IsNullOrEmpty(NotCompletedStr) == false)
                    {
                        string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        model.NotCompletedFileTypeStr = Path.GetFileName(PrifixLetter + "_" + model.NotCompletedFileTypeStrFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.NotCompletedFileTypeStr);
                        model.NotCompletedFileTypeStrFile.SaveAs(path);

                    }
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Notifications"] = "आयोजनाको सम्पन्नताको अवस्था सुरक्षित भयो ।";
                    return RedirectToAction("ProgressDetailList");

                }

                else
                {
                    string NotCompletedStr = model.NotCompletedFileTypeStrFile == null ? string.Empty : model.NotCompletedFileTypeStrFile.FileName;
                    if (string.IsNullOrEmpty(NotCompletedStr) == false)
                    {
                        string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        model.NotCompletedFileTypeStr = Path.GetFileName(PrifixLetter + "_" + model.NotCompletedFileTypeStrFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.NotCompletedFileTypeStr);
                        model.NotCompletedFileTypeStrFile.SaveAs(path);

                        db.ApplicationCompletionStatus.Add(model);
                        db.SaveChanges();
                        TempData["Notifications"] = "आयोजनाको सम्पन्नताको अवस्था सुरक्षित भयो ।";
                        return RedirectToAction("ProgressDetailList");
                    }
                    else
                    {
                        int? AppId = model.ApplicationId;
                        int? GrantTypeId = model.GrantTypeID;
                        model.CompletionStatusId = 0;

                        ViewBag.ErrorMessage = "कृपया फाईल अपलोड गर्नुहोस् । फाईल अपलोड विना फाराम सुरक्षित हुदैन ।";
                        return View(model);
                    }
                }

            }
            else//droped project
            {
                if (model.ApplicationCompletionStatusId > 0)
                {
                    string UploadFileUrlForCompletionStr = model.UploadFileUrlForDroppedProjectFile == null ? string.Empty : model.UploadFileUrlForDroppedProjectFile.FileName;
                    if (string.IsNullOrEmpty(UploadFileUrlForCompletionStr) == false)
                    {
                        string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        model.WorkCompletionPhoto1 = Path.GetFileName(PrifixLetter + "_" + model.UploadFileUrlForDroppedProjectFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.WorkCompletionPhoto1);
                        model.UploadFileUrlForDroppedProjectFile.SaveAs(path);
                    }
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Notifications"] = "आयोजनाको सम्पन्नताको अवस्था सुरक्षित भयो ।";
                    return RedirectToAction("ProgressDetailList");
                }
                else
                {
                    string UploadFileUrlForCompletionStr = model.UploadFileUrlForDroppedProjectFile == null ? string.Empty : model.UploadFileUrlForDroppedProjectFile.FileName;
                    if (string.IsNullOrEmpty(UploadFileUrlForCompletionStr) == false)
                    {

                        string PrifixLetter = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        model.WorkCompletionPhoto1 = Path.GetFileName(PrifixLetter + "_" + model.UploadFileUrlForDroppedProjectFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.WorkCompletionPhoto1);
                        model.UploadFileUrlForDroppedProjectFile.SaveAs(path);
                        db.ApplicationCompletionStatus.Add(model);
                        db.SaveChanges();
                        TempData["Notifications"] = "आयोजनाको सम्पन्नताको अवस्था सुरक्षित भयो ।";

                        return RedirectToAction("ProgressDetailList");

                    }
                    else
                    {
                        model.CompletionStatusId = 0;
                        ViewBag.ErrorMessage = "कृपया फाईल अपलोड गर्नुहोस् ।";
                        return View(model);
                    }
                }

            }


            return RedirectToAction("ProgressDetailList");
        }




    }
}