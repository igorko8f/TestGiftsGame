using System;
using Codebase.Gameplay.ItemContainer;
using Codebase.MVP;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Codebase.Gameplay.Crafting
{
    [RequireComponent(typeof(Image))]
    public class CraftingSlot : RawView, ICraftingSlotView, IDropHandler
    {
        public IObservable<Unit> OnItemDropped => _onItemDropped;

        [SerializeField] private Sprite _openSlotSprite;
        [SerializeField] private Sprite _closedSlotSprite;
        [SerializeField] private DraggableItem _createdGiftView;
        [SerializeField] private Transform _viewGenerationPoint;

        private Subject<Unit> _onItemDropped = new();
        private Image _slotImage;

        public void Initialize()
        {
            _slotImage = GetComponent<Image>();
        }

        public void SetOpenState(bool openState)
        {
            _slotImage.raycastTarget = openState;
            _slotImage.sprite = openState ? _openSlotSprite : _closedSlotSprite;
        }

        public DraggableItem CreateViewForGift()
        {
            return Instantiate(_createdGiftView, _viewGenerationPoint);
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