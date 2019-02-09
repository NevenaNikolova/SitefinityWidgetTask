using System;
using System.ComponentModel.DataAnnotations;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace SitefinityWebApp.Mvc.Models
{
    /// <summary>
    /// This class is a View Model for Sitefinity`s Event and
    /// represents the events to the user with the properties specified in the model.
    /// </summary>
    public class EventViewModel
    {
        public EventViewModel(Event eventItem)
        {
            Id = eventItem.Id;
            Title = eventItem.Title;
            Content = eventItem.Content;
            EventStart = eventItem.EventStart;
            EventEnd = eventItem.EventEnd;
            UrlName = eventItem.UrlName;
            PublicationDate = eventItem.PublicationDate;
            Parent = eventItem.Parent;
            Visible = eventItem.Visible;
            Status = eventItem.Status;
        }

        [Required]
        public Guid Id{ get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Content { get; set; }
        [Required]
        public DateTime EventStart { get; set; }
        public DateTime? EventEnd { get; set; }
        [Required]
        public string UrlName { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public Calendar Parent { get; set; }
        public bool Visible { get; set; }
        public ContentLifecycleStatus Status { get; set; }
    }

}