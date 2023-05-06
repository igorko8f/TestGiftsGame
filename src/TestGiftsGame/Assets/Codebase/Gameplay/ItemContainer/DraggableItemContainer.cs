using Codebase.MVP;
using UnityEngine;

namespace Codebase.Gameplay.ItemContainer
{
    public class DraggableItemContainer : RawView, IDraggableItemContainerView
    {
        [SerializeField] private DraggableItem _draggableItem;
        [SerializeField] private Transform _generationPoint;
        
        public void Initialize()
        {
            
        }
        
        public DraggableItem CreateViewForGift()
        {
            return Instantiate(_draggableItem, _generationPoint);
        }
    }
}