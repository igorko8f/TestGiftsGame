using System.Linq;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IStaticDataService _staticDataService;
        private const string PlayerProgressSaveKey = "PlayerProgressSaveKey";

        public SaveLoadService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
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

        public void SavePlayerState(int resourcesCount, int lastLevelIndex, string[] boughtCraftingSlots)
        {
            var playerState = DefaultState();
            playerState.ResourcesCount = resourcesCount;
            playerState.LastLevelIndex = lastLevelIndex;
            playerState.BoughtCraftingSlots = boughtCraftingSlots;
            
            PlayerPrefs.SetString(PlayerProgressSaveKey,
                JsonUtility.ToJson(playerState));
        }
        
        private PlayerStateSave DefaultState() =>
            new PlayerStateSave
            {
                ResourcesCount = 0,
                LastLevelIndex = 1,
                BoughtCraftingSlots = GetBoughtCraftingSlots()
            };

        private string[] GetBoughtCraftingSlots() =>
            _staticDataService.CraftingSlots
                .Where(x => x.OpenFromStart)
                .Select(x => x.ID)
                .ToArray();

        public void Dispose()
        {
        }
    }
}