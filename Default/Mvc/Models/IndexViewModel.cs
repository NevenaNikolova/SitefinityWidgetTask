using SitefinityWebApp.Mvc.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitefinityWebApp.Mvc.Models
{/// <summary>
/// This class is a View Model which represents the collection of events 
/// that will be presented to the user. The properties for ordering and filtration
/// are nullable and the class has two constructors 
/// which allowes it to work with or without filtration and ordering.
/// </summary>
    public class IndexViewModel
    {
        public IndexViewModel(IQueryable<EventViewModel> eventItems)
        {
            EventItems = eventItems;
        }

        public IndexViewModel(IQueryable<EventViewModel> eventItems, DateTime? filterDate, CalendarNames calendarName, OrderTypes orderBy)
            : this(eventItems)
        {
            FilterDate = filterDate;
            CalendarName = calendarName;
            OrderBy = orderBy;
        }
       
        public IQueryable<EventViewModel> EventItems { get; set; }

        // This property will hold a state, selected by user.
        [Display(Name = "Filter By Date")]
        [DataType(DataType.Date)]
        public DateTime? FilterDate { get; set; }
   
        // This property holds an enumeration with the available options for calendars
        //to choose from.
        [Display(Name = "Filter By Calendar")]
        [EnumDataType(typeof(CalendarNames))]
        public CalendarNames CalendarName { get; set; }

        //This property holds an enumeration with available options for ordering the results.
        [Display(Name ="Order By")]
        [EnumDataType(typeof(OrderTypes))]
        public OrderTypes OrderBy { get; set; }
    }
}