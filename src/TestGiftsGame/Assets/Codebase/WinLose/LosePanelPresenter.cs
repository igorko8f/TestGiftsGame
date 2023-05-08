using Codebase.MVP;
using Codebase.StaticData;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Payloads;
using Codebase.Systems.CommandSystem.Signals;
using UniRx;

namespace Codebase.WinLose
{
    public class LosePanelPresenter : BasePresenter<IEndLevelPanelView>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public LosePanelPresenter(
            IEndLevelPanelView viewContract,
            ICommandDispatcher commandDispatcher) 
            : base(viewContract)
        {
            _commandDispatcher = commandDispatcher;
            
            View.OnNextLevelButtonPressed
                .Subscribe(_ => RestartLevel())
                .AddTo(CompositeDisposable);
        }

        private void RestartLevel()
        {
            _commandDispatcher.Dispatch<LoadNextLevelSignal>(new SceneNamePayload(SceneNames.GameplayScene));
        }
    }
}