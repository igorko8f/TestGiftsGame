using System.Collections.Generic;
using Codebase.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Services
{
    public class ProjectResourcesProvider: IProjectResourcesProvider
    {
        public IEnumerable<TResource> LoadResources<TResource>() where TResource : Object, IResource
        {
            var path = ResourceNames.GetLocation<TResource>();
            return Resources.LoadAll<TResource>(path);
        }
    }
}