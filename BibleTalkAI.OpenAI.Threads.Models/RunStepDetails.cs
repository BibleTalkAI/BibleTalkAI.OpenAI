using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct RunStepDetails
{
    public string Type { get; set; }

    [JsonPropertyName("tool_calls")]
    public ImmutableArray<RunToolCall>? ToolCalls { get; set; }

    [JsonPropertyName("message_creation")]
    public MessageCreation? MessageCreation { get; set; }
}
