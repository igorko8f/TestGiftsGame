using Codebase.Gameplay;
using Codebase.HUD;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Payloads;
using Codebase.Systems.CommandSystem.Signals;
using Codebase.WinLose;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class GameplayMonoInstaller : MonoInstaller
    {
        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private WinLoseView _winLoseView;
        [SerializeField] private HudView _hudView;
        
        public override void InstallBindings() => 
            InstallGameplay();

        private void Awake() => 
            Container
                .Resolve<ICommandDispatcher>()
                .Dispatch<SetupGameplaySignal>(new SetupGameplayPayload(_gameplayView, _winLoseView, _hudView, Container));

        private void InstallGameplay() => 
            Container.Install<GameplayInstaller>();
    }
}