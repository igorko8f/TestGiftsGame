using Codebase.Gameplay;
using Codebase.HUD;
using Codebase.Level;
using Codebase.Services;
using Codebase.Systems.CommandSystem.Payloads;
using Codebase.WinLose;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class SetupGameplayUICommand : Command
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly ILevelUIProvider _levelUIProvider;
        private readonly FadeScreen _fadeScreen;

        public SetupGameplayUICommand(
            IStaticDataService staticDataService,
            IPlayerProgressService playerProgressService,
            ILevelUIProvider levelUIProvider,
            FadeScreen fadeScreen)
        {
            _staticDataService = staticDataService;
            _playerProgressService = playerProgressService;
            _levelUIProvider = levelUIProvider;
            _fadeScreen = fadeScreen;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var setupPayload = payload as SetupGameplayPayload;
            var container = setupPayload.Container;
            
            var gamePlayView = container.InstantiatePrefabForComponent<IGameplayView>(setupPayload.GameplayView);
            var winLoseView = container.InstantiatePrefabForComponent<IWinLoseView>(setupPayload.WinLoseView);
            var hudView = container.InstantiatePrefabForComponent<IHudView>(setupPayload.HudView);

            _levelUIProvider.SetGameplayPresenter(container.Instantiate<GameplayPresenter>(new[] {gamePlayView}));
            _levelUIProvider.SetWinLosePresenter(container.Instantiate<WinLosePresenter>(new[] {winLoseView}));
            _levelUIProvider.SetHudPresenter(container.Instantiate<HudPresenter>(new[] {hudView}));
            
            _levelUIProvider.GamePlayPresenter.ConstructGameplay(_staticDataService
                    .GetConfigForLevel(_playerProgressService.LastLevelIndex.Value));
            
            _levelUIProvider.HudPresenter.ConstructHUD();
            
            _fadeScreen.FadeOut();
            Release();
        }
    }
}