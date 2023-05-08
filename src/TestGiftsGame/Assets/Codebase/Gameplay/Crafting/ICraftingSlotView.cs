using System;
using Codebase.Gameplay.DraggableItems;
using Codebase.MVP;
using UniRx;

namespace Codebase.Gameplay.Crafting
{
    public interface ICraftingSlotView : IView
    {
        IObservable<Unit> OnItemDropped { get; }
        IObservable<Unit> OnBuySlot { get; }
        void SetOpenState(bool openState, int price);
        DraggableItem CreateViewForGift();
    }
}