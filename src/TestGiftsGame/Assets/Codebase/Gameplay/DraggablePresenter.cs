using Codebase.Gameplay.ItemContainer;
using Codebase.MVP;
using Codebase.Services;
using UniRx;
using UnityEngine;

namespace Codebase.Gameplay
{
    public class DraggablePresenter : BasePresenter<IDraggable>
    {
        protected readonly IInputService _inputService;
        protected readonly Canvas _canvas;

        public DraggablePresenter(
            IDraggable viewContract,
            IInputService inputService,
            Canvas canvas) 
            : base(viewContract)
        {
            _inputService = inputService;
            _canvas = canvas;
            
            View.OnDragBegin
                .Subscribe(_ => AddDraggableToService())
                .AddTo(CompositeDisposable);

            View.OnDragEnd
                .Subscribe(_ => ResetDraggable())
                .AddTo(CompositeDisposable);
        }
        
        protected virtual void AddDraggableToService()
        {
            _inputService.SetCurrentDraggableItem(this);
        }

        protected virtual void ResetDraggable()
        {
            _inputService.SetCurrentDraggableItem(null);
        }
    }
}