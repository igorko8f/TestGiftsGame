using Codebase.Gameplay;
using Codebase.HUD;
using Codebase.Services;
using Codebase.Systems.CommandSystem.Payloads;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class DisposeGameplayCommand : Command
    {
        private readonly ILevelUIProvider _levelUIProvider;

        public DisposeGameplayCommand(ILevelUIProvider levelUIProvider)
        {
            _levelUIProvider = levelUIProvider;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            
            _levelUIProvider.GamePlayPresenter.Dispose();
            _levelUIProvider.HudPresenter.Dispose();
            
            Release();
        }
    }
}