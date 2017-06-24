
namespace WhyDependencyInjection
{
    public static class Program
    {
        public static void Main()
        {
            // Register
            var reader = new ConsoleReader();
            //var writer = new ConsoleWriter();
            var writer = new FileWriter("text.txt");
            var copyProcess = new CopyProcess(reader, writer);

            // Resolve
            copyProcess.Copy();

            // Release
            writer.Dispose();
        }
    }
}