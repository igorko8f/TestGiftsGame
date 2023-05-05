using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Services
{
    public interface IProjectResourcesProvider
    {
        public IEnumerable<TResource> LoadResources<TResource>() where TResource : Object, IResource;
    }
}