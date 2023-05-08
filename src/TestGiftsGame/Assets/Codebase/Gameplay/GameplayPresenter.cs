using Codebase.Customers;
using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.DraggableItems;
using Codebase.Level;
using Codebase.MVP;
using Codebase.Services;

namespace Codebase.Gameplay
{
    public class GameplayPresenter : BasePresenter<IGameplayView>
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly ILevelProgressService _levelProgressService;
        private readonly IInputService _inputService;

        private ICustomerFactory _customerFactory;
        private ICustomerHolder _customerHolder;

        public GameplayPresenter(
            IGameplayView viewContract,
            IStaticDataService staticDataService,
            IPlayerProgressService playerProgressService,
            ILevelProgressService levelProgressService,
            IInputService inputService) 
            : base(viewContract)
        {
            _staticDataService = staticDataService;
            _playerProgressService = playerProgressService;
            _levelProgressService = levelProgressService;
            _inputService = inputService;
        }

        public void ConstructGameplay(LevelConfiguration levelConfiguration)
        {
            View.Hide();

            ConstructCustomerFactory(levelConfiguration);
            
            ConstructCraftSlots();
            ConstructBoxVariants(levelConfiguration);
            ConstructBowVariants(levelConfiguration);
            ConstructsDesignVariants(levelConfiguration);
            ConstructTrashBin();
            
            CreateInitialCustomers();
            
            View.Show();
        }

        private void ConstructCustomerFactory(LevelConfiguration levelConfiguration)
        {
            _customerFactory = CreateCustomerFactory(levelConfiguration);
            _customerHolder = CreateCustomerHolder();
            
            AddDisposable(_customerFactory);
            AddDisposable(_customerHolder);
        }

        private void CreateInitialCustomers()
        {
            _customerHolder.CreateInitialCustomers(View.CustomerSpawnPoints.Length);
        }

        private ICustomerFactory CreateCustomerFactory(LevelConfiguration levelConfiguration) =>
            new CustomerFactory(
                _staticDataService.CraftingRecipes,
                levelConfiguration,
                _inputService,
                View.Customers,
                View.CustomerSpawnPoints);

        private ICustomerHolder CreateCustomerHolder() =>
            new CustomersHolder(
                _customerFactory, 
                _levelProgressService, 
                _playerProgressService,
                _staticDataService.PriceList);

        private void ConstructTrashBin() => 
            AddDisposable(new TrashBinPresenter(View.TrashBin, _inputService));

        private void ConstructsDesignVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableDesigns.Length; i++)
            {
                if (i >= View.DesignContainers.Length) break;
                var giftPart = levelConfiguration.AvailableDesigns[i];
                var view = View.DesignContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, View.Canvas, giftPart));
            }
        }

        private void ConstructBowVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableBows.Length; i++)
            {
                if (i >= View.BowContainers.Length) break;
                var giftPart = levelConfiguration.AvailableBows[i];
                var view = View.BowContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, View.Canvas, giftPart));
            }
        }

        private void ConstructBoxVariants(LevelConfiguration levelConfiguration)
        {
            for (int i = 0; i < levelConfiguration.AvailableBoxes.Length; i++)
            {
                if (i >= View.BoxContainers.Length) break;
                var giftPart = levelConfiguration.AvailableBoxes[i];
                var view = View.BoxContainers[i];

                AddDisposable(new DraggableItemContainerPresenter(view, _inputService, View.Canvas, giftPart));
            }
        }

        private void ConstructCraftSlots()
        {
            for (int i = 0; i < _staticDataService.CraftingSlots.Length; i++)
            {
                if (i >= View.CraftingSlots.Length) break;
                var view = View.CraftingSlots[i];
                var craftingSlotConfig = _staticDataService.CraftingSlots[i];
                
                AddDisposable(new CraftingSlotPresenter(view, _staticDataService.CraftingRecipes, 
                    _inputService, _playerProgressService, craftingSlotConfig, View.Canvas));
            }
        }
    }
}