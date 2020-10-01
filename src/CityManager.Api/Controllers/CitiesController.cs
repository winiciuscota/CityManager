using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CityManager.Api.ViewModel;
using CityManager.Domain.Entities;
using CityManager.Domain.Queries;
using CityManager.Domain.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CityManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CitiesController(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets a list of cities
        /// </summary>
        /// <param name="cityQuery"></param>
        /// <response code="200">Cities list returned successfully</response>
        [HttpGet]
        [ProducesResponseType(typeof(CityListViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCitiesAsync([FromQuery] CityQuery cityQuery)
        {
            var queryResult = await _cityRepository.GetAllAsync(cityQuery);
            var result = queryResult.Adapt<CityListViewModel>();

            return Ok(result);
        }

        /// <summary>
        /// Gets a list of UFs
        /// </summary>
        /// <response code="200">UFs list returned successfully</response>
        [HttpGet("UFs"), ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUFsAsync() => Ok(await _cityRepository.GetUFsAsync());

        /// <summary>
        /// Gets a list of regions
        /// </summary>
        /// <response code="200">Regions list returned successfully</response>
        [HttpGet("Regions"), ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRegionsAsync() => Ok(await _cityRepository.GetRegionsAsync());

        /// <summary>
        /// Add city
        /// </summary>
        /// <param name="viewModel">The City's data</param>
        [HttpPost]
        public async Task<IActionResult> AddCityAsync([FromBody] EditCityViewModel viewModel)
        {
            var city = viewModel.Adapt<City>();
            var conflictingCities = await _cityRepository.GetAllAsync(x => x.Ibge == city.Ibge || (x.Name == city.Name && x.UF == city.UF));

            if (conflictingCities.Any())
            {
                return Conflict($"Conflicting cities found");
            }

            _cityRepository.Add(city);
            await _unitOfWork.CompleteAsync();
            var result = city.Adapt<CityViewModel>();

            return Ok(result);
        }

        /// <summary>
        /// Edit city
        /// </summary>
        /// <param name="id">Id of the city to be edited</param>
        /// <param name="viewModel">new city's data</param>
        /// <response code="404">City not found</response>
        /// <response code="200">City successfully edited</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCityAsync([FromRoute] int id, [FromBody] EditCityViewModel viewModel)
        {
            var city = await _cityRepository.GetAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            var conflictingCities = await _cityRepository.GetAllAsync(x => x.Id != id && (x.Ibge == city.Ibge || (x.Name == city.Name && x.UF == city.UF)));
            if (conflictingCities.Any())
            {
                return Conflict($"Conflicting cities found");
            }

            viewModel.Adapt(city);
            await _unitOfWork.CompleteAsync();
            var result = city.Adapt<CityViewModel>();

            return Ok(result);
        }

        /// <summary>
        /// Delete city
        /// </summary>
        /// <param name="id">Id of the city to be deleted</param>
        /// <response code="404">City not found</response>
        /// <response code="200">City successfully deleted</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCityAsync([FromRoute] int id)
        {
            var city = await _cityRepository.GetAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            _cityRepository.Remove(city);
            await _unitOfWork.CompleteAsync();
            var result = city.Adapt<CityViewModel>();

            return Ok(result);
        }



    }
}