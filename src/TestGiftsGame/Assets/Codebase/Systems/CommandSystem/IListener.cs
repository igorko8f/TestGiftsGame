using System;
using Codebase.Systems.CommandSystem.Payloads;

namespace Codebase.Systems.CommandSystem
{
    public interface IListener
    {
        void AddListener(Type type, Action<ICommandPayload> action);
        void RemoveListener(Type type);
    }
}