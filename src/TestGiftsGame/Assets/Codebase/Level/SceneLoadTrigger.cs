using System;
using Codebase.StaticData;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Payloads;
using Codebase.Systems.CommandSystem.Signals;
using UnityEngine;
using Zenject;

namespace Codebase.Level
{
    public class SceneLoadTrigger: MonoBehaviour
    {
        private ICommandDispatcher _commandDispatcher;
        
        [Inject]
        public void Construct(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        private void Start()
        {
            Trigger(SceneNames.GameplayScene);
        }

        private void Trigger(SceneInfo sceneInfo)
        {
            _commandDispatcher.Dispatch<LoadGameplaySignal>(new SceneNamePayload(sceneInfo));
        }
    }
}