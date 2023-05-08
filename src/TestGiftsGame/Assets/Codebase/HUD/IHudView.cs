using Codebase.MVP;

namespace Codebase.HUD
{
    public interface IHudView : IView
    {
        ChangeableTextView PlayerResources { get; }
        ChangeableTextView CustomersCount { get; }
        ChangeableTextView CurrentTimer { get; }
    }
}