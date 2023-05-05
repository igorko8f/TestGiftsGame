using Codebase.StaticData;

namespace Codebase.Systems.CommandSystem.Payloads
{
    public class SceneNamePayload : ICommandPayload
    {
        public SceneName Info;

        public SceneNamePayload(SceneName info)
        {
            Info = info;
        }
    }
}