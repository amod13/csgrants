using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Text.RegularExpressions;

namespace GrantApp.Areas.Admin
{
    public class FunctionClass
    {

        public static int GetCurrentLoginProvinceId()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                try
                {
                    Guid UserId = GetCurrentUser();
                    string UserIdstr = UserId.ToString();
                    var Result = ent.Database.SqlQuery<SelectListModel>("GetCurrentLoginUserprovinceId {0}", UserIdstr).Single();
                    return Convert.ToInt32(Result.Id);

                }

                catch (Exception ex)
                {

                    return 0;
                }


            }

        }

        public static SelectList GetProvincesDD()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ProvinceId as Id, ProvinceTitleNep as Title From Province").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetProvincesDDWithoutSelect()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select ProvinceId as Id, ProvinceTitleNep as Title From Province").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static SelectList GetProvincesAsOffcieNameListDD()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select OfficeId as Id, OfficeName as Title from OfficeDetails where UserType=2 order by ProvincesId").ToList();
                //ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }



        public static SelectList GetDistrictByStateIdDD(int StateId)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select DistrcitCode as Idstr,DistrictNameNep as Title from DistrictSetup where ProvinceId='" + StateId + "' ").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }

        public static SelectList GetRuralMunicipalitybyDistrictDD(int DistrictId)
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select VdcMunCode as Idstr, VdcMunNameNep as Title From VdcMun where DistrictCode='" + DistrictId + "'").ToList(), "Idstr", "Title");
            }

        }
        public static SelectList GetRuralMunicipalitybyDistrictDDDefault(string DistrictCode)
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select VdcMunCode as Idstr,VdcMunNameEng as Title from VdcMun where DistrictCode='" + DistrictCode + "' ").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");

            }


        }


        public static SelectList GetCaste()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select CasteID as Id, CasteName as Title from Caste").ToList();
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }
        public static SelectList GetDistrict()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select DistrcitCode as Idstr,DistrictNameNep as Title from DistrictSetup").ToList();
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Idstr.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public static string GetProvinceNameByID(int ProvinceId)
        {
            if (ProvinceId > 0)
            {
                using (GrantAppDBEntities db = new GrantAppDBEntities())
                {
                    string ProvincesName = db.Database.SqlQuery<string>(@"select ProvinceTitleNep From Province where ProvinceId='" + ProvinceId + "'").FirstOrDefault();
                    return ProvincesName.ToString();

                }
            }
            else
            {
                return "";
            }


        }

        public static string GetDistrictNameByDistrctitCode(string DistrictCode)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string DistrictName = db.Database.SqlQuery<string>(@"select DistrictNameNep From DistrictSetup where DistrcitCode='" + DistrictCode + "'").FirstOrDefault();
                return DistrictName.ToString();

            }

        }

        public static string GetVDCNPByVDCCode(string VDCNPCode)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string VDCMUN = db.Database.SqlQuery<string>(@"select VdcMunNameNep From VdcMun where VdcMunCode='" + VDCNPCode + "'").FirstOrDefault();
                return VDCMUN.ToString();

            }

        }


        public static SelectList GetUserTypeList()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"Select UserTypeId as Id, UserTypeName as Title From UserType").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = null });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");
            }
        }


        public class SelectListModelFunctionClass
        {
            public int Id { get; set; }

            public string Idstr { get; set; }
            public string Title { get; set; }

            public int ProvinceId { get; set; }
            public string ProvinceTitleNep { get; set; }
        }


        public static SelectList GetMainSectionListDD(int GrantTypeId)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"SELECT MainsectionId AS Id, SectionName AS Title FROM MainSection where status=1 and GrantTypeId='" + GrantTypeId + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "----Select----", Value = null });
                foreach (var item in collection)
                {

                    string[] ProgramTitle = FunctionClass.GetLimitedCharacter(item.Title.ToString(), 135);

                    ddlList.Add(new SelectListItem { Text = ProgramTitle[0].ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                
                return new SelectList(ddlList.ToList(), "Value ", "Text");
            }


        }


        public static SelectList GetMainSectionListDDUpdated(int GrantTypeId)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"SELECT MainsectionId AS Id, SectionName AS Title FROM MainSection where [Status]=1 and GrantTypeId='" + GrantTypeId + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "----Select----", Value = null });
                foreach (var item in collection)
                {

                    string[] ProgramTitle = FunctionClass.GetLimitedCharacter(item.Title.ToString(), 135);

                    ddlList.Add(new SelectListItem { Text = ProgramTitle[0].ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value ", "Text");
            }


        }
        public static SelectList GetMainSectionListDefaultOnly()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                //var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"SELECT MainsectionId AS Id, SectionName AS Title FROM MainSection where GrantTypeId='" + GrantTypeId + "'").ToList();
                ddlList.Add(new SelectListItem { Text = "----Select----", Value = "0" });
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value ", "Text");
            }


        }

        public static SelectList GetProgramListDD(int GrantTypeId)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"Select ProgramId as Id, ProgramName as Title from ProgramSetup where GrantTypeId='" + GrantTypeId + "'").ToList();

                ddlList.Add(new SelectListItem { Text = "----Select----", Value = "0" });
                foreach (var item in collection)
                {

                    string[] ProgramTitle = FunctionClass.GetLimitedCharacter(item.Title.ToString(), 135);

                    ddlList.Add(new SelectListItem { Text = ProgramTitle[0].ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value ", "Text");
            }


        }


        public static SelectList GetProgramListDDForEdit(int MainSectionId)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"Select ProgramId as Id, ProgramName as Title from ProgramSetup where MainSectionId='" + MainSectionId + "'").ToList();

                ddlList.Add(new SelectListItem { Text = "----Select----", Value = "0" });
                foreach (var item in collection)
                {

                    string[] ProgramTitle = FunctionClass.GetLimitedCharacter(item.Title.ToString(), 135);

                    ddlList.Add(new SelectListItem { Text = ProgramTitle[0].ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value ", "Text");
            }


        }


        public static IEnumerable<SelectListItem> GetProgramListDDForEditNew(int MainSectionId)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();

                // Use parameterized SQL query to prevent SQL Injection
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(
                    @"Select ProgramId as Id, ProgramName as Title 
              from ProgramSetup 
              where MainSectionId = @p0", MainSectionId).ToList();

                ddlList.Add(new SelectListItem { Text = "----Select----", Value = "0" });

                foreach (var item in collection)
                {
                    string[] ProgramTitle = FunctionClass.GetLimitedCharacter(item.Title.ToString(), 135);
                    ddlList.Add(new SelectListItem { Text = ProgramTitle[0], Value = item.Id.ToString() });
                }

                return ddlList; // No need to wrap in SelectList
            }
        }



        public static SelectList GetProgramListDDForInsert()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "----Select----", Value = null });
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value ", "Text");
            }
        }





        public static string[] GetLimitedCharacter(string itemDescription, int length)
        {
            string[] description = new string[2];

            description[0] = Regex.Replace(itemDescription, "<.*?>", String.Empty);

            if (description[0].Length > length)
            {
                description[0] = description[0].Substring(0, length) + "....";
                description[1] = "Show";
            }
            else
            {
                description[1] = "Hide";
            }
            return description;

        }



        public static string GetGrantTypeNameById(int GrantTypeId)
        {
            string GrantTypeName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                GrantTypeName = db.Database.SqlQuery<string>("select GrantTypeName from GrantType where GrantTypeId=@id", new SqlParameter("@id", GrantTypeId))
                            .FirstOrDefault();
            }

            return GrantTypeName;
        }

        public static int GetOfficeIdFromVDCMUNCODE(int VDCMUNCODE)
        {
            int OfficeId = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                OfficeId = db.Database.SqlQuery<int>("select isnull(OfficeId,0) as OfficeId From OfficeDetails where UserType=4 and VDCMUNCode=@id", new SqlParameter("@id", VDCMUNCODE))
                            .FirstOrDefault();
            }

            return OfficeId;
        }



        public static int GetMetroSubMetroTypeByOfficeId(int OfficeId)
        {
            int MetroSubMetroType = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                MetroSubMetroType = db.Database.SqlQuery<int>(@"SELECT VMUN.MetroSubMetroType AS MetroType FROM dbo.VdcMun VMUN
                                                                INNER JOIN dbo.OfficeDetails OD ON OD.VDCMUNCode = VMUN.VdcMunCode
                                                                WHERE OD.OfficeId= @id", new SqlParameter("@id", OfficeId)).FirstOrDefault();
            }

            return MetroSubMetroType;
        }






        public static string GetMainSectionNameByMainSectionid(int MainSectionId)
        {
            string GrantTypeName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                GrantTypeName = db.Database.SqlQuery<string>("SELECT SectionName FROM MainSection WHERE MainSectionId=@id", new SqlParameter("@id", MainSectionId))
                            .FirstOrDefault();
            }

            return GrantTypeName;
        }


        public static string GetProvinceDistrictVDCMUNByOfficeID(int OfficeId)
        {
            string GrantTypeName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                GrantTypeName = db.Database.SqlQuery<string>(@"select concat(PV.ProvinceTitleEng, ' , ',DS.DistrictNameEng,' ,',VDC.VdcMunNameEng) as ProVDCMUNAdd From OfficeDetails OD
                            inner join Province PV on PV.ProvinceId = OD.ProvincesId
                            inner join DistrictSetup DS on DS.DistrcitCode = OD.DistrictCode
                            inner join VdcMun VDC on VDC.VdcMunCode = OD.VDCMUNCode
                            where OD.OfficeId = @id", new SqlParameter("@id", OfficeId))
                            .FirstOrDefault();
            }

            return GrantTypeName;
        }




        public static Guid GetCurrentUser()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            Guid CurrentUserid = new Guid(user);
            return CurrentUserid;
        }


        public static int GetCurrentLoginUserType()
        {
            int CurretnUserTypeId = 0;
            Guid UserId = GetCurrentUser();
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                CurretnUserTypeId = db.Database.SqlQuery<int>("select UserType From AspNetUsers where id=@id", new SqlParameter("@id", UserId))
                            .FirstOrDefault();


            }

            return CurretnUserTypeId;
        }

        public static int GetUsertypeFromOfficeId(int officeId)
        {
            int CurretnUserTypeId = 0;

            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                CurretnUserTypeId = db.Database.SqlQuery<int>("select UserType From AspNetUsers where OfficeId=@id", new SqlParameter("@id", officeId))
                            .FirstOrDefault();


            }

            return CurretnUserTypeId;
        }


        public static int GetCurrentLoginUserClientId()
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                try
                {
                    Guid UserId = GetCurrentUser();
                    string UserIdstr = UserId.ToString();
                    var Result = ent.Database.SqlQuery<SelectListModel>("GetCurrentLoginUserClientId {0}", UserIdstr).Single();
                    return Convert.ToInt32(Result.Id);

                }

                catch (Exception ex)
                {

                    return 0;
                }


            }

        }






        public static string GetOfficeNameByOfficeId(int OfficeId)
        {
            string OfficeName = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Result = db.OfficeDetails.SingleOrDefault(x => x.OfficeId == OfficeId);
                if (Result != null)
                {
                    OfficeName = Result.OfficeName;
                }
                return OfficeName;
            }

        }

        public static int GetOfficeIdFromSubProgramId(int subProgramId)
        {
            int OfficeId = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                var Result = db.SubProgramMaster.SingleOrDefault(x => x.SubProgramId == subProgramId);
                if (Result != null)
                {
                    OfficeId = Result.OfficeId;
                }
                return OfficeId;
            }

        }



        public static SelectList GetGrantType()
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModel>(@"select GrantTypeId as Id, GrantTypeName as Title From GrantType").ToList(), "Id", "Title");
            }

        }



        public class SelectListModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

        public static int GetVariableValue(int SubProgramId, int VariableId)
        {
            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                try
                {
                    int value = ent.Database.SqlQuery<int>("GetVariableValue {0},{1}", SubProgramId, VariableId).Single();
                    return value;

                }

                catch (Exception ex)
                {

                    return 0;
                }


            }

        }


        public static SelectList GetProgramYearList()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="१"},
                new {Id="2",Value="२"},
                new {Id="3",Value="३"},

            }, "Id", "Value");

        }

        public static SelectList GetProgramYearListForProgramSubmission()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="१"},
                new {Id="2",Value="२"},


            }, "Id", "Value");

        }

        public static SelectList RunningOrNewDD()
        {
            return new SelectList(new[]
            {

                new {Id="0",Value="नयाँ"},
                new {Id="1",Value="क्रमागत"},


            }, "Id", "Value");

        }

        public static SelectList SearchByProvinceOrPalikaDD()
        {
            return new SelectList(new[]
            {

                new {Id="0",Value="प्रदेश अनुसार"},
                new {Id="1",Value="पालिका अनुसार"},


            }, "Id", "Value");

        }

        public static SelectList CompletionProgramStatusDD()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="पूर्ण सम्पन्न"},
                new {Id="2",Value="आंशिक सम्पन्न"},


            }, "Id", "Value");

        }


        public static SelectList GetGrantTypeWithDefaultValue()
        {
            return new SelectList(new[]
            {
                new {Id="0",Value="--विशेष/समपुरक--"},
                new {Id="1",Value="विशेष"},
                new {Id="2",Value="समपुरक"},


            }, "Id", "Value");

        }

        public static SelectList RequestOrNotDD()
        {
            return new SelectList(new[]
            {

                new {Id="2",Value="--पेश गर्ने--"},
                new {Id="1",Value="--पेश नगर्ने--"},


            }, "Id", "Value");

        }

        public static SelectList ApplicationProgressStatusDD()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="--कार्यान्वयन भईरहेको"},
                new {Id="2",Value="--कार्यान्वयन नभएको--"},



            }, "Id", "Value");

        }

        public static SelectList ApplicationProgressStatusDD(int? StatusId)
        {
            if (StatusId == 1)
            {
                return new SelectList(new[]
            {
                new {Id="1",Value="--कार्यान्वयन भईरहेको"},



            }, "Id", "Value");
            }
            else
            {
                return new SelectList(new[]
            {
                new {Id="2",Value="--कार्यान्वयन नभएको--"},



            }, "Id", "Value");
            }


        }

        public static SelectList RequestOrNotDDWithDefault()
        {
            return new SelectList(new[]
            {
                new {Id="0",Value="--सबै--"},
                new {Id="2",Value="--पेश गर्ने--"},
                new {Id="1",Value="--पेश नगर्ने--"},


            }, "Id", "Value");

        }

        public static SelectList RequestOrNotDDWithDefaultNew()
        {
            return new SelectList(new[]
            {
                new {Id="0",Value="--सबै--"},
                new {Id="2",Value="--पेश गरेको--"},
                new {Id="1",Value="--पेश नगरेको--"},


            }, "Id", "Value");

        }

        public static SelectList ApprovedOrRejectedDD()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="--स्वीकृत भएको--"},
                new {Id="2",Value="--स्वीकृत नभएको--"},


            }, "Id", "Value");

        }
        public static SelectList ApprovedOrRejectedDDWithAll()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="--स्वीकृत भएको--"},
                new {Id="0",Value="--स्वीकृत नभएको--"},
                new {Id="-1",Value="--सबै--"},



            }, "Id", "Value");

        }


        public static SelectList GetProgramPriorityDD()
        {
            int CurrentLoginUserType = GetCurrentLoginUserType();
            if (CurrentLoginUserType == 4)
            {
                return new SelectList(new[]
                {

                new {Id="1",Value="१"},
                new {Id="2",Value="२"},
                new {Id="3",Value="३"},
                //new {Id="4",Value="४"},
                //new {Id="5",Value="५"},
                //new {Id="6",Value="६"},
                //new {Id="7",Value="७"},
                //new {Id="8",Value="८"},
                //new {Id="9",Value="९"},
                //new {Id="10",Value="१०"},

                }, "Id", "Value");

            }
            else
            {
                return new SelectList(new[]
                {

                new {Id="1",Value="१"},
                new {Id="2",Value="२"},
                new {Id="3",Value="३"},
                new {Id="4",Value="४"},
                new {Id="5",Value="५"},
                new {Id="6",Value="६"},
                new {Id="7",Value="७"},
                new {Id="8",Value="८"},
                new {Id="9",Value="९"},
                new {Id="10",Value="१०"},
                new {Id="11",Value="११"},
                new {Id="12",Value="१२"},
                new {Id="13",Value="१३"},
                new {Id="14",Value="१४"},
                new {Id="15",Value="१५"},

                 }, "Id", "Value");

            }

        }



        public static string CheckIfAlreadyInsertedProgressRpt(int OfficeId, int ProgramId, int FiscalYearid, int QuadrimesterId)
        {
            int TotalCount = 0;
            string CssClass = @"btn btn-danger";
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                TotalCount = db.Database.SqlQuery<int>(@"select count(*) Total From QuadrimesterReportsDetail where OfficeId='" + OfficeId + "' and ProgramId='" + ProgramId + "' and FiscalYearId='" + FiscalYearid + "' and QuadrimesterId='" + QuadrimesterId + "'").FirstOrDefault();
            }

            if (TotalCount > 0)
            {
                CssClass = "btn btn-info";
            }
            return CssClass;
        }



        public static SelectList GetProgramPhaseListDD()
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select phaseTitle as Title,PhaseNumber as ID From ProgramPhaseStatus").ToList(), "Id", "Title");
            }

        }

        public static SelectList GetProgramPhaseListDDDefault()
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                var collection = ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select phaseTitle as Title, PhaseNumber as Id From ProgramPhaseStatus").ToList();
                ddlList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (var item in collection)
                {
                    ddlList.Add(new SelectListItem { Text = item.Title.ToString(), Value = item.Id.ToString() });
                }
                var ddlSelectOptionList = ddlList;
                return new SelectList(ddlList.ToList(), "Value", "Text");

            }


        }


        public static List<SelectListItem> GetOfficeTypeListWithDefault()
        {
            // Hard-coded example; ideally fetch from OfficeType table if dynamic
            var list = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "All Office Types" },  // Default option
            new SelectListItem { Value = "2", Text = "Province" },
            new SelectListItem { Value = "4", Text = "Municipality" },
         

        };

            return list;
        }



        public static SelectList GetFiscalYearListDD()
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearRecordId as ID,Title from FiscalYearRecords").ToList(), "Id", "Title");
            }

        }


        public static SelectList GetFiscalYearListDDFromProgramPhaseTable()
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select FiscalYearId as Id, PhaseTitle as Title From ProgramPhaseStatus").ToList(), "Id", "Title");
            }

        }
        public static int GetPhaseNumberFromFiscalYearPhaseTable(int FYID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                try
                {
                    int ApprovedAmount = db.Database.SqlQuery<int>(@"select PhaseNumber as Id From ProgramPhaseStatus where FiscalYearId='" + FYID + "'").FirstOrDefault();
                    return ApprovedAmount;
                }
                catch (Exception)
                {

                    return 0;
                }



            }
        }


        public static decimal GetGrantedAmountApprovedByAdmin(int ProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                try
                {
                    decimal ApprovedAmount = db.Database.SqlQuery<decimal>(@"select AmountSecondYear From programwiseamount where programid='" + ProgramId + "'").FirstOrDefault();
                    return ApprovedAmount;
                }
                catch (Exception)
                {

                    return 0;
                }


            }

        }


        public static decimal GetGrantedAmountApprovedByAdmin(int ProgramId, int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                try
                {
                    if (PhaseNumber == 2)
                    {
                        decimal ApprovedAmount = db.Database.SqlQuery<decimal>(@"select isnull(AmountThirdYear,0) as GAmount From programwiseamount where programid='" + ProgramId + "'").FirstOrDefault();
                        return ApprovedAmount;
                    }
                    else
                    {
                        decimal ApprovedAmount = db.Database.SqlQuery<decimal>(@"select isnull(AmountSecondYear,0) as GAmmount From programwiseamount where programid='" + ProgramId + "'").FirstOrDefault();
                        return ApprovedAmount;
                    }

                }
                catch (Exception)
                {

                    return 0;
                }


            }

        }

        public static SelectList GetStaticFiscalYearDD()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="२०७६/७७"},
                new {Id="2",Value="२०७७/७८"},
                new {Id="3",Value="२०७८/७९"},
                new {Id="4",Value="२०७९/८०"},
                new {Id="5",Value="२०८०/८१"},
                new {Id="6",Value="२०८१/८२"},

            }, "Id", "Value");

        }
        public static SelectList GetStaticFiscalYearDDForAnusuchisix()
        {
            return new SelectList(new[]
            {

                new {Id="1",Value="२०७६/७७"},
                new {Id="2",Value="२०७७/७८"},
                new {Id="3",Value="२०७८/७९"},
                new {Id="4",Value="२०७९/८०"},
                new {Id="5",Value="२०८०/८१"},

            }, "Id", "Value");

        }

        public static int GetStaticFiscalYearFromPhaseNumber(int PhaseNumber)
        {

            if (PhaseNumber == 1)
            {
                return 1;
            }
            else if (PhaseNumber == 2)
            {
                return 11;
            }
            else if (PhaseNumber == 3)
            {
                return 13;
            }
            else if (PhaseNumber == 4)
            {
                return 14;
            }
            else if (PhaseNumber == 5)
            {
                return 15;
            }
            else if (PhaseNumber==6)
            {
                return 16;
            }
            else
                return 1;

        }
        public static string GetStaticFiscalYearNameFormId(int FyId)
        {
            //Fiscal year or phase number
            string fiscalYearName = string.Empty;
            if (FyId == 1)
            {
                fiscalYearName = "२०७६/७७";
            }
            else if (FyId == 2)
            {

                fiscalYearName = "२०७७/७८";
            }
            else if (FyId == 3)
            {
                fiscalYearName = "२०७८/७९";
            }
            else if (FyId == 4)
            {
                fiscalYearName = "२०७९/८०";

            }
            else
            {
                fiscalYearName = "";

            }

            return fiscalYearName;


        }


        public static SelectList GetProgramPriorityListDD()
        {
            return new SelectList(new[]
            {
                new {Id="0",Value="--सबै--"},
                new {Id="1",Value="P1"},
                new {Id="2",Value="P2"},
                new {Id="3",Value="P3"},
                new {Id="4",Value="P4"},
                new {Id="5",Value="P5 वा भन्दा धेरै"},





            }, "Id", "Value");

        }


        public static SelectList GetPointDetailsToUpdateFromAdminDD(int GrantTypeId, int VariableId)
        {

            using (GrantAppDBEntities ent = new GrantAppDBEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                return new SelectList(ent.Database.SqlQuery<SelectListModelFunctionClass>(@"select CONCAT(' (', GVD.Valuation,' )',GVD.VariableDetailBasis ) AS Title ,Valuation as ID From GrantVariables GV inner join GrantVariablesDetail GVD on GVD.VariableId=GV.VariableId where GV.GrantTypeId='" + GrantTypeId + "' and GVD.Valuation>0 and GV.VariableId='" + VariableId + "'").ToList(), "Id", "Title");
            }

        }


        public static int GetTotalProgressReportCount(int FYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) From QuadrimesterReportsDetail qd
inner join SubProgramMaster sm on qd.ProgramId=sm.SubProgramId
inner join OfficeDetails OD on od.OfficeId=sm.OfficeId
where OD.UserType=2 and sm.PhaseStatus=@id", new SqlParameter("@id", FYID))
                             .FirstOrDefault();
            }
            return Total;

        }

        public static string GetTotalProgressReportCountstr(int FYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct qd.programid) From QuadrimesterReportsDetail qd
inner join SubProgramMaster sm on qd.ProgramId=sm.SubProgramId
inner join OfficeDetails OD on od.OfficeId=sm.OfficeId
where OD.UserType=2").FirstOrDefault();
            }
            return EnglishToNepaliNumber(Total.ToString());

        }


        public static int GetTotalProgressReportSubmitedByOfficesCount(int FYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct qd.OfficeId) From QuadrimesterReportsDetail qd
inner join SubProgramMaster sm on qd.ProgramId=sm.SubProgramId
inner join OfficeDetails OD on od.OfficeId=sm.OfficeId
where OD.UserType=2 and sm.PhaseStatus=@id", new SqlParameter("@id", FYID))
                             .FirstOrDefault();
            }
            return Total;

        }


        public static string GetTotalProgressReportSubmitedByOfficesCountStr(int FYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct qd.OfficeId) From QuadrimesterReportsDetail qd
inner join SubProgramMaster sm on qd.ProgramId=sm.SubProgramId
inner join OfficeDetails OD on od.OfficeId=sm.OfficeId
where OD.UserType=2").FirstOrDefault();
            }
            return EnglishToNepaliNumber(Total.ToString());

        }


        public static int GetTotalProgressReportCountLocalLevel(int FYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(*) From QuadrimesterReportsDetail qd
inner join SubProgramMaster sm on qd.ProgramId=sm.SubProgramId
inner join OfficeDetails OD on od.OfficeId=sm.OfficeId
where OD.UserType=4 and sm.PhaseStatus=@id", new SqlParameter("@id", FYID))
                             .FirstOrDefault();
            }
            return Total;

        }

        public static string GetTotalProgressReportCountLocalLevelstr(int FYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct qd.programid) From QuadrimesterReportsDetail qd
                inner join SubProgramMaster sm on qd.ProgramId=sm.SubProgramId
                inner join OfficeDetails OD on od.OfficeId=sm.OfficeId
                where OD.UserType=4").FirstOrDefault();
            }
            return EnglishToNepaliNumber(Total.ToString());

        }

        public static string GetTotalCountReportsByOfficeTypestr(int OfficeType)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select COUNT(*) TotalReceived From QuadrimesterReportsDetail
                            qrd inner join OfficeDetails od on qrd.OfficeId=od.OfficeId
                            where qrd.QuadrimesterId=0 and od.UserType='" + OfficeType + "'").FirstOrDefault();
            }
            return EnglishToNepaliNumber(Total.ToString());

        }



        public static string GetTotalProgressReportSubmitedByOfficesCountLocalLevel(int FYID)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct qd.OfficeId) From QuadrimesterReportsDetail qd
inner join SubProgramMaster sm on qd.ProgramId=sm.SubProgramId
inner join OfficeDetails OD on od.OfficeId=sm.OfficeId
where OD.UserType=4").FirstOrDefault();
            }


            return EnglishToNepaliNumber(Total.ToString());

        }


        public static string GetTotalReportSubmitedByOfficesType(int OfficeTypeId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select count(distinct qd.OfficeId) From QuadrimesterReportsDetail qd
                        inner join OfficeDetails OD on od.OfficeId=qd.OfficeId
                        where qd.QuadrimesterId=0 and OD.UserType='" + OfficeTypeId + "'").FirstOrDefault();
            }


            return EnglishToNepaliNumber(Total.ToString());

        }

        public static int GetRGAFiscalYearByPhaseNumber(int PhaseNumber)
        {
            int CurretnUserTypeId = 0;
            Guid UserId = GetCurrentUser();
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                CurretnUserTypeId = db.Database.SqlQuery<int>(@"select FiscalYearId from ProgramPhaseStatus
                where PhaseNumber = @id", new SqlParameter("@id", PhaseNumber))
                            .FirstOrDefault();


            }

            return CurretnUserTypeId;
        }

        public static string GetPhaseTitelFromFiscalYearId(int FiscalYearId)
        {
            string phaseTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                phaseTitle = db.Database.SqlQuery<string>(@"select PhaseTitle From ProgramPhaseStatus
                    where FiscalYearId='" + FiscalYearId + "'").FirstOrDefault();
                return phaseTitle;
            }

        }


        public static string GetPhaseTitleBYPhaseNumber(int PhaseNumber)
        {
            string PhaseTitle = string.Empty;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                PhaseTitle = db.Database.SqlQuery<string>(@"select PhaseTitle from ProgramPhaseStatus
                where PhaseNumber = @id", new SqlParameter("@id", PhaseNumber)).FirstOrDefault();


            }

            return PhaseTitle;
        }


        public static SelectList GetApprovedStatusListDD()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="स्वीकृत"},
                new {Id="2",Value="अस्वीकृत"},

            }, "Id", "Value");

        }


        public static string EnglishToNepaliNumber(string input)
        {

            return input.Replace('0', '०')
                    .Replace('1', '१')
                    .Replace('2', '२')
                    .Replace('3', '३')
                    .Replace('4', '४')
                    .Replace('5', '५')
                    .Replace('6', '६')
                    .Replace('7', '७')
                    .Replace('8', '८')
                    .Replace('9', '९')
                    .Replace('.', '.');
        }



        public static decimal GetApprovedAmount(int SubprogramId, int Year)
        {
            decimal approvedAmount = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                if (Year == 1)
                {
                    approvedAmount = db.Database.SqlQuery<decimal>(@"select top 1 isnull(Amount,0) as AmountFirstYear From ProgramwiseAmount where ProgramId='" + SubprogramId + "'").FirstOrDefault();

                }
                else if (Year == 2)
                {
                    approvedAmount = db.Database.SqlQuery<decimal>(@"select top 1 isnull(AmountSecondYear,0) as AmountFirstYear From ProgramwiseAmount where ProgramId='" + SubprogramId + "'").FirstOrDefault();

                }
                else
                {
                    approvedAmount = db.Database.SqlQuery<decimal>(@"select top 1 isnull(AmountThirdYear,0) as AmountFirstYear From ProgramwiseAmount where ProgramId='" + SubprogramId + "'").FirstOrDefault();

                }


                return approvedAmount;

            }

        }


        public static SelectList ApplicationCompletionListDD()
        {
            return new SelectList(new[]
            {
                new {Id="0",Value="--छान्नुहोस--"},
                new {Id="1",Value="-- आयोजना वा कार्यक्रम सम्पन्न भैसकेको--"},
                new {Id="2",Value="--आयोजना वा कार्यक्रम अधुरो अपुरो अवस्थामा रहेको--"},
                new {Id="3",Value="--आयोजना वा कार्यक्रम कार्यान्वयन गर्न नसकिएको--"},


            }, "Id", "Value");

        }



        public static int GetTotalByApplicationCompletionStatusId(int ApplicationStatusId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select COUNT(*) as Total From ApplicationCompletionStatus
                                where CompletionStatusId = '" + ApplicationStatusId + "'").FirstOrDefault();
                return Total;
            }

        }



        public static int GetTotalFullForParitalCompletion(int ApplicationStatusId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select COUNT(*) as Total From ApplicationCompletionStatus
                                where CompleteOrParitalCompleteStatusId = '" + ApplicationStatusId + "'").FirstOrDefault();
                return Total;
            }

        }


        public static int GetTotelSubmtedProgressReportByOffice(int OfficeId, int SubprogramId)
        {
            int Total = 0;
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                Total = db.Database.SqlQuery<int>(@"select COUNT(*) as TotalSubmitedProgressReport From QuadrimesterReportsDetail
                where OfficeId='" + OfficeId + "' and ProgramId='" + SubprogramId + "' and QuadrimesterId=0").FirstOrDefault();
            }
            //return EnglishToNepaliNumber(Total.ToString());
            return Total;

        }

        public static SelectList ProvinceWiseOrLocalLevelWiseDD()
        {
            return new SelectList(new[]
            {

                new {Id="2",Value="--प्रदेश अनुसार--"},
                new {Id="4",Value="--स्था.तह अनुसार--"},
                



            }, "Id", "Value");

        }









    }
}