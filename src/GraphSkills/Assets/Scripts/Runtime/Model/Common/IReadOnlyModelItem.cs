using System;

namespace Kefir.Сommon.Model.Bindings
{
    public interface IReadOnlyModelItem<out T> : IReadOnlyModelItem
    {
        T Value { get; }

        void Bind(Action<T> action);
        void Bind(Action<T> action, bool invokeOnStart);
        void Unbind(Action<T> action);
    }

    public interface IReadOnlyModelItem
    {
        void Bind(Action action);
        void Unbind(Action action);
    }
}