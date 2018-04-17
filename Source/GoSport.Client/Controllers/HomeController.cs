using GoSport.Client.Infrastructure;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Client.ViewModels.SportCenters;
using GoSport.Services.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoSport.Client.Controllers
{
    public class HomeController : BaseController
    {
        private ISportCenterService sportCenterService;
        private IUserService usersService;

        public HomeController(
            ISportCategoryService sportCategories,
            IAddressService addressService,
            ISportCategoryService categoryService,
            ISportCenterService sportCenterService,
            IUserService usersService)
            : base(sportCategories, addressService, categoryService)
        {
            this.sportCenterService = sportCenterService;
            this.usersService = usersService;

            ViewBag.AllSportCentersCount = sportCenterService.All().Count();
            ViewBag.ItemsPerPage = 4;
        }

        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            if (id <= 0) id = 1;

            var model = this.HttpContext.Cache[string.Format("Sportcenters by page: {0}", id)];

            if (model == null)
            {
                model = sportCenterService.All()
                        .OrderByDescending(x => x.Rating.Total)
                        .Skip((id-1)* (int)ViewBag.ItemsPerPage)
                        .Take((int)ViewBag.ItemsPerPage)
                        .To<SportCenterViewModel>()
                        .ToList();

                this.HttpContext.Cache.Insert(
                    string.Format("Sportcenters by page: {0}", id),
                    model,
                    null,
                    DateTime.Now.AddSeconds(10),
                    TimeSpan.Zero
                    );
            }

                foreach (var sportCenter in (IEnumerable<SportCenterViewModel>)model)
            {
                sportCenter.Images = ImageHelper.SanitizeImageUrls(sportCenterService.GetImagesForSportCenter(sportCenter.Name).ToArray());
            }

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}