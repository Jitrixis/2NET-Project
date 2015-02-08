using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Database
{
    public class Waiter
    {
        public Waiter() {
            Bills = new List<Bill>();
        }
        public int WaiterId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual List<Bill> Bills { get; set; }
    }
}
