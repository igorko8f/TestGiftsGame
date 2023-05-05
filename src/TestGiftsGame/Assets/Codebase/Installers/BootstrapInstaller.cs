using Codebase.Systems.CommandSystem;
using Codebase.Systems.EventBroker;
using Codebase.Systems.StaticData;
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
            BindEventBrokerService();
            BindCommandDispatcher();
            BindStaticDataService();
            BindUnityLifecycle();
        }

        private void BindCommandBinder() => 
            Container
                .BindInterfacesTo<CommandBinder>()
                .AsCached()
                .CopyIntoAllSubContainers();

        private void BindEventBrokerService() => 
            Container
                .BindInterfacesTo<EventBrokerService>()
                .FromNew()
                .AsSingle();

        private void BindCommandDispatcher() => 
            Container
                .BindInterfacesTo<CommandDispatcher>()
                .FromNew()
                .AsSingle();

        private void BindStaticDataService() =>
            Container
                .BindInterfacesTo<StaticDataService>()
                .FromNew()
                .AsSingle();

        private void BindUnityLifecycle() =>
            Container
                .Bind<IUnityLifecycleHandler>()
                .To<UnityLifecycleHandler>()
                .FromComponentInNewPrefab(lifecycleHandler)
                .AsSingle();
    }
}