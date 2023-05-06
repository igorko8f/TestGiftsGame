using System;
using Codebase.Gameplay.ItemContainer;
using Codebase.MVP;
using UniRx;

namespace Codebase.Gameplay.Crafting
{
    public interface ICraftingSlotView : IView
    {
        IObservable<Unit> OnItemDropped { get; }
        void SetOpenState(bool openState);
        DraggableItem CreateViewForGift();
    }
}