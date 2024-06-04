using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct MessageCreate()
{
    public string Role { get; set; } = "user"; // Currently only "user" is supported
    public string Content { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ImmutableArray<Attachment>? Attachments { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ImmutableDictionary<string, string?>? Metadata { get; set; }
}
