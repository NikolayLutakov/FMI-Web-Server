using CarShop.Models.ServiceModels.Output;
using System.Collections.Generic;

namespace CarShop.Models.ViewModels
{
    public class ShowEventsViewModel
    {
        public CarOutputModel Car { get; set; }
        public IEnumerable<EventOutputModel> Events { get; set; }
    }
}
