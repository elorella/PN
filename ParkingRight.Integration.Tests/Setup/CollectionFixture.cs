using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ParkingRight.Integration.Tests.Setup
{
    [CollectionDefinition("api")]
    public class CollectionFixture : ICollectionFixture<TestContext>
    {
    }
}
