using Codebase.MVP;

namespace Codebase.HUD
{
    public interface IChangeableTextView : IView
    {
        void SetText(string text);
        void SetText(int value);
        void SetText(float time);
    }
}