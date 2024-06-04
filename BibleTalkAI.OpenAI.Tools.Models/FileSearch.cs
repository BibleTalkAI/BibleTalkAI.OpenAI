using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Tools.Models;

public struct FileSearch()
{
    [JsonPropertyName("vector_store_ids")]
    public ImmutableArray<string> VectorStoreIds { get; set; } = [];
}
