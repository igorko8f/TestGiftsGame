using UniRx;

namespace Codebase.Services
{
    public class LevelProgressService : ILevelProgressService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _playerProgressService;
        
        public IReadOnlyReactiveProperty<int> CustomersCount => _customersCount;
        public IReadOnlyReactiveProperty<float> CurrentTime => _currentTime;
        
        private readonly ReactiveProperty<int> _customersCount;
        private readonly ReactiveProperty<float> _currentTime;
        private readonly CompositeDisposable _compositeDisposable;

        public LevelProgressService(
            IStaticDataService staticDataService,
            IPlayerProgressService playerProgressService)
        {
            _staticDataService = staticDataService;
            _playerProgressService = playerProgressService;
            _compositeDisposable = new CompositeDisposable();

            var levelConfig = _staticDataService.GetConfigForLevel(_playerProgressService.LastLevelIndex.Value);
            _customersCount = new ReactiveProperty<int>(levelConfig.CustomersCount);
            _currentTime = new ReactiveProperty<float>(levelConfig.OrderPreparationTime * levelConfig.CustomersCount);
        }

        public void DecreaseCustomers()
        {
            if (_customersCount.Value <= 0) return;
            _customersCount.Value -= 1;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}