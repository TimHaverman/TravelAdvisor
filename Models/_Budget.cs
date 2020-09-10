using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAdvisor.Models
{
    public partial class Budget
    {
        public decimal foodTotal { get { return FoodOrders.Sum(f => f.Total ?? 0m); } } //0m is zero decimal
        public decimal accommodationTotal => Accommodations.Sum(a => a.Amount ?? 0m);
        public decimal otherTotal => Other.Sum(o => o.Amount ?? 0m);
        public decimal transportationTotal => Transportations.Sum(t => t.Amount ?? 0m);
        public decimal ActualTotal => (foodTotal + accommodationTotal + otherTotal + transportationTotal);
        public void deleteBudget(TravelAdvisorEntities1 db, bool deleteExpense)
        {
            if (deleteExpense)
            {
                foreach (var acc in Accommodations.ToList())
                {
                    db.Accommodations.Remove(acc);
                }
                foreach (var food in FoodOrders.ToList())
                {
                    db.FoodOrders.Remove(food);
                }
                foreach (var tra in Transportations.ToList())
                {
                    db.Transportations.Remove(tra);
                }
                foreach (var othr in Other.ToList())
                {
                    db.Other.Remove(othr);
                }
            }
            db.Budgets.Remove(this);
        }
    }
}