using System;
using Codebase.Craft;
using Codebase.Customers.Orders;
using Codebase.MVP;
using Codebase.Services;
using UniRx;
using UnityEngine;

namespace Codebase.Customers
{
    public class CustomerPresenter : BasePresenter<ICustomerView>
    {
        public event Action<CustomerPresenter, int> OnCustomerOrderComplete; 
        public event Action<CustomerPresenter> OnCustomerTimeLeft; 
        
        private readonly Customer _customerModel;
        private readonly CustomerSpawnPoint _spawnPoint;
        private readonly IDisposable _orderCompleteSubscription;

        private IObservable<long> _timer;
        private float _currentTimerValue = 0f;

        public CustomerPresenter(
            ICustomerView viewContract,
            IInputService inputService,
            BoxCraftingRecipes craftingRecipes,
            Customer customerModel,
            CustomerSpawnPoint spawnPoint) 
            : base(viewContract)
        {
            _customerModel = customerModel;
            _spawnPoint = spawnPoint;

            var orderPresenter =
                new OrderPresenter(View.OrderView, inputService, craftingRecipes, _customerModel.Order);

            _orderCompleteSubscription = orderPresenter.OnOrderComplete
                .Subscribe(_ => OnOrderComplete());

            View.ShowCustomerAnimation();
            SetupTimer();
        }

        private void UpdateTimer()
        {
            if (_currentTimerValue <= 0)
            {
                OnTimeLeft();
                return;
            }

            _currentTimerValue -= Time.fixedDeltaTime;
            var step = (1 / _customerModel.OrderPreparationTime);
            var normalizedValue = _currentTimerValue * step;
            
            View.SetTimer(normalizedValue);
        }

        private void SetupTimer()
        {
            _currentTimerValue = _customerModel.OrderPreparationTime;
            _timer = Observable.Timer(TimeSpan.FromSeconds(Time.fixedDeltaTime));
            _timer.Repeat()
                .Subscribe(_ => UpdateTimer())
                .AddTo(CompositeDisposable);
        }

        private void OnOrderComplete()
        {
            DisposeCustomerWithAnimation(LogOrderComplete);
        }

        private void OnTimeLeft()
        {
            DisposeCustomerWithAnimation(LogTimeLeft);
        }

        private void DisposeCustomerWithAnimation(Action action)
        {
            View.HideCustomerAnimation(() => DisposeCustomer(action));
        }

        private void DisposeCustomer(Action action)
        {
            _orderCompleteSubscription?.Dispose();
            RaiseSpawnPoint();
            action?.Invoke();

            View.Dispose();
            Dispose();
        }

        private void LogOrderComplete() => 
            OnCustomerOrderComplete?.Invoke(this, _customerModel.Order.GiftsInOrder.Count);
        
        private void LogTimeLeft() => 
            OnCustomerTimeLeft?.Invoke(this);

        private void RaiseSpawnPoint()
        {
            _spawnPoint.SetEmptyState(true);
        }
    }
}