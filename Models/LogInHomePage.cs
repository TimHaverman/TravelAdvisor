using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelAdvisor.Models
{
    public class LogInHomePage
    {
        public Customer LoggedIn = new Customer();
        List<Trip> Places = new List<Trip>();
        List<Transportation> Transport = new List<Transportation>();
        List<Accommodation> Accommodations = new List<Accommodation>();
        List<Other> Others = new List<Other>();
        List<FoodOrder> FoodOrders = new List<FoodOrder>();
        public int tripsCreated { get; set; }
        
    }
}