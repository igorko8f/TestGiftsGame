using Codebase.Gameplay;
using Codebase.Level;
using Codebase.Services;
using Codebase.Systems.CommandSystem.Payloads;
using Zenject;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class SetupGameplayUICommand : Command
    {
        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly FadeScreen _fadeScreen;

        public SetupGameplayUICommand(
            IInstantiator instantiator,
            IStaticDataService staticDataService,
            IPlayerProgressService playerProgressService,
            FadeScreen fadeScreen)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
            _playerProgressService = playerProgressService;
            _fadeScreen = fadeScreen;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var gamePresenter = _instantiator.Instantiate<GameplayPresenter>();
            gamePresenter.ConstructGameplay(_staticDataService
                    .GetConfigForLevel(_playerProgressService.LastLevelIndex.Value));
            
            
            _fadeScreen.FadeOut();
            Release();
        }
    }
}