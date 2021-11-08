using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnWoodStore.Model.Database
{
    class Check
    { 
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateOfCheck { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
