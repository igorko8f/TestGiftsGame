using Codebase.MVP;

namespace Codebase.Customers
{
    public class CustomerPresenter : BasePresenter<ICustomerView>
    {
        private readonly Customer _customerModel;
        private readonly CustomerSpawnPoint _spawnPoint;

        public CustomerPresenter(
            ICustomerView viewContract,
            Customer customerModel,
            CustomerSpawnPoint spawnPoint) 
            : base(viewContract)
        {
            _customerModel = customerModel;
            _spawnPoint = spawnPoint;
        }
        
        public void RaiseSpawnPoint()
        {
            _spawnPoint.SetEmptyState(true);
        }
    }
}