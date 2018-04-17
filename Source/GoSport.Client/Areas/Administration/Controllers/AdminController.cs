namespace GoSport.Client.Areas.Administration.Controllers
{
    using GoSport.Client.Controllers;
    using Services.Contracts;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {

        protected ISportCenterService sportCenterService;

        public AdminController(
            ISportCategoryService sportCategories,
            IAddressService addressService,
            ISportCategoryService categoryService,
            ISportCenterService sportCenterService)
            : base(sportCategories, addressService, categoryService)
        {
            this.sportCenterService = sportCenterService;
        }
    }
}