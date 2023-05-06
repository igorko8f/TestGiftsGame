using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.ItemContainer;
using Codebase.MVP;
using UnityEngine;

namespace Codebase.Gameplay
{
    public interface IGameplayView : IView
    {
        CraftingSlot[] CraftingSlots { get; }
        DraggableItemContainer[] BoxContainers { get; }
        DraggableItemContainer[] BowContainers { get; }
        DraggableItemContainer[] DesignContainers { get; }
        Canvas Canvas { get; }
    }
}