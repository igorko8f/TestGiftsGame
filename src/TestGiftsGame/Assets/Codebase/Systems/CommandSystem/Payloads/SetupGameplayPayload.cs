using Codebase.Gameplay;
using Codebase.HUD;
using Codebase.WinLose;
using Zenject;

namespace Codebase.Systems.CommandSystem.Payloads
{
    public class SetupGameplayPayload : ICommandPayload
    {
        public GameplayView GameplayView;
        public WinLoseView WinLoseView;
        public HudView HudView;
        public DiContainer Container;

        public SetupGameplayPayload(GameplayView gameplayView, WinLoseView winLoseView, HudView hudView, DiContainer container)
        {
            GameplayView = gameplayView;
            WinLoseView = winLoseView;
            HudView = hudView;
            Container = container;
        }
    }
}