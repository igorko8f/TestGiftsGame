using UniRx;
using UnityEngine;

namespace Codebase.Services
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public IReadOnlyReactiveProperty<int> ResourcesCount => _resourcesCount;
        public IReadOnlyReactiveProperty<int> LastLevelIndex => _lastLevelIndex;
        
        private readonly ReactiveProperty<int> _resourcesCount;
        private readonly ReactiveProperty<int> _lastLevelIndex;

        private readonly CompositeDisposable _compositeDisposable;
        private readonly ISaveLoadService _saveLoadService;

        public PlayerProgressService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            
            var playerState = _saveLoadService.LoadPlayerState();

            _compositeDisposable = new CompositeDisposable();
            _resourcesCount = new ReactiveProperty<int>(playerState.ResourcesCount);
            _lastLevelIndex = new ReactiveProperty<int>(playerState.LastLevelIndex);
            
            ResourcesCount
                .Subscribe(_ => StashChanges())
                .AddTo(_compositeDisposable);
            
            LastLevelIndex
                .Subscribe(_ => StashChanges())
                .AddTo(_compositeDisposable);
        }

        public void AddResources(int amount)
        {
            if (amount < 0) return;
            _resourcesCount.Value += amount;
        }

        public void SpendResources(int amount)
        {
            var resourcesToSpend = Mathf.Abs(amount);
            if (_resourcesCount.Value < resourcesToSpend) return;

            _resourcesCount.Value -= resourcesToSpend;
        }

        private void StashChanges()
        {
            _saveLoadService.SavePlayerState(_resourcesCount.Value, _lastLevelIndex.Value);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}