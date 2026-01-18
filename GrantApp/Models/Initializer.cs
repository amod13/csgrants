using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using GrantApp.Areas.Admin.Models;


namespace GrantApp.Models
{
    public class Initializer
    {

    }

    public class GrantAppDBEntities : DbContext
    {


        public DbSet<ProgramSetup> ProgramSetup { get; set; }
        public DbSet<SubProgramMaster> SubProgramMaster { get; set; }

        public DbSet<OfficeDetails> OfficeDetails { get; set; }

        public DbSet<NewProgramInitiation> NewProgramInitiation { get; set; }

        public DbSet<ProfileUpdates> ProfileUpdates { get; set; }

        public DbSet<DocumentRequirementsUpload> DocumentRequirementsUpload { get; set; }
        public DbSet<DocumentsRequirements> DocumentsRequirements { get; set; }
        //public DbSet<AspNetCustomUserRoles> AspNetCustomUserRoles { get; set; }
        //public DbSet<OfficeRecord> OfficeSetupModel { get; set; }
        public DbSet<ApplicationCompletionStatus> ApplicationCompletionStatus { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}