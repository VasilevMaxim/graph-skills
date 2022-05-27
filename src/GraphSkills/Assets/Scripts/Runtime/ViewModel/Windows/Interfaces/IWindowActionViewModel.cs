using System;

namespace Kefir.ViewModel
{
    internal interface IWindowActionViewModel
    {
        void ResetChoose(params Action[] actions);
    }
}