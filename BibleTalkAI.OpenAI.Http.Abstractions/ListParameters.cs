namespace BibleTalkAI.OpenAI.Http.Abstractions;

public readonly struct ListParameters()
{
    public static readonly ListParameters DefaultLimit1 = new(limit: 1, default, default, default);
    public static readonly ListParameters Default = new();

    public ListParameters(int? limit, string? order, string? after, string? before) : this()
    {
        Limit = limit; 
        Order = order; 
        After = after; 
        Before = before;
    }

    public int? Limit { get; init; } // = 20; // max 100
    public string? Order { get; init; } // = "desc";
    public string? After { get; init; }
    public string? Before { get; init; }

    public Uri ToUri(string? path1 = null, string? path2 = null, string? path3 = null, string? path4 = null, UriKind kind = UriKind.Relative)
    {
        bool hasLimit = Limit != null;
        bool hasOrder = Order != null;
        bool hasAfter = After != null;
        bool hasBefore = Before != null;

        string path = (hasLimit, hasOrder, hasAfter, hasBefore) switch
        {
            (true, true, true, true) => $"{path1}{path2}{path3}{path4}?limit={Limit}&order={Order}&after={After}&before={Before}",
            (true, true, true, false) => $"{path1}{path2}{path3}{path4}?limit={Limit}&order={Order}&after={After}",
            (true, true, false, true) => $"{path1}{path2}{path3}{path4}?limit={Limit}&order={Order}&before={Before}",
            (true, true, false, false) => $"{path1}{path2}{path3}{path4}?limit={Limit}&order={Order}",
            (true, false, true, true) => $"{path1}{path2}{path3}{path4}?limit={Limit}&after={After}&before={Before}",
            (true, false, true, false) => $"{path1}{path2}{path3}{path4}?limit={Limit}&after={After}",
            (true, false, false, true) => $"{path1}{path2}{path3}{path4}?limit={Limit}&before={Before}",
            (true, false, false, false) => $"{path1}{path2}{path3}{path4}?limit={Limit}",
            (false, true, true, true) => $"{path1}{path2}{path3}{path4}?order={Order}&after={After}&before={Before}",
            (false, true, true, false) => $"{path1}{path2}{path3}{path4}?order={Order}&after={After}",
            (false, true, false, true) => $"{path1}{path2}{path3}{path4}?order={Order}&before={Before}",
            (false, true, false, false) => $"{path1}{path2}{path3}{path4}?order={Order}",
            (false, false, true, true) => $"{path1}{path2}{path3}{path4}?after={After}&before={Before}",
            (false, false, true, false) => $"{path1}{path2}{path3}{path4}?after={After}",
            (false, false, false, true) => $"{path1}{path2}{path3}{path4}?before={Before}",
            (false, false, false, false) => path1 + path2 + path3 + path4
        };

        return new(path, kind);
    }
}
