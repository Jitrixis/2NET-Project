using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Database
{
    public class Bill
    {
        public Bill() {
            string_Meals = new List<MealString>();
        }

        public int BillId { get; set; }

        public int WaiterId { get; set; }

        public double BillPrice { get; set; }

        public virtual Waiter Waiter { get; set; }

        public int TableId { get; set; }

        public virtual Table Table { get; set; }

        public virtual List<MealString> string_Meals { get; set; }
    }
}
