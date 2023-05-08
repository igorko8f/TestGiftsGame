using Codebase.Customers;
using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.DraggableItems;
using Codebase.HUD;
using Codebase.MVP;
using UnityEngine;

namespace Codebase.Gameplay
{
    [RequireComponent(typeof(Canvas))]
    public class GameplayView : RawView, IGameplayView
    {
        public CraftingSlotView[] CraftingSlots => _craftingSlots;
        public DraggableItemContainer[] DesignContainers => _boxContainers;
        public DraggableItemContainer[] BoxContainers => _bowContainers;
        public DraggableItemContainer[] BowContainers => _designContainers;
        public TrashBin TrashBin => _trashBin;
        public CustomerView[] Customers => _customers;
        public CustomerSpawnPoint[] CustomerSpawnPoints => _spawnPoints;
        public Canvas Canvas => _canvas;

        [Header("Box Crafting")]
        [SerializeField] private CraftingSlotView[] _craftingSlots;
        [SerializeField] private DraggableItemContainer[] _boxContainers;
        [SerializeField] private DraggableItemContainer[] _bowContainers;
        [SerializeField] private DraggableItemContainer[] _designContainers;
        [SerializeField] private TrashBin _trashBin;

        [Header("Customers")] 
        [SerializeField] private CustomerView[] _customers; 
        [SerializeField] private CustomerSpawnPoint[] _spawnPoints;

        private Canvas _canvas;

        public void Initialize()
        {
            _canvas = GetComponent<Canvas>();
            foreach (var spawnPoint in _spawnPoints)
                spawnPoint.SetEmptyState(true);
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