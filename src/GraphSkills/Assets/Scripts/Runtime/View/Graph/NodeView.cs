using System;
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

    [RequireComponent(typeof(Image))]
    internal sealed class NodeView : ViewBase, IPointerClickHandler, IPointerEnterHandler
    {
        public event Action Clicked;
        
        [SerializeField] private Image _image;
        [SerializeField] private DataNodeView _dataColor;
        
        private void Start()
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void Open()
        {
            _image.color = _dataColor.Entered;
        }
        
        public void Forget()
        {
            _image.color = _dataColor.Default;
        }
    }
}