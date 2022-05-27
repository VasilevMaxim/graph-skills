using UnityEngine;
using UnityEngine.UI;

namespace Kefir.View
{
    [RequireComponent(typeof(Button))]
    internal sealed class ButtonSalaryView : ViewBase
    {
        public Button Button => _button != null ? _button : (_button = GetComponent<Button>());
        private Button _button;
    }
}