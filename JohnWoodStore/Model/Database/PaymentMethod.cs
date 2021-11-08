using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnWoodStore.Model.Database
{
    class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
