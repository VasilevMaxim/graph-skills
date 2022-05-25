using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Kefir.View
{
    internal sealed class WindowErrorView : ViewBase
    {
        [SerializeField] private Transform _pointOn;
        [SerializeField] private Transform _pointOff;
        
        [SerializeField] private Text _text;

        public void ShowErrorOpened()
        {
            Move();
            _text.text = "Невозможно открыть умение";
        }
        
        public void ShowErrorForget()
        {
            Move();
            _text.text = "Невозможно забыть умение";
        }

        private void Move()
        {
            var moveSequence = DOTween.Sequence();
            moveSequence.Append(transform.DOMove(_pointOn.position, 1)
                                         .SetEase(Ease.OutCubic));
            moveSequence.PrependInterval(1);
            moveSequence.Append(transform.DOMove(_pointOff.position, 1)
                                         .SetEase(Ease.OutCubic));
        }
    }
}