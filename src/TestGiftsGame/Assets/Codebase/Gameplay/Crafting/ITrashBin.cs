using System;
using Codebase.MVP;
using UniRx;

namespace Codebase.Gameplay.Crafting
{
    public interface ITrashBin : IView
    {
        IObservable<Unit> OnItemDropped { get; }
    }
}