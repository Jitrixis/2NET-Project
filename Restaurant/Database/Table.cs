using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Database
{
    class Table
    {
        public int TableId { get; set; }

        public bool isEmpty { get; set; }

        public int Chair_number { get; set; }

        public virtual List<Bill> Bills { get; set; }
    }
}
