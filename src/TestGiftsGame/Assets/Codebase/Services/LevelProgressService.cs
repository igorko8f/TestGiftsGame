using System;
using UniRx;

namespace Codebase.Services
{
    public class LevelProgressService : ILevelProgressService
    {
        public IReadOnlyReactiveProperty<int> CustomersCount => _customersCount;
        public IReadOnlyReactiveProperty<float> CurrentTime => _currentTime;
        
        private readonly ReactiveProperty<int> _customersCount;
        private readonly ReactiveProperty<float> _currentTime;
        private readonly CompositeDisposable _compositeDisposable;

        private float _currentTimerValue = 0f;
        
        public LevelProgressService(
            IStaticDataService staticDataService,
            IPlayerProgressService playerProgressService)
        {
            _compositeDisposable = new CompositeDisposable();

            var levelConfig = staticDataService.GetConfigForLevel(playerProgressService.LastLevelIndex.Value);
            _currentTimerValue = levelConfig.OrderPreparationTime * levelConfig.CustomersCount;
            
            _customersCount = new ReactiveProperty<int>(levelConfig.CustomersCount);
            _currentTime = new ReactiveProperty<float>(_currentTimerValue);

            Observable.Timer(TimeSpan.FromSeconds(1f))
                .Repeat()
                .Subscribe(_ => UpdateTimer())
                .AddTo(_compositeDisposable);
        }

        public void DecreaseCustomers()
        {
            if (_customersCount.Value <= 0) return;
            _customersCount.Value -= 1;
        }

        private void UpdateTimer()
        {
            if (_currentTimerValue <= 0)
            {
                //TriggerLose
                Dispose();
            }

            _currentTimerValue -= 1f;
            _currentTime.Value = _currentTimerValue;
        }
        
        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}