using System;

namespace WhyDependencyInjection
{
    public sealed class ConsoleReader : IReader
    {
        public ReadResult Read()
        {
            var consoleKeyInfo = Console.ReadKey(true);
            return new ReadResult(consoleKeyInfo.KeyChar, consoleKeyInfo.Key == ConsoleKey.Escape);
        }
    }
}