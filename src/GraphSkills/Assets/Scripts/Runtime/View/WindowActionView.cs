using UnityEngine;
using UnityEngine.UI;

namespace Kefir.View
{
    internal sealed class WindowActionView : ViewBase
    {
        [SerializeField] private Button _study;
        [SerializeField] private Button _forget;

        public void Show(bool isForget)
        {
            _forget.gameObject.SetActive(isForget);
            _study.gameObject.SetActive(!isForget);
            Move();
        }

        private void Move()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            gameObject.SetActive(true);
        }
        
        public void Hide() => gameObject.SetActive(false);

    }
}