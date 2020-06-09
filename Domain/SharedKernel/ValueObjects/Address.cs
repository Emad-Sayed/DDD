using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.SharedKernel.ValueObjects
{
    public class Address : ValueObject
    {
        public string Region { get; private set; }
        public string City { get; private set; }

        private Address() { }

        public Address(string region, string city)
        {
            Region = region;
            City = city;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Region;
            yield return City;
        }
    }
}
