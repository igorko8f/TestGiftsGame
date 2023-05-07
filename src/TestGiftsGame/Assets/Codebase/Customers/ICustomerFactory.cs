using System;

namespace Codebase.Customers
{
    public interface ICustomerFactory : IDisposable
    {
        CustomerPresenter CreateCustomer();
    }
}