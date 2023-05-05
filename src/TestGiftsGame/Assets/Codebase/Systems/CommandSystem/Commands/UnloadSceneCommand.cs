using Codebase.Systems.CommandSystem.Payloads;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class UnloadSceneCommand : Command
    {
        public UnloadSceneCommand()
        {
            
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            
            var scene = payload as SceneNamePayload;
            if (scene is null)
            {
                Release();
                return;
            }
            
            var unloadSceneOperation = SceneManager.UnloadSceneAsync(scene.Info.Name);
            unloadSceneOperation.completed += ReleaseCommand;
            
            Release();
        }
        
        private void ReleaseCommand(AsyncOperation operation)
        {
            operation.completed -= ReleaseCommand;
            Release();
        }
    }
}