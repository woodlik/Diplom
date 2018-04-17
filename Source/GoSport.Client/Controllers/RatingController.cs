using GoSport.Data.Models;
using GoSport.Services.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoSport.Client.Controllers
{
    public class RatingController : BaseController
    {
        ISportCenterService sportCenterService;

        public RatingController(
            ISportCategoryService sportCategories,
            IAddressService addressService,
            ISportCategoryService categoryService,
            ISportCenterService sportCenterService)
            : base(sportCategories, addressService, categoryService)
        {
            this.sportCenterService = sportCenterService;
        }

        [Authorize]
        [HttpPost]
        public JsonResult RateSportCenter(int sportCenterId, string ratingType, int value)
        {
            if (!ModelState.IsValid) throw new HttpException("Invalid parameters passed");

            var newValue = sportCenterService.AddRatingToSportCenter(sportCenterId, User.Identity.GetUserId(), ratingType, value);

            return Json(new { value = newValue });
        }
    }
}