using CarShop.Models;
using CarShop.Models.ServiceModels.Input;
using CarShop.Models.ServiceModels.Output;
using CarShop.Models.ViewModels;
using CarShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly CarsService carsService;
        private readonly EventsService eventsService;

        public HomeController(ILogger<HomeController> logger, CarsService carsService, EventsService eventsService)
        {
            this.carsService = carsService;
            this.logger = logger;
            this.eventsService = eventsService;
        }

        public IActionResult Index()
        {
            var cars = carsService.GetAllCars();

            return View(cars);
        }

        public IActionResult AddCar()
        {
            var model = new CarFormModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddCar(CarFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(new CarFormModel());
            }

            var inputModel = new AddCarInputModel()
            {
                Name = input.Name,
                Make = input.Make,
                Model = input.Model,
                Year = input.Year,
                LicensePlate = input.LicensePlate
            };

            var result = this.carsService.AddCar(inputModel);

            if (!result)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCar(int id)
        {
            var result = this.carsService.DeleteCar(id);

            if (!result)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditCar(int id)
        {
            var (result, model) = this.carsService.GetCar(id);

            if (result == false)
            {
                return new BadRequestResult();
            }

            var formModel = new CarFormModel()
            {
                Id = id,
                Name = model.Name,
                Make = model.Make,
                Model = model.Model,
                LicensePlate = model.LicensePlate,
                Year = DateTime.Parse(model.Year)

            };

            return View(formModel);
        }

        [HttpPost]
        public IActionResult EditCar(CarFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var inputModel = new EditCarInputModel()
            {
                Id = input.Id,
                Name = input.Name,
                Make = input.Make,
                Model = input.Model,
                Year = input.Year,
                LicensePlate = input.LicensePlate
            };


            var result = this.carsService.EditCar(inputModel);

            if (!result)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("Index");
        }

        public IActionResult ShowEvents(int id)
        {
            var (_, car) = this.carsService.GetCar(id);
            var events = this.eventsService.GetEvents(id);

            var model = new ShowEventsViewModel()
            {
                Car = car,
                Events = events
            };

            return View(model);
        }


        public IActionResult AddEvent(int carId)
        {
            var model = new EventFormModel()
            {
                CarId = carId,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEvent(EventFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(new EventFormModel()
                {
                    CarId = input.CarId
                });
            }

            var inputModel = new AddEventInputModel()
            {
                CarId = input.CarId,
                Comment = input.Comment,
                IsPeriodic = input.IsPeriodic,
                Mileage = input.Mileage,
                NextDate = input.NextDate,
                NextMileage = input.NextMileage
            };

            var result = this.eventsService.AddEvent(inputModel);

            if (!result)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("ShowEvents", new { Id = input.CarId });
        }

        public IActionResult DeleteEvent(int evId, int carId)
        {
            var result = this.eventsService.DeleteEvent(evId);

            if (!result)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("ShowEvents", new { Id = carId });
        }

        public IActionResult EditEvent(int id)
        {
            var (result, model) = this.eventsService.GetEvent(id);

            if (result == false)
            {
                return new BadRequestResult();
            }

            var formModel = new EventFormModel()
            {
                CarId = model.CarId,
                Id = model.Id,
                Comment = model.Comment,
                IsPeriodic = model.IsPeriodic,
                Mileage = model.Mileage,
                NextDate = DateTime.Parse(model.NextDate),
                NextMileage = model.NextMileage,

            };

            return View(formModel);
        }

        [HttpPost]
        public IActionResult EditEvent(EventFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var inputModel = new EditEventInputModel()
            {
               Comment = input.Comment,
               NextMileage = input.NextMileage,
               NextDate = input.NextDate,
               Mileage = input.Mileage,
               IsPeriodic = input.IsPeriodic,
               Id = input.Id
            };

            var result = this.eventsService.EditEvent(inputModel);

            if (!result)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("ShowEvents", new { Id = input.CarId });

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
