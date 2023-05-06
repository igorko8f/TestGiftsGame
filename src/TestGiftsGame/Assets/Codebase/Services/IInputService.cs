using System;
using Codebase.Gameplay.ItemContainer;

namespace Codebase.Services
{
    public interface IInputService : IDisposable
    {
        DraggablePresenter CurrentDraggableItem { get; }
        void SetCurrentDraggableItem(DraggablePresenter current);
    }
}