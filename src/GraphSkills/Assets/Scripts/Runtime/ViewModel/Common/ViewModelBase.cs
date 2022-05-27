using System;
using System.Collections.Generic;
using Kefir.Сommon.Model.Bindings;

namespace Kefir.ViewModel
{
    public abstract class ViewModelBase : IDisposable
    {
        private List<Action> _unindList = new ();
        
        protected void Bind<T>(IReadOnlyModelItem<T> target, Action<T> receiver, bool invokeOnStart = false)
        {
            Action action = () =>  receiver.Invoke(target.Value);
            
            target.Bind(action);
            
            if (invokeOnStart)
                receiver.Invoke(target.Value);
            
            _unindList.Add(() => target.Unbind(action));
        }
        
        public void Dispose()
        {
            Unbind();
        }
        
        protected void Unbind()
        {
            _unindList.ForEach(o => o.Invoke());
        }
    }
}