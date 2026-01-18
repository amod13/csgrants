using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramSetups.Model;

namespace ProgramSetups
{
    public static class ProgramPhaseSetup
    {

        public static string AddProgramPhaseDetails(ProgramPhaseStatus model)
        {
            using (SetupDBContext db = new SetupDBContext())
            {
                try
                {
                    db.ProgramPhaseStatus.Add(model);
                    int saveResult= db.SaveChanges();
                    if (saveResult == 1)
                        return "Save Successfully";
                    else
                        return "Error";
                }
                catch (Exception ex)
                {

                    return ex.Message.ToString();
                }
                

            }

        }


        public static string EditProgramPhaseDetails(ProgramPhaseStatus model)
        {
            using (SetupDBContext db = new SetupDBContext())
            {
                var objProgramPhase = db.ProgramPhaseStatus.Find(model.ProgramPhaseStatausId);
                if(objProgramPhase is null)
                {
                    return "Not found";
                }
                try
                {
                    db.ProgramPhaseStatus.Add(model);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return "Updated";
                }
                catch (Exception e)
                {

                    return e.Message.ToString();
                }
               



            }

        }

       

    }
}
