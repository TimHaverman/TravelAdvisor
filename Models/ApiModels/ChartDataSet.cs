using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAdvisor.Models.ApiModels
{
    public class ChartDataSet
    {
        public string label { get; set; }
        public IEnumerable<decimal> data { get; set; }
        public string type { get; set; }
        public bool? fill { get; set; }
        public string borderColor { get; set; }
        public decimal cutoutPrecentage { get; set; }
    }
}