using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct Message
{
    public string Id { get; set; }
    public int CreatedAt { get; set; }
    public string ThreadId { get; set; }
    public string Role { get; set; }
    public string? AssistantId { get; set; }
    public string? RunId { get; set; }
    public ImmutableArray<MessageContent>? Content { get; set; }
    public ImmutableDictionary<string, string?>? Metadata { get; set; }
}
