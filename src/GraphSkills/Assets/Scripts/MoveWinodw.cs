using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveWinodw : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _pointOn;
    [SerializeField] private Transform _pointOff;

    private bool _isView;
    
    public void Move()
    {
        
        var endPosition = _isView ? _pointOff.position : _pointOn.position;
        _transform.DOMove(endPosition, 1.5f).SetEase(Ease.OutCubic);

        _isView = !_isView;
        _arrow.localEulerAngles = new Vector3(_arrow.localEulerAngles.x, _arrow.localEulerAngles.y, _isView ? 0 : 180);
    }
}
