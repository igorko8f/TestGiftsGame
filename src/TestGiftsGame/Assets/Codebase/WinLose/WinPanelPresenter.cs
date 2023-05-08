using Codebase.MVP;
using Codebase.Services;
using Codebase.StaticData;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Payloads;
using Codebase.Systems.CommandSystem.Signals;
using UniRx;

namespace Codebase.WinLose
{
    public class WinPanelPresenter : BasePresenter<IEndLevelPanelView>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPlayerProgressService _playerProgressService;

        public WinPanelPresenter(
            IEndLevelPanelView viewContract,
            ICommandDispatcher commandDispatcher,
            IPlayerProgressService playerProgressService) 
            : base(viewContract)
        {
            _commandDispatcher = commandDispatcher;
            _playerProgressService = playerProgressService;

            View.OnNextLevelButtonPressed
                .Subscribe(_ => LoadNextLevel())
                .AddTo(CompositeDisposable);
        }

        private void LoadNextLevel()
        {           
            _playerProgressService.IncreaseLevelIndex();
            _commandDispatcher.Dispatch<LoadNextLevelSignal>(new SceneNamePayload(SceneNames.GameplayScene));
        }
    }
}