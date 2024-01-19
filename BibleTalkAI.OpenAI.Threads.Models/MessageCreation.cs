using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct MessageCreation
{
    [JsonPropertyName("message_id")]
    public string MessageId { get; set; }
}
