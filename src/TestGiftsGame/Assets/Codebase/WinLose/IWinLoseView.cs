using Codebase.MVP;

namespace Codebase.WinLose
{
    public interface IWinLoseView : IView
    {
        void FadeBackground();
        IEndLevelPanelView GetWinPanel();
        IEndLevelPanelView GetLosePanel();
    }
}