using GoSport.Client.ViewModels.Users;
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
    [Authorize]
    public class UserController:BaseController
    {
        IUserService userService;

        public UserController(
            ISportCategoryService sportCategories, 
            IAddressService addressService, 
            ISportCategoryService categoryService,
            IUserService userService)
            :base(sportCategories,addressService,categoryService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public new  ActionResult Profile()
        {
            var currentUserId = User.Identity.GetUserId();
            var userFromDb = userService.GetUserById(currentUserId);
            var model = new UserProfileViewModel();

            if (userFromDb != null)
            {
                model = Mapper.Map<UserProfileViewModel>(userFromDb);
            }

            model.AvatarUrl = String.Format("/Content/Avatars/{0}.jpg", this.User.Identity.GetUserName());

            return View(model);        
        }
    }
}