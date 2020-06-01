using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class Photo
    {

        public Photo()
        {

        }

        public Photo(byte[] content, string extension, Guid id = default)
        {
            Content = content;
            Extension = extension;
            Id = id == default ? Guid.NewGuid() : id;
        }
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public string Extension { get; set; }
        public long Size => Content.Length;

        public string Name => Id + Extension;
    }
}
