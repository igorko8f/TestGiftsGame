using System;

namespace Codebase.Systems.Binders
{
    public interface IBinder<T> : IDisposable where T : class
    {
        IBinding<T> Bind<TKey>();
        void Unbind<TKey>();
        IBinding<T> GetBinding<TKey>();
    }
}