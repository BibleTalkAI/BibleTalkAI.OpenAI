using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct RunSubmitToolOutputs
{
    [JsonPropertyName("tool_calls")]
    public ImmutableArray<RunToolCall> ToolCalls { get; set; }
}
