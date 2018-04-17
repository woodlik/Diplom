using GoSport.Client.Areas.Administration.ViewModels;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
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
    public class AddressesController : AdminController
    {
        public AddressesController(
           ISportCategoryService sportCategories,
           IAddressService addressService,
           ISportCategoryService categoryService,
           ISportCenterService sportCenterService)
            : base(sportCategories, addressService, categoryService, sportCenterService)
        {

        }

        //public ActionResult Index()
        //{
        //    var model = addressService
        //        .All()
        //        .OrderByDescending(x => x.CreatedOn)
        //        .To<AdminAddressViewModel>()
        //        .ToList();

        //    return View(model);
        //}

        public ActionResult Index([DataSourceRequest(Prefix = "Grid")] DataSourceRequest request)
        {
            if (request.PageSize == 0)
            {
                request.PageSize = 20;
            }


            var model = addressService
                .All();

            var total = model.Count();

            if (request.Page > 0)
            {
                model = model
                    .OrderByDescending(x => x.CreatedOn)
                    .Skip((request.Page - 1) * request.PageSize);                  
            }

            model=model.Take(request.PageSize);

            ViewData["total"] = total;

            return View(model.To<AdminAddressViewModel>().ToList());
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, AdminAddressViewModel model)
        {
            addressService.Create(model.City, model.Neighbour);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var address = this.addressService.GetById((int)id);
            if (address == null)
            {
                return HttpNotFound();
            }

            return View(address);
        }

        [HttpPost]
        public ActionResult Edit(AdminAddressViewModel address)
        {
            if (ModelState.IsValid)
            {
                this.addressService.Update(address.Id, address.City, address.Neighbour);

                return RedirectToAction("Index");
            }

            return View(address);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var isDeleted = this.addressService.Delete((int)id);

            if (!isDeleted)
            {
                return HttpNotFound();
            }

            return View();
        }
    }
}