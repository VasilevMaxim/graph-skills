using UnityEngine;

namespace Kefir.View
{
    internal sealed class WindowActionView : ViewBase
    {
        public void Show()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            gameObject.SetActive(true);
        }
        
        public void Hide() => gameObject.SetActive(false);

    }
}