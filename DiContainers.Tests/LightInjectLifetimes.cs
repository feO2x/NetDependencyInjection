using System;
using System.Threading;
using Light.GuardClauses;
using LightInject;
using Xunit;

namespace DiContainers.Tests
{
    public sealed class LightInjectLifetimes
    {
        [Fact]
        public void SingletonLifetime()
        {
            var container = new ServiceContainer();
            container.Register<A>()
                     .Register<B>()
                     .Register<C>(new PerContainerLifetime());

            var instanceOfA = container.GetInstance<A>();

            instanceOfA.InstanceOfC.MustBeSameAs(instanceOfA.InstanceOfB.InstanceOfC);
        }

        [Fact]
        public void PerScopeLifetime()
        {
            var container = new ServiceContainer();
            container.Register<A>()
                     .Register<B>()
                     .Register<C>(new PerScopeLifetime());

            using (container.BeginScope())
            {
                var instanceOfA = container.GetInstance<A>();

                instanceOfA.InstanceOfC.MustBeSameAs(instanceOfA.InstanceOfB.InstanceOfC);
            }
        }

        [Fact]
        public void PerRequestLifetime()
        {
            var container = new ServiceContainer();
            container.Register<A>()
                     .Register<B>()
                     .Register<C>(new PerRequestLifeTime());

            using (container.BeginScope())
            {
                var instanceOfA = container.GetInstance<A>();

                instanceOfA.InstanceOfC.MustNotBeSameAs(instanceOfA.InstanceOfB.InstanceOfC);
            }
        }

        [Fact]
        public void CustomPerThreadLifetime()
        {
            var container = new ServiceContainer();
            container.Register<D>(new PerThreadLifetime());
            void ResolveD() => container.GetInstance<D>();
            var thread1 = new Thread(ResolveD);
            var thread2 = new Thread(ResolveD);

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            D.NumberOfInstances.MustBe(2);
        }
    }

    public class A
    {
        public readonly B InstanceOfB;
        public readonly C InstanceOfC;

        public A(B instanceOfB, C instanceOfC)
        {
            InstanceOfB = instanceOfB.MustNotBeNull();
            InstanceOfC = instanceOfC.MustNotBeNull();
        }
    }

    public class B
    {
        public readonly C InstanceOfC;

        public B(C instanceOfC)
        {
            InstanceOfC = instanceOfC.MustNotBeNull();
        }
    }

    public class C { }

    public class D
    {
        private static int _numberOfInstances;

        public static int NumberOfInstances => Volatile.Read(ref _numberOfInstances);

        public D()
        {
            Interlocked.Increment(ref _numberOfInstances);
        }
    }

    public sealed class PerThreadLifetime : ILifetime
    {
        private readonly ThreadLocal<object> _threadLocal = new ThreadLocal<object>();

        public object GetInstance(Func<object> createInstance, Scope scope)
        {
            if (_threadLocal.IsValueCreated == false)
                _threadLocal.Value = createInstance();

            return _threadLocal.Value;
        }
    }
}