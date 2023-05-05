using System.Collections.Generic;
using System.Linq;
using Codebase.Gifts;
using UnityEngine;

namespace Codebase.Craft
{
    [CreateAssetMenu(fileName = "BoxRecipes", menuName = "StaticData/Craft/BoxRecipes")]
    public class BoxCraftingRecipes : ScriptableObject
    {
        public List<CraftingRecipe> Recipes;

        public Sprite GetGiftSpriteByRecipe(Gift giftConfig)
        {
            if (giftConfig.Box is null) return null;

            var recipesWithEqualBox = GetRecipes(Recipes, giftConfig.Box);
            if (giftConfig.Bow is null)
                return ReturnFirstMatch(recipesWithEqualBox);

            var recipesWithEqualBow = GetRecipes(recipesWithEqualBox, giftConfig.Bow);
            if (giftConfig.Design is null)
                return ReturnFirstMatch(recipesWithEqualBow);
            
            var recipesWithEqualDesign = GetRecipes(recipesWithEqualBow, giftConfig.Design);
            return recipesWithEqualDesign.Any() ? recipesWithEqualDesign.FirstOrDefault()?.Result : null;
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