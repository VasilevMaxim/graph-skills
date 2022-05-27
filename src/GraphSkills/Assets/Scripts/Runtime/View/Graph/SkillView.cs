using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kefir.View.Graph
{
    [RequireComponent(typeof(Image), typeof(Outline))]
    public sealed class SkillView : ViewBase, ISkillView, IPointerClickHandler
    {
        public event Action Clicked;
        private Outline Outline => _outline != null ? _outline : (_outline = GetComponent<Outline>());
        private Outline _outline;
        
        [SerializeField] private Image _image;
        [SerializeField] private DataNodeView _dataColor;
        [SerializeField] private Text _cost;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void Open()
        {
            _image.color = _dataColor.Entered;
        }
        
        public void Forget()
        {
            _image.color = _dataColor.Default;
        }

        public void Choose()
        {
            Outline.enabled = true;
            _cost.gameObject.SetActive(true);
        }

        public void CancelChoose()
        {
            Outline.enabled = false;
            _cost.gameObject.SetActive(false);
        }
        
        public void SetCost(int value) => _cost.text = value.ToString();
    }
}