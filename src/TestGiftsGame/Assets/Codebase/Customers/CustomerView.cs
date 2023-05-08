using Codebase.Customers.Orders;
using Codebase.MVP;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Customers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CustomerView : RawView, ICustomerView
    {
        private const float AnimationSpeed = 0.5f;
        private const float AnimationPositionOffset = 300f;
        
        public OrderView OrderView => _orderView;
        [SerializeField] private OrderView _orderView;

        [SerializeField] private Image _customerImage;
        [SerializeField] private Sprite _customerSad;
        [SerializeField] private float _sadMoodValue;

        [SerializeField] private Image _timerSlice;
        [SerializeField] private Image _timerBack;

        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _lateColor;

        private bool _isSad = false;
        private Vector3 _startPosition;
        private CanvasGroup _canvasGroup;
            
        private Sequence _idleAnimation;

        public void Initialize()
        {
            _isSad = false;
            _timerSlice.fillAmount = 1;
            _startPosition = transform.localPosition;
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
        }

        public void SetTimer(float value)
        {
            _timerSlice.fillAmount = value;
            var timerColor = Color.Lerp(_normalColor, _lateColor, 1 - value);
            _timerSlice.color = timerColor;
            _timerBack.color = timerColor;

            if (_timerSlice.fillAmount <= _sadMoodValue && _isSad == false)
            {
                _customerImage.sprite = _customerSad;
                _isSad = true;
            }
        }

        public void ShowCustomerAnimation()
        {
            var showAnimation = SetupMoveAnimation(-AnimationPositionOffset, 1f, RunIdleAnimation);
            showAnimation.Play();
        }

        public void HideCustomerAnimation(TweenCallback callback)
        {
            _idleAnimation.Kill();
            
            var hideAnimation = SetupMoveAnimation(AnimationPositionOffset, 0f, callback);
            hideAnimation.Play();
        }

        private void RunIdleAnimation()
        {
            _idleAnimation = SetupIdleAnimation();
            _idleAnimation.Play();
        }

        private Sequence SetupMoveAnimation(float distance, float fade, TweenCallback callback)
        {
            var sequence = DOTween.Sequence(transform);
            sequence.SetTarget(transform);
            sequence.SetAutoKill();

            var endPosition = _startPosition;
            endPosition.y += distance;
            transform.localPosition = endPosition;

            sequence.Append(transform.DOLocalMoveY(0f, AnimationSpeed));
            sequence.Insert(0f, _canvasGroup.DOFade(fade, AnimationSpeed));
            sequence.AppendCallback(callback);
                
            return sequence;
        }
        
        private Sequence SetupIdleAnimation()
        {
            var sequence = DOTween.Sequence(transform);
            sequence.SetTarget(transform);
            sequence.SetAutoKill();

            transform.localPosition = _startPosition;
            var animationSpeed = AnimationSpeed + (Random.Range(AnimationSpeed, AnimationSpeed * 2));
            
            sequence.Append(transform.DOLocalMoveY(30f, animationSpeed));
            sequence.Append(transform.DOLocalMoveY(0f, animationSpeed));
            sequence.SetLoops(-1);

            return sequence;
        }
    }
}