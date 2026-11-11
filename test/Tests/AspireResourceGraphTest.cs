using FluentAssertions;
using Orchid.Service.Aspire;
using Xunit;

namespace Orchid.Service.Aspire.Tests;

public sealed class AspireResourceGraphTest
{
    [Fact]
    public void GetServiceImagesShouldIncludeApiGatewayClipboardAndWebContainers()
    {
        // Given & When
        IReadOnlyDictionary<string, string> images = Program.GetServiceImages();

        // Then
        images.Keys.Should().BeEquivalentTo(["orchid-api-gateway", "orchid-clipboard", "orchid-web"]);
    }

    [Fact]
    public void StagingIngressAndGatewayConfigMapShouldUseOrchidSubpathAndPathRemovePrefix()
    {
        // Given
        string repoRoot = FindRepositoryRoot();
        string stagingIngress = File.ReadAllText(Path.Combine(repoRoot, "deploy/k8s/overlays/staging/ingress.yaml"));
        string baseKustomization = File.ReadAllText(Path.Combine(repoRoot, "deploy/k8s/base/kustomization.yaml"));
        string gatewayConfigMap = File.ReadAllText(Path.Combine(repoRoot, "deploy/k8s/base/api-gateway/config-map.yaml"));

        // When & Then
        stagingIngress.Should().Contain("host: stag.botanicsoft.com");
        stagingIngress.Should().Contain("path: /orchid");
        baseKustomization.Should().Contain("web/deployment.yaml");
        baseKustomization.Should().Contain("web/service.yaml");
        gatewayConfigMap.Should().Contain("/orchid/hubs/clipboard/{**catch-all}");
        gatewayConfigMap.Should().Contain("\"web-subpath-route\"");
        gatewayConfigMap.Should().Contain("http://orchid-web-service:8080");
        gatewayConfigMap.Should().Contain("\"PathRemovePrefix\": \"/orchid\"");
    }

    private static string FindRepositoryRoot()
    {
        DirectoryInfo? dir = new(AppContext.BaseDirectory);
        while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "Orchid.slnx")))
        {
            dir = dir.Parent;
        }

        return dir?.FullName ?? throw new InvalidOperationException("Orchid.slnx not found.");
    }
}
