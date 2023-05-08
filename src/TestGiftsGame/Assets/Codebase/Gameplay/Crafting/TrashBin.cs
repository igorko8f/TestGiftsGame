using System;
using Codebase.Gameplay.DraggableItems;
using Codebase.MVP;
using UniRx;
using UnityEngine.EventSystems;

namespace Codebase.Gameplay.Crafting
{
    public class TrashBin : RawView, IDropHandler, ITrashBin
    {
        public IObservable<Unit> OnItemDropped => _onItemDropped;
        private Subject<Unit> _onItemDropped = new();
        
        public void Initialize()
        {
            
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out DraggableItem draggableItem))
            {
                _onItemDropped?.OnNext(Unit.Default);
            }
        }
    }
}