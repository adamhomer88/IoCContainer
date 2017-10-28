using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace IoCContainer
{
    public class Container
    {
        private readonly Dictionary<Type,Func<object>> Bindings = new Dictionary<Type, Func<object>>();

        public void Bind<T1, T2>(LifeCycle lifeCycle = LifeCycle.Transient)
        {
            if(typeof(T2).GetTypeInfo().IsAbstract)
            {
                //throw exception
            }

            Bindings[typeof(T1)] = () => ResolveByType(typeof(T2)); 
        }

        private object ResolveByType(Type type)
        {
            var constructor = type.GetConstructors().FirstOrDefault();
            if(constructor == null)
            {
                System.Diagnostics.Debug.WriteLine($"resolving object of type {type}.");
                return FormatterServices.GetUninitializedObject(type);
            }
            var args = constructor.GetParameters().Select(x => Resolve(x.ParameterType)).ToArray();
            return constructor.Invoke(args);
        }

        private object Resolve(Type type)
        {
            if (Bindings.TryGetValue(type, out Func<object> binding))
                return binding();
            return ResolveByType(type);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
    }
}
