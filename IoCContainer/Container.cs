using IoCContainer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace IoCContainer
{
    public class Container
    {
        private readonly Dictionary<Type,Binding> Bindings = new Dictionary<Type, Binding>();

        public void Bind<T1, T2>(LifeCycle lifeCycle = LifeCycle.Transient) where T2:T1
        {
            var type1 = typeof(T1);
            var type2 = typeof(T2);
            if (Bindings.Keys.Contains(type1))
                throw new DuplicateBindingKeyException($"A binding for {type1} already exists. You must remove that binding before trying to rebind or call the Rebind method.");
            if (type2.GetTypeInfo().IsAbstract)
                throw new BindToAbstractException($"Attempted to bind {type2} to {type1}. A binding must be from interface or abstract to concrete.");

            Bindings[type1] = new Binding(lifeCycle, () => ResolveByType(typeof(T2))); 
        }

        public bool RemoveBind<T1>()
        {
            return Bindings.Remove(typeof(T1));
        }

        public void ReBind<T1, T2>(LifeCycle lifeCycle = LifeCycle.Transient) where T2:T1
        {
            RemoveBind<T1>();
            Bind<T1, T2>();
        }

        private object ResolveByType(Type type)
        {
            var constructor = type.GetConstructors().FirstOrDefault();
            if(constructor == null)
            {
                if (type.IsAbstract)
                    throw new ResolveToAbstractException($"Missing binding in dependency chain for {type}.");
                return FormatterServices.GetUninitializedObject(type);
            }
            var args = constructor.GetParameters().Select(x => Resolve(x.ParameterType)).ToArray();
            return constructor.Invoke(args);
        }

        private object Resolve(Type type)
        {
            if (Bindings.TryGetValue(type, out Binding binding))
                return binding.ResolveBinding();
            return ResolveByType(type);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
    }
}
