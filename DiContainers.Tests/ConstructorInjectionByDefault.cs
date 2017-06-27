using Light.GuardClauses;
using LightInject;
using Xunit;

namespace DiContainers.Tests
{
    public sealed class ConstructorInjectionByDefault
    {
        [Fact]
        public void ResolvingExamples()
        {
            var container = new ServiceContainer();

            container.Register<Dependency>()
                     .Register<ConstructorInjection>()
                     .Register<PropertyInjection>()
                     .Register(factory =>
                               {
                                   var instance = new InitializeMethod();
                                   instance.Initialize(factory.GetInstance<Dependency>());
                                   return instance;
                               });

            var instance1 = container.GetInstance<ConstructorInjection>();
            var instance2 = container.GetInstance<PropertyInjection>();
            var instance3 = container.GetInstance<InitializeMethod>();

            instance1.Dependency.MustNotBeNull();
            instance2.Dependency.MustNotBeNull();
            instance3.Dependency.MustNotBeNull();
        }
    }

    public sealed class Dependency { }

    public sealed class ConstructorInjection
    {
        public readonly Dependency Dependency;

        public ConstructorInjection(Dependency dependency)
        {
            Dependency = dependency.MustNotBeNull();
        }
    }

    public sealed class PropertyInjection
    {
        [Inject]
        public Dependency Dependency { get; set; }
    }

    public sealed class InitializeMethod
    {
        private Dependency _dependency;

        public void Initialize(Dependency dependency)
        {
            _dependency = dependency.MustNotBeNull();
        }

        public Dependency Dependency => _dependency;
    }
}