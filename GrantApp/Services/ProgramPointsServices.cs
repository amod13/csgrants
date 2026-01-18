using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace GrantApp.Services
{
    public class ProgramPointsServices
    {

        public string InsertProgramPoints(SubProgramMaster _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                if (_Model.PopulatePointsVariableListViewModelList?.Any() != true)
                {

                    msg = "Error...Please try again";

                }
                else
                {
                    foreach (var item in _Model.PopulatePointsVariableListViewModelList)
                    {
                        var SubProgramMasterIdParam = new SqlParameter { ParameterName = "@SubProgramMasterId", Value = _Model.SubProgramId };
                        var PointsVarialbeIdParam = new SqlParameter { ParameterName = "@PointsVarialbeId", Value = item.PointsVariableSetupId };
                        var PointsObtainParam = new SqlParameter { ParameterName = "@PointsObtain", Value = item.PointsObtain };
                        var createdDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };
                        var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = 1 };
                        var StatusParam = new SqlParameter { ParameterName = "@Status", Value = 2 };


                        var MessageParam = new SqlParameter
                        {
                            ParameterName = "@Message",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };



                        var result = db.Database.ExecuteSqlCommand("exec InsertProgramPoints @SubProgramMasterId,@PointsVarialbeId,@PointsObtain,@CreatedDate,@CreatedBy,@Status,@Message OUT",
                            SubProgramMasterIdParam, PointsVarialbeIdParam, PointsObtainParam, createdDateParam, CreatedByParam, StatusParam, MessageParam);

                        msg = MessageParam.SqlValue.ToString();
                    }


                }

                return msg;

            }
        }


        public List<ValuationBasisModel> PopulateVariableBasisDetail(int SubProgramId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new Models.GrantAppDBEntities())
            {
                List<ValuationBasisModel> tempList = new List<Models.ValuationBasisModel>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };

                tempList = db.Database.SqlQuery<ValuationBasisModel>("PopulateVariableBasisDetail @SubProgramId,@GrantTypeId", SubProgramIdParam, GrantTypeIdParam).ToList();

                return tempList;
            }
        }

        public List<ValuationBasisModel> PopulateVariableBasisDetailEdit(int SubProgramId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new Models.GrantAppDBEntities())
            {
                List<ValuationBasisModel> tempList = new List<Models.ValuationBasisModel>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };

                tempList = db.Database.SqlQuery<ValuationBasisModel>("PopulateVariableBasisDetailEdit @SubProgramId,@GrantTypeId", SubProgramIdParam, GrantTypeIdParam).ToList();

                return tempList;
            }
        }


        public string InsertSubProgramVariableBasis(SubProgramMaster _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var DelSubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = _Model.SubProgramId };
                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var delresult = db.Database.ExecuteSqlCommand("exec DeleteVariableBasisDetail @SubProgramId,@Message OUT",
                        DelSubProgramIdParam, DelMessageParam);

                msg = DelMessageParam.SqlValue.ToString();

                if (msg == "Deleted Successfully")
                {
                    foreach (var item in _Model.ValuationBasisModelList)
                    {
                        var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = _Model.SubProgramId };
                        var VariableIdParam = new SqlParameter { ParameterName = "@VariableId", Value = item.VariableId };
                        var VariableDetailIdParam = new SqlParameter { ParameterName = "@VariableDetailId", Value = item.VariableDetailid };
                        var IsCheckedParam = new SqlParameter { ParameterName = "@IsChecked", Value = item.IsChecked };
                        var InsertedDateParam = new SqlParameter { ParameterName = "@InsertedDate", Value = DateTime.Now };
                        var InsertedByParam = new SqlParameter { ParameterName = "@InsertedBy", Value = Guid.NewGuid() };


                        var MessageParam = new SqlParameter
                        {
                            ParameterName = "@Message",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };



                        var result = db.Database.ExecuteSqlCommand("exec InsertSubProgramVariableBasis @SubProgramId,@VariableId,@VariableDetailId,@IsChecked,@InsertedDate,@InsertedBy,@Message OUT",
                            SubProgramIdParam, VariableIdParam, VariableDetailIdParam, IsCheckedParam, InsertedDateParam, InsertedByParam, MessageParam);

                        msg = MessageParam.SqlValue.ToString();
                    }

                }


                return msg;

            }
        }



        public List<ValuationBasisModel> PopulateProposalValuationList(int SubProgramId, int GrantTypeId, int OfficeId)
        {
            using (GrantAppDBEntities db = new Models.GrantAppDBEntities())
            {
                List<ValuationBasisModel> tempList = new List<Models.ValuationBasisModel>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };

                tempList = db.Database.SqlQuery<ValuationBasisModel>("PopulateProposalValuationList @SubProgramId,@GrantTypeId,@OfficeId", SubProgramIdParam, GrantTypeIdParam, OfficeIdParam).ToList();

                return tempList;
            }
        }

        public bool CheckIfProgramValuationBasisExist(int SubProgramId, int GrantTypeId, int OfficeId)
        {
            using (GrantAppDBEntities db = new Models.GrantAppDBEntities())
            {
                bool IfExists = false;
                IfExists = PopulateProposalValuationList(0, GrantTypeId, OfficeId).Where(c => c.SubProgramId == SubProgramId).Any();
                return IfExists;
            }
        }

        public List<ValuationBasisModel> PopulateProposalValuationResultList(int GrantTypeId, int Province, int District, int GpNp, int UserType, int PhaseNumber)
        {
            using (GrantAppDBEntities db = new Models.GrantAppDBEntities())
            {
                List<ValuationBasisModel> tempList = new List<Models.ValuationBasisModel>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                var ProvinceParam = new SqlParameter { ParameterName = "@Province", Value = Province };
                var DistrictParam = new SqlParameter { ParameterName = "@District", Value = District };
                var GpNpParam = new SqlParameter { ParameterName = "@VdcMun", Value = GpNp };
                var UserTypeParam = new SqlParameter { ParameterName = "@UserType", Value = UserType };
                var PhaseNumberParam = new SqlParameter { ParameterName = "@PhaseNumber", Value = PhaseNumber };
                
                tempList = db.Database.SqlQuery<ValuationBasisModel>("PopulateProposalValuationResultList @GrantTypeId,@Province,@District,@VdcMun,@UserType,@PhaseNumber", GrantTypeIdParam, ProvinceParam, DistrictParam, GpNpParam, UserTypeParam,PhaseNumberParam).ToList();
                return tempList;
            }
        }





        public List<ValuationResultViewModel> PopulateVariableBasisDetailForReport(int SubProgramId, int GrantTypeId)
        {
            using (GrantAppDBEntities db = new Models.GrantAppDBEntities())
            {
                List<ValuationResultViewModel> tempList = new List<Models.ValuationResultViewModel>();
                var SubProgramIdParam = new SqlParameter { ParameterName = "@SubProgramId", Value = SubProgramId };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                tempList = db.Database.SqlQuery<ValuationResultViewModel>("PopulateVariableBasisDetailForReport @SubProgramId,@GrantTypeId", SubProgramIdParam, GrantTypeIdParam).ToList();
                return tempList;
            }
        }



        public decimal GetProgramWiseAmount(int ProgramId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                decimal totalAmount = db.Database.SqlQuery<decimal>(@"select isnull(Amount,0) as ApprovedAmount from ProgramwiseAmount
                    where ProgramId='"+ProgramId+"'").FirstOrDefault();
                return totalAmount;

            }

        }


    }
}