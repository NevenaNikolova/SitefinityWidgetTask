using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Utilities
{
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