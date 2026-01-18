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
using System.IO;



namespace GrantApp.Areas.VDCMUNLevel.Controllers
{
    [Authorize]
    public class AdhuroGrantController : BaseController
    {
        // GET: VDCMUNLevel/AdhuroGrant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNotCompletedProjectList()
        {


            AdhuroApuroViewModel model = new AdhuroApuroViewModel();
            AdhuroApuroServices DS = new AdhuroApuroServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            model.GetNotCompletedProgramListByOfficeIdVMList = DS.SUP_PupulateAdhuroApuroProjects(OfficeId);

            return View(model);
        }


        public ActionResult GetBhuktaniRemainingProjectList()
        {


            AdhuroApuroViewModel model = new AdhuroApuroViewModel();
            AdhuroApuroServices DS = new AdhuroApuroServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            model.GetNotCompletedProgramListByOfficeIdVMList = DS.SUP_PupulateCompletedProjects(OfficeId);

            return View(model);
        }

        public ActionResult Create(int id, int id1) // id subprogram, id1 
        {
            AdhuroApuroServices DS = new AdhuroApuroServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            AdhuroApuroGrantRequest model = DS.SUP_AdhuroApuroGrantRequest(id, OfficeId)
                                 ?? new AdhuroApuroGrantRequest
                                 {
                                     OfficeId = OfficeId,
                                     ProgramId = id,
                                     PhaseStatus = 7
                                 };
            if (model != null)
            {
                model.AdditionalFundRequested = model.AdditionalFundRequested / 100000;
                model.AmountCoveredByLocalLevel = model.AmountCoveredByLocalLevel / 100000;
                model.TotalAllocation = model.TotalAllocation / 100000;
                model.TotalExpenditure = model.TotalExpenditure / 100000;
            }
        
            return View(model);

        }



        [HttpPost]
        public ActionResult Create(AdhuroApuroGrantRequest model, List<HttpPostedFileBase> files)
        {
            try
            {
                int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                model.OfficeId = OfficeId;
                AdhuroApuroServices DS = new AdhuroApuroServices();
                string uploadPath = Server.MapPath("~/RequiredDocs/");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }



                // File fields in model
                Dictionary<string, string> fileFields = new Dictionary<string, string>
                    {
                        { "ContractAgreementFilePath", model.ContractAgreementFilePath },
                        { "PaymentVoucherFilePath", model.PaymentVoucherFilePath },
                        { "ProjectImagesFilePath", model.ProjectImagesFilePath },
                        { "ExtensionLetterFilePath", model.ExtensionLetterFilePath },
                        { "CommitmentLetterFilePath", model.CommitmentLetterFilePath },
                        { "ExecutiveDecisionFilePath", model.ExecutiveDecisionFilePath }
                    };

                int index = 0;
                foreach (var fileKey in fileFields.Keys.ToList())
                {
                    if (files != null && index < files.Count && files[index] != null && files[index].ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(files[index].FileName);
                        string uniqueFileName = $"{OfficeId}_{Guid.NewGuid()}_{System.IO.Path.GetFileName(fileName)}";
                        string filePath = Path.Combine(uploadPath, uniqueFileName);
                        files[index].SaveAs(filePath);

                        // Save relative path in model
                        fileFields[fileKey] =  uniqueFileName;
                    }
                    index++;
                }

                // Assign file paths to model
                model.ContractAgreementFilePath = fileFields["ContractAgreementFilePath"];
                model.PaymentVoucherFilePath = fileFields["PaymentVoucherFilePath"];
                model.ProjectImagesFilePath = fileFields["ProjectImagesFilePath"];
                model.ExtensionLetterFilePath = fileFields["ExtensionLetterFilePath"];
                model.CommitmentLetterFilePath = fileFields["CommitmentLetterFilePath"];
                model.ExecutiveDecisionFilePath = fileFields["ExecutiveDecisionFilePath"];

                model.AdditionalFundRequested = model.AdditionalFundRequested * 100000;
                model.AmountCoveredByLocalLevel = model.AmountCoveredByLocalLevel * 100000;
                model.TotalAllocation = model.TotalAllocation * 100000;
                model.TotalExpenditure = model.TotalExpenditure * 100000;
       
                // Insert record
                string message = DS.InsertAdhuroApuroGrantRequest(model);

                if (message.ToLower().Contains("success"))
                {
                    TempData["Success"] = "Grant request submitted successfully!";
                    return RedirectToAction("GetNotCompletedProjectList");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
            }

            return View(model);
        }


        public ActionResult RequestForRemainingBhuktani(int id, int id1) // id subprogram, id1 
        {
            AdhuroApuroServices DS = new AdhuroApuroServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();

            RemainingBhuktaniGrantRequest model = DS.SUP_RemainingBhuktaniGrantRequest(id, OfficeId)
                                   ?? new RemainingBhuktaniGrantRequest
                                   {
                                       OfficeId = OfficeId,
                                       ProgramId = id,
                                       PhaseStatus = 7
                                   };
            if (model != null)
            {
                model.AdditionalAmountRequested = model.AdditionalAmountRequested / 100000;
                model.AmountToBeCoveredByLevel = model.AmountToBeCoveredByLevel / 100000;
                model.TotalAllocation = model.TotalAllocation / 100000;
                model.TotalExpenditure = model.TotalExpenditure / 100000;
            }
            return View(model);
        }




        [HttpPost]
        public ActionResult RequestForRemainingBhuktani(RemainingBhuktaniGrantRequest model, List<HttpPostedFileBase> files)
        {
            try
            {
                int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                model.OfficeId = OfficeId;
                AdhuroApuroServices DS = new AdhuroApuroServices();
                string uploadPath = Server.MapPath("~/RequiredDocs/");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // File fields in model
                Dictionary<string, string> fileFields = new Dictionary<string, string>
                    {
                        
                        { "ContractAgreementPath", model.ContractAgreementPath },
                        { "ExtensionLetterPath", model.ExtensionLetterPath },
                        { "PaymentVouchersPath", model.PaymentVouchersPath },
                        { "ProjectImagesPath", model.ProjectImagesPath },
       
                        { "LiabilityCertificationPath", model.LiabilityCertificationPath },
                        { "ExecutiveDecisionPath", model.ExecutiveDecisionPath }
                    };

                int index = 0;
                foreach (var fileKey in fileFields.Keys.ToList())
                {
                    if (files != null && index < files.Count && files[index] != null && files[index].ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(files[index].FileName);
                        string uniqueFileName = $"{OfficeId}_{Guid.NewGuid()}_{System.IO.Path.GetFileName(fileName)}";
                        string filePath = Path.Combine(uploadPath, uniqueFileName);
                        files[index].SaveAs(filePath);

                        // Save relative path in model
                        fileFields[fileKey] = uniqueFileName;
                    }
                    index++;
                }


                // Assign file paths to model
                model.ExtensionLetterPath = fileFields["ExtensionLetterPath"];
                model.ContractAgreementPath = fileFields["ContractAgreementPath"];
                model.PaymentVouchersPath = fileFields["PaymentVouchersPath"];
                model.ProjectImagesPath = fileFields["ProjectImagesPath"];
              
                model.LiabilityCertificationPath = fileFields["LiabilityCertificationPath"];
                model.ExecutiveDecisionPath = fileFields["ExecutiveDecisionPath"];


                model.AdditionalAmountRequested = model.AdditionalAmountRequested * 100000;
                model.AmountToBeCoveredByLevel = model.AmountToBeCoveredByLevel * 100000;
                model.TotalAllocation = model.TotalAllocation * 100000;
                model.TotalExpenditure = model.TotalExpenditure * 100000;

                // Insert record
                string message = DS.InsertRemainingBhuktaniGrantRequest(model);

                if (message.ToLower().Contains("success"))
                {
                    TempData["Success"] = "Grant request submitted successfully!";
                    return RedirectToAction("GetBhuktaniRemainingProjectList");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
            }

            return View(model);
        }


        private string SaveFile(HttpPostedFileBase file)
        {
            string filePath = "";
            if (file != null && file.ContentLength > 0)
            {
                string uploadDir = Server.MapPath("~/RequiredDocs/");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(uploadDir, fileName);
                file.SaveAs(filePath);
                return fileName; 
            }
            return filePath;
        }

        public FileResult Download(string FileName)
        {
            var FileVirtualPath = "~/RequiredDocs/" + FileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }


    }
}