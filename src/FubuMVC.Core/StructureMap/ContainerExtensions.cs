using System;
using System.Linq;
using FubuCore;
using FubuMVC.Core.Runtime;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace FubuMVC.Core.StructureMap
{
    public static class ContainerExtensions
    {
        public static void Activate<T>(this Registry registry, string description, Action<T> activation)
        {
            registry.For<IActivator>().Add<LambdaActivator<T>>()
                .Ctor<string>().Is(description)
                .Ctor<Action<T>>().Is(activation);
        }

        public static void Activate(this Registry registry, string description, Action activation)
        {
            registry.For<IActivator>().Add<LambdaActivator>()
                .Ctor<string>().Is(description)
                .Ctor<Action>().Is(activation);
        }

        public static void Activate(this IInitializationExpression expression, string description, Action activation)
        {
            expression.For<IActivator>().Add<LambdaActivator>()
                .Ctor<string>().Is(description)
                .Ctor<Action>().Is(activation);
        }

        public static Instance FindDependencyDefinitionFor<T>(this IConfiguredInstance instance)
        {
            var dependency = instance.Dependencies.FirstOrDefault(x => x.Type == typeof (T));
            return dependency == null ? null : dependency.Dependency as Instance;
        }

        public static T FindDependencyValueFor<T>(this IConfiguredInstance instance) where T : class
        {
            var dependency = instance.Dependencies.FirstOrDefault(x => x.Type == typeof(T));
            return dependency == null ? null : dependency.Dependency.As<ObjectInstance>().Object as T;
        }
    }
}