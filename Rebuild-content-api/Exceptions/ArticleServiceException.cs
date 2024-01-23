﻿namespace Rebuild_content_api.Exceptions
{
    public class ArticleServiceException : Exception
    {
        public ArticleServiceException() { }

        public ArticleServiceException(string message) : base(message) { }

        public ArticleServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
