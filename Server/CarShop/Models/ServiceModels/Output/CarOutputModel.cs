using System;
using System.Collections.Generic;

namespace CarShop.Models.ServiceModels.Output
{
    public class CarOutputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string LicensePlate { get; set; }

        public IEnumerable<int> Events { get; set; }
    }
}
