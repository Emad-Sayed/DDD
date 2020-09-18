using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.CustomerManagment.Exceptions
{
    public class CityAlreadyExistException : BusinessException
    {
        public CityAlreadyExistException(string name)
           : base(HttpStatusCode.BadRequest, $"City with name {name} already exist", "city_exist")
        {
        }
    }
}
