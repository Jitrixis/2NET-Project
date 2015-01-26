using System;
using System.Collections.Generic;

namespace _2NET_Restaurant_Management_Software.Database
{
    class Waiter
    {
        public int WaiterId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual List<Bill> Bills { get; set; }
    }
}
