using System;

namespace CarShop.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Mileage { get; set; }

        public string Comment { get; set; }

        public bool IsPeriodic { get; set; }

        public DateTime NextDate { get; set; }

        public int NextMileage { get; set; }

        public virtual Car Car { get; set; }
    }
}
