using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Application.IntegrationTests.OrderManagment
{
    using static OrderManagmentTesting;

    public class OrderManagmentTestBase
    {

        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
