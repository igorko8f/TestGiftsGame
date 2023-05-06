using System;
using UniRx;

namespace Codebase.MVP
{
    public class BasePresenter<TViewContract> : IDisposable where TViewContract : IView
    {
        protected TViewContract View;
        protected readonly CompositeDisposable CompositeDisposable;

        public BasePresenter(TViewContract viewContract)
        {
            View = viewContract;
            CompositeDisposable = new CompositeDisposable();
        }

        public void AddDisposable(IDisposable disposable)
        {
            CompositeDisposable.Add(disposable);
        }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}