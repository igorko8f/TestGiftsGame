using Codebase.Level;
using Codebase.Systems.CommandSystem.Payloads;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Codebase.Systems.CommandSystem.Commands
{
    public class LoadSceneCommand : Command
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly FadeScreen _fadeScreen;

        public LoadSceneCommand(ZenjectSceneLoader sceneLoader, FadeScreen fadeScreen)
        {
            _sceneLoader = sceneLoader;
            _fadeScreen = fadeScreen;
        }

        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            _fadeScreen.FadeIn();

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