using GoSport.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoSport.Client.ViewModels.SportCenters;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System.IO;
using GoSport.Client.Infrastructure;
using Microsoft.AspNet.Identity;
using GoSport.Client.ViewModels.Comments;

namespace GoSport.Client.Controllers
{
    public class SportCenterController : BaseController
    {
        ISportCenterService sportCenterService;

        public SportCenterController(
            ISportCategoryService sportCategories,
            IAddressService addressService,
            ISportCategoryService categoryService,
            ISportCenterService sportCenterService)
            : base(sportCategories, addressService, categoryService)
        {
            this.sportCenterService = sportCenterService;
        }

        public ActionResult SportCenters()
        {
            var all = sportCenterService.All().To<SportCenterViewModel>().ToList();

            Response.Write("All sport centers are: " + all.Count());
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {


            var model = Mapper.Map<SportCenterViewModel>(sportCenterService.GetById(id));

            model.Images = ImageHelper.SanitizeImageUrls(sportCenterService.GetImagesForSportCenter(model.Name).ToArray());

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddSportCenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sportCenter = Mapper.Map<SportCenter>(model);

                sportCenter = sportCenterService.Create(sportCenter);

                // address
                int addressId;
                if (int.TryParse(model.Neighborhood, out addressId))
                {
                    addressService.AddAddressForSportCenter(sportCenter.Name, addressId);
                }

                // categories
                if (model.SportCategories != null)
                {
                    var categoriesNames = Array
                    .ConvertAll(model.SportCategories.Split(','), p => p.Trim())
                    .Where(x => !string.IsNullOrEmpty(x)).ToList();

                    categoryService.AddCategoriesForSportCenter(categoriesNames, sportCenter.Name);
                }

                this.AddImages(model);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private void AddImages(AddSportCenterViewModel model)
        {
            var imagesUrls = new List<string>();

            if (model.UplodadedImage1 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage1, model.Name, 1);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage2 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage2, model.Name, 2);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage3 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage3, model.Name, 3);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage4 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage4, model.Name, 4);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage5 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage5, model.Name, 5);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage6 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage6, model.Name, 6);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage7 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage7, model.Name, 7);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage8 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage8, model.Name, 8);
                imagesUrls.Add(image);
            }
            if (model.UplodadedImage9 != null)
            {
                var image = ImageHelper.SaveImage(this.sportCenterPath, model.UplodadedImage9, model.Name, 9);
                imagesUrls.Add(image);
            }

            sportCenterService.AddImagesToSportCenter(model.Name, imagesUrls);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(int sportCenterId, string content)
        {
            if (ModelState.IsValid && sportCenterId != 0 && content != null && content != string.Empty)
            {
                sportCenterService.AddCommentToSportCenter(sportCenterId, User.Identity.GetUserId(), content);
                var sportCenter = sportCenterService.GetById(sportCenterId);
                //var model = new List<CommentViewModel>();
                var allComments = sportCenter.Comments.ToList();

                //return RedirectToAction("Details", new { @id = sportCenterId });
                return PartialView("_AllCommentsPartial", allComments);
            }

            throw new HttpException(400, "Invalid comment");
        }
    }
}