using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrantApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace GrantApp.Services
{
    public class ProgramServices
    {

        #region Grant Type

        public List<GrantTypeViewModel> PopulateGrantType()
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<GrantTypeViewModel> GrantTypeViewModelList = new List<GrantTypeViewModel>();
                //var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                GrantTypeViewModelList = db.Database.SqlQuery<GrantTypeViewModel>("PopulateGrantType").ToList();
                return GrantTypeViewModelList;
            }
        }

        #endregion



        public List<ProgramSetup> PopulateProgram(int GrantTypeId)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                List<ProgramSetup> ProgramSetupList = new List<ProgramSetup>();
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = GrantTypeId };
                ProgramSetupList = db.Database.SqlQuery<ProgramSetup>("PopulateProgram @GrantTypeId", GrantTypeIdParam).ToList();
                return ProgramSetupList;
            }
        }



        public int InsertProgram(ProgramSetup _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                _Model.Status = true;
                var MainSectionIdParam = new SqlParameter { ParameterName = "@MainSectionId", Value = _Model.MainSectionId };
                var ProgramNameParam = new SqlParameter { ParameterName = "@ProgramName", Value = _Model.ProgramName };

                var createdDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = 1 };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = _Model.GrantTypeId };
                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = _Model.Status ? false : _Model.Status };


                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var PrimaryIdParam = new SqlParameter
                {
                    ParameterName = "@PrimaryId",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec InsertProgram @MainSectionId, @ProgramName,@CreatedDate,@CreatedBy,@GrantTypeId,@Status,@Message OUT,@PrimaryId OUT",
                    MainSectionIdParam, ProgramNameParam, createdDateParam, CreatedByParam, GrantTypeIdParam, StatusParam, MessageParam, PrimaryIdParam);
                int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                //msg = MessageParam.SqlValue.ToString();

                return UserRegistrationIdValue;

            }
        }

        public string UpdateProgram(ProgramSetup _Model)
        {
            using (GrantAppDBEntities db = new GrantAppDBEntities())
            {
                string msg = string.Empty;
                var MainSectionIdParam = new SqlParameter { ParameterName = "@MainSectionId", Value = _Model.MainSectionId };
                var ProgramIdParam = new SqlParameter { ParameterName = "ProgramId", Value = _Model.ProgramId };
                var ProgramNameParam = new SqlParameter { ParameterName = "@ProgramName", Value = _Model.ProgramName };
                var createdDateParam = new SqlParameter { ParameterName = "@CreatedDate", Value = DateTime.Now };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = 1 };
                var GrantTypeIdParam = new SqlParameter { ParameterName = "@GrantTypeId", Value = _Model.GrantTypeId };
                var StatusParam = new SqlParameter { ParameterName = "@Status", Value = _Model.Status ? false : _Model.Status };


                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec UpdateProgram @MainSectionId,@ProgramId,@ProgramName,@CreatedDate,@CreatedBy,@GrantTypeId,@Status,@Message OUT",
                    MainSectionIdParam, ProgramIdParam, ProgramNameParam, createdDateParam, CreatedByParam, GrantTypeIdParam, StatusParam, MessageParam);
                //int UserRegistrationIdValue = (int)PrimaryIdParam.Value;
                msg = MessageParam.SqlValue.ToString();

                //return UserRegistrationIdValue;
                return msg;
            }
        }




    }
}