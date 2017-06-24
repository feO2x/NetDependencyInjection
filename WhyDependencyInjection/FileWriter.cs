using System;
using System.IO;

namespace WhyDependencyInjection
{
    public sealed class FileWriter : IWriter, IDisposable
    {
        private readonly StreamWriter _streamWriter;

        public FileWriter(string path)
        {
            _streamWriter = new StreamWriter(new FileStream(path, FileMode.Create));
        }

        public void Dispose()
        {
            _streamWriter.Dispose();
        }


        public void Write(char character)
        {
            _streamWriter.Write(character);
        }
    }
}