using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Tools.Models;

public struct CodeInterpreter()
{
    [JsonPropertyName("file_ids")]
    public ImmutableArray<string> FileIds { get; set; } = [];
}
