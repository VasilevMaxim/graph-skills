using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kefir.View
{
    [RequireComponent(typeof(Button))]
    public class AdvancedButton : MonoBehaviour, IAdvancedButton
    {
        public Button Button => _button != null ? _button : (_button = GetComponent<Button>());
        private Button _button;

        public void Bind(Action action, bool invokeOnStart = false)
        {
            Button.onClick.AddListener(() => action?.Invoke());

            if (invokeOnStart)
                action?.Invoke();
        }

        public void Unbind()
        {
            Button.onClick.RemoveAllListeners();
        }
        
        public void OnDisable()
        {
            Unbind();
        }
    }
}