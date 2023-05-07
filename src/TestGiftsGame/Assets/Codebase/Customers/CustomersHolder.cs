using System.Collections.Generic;
using Codebase.Services;
using Codebase.StaticData;

namespace Codebase.Customers
{
    public class CustomersHolder : ICustomerHolder
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly ILevelProgressService _levelProgressService;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly PriceList _priceList;
        private readonly List<CustomerPresenter> _customers = new();
        
        public CustomersHolder(
            ICustomerFactory customerFactory,
            ILevelProgressService levelProgressService,
            IPlayerProgressService playerProgressService,
            PriceList priceList)
        {
            _customerFactory = customerFactory;
            _levelProgressService = levelProgressService;
            _playerProgressService = playerProgressService;
            _priceList = priceList;
        }

        public void CreateInitialCustomers(int count)
        {
            for (int i = 0; i < count; i++)
                CreateAnotherCustomer();
        }

        private void TrackServedCustomer(CustomerPresenter customer, int ordersCount)
        {
            _levelProgressService.DecreaseCustomers();
            _playerProgressService.AddResources(_priceList.GetRewardByOrderCount(ordersCount));
            RemoveCustomer(customer);
        }

        private void RemoveCustomer(CustomerPresenter customer)
        {
            UnsubscribeFromCustomerEvents(customer);
            _customers.Remove(customer);
            CreateAnotherCustomer();
        }

        private void CreateAnotherCustomer()
        {
            if (_levelProgressService.CustomersCount.Value <= 0) return;

            var customer = _customerFactory.CreateCustomer();
            if (customer is null) return;

            SubscribeOnCustomerEvents(customer);
            _customers.Add(customer);
        }

        private void SubscribeOnCustomerEvents(CustomerPresenter customer)
        {
            customer.OnCustomerOrderComplete += TrackServedCustomer;
            customer.OnCustomerTimeLeft += RemoveCustomer;
        }
        
        private void UnsubscribeFromCustomerEvents(CustomerPresenter customer)
        {
            customer.OnCustomerOrderComplete -= TrackServedCustomer;
            customer.OnCustomerTimeLeft -= RemoveCustomer;
        }
        
        public void Dispose()
        {
            foreach (var customer in _customers)
                UnsubscribeFromCustomerEvents(customer);
            
            _customers.Clear();
        }
    }
}