using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ProductCatalog
{
    using static ProductCatalogTesting;

    public class ProductCatalogTestBase
    {

        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
