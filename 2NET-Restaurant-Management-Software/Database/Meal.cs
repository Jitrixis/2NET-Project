using System;
using System.Collections.Generic;

namespace _2NET_Restaurant_Management_Software.Database
{
    class Meal
    {
        public int MealId { get; set; }

        public string Meal_name { get; set; }

        public double Price { get; set; }

        public virtual List<Bill> Bill { get; set; }
    }
}
