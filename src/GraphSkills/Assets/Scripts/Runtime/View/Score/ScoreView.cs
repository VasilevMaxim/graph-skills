using UnityEngine;
using UnityEngine.UI;

namespace Runtime.View
{
    public class ScoreView : ViewBase, IScoreView
    {
        [SerializeField] private Text _text;

        public void UpdateText(int value)
        {
            _text.text = value.ToString();
        }
    }
}