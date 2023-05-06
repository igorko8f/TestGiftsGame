using System;
using Codebase.Gameplay;

namespace Codebase.Services
{
    public interface IInputService : IDisposable
    {
        DraggablePresenter CurrentGiftPartDraggableItem { get; }
        void SetCurrentDraggableItem(DraggablePresenter current);
    }
}