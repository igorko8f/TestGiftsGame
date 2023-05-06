using Codebase.Services;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Commands;
using Codebase.Systems.CommandSystem.Signals;
using Codebase.Systems.EventBroker;

namespace Codebase.Installers
{
    public class EntryPointInstaller : BaseInstaller
    {
        public EntryPointInstaller(ICommandBinder commandBinder) : base(commandBinder)
        {
        }

        protected override void InstallCommands(ICommandBinder commandBinder)
        {
            commandBinder.Bind<LoadGameplaySignal>()
                .To<LoadSceneCommand>();
        }

        protected override void InstallServices()
        {
            BindEventBrokerService();
            BindResourceProviderService();
            BindStaticDataService();
        }
        
        private void BindEventBrokerService() => 
            Container
                .BindInterfacesTo<EventBrokerService>()
                .FromNew()
                .AsSingle();
        
        private void BindResourceProviderService() =>
            Container
                .BindInterfacesTo<ProjectResourcesProvider>()
                .FromNew()
                .AsSingle();
        
        private void BindStaticDataService() =>
            Container
                .BindInterfacesTo<StaticDataService>()
                .FromNew()
                .AsSingle();
    }
}