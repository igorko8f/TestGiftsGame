using System;
using System.Linq;
using Codebase.Craft;
using Codebase.Gameplay.DraggableItems;
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
        private readonly IPlayerProgressService _playerProgressService;
        private readonly CraftingSlot _slot;
        private readonly Canvas _gameplayCanvas;

        private GiftDraggablePresenter _generatedDraggableGift;
        private IDisposable _giftDestroySubscription;
        
        public CraftingSlotPresenter(
            ICraftingSlotView viewContract,
            BoxCraftingRecipes craftingRecipes,
            IInputService inputService,
            IPlayerProgressService playerProgressService,
            CraftingSlot slot,
            Canvas canvas) 
            : base(viewContract)
        {
            _craftingRecipes = craftingRecipes;
            _inputService = inputService;
            _playerProgressService = playerProgressService;
            _slot = slot;
            _gameplayCanvas = canvas;
            
            View.OnItemDropped
                .Subscribe(_ => OnItemDropped())
                .AddTo(CompositeDisposable);

            View.OnBuySlot
                .Subscribe(_ => TryBuySlot())
                .AddTo(CompositeDisposable);
            
            View.SetOpenState(_playerProgressService.BoughtCraftingSlots.Contains(slot.ID), slot.Price);
        }

        private void OnItemDropped()
        {
            if (_inputService.CurrentGiftPartDraggableItem is not GiftPartDraggablePresenter draggable) return;
            var isBox = draggable.GiftPart is Box;
            
            if (_generatedDraggableGift is null && !isBox) return; 
                
            if (_generatedDraggableGift is null) SetupGiftView();
            
            if (isBox && _generatedDraggableGift.Gift.Box != null) return;

            var gift = _generatedDraggableGift.Gift.Clone();
            gift.ApplyGiftPart(draggable.GiftPart);

            var craftResult = _craftingRecipes.GetGiftSpriteByRecipe(gift);
            if (craftResult is null) return;

            _generatedDraggableGift.Gift.Copy(gift);
            draggable.ApplyItem();
            
            _generatedDraggableGift.ChangeVisual(craftResult, !isBox);
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

        private void TryBuySlot()
        {
            if (_playerProgressService.SpendResources(_slot.Price) == false) return;
            _playerProgressService.BuyCraftingSlot(_slot.ID);
            View.SetOpenState(true, _slot.Price);
        }
    }
}