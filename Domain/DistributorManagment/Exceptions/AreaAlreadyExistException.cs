using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.DistributorManagment.Exceptions
{
    public class AreaAlreadyExistException : BusinessException
    {
        public AreaAlreadyExistException(string name)
           : base(HttpStatusCode.BadRequest, $"Area with name {name} already exist", "area_exist")
        {
        }
    }
}
