using Codebase.MVP;
using UnityEngine;

namespace Codebase.HUD
{
    public class HudView : RawView, IHudView
    {
        public ChangeableTextView PlayerResources => _playerResources;
        public ChangeableTextView CustomersCount => _customersCount;
        public ChangeableTextView CurrentTimer => _currentTimer;
        
        [Header("HUD")] 
        [SerializeField] private ChangeableTextView _playerResources;
        [SerializeField] private ChangeableTextView _customersCount;
        [SerializeField] private ChangeableTextView _currentTimer;
        
        public void Initialize()
        {
        }
    }
}