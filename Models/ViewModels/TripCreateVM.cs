using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelAdvisor.Models.ViewModels
{
    public class TripCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CustId { get; set; }
        public string Description { get; set; }
        [Required]
        public string TypeOfTrip { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public Nullable<bool> Suggested { get; set; }
        [Range(1,30)]
        public int numberDays { get; set; }

        public Trip convertToTrip()
        {
            var trip = new Trip { Name = this.Name, CustId = this.CustId, Description = this.Description, TypeOfTrip = this.TypeOfTrip, CompletedDateTime = this.CompletedDateTime, Suggested = this.Suggested };
            var days = new List<Day>(numberDays);
            for(int i = 1; i <= numberDays; i++)
            {
                var day = new Day { DayNumber = i };
                days.Add(day);
            }
            trip.Days = days;
            return trip;
        }
    }
}