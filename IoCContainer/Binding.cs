using System;
using System.Collections.Generic;
using System.Threading;

namespace IoCContainer
{
    public class Binding
    {
        private LifeCycle _lifeCycle;
        private Func<object> _resolver;
        private object _singleton;
        private Dictionary<int, object> _threadStatic;

        public Binding(LifeCycle lifeCycle, Func<object> resolver)
        {
            _lifeCycle = lifeCycle;
            _resolver = resolver;
        }

        public object ResolveBinding()
        {
            object _object;
            switch (_lifeCycle)
            {
                case LifeCycle.Transient:
                    _object = _resolver();
                    break;
                case LifeCycle.Singleton:
                    _object = ResolveSingleton();
                    break;
                case LifeCycle.Thread:
                    _object = ResolveThreadStatic();
                    break;
                default:
                    _object = _resolver();
                    break;
            }
            return _object;
        }

        private object ResolveThreadStatic()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (!_threadStatic.ContainsKey(threadId))
                _threadStatic[threadId] = _resolver();
            return _threadStatic[threadId];
        }

        private object ResolveSingleton()
        {
            if (_singleton == null)
                _singleton = _resolver();
            return _singleton;
        }
    }
}
