using System;
using Codebase.Systems.CommandSystem.Payloads;

namespace Codebase.Systems.CommandSystem
{
    public interface ICommand : IDisposable
    {
        event Action OnExecuted;
        bool IsRetained { get; }
        void Invoke();
        void Invoke(ICommandPayload payload);
    }
}