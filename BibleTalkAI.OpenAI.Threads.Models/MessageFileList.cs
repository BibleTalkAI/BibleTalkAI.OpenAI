using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct MessageFileList
{
    public ImmutableArray<MessageFile> Data { get; set; }

    [JsonPropertyName("first_id")]
    public string FirstId { get; set; }

    [JsonPropertyName("last_id")]
    public string LastId { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}
