using Codebase.MVP;
using UnityEngine;

namespace Codebase.Customers
{
    public class CustomerSpawnPoint : RawView
    {
        public bool IsEmpty { get; private set; }
        public Vector3 SpawnPosition => transform.position;

        public void SetEmptyState(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }
    }
}