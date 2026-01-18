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
    public class AdhuroApuroServices
    {

        public List<AdhuroApuroViewModel> SUP_PupulateAdhuroApuroProjects(int OfficeId )
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<AdhuroApuroViewModel> PopulateList = new List<AdhuroApuroViewModel>();
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };//List All 
                PopulateList = db.Database.SqlQuery<AdhuroApuroViewModel>("SUP_PupulateAdhuroApuroProjects @OfficeId", OfficeIdParam).ToList();
                return PopulateList;
            }
        }

        public List<AdhuroApuroViewModel> SUP_PupulateCompletedProjects(int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
               
                List<AdhuroApuroViewModel> PopulateList = new List<AdhuroApuroViewModel>();
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };//List All 
                PopulateList = db.Database.SqlQuery<AdhuroApuroViewModel>("SUP_PupulateCompletedProjects @OfficeId", OfficeIdParam).ToList();
                return PopulateList;
            }
        }


        public AdhuroApuroGrantRequest SUP_AdhuroApuroGrantRequest(int programId,int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                AdhuroApuroGrantRequest PopulateData = new AdhuroApuroGrantRequest();
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = programId };
             
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                PopulateData = db.Database.SqlQuery<AdhuroApuroGrantRequest>("SUP_AdhuroApuroGrantRequest @ProgramId,@OfficeId", ProgramIdParam, OfficeIdParam).FirstOrDefault();
                return PopulateData;
            }
        }

        public AdhuroApuroReportViewModel SUP_GetAdhuroApuroDetailById(int id)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                AdhuroApuroReportViewModel PopulateData = new AdhuroApuroReportViewModel();
                var IdParam = new SqlParameter { ParameterName = "@Id", Value = id };

                PopulateData = db.Database.SqlQuery<AdhuroApuroReportViewModel>("SUP_GetAdhuroApuroDetailById @Id", IdParam).FirstOrDefault();
                return PopulateData;
            }
        }


        public RemainingBhuktaniGrantReportViewModel SUP_GetRemainingBhuktaniDetailById(int id)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                RemainingBhuktaniGrantReportViewModel PopulateData = new RemainingBhuktaniGrantReportViewModel();
                var IdParam = new SqlParameter { ParameterName = "@Id", Value = id };

                PopulateData = db.Database.SqlQuery<RemainingBhuktaniGrantReportViewModel>("SUP_GetRemainingBhuktaniDetailById @Id", IdParam).FirstOrDefault();
                return PopulateData;
            }
        }


        public RemainingBhuktaniGrantRequest SUP_RemainingBhuktaniGrantRequest(int programId, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                RemainingBhuktaniGrantRequest PopulateData = new RemainingBhuktaniGrantRequest();
                var ProgramIdParam = new SqlParameter { ParameterName = "@ProgramId", Value = programId };

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                PopulateData = db.Database.SqlQuery<RemainingBhuktaniGrantRequest>("SUP_RemainingBhuktaniGrantRequest @ProgramId,@OfficeId", ProgramIdParam, OfficeIdParam).FirstOrDefault();
                return PopulateData;
            }
        }


        


        public string InsertAdhuroApuroGrantRequest(AdhuroApuroGrantRequest model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@OfficeId", model.OfficeId),
            new SqlParameter("@ProgramId", model.ProgramId),
            new SqlParameter("@PhaseStatus", model.PhaseStatus),
            new SqlParameter("@AdditionalFundRequested", model.AdditionalFundRequested),
            new SqlParameter("@AmountCoveredByLocalLevel", model.AmountCoveredByLocalLevel),
            new SqlParameter("@PhysicalProgress", model.PhysicalProgress),
            new SqlParameter("@FinancialProgress", model.FinancialProgress),
            new SqlParameter("@TotalAllocation", model.TotalAllocation),
            new SqlParameter("@TotalExpenditure", model.TotalExpenditure),

            // File Paths
            new SqlParameter("@ContractAgreementFilePath", model.ContractAgreementFilePath ?? (object)DBNull.Value),
            new SqlParameter("@PaymentVoucherFilePath", model.PaymentVoucherFilePath ?? (object)DBNull.Value),
            new SqlParameter("@ProjectImagesFilePath", model.ProjectImagesFilePath ?? (object)DBNull.Value),
            new SqlParameter("@ExtensionLetterFilePath", model.ExtensionLetterFilePath ?? (object)DBNull.Value),
            new SqlParameter("@CommitmentLetterFilePath", model.CommitmentLetterFilePath ?? (object)DBNull.Value),
            new SqlParameter("@ExecutiveDecisionFilePath", model.ExecutiveDecisionFilePath ?? (object)DBNull.Value),

            new SqlParameter("@CreatedDate", model.CreatedDate),

            // Output Parameter
            new SqlParameter
            {
                ParameterName = "@Message",
                DbType = DbType.String,
                Size = 50,
                Direction = System.Data.ParameterDirection.Output
            }
        };

                // Execute stored procedure
                db.Database.ExecuteSqlCommand(
                    "EXEC InsertAdhuroApuroGrantRequest " +
                    "@OfficeId, @ProgramId, @PhaseStatus, @AdditionalFundRequested, " +
                    "@AmountCoveredByLocalLevel, @PhysicalProgress, @FinancialProgress, " +
                    "@TotalAllocation, @TotalExpenditure, @ContractAgreementFilePath, " +
                    "@PaymentVoucherFilePath, @ProjectImagesFilePath, @ExtensionLetterFilePath, " +
                    "@CommitmentLetterFilePath, @ExecutiveDecisionFilePath, @CreatedDate, @Message OUT",
                    parameters.ToArray()
                );

                msg = parameters.Last().Value.ToString();
                return msg;
            }
        }


        public string InsertRemainingBhuktaniGrantRequest(RemainingBhuktaniGrantRequest model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@OfficeId", model.OfficeId),
            new SqlParameter("@ProgramId", model.ProgramId),
            new SqlParameter("@PhaseStatus", model.PhaseStatus),
            new SqlParameter("@AdditionalAmountRequested", model.AdditionalAmountRequested),
            new SqlParameter("@AmountToBeCoveredByLevel", model.AmountToBeCoveredByLevel),
            new SqlParameter("@PhysicalProgress", model.PhysicalProgress),
            new SqlParameter("@FinancialProgress", model.FinancialProgress),
            new SqlParameter("@TotalAllocation", model.TotalAllocation),
            new SqlParameter("@TotalExpenditure", model.TotalExpenditure),

            // File Paths
             new SqlParameter("@ExtensionLetterPath", model.ExtensionLetterPath ?? (object)DBNull.Value),
            new SqlParameter("@ContractAgreementPath", model.ContractAgreementPath ?? (object)DBNull.Value),
            new SqlParameter("@PaymentVouchersPath", model.PaymentVouchersPath ?? (object)DBNull.Value),
            new SqlParameter("@ProjectImagesPath", model.ProjectImagesPath ?? (object)DBNull.Value),
       
            new SqlParameter("@LiabilityCertificationPath", model.LiabilityCertificationPath ?? (object)DBNull.Value),
            new SqlParameter("@ExecutiveDecisionPath", model.ExecutiveDecisionPath ?? (object)DBNull.Value),

            new SqlParameter("@CreatedDate", model.CreatedDate),

            // Output Parameter
            new SqlParameter
            {
                ParameterName = "@Message",
                DbType = DbType.String,
                Size = 50,
                Direction = System.Data.ParameterDirection.Output
            }
        };

                // Execute stored procedure
                db.Database.ExecuteSqlCommand(
                    "EXEC InsertRemainingBhuktaniGrantRequest " +
                    "@OfficeId, @ProgramId, @PhaseStatus, @AdditionalAmountRequested, " +
                    "@AmountToBeCoveredByLevel, @PhysicalProgress, @FinancialProgress, " +
                    "@TotalAllocation, @TotalExpenditure, @ExtensionLetterPath,@ContractAgreementPath, " +
                    "@PaymentVouchersPath, @ProjectImagesPath,@LiabilityCertificationPath, " +
                    "@ExecutiveDecisionPath, @CreatedDate, @Message OUT",
                    parameters.ToArray()
                );

                msg = parameters.Last().Value.ToString();
                return msg;
            }
        }


    }
}