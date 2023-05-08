using Codebase.Gameplay;
using Codebase.HUD;
using Codebase.WinLose;

namespace Codebase.Services
{
    public class LevelUIProvider : ILevelUIProvider
    {
        public GameplayPresenter GamePlayPresenter { get; private set; }
        public WinLosePresenter WinLosePresenter { get; private set; }
        public HudPresenter HudPresenter { get; private set; }

        public void SetGameplayPresenter(GameplayPresenter gameplayPresenter) => GamePlayPresenter = gameplayPresenter;
        public void SetWinLosePresenter(WinLosePresenter winLosePresenter) => WinLosePresenter = winLosePresenter;
        public void SetHudPresenter(HudPresenter hudPresenter) => HudPresenter = hudPresenter;

        public void Dispose()
        {
        }
    }
}