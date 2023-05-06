using Codebase.Gameplay;
using Codebase.Systems.CommandSystem;
using Codebase.Systems.CommandSystem.Signals;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class GameplayMonoInstaller : MonoInstaller
    {
        [SerializeField] private GameplayView _gameplayView;
        
        public override void InstallBindings()
        {
            BindGameplayView();
            InstallGameplay();
        }

        private void BindGameplayView()
        {
            Container
                .BindInterfacesTo<GameplayView>()
                .FromInstance(_gameplayView)
                .AsSingle();
        }

        private void Awake()
        {
            Container.Resolve<ICommandDispatcher>().Dispatch<SetupGameplaySignal>();
        }

        private void InstallGameplay() => 
            Container.Install<GameplayInstaller>();
    }
}