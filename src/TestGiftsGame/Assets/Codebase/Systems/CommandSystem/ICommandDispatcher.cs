using System;
using Codebase.Systems.CommandSystem.Payloads;

namespace Codebase.Systems.CommandSystem
{
    public interface ICommandDispatcher : IDisposable
    {
        void Dispatch<TSignal>() where TSignal : ISignal;
        void Dispatch<TSignal>(ICommandPayload payload) where TSignal : ISignal;
        bool HasListener(Type type);
    }
}