using System;
using Codebase.Gameplay.ItemContainer;
using Codebase.Gifts;
using Codebase.Services;
using UniRx;
using UnityEngine;

namespace Codebase.Gameplay.Crafting
{
    public class GiftDraggablePresenter : DraggablePresenter
    {
        public Gift Gift;

        public IObservable<Unit> OnGiftDestroy => _onGiftDestroy;
        private readonly Subject<Unit> _onGiftDestroy = new();

        public GiftDraggablePresenter(
            IDraggable viewContract,
            IInputService inputService,
            Canvas canvas) 
            : base(viewContract, inputService, canvas)
        {
            Gift = new Gift();
        }

        public void ChangeVisual(Sprite giftVisual)
        {
            View.SetSprite(giftVisual);
        }
        
        public void DestroyGift()
        {
            _onGiftDestroy?.OnNext(Unit.Default);
            View.Dispose();
            Dispose();
        }

        protected override void AddDraggableToService()
        {
            base.AddDraggableToService();
            View.SetParent(_canvas.transform);
        }
    }
}