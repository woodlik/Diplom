using GoSport.Client.Infrastructure;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Client.ViewModels.Advanced;
using GoSport.Client.ViewModels.SportCenters;
using GoSport.Services;
using GoSport.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoSport.Client.Controllers
{
    public class AdvancedController : BaseController
    {
        ISportCenterService sportCenterService;

        public AdvancedController(
            ISportCategoryService sportCategories,
            IAddressService addressService,
            ISportCategoryService categoryService,
            ISportCenterService sportCenterService)
            : base(sportCategories, addressService, categoryService)
        {
            this.sportCenterService = sportCenterService;
        }

        [HttpGet]
        public ActionResult BySortPreferance()
        {
            return RedirectToAction("ByPreferance");
        }

        [HttpGet]
        public ActionResult ByPreferance()
        {
            var model = GetByQueryStringParameter().ToList();

            GetImageUrls(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ByPreferance(SearchViewModel model)
        {
            string city = string.Empty;
            string neighbour = string.Empty;
            if (model.City != 0) city = addressService.All().FirstOrDefault(x => x.Id == model.City).City;
            if (model.Neighborhood != 0) neighbour = addressService.All().FirstOrDefault(x => x.Id == model.Neighborhood).Neighborhood;
            var cityOrNeighbourPassed = (city != string.Empty || neighbour != string.Empty);

            var fromCitiesAndNeihbours = sportCenterService.All()
                .Where(x => city != string.Empty ? x.Address.City == city : x.Address.City == null)
                .Where(x => neighbour != string.Empty ? x.Address.Neighborhood == neighbour : x.Address.Neighborhood == null)
                .To<SportCenterViewModel>()
                .ToList();

            // cities and neighbours
            if (string.IsNullOrEmpty(neighbour))
            {
                fromCitiesAndNeihbours = sportCenterService.All()
                .Where(x => city != string.Empty ? x.Address.City == city : x.Address.City == null)
                .To<SportCenterViewModel>()
                .ToList();
            }

            // categories
            var categoriesNames = new List<string>();

            if (model.SportCategories != null)
            {
                categoriesNames = Array
                    .ConvertAll(model.SportCategories.Split(','), p => p.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            }

            var fromCategories = new List<SportCenterViewModel>();

            foreach (var category in categoriesNames)
            {
                var current = categoryService.All()
                    .FirstOrDefault(x => x.Name == category);

                if (current != null)
                {
                    fromCategories.AddRange(current.SportCenters
                    .AsQueryable()
                    .To<SportCenterViewModel>().ToList());
                }

            }

            var resultSportCenters = new List<SportCenterViewModel>();

            if (fromCitiesAndNeihbours.Count > 0 && fromCategories.Count > 0)
            {
                resultSportCenters = fromCitiesAndNeihbours
                    .Where(x => fromCategories.Any(y => y.Name == x.Name))
                    .ToList();
            }
            else if (fromCategories.Count > 0 && !cityOrNeighbourPassed)
            {
                resultSportCenters = fromCategories;
            }
            else if(cityOrNeighbourPassed)
            {
                resultSportCenters = fromCitiesAndNeihbours
                    .Where(x => city != string.Empty ? x.Address.City == city : x.Address.City != null)
                    .Where(x => neighbour != string.Empty ? x.Address.Neighborhood == neighbour : x.Address.Neighborhood != null)
                    .ToList();
            }

            this.GetImageUrls(resultSportCenters);

            return View(resultSportCenters);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BySortPreferance(string sortParam)
        {
            var model = new List<SportCenterViewModel>();

            if (sortParam != null || sortParam != string.Empty)
            {
                if (sortParam == "Rating")
                {
                    model = sportCenterService.All()
                            .OrderByDescending(x => x.Rating.Total)
                            .To<SportCenterViewModel>()
                            .ToList();
                }
                if (sortParam == "Date")
                {
                    model = sportCenterService.All()
                            .OrderByDescending(x => x.CreatedOn)
                            .To<SportCenterViewModel>()
                            .ToList();
                }
                if (sortParam == "Name")
                {
                    model = sportCenterService.All()
                            .OrderBy(x => x.Name)
                            .To<SportCenterViewModel>()
                            .ToList();
                }
                if (sortParam == "Most comments")
                {
                    model = sportCenterService.All()
                            .OrderByDescending(x => x.Comments.Count())
                            .To<SportCenterViewModel>()
                            .ToList();
                }
            }
            else
            {
                model = sportCenterService.All()
                .OrderByDescending(x => x.CreatedOn)
                .To<SportCenterViewModel>()
                .ToList();
            }

            GetImageUrls(model);

            return this.View("ByPreferance", model);
        }

        private IEnumerable<SportCenterViewModel> GetByQueryStringParameter()
        {
            if (Request.QueryString["City"] != null)
            {
                var param = (string)Request.QueryString["City"];

                return sportCenterService.All()
                      .OrderByDescending(x => x.CreatedOn)
                      .Where(x => x.Address.City == param)
                      .To<SportCenterViewModel>();
            }

            if (Request.QueryString["Neighbour"] != null)
            {
                var param = (string)Request.QueryString["Neighbour"];

                return sportCenterService.All()
                      .OrderByDescending(x => x.CreatedOn)
                      .Where(x => x.Address.Neighborhood == param)
                      .To<SportCenterViewModel>();
            }

            if (Request.QueryString["category"] != null)
            {
                var param = (string)Request.QueryString["category"];

                return categoryService.All()
                      .OrderByDescending(x => x.CreatedOn)
                      .FirstOrDefault(x => x.Name == param)
                      .SportCenters
                      .AsQueryable()
                      .To<SportCenterViewModel>();
            }

            return sportCenterService.All()
                        .OrderByDescending(x => x.CreatedOn)
                        .To<SportCenterViewModel>();
        }

        private void GetImageUrls(IEnumerable<SportCenterViewModel> collection)
        {
            foreach (var sportCenter in collection)
            {
                sportCenter.Images = ImageHelper.SanitizeImageUrls(sportCenterService.GetImagesForSportCenter(sportCenter.Name).ToArray());
            }
        }
    }
}