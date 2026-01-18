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
    public class PublicGrantController : BaseController
    {
        // GET: VDCMUNLevel/AdhuroGrant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProjectList()
        {
            PublicPrivateGrantViewModel model = new PublicPrivateGrantViewModel();
            PublicPrivateGrantServices DS = new PublicPrivateGrantServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.GetPublicPrivateGrantVMList = DS.SUP_PupulatePublicPrivateGrantProjects(OfficeId);
            return View(model);
        }


        public ActionResult Create() // id subprogram, id1 
        {

            PublicPrivateGrantServices DS = new PublicPrivateGrantServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int PhaseStatus = CommontUtilities.GetCurrentProgramPhaseNumber();

            
            if (DS.CheckIfOfficeHasAppliedForPublicGrantForPhase(OfficeId, PhaseStatus))
            {
                TempData["Notifications"] = "आयोजना वा कार्यक्रम पेश भैसकेको छ !";
                return RedirectToAction("GetProjectList");
            }
            

            PublicPrivateGrantRequest model = new PublicPrivateGrantRequest();
            return View(model);

        }



        [HttpPost]
        public ActionResult Create(PublicPrivateGrantRequest model, List<HttpPostedFileBase> files)
        {
            try
            {
                int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                int PhaseStatus = CommontUtilities.GetCurrentProgramPhaseNumber();
                model.PhaseStatus = PhaseStatus;
                model.OfficeId = OfficeId;
                PublicPrivateGrantServices DS = new PublicPrivateGrantServices();
                string uploadPath = Server.MapPath("~/RequiredDocs/");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                // File fields in model
                Dictionary<string, string> fileFields = new Dictionary<string, string>
                    {
                        { "FeasibilityStudy", model.FeasibilityStudy },
                        { "EnvironmentalReport", model.EnvironmentalReport },
                        { "PriorityDetails", model.PriorityDetails },
                        { "LocalGovtDecision", model.LocalGovtDecision },
                        { "DeclarationSchedule", model.DeclarationSchedule }
                      
                    };

                int index = 0;
                foreach (var fileKey in fileFields.Keys.ToList())
                {
                    if (files != null && index < files.Count && files[index] != null && files[index].ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(files[index].FileName);
                        string filePath = Path.Combine(uploadPath, fileName);
                        files[index].SaveAs(filePath);

                        // Save relative path in model
                        fileFields[fileKey] =  fileName;
                    }
                    index++;
                }

                // Assign file paths to model
                model.FeasibilityStudy = fileFields["FeasibilityStudy"];
                model.EnvironmentalReport = fileFields["EnvironmentalReport"];
                model.PriorityDetails = fileFields["PriorityDetails"];
                model.LocalGovtDecision = fileFields["LocalGovtDecision"];
                model.DeclarationSchedule = fileFields["DeclarationSchedule"];


           
                model.TotalCost = model.TotalCost * 100000;
                model.StateContribution = model.StateContribution * 100000;
                model.PrivateContribution = model.PrivateContribution * 100000;
                model.VGFFund = model.VGFFund * 100000;
                model.RequestedAmount = model.RequestedAmount * 100000;


                string message = "";
                if (model.Id>0)
                {

                    message = DS.UpdatePublicPrivateGrantRequest(model);
                }
                else
                {
                    message = DS.InsertPublicPrivateGrantRequest(model);
 


                }

                // Insert record
              

                if (message.ToLower().Contains("success"))
                {
                    TempData["Success"] = "Grant request submitted successfully!";
                    return RedirectToAction("GetProjectList");
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



        public ActionResult Edit(int id) // id subprogram, id1 
        {

            PublicPrivateGrantServices DS = new PublicPrivateGrantServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            int PhaseStatus = CommontUtilities.GetCurrentProgramPhaseNumber();
            PublicPrivateGrantRequest model = new PublicPrivateGrantRequest();
            if (id > 0)
            {
                model = DS.PopulatePublicPrivateGrantProjectById(id,OfficeId);
                if(model != null)
                {
                    model.TotalCost = model.TotalCost / 100000;
                    model.StateContribution = model.StateContribution / 100000;
                    model.PrivateContribution = model.PrivateContribution / 100000;
                    model.VGFFund = model.VGFFund / 100000;
                    model.RequestedAmount = model.RequestedAmount / 100000;

                    return View(model);
                }
                else{
                    TempData["Notifications"] = "केहि कुरा मिलेन !";
                    return RedirectToAction("GetProjectList");
                }
            }
            else
            {
                
                    TempData["Notifications"] = "केहि कुरा मिलेन !";
                    return RedirectToAction("GetProjectList");
                
            }

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