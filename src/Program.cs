namespace Orchid.Service.Aspire;

/// <summary>
/// Orchid 로컬 컨테이너 오케스트레이션 진입점입니다.
/// </summary>
public static class Program
{
    /// <summary>
    /// 오케스트레이션 대상 서비스 컨테이너 이미지 목록을 반환합니다.
    /// </summary>
    /// <param name="registryPrefix">컨테이너 레지스트리 접두사</param>
    /// <param name="imageTag">이미지 태그</param>
    /// <returns>서비스 슬러그와 이미지 이름 매핑</returns>
    public static IReadOnlyDictionary<string, string> GetServiceImages(
        string registryPrefix = "harbor.hanul.botanicsoft.com/orchid/",
        string imageTag = "latest")
    {
        return new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["orchid-api-gateway"] = $"{registryPrefix}api-gateway:{imageTag}",
            ["orchid-clipboard"] = $"{registryPrefix}clipboard:{imageTag}",
            ["orchid-web"] = $"{registryPrefix}web:{imageTag}"
        };
    }

    /// <summary>
    /// 애플리케이션 진입점입니다.
    /// </summary>
    public static void Main()
    {
    }
}
