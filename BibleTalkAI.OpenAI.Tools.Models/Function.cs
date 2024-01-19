using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Tools.Models;

public struct Function
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ImmutableArray<FunctionParameter>? Parameters { get; set; }
}
