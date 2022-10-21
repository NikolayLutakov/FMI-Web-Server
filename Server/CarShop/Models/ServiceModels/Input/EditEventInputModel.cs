using System;

namespace CarShop.Models.ServiceModels.Input
{
    public class EditEventInputModel
    {
        public int Id { get; set; }

        public int Mileage { get; set; }

        public string Comment { get; set; }

        public bool IsPeriodic { get; set; }

        public DateTime NextDate { get; set; }

        public int NextMileage { get; set; }
    }
}
