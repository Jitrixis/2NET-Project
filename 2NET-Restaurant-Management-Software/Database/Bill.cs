using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2NET_Restaurant_Management_Software.Database
{
    class Bill
    {
        public int BillId { get; set; }

        public int WaiterId { get; set; }

        public virtual Waiter Waiter { get; set; }

        public virtual List<Meal> Meals { get; set; }
    }
}
