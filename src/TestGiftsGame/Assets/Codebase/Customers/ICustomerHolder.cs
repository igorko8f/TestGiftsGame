using System;
using UniRx;

namespace Codebase.Customers
{
    public interface ICustomerHolder: IDisposable
    {
        IReadOnlyReactiveCollection<CustomerPresenter> Customers { get; }
        void CreateInitialCustomers(int count);
    }
}