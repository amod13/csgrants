using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;
using GrantApp.Services;
using System.IO;

namespace GrantApp.Areas.ProvinceLevel.Controllers
{
    [Authorize]
    public class SubProgramSetupController : Controller
    {


        SubProgramServices services = new SubProgramServices();
        // GET: ProvinceLevel/SubProgramSetup
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListSubProgram(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.SubProgramMasterList = services.PopulateSubProgram(id).Where(x => x.OfficeId == model.OfficeId).ToList();
            model.ViewbagGrantTypeId = id;
            return View(model);
        }




        public ActionResult Create(int id)
        {
            SubProgramMaster model = new SubProgramMaster();
            model.ViewbagGrantTypeId = id;
            int CurrentLoginUserOfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            model.ViewBagCurrentOfficeType = GrantApp.Areas.Admin.FunctionClass.GetMetroSubMetroTypeByOfficeId(CurrentLoginUserOfficeId);//VDCmun or MetroSubMetro
            model.ViewBagCurrentLoginUserUserTypeId = GrantApp.CommontUtilities.GetCurrentLoginUserUserTypeByOfficeId(CurrentLoginUserOfficeId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            //check if program already inserted or not.....
            if (model.TotalBudget > 1000000000)
            {
                ViewBag.ErrorMessage = "कुल लागत १ अर्ब भन्दा धेरै भयो ।";
                return View(model);
            }

            model.GrantTypeId = model.ViewbagGrantTypeId;
            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            string PrifixLetter = model.OfficeId + "-" + model.ProgramId;
            for (int i = 0; i < files.Count(); i++)
            {
                if (list[i] != null)
                {
                    if (list[i].ContentLength > 0 && i == 0)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.FeasibilitiesStudyFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.FeasibilitiesStudyFile);
                        file.SaveAs(path);
                    }
                    if (list[i].ContentLength > 0 && i == 1)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.SDSDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.SDSDocFile);
                        file.SaveAs(path);
                    }
                    if (list[i].ContentLength > 0 && i == 2)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.DecisionDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.DecisionDocFile);
                        file.SaveAs(path);
                    }

                    if (list[i].ContentLength > 0 && i == 3)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.EnvironmentDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.EnvironmentDocFile);
                        file.SaveAs(path);
                    }
                    if (list[i].ContentLength > 0 && i == 4)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.ExtraDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.ExtraDocFile);
                        file.SaveAs(path);
                    }

                    if (list[i].ContentLength > 0 && i == 5)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.NecessityForProgram = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.NecessityForProgram);
                        file.SaveAs(path);
                    }

                    if (list[i].ContentLength > 0 && i == 6)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.PhysicalAndManPower = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.PhysicalAndManPower);
                        file.SaveAs(path);
                    }

                    if (list[i].ContentLength > 0 && i == 7)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.YearlyProgramAndBudget = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.YearlyProgramAndBudget);
                        file.SaveAs(path);
                    }

                }

            }

            services.InsertSubProgram(model);
            return RedirectToAction("ListSubProgram", new { @id = model.ViewbagGrantTypeId });
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
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SubProgramMaster model, IEnumerable<HttpPostedFileBase> files)
        {
            model.GrantTypeId = model.ViewbagGrantTypeId;
            if (model.TotalBudget > 1000000000)
            {
                ViewBag.ErrorMessage = "कुल लागत १ अर्ब भन्दा धेरै भयो ।";
                return View(model);
            }


            IList<HttpPostedFileBase> list = (IList<HttpPostedFileBase>)files;
            HttpFileCollectionBase filesCollection = Request.Files;
            model.OfficeId = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            string PrifixLetter = model.OfficeId + "-" + model.ProgramId;
            for (int i = 0; i < files.Count(); i++)
            {
                if (list[i] != null)
                {
                    if (list[i].ContentLength > 0 && i == 0)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.FeasibilitiesStudyFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.FeasibilitiesStudyFile);
                        file.SaveAs(path);
                    }
                    if (list[i].ContentLength > 0 && i == 1)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.SDSDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.SDSDocFile);
                        file.SaveAs(path);
                    }
                    if (list[i].ContentLength > 0 && i == 2)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.DecisionDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.DecisionDocFile);
                        file.SaveAs(path);
                    }

                    if (list[i].ContentLength > 0 && i == 3)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.EnvironmentDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.EnvironmentDocFile);
                        file.SaveAs(path);
                    }
                    if (list[i].ContentLength > 0 && i == 4)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.ExtraDocFile = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.ExtraDocFile);
                        file.SaveAs(path);
                    }


                    if (list[i].ContentLength > 0 && i == 5)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.NecessityForProgram = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.NecessityForProgram);
                        file.SaveAs(path);
                    }

                    if (list[i].ContentLength > 0 && i == 6)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.PhysicalAndManPower = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.PhysicalAndManPower);
                        file.SaveAs(path);
                    }

                    if (list[i].ContentLength > 0 && i == 7)
                    {

                        HttpPostedFileBase file = filesCollection[i];
                        model.ObjSupportingDocumentsModel.YearlyProgramAndBudget = Path.GetFileName(PrifixLetter + "_" + list[i].FileName);
                        var path = Path.Combine(Server.MapPath("~/RequiredDocs"), model.ObjSupportingDocumentsModel.YearlyProgramAndBudget);
                        file.SaveAs(path);
                    }
                }

            }
            string ReturnMessage = services.UpdateSubProgram(model);
            return RedirectToAction("ListSubProgram", new { @id = model.ViewbagGrantTypeId });
        }

        public ActionResult ViewDetails(int id, int id1)//id sub program id, id1 grant type id
        {
            SubProgramMaster model = new SubProgramMaster();
            model = services.PopulateSubProgram(id1).SingleOrDefault(x => x.SubProgramId == id);
            model.ObjSupportingDocumentsModel = services.PopulateSubProgramSupportingDoc(id).SingleOrDefault();
            model.ViewbagGrantTypeId = id1;
            return View(model);
        }


        [HttpPost]
        public ActionResult RequestGrant(SubProgramMaster model)
        {

            services.UpdateSubProgramStatusSP(model.SubProgramId, model.ProgramId, model.FinalDocumentsUrl,model.TermsAndCondtions,2);
            return RedirectToAction("ViewDetails", new { id = model.SubProgramId, id1 = model.ViewbagGrantTypeId });
        }



        public ActionResult RequestForApproved(int id, int id1)
        {
            //Update status ......

            return View();
        }


    }
}