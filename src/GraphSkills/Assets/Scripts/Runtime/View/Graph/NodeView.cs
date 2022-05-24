using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kefir.View.Graph
{
    [RequireComponent(typeof(Image))]
    internal class ButtonNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        [SerializeField] private DataNodeView _colors;
        
        internal bool Interactable { get; set; }
        
        private Image Image => _image != null ? _image : (_image = GetComponent<Image>());
        private Image _image;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }
    }
    
    internal sealed class DataNodeView : ScriptableObject
    {
        public Color Default => _default;
        public Color Selected => _selected;
        public Color Entered => _entered;
        
        [SerializeField] private Color _default;
        [SerializeField] private Color _selected;
        [SerializeField] private Color _entered;
    }

    [RequireComponent(typeof(Image))]
    internal sealed class NodeView : ViewBase
    {
        [SerializeField] private Image _image;
        [SerializeField] private DataNodeView _dataColor;
        
        private void Start()
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _image.color = _dataColor.Selected;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _image.color = _dataColor.Entered;
        }
    }
}