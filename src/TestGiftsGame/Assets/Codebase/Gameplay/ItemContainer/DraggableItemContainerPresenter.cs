using System;
using Codebase.Gifts;
using Codebase.MVP;
using Codebase.Services;
using UniRx;
using UnityEngine;

namespace Codebase.Gameplay.ItemContainer
{
    public class DraggableItemContainerPresenter : BasePresenter<IDraggableItemContainerView>
    {
        private readonly GiftPart _generatedPart;
        private readonly Canvas _gameplayCanvas;
        private readonly IInputService _inputService;
        
        private DraggablePresenter _currentDraggable;
        private IDisposable _itemApplySubscription;
        
        public DraggableItemContainerPresenter(
            IDraggableItemContainerView viewContract,
            IInputService inputService,
            Canvas canvas,
            GiftPart giftPart) 
            : base(viewContract)
        {
            _generatedPart = giftPart;
            _inputService = inputService;
            _gameplayCanvas = canvas;

            SetupGiftPartView();
        }
        
        private void SetupGiftPartView()
        {
            var giftView = View.CreateViewForGift();
            _currentDraggable = new DraggablePresenter(giftView, _inputService, _generatedPart, _gameplayCanvas);
            _itemApplySubscription = _currentDraggable.OnItemApplied
                .Subscribe(_ => OnItemApplied());
        }

        private void OnItemApplied()
        {
            _itemApplySubscription?.Dispose();
            SetupGiftPartView();
        }
    }
}