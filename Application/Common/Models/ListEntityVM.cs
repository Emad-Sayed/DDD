using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class ListEntityVM<T> where T: class
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
