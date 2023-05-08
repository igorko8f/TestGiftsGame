using Codebase.Craft;
using Codebase.Level;
using Codebase.StaticData;

namespace Codebase.Services
{
    public interface IStaticDataService
    {
        BoxCraftingRecipes CraftingRecipes { get; }
        PriceList PriceList { get; }
        CraftingSlot[] CraftingSlots { get; }
        int TotalLevelsCount { get; }
        LevelConfiguration GetConfigForLevel(int level);
    }
}