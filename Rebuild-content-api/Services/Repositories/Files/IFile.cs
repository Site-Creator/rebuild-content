﻿namespace Rebuild_content_api.Services.Repositories.Files
{
    public interface IFile
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Path { get; set; }
    }
}
