using Codebase.Services;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Commands;
using Codebase.Systems.CommandSystem.Signals;

namespace Codebase.Installers
{
    public class GameplayInstaller : BaseInstaller
    {
        public GameplayInstaller(ICommandBinder commandBinder) : base(commandBinder)
        {
        }

        protected override void InstallCommands(ICommandBinder commandBinder)
        {
            commandBinder.Bind<SetupGameplaySignal>()
                .To<SetupGameplayUICommand>();
        }

        protected override void InstallServices()
        {
            BindInputService();
            BindLevelProgressService();
        }

        private void BindLevelProgressService()
        {
            Container.BindInterfacesTo<LevelProgressService>()
                .FromNew()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container
                .BindInterfacesTo<InputService>()
                .FromNew()
                .AsSingle();
        }
    }
}