using UnityEngine;

namespace Codebase.MVP
{
    public class RawView : MonoBehaviour
    {
        private bool _disposed = false;
        
        public void OnDestroy()
        {
            if (_disposed) return;
            Dispose();
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            DisposeView();

            _disposed = true;
            Destroy(gameObject);
        }
        
        public virtual void DisposeView()
        {
            
        }
    }
}