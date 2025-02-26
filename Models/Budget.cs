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
    
    public partial class Budget
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Budget()
        {
            this.Accommodations = new HashSet<Accommodation>();
            this.FoodOrders = new HashSet<FoodOrder>();
            this.Other = new HashSet<Other>();
            this.Transportations = new HashSet<Transportation>();
        }
    
        public int Id { get; set; }
        public int DeparmentID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> TripId { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Trip Trip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accommodation> Accommodations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Other> Other { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transportation> Transportations { get; set; }
    }
}
