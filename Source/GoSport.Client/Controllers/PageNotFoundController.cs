using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoSport.Client.Controllers
{
    public class PageNotFoundController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
    }
}