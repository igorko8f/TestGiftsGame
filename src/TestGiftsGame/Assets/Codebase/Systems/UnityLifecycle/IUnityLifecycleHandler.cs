using System;

namespace Codebase.Systems.UnityLifecycle
{
    public interface IUnityLifecycleHandler
    {
        event Action UpdateTick;
        event Action FixedUpdateTick;
        event Action LateUpdateTick;
    }
}