namespace Codebase.Systems.UnityLifecycle.Ticks
{
    public interface ILateUpdateTick : ITickListener
    {
        void LateUpdateTick();
    }
}