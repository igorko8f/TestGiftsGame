using System;
using System.Collections.Generic;

namespace Codebase.Systems.EventBroker
{
    public interface ISubscribersCollection<TSubscriber> : IDisposable where TSubscriber : class
    {
        bool IsExecuting { get; set; }
        void Add(TSubscriber subscriber);
        void Remove(TSubscriber subscriber);
        void CleanCollection();
        IEnumerable<TSubscriber> GetCollection();
    }
}