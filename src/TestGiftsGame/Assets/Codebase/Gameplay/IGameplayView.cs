using Codebase.Customers;
using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.ItemContainer;
using Codebase.HUD;
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
        TrashBin TrashBin { get; }
        CustomerView[] Customers { get; }
        CustomerSpawnPoint[] CustomerSpawnPoints { get; }
        ChangeableTextView PlayerResources { get; }
        ChangeableTextView CustomersCount { get; }
        ChangeableTextView CurrentTimer { get; }
        Canvas Canvas { get; }
        void Show();
        void Hide();
    }
}