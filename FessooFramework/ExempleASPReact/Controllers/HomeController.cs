using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExempleASPReact.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["SiteName"] = _Data._1_Information.Name;
            ViewData["SiteMainTitle"] = _Data._1_Information.MainTitle;
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View();
        }
    }
}
