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

        [SerializeField] private Image _customerImage;
        [SerializeField] private Sprite _customerSad;
        [SerializeField] private float _sadMoodValue;

        [SerializeField] private Image _timerSlice;
        [SerializeField] private Image _timerBack;

        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _lateColor;

        private bool _isSad = false;
        
        public void Initialize()
        {
            _isSad = false;
            _timerSlice.fillAmount = 1;
        }

        public void SetTimer(float value)
        {
            _timerSlice.fillAmount = value;
            var timerColor = Color.Lerp(_normalColor, _lateColor, 1 - value);
            _timerSlice.color = timerColor;
            _timerBack.color = timerColor;

            if (_timerSlice.fillAmount <= _sadMoodValue && _isSad == false)
            {
                _customerImage.sprite = _customerSad;
                _isSad = true;
            }
        }
    }
}