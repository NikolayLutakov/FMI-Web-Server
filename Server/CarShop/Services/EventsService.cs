using CarShop.Data.Context;
using CarShop.Data.Models;
using CarShop.Models.ServiceModels.Input;
using CarShop.Models.ServiceModels.Output;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Services
{
    public class EventsService
    {
        private readonly CarShopContext db;
        private readonly ILogger<EventsService> logger;

        public EventsService(ILogger<EventsService> logger, CarShopContext db)
        {
            this.db = db;
            this.logger = logger;
        }

        public IEnumerable<EventOutputModel> GetEvents(int carId)
        {
            var events = db.Events.Where(x => x.Car.Id == carId)
                .Select(ev => new EventOutputModel()
                {
                    CarId = carId,
                    Id = ev.Id,
                    Date = ev.Date.ToString("d"),
                    Comment = ev.Comment,
                    IsPeriodic = ev.IsPeriodic,
                    Mileage = ev.Mileage,
                    NextDate = ev.NextDate.ToString("d"),
                    NextMileage = ev.NextMileage
                })
                .ToList();

            return events;
        }

        public (bool, EventOutputModel) GetEvent(int id)
        {
            var ev = this.db.Events.Where(x => x.Id == id).Include(x => x.Car).FirstOrDefault();

            if (ev == null)
            {
                this.logger.LogError($"Event with Id: {id} not found!");
                return (false, null);
            }

            var eventOutput = new EventOutputModel();

            eventOutput.CarId = ev.Car.Id;
            eventOutput.Id = ev.Id;
            eventOutput.Date = ev.Date.ToString("d");
            eventOutput.Mileage = ev.Mileage;
            eventOutput.Comment = ev.Comment;
            eventOutput.IsPeriodic = ev.IsPeriodic;
            eventOutput.NextDate = ev.NextDate.ToString("d");
            eventOutput.NextMileage = ev.NextMileage;

            return (true, eventOutput);
        }

        public bool AddEvent(AddEventInputModel input)
        {
            var car = this.db.Cars.Find(input.CarId);

            if (car == null)
            {
                this.logger.LogError($"Car with Id: {input.CarId} not found!");
                return false;
            }

            var eventToAdd = new Event()
            {
                Date = DateTime.Now,
                Comment = input.Comment,
                IsPeriodic = input.IsPeriodic,
                Mileage = input.Mileage,
                NextDate = input.NextDate,
                NextMileage = input.NextMileage,
                Car = car,
            };

            try
            {
                db.Events.Add(eventToAdd);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return false;
            }

            return true;
        }

        public bool EditEvent(EditEventInputModel input)
        {
            var eventToEdit = db.Events.Find(input.Id);

            if (eventToEdit == null)
            {
                this.logger.LogError($"Event with Id: {input.Id} not found!");
                return false;
            }

            eventToEdit.Comment = input.Comment;
            eventToEdit.IsPeriodic = input.IsPeriodic;
            eventToEdit.Mileage = input.Mileage;
            eventToEdit.NextDate = input.NextDate;
            eventToEdit.NextMileage = input.NextMileage;

            try
            {
                this.db.Events.Update(eventToEdit);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return false;
            }

            return true;
        }

        public bool DeleteEvent(int eventId)
        {
            var evToDelete = this.db.Events.Find(eventId);

            if (evToDelete == null)
            {
                this.logger.LogError($"Event with Id: {eventId} not found!");
                return false;
            }

            try
            {
                this.db.Remove(evToDelete);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return false;
            }

            return true;
        }
    }
}
