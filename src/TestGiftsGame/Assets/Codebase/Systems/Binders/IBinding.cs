using System;

namespace Codebase.Systems.Binders
{
    public interface IBinding<T> : IDisposable where T : class
    {
        void To<TKey>() where TKey : T;
    }
}