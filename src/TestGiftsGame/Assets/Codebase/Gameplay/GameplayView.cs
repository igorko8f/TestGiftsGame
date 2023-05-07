using Codebase.Customers;
using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.ItemContainer;
using Codebase.HUD;
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
        public TrashBin TrashBin => _trashBin;
        public CustomerView[] Customers => _customers;
        public CustomerSpawnPoint[] CustomerSpawnPoints => _spawnPoints;
        public ChangeableTextView PlayerResources => _playerResources;
        public ChangeableTextView CustomersCount => _customersCount;
        public ChangeableTextView CurrentTimer => _currentTimer;
        public Canvas Canvas => _canvas;

        [Header("Box Crafting")]
        [SerializeField] private CraftingSlot[] _craftingSlots;
        [SerializeField] private DraggableItemContainer[] _boxContainers;
        [SerializeField] private DraggableItemContainer[] _bowContainers;
        [SerializeField] private DraggableItemContainer[] _designContainers;
        [SerializeField] private TrashBin _trashBin;

        [Header("Customers")] 
        [SerializeField] private CustomerView[] _customers; 
        [SerializeField] private CustomerSpawnPoint[] _spawnPoints;

        [Header("HUD")] 
        [SerializeField] private ChangeableTextView _playerResources;
        [SerializeField] private ChangeableTextView _customersCount;
        [SerializeField] private ChangeableTextView _currentTimer;
        
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