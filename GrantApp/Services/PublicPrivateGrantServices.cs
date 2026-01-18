using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Areas.Admin.Models;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;



namespace GrantApp.Services
{
    public class PublicPrivateGrantServices
    {

        public List<PublicPrivateGrantViewModel> SUP_PupulatePublicPrivateGrantProjects(int OfficeId )
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<PublicPrivateGrantViewModel> PopulateList = new List<PublicPrivateGrantViewModel>();
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };//List All 
                PopulateList = db.Database.SqlQuery<PublicPrivateGrantViewModel>("SUP_PopulatePublicPrivateGrantProjects @OfficeId", OfficeIdParam).ToList();
                return PopulateList;
            }
        }


        public PublicPrivateGrantRequest PopulatePublicPrivateGrantProjectById(int id, int OfficeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                PublicPrivateGrantRequest  SubProgramSetupList = new PublicPrivateGrantRequest();
                var IdParam = new SqlParameter { ParameterName = "@Id", Value = id };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                SubProgramSetupList = db.Database.SqlQuery<PublicPrivateGrantRequest>("PopulatePublicPrivateGrantProjectById @Id,@OfficeId", IdParam,OfficeIdParam).FirstOrDefault();
                return SubProgramSetupList;
            }
        }


        public bool CheckIfOfficeHasAppliedForPublicGrantForPhase(int officeId, int phaseStatus)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string query = @"
                                SELECT CASE 
                                           WHEN COUNT(*) > 0 THEN 1 
                                           ELSE 0 
                                       END 
                                FROM dbo.PublicPrivateGrantRequest
                                WHERE OfficeId = @p0 AND PhaseStatus = @p1";

                int result = db.Database.SqlQuery<int>(query, officeId, phaseStatus).FirstOrDefault();

                return result > 0;
            }
        }


        public string InsertPublicPrivateGrantRequest(PublicPrivateGrantRequest model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@OfficeId", model.OfficeId),
            new SqlParameter("@PhaseStatus", model.PhaseStatus),
            new SqlParameter("@ProgramName", model.ProgramName),
            new SqlParameter("@ProgramSector", model.ProgramSector),
            new SqlParameter("@TimeDuration", model.TimeDuration),
            new SqlParameter("@TotalCost", model.TotalCost),
            new SqlParameter("@StateContribution", model.StateContribution),
            new SqlParameter("@PrivateContribution", model.PrivateContribution),
            new SqlParameter("@VGFFund", model.VGFFund),
            new SqlParameter("@RequestedAmount", model.RequestedAmount),

            // File Paths
            new SqlParameter("@FeasibilityStudy", model.FeasibilityStudy ?? (object)DBNull.Value),
            new SqlParameter("@EnvironmentalReport", model.EnvironmentalReport ?? (object)DBNull.Value),
            new SqlParameter("@PriorityDetails", model.PriorityDetails ?? (object)DBNull.Value),
            new SqlParameter("@LocalGovtDecision", model.LocalGovtDecision ?? (object)DBNull.Value),
            new SqlParameter("@DeclarationSchedule", model.DeclarationSchedule ?? (object)DBNull.Value),

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
                    "EXEC InsertPublicPrivateGrantRequest " +
                    "@OfficeId, @PhaseStatus, @ProgramName, @ProgramSector, @TimeDuration, " +
                    "@TotalCost, @StateContribution, @PrivateContribution, " +
                    "@VGFFund, @RequestedAmount, @FeasibilityStudy, " +
                    "@EnvironmentalReport, @PriorityDetails, @LocalGovtDecision, " +
                    "@DeclarationSchedule, @CreatedDate, @Message OUT",
                    parameters.ToArray()
                );

                msg = parameters.Last().Value.ToString();
                return msg;
            }
        }

        public string UpdatePublicPrivateGrantRequest(PublicPrivateGrantRequest model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;

                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@Id", model.Id),
            new SqlParameter("@OfficeId", model.OfficeId),
            new SqlParameter("@PhaseStatus", model.PhaseStatus),
            new SqlParameter("@ProgramName", model.ProgramName),
            new SqlParameter("@ProgramSector", model.ProgramSector),
            new SqlParameter("@TimeDuration", model.TimeDuration),
            new SqlParameter("@TotalCost", model.TotalCost),
            new SqlParameter("@StateContribution", model.StateContribution),
            new SqlParameter("@PrivateContribution", model.PrivateContribution),
            new SqlParameter("@VGFFund", model.VGFFund),
            new SqlParameter("@RequestedAmount", model.RequestedAmount),

            // File Paths
            new SqlParameter("@FeasibilityStudy", model.FeasibilityStudy ?? (object)DBNull.Value),
            new SqlParameter("@EnvironmentalReport", model.EnvironmentalReport ?? (object)DBNull.Value),
            new SqlParameter("@PriorityDetails", model.PriorityDetails ?? (object)DBNull.Value),
            new SqlParameter("@LocalGovtDecision", model.LocalGovtDecision ?? (object)DBNull.Value),
            new SqlParameter("@DeclarationSchedule", model.DeclarationSchedule ?? (object)DBNull.Value),

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
                    "EXEC UpdatePublicPrivateGrantRequest " +
                    "@Id, @OfficeId, @PhaseStatus, @ProgramName, @ProgramSector, @TimeDuration, " +
                    "@TotalCost, @StateContribution, @PrivateContribution, " +
                    "@VGFFund, @RequestedAmount, @FeasibilityStudy, " +
                    "@EnvironmentalReport, @PriorityDetails, @LocalGovtDecision, " +
                    "@DeclarationSchedule, @CreatedDate, @Message OUT",
                    parameters.ToArray()
                );

                msg = parameters.Last().Value.ToString();
                return msg;
            }
        }




    }
}