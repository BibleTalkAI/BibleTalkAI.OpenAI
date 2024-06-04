using BibleTalkAI.OpenAI.Tools.Models;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct Thread
{
    public string Id { get; set; }

    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }

    public ImmutableArray<Tool>? Tools { get; set; }

    public ToolResource? ToolResources { get; set; }

    public ImmutableDictionary<string, string?>? Metadata { get; set; }
}
