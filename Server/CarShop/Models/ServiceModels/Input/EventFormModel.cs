using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Models.ServiceModels.Input
{
    public class EventFormModel
    {
        public int CarId { get; set; }

        public int Id { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string Comment { get; set; }

        public bool IsPeriodic { get; set; }

        [Required]
        public DateTime NextDate { get; set; }

        [Required]
        public int NextMileage { get; set; }
    }
}
