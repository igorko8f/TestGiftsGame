using Codebase.Gameplay.Crafting;
using Codebase.Gameplay.ItemContainer;
using Codebase.Level;
using Codebase.MVP;
using Codebase.Services;

namespace Codebase.Gameplay
{
    public class GameplayPresenter : BasePresenter<IGameplayView>
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;

        public GameplayPresenter(
            IGameplayView viewContract,
            IStaticDataService staticDataService,
            IInputService inputService) 
            : base(viewContract)
        {
            _staticDataService = staticDataService;
            _inputService = inputService;
        }

        public void ConstructGameplay(LevelConfiguration levelConfiguration)
        {
            View.Hide();
            
            ConstructCraftSlots();
            ConstructBoxVariants(levelConfiguration);
            ConstructBowVariants(levelConfiguration);
            ConstructsDesignVariants(levelConfiguration);
            
            View.Show();
        }

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
    }
}