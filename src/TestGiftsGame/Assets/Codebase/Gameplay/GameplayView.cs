using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.ItemContainer;
using Codebase.MVP;
using UnityEngine;

namespace Codebase.Gameplay
{
    [RequireComponent(typeof(Canvas))]
    public class GameplayView : RawView, IGameplayView
    {
        public CraftingSlot[] CraftingSlots => _craftingSlots;
        public DraggableItemContainer[] DesignContainers => _boxContainers;
        public DraggableItemContainer[] BoxContainers => _bowContainers;
        public DraggableItemContainer[] BowContainers => _designContainers;
        public Canvas Canvas => _canvas;

        [SerializeField] private CraftingSlot[] _craftingSlots;
        [SerializeField] private DraggableItemContainer[] _boxContainers;
        [SerializeField] private DraggableItemContainer[] _bowContainers;
        [SerializeField] private DraggableItemContainer[] _designContainers;

        private Canvas _canvas;

        public void Initialize()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}