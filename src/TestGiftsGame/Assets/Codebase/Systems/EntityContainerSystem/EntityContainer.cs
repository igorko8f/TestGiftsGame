using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Systems.EntityContainerSystem
{
    public class EntityContainer : MonoBehaviour, IDisposable
    {
        private Dictionary<Type, object> _container;

        private void Awake()
        {
            _container = new Dictionary<Type, object>();
            BindComponents();
        }

        public TComponent Resolve<TComponent>() where TComponent : MonoBehaviour => 
            KeyExist(typeof(TComponent)) ? (TComponent)_container[typeof(TComponent)] : null;

        protected virtual void BindComponents()
        {
            //override in children to bind components throw BinComponent method
        }

        protected void BindComponent<TComponent>(TComponent component) where TComponent : MonoBehaviour
        {
            var key = component.GetType();
            if (KeyExist(key) == false) 
                _container.Add(key, component);
        }

        private bool KeyExist(Type key) => 
            _container.ContainsKey(key);

        public void Dispose() => 
            _container.Clear();
    }
}
