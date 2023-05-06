﻿using System;
using Codebase.MVP;
using UniRx;
using UnityEngine;

namespace Codebase.Gameplay.ItemContainer
{
    public interface IDraggable : IView
    {
        IObservable<Unit> OnDragBegin { get; }
        IObservable<Unit> OnDragEnd { get; }
        void SetScaleFactor(float scaleFactor);
        void SetSprite(Sprite sprite);
        void SetParent(Transform canvasTransform);
        void ResetParent();
    }
}