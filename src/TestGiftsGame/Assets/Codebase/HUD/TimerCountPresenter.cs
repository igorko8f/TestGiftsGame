using Codebase.MVP;
using Codebase.Services;
using UniRx;

namespace Codebase.HUD
{
    public class TimerCountPresenter : BasePresenter<IChangeableTextView>
    {
        public TimerCountPresenter(
            IChangeableTextView viewContract,
            ILevelProgressService levelProgressService) 
            : base(viewContract)
        {
            levelProgressService.CurrentTime
                .Subscribe(View.SetText)
                .AddTo(CompositeDisposable);
        }
    }
}