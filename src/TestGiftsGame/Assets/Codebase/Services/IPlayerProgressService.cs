using System;
using UniRx;

namespace Codebase.Services
{
    public interface IPlayerProgressService : IDisposable
    {
        IReadOnlyReactiveProperty<int> ResourcesCount { get; }
        IReadOnlyReactiveProperty<int> LastLevelIndex { get; }
        void AddResources(int amount);
        void SpendResources(int amount);
    }
}