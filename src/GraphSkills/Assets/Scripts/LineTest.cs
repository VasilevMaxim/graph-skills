using System;
using UnityEngine;

[ExecuteInEditMode]
public class LineTest : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _pointStart;
    [SerializeField] private Transform _pointEnd;
    
    private void Update()
    {
        _lineRenderer.SetPosition(0, _pointStart.position);
        _lineRenderer.SetPosition(1, _pointEnd.position);
    }
}
