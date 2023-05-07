using System.Collections.Generic;
using System.Linq;
using Codebase.Gifts;
using Codebase.Services;
using UnityEngine;

namespace Codebase.Craft
{
    [CreateAssetMenu(fileName = "BoxRecipes", menuName = "StaticData/Craft/BoxRecipes")]
    public class BoxCraftingRecipes : ScriptableObject, IResource
    {
        public List<CraftingRecipe> Recipes;

        public Sprite GetGiftSpriteByRecipe(Gift giftConfig)
        {
            if (giftConfig.Box is null) return null;

            var recipesFound = GetRecipes(Recipes, giftConfig.Box);
            if (giftConfig.Bow is not null)
                recipesFound = GetRecipes(recipesFound, giftConfig.Bow);
            if (giftConfig.Design is not null)
                recipesFound = GetRecipes(recipesFound, giftConfig.Design);

            return ReturnFirstMatch(recipesFound);
        }

        private List<CraftingRecipe> GetRecipes(List<CraftingRecipe> from, GiftPart part)
        {
            return from
                .Where(x => x.GiftParts.Contains(part))
                .ToList();
        }

        private Sprite ReturnFirstMatch(List<CraftingRecipe> from)
        {
            return from.Any() ? from.FirstOrDefault()?.Result : null;
        }
    }
}