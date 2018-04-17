using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Services.Contracts
{
    public interface IAddressService
    {
        Address GetById(int id);

        void Update(int id, string city, string neigbour);

        bool Delete(int id);

        void Create(string city, string neighbour);

        IQueryable<Address> AllCities();

        IQueryable<Address> All();


        IQueryable<Address> GetByCity(string cityName);

        void AddAddressForUser(string userId,int neighbourId);

        void AddAddressForSportCenter(string sportCenterName, int neighbourId);
    }
}
