using System;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.GenericContent.Model;
using SitefinityWebApp.Mvc.Models;
using SitefinityWebApp.Mvc.Utilities;

namespace SitefinityWebApp.Mvc.Controllers
{
    /// <summary>
    /// This class inherits from the System.Web.Mvc.Controller class 
    /// and contains 2 methods - Index and Details which return objects of type ActionResult.
    /// The controller implements the business logic of the widget. 
    /// The methods choose which Model to work with and which View to present to the user.
    /// The ControllerToolboxItem attribute register the widget in the Sitefinity CMS toolbox
    /// It defines the name, the title and the folder in which the widget will be placed.
    /// </summary>
    [ControllerToolboxItem(Name = "TenConsecutiveEvents", Title = "Ten Consecutive Events", SectionName = "MVCTools")]
    public class EventController : Controller
    {
        private readonly EventsManager manager;

        public EventController(EventsManager manager)
        {
            this.manager = manager;
        }      
        /// <summary>
        /// This is the default action. 
        /// It queries the events first by the two calendars.
        /// If there are no events registered in the calendars, 
        /// the method calls the internal static class which creates the 10 events.
        /// The query continues with the nullable parameters for filtering and ordering 
        /// the results and returns the Index View with the Index View Model.
        /// </summary>
    
        public ActionResult Index(DateTime? filterDate, int? calendarName, int? orderBy)
        {
            IQueryable<Event> events = manager.GetEvents()
                .Where(ev => ev.Parent.Title == Constants.CalendarOne
                || ev.Parent.Title == Constants.CalendarTwo);

            if (!events.Any())
            {
                CustomEvents.CreateEvents();
            }
            // Queries only the events which are visible and have Live status
            events = events.Where(ev => ev.Visible == true)
                          .Where(ev => ev.Status == ContentLifecycleStatus.Live);
            if (filterDate != null)
            {
                events = events.Where(ev => ev.EventStart == filterDate) ??
                    throw new ArgumentNullException(Constants.NoEvents);
                TempData["Message"] = Constants.NoEvents;
            }
            if (calendarName != null)
            {
                var calendar = Enum.GetName(typeof(CalendarNames), calendarName);
                events = events.Where(ev => ev.Parent.Title.Contains(calendar));
            }
            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case 1: events = events.OrderBy(ev => ev.EventStart); break;
                    case 2: events = events.OrderBy(ev => ev.EventEnd); break;
                    case 3: events = events.OrderBy(ev => ev.Title); break;
                    default:
                        break;
                }
            }
            var eventsModel = events.Select(ev => new EventViewModel(ev));
            var model = new IndexViewModel(eventsModel);
            return View("Index", model);
        }
        /// <summary>
        /// This action uses urlName parameter of the event which is bind from the Index View.
        /// It makes a query with .SingleOrDefault() as there should be only one event with the
        /// specific url and returns a detailed View with an EventViewModel.
        /// If the event is not found the method throws ArgumentNullException.
        /// </summary>        
        public ActionResult Detail(string urlName)
        {
            var eventItem = manager.GetEvents()
                .Where(ev => ev.UrlName == urlName && ev.Visible == true &&
                ev.Status == ContentLifecycleStatus.Live)
                .SingleOrDefault() ?? throw new ArgumentNullException(Constants.NoEvent);
            
            var model = new EventViewModel(eventItem);

            return View("Detail", model);
        }
    }
}

