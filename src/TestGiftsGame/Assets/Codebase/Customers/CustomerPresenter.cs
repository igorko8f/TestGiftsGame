using System;
using Codebase.Craft;
using Codebase.Customers.Orders;
using Codebase.MVP;
using Codebase.Services;
using UniRx;

namespace Codebase.Customers
{
    public class CustomerPresenter : BasePresenter<ICustomerView>
    {
        public event Action<CustomerPresenter> OnCustomerOrderComplete; 
        
        private readonly Customer _customerModel;
        private readonly CustomerSpawnPoint _spawnPoint;
        private readonly IDisposable _orderCompleteSubscription;

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
        }

        private void OnOrderComplete()
        {
            _orderCompleteSubscription?.Dispose();
            RaiseSpawnPoint();
            OnCustomerOrderComplete?.Invoke(this);
            
            View.Dispose();
            Dispose();
        }

        private void RaiseSpawnPoint()
        {
            _spawnPoint.SetEmptyState(true);
        }
    }
}