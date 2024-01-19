using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct MessageFile
{
    public string Id { get; set; }

    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }

    [JsonPropertyName("message_id")]
    public string MessageId { get; set; }
}
