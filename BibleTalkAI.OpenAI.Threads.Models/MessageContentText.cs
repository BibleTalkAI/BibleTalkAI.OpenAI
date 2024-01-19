using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct MessageContentText
{
    public string? Value { get; set; }
    public ImmutableArray<string>? Annotations { get; set; }
}