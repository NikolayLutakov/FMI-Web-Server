using System;

namespace CarShop.Models.ServiceModels.Output
{
    public class EventOutputModel
    {
        public int CarId { get; set; }

        public int Id { get; set; }

        public string Date { get; set; }

        public int Mileage { get; set; }

        public string Comment { get; set; }

        public bool IsPeriodic { get; set; }

        public string NextDate { get; set; }

        public int NextMileage { get; set; }
    }
}
