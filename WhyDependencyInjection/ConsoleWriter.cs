using System;

namespace WhyDependencyInjection
{
    public sealed class ConsoleWriter : IWriter
    {
        public void Write(char character)
        {
            Console.Write(character);
        }
    }
}