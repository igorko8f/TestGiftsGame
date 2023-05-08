using System;
using Codebase.Gifts;
using Codebase.MVP;
using UniRx;
using UnityEngine;

namespace Codebase.Customers.Orders
{
    public interface IOrderView : IView
    {
        IObservable<Unit> OnItemDropped { get; }
        void SetOrderView(Sprite giftImage, Gift gift);
        void SetOrdersCountText(int ordersCount);
        void ShakePanel();
    }
}