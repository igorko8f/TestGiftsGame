using Codebase.Gameplay;

namespace Codebase.Services
{
    public class InputService : IInputService
    {
        public DraggablePresenter CurrentGiftPartDraggableItem => _currentGiftPartDraggableItem;
        private DraggablePresenter _currentGiftPartDraggableItem;

        public void SetCurrentDraggableItem(DraggablePresenter current)
        {
            _currentGiftPartDraggableItem = current;
        }
        
        public void Dispose()
        {
            _currentGiftPartDraggableItem = null;
        }
    }
}