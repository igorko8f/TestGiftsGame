using System;
using System.Linq;
using Codebase.Services;
using UnityEditor;

namespace Codebase.Craft.Editor
{
    [CustomEditor(typeof(CraftingSlot))]
    public class CraftingSlotEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var slot = (CraftingSlot) target;
            var resourceProvider = new ProjectResourcesProvider();

            if (string.IsNullOrEmpty(slot.ID))
            {
                GenerateID(slot);
            }
            else
            {
                var allSlots = resourceProvider.LoadResources<CraftingSlot>().ToArray();
                if (allSlots.Any(x => x != slot && x.ID == slot.ID))
                {
                    GenerateID(slot);
                }
            }
        }

        private void GenerateID(CraftingSlot slot)
        {
            slot.ID = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(slot);
        }
    }
}