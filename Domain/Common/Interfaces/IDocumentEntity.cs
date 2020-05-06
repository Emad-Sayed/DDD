using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces
{
    public interface IDocumentEntity
    {
        string DocumentName { get; set; }
        string DocumentSystemName { get; set; }

        void SetDocument(string documentName, string documentSystemName);
    }
}
