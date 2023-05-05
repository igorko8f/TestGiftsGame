using Codebase.Systems.CommandSystem;
using Zenject;

namespace Codebase.Installers
{
    public abstract class BaseInstaller : Installer
    {
        private readonly ICommandBinder _commandBinder;

        protected BaseInstaller(ICommandBinder commandBinder)
        {
            _commandBinder = commandBinder;
        }
        
        public override void InstallBindings()
        {
            InstallServices();
            InstallComponents();
            InstallCommands(_commandBinder);
        }

        protected abstract void InstallComponents();
        protected abstract void InstallCommands(ICommandBinder commandBinder);
        protected abstract void InstallServices();
    }
}