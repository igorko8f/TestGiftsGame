using System;

namespace Codebase.Services
{
    public class StaticDataService : IStaticDataService, IDisposable
    {
        private readonly IProjectResourcesProvider _resourcesProvider;
        
        private bool _initialized = false;

        public StaticDataService(IProjectResourcesProvider resourcesProvider)
        {
            _resourcesProvider = resourcesProvider;
            _initialized = CollectData();
        }
        
        private bool CollectData()
        {
            return false;
        }
        
        public void Dispose()
        {
           
        }
    }
}