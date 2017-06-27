using Light.GuardClauses;
using LightInject;
using Xunit;

namespace DiContainers.Tests
{
    public sealed class ServiceLocatorAntiPattern
    {
        [Fact]
        public void ResolveExample()
        {
            var container = new ServiceContainer();
            container.Register<Dependency1>(new PerContainerLifetime())
                     .Register<Dependency2>(new PerContainerLifetime())
                     .RegisterInstance(container)
                     .Register<ServiceLocatorClient>()
                     .Register<ConstructorInjectionClient>();

            var serviceLocatorClient = container.GetInstance<ServiceLocatorClient>();
            var constructorInjectionClient = container.GetInstance<ConstructorInjectionClient>();

            serviceLocatorClient.Dependency1.MustBeSameAs(constructorInjectionClient.Dependency1);
            serviceLocatorClient.Dependency2.MustBeSameAs(constructorInjectionClient.Dependency2);
        }
    }

    public sealed class Dependency1 { }

    public sealed class Dependency2 { }

    public sealed class ServiceLocatorClient
    {
        public readonly Dependency1 Dependency1;
        public readonly Dependency2 Dependency2;

        public ServiceLocatorClient(ServiceContainer container)
        {
            Dependency1 = container.GetInstance<Dependency1>();
            Dependency2 = container.GetInstance<Dependency2>();
        }
    }

    public sealed class ConstructorInjectionClient
    {
        public readonly Dependency1 Dependency1;
        public readonly Dependency2 Dependency2;

        public ConstructorInjectionClient(Dependency1 dependency1, Dependency2 dependency2)
        {
            Dependency1 = dependency1.MustNotBeNull();
            Dependency2 = dependency2.MustNotBeNull();
        }
    }
}