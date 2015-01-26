using System;
using System.Collections.Generic;

namespace _2NET_Restaurant_Management_Software.Database
{
    class Table
    {
        public int TableId { get; set; }

        public bool isEmpty { get; set; }

        public int Chair_number { get; set; }

        public virtual List<Bill> Bills { get; set; }
    }
}
