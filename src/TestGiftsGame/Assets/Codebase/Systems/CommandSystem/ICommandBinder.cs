using Codebase.Systems.Binders;

namespace Codebase.Systems.CommandSystem
{
    public interface ICommandBinder: IBinder<ICommand>
    {
        ICommandBinding Bind<TSignal>() where TSignal : ISignal;
    }
}