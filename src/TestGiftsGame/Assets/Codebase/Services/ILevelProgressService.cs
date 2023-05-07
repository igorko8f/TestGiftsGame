using System;
using UniRx;

namespace Codebase.Services
{
    public interface ILevelProgressService: IDisposable
    {
        IReadOnlyReactiveProperty<int> CustomersCount { get; }
        IReadOnlyReactiveProperty<float> CurrentTime { get; }
        void DecreaseCustomers();
    }
}