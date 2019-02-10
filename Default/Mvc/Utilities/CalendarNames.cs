using System.ComponentModel.DataAnnotations;

namespace SitefinityWebApp.Mvc.Utilities
{
    /// <summary>
    /// Enumeration for the calendar names, used for filtering options in the Index View.
    /// </summary>
    public enum CalendarNames
    {
        [Display(Name =Constants.CalendarOne)]
        One=1,
        [Display(Name =Constants.CalendarTwo)]
        Two=2
    }
}