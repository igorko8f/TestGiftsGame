using System;
using Codebase.MVP;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Codebase.Gameplay.ItemContainer
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Image))]
    public class DraggableItem: RawView, IDraggable, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public IObservable<Unit> OnDragBegin => _onBeginDrag;
        public IObservable<Unit> OnDragEnd => _onEndDrag;

        private readonly Subject<Unit> _onBeginDrag = new ();
        private readonly Subject<Unit> _onEndDrag = new ();

        private CanvasGroup _canvasGroup;
        private Image _image;
        private RectTransform _transform;
        private Transform _parent;
        private Vector3 _cashedPosition;

        public void Initialize()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();
            _transform = GetComponent<RectTransform>();
            _parent = _transform.parent;
            _cashedPosition = _transform.localPosition;
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetParent(Transform parent)
        {
            _transform.parent = parent;
        }

        public void ResetParent()
        {
            SetParent(_parent);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _onBeginDrag?.OnNext(Unit.Default);
        }

        public void OnDrag(PointerEventData eventData)
        {
            ChangePosition(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _onEndDrag?.OnNext(Unit.Default);
            ResetParent();
            ReturnBack();
        }

        private void ReturnBack()
        {
            _transform.localPosition = _cashedPosition;
        }

        private void ChangePosition(Vector2 position)
        {
            _transform.position = position;
        }
    }
}