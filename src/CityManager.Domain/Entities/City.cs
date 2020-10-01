using System;

namespace CityManager.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Ibge { get; set; }
        public string UF { get; set; }
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Region { get; set; }
    }
}