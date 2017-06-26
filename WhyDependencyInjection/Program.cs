
using LightInject;

namespace WhyDependencyInjection
{
    public static class Program
    {
        public static void Main()
        {
            // Register
            var container = new ServiceContainer();
            container.Register<IReader, ConsoleReader>()
                     //.Register<IWriter, ConsoleWriter>()
                     .Register<IWriter>(f => new FileWriter("text.txt"), new PerRequestLifeTime())
                     .Register<CopyProcess>();


            //var reader = new ConsoleReader();
            ////var writer = new ConsoleWriter();
            //var writer = new FileWriter("text.txt");
            using (container.BeginScope())
            {
                var copyProcess = container.GetInstance<CopyProcess>();

                // Resolve
                copyProcess.Copy();
            }

            // Release
            //writer.Dispose();
        }
    }
}