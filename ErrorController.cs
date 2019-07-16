using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using System.Web.Mvc;
using Fonery.Model;
using Fonery.DAL;
using System.Collections.Generic;
using System.Linq;

namespace CementPSI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error404()
        {
            return View("Error");
        }

        public ActionResult Error500()
        {
            return View("Error");
        }
    }
}
