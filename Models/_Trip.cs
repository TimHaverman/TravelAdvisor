using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using TravelAdvisor.Models.ApiModels;

namespace TravelAdvisor.Models
{
    [MetadataType(typeof(tripMetaData))]
    public partial class Trip
    {
        public decimal foodTotal { get { return Days.Sum(f => f.foodTotal); } } //0m is zero decimal
        public decimal accommodationTotal => Days.Sum(a => a.accommodationTotal);
        public decimal otherTotal => Days.Sum(o => o.otherTotal);
        public decimal transportationTotal => Days.Sum(t => t.transportationTotal);
        public decimal actualTotal => (foodTotal + accommodationTotal + otherTotal + transportationTotal);
        public decimal budgetTotal => Budgets.Sum(b => b.Amount ?? 0m);
        public void deleteTrip(TravelAdvisorEntities1 db)
        {
            foreach(var bud in Budgets.ToList())
            {
                bud.deleteBudget(db, true);
            }
            foreach(var da in Days.ToList())
            {
                da.deleteDay(db, false);
            }
            Customers.Clear();
            db.Trips.Remove(this);
        }
        public ChartDescription GetChartDescription()
        {
            if(Days.Count() == 0)
            {
                return null;
            }



            var foodBudget = Budgets.SingleOrDefault(b => b.Department.Name == "FoodOrder")?.Amount;
            if(!foodBudget.HasValue)
            {
                foodBudget = 0.0m;
            }
            foodBudget /= Days.Count();

            var accommodationBudget = Budgets.SingleOrDefault(a => a.Department.Name == "Accommodation")?.Amount;
            if (!accommodationBudget.HasValue)
            {
                accommodationBudget = 0.0m;
            }
            accommodationBudget /= Days.Count();

            var transportationBudget = Budgets.SingleOrDefault(a => a.Department.Name == "Transportation")?.Amount;
            if (!transportationBudget.HasValue)
            {
                transportationBudget = 0.0m;
            }
            transportationBudget /= Days.Count();

            var otherBudget = Budgets.SingleOrDefault(a => a.Department.Name == "Other")?.Amount;
            if (!otherBudget.HasValue)
            {
                otherBudget = 0.0m;
            }
            otherBudget /= Days.Count();

            var chartdescription = new ChartDescription
            {
                type = "bar",
                data = new ChartData
                {
                    labels = Days.OrderBy(d => d.DayNumber).Select(d => "Day " + d.DayNumber.ToString()),
                    datasets = new List<ChartDataSet>
                    {
                        new ChartDataSet
                        {
                            label = "Food",
                            data = Days.OrderBy(d => d.DayNumber).Select(d => d.foodTotal)
                        },
                        new ChartDataSet
                        {
                            label = "Accommodation",
                            data = Days.OrderBy(d => d.DayNumber).Select(d => d.accommodationTotal)
                        },
                        new ChartDataSet
                        {
                            label = "Transportation",
                            data = Days.OrderBy(d => d.DayNumber).Select(d => d.transportationTotal)
                        },
                        new ChartDataSet
                        {
                            label = "Other",
                            data = Days.OrderBy(d => d.DayNumber).Select(d => d.otherTotal)
                        },
                        new ChartDataSet
                        {
                            label = "Total",
                            data = Days.OrderBy(d => d.DayNumber).Select(d => d.ActualTotal)
                        },
                        // new ChartDataSet
                        //{
                        //    label = "Food Budget",
                        //    data = Days.OrderBy(d => d.DayNumber).Select(d => foodBudget.Value),
                        //    type = "line",
                        //    fill = false,
                        //    borderColor = "red"
                        //},
                        //  new ChartDataSet
                        //{
                        //    label = "Accommodation Budget",
                        //    data = Days.OrderBy(d => d.DayNumber).Select(d => accommodationBudget.Value),
                        //    type = "line",
                        //    fill = false,
                        //    borderColor = "red"
                        //},
                        //   new ChartDataSet
                        //{
                        //    label = "Transportation Budget",
                        //    data = Days.OrderBy(d => d.DayNumber).Select(d => transportationBudget.Value),
                        //    type = "line",
                        //    fill = false,
                        //    borderColor = "red"
                        //},
                        //    new ChartDataSet
                        //{
                        //    label = "Other Budget",
                        //    data = Days.OrderBy(d => d.DayNumber).Select(d => otherBudget.Value),
                        //    type = "line",
                        //    fill = false,
                        //    borderColor = "red"
                        //},
                             new ChartDataSet
                        {
                            label = "Total Budget",
                            data = Days.OrderBy(d => d.DayNumber).Select(d => budgetTotal / Days.Count()),
                            type = "line",
                            fill = false,
                            borderColor = "red"
                        },
                    },

                },
            };
            return chartdescription; 
            
            
        }
        public ChartDescription GetTotalChartDescription()
        {
            if (Days.Count() == 0)
            {
                return null;
            }



            var foodBudget = Budgets.SingleOrDefault(b => b.Department.Name == "FoodOrder")?.Amount;
            if (!foodBudget.HasValue)
            {
                foodBudget = 0.0m;
            }
            var accommodationBudget = Budgets.SingleOrDefault(a => a.Department.Name == "Accommodation")?.Amount;
            if (!accommodationBudget.HasValue)
            {
                accommodationBudget = 0.0m;
            }
            var transportationBudget = Budgets.SingleOrDefault(a => a.Department.Name == "Transportation")?.Amount;
            if (!transportationBudget.HasValue)
            {
                transportationBudget = 0.0m;
            }
            var otherBudget = Budgets.SingleOrDefault(a => a.Department.Name == "Other")?.Amount;
            if (!otherBudget.HasValue)
            {
                otherBudget = 0.0m;
            }
            var chartdescription = new ChartDescription
            {
                type = "bar",
                data = new ChartData
                {
                    labels = new[] { "Food","Accommodation","Transportation","Other","Total" },
                    datasets = new List<ChartDataSet>
                    {
                        new ChartDataSet
                        {
                            label = "Spent",
                            data = new[] { foodTotal, accommodationTotal, transportationTotal,otherTotal, actualTotal }
                        },
                        new ChartDataSet
                        {
                            label = "Budget",
                            data = new [] { foodBudget.Value, accommodationBudget.Value, transportationBudget.Value, otherBudget.Value, budgetTotal }
                        },
                        
                    },

                },
            };
            return chartdescription;


        }
    }
    public class tripMetaData
    {
        public string Name { get; set; }
        public string CustId { get; set; }
        public string Description { get; set; }
        public string TypeOfTrip { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public Nullable<bool> Suggested { get; set; }

    }

}