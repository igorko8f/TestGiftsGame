using System;
using Codebase.Gifts;
using Codebase.Services;
using UniRx;
using UnityEngine;

namespace Codebase.Gameplay.DraggableItems
{
    public class GiftPartDraggablePresenter : DraggablePresenter
    {
        public GiftPart GiftPart { get; private set; }
        public IObservable<Unit> OnItemApplied => _onItemApplied;
        private readonly Subject<Unit> _onItemApplied = new();

        public GiftPartDraggablePresenter(
            IDraggable viewContract,
            IInputService inputService,
            GiftPart giftPart,
            Canvas canvas) 
            : base(viewContract, inputService, canvas)
        {
            GiftPart = giftPart;
            View.SetSprite(giftPart.Sprite);
        }

        protected override void AddDraggableToService()
        {
            base.AddDraggableToService();
            View.SetParent(_canvas.transform);
        }

        public void ApplyItem()
        {
            ResetDraggable();

            _onItemApplied?.OnNext(Unit.Default);
            View.Dispose();
            Dispose();
        }
    }
}