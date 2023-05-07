using Codebase.Customers.Orders;
using Codebase.MVP;

namespace Codebase.Customers
{
    public interface ICustomerView : IView
    {
        OrderView OrderView { get; }
        void SetTimer(float value, float step);
    }
}