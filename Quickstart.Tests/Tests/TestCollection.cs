using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Quickstart.Tests.Tests
{
    [CollectionDefinition("Integration Tests")]
   public class TestCollection:ICollectionFixture<WebApplicationFactory<QuickStart.API.Startup>>
    {
    }
}
