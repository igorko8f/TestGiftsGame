using Codebase.Installers;
using Codebase.Services;
using Codebase.Systems.CommandSystem.Payloads;
using Codebase.WinLose;
using UnityEngine;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class DisposeWinLoseCommand : Command
    {
        private readonly ILevelUIProvider _levelUIProvider;

        public DisposeWinLoseCommand(ILevelUIProvider levelUIProvider)
        {
            _levelUIProvider = levelUIProvider;
        }

        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            
            _levelUIProvider.WinLosePresenter.Dispose();
            
            Release();
        }
    }
}