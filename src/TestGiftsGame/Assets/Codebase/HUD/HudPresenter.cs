using Codebase.MVP;
using Codebase.Services;

namespace Codebase.HUD
{
    public class HudPresenter : BasePresenter<IHudView>
    {
        private readonly IPlayerProgressService _playerProgressService;
        private readonly ILevelProgressService _levelProgressService;

        public HudPresenter(
            IHudView viewContract,
            IPlayerProgressService playerProgressService,
            ILevelProgressService levelProgressService) 
            : base(viewContract)
        {
            _playerProgressService = playerProgressService;
            _levelProgressService = levelProgressService;
        }
        
        public void ConstructHUD()
        {
            AddDisposable(new PlayerResourcesPresenter(View.PlayerResources, _playerProgressService));
            AddDisposable(new CustomersCountPresenter(View.CustomersCount, _levelProgressService));
            AddDisposable(new TimerCountPresenter(View.CurrentTimer, _levelProgressService));
        }
    }
}