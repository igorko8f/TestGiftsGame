using System;
using Codebase.MVP;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.WinLose
{
    public class EndLevelPanelView : RawView, IEndLevelPanelView
    { 
        private const float AnimationSpeed = 0.5f;
        private const float AnimationPositionOffset = 100f;
        public IObservable<Unit> OnNextLevelButtonPressed { get; private set; }

        [SerializeField] private Button _nextLevelButton;

        public void Initialize()
        {
            OnNextLevelButtonPressed = _nextLevelButton.OnClickAsObservable();
        }

        public void AnimateEnter()
        {
            var enterAnimation = SetupEnterAnimation();
            enterAnimation.Play();
        }

        private Sequence SetupEnterAnimation()
        {
            var sequence = DOTween.Sequence(transform);
            sequence.SetTarget(transform);
            sequence.SetAutoKill();

            var endPosition = transform.localPosition;
            endPosition.y += AnimationPositionOffset;
            transform.localPosition = endPosition;

            sequence.Append(transform.DOLocalMoveY(-AnimationPositionOffset, AnimationSpeed));

            return sequence;
        }
    }
}