using System;
using Codebase.Gameplay.DraggableItems;
using Codebase.Gifts;
using Codebase.MVP;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Codebase.Customers.Orders
{
    [RequireComponent(typeof(Image))]
    public class OrderView : RawView, IOrderView, IDropHandler
    {
        public const float AnimationSpeed = 0.5f;
        
        public IObservable<Unit> OnItemDropped => _onItemDropped;

        [SerializeField] private Image _orderImage;
        [SerializeField] private Image _boxImage;
        [SerializeField] private Image _bowImage;
        [SerializeField] private Image _designImage;
        [SerializeField] private TMP_Text _ordersCountText;

        private Subject<Unit> _onItemDropped = new();

        public void Initialize()
        {
        }

        public void SetOrderView(Sprite giftImage, Gift gift)
        {
            _orderImage.sprite = giftImage;
            
            ChangeGiftPartSprite(gift.Box, _boxImage);
            ChangeGiftPartSprite(gift.Bow, _bowImage);
            ChangeGiftPartSprite(gift.Design, _designImage);
        }

        public void SetOrdersCountText(int ordersCount)
        {
            _ordersCountText.enabled = ordersCount > 1;
            _ordersCountText.text = $"x{ordersCount}";
        }

        public void ShakePanel()
        {
            var sequence = SetupShakeSequence();
            sequence.Play();
        }

        private void ChangeGiftPartSprite(GiftPart giftPart, Image image)
        {
            bool partExist = giftPart is not null;
            image.enabled = partExist;
            image.sprite = partExist ? giftPart.Sprite : null;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out DraggableItem draggable))
            {
                _onItemDropped?.OnNext(Unit.Default);
            }
        }

        private Sequence SetupShakeSequence()
        {
            var sequence = DOTween.Sequence(transform);
            sequence.SetTarget(transform);
            sequence.SetAutoKill();

            sequence.Append(transform.DOShakePosition(AnimationSpeed, 30, 20));
            return sequence;
        }
    }
}