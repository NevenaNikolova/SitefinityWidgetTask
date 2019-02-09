using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Utilities
{
    public enum CalendarNames
    {
        [Display(Name =Constants.CalendarOne)]
        One=1,
        [Display(Name =Constants.CalendarTwo)]
        Two=2
    }
}