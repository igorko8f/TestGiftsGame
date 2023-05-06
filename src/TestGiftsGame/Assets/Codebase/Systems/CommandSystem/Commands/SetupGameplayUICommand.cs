using Codebase.Gameplay;
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

        public SetupGameplayUICommand(
            IInstantiator instantiator,
            IStaticDataService staticDataService,
            IPlayerProgressService playerProgressService)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
            _playerProgressService = playerProgressService;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var gamePresenter = _instantiator.Instantiate<GameplayPresenter>();
            gamePresenter.ConstructGameplay(_staticDataService
                    .GetConfigForLevel(_playerProgressService.LastLevelIndex.Value));
            
            Release();
        }
    }
}