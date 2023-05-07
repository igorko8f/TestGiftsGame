using Codebase.Services;
using UniRx;

namespace Codebase.Customers
{
    public class CustomersHolder : ICustomerHolder
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly ILevelProgressService _levelProgressService;
        public IReadOnlyReactiveCollection<CustomerPresenter> Customers => _customer;
        private readonly IReactiveCollection<CustomerPresenter> _customer;
        
        private readonly CompositeDisposable _compositeDisposable;
        
        public CustomersHolder(
            ICustomerFactory customerFactory,
            ILevelProgressService levelProgressService)
        {
            _customerFactory = customerFactory;
            _levelProgressService = levelProgressService;
            
            _customer = new ReactiveCollection<CustomerPresenter>();
            _compositeDisposable = new CompositeDisposable();

            Customers.ObserveRemove()
                .Subscribe(_ => RemoveCustomer(_.Value))
                .AddTo(_compositeDisposable);
        }

        public void CreateInitialCustomers(int count)
        {
            for (int i = 0; i < count; i++)
                CreateAnotherCustomer();
        }
        
        private void RemoveCustomer(CustomerPresenter customer)
        {
            _levelProgressService.DecreaseCustomers();
            customer.RaiseSpawnPoint();
            CreateAnotherCustomer();
        }

        private void CreateAnotherCustomer()
        {
            if (_levelProgressService.CustomersCount.Value <= 0) return;

            var customer = _customerFactory.CreateCustomer();
            if (customer is null) return;
            
            _customer.Add(_customerFactory.CreateCustomer());
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}