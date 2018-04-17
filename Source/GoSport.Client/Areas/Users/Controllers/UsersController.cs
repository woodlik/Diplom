using GoSport.Client.Controllers;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Client.ViewModels.Users;
using GoSport.Data.Models;
using GoSport.Services.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoSport.Client.Areas.Users.Controllers
{
    [Authorize]
    public class UsersController: BaseController
    {
        IUserService usersService;
        IMessageService messageService;

        public UsersController(
            ISportCategoryService sportCategories,
            IAddressService addressService,
            ISportCategoryService categoryService,
            ISportCenterService sportCenterService,
            IUserService usersService,
            IMessageService messageService)
            : base(sportCategories, addressService, categoryService)
        {
            this.usersService=usersService;
            this.messageService = messageService;
        }

        [HttpGet]
        public ActionResult All()
        {
            var users = usersService.GetAll().To<UserProfileViewModel>().ToList();

            return View(users);
        }

        [HttpGet]
        public new ActionResult Profile(string id)
        {
            var userFromDb = usersService.GetUserById(id);
            var visitorId = User.Identity.GetUserId();
            //var visitor = usersService.GetUserById(id);
            var model = new UserProfileViewModel();
            var allMessages= new List<Message>();

            if (userFromDb != null && id != visitorId)
            {
                allMessages = messageService.GetAllMessages(id, visitorId).ToList();
            }

            model = Mapper.Map<UserProfileViewModel>(userFromDb);
            model.AvatarUrl = String.Format("/Content/Avatars/{0}.jpg", userFromDb.UserName);
            model.Messages = allMessages;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMessage(string recieverId, string content)
        {
            if (ModelState.IsValid && recieverId != null && content != null && content != string.Empty)
            {
                var authorId = User.Identity.GetUserId();
                messageService.SendMessage(recieverId, authorId, content);
                var allMessages = messageService.GetAllMessages(recieverId,authorId).ToList();
                
                return PartialView("_AllMessagesPartial", allMessages);
            }

            throw new HttpException(400, "Invalid comment");
        }
    }
}