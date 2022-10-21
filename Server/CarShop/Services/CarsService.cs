using CarShop.Data.Context;
using CarShop.Data.Models;
using CarShop.Models.ServiceModels.Input;
using CarShop.Models.ServiceModels.Output;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Services
{
    public class CarsService
    {
        private readonly CarShopContext db;
        private readonly ILogger<CarsService> logger;

        public CarsService(ILogger<CarsService> logger, CarShopContext db)
        {
            this.db = db;
            this.logger = logger;
        }

        public IEnumerable<CarOutputModel> GetAllCars()
        {
            var cars = this.db.Cars.Select(c => new CarOutputModel()
            {
                Id = c.Id,
                Name = c.Name,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year.ToString("d"),
                LicensePlate = c.LicensePlate,
                Events = c.Events.Select(e => e.Id),
            })
                .ToList();

            return cars;
        }

        public (bool, CarOutputModel) GetCar(int id)
        {
            
            var car = this.db.Cars.Find(id);

            if (car == null)
            {
                this.logger.LogError($"Car with Id: {id} not found!");
                return (false, null);
            }

            var carOutput = new CarOutputModel();

            carOutput.Id = car.Id;
            carOutput.Name = car.Name;
            carOutput.Model = car.Model;
            carOutput.Make = car.Make;
            carOutput.Year = car.Year.ToString("d");
            carOutput.LicensePlate = car.LicensePlate;
            carOutput.Events = car.Events.Select(x => x.Id);

            return (true, carOutput);
        }

        public bool AddCar(AddCarInputModel car)
        {
            var carToAdd = new Car()
            {
                Name = car.Name,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                LicensePlate = car.LicensePlate,
            };

            try
            {
                db.Cars.Add(carToAdd);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return false;
            }

            return true;
        }

        public bool EditCar(EditCarInputModel car)
        {
            var carToEdit = db.Cars.Find(car.Id);

            if(carToEdit == null)
            {
                this.logger.LogError($"Car with Id: {car.Id} not found!");
                return false;
            }

            carToEdit.Model = car.Model;
            carToEdit.Make = car.Make;
            carToEdit.Year = car.Year;
            carToEdit.LicensePlate = car.LicensePlate;

            try
            {
                this.db.Cars.Update(carToEdit);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return false;
            }

            return true;
        }

        public bool DeleteCar(int id)
        {
            var carToDelete = this.db.Cars.Find(id);

            if (carToDelete == null)
            {
                this.logger.LogError($"Car with Id: {id} not found!");
                return false;
            }

            try
            {
                this.db.Remove(carToDelete);
                this.db.SaveChanges();
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                return false;
            }

            return true;
        }    
    }
}
