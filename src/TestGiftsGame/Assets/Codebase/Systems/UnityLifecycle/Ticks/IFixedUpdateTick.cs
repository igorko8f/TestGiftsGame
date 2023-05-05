namespace Codebase.Systems.UnityLifecycle.Ticks
{
    public interface IFixedUpdateTick : ITickListener
    {
        void FixedUpdateTick();
    }
}