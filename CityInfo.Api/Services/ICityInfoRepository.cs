using CityInfo.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.Api.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        // alternative IQueryable, 
        // after which where can be added, 
        // but execution not purely in Repository

        City GetCity(int cityId, bool includePoitsOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

        bool CityExists(int cityId);

        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        bool Save();
    }
}
