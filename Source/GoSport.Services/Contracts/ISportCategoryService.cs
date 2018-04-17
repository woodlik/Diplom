using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Services.Contracts
{
    public interface ISportCategoryService
    {
        SportCategory GetById(int id);

        IQueryable<string> AllNames();

        IQueryable<SportCategory> All();

        void Create(string name);

        bool UpdateById(int id, string name);

        bool DeleteById(int id);

        void AddCategoriesForUser(IEnumerable<string> categories, string userId);

        void AddCategoriesForSportCenter(IEnumerable<string> categoriesNames, string sportCenterName);

        IQueryable<SportCenter> GetByCategories(IEnumerable<string> categoriesNames);
    }
}
