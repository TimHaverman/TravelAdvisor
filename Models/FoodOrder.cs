//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelAdvisor.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FoodOrder()
        {
            this.FoodOrderLineItems = new HashSet<FoodOrderLineItem>();
        }
    
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Input is required.")] public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Input is required.")] public string Description { get; set; }
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> BudgetId { get; set; }
        public Nullable<short> NumberAdults { get; set; }
        public Nullable<short> NumberChildren { get; set; }
        public Nullable<int> TripId { get; set; }
        public Nullable<int> DayId { get; set; }
    
        public virtual Budget Budget { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodOrderLineItem> FoodOrderLineItems { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Day Day { get; set; }
    }
}
