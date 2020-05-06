using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces
{
    public interface ILocalizedDescription
    {
        LocalizedString Description { get; set; }
    }
}
