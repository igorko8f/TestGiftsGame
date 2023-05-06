using Codebase.Gameplay.ItemContainer;

namespace Codebase.Services
{
    public class InputService : IInputService
    {
        public DraggablePresenter CurrentDraggableItem => _currentDraggableItem;
        private DraggablePresenter _currentDraggableItem;

        public void SetCurrentDraggableItem(DraggablePresenter current)
        {
            _currentDraggableItem = current;
        }
        
        public void Dispose()
        {
            _currentDraggableItem = null;
        }
    }
}