using System;
using System.Collections.Generic;
using System.Text;
using System;

namespace Application.Common.Models
{
    public interface IDocumentRepository
    {
        Document GetDocument(string name);
        List<Document> GetDocumentsList(List<string> names);
        void DeleteDocument(string name);
        void UpdateDocument(Document document);
        void InsertDocument(Document document);
    }
}
