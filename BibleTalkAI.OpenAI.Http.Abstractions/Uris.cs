namespace BibleTalkAI.OpenAI.Http.Abstractions;

internal static class Uris
{
    public static readonly Uri Empty = Uri(string.Empty);
    public static Uri Id(string id) => Uri(id);

    private static Uri Uri(string path) => new(path, UriKind.Relative);
}
