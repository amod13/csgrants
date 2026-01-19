using DocumentFormat.OpenXml.EMMA;
using GrantApp.Models;
using GrantApp.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace GrantApp.Areas.VDCMUNLevel.Controllers
{
    [Authorize]
    public class GrantController : BaseController
    {
        private readonly GrantAppDBEntities _db = new GrantAppDBEntities();

        SubProgramServices services = new SubProgramServices();

        CommonServices commonServices = new CommonServices();

        public ActionResult Index(int id)
        {
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            var data = services.GetApplicationStatus(CurrentLoginUserOfficeId, id, CurrentPhaseNumber);
            if (data != null  )
            {
                if (data.Status == 1)
                {
                    return RedirectToAction("TotalRequests", new { id = id });
                }
                else if(data.Status == 2)
                {
                    return RedirectToAction("FormEntry", new { id = id });
                }
               
            }

            NewProgramInitiation model = new NewProgramInitiation();
            model.GrantType = id;
            return View(model);
        }


        public PartialViewResult GetRequestFile(int id)
        {
            int officeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int phaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            SubProgramMaster model = new SubProgramMaster();
            model.NewProgramInitiationModel = _db.NewProgramInitiation
                .FirstOrDefault(x => x.OfficeId == officeId
                                     && x.GrantType == id
                                     && x.ProjectPhase == phaseNumber);

            if (model.NewProgramInitiationModel == null)
            {
                // 
                // Return an empty partial view or an error message
                return PartialView("_EmptyRequestFile");
            }
            model.NewProgramInitiationModel.RequestFileDoc = "/RequiredDocs/" + model.NewProgramInitiationModel.RequestFileDoc;
            return PartialView("_RequestFileModal", model);
        }

        public PartialViewResult UploadFilePartial(int id)
        {
            //  var model = new NewProgramInitiation(); // Load any required data here if needed
            // model.GrantType = id;

            int officeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int phaseNuber = CommontUtilities.GetCurrentProgramPhaseNumber();
            var pu = _db.NewProgramInitiation
                    .FirstOrDefault(x => x.OfficeId == officeId
                                      && x.GrantType == id
                                      && x.ProjectPhase == phaseNuber);
            if (pu != null && pu.RequestFileDoc != null)
            {
                if (pu.Status == 1)
                {
                    NewProgramInitiation model = new NewProgramInitiation();
                    model.OfficeId = pu.OfficeId;
                    model.GrantType = id;
                    return PartialView("_UploadFile", model);
                }
            }
            else
            {
                return PartialView("_UploadFileError");

            }

                return PartialView("_UploadFileError");

            
        }

        [HttpPost]
        public ActionResult UploadFile(NewProgramInitiation model, HttpPostedFileBase file)
        {

            if (file == null || !(file.ContentType.Contains("image") || file.FileName.EndsWith(".pdf")))
            {
                TempData["ErrorMessage"] = "Invalid file format. Only images and PDFs are allowed.";
                return RedirectToAction("Index");
            }

            try
            {
                // Generate a unique file name to avoid overwriting
                string uniqueFileName = $"{Guid.NewGuid()}_{System.IO.Path.GetFileName(file.FileName)}";
                string path = System.IO.Path.Combine(Server.MapPath("~/RequiredDocs"), uniqueFileName);
                file.SaveAs(path);
                model.Status = 1;
                model.RequestFileDoc = uniqueFileName;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "File upload failed: " + ex.Message;
                return RedirectToAction("Index");
            }

            if (model != null)
            {
                model.ProjectPhase = CommontUtilities.GetCurrentProgramPhaseNumber();
                model.FiscalYearId = 18;
                model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            }
            int officeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int phaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

            // Check if a record already exists for the given criteria
            var pu = _db.NewProgramInitiation
                        .FirstOrDefault(x => x.OfficeId == officeId
                                          && x.GrantType == model.GrantType
                                          && x.ProjectPhase == phaseNumber);
            int update = 0;
            if (pu != null)
            {
                update = 1;
                // Update existing record
                pu.UpdatedDate = DateTime.Now;
                pu.RequestFileDoc  = model.RequestFileDoc;
                _db.Entry(pu).State = EntityState.Modified;
            }
            else
            {
                // Save new record
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = 1;
                _db.NewProgramInitiation.Add(model);
            }

            try
            {
                _db.SaveChanges();
                if(update > 0)
                {
                    return RedirectToAction("FormEntry", new { id = model.GrantType });
                }
                else
                {
                    return RedirectToAction("TotalRequests", new { id = model.GrantType });
                }
             
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Database operation failed: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult TotalRequests(int id) //grant type
        {
            int officeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int phaseNuber = CommontUtilities.GetCurrentProgramPhaseNumber();
            var pu = _db.NewProgramInitiation
                    .FirstOrDefault(x => x.OfficeId == officeId
                                      && x.GrantType == id
                                      && x.ProjectPhase == phaseNuber );
            if(pu !=null && pu.RequestFileDoc!=null)
            {
                if(pu.TotalAppliedProject == null || pu.TotalAppliedProject ==0 )
                {
                    NewProgramInitiation model = new NewProgramInitiation();
                    model.OfficeId = pu.OfficeId;
                    model.GrantType = id;
                    ViewBag.SubProgramList = new SelectList(GetAvailableSubPrograms(id), "Value", "Text");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("FormEntry", new { id = id });
                }
               
            }
            else
            {
                return RedirectToAction("Index", new { id = id });
            }

        
        }


        public PartialViewResult TotalRequestsPartial(int id) //grant type
        {
            int officeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int phaseNuber = CommontUtilities.GetCurrentProgramPhaseNumber();
            var pu = _db.NewProgramInitiation
                    .FirstOrDefault(x => x.OfficeId == officeId
                                      && x.GrantType == id
                                      && x.ProjectPhase == phaseNuber);
            if (pu != null && pu.RequestFileDoc != null)
            {
                if (pu.Status == null || pu.Status == 1)
                {
                    NewProgramInitiation model = new NewProgramInitiation();
                    model.OfficeId = pu.OfficeId;
                    model.GrantType = id;
                    ViewBag.SubProgramList = new SelectList(GetAvailableSubPrograms(id), "Value", "Text");
                    return PartialView("_TotalRequestsPartial", model);
                }
                else
                {
                    return PartialView("_TotalRequestsPartialError");
                }

            }
            else
            {
                return PartialView("_TotalRequestsPartialError");
            }


        }


        [HttpPost]
        public ActionResult TotalRequests(NewProgramInitiation model)
        {
            if(model != null && model.GrantType > 0)
            {

                int officeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                if (model.TotalAppliedProject > GetMaxAvailableSubPrograms(officeId))
                {
                    TempData["ErrorMessage"] = "operation failed: ";
                    return RedirectToAction("Index");
                }


                int phaseNuber = CommontUtilities.GetCurrentProgramPhaseNumber();
                int totalPRojects = CommontUtilities.TotalAppliedThisYear(officeId, model.GrantType);

                if ( totalPRojects > model.TotalAppliedProject)
                {
                    TempData["Notifications"] = "प्रविष्ट भएको आयोजना हटाएर पुन सच्चयाउनुहोस् ";
                    return RedirectToAction("FormEntry", new { id = model.GrantType });
                }

                var pu = _db.NewProgramInitiation
                        .FirstOrDefault(x => x.OfficeId == officeId
                                          && x.GrantType == model.GrantType
                                          && x.ProjectPhase == phaseNuber);
            

              

                if(pu !=null)
                {  
                    
                    pu.TotalAppliedProject = model.TotalAppliedProject;
                    pu.UpdatedDate = DateTime.Now;
                    pu.Status = 1;
                    _db.Entry(pu).State = EntityState.Modified;

                    try
                    {
                        _db.SaveChanges();
                       // int total = 2;
                       // ViewBag.SubProgramList = new SelectList(GetAvailableSubPrograms(model.GrantType), "Value", "Text");
                        return RedirectToAction("FormEntry", new { id = model.GrantType });
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = "Database operation failed: " + ex.Message;
                        return RedirectToAction("Index");
                    }


                 
                }
                else
                {
                    return RedirectToAction("Index");
                }
               
            }
            else
            {
                return RedirectToAction("TotalRequests", new { id = model.GrantType });
            }
           
        }

        public ActionResult FormEntry(int id, int ? id2)
        {
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            ViewBag.TotalRemainingApplication = CommontUtilities.RemainingNewApplicationThisyear(CurrentLoginUserOfficeId, id);
            ViewBag.WillApplyTotal = CommontUtilities.GetWillApplyTotalForThisYear(CurrentLoginUserOfficeId, id);
            ViewBag.SubProgramList = new SelectList(GetAvailableSubPrograms(id), "Value", "Text");
            SubProgramMaster model = new SubProgramMaster();

            var  data = services.GetApplicationStatus(CurrentLoginUserOfficeId, id, CurrentPhaseNumber);
            if(data != null)
            {
                ViewBag.ApplicationStatus = data.Status;
                ViewBag.FinalDocUrl = data.FinalFileDoc;
            }
         

            if (data == null || data.Status != 2 || data.FinalFileDoc == null)
            {
                model = services.PopulateSubProgram(id).SingleOrDefault(x => x.SubProgramId == id2 && x.OfficeId == CurrentLoginUserOfficeId );
            }

            if (model == null)
            {
                SubProgramMaster mdl = new SubProgramMaster();
          
                mdl.ViewbagGrantTypeId = id;
                
            
                mdl.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
                mdl.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
                mdl.SubProgramMasterListNotSubmited = services.SP_GetSubmitedProgramListbyType(id, 1, CurrentPhaseNumber, CurrentLoginUserOfficeId).ToList();
                mdl.DocumentsRequirementsViewModel = services.SPUP_PopulateDocRequirementList(id, CurrentLoginUserType);
                mdl.OfficeId = CurrentLoginUserOfficeId;
                mdl.GrantTypeId = id;
                return View(mdl);
            }
            else
            {

                model.ObjSupportingDocumentsModel = services.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
                model.ViewbagGrantTypeId = id;
                CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
                model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
                model.SubProgramMasterListNotSubmited = services.SP_GetSubmitedProgramListbyType(id, 1, CurrentPhaseNumber, CurrentLoginUserOfficeId).ToList();
                model.GrantTypeId = id;
                if (_db.DocumentRequirementsUpload.Where(x => x.SubprogramId == id2).Count() > 0)

                {
                    model.DocumentsRequirementsViewModel = services.SPUP_PopulateRequiredDocForEdit(id2??0, CurrentLoginUserType);

                }
                else
                {
                    model.DocumentsRequirementsViewModel = services.SPUP_PopulateDocRequirementList(id, CurrentLoginUserType);
                }

                model.OfficeId = CurrentLoginUserOfficeId;
                ViewBag.Mode = "Edit";
                // model.SelectedProgramIds = model.SelectedPrograms?.Split(',').Select(int.Parse).ToList();
                model.AvailablePrograms = _db.ProgramSetup
                                           .Where(p => p.MainSectionId == model.MainSectionId) // Filter by MainSectionId
                                           .Select(p => new SelectListItem
                                           {
                                               Value = p.ProgramId.ToString(),
                                               Text = p.ProgramName
                                           })
                                           .ToList();

                // Append "Others" (9999) to the list
                model.AvailablePrograms.Add(new SelectListItem
                {
                    Value = "9999",
                    Text = "अन्य"
                });

              
                model.TotalBudget = model.TotalBudget / 100000;
                model.TotalBudgetBisesh = model.TotalBudget;
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
            }
            
            //var SubmissionDate = GetSubmissionDate();
            DateTime TodayDate = DateTime.Today;
            //if (TodayDate > SubmissionDate)
            //{
            //    TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
            //    return RedirectToAction("ListSubProgram", new { @id = id });
            //}
            //else
            //{
            //    return View(model);
            //}
            return View(model);
        }

        [HttpPost]
       public ActionResult FormEntry(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {

           


            if (model.TotalBudgetBisesh>0)
            {
                model.TotalBudget = model.TotalBudgetBisesh;
            }
            model = BindUploadedFilesToModel(model, files);
            int id = model.ViewbagGrantTypeId;
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
    
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            ViewBag.TotalRemainingApplication = CommontUtilities.RemainingNewApplicationThisyear(CurrentLoginUserOfficeId, id);



            model.ViewbagGrantTypeId = model.GrantTypeId;


            model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
            model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            model.SubProgramMasterListNotSubmited = services.SP_GetSubmitedProgramListbyType(id, 1, CurrentPhaseNumber, CurrentLoginUserOfficeId).ToList();
            model.DocumentsRequirementsViewModel = services.SPUP_PopulateDocRequirementList(id, CurrentLoginUserType);
            model.OfficeId = CurrentLoginUserOfficeId;
            model.GrantTypeId = id;
            model.ViewbagGrantTypeId = id;


            var data = services.GetApplicationStatus(CurrentLoginUserOfficeId, id, CurrentPhaseNumber);
            if (data != null)
            {
                ViewBag.ApplicationStatus = data.Status;
                ViewBag.FinalDocUrl = data.FinalFileDoc;
            }


            model.AvailablePrograms = _db.ProgramSetup
                                         .Where(p => p.MainSectionId == model.MainSectionId) // Filter by MainSectionId
                                         .Select(p => new SelectListItem
                                         {
                                             Value = p.ProgramId.ToString(),
                                             Text = p.ProgramName
                                         })
                                         .ToList();

            // Append "Others" (9999) to the list
            model.AvailablePrograms.Add(new SelectListItem
            {
                Value = "9999",
                Text = "अन्य"
            });


            // bisesh

            // napa gapa kul algata 25 lakh - 3 crore
            // upa ra maha 25 - 5 crore
            // pradesh 5 - 10 crore


            // samapurak 
            // 1- 7 napa gapa
            // 1- 15 upa maha 
            // pradesh 10 crore - 30 crore


            // bisehs
            // 1 barsa --- 90- 100 kul lagat ko
            // 2 barsa -- same complemetary --- kul lagat ko
            // 3 barsa -- same complemenrtary -- kul lagat 


            // pratamikikaran ko validation 

            // batti balne 




            if (model.SelectedProgramIds == null || !model.SelectedProgramIds.Any())
            {
                ModelState.AddModelError("SelectedProgramIds", "कम्तीमा एउटा कार्यक्रम चयन गर्नुहोस्।");
                ViewBag.ErrMode = "On";
                return View(model);
            }
            model.SelectedPrograms = string.Join(",", model.SelectedProgramIds);

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


            if (model.SubProgramId > 0)
            {
                int programPriority = services.GetPriorityofProgramById(model.SubProgramId, model.OfficeId, CurrentPhaseNumber);
                if(programPriority != model.ProgramPirority)
                {
                    if (services.CheckProgramPriorityAlreadyInserted(model.ViewbagGrantTypeId, model.OfficeId, CurrentPhaseNumber, model.ProgramPirority)) // office, phase
                    {
                        ViewBag.ErrorMessage = "आगामी आ.व.को लागि पेश गन खोजिएको र्आयोजनाको प्राथमिकीकरण पहिलेनै प्रणालीमा प्रविष्टि भैसकेको छ ।";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }

                }
            }
            else
            {

                if (services.CheckProgramPriorityAlreadyInserted(model.ViewbagGrantTypeId, model.OfficeId, CurrentPhaseNumber, model.ProgramPirority)) // office, phase
                {
                    ViewBag.ErrorMessage = "आगामी आ.व.को लागि पेश गन खोजिएको र्आयोजनाको प्राथमिकीकरण पहिलेनै प्रणालीमा प्रविष्टि भैसकेको छ ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }
            }


            

            if (model.ViewbagGrantTypeId == 1)
            {


                if (services.CheckTotalYearlyValueMatchOrNot(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear) == false)
                {
                    ViewBag.ErrorMessage = "नेपाल सरकारसँग माग गरिएको रकम जोड  कुल लागतको रकम भन्दा कम वा बराबर राख्नुहोस्  ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }

            }

            else
            {

                if (services.CheckTotalYearlyValueMatchOrNotComplementary(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear, model.AmountProvinceVdc) == false)
                {
                    ViewBag.ErrorMessage = " नेपाल सरकारसँग माग गरिएको रकम र प्रदेश वा स्थानिय तहले व्यहोर्ने रकमको जोड  कुल लागतको रकम भन्दा कम वा बराबर राख्नुहोस्  ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }



                if (!services.CheckTotalYearlyValueMatchOrNotForComplementry(model.TotalBudget, model.AmountProvinceVdc, model.OfficeId)) // budget , amount , office
                {
                    ViewBag.ErrorMessage = "सम्बन्धित तहले व्यहोर्ने रकम साझेदारी अनुपात भन्दा कम भयो ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }



                if (model.TimeDurationYear == 1)
                {
                    if (!services.CheckTotalYearlyValueMatchOrNotForComplementryFirstYear(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.AmountProvinceVdc))
                    {
                        ViewBag.ErrorMessage = "नेपाल सरकारले व्यहोर्ने रकमको (९०% - १००%) रकम पेश गनुहोस्";
                        ViewBag.ErrMode = "On";

                        return View(model);
                    }
                }
                else if (model.TimeDurationYear == 2)
                {
                    if (!services.CheckTotalYearlyValueMatchOrNotForComplementrySecondYear(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.AmountProvinceVdc))
                    {
                        ViewBag.ErrorMessage = "नेपाल सरकारले व्यहोर्ने रकमको पहिलो वर्ष (४०% - ६०%)  रकम पेश गनुहोस्";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }
                }
                else if (model.TimeDurationYear == 3)
                {
                    if (!services.CheckTotalYearlyValueMatchOrNotForComplementryThirdYear(model.TimeDurationYear, model.TotalBudget, model.BudgetForFirstYear, model.BudgetForSecondYear, model.BudgetForThirdYear, model.AmountProvinceVdc, model.NGOINGOAmount))
                    {
                        ViewBag.ErrorMessage = "नेपाल सरकारले व्यहोर्ने रकमको पहिलो वर्ष (२५% - ३०%), दोस्रो (३५% - ४०%)  रकम पेश गनुहोस्";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "अवधि ३ वर्षभन्दा बढी राख्न मिल्दैन";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }


            }

            if (model.SubProgramId > 0)
            {
                string  SubProgramTitle = services.GetProgramNameByProgramId(model.SubProgramId, model.OfficeId);
                if (SubProgramTitle != model.SubProgramTitle)
                {

                    if (services.CheckProgramNameAlreadyInserted(model.ProgramId, model.ViewbagGrantTypeId, model.OfficeId, model.SubProgramTitle) > 0)
                    {
                        ViewBag.ErrorMessage = "यो आयोजना/कार्यक्रम पहिले नै सिस्टममा छ ।";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }

                }
            }
            else
            {

                if (services.CheckProgramNameAlreadyInserted(model.ProgramId, model.ViewbagGrantTypeId, model.OfficeId, model.SubProgramTitle) > 0)
                {
                    ViewBag.ErrorMessage = "यो आयोजना/कार्यक्रम पहिले नै सिस्टममा छ ।";
                    ViewBag.ErrMode = "On";
                    return View(model);
                }
            }



            //check total value year1 year2 year3=total amount

            //check total amount validation....For gapanapa 1-10 crore, upmanapa-5-25 crore....pradesh 20-1 arba 1000000000

            //check total amount validation....For gapanapa 1-10 crore, upmanapa-5-25 crore....pradesh 10-30 karod
            if (model.ViewbagGrantTypeId == 1)  // biseshko lagi ceiling
            {
                if (model.ViewBagCurrentLoginUserUserTypeId == 2) // prdsh
                {
                    if (model.TotalBudget < 50000000 || model.TotalBudget > 100000000)
                    {
                        
                        ViewBag.ErrorMessage = "कुल लागत कम्तिमा ५ करोड वा बढीमा रू १० करोड हुनु पर्दछ ।";
                        return View(model);
                    }
                }
                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा
                    {
                        if (model.TotalBudget < 2500000 || model.TotalBudget > 30000000)//गापा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा  २५ लाख र बढीमा ३ करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)//nagarpalika
                    {
                        if (model.TotalBudget < 2500000 || model.TotalBudget > 30000000)//गापा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा  २५ लाख र बढीमा ३ करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }
                    }
                    else//upmanapa ra mahanagarpaliak
                    {
                        if (model.TotalBudget < 2500000 || model.TotalBudget > 50000000)//गापा
                        {
                            ViewBag.ErrorMessage = "कुल लागत कम्तिमा  २५ लाख र बढीमा ५ करोड सम्म हुनु पर्दछ ।";
                            return View(model);
                        }
                    }
                }

            }
            else
            {

                if (model.ViewBagCurrentLoginUserUserTypeId == 2)
                {
                    if (model.TotalBudget > 300000000 || model.TotalBudget < 100000000)
                    {
                        ViewBag.ErrorMessage = "कुल लागत १० करोड देखि ३० करोड सम्म हुनु पर्दछ ।";
                        ViewBag.ErrMode = "On";
                        return View(model);
                    }
                }

                else
                {
                    if (model.ViewBagCurrentOfficeType == 1)//गापा नपा
                    {
                        if (model.TotalBudget > 70000000 || model.TotalBudget < 10000000)
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि ७ करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }

                    }
                    else if (model.ViewBagCurrentOfficeType == 4)//गापा नपा
                    {
                        if (model.TotalBudget > 70000000 || model.TotalBudget < 10000000)
                        {
                            ViewBag.ErrorMessage = "कुल लागत १ करोड देखि ७ करोड सम्म हुनु पर्दछ  ।";
                            ViewBag.ErrMode = "On";
                            return View(model);
                        }
                    }

                    else
                    {
                        if (model.TotalBudget > 150000000 || model.TotalBudget < 30000000)//उपमनपा र मनपा
                        {
                            ViewBag.ErrorMessage = "कुल लागत ३ करोड देखि १५ करोड सम्म हुनु पर्दछ  ।";
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
            int Primarid = 0;
            model.GovnNepalAmount = Convert.ToDecimal(0);

            string validationMessage = commonServices.ValidateFiles(files);
            if (validationMessage != "Valid")
            {
                TempData["RequestGrantNotifications"] = validationMessage;
                ViewBag.ErrorMessage = " फाईल पुन अपलोड गर्नुहोस्  ।";
                ViewBag.ErrMode = "On";
                return View(model); // Return view with validation message
            }



            if (model.SubProgramId > 0)
            {
                
                string ReturnMessage = services.UpdateSubProgram(model);
            }
            else
            {
                Primarid = services.InsertSubProgram(model);
            }
           
           
            //int Primarid = 0;
            if (Primarid > 0)
            {
                model.SubProgramId = Primarid;
              //  TempData["Notifications"] = "डाटा सुरक्छित भयो ।";
            }
            else
            {
                //TempData["Notifications"] = "सिस्टममा केही समस्या देखियो । पुनह प्रयास गर्नुहोस ।";
            }

            //file


            //Comment this....
          


          



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
                        model.DocumentsRequirementsViewModel[index].UploadFileUrl = fileName;
                    }

                    index++;
                }
            }

            services.SP_UPDeleteRequiredDocumentsUpload(model.SubProgramId);
            // Save data to DB (Adjust this based on your DB structure)
            foreach (var item in model.DocumentsRequirementsViewModel)
            {
                services.SPUP_InsertProgramConditionDetails(item.RequiredDocID, model.SubProgramId, item.UploadFileUrl, 7);
            }

            model.ViewbagGrantTypeId = model.ViewbagGrantTypeId;


            //


            return RedirectToAction("FormEntry", new { id = model.ViewbagGrantTypeId });

        }


        public ActionResult ViewDetail(int id,int id1)
        {
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            ViewBag.TotalRemainingApplication = CommontUtilities.RemainingNewApplicationThisyear(CurrentLoginUserOfficeId, id);
            ViewBag.SubProgramList = new SelectList(GetAvailableSubPrograms(id), "Value", "Text");
            SubProgramMaster model = new SubProgramMaster();

           


           
            model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id && x.OfficeId == CurrentLoginUserOfficeId);
       

            if (model != null)
            {
                var data = services.GetApplicationStatus(CurrentLoginUserOfficeId, id1, CurrentPhaseNumber);
                if (data != null)
                {
                    ViewBag.ApplicationStatus = data.Status;
                    ViewBag.FinalDocUrl = data.FinalFileDoc;
                }



                model.ObjSupportingDocumentsModel = services.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
                model.ViewbagGrantTypeId = id1;
               
                model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
                model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
                model.SubProgramMasterListNotSubmited = services.SP_GetSubmitedProgramListbyType(id, 1, CurrentPhaseNumber, CurrentLoginUserOfficeId).ToList();
                model.GrantTypeId = id;

                try
                {
                    model.DocumentsRequirementsViewModel = services.SPUP_PopulateRequiredDocForEdit(id, 1);
                }
                catch (Exception)
                {

                    model.DocumentsRequirementsViewModel = new List<DocumentsRequirementsViewModel>();
                }

                model.OfficeId = CurrentLoginUserOfficeId;
             
                // model.SelectedProgramIds = model.SelectedPrograms?.Split(',').Select(int.Parse).ToList();
                model.AvailablePrograms = _db.ProgramSetup
                                           .Where(p => p.MainSectionId == model.MainSectionId) // Filter by MainSectionId
                                           .Select(p => new SelectListItem
                                           {
                                               Value = p.ProgramId.ToString(),
                                               Text = p.ProgramName
                                           })
                                           .ToList();

                // Append "Others" (9999) to the list
                model.AvailablePrograms.Add(new SelectListItem
                {
                    Value = "9999",
                    Text = "अन्य"
                });


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
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {

           

            if (model.SelectedProgramIds == null || !model.SelectedProgramIds.Any())
            {
                ModelState.AddModelError("SelectedProgramIds", "कम्तीमा एउटा कार्यक्रम चयन गर्नुहोस्।");
                ViewBag.ErrMode = "On";
                return View(model);
            }
            model.SelectedPrograms = string.Join(",", model.SelectedProgramIds);
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


            return RedirectToAction("FormEntry", new { @id = model.ViewbagGrantTypeId });
        }

        public ActionResult DeleteSubProgramMaster(int id, int id1) //id = subprogram id,  id1= grant type
        {
            var SubmissionDate = DateTime.ParseExact("13/03/2025", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime TodayDate = DateTime.Today;
            if (TodayDate > SubmissionDate)
            {
                TempData["Notifications"] = GrantApp.StaticValue.ConstantValues.SubmissionDateErrorMessage;
                return RedirectToAction("FormEntry", "Grant", new { @id = id1 });
            }
            else
            {
                //check if already request ....

                int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
                int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

                ViewBag.SubProgramList = new SelectList(GetAvailableSubPrograms(id), "Value", "Text");
                SubProgramMaster model = new SubProgramMaster();

                if (!services.IsApplicationEditable(CurrentLoginUserOfficeId, id1, CurrentPhaseNumber))
                {
                    return RedirectToAction("FormEntry", "Grant", new { @id = id1 });
                }

                else
                {

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

                }

               
                

                return RedirectToAction("FormEntry", "Grant", new { @id = id1 });
            }
        }

        public ActionResult GenerateAnusuchi2(int id)
        {

            
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int CurrentFiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
            AnusuchiViewModel model = new AnusuchiViewModel();
            model.OfficeName = CommontUtilities.GetVDCMUNNameByOfficeIdForLocalLevel(CurrentLoginUserOfficeId);
            model.AnusuchiData = services.PopulateAnusuchi(id, CurrentPhaseNumber, CurrentLoginUserOfficeId).ToList();
            return View(model);
        }


        public ActionResult GenerateAnusuchiBisesh(int id)
        {

            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int CurrentLoginUserType = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            int CurrentFiscalYearId = CommontUtilities.GetCurrentFiscalYearId();
            AnusuchiViewModel model = new AnusuchiViewModel();
            model.OfficeName = CommontUtilities.GetVDCMUNNameByOfficeIdForLocalLevel(CurrentLoginUserOfficeId);
            model.AnusuchiData = services.PopulateAnusuchi(id, CurrentPhaseNumber, CurrentLoginUserOfficeId).ToList();
            return View(model);
        }

       private string GeneratePdf(SubProgramMaster SubprogramMasters)
        {
            string filePath = System.IO.Path.Combine(Server.MapPath("~/RequiredDocs"), "GrantDetails.pdf");
            //Document doc = new Document();
            //PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            //doc.Open();
            //doc.Add(new Paragraph("Grant Details"));
            //foreach (var sp in SubprogramMasters)
            //{
            //    doc.Add(new Paragraph($"{sp.SubProgramTitle}"));
            //}
            //doc.Close();
            return filePath;
        }

        [HttpPost]
        public ActionResult UploadSignedPdf(SubProgramMaster model, HttpPostedFileBase file,  int[] SubProgramIds)
        {
            if (file == null || !(file.ContentType.Contains("image") || file.FileName.EndsWith(".pdf")))
            {
                TempData["Notifications"] = "Invalid file format. Only images and PDFs are allowed.";
                return RedirectToAction("FormEntry", new { id = model.GrantTypeId });
            }

             bool checkFileSavedCorrectly = services.CheckIfFileExists(SubProgramIds);
            
            if (!checkFileSavedCorrectly)
            {
                TempData["Notifications"] = "आयोजनाहरुको सबै फाईल अपलोड भएको छैन । कृपया रुजु गरि पुन प्रयास गर्नुहोस् ।";
                return RedirectToAction("FormEntry", new { id = model.GrantTypeId });
            }

            try
            {
                // Generate a unique file name to avoid overwriting
                string uniqueFileName = $"{Guid.NewGuid()}_{System.IO.Path.GetFileName(file.FileName)}";
                string path = System.IO.Path.Combine(Server.MapPath("~/RequiredDocs"), uniqueFileName);
                file.SaveAs(path);
                model.FinalDocumentsUrl = uniqueFileName;
                model.PhaseStatus = 7;
                var x = services.FinalDocUrlAndPeshToNpc(SubProgramIds,model);
                TempData["Notifications"] = "आयोजना वा कार्यक्रम पेश भएको छ । पुन आयोजना वा कार्यक्रम संशोधन गर्नु परेमा पेश गरिएको शर्तनामा प्रणालिबाट हटाए पश्चात मात्र संशोधन गर्न सक्नुहुनेछ  ।";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "File upload failed: " + ex.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Invalid file format!";

            return RedirectToAction("FormEntry", "Grant", new { @id = model.GrantTypeId });
        }

        [HttpPost]
        public ActionResult CancelSubmission(SubProgramMaster model, int[] SubProgramIds)
        {
            try
            {

                model.PhaseStatus = 7;

                var x = services.CancelSubmission(SubProgramIds, model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "File upload failed: " + ex.Message;
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Invalid file format!";

            return RedirectToAction("FormEntry", "Grant", new { @id = model.GrantTypeId });
        }

        private List<SelectListItem> GetAvailableSubPrograms(int id) // id = grant type
        {
            
            int officeId= GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int officeTypeId = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(officeId);
            //int officeTypeId = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(officeId);//VDCmun or MetroSubMetro
            int maxForOffice = 0;
            int kramagatApplied = CommontUtilities.GetKramagatAppliedByGrantTypeId(officeId,id);

            if (officeTypeId == 4) { maxForOffice = 3; }
            else if (officeTypeId == 2) { maxForOffice = 10; }
            else {  maxForOffice = 0; }
            int maxAllowed = Math.Max(maxForOffice - kramagatApplied, 0);
            return Enumerable.Range(1, maxAllowed).Select(i => new SelectListItem { Text =  $" कार्यालयको निर्णय बमोजिम {i} वटा नँया आयोजना वा कार्यक्रम पेश गर्नेछौ ।", Value = i.ToString() }).ToList();
        }



        private int GetMaxAvailableSubPrograms(int id) // id = grant type
        {

            int officeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int officeTypeId = CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(officeId);
            //int officeTypeId = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(officeId);//VDCmun or MetroSubMetro
            int maxForOffice = 0;
            int kramagatApplied = CommontUtilities.GetKramagatAppliedByGrantTypeId(officeId, id);

            if (officeTypeId == 4) { maxForOffice = 3; }
            else if (officeTypeId == 2) { maxForOffice = 10; }
            else { maxForOffice = 0; }
            int maxAllowed = Math.Max(maxForOffice - kramagatApplied, 0);
            return maxAllowed;
        }

        private SubProgramMaster BindUploadedFilesToModel(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null && files.Any(f => f != null && f.ContentLength > 0))
            {
                model.Files = new List<SubProgramFileViewModel>(); // Ensure list is initialized

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            model.Files.Add(new SubProgramFileViewModel
                            {
                                FileData = ms.ToArray(),  // Store file as byte array
                                FileName = file.FileName,
                                FileContentType = file.ContentType
                            });
                        }
                    }
                }
            }
            return model;
        }


        public JsonResult ValidateBudgetSamapurak(decimal TotalBudget)
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


        public JsonResult ValidateBudgetBisesh(decimal TotalBudgetBisesh)
        {
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            int officeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro

            if (officeType == 1 || officeType == 4) //gapa napa
            {
                if (TotalBudgetBisesh >= 25 && TotalBudgetBisesh <= 000)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json("कुल लागत २५ देखि ३०० सम्म राख्नुहोला ।", JsonRequestBehavior.AllowGet);
            }
            else if (officeType == 2 || officeType == 3)
            {
                if (TotalBudgetBisesh >= 25 && TotalBudgetBisesh <= 500)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json("कुल लागत २५ देखि ५०० सम्म राख्नुहोला ।", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (TotalBudgetBisesh >= 500 && TotalBudgetBisesh <= 1000)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json("कुल लागत ५०० देखि १००० सम्म राख्नुहोला ।", JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult DownLoadSamapurakAnusuchi1()
        {
            return File("~/Content/samapurak-anusuchi-1.pdf", "application/pdf");
        }

        public ActionResult DownLoadSamapurakAnusuchi5()
        {
            return File("~/Content/samapurak-anusuchi-5.pdf", "application/pdf");
        }


        public ActionResult DownLoadBiseshAnusuchi2()
        {
            return File("~/Content/Bisesh-anusuchi-2.pdf", "application/pdf");
        }


        public ActionResult DownLoadBiseshAnusuchi3()
        {
            return File("~/Content/Bisesh-anusuchi-3.pdf", "application/pdf");
        }

        public ActionResult DownLoadBiseshAnusuchi7()
        {
            return File("~/Content/Bisesh-anusuchi-7.pdf", "application/pdf");
        }


    }
}