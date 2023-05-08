using System;
using UniRx;

namespace Codebase.Services
{
    public interface IPlayerProgressService : IDisposable
    {
        IReadOnlyReactiveProperty<int> ResourcesCount { get; }
        IReadOnlyReactiveProperty<int> LastLevelIndex { get; }
        IReadOnlyReactiveCollection<string> BoughtCraftingSlots { get; }
        void AddResources(int amount);
        bool SpendResources(int amount);
        void IncreaseLevelIndex();
        void BuyCraftingSlot(string id);
    }
}