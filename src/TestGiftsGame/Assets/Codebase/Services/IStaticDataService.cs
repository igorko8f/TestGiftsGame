using Codebase.Craft;
using Codebase.Level;

namespace Codebase.Services
{
    public interface IStaticDataService
    {
        BoxCraftingRecipes CraftingRecipes { get; }
        LevelConfiguration GetConfigForLevel(int level);
    }
}