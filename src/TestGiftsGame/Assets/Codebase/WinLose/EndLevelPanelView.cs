using System;
using Codebase.MVP;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.WinLose
{
    public class EndLevelPanelView : RawView, IEndLevelPanelView
    { 
        public IObservable<Unit> OnNextLevelButtonPressed { get; private set; }

        [SerializeField] private Button _nextLevelButton;

        public void Initialize()
        {
            OnNextLevelButtonPressed = _nextLevelButton.OnClickAsObservable();
        }
    }
}