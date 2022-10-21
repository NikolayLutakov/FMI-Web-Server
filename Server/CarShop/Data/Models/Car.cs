using System;
using System.Collections.Generic;

namespace CarShop.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Events = new List<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public DateTime Year { get; set; }

        public string LicensePlate { get; set; }


        public virtual IEnumerable<Event> Events { get; set; }
    }
}
