using System;
using Codebase.MVP;
using TMPro;
using UnityEngine;

namespace Codebase.HUD
{
    public class ChangeableTextView : RawView, IChangeableTextView
    {
        [SerializeField] private TMP_Text _changeableText;
        public void Initialize()
        {
        }

        public void SetText(string text)
        {
            _changeableText.text = text;
        }
        
        public void SetText(int value)
        {
            _changeableText.text = value.ToString();
        }
        
        public void SetText(float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            _changeableText.text =  timeSpan.ToString("mm':'ss");
        }
    }
}