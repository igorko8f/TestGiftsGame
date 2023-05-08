namespace Codebase.Systems.CommandSystem.Payloads
{
    public class EndLevelStatePayload : ICommandPayload
    {
        public bool LevelComplete;

        public EndLevelStatePayload(bool levelComplete)
        {
            LevelComplete = levelComplete;
        }
    }
}