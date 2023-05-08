using Codebase.Customers;
using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.DraggableItems;
using Codebase.HUD;
using Codebase.MVP;
using UnityEngine;

namespace Codebase.Gameplay
{
    public interface IGameplayView : IView
    {
        CraftingSlotView[] CraftingSlots { get; }
        DraggableItemContainer[] BoxContainers { get; }
        DraggableItemContainer[] BowContainers { get; }
        DraggableItemContainer[] DesignContainers { get; }
        TrashBin TrashBin { get; }
        CustomerView[] Customers { get; }
        CustomerSpawnPoint[] CustomerSpawnPoints { get; }
        Canvas Canvas { get; }
        void Show();
        void Hide();
    }
}