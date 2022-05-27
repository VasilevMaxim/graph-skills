using UnityEngine;
using UnityEngine.UI;

namespace Kefir.View
{
    public sealed class WindowActionView : ViewBase, IWindowActionView
    {
        [SerializeField] private Button _study;
        [SerializeField] private Button _forget;

        public void Show(bool isForget)
        {
            _forget.gameObject.SetActive(isForget);
            _study.gameObject.SetActive(!isForget);
            gameObject.SetActive(true);
            
            Move();
        }
        
        public void Hide() => gameObject.SetActive(false);
        
        private void Move()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }
}