using CityInfo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CityDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // find point fo interest
            var pointOfIntrest = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfIntrest == null)
            {
                return NotFound();
            }

            return Ok(pointOfIntrest);
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId, 
            [FromBody] PointOfInterestForCreationDto pointOfInterest) // FromBody is optional, the ApiController takes care of that
        {
            //if (pointOfInterest == null)
            //{
            //    return BadRequest();
            //} // this is taken care of by ApiController Attribute

            var city = CityDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // next we need to map from a PointOfInterestForCreationDto to PointOfInterestDto
            // to have the id of it
            // demo purpose - to be improved
            var maxPointOfInterestId = CityDataStore.Current.Cities.SelectMany(
                        c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);
            return CreatedAtRoute(
                "GetPointOfInterest",
                new { cityId, id = finalPointOfInterest.Id },
                finalPointOfInterest);
        }
    }
}
