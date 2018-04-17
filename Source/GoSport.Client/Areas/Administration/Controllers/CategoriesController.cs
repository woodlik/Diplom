using GoSport.Client.Areas.Administration.ViewModels;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Services.Contracts;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GoSport.Client.Areas.Administration.Controllers
{
    public class CategoriesController:AdminController
    {
        public CategoriesController(
           ISportCategoryService sportCategories,
           IAddressService addressService,
           ISportCategoryService categoryService,
           ISportCenterService sportCenterService)
            : base(sportCategories, addressService, categoryService, sportCenterService)
        {

        }

        public ActionResult Index()
        {
            var model = categoryService
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .To<AdminSportCategoryViewModel>()
                .ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, AdminSportCategoryViewModel model)
        {
            categoryService.Create(model.Name);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = this.categoryService.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(AdminSportCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.categoryService.UpdateById(model.Id,model.Name);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var isDeleted = this.categoryService.DeleteById((int)id);

            if (!isDeleted)
            {
                return HttpNotFound();
            }

            return View();
        }
    }
}