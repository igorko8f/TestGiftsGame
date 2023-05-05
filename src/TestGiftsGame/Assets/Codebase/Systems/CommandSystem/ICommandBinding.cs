using Codebase.Systems.Binders;

namespace Codebase.Systems.CommandSystem
{
    public interface ICommandBinding: IBinding<ICommand>
    {
        ICommandBinding To<TCommand>(params object[] args) where TCommand : ICommand;
    }
}