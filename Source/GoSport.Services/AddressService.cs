using GoSport.Data.Common.Repository;
using GoSport.Data.Models;
using GoSport.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Services
{
    public class AddressService : IAddressService
    {
        private IDeletableEntityRepository<Address> addressesDb;
        private IDeletableEntityRepository<User> usersDb;
        private IDeletableEntityRepository<SportCenter> sportCenters;
        private ISportCenterService sportCenterService;

        public AddressService(
            IDeletableEntityRepository<Address> addressesDb,
            IDeletableEntityRepository<User> usersDb,
             IDeletableEntityRepository<SportCenter> sportCenters,
             ISportCenterService sportCenterService)
        {
            this.addressesDb = addressesDb;
            this.usersDb = usersDb;
            this.sportCenters = sportCenters;
            this.sportCenterService = sportCenterService;
        }

        public void Create(string city, string neighbour)
        {
            addressesDb.Add(new Address() { City = city, Neighborhood = neighbour });
            addressesDb.SaveChanges();
        }

        public void Update(int id, string city, string neigbour)
        {
            var model = addressesDb.GetById(id);

            model.City = city;
            model.Neighborhood = neigbour;

            addressesDb.SaveChanges();
        }

        public bool Delete(int id)
        {
            var model = addressesDb.GetById(id);
            if (model == null)
            {
                return false;
            }

            var centers = addressesDb.GetById(id).SportCenters;

            foreach (var center in centers)
            {
                sportCenterService.DeleteById(center.Id);
            }

            addressesDb.Delete(model);
            addressesDb.SaveChanges();

            return true;
        }

        public IQueryable<Address> AllCities()
        {
            return this
                .addressesDb.All()
                .GroupBy(x => x.City)
                .Select(x => x.FirstOrDefault());
        }

        public IQueryable<Address> All()
        {
            return this
                .addressesDb.All();
        }

        public IQueryable<Address> GetByCity(string cityName)
        {
            return this.addressesDb
                .All()
                .Where(x => x.City == cityName);
        }

        public void AddAddressForUser(string userId, int neighbourId)
        {
            var address = addressesDb.All()
                .FirstOrDefault(x => neighbourId == x.Id);

            var user = usersDb.All().FirstOrDefault(x => x.Id == userId);
            user.AddressId = address.Id;

            usersDb.SaveChanges();
        }

        public void AddAddressForSportCenter(string sportCenterName, int neighbourId)
        {
            var address = addressesDb.All()
              .FirstOrDefault(x => neighbourId == x.Id);

            var spotrtCenter = sportCenters.All().FirstOrDefault(x => x.Name == sportCenterName);
            spotrtCenter.AddressId = address.Id;

            sportCenters.SaveChanges();
        }

        public Address GetById(int id)
        {
            return addressesDb.All()
              .FirstOrDefault(x => id == x.Id);
        }
    }
}
