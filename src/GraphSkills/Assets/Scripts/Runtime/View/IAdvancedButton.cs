using System;

namespace Kefir.View
{
    public interface IAdvancedButton
    {
        void Bind(Action action, bool invokeOnStart = false);
        void Unbind();
    }
}