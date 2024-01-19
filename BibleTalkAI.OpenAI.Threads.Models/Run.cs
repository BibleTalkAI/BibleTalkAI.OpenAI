using BibleTalkAI.OpenAI.Tools.Models;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct Run
{
    public string Id { get; set; }

    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }

    [JsonPropertyName("thread_id")]
    public string ThreadId { get; set; }

    [JsonPropertyName("assistant_id")]
    public string AssistantId { get; set; }

    public string Status { get; set; }

    [JsonPropertyName("required_action")]
    public RunRequiredAction? RequiredAction { get; set; }

    [JsonPropertyName("last_error")]
    public RunError? LastError { get; set; }

    [JsonPropertyName("expires_at")]
    public int? ExpiresAt { get; set; }

    [JsonPropertyName("started_at")]
    public int? StartedAt { get; set; }

    [JsonPropertyName("cancelled_at")]
    public int? CancelledAt { get; set; }

    [JsonPropertyName("failed_at")]
    public int? FailedAt { get; set; }

    [JsonPropertyName("completed_at")]
    public int? CompletedAt { get; set; }
    public string Model { get; set; }
    public string? Instructions { get; set; }
    public ImmutableArray<Tool>? Tools { get; set; }

    [JsonPropertyName("file_ids")]
    public ImmutableArray<string>? FileIds { get; set; }
    public ImmutableDictionary<string, string?>? Metadata { get; set; }
}
