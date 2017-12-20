﻿using System.Threading.Tasks;
using TaurusSoftware.BillomatNet.Model;
using Xunit;

namespace TaurusSoftware.BillomatNet.Tests
{
    public class Test
    {
        [Fact]
        public async Task Testit()
        {
            var config = Helpers.GetTestConfiguration();

            var s = new ClientService(config);
            var r = await s.ListAsync(new ClientFilterSortOptions
            {
                Filter = new ClientFilter
                {
                    Name = "Regiofaktur"
                }
            });
            Assert.True(true);
        }
    }
}
