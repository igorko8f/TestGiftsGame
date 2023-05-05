using Codebase.Systems.CommandSystem.Payloads;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class LoadSceneCommand : Command
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        
        public LoadSceneCommand(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
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

            var loadSceneOperation = _sceneLoader.LoadSceneAsync(scene.Info.Name, LoadSceneMode.Additive, null, LoadSceneRelationship.Child);
            loadSceneOperation.completed += ReleaseCommand;
        }

        private void ReleaseCommand(AsyncOperation operation)
        {
            operation.completed -= ReleaseCommand;
            Release();
        }
    }
}