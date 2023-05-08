using System.Linq;
using UniRx;
using UnityEngine;

namespace Codebase.Services
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public IReadOnlyReactiveProperty<int> ResourcesCount => _resourcesCount;
        public IReadOnlyReactiveProperty<int> LastLevelIndex => _lastLevelIndex;
        public IReadOnlyReactiveCollection<string> BoughtCraftingSlots => _boughtCraftingSlots;

        private readonly ReactiveProperty<int> _resourcesCount;
        private readonly ReactiveProperty<int> _lastLevelIndex;
        private readonly ReactiveCollection<string> _boughtCraftingSlots;

        private readonly CompositeDisposable _compositeDisposable;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        public PlayerProgressService(
            ISaveLoadService saveLoadService,
            IStaticDataService staticDataService)
        {
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;

            var playerState = _saveLoadService.LoadPlayerState();

            _compositeDisposable = new CompositeDisposable();
            _resourcesCount = new ReactiveProperty<int>(playerState.ResourcesCount);
            _lastLevelIndex = new ReactiveProperty<int>(playerState.LastLevelIndex);
            _boughtCraftingSlots = new ReactiveCollection<string>(playerState.BoughtCraftingSlots);
            
            ResourcesCount
                .Subscribe(_ => StashChanges())
                .AddTo(_compositeDisposable);
            
            LastLevelIndex
                .Subscribe(_ => StashChanges())
                .AddTo(_compositeDisposable);

            BoughtCraftingSlots.ObserveAdd()
                .Subscribe(_ => StashChanges())
                .AddTo(_compositeDisposable);
        }

        public void AddResources(int amount)
        {
            if (amount < 0) return;
            _resourcesCount.Value += amount;
        }

        public bool SpendResources(int amount)
        {
            var resourcesToSpend = Mathf.Abs(amount);
            if (_resourcesCount.Value < resourcesToSpend) return false;

            _resourcesCount.Value -= resourcesToSpend;
            return true;
        }

        public void IncreaseLevelIndex()
        {
            if (_lastLevelIndex.Value > _staticDataService.TotalLevelsCount)
            {
                _lastLevelIndex.Value = 1;
                return;
            }

            _lastLevelIndex.Value += 1;
        }

        public void BuyCraftingSlot(string id)
        {
            if (_boughtCraftingSlots.Contains(id)) return;
            _boughtCraftingSlots.Add(id);
        }
        
        private void StashChanges()
        {
            _saveLoadService.SavePlayerState(_resourcesCount.Value, _lastLevelIndex.Value, _boughtCraftingSlots.ToArray());
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}