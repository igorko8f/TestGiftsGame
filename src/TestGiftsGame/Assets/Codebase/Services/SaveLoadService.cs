using Codebase.StaticData;
using UniRx;
using UnityEngine;

namespace Codebase.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerProgressSaveKey = "PlayerProgressSaveKey";
        
        public SaveLoadService()
        {
        }

        public PlayerStateSave LoadPlayerState()
        {
            var playerState = DefaultState();
            if (PlayerPrefs.HasKey(PlayerProgressSaveKey))
            {
                playerState = JsonUtility
                    .FromJson<PlayerStateSave>(PlayerPrefs.GetString(PlayerProgressSaveKey));
            }
            else
            {
                PlayerPrefs.SetString(PlayerProgressSaveKey,
                    JsonUtility.ToJson(playerState));
            }

            return playerState;
        }

        public void SavePlayerState(int resourcesCount, int lastLevelIndex)
        {
            var playerState = DefaultState();
            playerState.ResourcesCount = resourcesCount;
            playerState.LastLevelIndex = lastLevelIndex;
            
            PlayerPrefs.SetString(PlayerProgressSaveKey,
                JsonUtility.ToJson(playerState));
        }
        
        private PlayerStateSave DefaultState()
        {
            return new PlayerStateSave
            {
                ResourcesCount = 0,
                LastLevelIndex = 1
            };
        }

        public void Dispose()
        {
        }
    }
}