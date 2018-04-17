using AutoMapper;
using GoSport.Client.Infrastructure;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Client.ViewModels;
using GoSport.Services.Contracts;
using MvcTemplate.Services.Web;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace GoSport.Client.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string userAvatarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\Avatars\");
        protected string sportCenterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\SportCenters\");

        protected ISportCategoryService sportCategories;
        protected IAddressService addressService;
        protected ISportCategoryService categoryService;

        public BaseController(ISportCategoryService sportCategories, IAddressService addressService, ISportCategoryService categoryService)
        {
            this.sportCategories = sportCategories;
            this.addressService = addressService;
            this.categoryService = categoryService;
        }

        public ICacheService Cache { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContenxt = filterContext.HttpContext;
            AddDataToCache();
        }

        protected void AddDataToCache()
        {
            var categories = this.HttpContext.Cache[Constants.CacheCategoriesName];
            var cities = this.HttpContext.Cache[Constants.CacheCitiesName];

            if (categories == null || cities == null)
            {
                this.HttpContext.Cache.Insert(
                   Constants.CacheCategoriesName,
                   sportCategories.AllNames().ToList(),
                   null,
                   DateTime.Now.AddSeconds(10),
                   TimeSpan.Zero
                   );


                this.HttpContext.Cache.Insert(
                   Constants.CacheCitiesName,
                   addressService.AllCities().To<AddressViewModel>().ToList(),
                   null,
                   DateTime.Now.AddSeconds(10),
                   TimeSpan.Zero
                   );

                categories = this.HttpContext.Cache[Constants.CacheCategoriesName];
                cities = this.HttpContext.Cache[Constants.CacheCitiesName];
            }

            ViewBag.Categories = categories;
            ViewBag.Cities = cities;
        }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}