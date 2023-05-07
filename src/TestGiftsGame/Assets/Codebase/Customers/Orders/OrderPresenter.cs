using System;
using Codebase.Craft;
using Codebase.Gameplay.Crafting;
using Codebase.Gifts;
using Codebase.MVP;
using Codebase.Services;
using UniRx;

namespace Codebase.Customers.Orders
{
    public class OrderPresenter : BasePresenter<IOrderView>
    {
        public IObservable<Unit> OnOrderComplete => _onOrderComplete;

        private readonly IInputService _inputService;
        private readonly BoxCraftingRecipes _craftingRecipes;
        private readonly Order _order;

        private readonly Subject<Unit> _onOrderComplete = new();
        private int _currentOrderIndex = 0;

        public OrderPresenter(
            IOrderView viewContract,
            IInputService inputService,
            BoxCraftingRecipes craftingRecipes,
            Order order
            ) : base(viewContract)
        {
            _inputService = inputService;
            _craftingRecipes = craftingRecipes;
            _order = order;
            
            View.OnItemDropped
                .Subscribe(_ => CompareGiftToOrder())
                .AddTo(CompositeDisposable);

            ActualizeOrderView(_order.GiftsInOrder[_currentOrderIndex]);
        }

        private void CompareGiftToOrder()
        {
            var giftDraggable = _inputService.CurrentGiftPartDraggableItem as GiftDraggablePresenter;
            if (giftDraggable is null) return;

            if (!giftDraggable.Gift.Compare(_order.GiftsInOrder[_currentOrderIndex])) return;
            
            giftDraggable.DestroyGift();
            PrepareNextOrder();
        }

        private void PrepareNextOrder()
        {
            _currentOrderIndex += 1;
            if (_currentOrderIndex >= _order.GiftsInOrder.Count)
            {
                _onOrderComplete?.OnNext(Unit.Default);
                DestroyOrder();
                return;
            }

            ActualizeOrderView(_order.GiftsInOrder[_currentOrderIndex]);
        }

        private void ActualizeOrderView(Gift currentOrder)
        {
            View.SetOrderView(_craftingRecipes.GetGiftSpriteByRecipe(currentOrder), currentOrder);
            View.SetOrdersCountText(_order.GiftsInOrder.Count - _currentOrderIndex);
        } 
            

        private void DestroyOrder()
        {
            View.Dispose();
            Dispose();
        }
    }
}