using Codebase.Services;
using Codebase.Systems.CommandSystem.Payloads;
using Codebase.WinLose;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class ConstructEndLevelUICommand : Command
    {
        private readonly ILevelUIProvider _levelUIProvider;

        public ConstructEndLevelUICommand(ILevelUIProvider levelUIProvider)
        {
            _levelUIProvider = levelUIProvider;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var endLevelState = payload as EndLevelStatePayload;
            _levelUIProvider.WinLosePresenter.ConstructWinLoseUI(endLevelState?.LevelComplete ?? false);
            
            Release();
        }
    }
}