using Codebase.MVP;
using Codebase.Services;
using Codebase.Systems.CommandSystem;

namespace Codebase.WinLose
{
    public class WinLosePresenter : BasePresenter<IWinLoseView>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPlayerProgressService _playerProgressService;

        public WinLosePresenter(
            IWinLoseView viewContract,
            ICommandDispatcher commandDispatcher,
            IPlayerProgressService playerProgressService) 
            : base(viewContract)
        {
            _commandDispatcher = commandDispatcher;
            _playerProgressService = playerProgressService;
        }

        public void ConstructWinLoseUI(bool victory)
        {
            View.FadeBackground();
            var endLevelPanelView = victory ? View.GetWinPanel() : View.GetLosePanel();
            
            if (victory)
                AddDisposable(new WinPanelPresenter(endLevelPanelView, _commandDispatcher, _playerProgressService));
            else
                AddDisposable(new LosePanelPresenter(endLevelPanelView, _commandDispatcher));
        }
    }
}