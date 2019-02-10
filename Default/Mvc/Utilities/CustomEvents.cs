using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Events.Model;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Mvc.Utilities
{/// <summary>
/// This class contains a method which creates the 10 events and assigns them to the 2 calendars.
/// The internal access modifier makes it visible only in the current assembly.
/// The method is declared as static void as it is used only to create the 10 events and does 
/// not need to have any instances.
/// </summary>
    internal class CustomEvents
    {
        internal static void CreateEvents()
        {
            var manager = EventsManager.GetManager();

            //Disables the security checks and gives the current user authorization to create events.
            manager.Provider.SuppressSecurityChecks = true;

            // Gets the two calendars or throws exception if not found.
            var oddCalendar = manager.GetCalendars()
                    .Where(c => c.Title == Constants.CalendarOne)
                    .SingleOrDefault()
                    ?? throw new ArgumentNullException(Constants.NoCalendar);

            var evenCalendar = manager.GetCalendars()
                .Where(c => c.Title == Constants.CalendarTwo)
                .SingleOrDefault()
                ?? throw new ArgumentNullException(Constants.NoCalendar);

            //Sets the start date for the first event.
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, DateTimeKind.Utc);

            for (int i = 1; i <= 10; i++)
            {
                // Creates the event
                Event current = manager.CreateEvent();

                // Sets the event properties
                current.Title = $"Event {i}";
                current.Content = $"Teambuilding - Day {i}";
                current.EventStart = startDate.AddDays(i);
                current.EventEnd = startDate.AddDays(i + 1);

                //Changes spaces and other symbols with "-"
                current.UrlName = Regex.Replace(current.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                current.PublicationDate = DateTime.Today;

                //Assigns events to calendars                
                if (current.EventStart.Day % 2 == 0)
                {
                    current.Parent = evenCalendar;
                }
                else
                {
                    current.Parent = oddCalendar;
                }

                //Recompiles and validates the url of the event item.
                manager.RecompileAndValidateUrls(current);

                // Save the changes
                manager.SaveChanges();

                // Publishes
                var bag = new Dictionary<string, string>();
                bag.Add("ContentType", typeof(Event).FullName);

                //Fixes the "You are not authorised to publish" issue.
                SystemManager.RunWithElevatedPrivilege(d => WorkflowManager.MessageWorkflow(current.Id, typeof(Event), null, "Publish", false, bag));

            }
            //Enables the security checks.
            manager.Provider.SuppressSecurityChecks = false;
        }
    }
}