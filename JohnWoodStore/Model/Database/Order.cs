using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnWoodStore.Model.Database
{
    class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime DateOfOrdering { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<AccountingForOrders> AccountingForOrders { get; set; }
        public Check Check { get; set; }
    }
}
