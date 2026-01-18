using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace GrantApp.Services
{
    public class RequestGrantAmountService
    {

        public string InsertRequestDemandAmount(RequestGrantAmountModel model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = model.ProgramId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var ProgramTimeDurationParam = new SqlParameter { ParameterName = "@ProgramTimeDuration", Value = model.ProgramTimeDuration };

                var AmountFirstParam = new SqlParameter { ParameterName = "@AmountFirst ", Value = model.AmountFirst.HasValue ? model.AmountFirst : 0 };
                var AmountSecondParam = new SqlParameter { ParameterName = "@AmountSecond ", Value = model.AmountSecond.HasValue ? model.AmountSecond : 0 };
                var AmountThirdParam = new SqlParameter { ParameterName = "@AmountThird ", Value = model.AmountThird.HasValue ? model.AmountThird : 0 };

                var ChaluAmountParam = new SqlParameter { ParameterName = "@ChaluAmount ", Value = model.ChaluAmount.HasValue ? model.ChaluAmount : 0 };
                var PujiAmountParam = new SqlParameter { ParameterName = "@PujiAmount ", Value = model.PujiAmount.HasValue ? model.PujiAmount : 0 };
                var IsCancelParam = new SqlParameter { ParameterName = "@IsCancel", Value = model.IsCanceled.HasValue ? model.IsCanceled : false };
                var OfficeAmountParam = new SqlParameter { ParameterName = "@OfficeAmount ", Value = model.OfficeAmount.HasValue ? model.OfficeAmount : 0 };

                var ContractAmountParam = new SqlParameter { ParameterName = "@ContractAmount", Value = model.ContractAmount.HasValue ? model.ContractAmount : 0 };

                var CreatedDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };

                var RequestReasonDocParam = new SqlParameter { ParameterName = "@RequestReasonDoc", Value = model.RequestReasonDoc };

                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = false };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var result = db.Database.ExecuteSqlCommand(
                                             "exec InsertRequestGrantAmount " +
                                             "@ProgramId, " +
                                             "@OfficeId," +
                                             "@FiscalYearId," +
                                             "@ProgramTimeDuration," +
                                             "@AmountFirst," +
                                             "@AmountSecond," +
                                             "@AmountThird, " +
                                             "@ChaluAmount," +
                                             "@PujiAmount," +
                                             "@IsCancel," +
                                             "@OfficeAmount," +
                                             "@ContractAmount," +
                                             "@CreatedDate," +
                                             "@RequestReasonDoc," +
                                             "@Status," +
                                             "@Message OUT",
                                             ProgramIdParam,
                                             OfficeIdParam,
                                             FiscalYearIdParam,
                                             ProgramTimeDurationParam,
                                             AmountFirstParam,
                                             AmountSecondParam,
                                             AmountThirdParam,
                                             ChaluAmountParam,
                                             PujiAmountParam,
                                             IsCancelParam,
                                             OfficeAmountParam,
                                             ContractAmountParam,
                                             CreatedDateParam,
                                             RequestReasonDocParam,
                                             StatusParam,
                                             MessageParam
                                         );

                msg = MessageParam.SqlValue.ToString();
                return msg;
            }
        }


        public string UpdateRequestDemandAmount(RequestGrantAmountModel model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var RequestGrantIdParam = new SqlParameter { ParameterName = "@RequestGrantAmountId", Value = model.RequestGrantAmountId };
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = model.ProgramId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var ProgramTimeDurationParam = new SqlParameter { ParameterName = "@ProgramTimeDuration", Value = model.ProgramTimeDuration };

                var AmountFirstParam = new SqlParameter { ParameterName = "@AmountFirst ", Value = model.AmountFirst.HasValue ? model.AmountFirst : 0 };
                var AmountSecondParam = new SqlParameter { ParameterName = "@AmountSecond ", Value = model.AmountSecond.HasValue ? model.AmountSecond : 0 };
                var AmountThirdParam = new SqlParameter { ParameterName = "@AmountThird ", Value = model.AmountThird.HasValue ? model.AmountThird : 0 };

                var ChaluAmountParam = new SqlParameter { ParameterName = "@ChaluAmount ", Value = model.ChaluAmount.HasValue ? model.ChaluAmount : 0 };
                var PujiAmountParam = new SqlParameter { ParameterName = "@PujiAmount ", Value = model.PujiAmount.HasValue ? model.PujiAmount : 0 };
                var IsCancelParam = new SqlParameter { ParameterName = "@IsCancel", Value = model.IsCanceled.HasValue ? model.IsCanceled : false };
                var OfficeAmountParam = new SqlParameter { ParameterName = "@OfficeAmount ", Value = model.OfficeAmount.HasValue ? model.OfficeAmount : 0 };

                var ContractAmountParam = new SqlParameter { ParameterName = "@ContractAmount", Value = model.ContractAmount.HasValue ? model.ContractAmount : 0 };

                var CreatedDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };

                var RequestReasonDocParam = new SqlParameter { ParameterName = "@RequestReasonDoc", Value = model.RequestReasonDoc };

                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = false };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };
                var result = db.Database.ExecuteSqlCommand(
                                             "exec UpdateRequestGrantAmount " +
                                             "@RequestGrantAmountId, " +
                                             "@ProgramId, " +
                                             "@OfficeId," +
                                             "@FiscalYearId," +
                                             "@ProgramTimeDuration," +
                                             "@AmountFirst," +
                                             "@AmountSecond," +
                                             "@AmountThird, " +
                                             "@ChaluAmount," +
                                             "@PujiAmount," +
                                             "@IsCancel," +
                                             "@OfficeAmount," +
                                             "@ContractAmount," +
                                             "@CreatedDate," + 
                                             "@RequestReasonDoc," +
                                             "@Status," +
                                             "@Message OUT",
                                             RequestGrantIdParam,
                                             ProgramIdParam,
                                             OfficeIdParam,
                                             FiscalYearIdParam,
                                             ProgramTimeDurationParam,
                                             AmountFirstParam,
                                             AmountSecondParam,
                                             AmountThirdParam,
                                             ChaluAmountParam,
                                             PujiAmountParam,
                                             IsCancelParam,
                                             OfficeAmountParam,
                                             ContractAmountParam,
                                             CreatedDateParam,
                                             RequestReasonDocParam,
                                             StatusParam,
                                             MessageParam
                                         );

                msg = MessageParam.SqlValue.ToString();
                return msg;
            }
        }

        public List<RequestGrantAmountModel> PopulateGrantRequestAmountDetail(int OfficeId, int ProgramId, int FyId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<RequestGrantAmountModel> RequestGrantAmountModelList = new List<RequestGrantAmountModel>();
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = ProgramId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = FyId };
                RequestGrantAmountModelList = db.Database.SqlQuery<RequestGrantAmountModel>("PopulateRequestGrantAmountDetail @OfficeId,@ProgramId,@FiscalYearId", OfficeIdParam, ProgramIdParam, FiscalYearIdParam).ToList();
                return RequestGrantAmountModelList;
            }

        }

    }
}