using System.Collections.Generic;
using Codebase.Services;

namespace Codebase.Customers
{
    public class CustomersHolder : ICustomerHolder
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly ILevelProgressService _levelProgressService;
        private readonly List<CustomerPresenter> _customers = new();
        
        public CustomersHolder(
            ICustomerFactory customerFactory,
            ILevelProgressService levelProgressService)
        {
            _customerFactory = customerFactory;
            _levelProgressService = levelProgressService;
        }

        public void CreateInitialCustomers(int count)
        {
            for (int i = 0; i < count; i++)
                CreateAnotherCustomer();
        }

        public void RemoveCustomer(CustomerPresenter customer)
        {
            customer.OnCustomerOrderComplete -= RemoveCustomer;
            
            _levelProgressService.DecreaseCustomers();
            _customers.Remove(customer);
            
            CreateAnotherCustomer();
        }

        private void CreateAnotherCustomer()
        {
            if (_levelProgressService.CustomersCount.Value <= 0) return;

            var customer = _customerFactory.CreateCustomer();
            if (customer is null) return;

            customer.OnCustomerOrderComplete += RemoveCustomer;
            _customers.Add(customer);
        }

        public void Dispose()
        {
            foreach (var customer in _customers)
                customer.OnCustomerOrderComplete -= RemoveCustomer;
            
            _customers.Clear();
        }
    }
}