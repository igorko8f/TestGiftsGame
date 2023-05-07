using Codebase.Customers.Orders;

namespace Codebase.Customers
{
    public class Customer
    {
        public Order Order;
        public float OrderPreparationTime;

        public Customer(Order order, float orderPreparationTime)
        {
            Order = order;
            OrderPreparationTime = orderPreparationTime;
        }
    }
}