using System;
using System.Collections.Generic;
using System.Text;

namespace world_cities_api
{
    public class World
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Country { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
        public string Capital { get; set; }
    }
}
