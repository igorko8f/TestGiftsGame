using Codebase.Services;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Commands;
using Codebase.Systems.CommandSystem.Signals;

namespace Codebase.Installers
{
    public class GameplayInstaller : BaseInstaller
    {
        private readonly ICommandDispatcher _dispatcher;

        public GameplayInstaller(ICommandBinder commandBinder, ICommandDispatcher dispatcher) : base(commandBinder)
        {
            _dispatcher = dispatcher;
        }

        protected override void InstallCommands(ICommandBinder commandBinder)
        {
            if (_dispatcher.HasListener(typeof(SetupGameplaySignal))) return;
            
            commandBinder.Bind<SetupGameplaySignal>()
                .To<SetupGameplayUICommand>();

            commandBinder.Bind<EndLevelSignal>()
                .To<DisposeGameplayCommand>()
                .To<ConstructEndLevelUICommand>();

            commandBinder.Bind<LoadNextLevelSignal>()
                .To<DisposeWinLoseCommand>()
                .To<UnloadSceneCommand>()
                .To<LoadSceneCommand>();
        }

        protected override void InstallServices()
        {
            BindInputService();
            BindLevelProgressService();
            BindLevelUiProvider();
        }

        private void BindLevelProgressService() =>
            Container.BindInterfacesTo<LevelProgressService>()
                .FromNew()
                .AsSingle();

        private void BindInputService() =>
            Container.BindInterfacesTo<InputService>()
                .FromNew()
                .AsSingle();

        private void BindLevelUiProvider() =>
            Container.BindInterfacesTo<LevelUIProvider>()
                .FromNew()
                .AsSingle();
    }
}