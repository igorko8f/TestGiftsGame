using Codebase.Services;
using Codebase.Systems.CustomAttributes;
using UnityEngine;

namespace Codebase.Craft
{
    [CreateAssetMenu(fileName = "CraftingSlot", menuName = "StaticData/Craft/CraftingSlot")]
    public class CraftingSlot : ScriptableObject, IResource
    {
        [ReadOnly] public string ID;
        public int Price;
        public bool OpenFromStart;
    }
}