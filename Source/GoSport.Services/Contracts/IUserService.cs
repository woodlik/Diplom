using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetUsersWithConcreteInterests(IEnumerable<SportCategory> categories);

        IQueryable<User> GetAll();

        User GetUserById(string id);
    }
}
