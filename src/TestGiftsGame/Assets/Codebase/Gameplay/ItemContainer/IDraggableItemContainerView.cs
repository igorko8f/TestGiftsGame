using Codebase.MVP;

namespace Codebase.Gameplay.ItemContainer
{
    public interface IDraggableItemContainerView : IView
    {
        DraggableItem CreateViewForGift();
    }
}