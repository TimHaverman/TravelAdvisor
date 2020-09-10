using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAdvisor.Models.ApiModels
{
    public class ChartData
    {
        public IEnumerable<string> labels { get; set; }
        public IEnumerable<ChartDataSet> datasets { get; set; }

        //public static implicit operator ChartData(ChartData v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}