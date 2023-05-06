using System;
using Codebase.Craft;
using Codebase.Gameplay.ItemContainer;
using Codebase.Gifts;
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
        private IDisposable _giftDestroySubscription;
        
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
            if (_inputService.CurrentGiftPartDraggableItem is not GiftPartDraggablePresenter draggable) return;
            
            if (_generatedDraggableGift is null) SetupGiftView();
            
            if (draggable.GiftPart is Box && _generatedDraggableGift.Gift.Box != null) return;

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
             _generatedDraggableGift = new GiftDraggablePresenter(giftView, _inputService, _gameplayCanvas);
             _giftDestroySubscription = _generatedDraggableGift.OnGiftDestroy
                 .Subscribe(_ => OnGiftDestroyed());
        }

        private void OnGiftDestroyed()
        {
            _giftDestroySubscription?.Dispose();
            _generatedDraggableGift = null;
        }
    }
}