using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces
{
    public interface ILocalizedName
    {
        LocalizedString Name { get; set; }
    }
}
