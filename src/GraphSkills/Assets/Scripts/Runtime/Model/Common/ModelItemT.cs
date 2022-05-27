using System;

namespace Kefir.Сommon.Model.Bindings
{
    /// <summary>
    /// Замена ReactiveProperty из UniRX, чтобы не тянуть всю библиотеку
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelItem<T> : ModelItem, IReadOnlyModelItem<T>
    {
        private Action<T> _onChange;
        protected T _value;

        public virtual T Value
        {
            get => _value;
            set { Set(value); }
        }

        public ModelItem(T value = default)
        {
            _value = value;
        }

        public virtual void Set(T value)
        {
            if (_value == null && value == null)
                return;
            if ((_value == null && value != null) || !_value.Equals(value))
            {
                _value = value;
                InvokeOnChange();
            }
        }

        public void Bind(Action<T> action)
        {
            _onChange += action;
        }

        public void Bind(Action<T> action, bool invokeOnStart)
        {
            Bind(action);
            if (invokeOnStart)
                action.Invoke(_value);
        }

        public void Unbind(Action<T> action)
        {
            _onChange -= action;
        }

        public static implicit operator T(ModelItem<T> d)
        {
            return d._value;
        }

        protected override void InvokeOnChange()
        {
            _onChange?.Invoke(_value);
            base.InvokeOnChange();
        }

        public override string ToString()
        {
            return $"{Value}";

        }
    }
}