using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SharedKernel.ViewModels
{
    public class BaseSearchVM
    {
        public string Keyword { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
