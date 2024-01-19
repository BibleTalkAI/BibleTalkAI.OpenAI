using BibleTalkAI.OpenAI.Tools.Models;
using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Assistants.Models;

public struct Assistant
{
    public string Id { get; set; }
    public string Model { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Instructions { get; set; }
    public ImmutableArray<Tool>? Tools { get; set; }
    public int CreatedAt { get; set; }
    public ImmutableDictionary<string, string?>? Metadata { get; set; }
}
