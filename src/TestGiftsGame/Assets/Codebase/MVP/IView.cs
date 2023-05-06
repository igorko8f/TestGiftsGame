using System;

namespace Codebase.MVP
{
    public interface IView : IDisposable
    {
        void DisposeView();
    }
}