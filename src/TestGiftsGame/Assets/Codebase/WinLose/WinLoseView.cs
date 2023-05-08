using Codebase.MVP;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.WinLose
{
    public class WinLoseView : RawView, IWinLoseView
    {
        [SerializeField] private Image _fader;
        [SerializeField] private EndLevelPanelView _winPanel;
        [SerializeField] private EndLevelPanelView _losePanel;
        [SerializeField] private Transform _panelSpawnPoint;
        
        public void Initialize()
        {
        }
        
        public void FadeBackground()
        {
            _fader.gameObject.SetActive(true);
            _fader.DOFade(0.4f, 1f);
        }

        public IEndLevelPanelView GetWinPanel()
        {
            return Instantiate(_winPanel, _panelSpawnPoint);
        }

        public IEndLevelPanelView GetLosePanel()
        {
            return Instantiate(_losePanel, _panelSpawnPoint);
        }
    }
}