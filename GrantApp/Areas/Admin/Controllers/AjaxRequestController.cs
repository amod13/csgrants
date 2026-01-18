using GrantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrantApp.Areas.Admin.Controllers
{
    public class AjaxRequestController : Controller
    {
        // GET: Admin/AjaxRequest
        public ActionResult Index()
        {

            return View();
        }

        public class SelectListModel
        {
            public int Id { get; set; }

            public string Idstr { get; set; }
            public string Title { get; set; }

            public int ProvinceId { get; set; }
            public string ProvinceTitleNep { get; set; }
        }


        public ActionResult GetDistrictDDByProvinceId(int id)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select DistrcitCode as Idstr,DistrictNameNep as Title from DistrictSetup where ProvinceId='" + id + "' ").ToList();
                // ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDistrictDDByProvinceIdWithSelectValue(int id)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select DistrcitCode as Idstr,DistrictNameNep as Title from DistrictSetup where ProvinceId='" + id + "' ").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }







        public ActionResult GetVDCMUNDDByDistrictId(string id)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select VdcMunCode as Idstr, VdcMunNameNep as Title From VdcMun where DistrictCode='" + id + "'").ToList();
                // ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVDCMUNDDByDistrictIdWithDefaultValue(string id)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select VdcMunCode as Idstr, VdcMunNameNep as Title From VdcMun where DistrictCode='" + id + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult GetProgramListByMainSectionId(string id)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"SELECT ProgramId AS Id, ProgramName AS Title FROM ProgramSetup WHERE MainSectionId='" + id + "'").ToList();
                // ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVDCMUNDDByDistrictIdWithDefault(string id)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {

                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModel>(@"select VdcMunCode as Idstr, VdcMunNameNep as Title From VdcMun where DistrictCode='" + id + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0", Selected = true });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;


                return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
            }
        }






    }
}