using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExempleASPReactDAL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ExempleASPReact.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["SiteName"] = DataHelper.Name;
            ViewData["SiteMainTitle"] = DataHelper.MainTitle;
            ViewBag.SiteMainTitle = DataHelper.MainTitle;

            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
