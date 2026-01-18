using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Services;

namespace GrantApp.Controllers
{
    public class FileCheckController : Controller
    {
        private readonly FileServices _fileService;

        public FileCheckController()
        {
            _fileService = new FileServices();
        }

        public ActionResult Index()
        {
            var files = _fileService.GetFiles();
            return View(files);
        }
    }
}