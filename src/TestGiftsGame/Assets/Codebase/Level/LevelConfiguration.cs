using Codebase.Gifts;
using Codebase.Services;
using UnityEngine;

namespace Codebase.Level
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "StaticData/Level/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObject, IResource
    {
        public int LevelNumber = 1;
        public int CustomersCount = 10;
        public float OrderPreparationTime = 1f;
        public Box[] AvailableBoxes;
        public Bow[] AvailableBows;
        public Design[] AvailableDesigns;
    }
}