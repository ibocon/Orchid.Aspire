using System.Net;
using FluentAssertions;
using Xunit;

namespace Orchid.Service.Aspire.IntegrationTests;

public sealed class StagingSubpathRelayIntegrationTest
{
    [Fact]
    public async Task NegotiateOnStagingOrchidSubpathShouldReturnOkAndConnectionId()
    {
        // Given
        using HttpClient client = new();
        Uri negotiateUri = new("https://stag.botanicsoft.com/orchid/hubs/clipboard/negotiate?negotiateVersion=1");

        // When
        using HttpResponseMessage response = await
            client.PostAsync(negotiateUri, new StringContent(string.Empty))
            .ConfigureAwait(false);
        string body = await
            response.Content.ReadAsStringAsync()
            .ConfigureAwait(false);

        // Then
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Contain("connectionId");
    }
}