using System;

namespace WhyDependencyInjection
{
    public sealed class CopyProcess
    {
        private readonly IReader _reader;
        private readonly IWriter _writer;

        public CopyProcess(IReader reader, IWriter writer)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void Copy()
        {
            while (true)
            {
                var readResult = _reader.Read();
                if (readResult.ShouldQuit) return;
                _writer.Write(readResult.Character);
            }
        }
    }
}