using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Common.Exceptions
{
    public class DataTypeException : ValidationExceptionBase
    {
        public override string ErrorTemplate => "DTE_{0}_{1}";

        public string[] ErrorCodes { get; }


        public DataTypeException(DataTypeExceptionError[] errors) : base(null)
        {
            ErrorCodes = errors.Select(x => $"{Code + string.Format(ErrorTemplate, x.Code, x.PropertyName)}")
                .ToArray();
        }


        public class DataTypeExceptionError
        {
            public string PropertyName { get; set; }
            public string Code { get; set; }
        }
    }
}
