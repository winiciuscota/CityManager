namespace CityManager.Domain.Queries
{
    public class CityQuery
    {

        /// <summary>
        /// UF of the city
        /// </summary>
        /// <value></value>
        public string UF { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Region of the city
        /// </summary>
        /// <value></value>
        public string Region { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}