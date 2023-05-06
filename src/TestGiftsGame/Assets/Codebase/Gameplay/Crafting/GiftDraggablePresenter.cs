using Codebase.Gameplay.ItemContainer;
using Codebase.Gifts;
using Codebase.MVP;
using UnityEngine;

namespace Codebase.Gameplay.Crafting
{
    public class GiftDraggablePresenter : BasePresenter<IDraggable>
    {
        public Gift Gift;
        
        public GiftDraggablePresenter(
            IDraggable viewContract,
            Canvas canvas) 
            : base(viewContract)
        {
            Gift = new Gift();
            View.SetScaleFactor(canvas.scaleFactor);
        }

        public void ChangeVisual(Sprite giftVisual)
        {
            View.SetSprite(giftVisual);
        }

        public void DestroyGift()
        {
            View.Dispose();
            Dispose();
        }
    }
}