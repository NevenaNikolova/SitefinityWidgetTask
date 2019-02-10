using System.ComponentModel.DataAnnotations;

namespace SitefinityWebApp.Mvc.Utilities
{/// <summary>
/// Enumeration for the ordering options, used for creating a dropdown list in the Index View.
/// </summary>
    public enum OrderTypes
    {
        [Display(Name ="Start Date")]
        Start=1,
        [Display(Name ="End Date")]
        End=2,
        [Display(Name ="Title")]
        Title=3
    }
}