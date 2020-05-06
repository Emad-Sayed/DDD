using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IFileRequest
    {
        string DocumentName { get; set; }
        byte[] DocumentContent { get; set; }
    }
}
