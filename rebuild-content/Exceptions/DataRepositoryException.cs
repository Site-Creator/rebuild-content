namespace RebuilderLib.Exceptions
{
    public class DataRepositoryException : Exception
    {
        public DataRepositoryException() { }

        public DataRepositoryException(string message) : base(message) { }

        public DataRepositoryException(string message, Exception inner) : base(message, inner) { }
    }
}
