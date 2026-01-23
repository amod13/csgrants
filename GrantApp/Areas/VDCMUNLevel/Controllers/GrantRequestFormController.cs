using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;
using GrantApp.Services;
using System.IO;
using System.Globalization;
using iText.Kernel.Pdf.Annot;

namespace GrantApp.Areas.VDCMUNLevel.Controllers
{
    [Authorize]
    public class GrantRequestFormController : BaseController
    {

        SubProgramServices SubProgramServices = new SubProgramServices();
        SubProgramMaster SubProgramModel = new SubProgramMaster();
        // GET: Admin/GrantRequestForm

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
        public ActionResult Index()
        {
            SubProgramModel.SubProgramListModelForAdminList = SubProgramServices.PopulateSubProgramListForAdmin(1);
            SubProgramModel.ViewbagGrantTypeId = 1;
            return View(SubProgramModel);

        }
        public ActionResult IndexComplementary()
        {
            SubProgramModel.SubProgramListModelForAdminList = SubProgramServices.PopulateSubProgramListForAdmin(2);
            SubProgramModel.ViewbagGrantTypeId = 2;
            return View(SubProgramModel);

        }


        public ActionResult ViewDeatils(int id, int id1)
        {
            SubProgramModel = SubProgramServices.PopulateSubProgram(0).SingleOrDefault(x => x.SubProgramId == id);
            SubProgramModel.ObjSupportingDocumentsModel = SubProgramServices.PopulateSubProgramSupportingDoc(id).SingleOrDefault();


            SubProgramModel.PopulatePointsVariableListViewModelList = SubProgramServices.PopulatePointsVariableList(id);

            //var path = Path.Combine(Server.MapPath("~/RequiredDocs"));
            //string myFilePath = path + SubProgramModel.ObjSupportingDocumentsModel.FeasibilitiesStudyFile;
            //string ext = Path.GetExtension(myFilePath);

            return View(SubProgramModel);
        }


        [HttpPost]
        public ActionResult SavePoints(SubProgramMaster model)
        {
            ProgramPointsServices pointService = new ProgramPointsServices();
            string msg = pointService.InsertProgramPoints(model);
            return RedirectToAction("ViewDeatils", new { id = model.SubProgramId, id1 = model.GrantTypeId });
        }


        public FileResult Download(string FileName)
        {
            var FileVirtualPath = "~/RequiredDocs/" + FileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        public FileResult ShowFile(string fileName)
        {
            var virtualPath = "~/RequiredDocs/" + fileName;
            var physicalPath = Server.MapPath(virtualPath);

            string contentType = MimeMapping.GetMimeMapping(fileName);

            return File(virtualPath,  fileName);
        }

        public ActionResult ProposalValuation(int id, int id1)
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
                SubProgramMaster _Model = new SubProgramMaster();
                ProgramPointsServices ObjService = new ProgramPointsServices();
                _Model.GrantTypeId = id1;
                _Model.SubProgramId = id;
                int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
                if (ObjService.CheckIfProgramValuationBasisExist(_Model.SubProgramId, _Model.GrantTypeId, OfficeId))
                {

                    _Model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(_Model.SubProgramId, _Model.GrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
                    ViewBag.Mode = "Edit";
                }
                else
                {
                    _Model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetail(id, id1).Where(x => x.IsSystemGenerated == false).ToList();
                }


                _Model.ViewbagGrantTypeId = id1;
                SubProgramServices service = new SubProgramServices();
                _Model.Status = service.GetCurrentStatusOfSubProgram(_Model.SubProgramId);

                return View(_Model);
            }
        }


        [HttpPost]
        public ActionResult ProposalValuation(SubProgramMaster _Model)
        {
            if (_Model.ValuationBasisModelList.Count > 0)
            {

                ProgramPointsServices ObjService = new ProgramPointsServices();
                ObjService.InsertSubProgramVariableBasis(_Model);
                _Model.ValuationBasisModelList = ObjService.PopulateVariableBasisDetailEdit(_Model.SubProgramId, _Model.GrantTypeId).Where(x => x.IsSystemGenerated == false).ToList();
            }
            return RedirectToAction("FifteenYojana", "SubProgramSetup", new { id = _Model.SubProgramId, id1 = _Model.GrantTypeId });
            //return RedirectToAction("CheckListActionUpload", "SubProgramSetup", new { id = _Model.SubProgramId, id1 = _Model.GrantTypeId });
            //return RedirectToAction("ProposalValuation", new { id = _Model.SubProgramId, id1 = _Model.GrantTypeId });
        }



        public ActionResult ProposalValuationList(int id)
        {
            SubProgramMaster _Model = new SubProgramMaster();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            int OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            _Model.ValuationBasisModelList = ObjService.PopulateProposalValuationList(0, id, OfficeId).Where(x => x.IsSystemGenerated == false).ToList();
            return View(_Model);
        }

        //check condtion 
        public ActionResult ProposalValuationResultList(int id)
        {
            SubProgramMaster _Model = new SubProgramMaster();
            ProgramPointsServices ObjService = new ProgramPointsServices();
            _Model.ValuationBasisModelList = ObjService.PopulateProposalValuationResultList(id, 0, 0, 0, 1, 2);
            return View(_Model);
        }








    }
}