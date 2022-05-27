using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Kefir.View
{
    internal sealed class WindowIncomeView : ViewBase, IWindowIncomeView
    {
        [SerializeField] private Transform _arrow;
        [SerializeField] private Transform _pointOn;
        [SerializeField] private Transform _pointOff;
        
        private bool _isView;

        public void Move()
        {
            var endPosition = _isView ? _pointOff.position : _pointOn.position;
            transform.DOMove(endPosition, 0.8f).SetEase(Ease.OutCubic);
            _isView = !_isView;
            _arrow.localEulerAngles = new Vector3(_arrow.localEulerAngles.x, _arrow.localEulerAngles.y, _isView ? 0 : 180);
        }
    }
}