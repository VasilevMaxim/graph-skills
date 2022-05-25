using UnityEngine;

namespace Kefir.View.Graph
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/DataNodeView", order = 1)]
    internal sealed class DataNodeView : ScriptableObject
    {
        public Color Default => _default;
        public Color Selected => _selected;
        public Color Entered => _entered;
        
        [SerializeField] private Color _default;
        [SerializeField] private Color _selected;
        [SerializeField] private Color _entered;
    }
}