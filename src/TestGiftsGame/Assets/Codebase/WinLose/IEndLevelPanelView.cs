using System;
using Codebase.MVP;
using UniRx;

namespace Codebase.WinLose
{
    public interface IEndLevelPanelView : IView
    {
        IObservable<Unit> OnNextLevelButtonPressed { get; }
    }
}