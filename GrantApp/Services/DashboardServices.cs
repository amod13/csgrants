using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Areas.Admin.Models;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;



namespace GrantApp.Services
{
    public class DashboardServices
    {


        public AdminDashboardNewprojectCountListViewModel AdminDashboardNewprojectCountList(int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                AdminDashboardNewprojectCountListViewModel returnModel = new AdminDashboardNewprojectCountListViewModel();
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                returnModel = db.Database.SqlQuery<AdminDashboardNewprojectCountListViewModel>("AdminDashboardNewprojectCountList @PhaseNumber", PhaseNumberParam).FirstOrDefault();
                if (returnModel == null)
                {
                    returnModel = new AdminDashboardNewprojectCountListViewModel();
                }
                else
                {
                    returnModel.NewAndRunning = returnModel.BisheshCount + returnModel.SampurakCount + returnModel.TotalRunning;
                }

                return returnModel;
            }
        }

        public List<DashboardModel> PopulateSectionWiseProgramCount(int GrantTypeID ,int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<DashboardModel> PopulateSectionWiseProgramCountList = new List<DashboardModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeID };//List All 
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                PopulateSectionWiseProgramCountList = db.Database.SqlQuery<DashboardModel>("PopulateSectionWiseProgramCount @GrantTypeId,@PhaseNumber", GrantTypeIdParam,PhaseNumberParam).ToList();
                return PopulateSectionWiseProgramCountList;
            }
        }

        public List<DashboardNewProgramListViewModel> DashboradPopulateProgramList(int GrantTypeID, int PhaseNumber,int? provinceId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<DashboardNewProgramListViewModel> DashBoardNewProgramList = new List<DashboardNewProgramListViewModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeID };//List All 
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseNumber };
                var ProvineIdParam = new SqlParameter { ParameterName = "@ProvineId", Value = provinceId.HasValue?provinceId:0 };
                DashBoardNewProgramList = db.Database.SqlQuery<DashboardNewProgramListViewModel>("DashboradPopulateProgramList @GrantTypeId,@PhaseStatus,@ProvineId", GrantTypeIdParam, PhaseNumberParam,ProvineIdParam).ToList();
                return DashBoardNewProgramList;
            }
        }

        public List<DashboardNewProgramListViewModel> DashboradPopulateRequestedAmountList(int GrantTypeID, int PhaseNumber, int ProvinceId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<DashboardNewProgramListViewModel> DashBoardNewProgramList = new List<DashboardNewProgramListViewModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeID };//List All 
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseNumber };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = ProvinceId };
                DashBoardNewProgramList = db.Database.SqlQuery<DashboardNewProgramListViewModel>("DashboradPopulateRequestedAmountList @GrantTypeId,@PhaseStatus,@ProvinceId", GrantTypeIdParam, PhaseNumberParam,ProvinceIdParam).ToList();
                return DashBoardNewProgramList;
            }
        }

        public string UpdateVDCMUNName(VDCMUNVIewModel _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                string OfficeNameVal = _Model.NewVDCName + ", कार्यलय";
                var VDCMunNameNepaliParam = new SqlParameter { ParameterName = "@VDCMunNameNepali", Value = _Model.NewVDCName == null ? string.Empty : _Model.NewVDCName };
                var VDCMUNCODEParam = new SqlParameter { ParameterName = "@VDCMUNCODE", Value = _Model.VDCMUNCode };
                var OfficeNameParam = new SqlParameter { ParameterName = "@OfficeName", Value = OfficeNameVal };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };



                var result = db.Database.ExecuteSqlCommand("exec UpdateVDCMunName @VDCMunNameNepali,@VDCMUNCODE,@OfficeName,@Message OUT",
                     VDCMunNameNepaliParam, VDCMUNCODEParam, OfficeNameParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();

                return msg;

            }
        }


        public List<AllProgramListOfClientViewModel> AllProgramListOfClientByOfficeId(int GrantTypeID, int PhaseNumber,int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AllProgramListOfClientViewModel> AllProgramListOfClientViewModelList = new List<AllProgramListOfClientViewModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeID };//List All 
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };

                AllProgramListOfClientViewModelList = db.Database.SqlQuery<AllProgramListOfClientViewModel>("PopulateAllProgramListByOfficeId @GrantTypeId,@PhaseStatus,@OfficeId", GrantTypeIdParam, PhaseNumberParam,OfficeIdParam).ToList();
                return AllProgramListOfClientViewModelList;
            }
        }

        public List<AllProgramListOfClientViewModel> SP_PopulateProgramSubmitStatusWise(int GrantTypeID, int PhaseNumber, int OfficeId,int SubmitedOrNotId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AllProgramListOfClientViewModel> AllProgramListOfClientViewModelList = new List<AllProgramListOfClientViewModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeID };//List All 
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseStatus", Value = PhaseNumber };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var SubmitedOrNotIdParam = new SqlParameter { ParameterName = "@SubmitStatus", Value = SubmitedOrNotId };
                AllProgramListOfClientViewModelList = db.Database.SqlQuery<AllProgramListOfClientViewModel>("SP_PopulateProgramSubmitStatusWise @GrantTypeId,@PhaseStatus,@OfficeId,@SubmitStatus", GrantTypeIdParam, PhaseNumberParam, OfficeIdParam,SubmitedOrNotIdParam).ToList();
                return AllProgramListOfClientViewModelList;
            }
        }

        public GetSubprogramDetailByIdViewModel SP_GetSubprogramDetailByIDForSearch(int SubProgramID)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                GetSubprogramDetailByIdViewModel returnModel = new GetSubprogramDetailByIdViewModel();
                var SubprogramIdParam = new SqlParameter { ParameterName = "@SubprogramId", Value = SubProgramID };//List All 
                
                returnModel = db.Database.SqlQuery<GetSubprogramDetailByIdViewModel>("SP_GetSubprogramDetailByIDForSearch @SubprogramId", SubprogramIdParam).FirstOrDefault();
                if(returnModel==null)
                {
                    returnModel = new GetSubprogramDetailByIdViewModel();
                }

                return returnModel;
            }
        }


        public List<SecionWiseProgramCountViewModel> AdminDashboardCountMainSectionWise(int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<SecionWiseProgramCountViewModel> AllProgramListOfClientViewModelList = new List<SecionWiseProgramCountViewModel>();
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                
                AllProgramListOfClientViewModelList = db.Database.SqlQuery<SecionWiseProgramCountViewModel>("AdminDashboardCountMainSectionWise @PhaseNumber", PhaseNumberParam).ToList();
                return AllProgramListOfClientViewModelList;
            }
        }

        public List<ViewNewProgramProvinceWiseStatusVM> SP_AdminDashboardNewProgramStatus(int PhaseNumber,int UserType, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewNewProgramProvinceWiseStatusVM> PopulateProvinceWiseListForNew = new List<ViewNewProgramProvinceWiseStatusVM>();
                var ProgramPhaseNumberParam = new SqlParameter { ParameterName = "@ProgramPhaseNumber", Value = PhaseNumber };//List All 
                var UserTypeParam = new SqlParameter { ParameterName = "@UserType", Value = UserType };
                var GrantTypeIdparam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };

                PopulateProvinceWiseListForNew = db.Database.SqlQuery<ViewNewProgramProvinceWiseStatusVM>("SP_AdminDashboardNewProgramStatus @ProgramPhaseNumber,@UserType,@GrantTypeId", ProgramPhaseNumberParam, UserTypeParam,GrantTypeIdparam).ToList();
                return PopulateProvinceWiseListForNew;
            }
        }

        public List<ViewNewProgramProvinceWiseStatusVM> SP_AdminDashboardNewRequestedProgramStatus(int RGAFiscalYearId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ViewNewProgramProvinceWiseStatusVM> PopulateProvinceWiseListForNew = new List<ViewNewProgramProvinceWiseStatusVM>();
                var RGAFiscalYearIdParam = new SqlParameter { ParameterName = "@RGAFiscalYearId", Value = RGAFiscalYearId };//List All 

                var GrantTypeIdparam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };

                PopulateProvinceWiseListForNew = db.Database.SqlQuery<ViewNewProgramProvinceWiseStatusVM>("SP_AdminDashboardNewRequestedProgramStatus @RGAFiscalYearId,@GrantTypeId", RGAFiscalYearIdParam, GrantTypeIdparam).ToList();
                return PopulateProvinceWiseListForNew;
            }
        }

        public List<LetterDetails> SP_PopulateLetterList(int letterDetailId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<LetterDetails> LetterDetailsList = new List<LetterDetails>();
                var LetterDetailIdParam = new SqlParameter { ParameterName = "@LetterDetailId", Value = letterDetailId };//List All 
                LetterDetailsList = db.Database.SqlQuery<LetterDetails>("SP_PopulateLetterList @LetterDetailId", LetterDetailIdParam).ToList();
                return LetterDetailsList;
            }
        }

        public string SP_InsertLetter(LetterDetails _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                
                var LetterDateParam = new SqlParameter { ParameterName = "@LetterDate", Value = _Model.LetterDate };
                var LetterDateEngParam = new SqlParameter { ParameterName = "@LetterDateEng", Value = _Model.LetterDateEng ==null?string.Empty:_Model.LetterDateEng};
                var LetterSubjectParam = new SqlParameter { ParameterName = "@LetterSubject", Value = _Model.LetterSubject==null?string.Empty:_Model.LetterSubject };
                var LetterDescriptionParam = new SqlParameter { ParameterName = "@LetterDescription",Value=_Model.LetterDescription==null?string.Empty:_Model.LetterDescription };
                var UploadDocParam = new SqlParameter { ParameterName = "@UploadDoc", Value = _Model.UploadDoc==null?string.Empty:_Model.UploadDoc };
                var LetterStatusParam = new SqlParameter { ParameterName = "@LetterStatus", Value = _Model.LetterStatus.HasValue ? _Model.LetterStatus : false };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                   var result = db.Database.ExecuteSqlCommand("exec SP_InsertLetter @LetterDate, @LetterDateEng,@LetterSubject,@LetterDescription,@UploadDoc,@LetterStatus,@Message OUT",
                    LetterDateParam, LetterDateEngParam, LetterSubjectParam, LetterDescriptionParam, UploadDocParam, LetterStatusParam, MessageParam);
                
                msg = MessageParam.SqlValue.ToString();

                return msg;

            }
        }


        public string SP_UpdateLetterDetail(LetterDetails _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var LetterDetailIdParam = new SqlParameter { ParameterName = "@LetterDetailId", Value = _Model.LetterDetailId };
                var LetterDateParam = new SqlParameter { ParameterName = "@LetterDate", Value = _Model.LetterDate };
                var LetterDateEngParam = new SqlParameter { ParameterName = "@LetterDateEng", Value = _Model.LetterDateEng == null ? string.Empty : _Model.LetterDateEng };
                var LetterSubjectParam = new SqlParameter { ParameterName = "@LetterSubject", Value = _Model.LetterSubject == null ? string.Empty : _Model.LetterSubject };
                var LetterDescriptionParam = new SqlParameter { ParameterName = "@LetterDescription", Value = _Model.LetterDescription == null ? string.Empty : _Model.LetterDescription };
                var UploadDocParam = new SqlParameter { ParameterName = "@UploadDoc", Value = _Model.UploadDoc == null ? string.Empty : _Model.UploadDoc };
                var LetterStatusParam = new SqlParameter { ParameterName = "@LetterStatus", Value = _Model.LetterStatus.HasValue ? _Model.LetterStatus : false };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec SP_UpdateLetterDetail @LetterDetailId,@LetterDate, @LetterDateEng,@LetterSubject,@LetterDescription,@UploadDoc,@LetterStatus,@Message OUT",
                 LetterDetailIdParam,LetterDateParam, LetterDateEngParam, LetterSubjectParam, LetterDescriptionParam, UploadDocParam, LetterStatusParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();

                return msg;

            }
        }

        public string SP_DeleteLetterDetail(int letterDetailId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var LetterDetailIdParam = new SqlParameter { ParameterName = "@LetterDetailId", Value = letterDetailId };//List All 
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@OutputMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var returnVal = db.Database.ExecuteSqlCommand("SP_DeleteLetterDetail @LetterDetailId,@OutputMessage", LetterDetailIdParam, MessageParam);
                msg = MessageParam.SqlValue.ToString();
                return msg;
            }
        }

        public List<GetNotCompletedProgramListByOfficeIdVM> SPUP_GetNotCompletedProgramListByOfficeId(int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<GetNotCompletedProgramListByOfficeIdVM> PopulateSectionWiseProgramCountList = new List<GetNotCompletedProgramListByOfficeIdVM>();
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };//List All 
                PopulateSectionWiseProgramCountList = db.Database.SqlQuery<GetNotCompletedProgramListByOfficeIdVM>("SPUP_GetNotCompletedProgramListByOfficeId @OfficeId", OfficeIdParam).ToList();
                return PopulateSectionWiseProgramCountList;
            }
        }

        public TotalProgramListPhaseWiseNewModel spCountProgramNumberWithPhase(int PhaseNumber)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                TotalProgramListPhaseWiseNewModel PopulateSectionWiseProgramCountList = new TotalProgramListPhaseWiseNewModel();
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };//List All 
                PopulateSectionWiseProgramCountList = db.Database.SqlQuery<TotalProgramListPhaseWiseNewModel>("spCountProgramNumberWithPhase @PhaseNumber", PhaseNumberParam).FirstOrDefault();
                return PopulateSectionWiseProgramCountList;
            }
        }

       
            

    }
}