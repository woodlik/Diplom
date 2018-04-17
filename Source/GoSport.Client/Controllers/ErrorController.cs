namespace Exam.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult NotFound()
        {
            return this.View();
        }
    }
}
