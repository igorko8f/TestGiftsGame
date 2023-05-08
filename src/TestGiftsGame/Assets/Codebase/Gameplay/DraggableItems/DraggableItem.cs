using System;
using Codebase.MVP;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Codebase.Gameplay.DraggableItems
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Image))]
    public class DraggableItem: RawView, IDraggable, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private const float AnimationSpeed = 0.5f;
        
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

        public void SetSpriteWithAnimation(Sprite sprite)
        {
            var setSpriteAnimation = SetupChangeSpriteAnimation(() => SetSprite(sprite));
            setSpriteAnimation.Play();
        }

        public void RunDestroyAnimation(TweenCallback callback)
        {
            var destroyAnimation = SetupDisposeAnimation(callback);
            destroyAnimation.Play();
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

        private Sequence SetupChangeSpriteAnimation(TweenCallback setSpriteCallBack)
        {
            var sequence = DOTween.Sequence(_transform);
            sequence.SetTarget(_transform);
            sequence.SetAutoKill();

            sequence.Append(_transform.DOScale(Vector3.one * 0.1f, AnimationSpeed).OnComplete(setSpriteCallBack));
            sequence.Insert(0f, _transform.DORotate(Vector3.forward * 180, AnimationSpeed, RotateMode.FastBeyond360));
            sequence.Append(_transform.DOScale(Vector3.one * 1f, AnimationSpeed));
            sequence.Insert(AnimationSpeed, _transform.DORotate(Vector3.zero, 0.5f));

            return sequence;
        }
        
        private Sequence SetupDisposeAnimation(TweenCallback setSpriteCallBack)
        {
            var sequence = DOTween.Sequence(_transform);
            sequence.SetTarget(_transform);
            sequence.SetAutoKill();

            var position = _transform.position;
            var parent = _transform.parent;
            sequence.Append(_transform.DOScale(Vector3.one * 0.1f, AnimationSpeed)
                .OnStart(() => { _transform.position = position; SetParent(parent);})
                .OnComplete(setSpriteCallBack));

            return sequence;
        }
    }
}