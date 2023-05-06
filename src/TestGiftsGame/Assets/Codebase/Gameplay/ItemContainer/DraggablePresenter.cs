using System;
using Codebase.Gifts;
using Codebase.MVP;
using Codebase.Services;
using UniRx;
using UnityEngine;

namespace Codebase.Gameplay.ItemContainer
{
    public class DraggablePresenter : BasePresenter<IDraggable>
    {
        public GiftPart GiftPart { get; private set; }
        public IObservable<Unit> OnItemApplied => _onItemApplied;

        private readonly IInputService _inputService;
        private readonly Canvas _canvas;
        private readonly Subject<Unit> _onItemApplied = new();

        public DraggablePresenter(
            IDraggable viewContract,
            IInputService inputService,
            GiftPart giftPart,
            Canvas canvas) 
            : base(viewContract)
        {
            GiftPart = giftPart;
            _inputService = inputService;
            _canvas = canvas;

            View.SetScaleFactor(canvas.scaleFactor);
            View.SetSprite(giftPart.Sprite);

            View.OnDragBegin
                .Subscribe(_ => AddDraggableToService())
                .AddTo(CompositeDisposable);

            View.OnDragEnd
                .Subscribe(_ => ResetDraggable())
                .AddTo(CompositeDisposable);
        }

        private void AddDraggableToService()
        {
            _inputService.SetCurrentDraggableItem(this);
            View.SetParent(_canvas.transform);
        }

        private void ResetDraggable()
        {
            _inputService.SetCurrentDraggableItem(null);
            View.ResetParent();
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