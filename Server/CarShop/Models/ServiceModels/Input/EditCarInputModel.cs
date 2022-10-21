using System;

namespace CarShop.Models.ServiceModels.Input
{
    public class EditCarInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public DateTime Year { get; set; }

        public string LicensePlate { get; set; }
    }
}
