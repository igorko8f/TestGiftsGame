using Codebase.Level;
using Codebase.MVP;
using UnityEngine;

namespace Codebase.Gameplay
{
    public class GameplayPresenter : BasePresenter<IGameplayView>
    {
        public GameplayPresenter(
            IGameplayView viewContract) 
            : base(viewContract)
        {
            
        }

        public void ConstructGameplay(LevelConfiguration levelConfiguration)
        {
            Debug.Log("LOADED LEVEL " + levelConfiguration.LevelNumber + " SUCESSFULY!");
        }
    }
}