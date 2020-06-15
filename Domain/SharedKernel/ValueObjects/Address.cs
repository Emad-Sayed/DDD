using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.SharedKernel.ValueObjects
{
    public class Address : ValueObject
    {
        public string Area { get; private set; }
        public string City { get; private set; }

        private Address() { }

        public Address(string area, string city)
        {
            Area = area;
            City = city;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Area;
            yield return City;
        }
    }
}
