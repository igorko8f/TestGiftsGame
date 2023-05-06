using System;

namespace Codebase.MVP
{
    public interface IView : IDisposable
    {
        void Initialize();
        void DisposeView();
    }
}