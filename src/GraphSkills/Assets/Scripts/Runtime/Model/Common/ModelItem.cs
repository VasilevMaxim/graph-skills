using System;

namespace Kefir.Сommon.Model.Bindings
{
    public abstract class ModelItem
    {
        private Action _onChange;

        protected virtual void InvokeOnChange()
        {
            _onChange?.Invoke();
        }

        public void Bind(Action action)
        {
            _onChange += action;
        }

        public void Unbind(Action action)
        {
            _onChange -= action;
        }
    }
}