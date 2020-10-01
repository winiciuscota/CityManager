using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CityManager.Domain.ValueObjects;

namespace CityManager.Api.ViewModel
{

    public class EditCityViewModel
    {
        /// <summary>
        /// Ibge of the city
        /// </summary>
        /// <value></value>
        [Required]
        public string Ibge { get; set; }

        /// <summary>
        /// UF of the city
        /// </summary>
        /// <value></value>
        [Required, MaxLength(2)]
        public string UF { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        /// <value></value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Longitude of the city
        /// </summary>
        /// <value></value>
        [Required]
        public string Longitude { get; set; }

        /// <summary>
        /// Latitude of the city
        /// </summary>
        /// <value></value>
        [Required]
        public string Latitude { get; set; }

        /// <summary>
        /// Region of the city
        /// </summary>
        /// <value></value>
        [Required]
        public string Region { get; set; }
    }


    public class CityViewModel : EditCityViewModel
    {
        /// <summary>
        /// City id on thesystem
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
    }

    /// <summary>
    /// List of cities
    /// </summary>
    public class CityListViewModel : PaginatedQueryResult<CityViewModel>
    {

    }
}