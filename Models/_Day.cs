using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using TravelAdvisor.Models.ApiModels;

namespace TravelAdvisor.Models
{
    public partial class Day
    {
        public decimal foodTotal { get { return FoodOrders.Sum(f => f.Total ?? 0m); } } //0m is zero decimal
        public decimal accommodationTotal => Accommodations.Sum(a => a.Amount ?? 0m);
        public decimal otherTotal => Other.Sum(o => o.Amount ?? 0m);
        public decimal transportationTotal => Transportations.Sum(t => t.Amount ?? 0m);
        public decimal ActualTotal => (foodTotal + accommodationTotal + otherTotal + transportationTotal);

        //public decimal precentFood => (foodTotal / ActualTotal) * 100;
        //public decimal precentAccommodation => (accommodationTotal / ActualTotal) * 100;
        //public decimal precentOther => (otherTotal / ActualTotal) * 100;
        //public decimal precentTransportation => (transportationTotal / ActualTotal) * 100;

        public void deleteDay(TravelAdvisorEntities1 db,bool deleteExpense)
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
            db.Days.Remove(this);
            
        }
        public ChartDescription GetTotalChartDescription()
        {          
            var chartdescription = new ChartDescription
            {
                type = "bar",
                data = new ChartData
                {
                    labels = new[] { "Food", "Accommodation", "Transportation", "Other", "Total" },
                    datasets = new List<ChartDataSet>
                    {
                        new ChartDataSet
                        {
                            label = "Spent",
                            data = new[] { foodTotal, accommodationTotal, transportationTotal,otherTotal, ActualTotal }
                        },
                        //new ChartDataSet
                        //{
                        //    label = "Budget",
                        //    data = new [] { foodBudget.Value, accommodationBudget.Value, transportationBudget.Value, otherBudget.Value, budgetTotal }
                        //},

                    },

                },
            };
            return chartdescription;


        }
               
        public ChartDescription GetDoughnutChartDescription()
        {
            var chartdescription = new ChartDescription
            {
                type = "doughnut",
                data = new ChartData
                {
                    labels = new[] { "Food", "Accommodation", "Transportation", "Other" },
                    datasets = new List<ChartDataSet>
                    {
                        new ChartDataSet
                        {
                            label = "Spent",
                            data = new[] { foodTotal, accommodationTotal, transportationTotal, otherTotal }, 
                            
                      
                        
                        },
                    },

                },
            };
            return chartdescription;
        }
        public ChartDescription GetpolarAreaChartDescription()
        {
            var chartdescription = new ChartDescription
            {
                type = "polarArea",
                data = new ChartData
                {
                    labels = new[] { "Food", "Accommodation", "Transportation", "Other" },
                    datasets = new List<ChartDataSet>
                    {
                        new ChartDataSet
                        {
                            label = "Spent",
                            data = new[] { foodTotal, accommodationTotal, transportationTotal, otherTotal },



                        },
                    },

                },
            };
            return chartdescription;
        }

    }
}
    
