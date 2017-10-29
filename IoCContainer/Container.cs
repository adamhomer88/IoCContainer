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

        /// <summary>
        /// Binds an abstract to a concrete implementation.
        /// Throws DuplicateBindingKeyException if an existing abstract is already bound to a concrete implementation.
        /// Throws BindToAbstractException if attempting to bind an abstract to an abstract.
        /// </summary>
        /// <typeparam name="T1">Abstract</typeparam>
        /// <typeparam name="T2">Concrete implementation</typeparam>
        /// <param name="lifeCycle"></param>
        public void Bind<T1, T2>(LifeCycles lifeCycle = LifeCycles.Transient) where T2:T1
        {
            var type1 = typeof(T1);
            var type2 = typeof(T2);
            if (Bindings.Keys.Contains(type1))
                throw new DuplicateBindingException($"A binding for {type1} already exists. You must remove that binding before trying to rebind or call the Rebind method.");
            if (type2.GetTypeInfo().IsAbstract)
                throw new BindToAbstractException($"Attempted to bind {type2} to {type1}. A binding must be from interface or abstract to concrete.");

            Bindings[type1] = new Binding(lifeCycle, () => ResolveByType(typeof(T2))); 
        }

        /// <summary>
        /// Removes the binding with the specified Type.
        /// </summary>
        /// <typeparam name="T1">Type</typeparam>
        /// <returns></returns>
        public bool Unbind<T1>()
        {
            return Bindings.Remove(typeof(T1));
        }

        /// <summary>
        /// Creates a new binding for the specified abstract.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="lifeCycle"></param>
        public void Rebind<T1, T2>(LifeCycles lifeCycle = LifeCycles.Transient) where T2:T1
        {
            Unbind<T1>();
            Bind<T1, T2>();
        }

        private object ResolveByType(Type type)
        {
            if (type.IsAbstract)
                return null;
            return InvokeValidConstructor(type);
        }

        private object InvokeValidConstructor(Type type)
        {
            var orderedConstructors = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            ConstructorInfo validConstructor = null;
            object[] args = null;
            foreach(var constructor in orderedConstructors)
            {
                args = constructor.GetParameters().Select(x => Resolve(x.ParameterType)).ToArray();
                if (args.Any(x => x == null))
                    continue;
                validConstructor = constructor;
                break;
            }
            if (validConstructor == null)
            {
                throw new MissingDependencyException($"Missing valid binding configuration for {type}.");
            }
            return validConstructor.Invoke(args);
        }

        private object Resolve(Type type)
        {
            if (Bindings.TryGetValue(type, out Binding binding))
                return binding.ResolveBinding();
            return ResolveByType(type);
        }

        /// <summary>
        /// Attempts to instantiate the given type using binding configurations.
        /// Throws MissingDependencyException if unable to resolve any constructors of the specified Type.
        /// Throws ResolveToAbstractException if specified type is an abstract.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            if (typeof(T).IsAbstract)
                throw new ResolveToAbstractException("Specified type must be a concrete class.");
            return (T)Resolve(typeof(T));
        }
    }
}
