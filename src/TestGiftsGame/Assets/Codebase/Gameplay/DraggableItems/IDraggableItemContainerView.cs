using Codebase.MVP;

namespace Codebase.Gameplay.DraggableItems
{
    public interface IDraggableItemContainerView : IView
    {
        DraggableItem CreateViewForGift();
    }
}