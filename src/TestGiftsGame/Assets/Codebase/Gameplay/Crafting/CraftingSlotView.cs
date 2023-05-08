using System;
using Codebase.Gameplay.DraggableItems;
using Codebase.MVP;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Codebase.Gameplay.Crafting
{
    [RequireComponent(typeof(Image))]
    public class CraftingSlotView : RawView, ICraftingSlotView, IDropHandler
    {
        public IObservable<Unit> OnItemDropped => _onItemDropped;
        public IObservable<Unit> OnBuySlot { get; private set; }

        [SerializeField] private Sprite _openSlotSprite;
        [SerializeField] private Sprite _closedSlotSprite;
        [SerializeField] private DraggableItem _createdGiftView;
        [SerializeField] private Transform _viewGenerationPoint;
        [SerializeField] private Button _buySlotButton;
        [SerializeField] private TMP_Text _priceText;

        private Subject<Unit> _onItemDropped = new();
        private Image _slotImage;
        
        public void Initialize()
        {
            _slotImage = GetComponent<Image>();
            OnBuySlot = _buySlotButton.OnClickAsObservable();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out DraggableItem draggableItem))
            {
                _onItemDropped?.OnNext(Unit.Default);
            }
        }

        public void SetOpenState(bool openState, int price)
        {
            _slotImage.raycastTarget = openState;
            _slotImage.sprite = openState ? _openSlotSprite : _closedSlotSprite;
            _buySlotButton.gameObject.SetActive(!openState);
            _priceText.text = $"{price} <sprite=0>";
        }

        public DraggableItem CreateViewForGift()
        {
            return Instantiate(_createdGiftView, _viewGenerationPoint);
        }
    }
}