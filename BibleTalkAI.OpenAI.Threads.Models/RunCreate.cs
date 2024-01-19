using BibleTalkAI.OpenAI.Tools.Models;
using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct RunCreate
{
    public string AssistantId { get; set; }
    public string? Model { get; set; }
    public string? Instructions { get; set; }
    public ImmutableArray<Tool>? Tools { get; set; }
    public ImmutableDictionary<string, string?>? Metadata { get; set; }
}
