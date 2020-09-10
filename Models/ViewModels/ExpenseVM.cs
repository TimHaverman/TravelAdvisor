using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAdvisor.Models.ViewModels
{
    public class ExpenseVM
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Input is required.")]public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Input is required.")]public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> BudgetId { get; set; }
        public Nullable<int> DayId { get; set; }
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<short> NumberAdults { get; set; }
        public Nullable<short> NumberChildren { get; set; }
        public Nullable<int> TripId { get; set; }
        public Budget budget { get; set; }
        public Day day { get; set; }

    }
}