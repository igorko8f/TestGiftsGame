using System.Collections.Generic;
using Codebase.Gifts;

namespace Codebase.Customers.Orders
{
    public class Order
    {
        public List<Gift> GiftsInOrder;

        public Order(List<Gift> giftsInOrder)
        {
            GiftsInOrder = giftsInOrder;
        }
    }
}