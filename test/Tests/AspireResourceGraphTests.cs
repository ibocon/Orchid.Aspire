using FluentAssertions;
using Orchid.Service.Aspire;
using Xunit;

namespace Orchid.Service.Aspire.Tests;

public sealed class AspireResourceGraphTests
{
    [Fact]
    public void GetServiceImagesShouldIncludeApiGatewayAndClipboardContainers()
    {
        IReadOnlyDictionary<string, string> images = Program.GetServiceImages();

        images.Keys.Should().BeEquivalentTo(["orchid-api-gateway", "orchid-clipboard"]);
    }
}