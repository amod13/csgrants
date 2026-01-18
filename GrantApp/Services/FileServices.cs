using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using GrantApp.Models;

namespace GrantApp.Services
{
    public class FileServices
    {

        private readonly string _connectionString;
        private readonly string _folderPath;

        public FileServices()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            _folderPath = Path.Combine(basePath, "RequiredDocs");
        }

        public List<FileCheckResult> GetFiles()
        {
            List<int> subProgramIds = GetSubProgramIds();
            List<FileCheckResult> results = new List<FileCheckResult>();

            foreach (int subProgramId in subProgramIds)
            {
                if (!Directory.Exists(_folderPath))
                    continue;

                var matchingFiles = Directory.GetFiles(_folderPath, $"{subProgramId}*")
                                         .Select(file => file.Replace(AppDomain.CurrentDomain.BaseDirectory, "").TrimStart('\\', '/'))
                                         .ToList();

                foreach (var file in matchingFiles)
                {
                    results.Add(new FileCheckResult
                    {
                        SubProgramId = subProgramId,
                        FileName = Path.GetFileName(file)
                    });
                }
            }

            return results;
        }

        private List<int> GetSubProgramIds()
        {
            List<int> subProgramIds = new List<int>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT subprogramid FROM dbo.SubProgramMaster 
                WHERE SubProgramId NOT IN (SELECT SubProgramId FROM dbo.DocumentRequirementsUpload) 
                AND PhaseStatus = 7";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    subProgramIds.Add(reader.GetInt32(0));
                }
            }

            return subProgramIds;
        }

    }
}