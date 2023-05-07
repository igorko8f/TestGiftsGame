using Codebase.MVP;
using Codebase.Services;
using UniRx;

namespace Codebase.HUD
{
    public class PlayerResourcesPresenter : BasePresenter<IChangeableTextView>
    {
        public PlayerResourcesPresenter(
            IChangeableTextView viewContract,
            IPlayerProgressService playerProgressService) 
            : base(viewContract)
        {
            playerProgressService.ResourcesCount
                .Subscribe(View.SetText)
                .AddTo(CompositeDisposable);
        }
    }
}