using Codebase.Customers.Orders;
using Codebase.MVP;
using DG.Tweening;

namespace Codebase.Customers
{
    public interface ICustomerView : IView
    {
        OrderView OrderView { get; }
        void SetTimer(float value);
        void ShowCustomerAnimation();
        void HideCustomerAnimation(TweenCallback callback);
    }
}