using System;
using Codebase.Gameplay;
using Codebase.HUD;
using Codebase.WinLose;

namespace Codebase.Services
{
    public interface ILevelUIProvider : IDisposable
    {
        GameplayPresenter GamePlayPresenter { get; }
        WinLosePresenter WinLosePresenter { get; }
        HudPresenter HudPresenter { get; }
        void SetWinLosePresenter(WinLosePresenter winLosePresenter);
        void SetGameplayPresenter(GameplayPresenter gameplayPresenter);
        void SetHudPresenter(HudPresenter hudPresenter);
    }
}