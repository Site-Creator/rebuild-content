namespace RebuilderLib.Exceptions
{
    public class FileRepositoryException : Exception
    {
        public FileRepositoryException() { }

        public FileRepositoryException(string message) : base(message) { }

        public FileRepositoryException(string message, Exception inner) : base(message, inner) { }
    }
}
