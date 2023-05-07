using Codebase.Customers.Orders;
using Codebase.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Customers
{
    public class CustomerView : RawView, ICustomerView
    {
        public OrderView OrderView => _orderView;
        [SerializeField] private OrderView _orderView;

        [SerializeField] private Image _timerSlice;
        [SerializeField] private Image _timerBack;

        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _lateColor;
        
        public void Initialize()
        {
        }

        public void SetTimer(float value, float step)
        {
            _timerSlice.fillAmount = value;
            var timerColor = Color.Lerp(_normalColor, _lateColor, step);
        }
    }
}