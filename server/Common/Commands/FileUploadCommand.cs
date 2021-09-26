using System;
using System.IO;
using MediatR;

namespace Common.Commands
{
    public class FileUploadCommand : INotification
    {
        public Guid Id { get; set; }
        public MemoryStream File { get; set; }
        public string FileName { get; set; }
    }
}