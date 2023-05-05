using System.Collections.Generic;
using System.Linq;
using Codebase.Systems.UnityLifecycle.Ticks;

namespace Codebase.Systems.UnityLifecycle
{
    public class UnityLifecycleService : IUnityLifecycleService
    {
        private readonly IUnityLifecycleHandler _lifecycleHandler;
        private readonly List<ITickListener> _listeners = new();

        public UnityLifecycleService(IUnityLifecycleHandler lifecycleHandler)
        {
            _lifecycleHandler = lifecycleHandler;
            
            _lifecycleHandler.UpdateTick += OnUpdateTick;
            _lifecycleHandler.FixedUpdateTick += OnFixedUpdateTick;
            _lifecycleHandler.LateUpdateTick += OnLateUpdateTick;
        }

        public void Subscribe(ITickListener listener)
        {
            if(_listeners.Contains(listener) == false)
                _listeners.Add(listener);
        }

        public void Unsubscribe(ITickListener listener)
        {
            if(_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
        
        private void OnUpdateTick()
        {
            var updateTicks = _listeners.OfType<IUpdateTick>().ToArray();
            foreach (var updateTick in updateTicks)
                updateTick.UpdateTick();
        }
        
        private void OnFixedUpdateTick()
        {
            var updateTicks = _listeners.OfType<IFixedUpdateTick>().ToArray();
            foreach (var updateTick in updateTicks)
                updateTick.FixedUpdateTick();
        }
        
        private void OnLateUpdateTick()
        {
            var updateTicks = _listeners.OfType<ILateUpdateTick>().ToArray();
            foreach (var updateTick in updateTicks)
                updateTick.LateUpdateTick();
        }
        
        public void Dispose()
        {
            _listeners.Clear();
            
            _lifecycleHandler.UpdateTick -= OnUpdateTick;
            _lifecycleHandler.FixedUpdateTick -= OnFixedUpdateTick;
            _lifecycleHandler.LateUpdateTick -= OnLateUpdateTick;
        }
    }
}