using Codebase.Customers.Orders;

namespace Codebase.Customers
{
    public class Customer
    {
        public Order Order;

        public Customer(Order order)
        {
            Order = order;
        }
    }
}