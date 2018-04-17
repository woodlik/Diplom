using GoSport.Client.Infrastructure.Mapping;
using GoSport.Client.ViewModels;
using GoSport.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoSport.Client.Controllers
{
    public class AddressController :BaseController
    {
        public AddressController(ISportCategoryService sportCategories, IAddressService addressService, ISportCategoryService categoryService)
            :base(sportCategories,addressService,categoryService)
        { }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllNeighbours()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllNeighbours(string city)
        {
            if (!ModelState.IsValid) throw new HttpException("Invalid parameters passed");

            var neighbours = this.HttpContext.Cache[string.Format("Neighbours by city: {0}", city)];

            if (neighbours == null)
            {
                this.HttpContext.Cache.Insert(
                    string.Format("Neighbours by city: {0}", city),
                    addressService.GetByCity(city).To<AddressViewModel>().ToList(),
                    null,
                    DateTime.Now.AddMinutes(60),
                    TimeSpan.Zero
                    );

                neighbours = this.HttpContext.Cache[string.Format("Neighbours by city: {0}", city)];
            }


            return Json(neighbours);
        }
    }
}