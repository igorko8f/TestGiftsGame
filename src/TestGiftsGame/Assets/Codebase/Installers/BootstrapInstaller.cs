using Codebase.Systems.CommandSystem;
using Codebase.Systems.UnityLifecycle;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private UnityLifecycleHandler lifecycleHandler;
        public override void InstallBindings()
        {
            BindCommandBinder();
            BindCommandDispatcher();
            
            InstallEntryPoint();
            
            BindUnityLifecycleHandler();
            BindUnityLifecycleService();
        }

        private void InstallEntryPoint() => 
            Container.Install<EntryPointInstaller>();

        private void BindCommandBinder() => 
            Container
                .BindInterfacesTo<CommandBinder>()
                .AsCached()
                .CopyIntoAllSubContainers();

        private void BindCommandDispatcher() => 
            Container
                .BindInterfacesTo<CommandDispatcher>()
                .FromNew()
                .AsSingle();

        private void BindUnityLifecycleHandler() =>
            Container
                .BindInterfacesTo<UnityLifecycleHandler>()
                .FromComponentInNewPrefab(lifecycleHandler)
                .AsSingle()
                .NonLazy();

        private void BindUnityLifecycleService() =>
            Container
                .BindInterfacesTo<UnityLifecycleService>()
                .FromNew()
                .AsSingle();
    }
}