using Codebase.Craft;
using Codebase.MVP;
using Codebase.Services;
using UniRx;
using UnityEngine;

namespace Codebase.Gameplay.Crafting
{
    public class CraftingSlotPresenter : BasePresenter<ICraftingSlotView>
    {
        private readonly BoxCraftingRecipes _craftingRecipes;
        private readonly IInputService _inputService;
        private readonly Canvas _gameplayCanvas;

        private GiftDraggablePresenter _generatedDraggableGift;
        
        public CraftingSlotPresenter(
            ICraftingSlotView viewContract,
            BoxCraftingRecipes craftingRecipes,
            IInputService inputService,
            Canvas canvas,
            bool openSlot) 
            : base(viewContract)
        {
            _craftingRecipes = craftingRecipes;
            _inputService = inputService;
            _gameplayCanvas = canvas;
            
            View.OnItemDropped
                .Subscribe(_ => OnItemDropped())
                .AddTo(CompositeDisposable);
            
            View.SetOpenState(openSlot);
        }

        private void OnItemDropped()
        {
            var draggable = _inputService.CurrentDraggableItem;
            if (draggable == null) return;
            
            if (_generatedDraggableGift is null) SetupGiftView();

            var gift = _generatedDraggableGift.Gift.Clone();
            gift.ApplyGiftPart(draggable.GiftPart);

            var craftResult = _craftingRecipes.GetGiftSpriteByRecipe(gift);
            if (craftResult is null) return;

            _generatedDraggableGift.Gift.Copy(gift);
            draggable.ApplyItem();

            _generatedDraggableGift.ChangeVisual(craftResult);
        }

        private void SetupGiftView()
        {
             var giftView = View.CreateViewForGift();
             _generatedDraggableGift = new GiftDraggablePresenter(giftView, _gameplayCanvas);
        }
    }
}