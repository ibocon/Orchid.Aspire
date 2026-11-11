using FluentAssertions;
using Orchid.Service.Aspire;
using Xunit;

namespace Orchid.Service.Aspire.Tests;

public sealed class AspireResourceGraphTest
{
    [Fact]
    public void GetServiceImagesShouldIncludeApiGatewayAndClipboardContainers()
    {
        IReadOnlyDictionary<string, string> images = Program.GetServiceImages();

        images.Keys.Should().BeEquivalentTo(["orchid-api-gateway", "orchid-clipboard"]);
    }
}