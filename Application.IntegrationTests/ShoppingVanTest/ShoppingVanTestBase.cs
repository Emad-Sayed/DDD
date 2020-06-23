using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ShoppingVanTest
{

     using static ShoppingVanTesting;

    public class ShoppingVanTestBase
    {

        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
