using Codebase.MVP;
using Codebase.Services;
using UniRx;

namespace Codebase.HUD
{
    public class CustomersCountPresenter : BasePresenter<IChangeableTextView>
    {
        public CustomersCountPresenter(
            IChangeableTextView viewContract,
            ILevelProgressService levelProgressService) 
            : base(viewContract)
        {
            levelProgressService.CustomersCount
                .Subscribe(View.SetText)
                .AddTo(CompositeDisposable);
        }
    }
}