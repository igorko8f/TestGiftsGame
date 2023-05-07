using System;

namespace Codebase.Customers
{
    public interface ICustomerHolder: IDisposable
    { 
        void CreateInitialCustomers(int count);
        void RemoveCustomer(CustomerPresenter customer);
    }
}