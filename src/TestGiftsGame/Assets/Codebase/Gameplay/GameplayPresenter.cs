using Codebase.Customers;
using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.ItemContainer;
using Codebase.HUD;
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
            
            ConstructHUD();
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

        private void ConstructHUD()
        {
            AddDisposable(new PlayerResourcesPresenter(View.PlayerResources, _playerProgressService));
            AddDisposable(new CustomersCountPresenter(View.CustomersCount, _levelProgressService));
            AddDisposable(new TimerCountPresenter(View.CurrentTimer, _levelProgressService));
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
            foreach (var view in View.CraftingSlots)
            {
                AddDisposable(new CraftingSlotPresenter(view, _staticDataService.CraftingRecipes, _inputService, View.Canvas,
                    true));
            }
        }

        private void ConstructTrashBin() => 
            AddDisposable(new TrashBinPresenter(View.TrashBin, _inputService));
    }
}