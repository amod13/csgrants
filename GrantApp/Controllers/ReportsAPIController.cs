using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using GrantApp.Areas.Admin;
using GrantApp.Models;
using GrantApp.Services;

namespace GrantApp.Controllers
{

    
    public class ReportsAPIController : ApiController
    {
        [HttpGet]
        [Route("api/ReportsAPI/download-excel-special")]
        public IHttpActionResult DownloadExcel()
        {
            ReportModel model = new ReportModel();
            model.AnusuchiOneViewModelForFMList = new List<AnusuchiOneViewModelForFM>();
            model.AnusuchiOneViewModelForFMList = GetAnusuchiSpeicalForFM();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                //worksheet.Cell("A1").InsertTable(model.AnusuchiOneViewModelForFMList.AsEnumerable());
                // Add data to the worksheet
                //worksheet.Cell("A1").Value ="क्रस";
                //worksheet.Cell("B1").Value = "आ.व";

                worksheet.FirstRow().FirstCell().InsertTable(model.AnusuchiOneViewModelForFMList.AsEnumerable());
                worksheet.Tables.FirstOrDefault().SetShowAutoFilter(false);
                //var headerRowRange = worksheet.Row(1);
                //headerRowRange.Delete();




                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                // Return the Excel file as a HttpResponseMessage
                var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Anusuchireport.xlsx"
                };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return ResponseMessage(result);
            }
        }

        [HttpGet]
        [Route("api/ReportsAPI/download-excel-comp")]
        public IHttpActionResult DownloadExcelFour()
        {
            ReportModel model = new ReportModel();
            model.AnusuchiComplementryViewModelForFMList = new List<AnusuchiComplementryViewModelForFM>();
            model.AnusuchiComplementryViewModelForFMList = GetAnusuchiComplementryForFM();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                //worksheet.Cell("A1").InsertTable(model.AnusuchiOneViewModelForFMList.AsEnumerable());
                // Add data to the worksheet
                //worksheet.Cell("A1").Value ="क्रस";
                //worksheet.Cell("B1").Value = "आ.व";

                worksheet.FirstRow().FirstCell().InsertTable(model.AnusuchiOneViewModelForFMList.AsEnumerable());
                worksheet.Tables.FirstOrDefault().SetShowAutoFilter(false);
                //var headerRowRange = worksheet.Row(1);
                //headerRowRange.Delete();




                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                // Return the Excel file as a HttpResponseMessage
                var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Anusuchireport.xlsx"
                };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return ResponseMessage(result);
            }
        }

        public List<AnusuchiOneViewModelForFM> GetAnusuchiSpeicalForFM()
        {
            ReportModel model = new ReportModel();
            ReportServices services = new ReportServices();
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.AnusuchiOneViewModelForFMList = new List<AnusuchiOneViewModelForFM>();
            model.AnusuchiOneViewModelForFMList = services.GetAnusuchiTwoForArth(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return model.AnusuchiOneViewModelForFMList;


        }
        public List<AnusuchiComplementryViewModelForFM> GetAnusuchiComplementryForFM()
        {
            ReportModel model = new ReportModel();
            ReportServices services = new ReportServices();
            int FYearId = FunctionClass.GetStaticFiscalYearFromPhaseNumber(model.ProgramPhaseNumber);
            model.AnusuchiComplementryViewModelForFMList = new List<AnusuchiComplementryViewModelForFM>();
            model.AnusuchiComplementryViewModelForFMList = services.GetAnusuchiOneForArthFM(model.ProvinceIdSearch, model.ProgramPhaseNumber, model.GrantTypeIdSearch, FYearId);//1 is not submited, 2 is submited
            model.ViewBagGrantTypeId = model.GrantTypeIdSearch;
            return model.AnusuchiComplementryViewModelForFMList;


        }



    }
}
