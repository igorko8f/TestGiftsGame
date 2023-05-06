using Codebase.MVP;
using Codebase.Services;
using UniRx;

namespace Codebase.Gameplay.Crafting
{
    public class TrashBinPresenter : BasePresenter<ITrashBin>
    {
        private readonly IInputService _inputService;

        public TrashBinPresenter(
            ITrashBin viewContract,
            IInputService inputService) 
            : base(viewContract)
        {
            _inputService = inputService;
            
            View.OnItemDropped
                .Subscribe(_ => OnItemDropped())
                .AddTo(CompositeDisposable);
        }

        private void OnItemDropped()
        {
            if (_inputService.CurrentGiftPartDraggableItem is not GiftDraggablePresenter draggable) return;
            draggable.DestroyGift();
        }
    }
}