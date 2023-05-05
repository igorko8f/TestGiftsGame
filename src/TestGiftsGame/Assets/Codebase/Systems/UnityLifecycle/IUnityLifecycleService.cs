using System;
using Codebase.Systems.UnityLifecycle.Ticks;

namespace Codebase.Systems.UnityLifecycle
{
    public interface IUnityLifecycleService : IDisposable
    {
        void Subscribe(ITickListener listener);
        void Unsubscribe(ITickListener listener);
    }
}