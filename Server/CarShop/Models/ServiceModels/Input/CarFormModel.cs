using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Models.ServiceModels.Input
{
    public class CarFormModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public DateTime Year { get; set; }

        [Required]
        public string LicensePlate { get; set; }
    }
}
