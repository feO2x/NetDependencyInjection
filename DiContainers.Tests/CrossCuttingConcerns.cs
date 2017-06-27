using Light.GuardClauses;
using LightInject;
using LightInject.Interception;
using Xunit;
using Xunit.Abstractions;

namespace DiContainers.Tests
{
    public sealed class CrossCuttingConcerns
    {
        private readonly ITestOutputHelper _output;

        public CrossCuttingConcerns(ITestOutputHelper output)
        {
            _output = output.MustNotBeNull();
        }

        [Fact]
        public void Interception()
        {
            var container = new ServiceContainer();
            container.Register<ISomeProcess, SomeProcess>()
                     .Register<ILogger, XunitLoggerAdapter>(new PerContainerLifetime())
                     .RegisterInstance(_output)
                     .Register<LoggingInterceptor>()
                     .Intercept(registration => registration.ServiceType == typeof(ISomeProcess), factory => factory.GetInstance<LoggingInterceptor>());

            var process = container.GetInstance<ISomeProcess>();
            process.DoSomethingSpecial();

            var xunitLogger = (XunitLoggerAdapter) container.GetInstance<ILogger>();
            xunitLogger.NumberOfLogCalls.MustBe(2);
        }

        [Fact]
        public void Decorator()
        {
            var container = new ServiceContainer();
            container.Register<ISomeProcess, SomeProcess>()
                     .Decorate<ISomeProcess, SomeProcessLoggingDecorator>()
                     .Register<ILogger, XunitLoggerAdapter>(new PerContainerLifetime())
                     .RegisterInstance(_output);

            var process = container.GetInstance<ISomeProcess>();
            process.DoSomethingSpecial();

            var xunitLogger = (XunitLoggerAdapter)container.GetInstance<ILogger>();
            xunitLogger.NumberOfLogCalls.MustBe(2);
        }
    }

    public interface ISomeProcess
    {
        void DoSomethingSpecial();
    }

    public sealed class SomeProcess : ISomeProcess
    {
        public void DoSomethingSpecial() { }
    }

    public sealed class LoggingInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public LoggingInterceptor(ILogger logger)
        {
            _logger = logger.MustNotBeNull();
        }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            _logger.Log($"I'm calling method \"{invocationInfo.Method.Name}\".");
            var returnValue = invocationInfo.Proceed();
            _logger.Log($"Method \"{invocationInfo.Method.Name}\" finished successfully.");
            return returnValue;
        }
    }

    public sealed class SomeProcessLoggingDecorator : ISomeProcess
    {
        private readonly ILogger _logger;
        private readonly ISomeProcess _decoratedProcess;

        public SomeProcessLoggingDecorator(ISomeProcess decoratedProcess, ILogger logger)
        {
            _decoratedProcess = decoratedProcess.MustNotBeNull();
            _logger = logger.MustNotBeNull();
        }

        public void DoSomethingSpecial()
        {
            _logger.Log($"Starting the process \"{_decoratedProcess}\".");
            _decoratedProcess.DoSomethingSpecial();
            _logger.Log($"Process \"{_decoratedProcess}\" finished successfully.");
        }
    }

    public interface ILogger
    {
        void Log(string message);
    }

    public sealed class XunitLoggerAdapter : ILogger
    {
        private readonly ITestOutputHelper _output;
        private int _numberOfLogCalls;

        public XunitLoggerAdapter(ITestOutputHelper output)
        {
            _output = output.MustNotBeNull();
        }

        public int NumberOfLogCalls => _numberOfLogCalls;

        public void Log(string message)
        {
            _numberOfLogCalls++;
            _output.WriteLine(message);
        }
    }
}