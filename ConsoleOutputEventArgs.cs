using System;

namespace z.Console
{
    public class ConsoleOutputEventArgs : EventArgs
    {
        public ConsoleOutputEventArgs(string line, bool isError, object Tag)
        {
            Line = line;
            IsError = isError;
            this.Tag = Tag;
        }

        public string Line { get; private set; }
        public bool IsError { get; private set; }
        public object Tag { get; private set; }
    }
}