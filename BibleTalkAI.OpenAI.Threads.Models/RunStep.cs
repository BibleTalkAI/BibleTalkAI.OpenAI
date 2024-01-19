using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct RunStep
{
    public string Id { get; set; }

    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }

    [JsonPropertyName("assistant_id")]
    public string AssistantId { get; set; }

    [JsonPropertyName("thread_id")]
    public string ThreadId { get; set; }

    [JsonPropertyName("run_id")]
    public string RunId { get; set; }

    public string Type { get; set; } // can be either message_creation or tool_calls.

    public string Status { get; set; } // can be either in_progress, cancelled, failed, completed, or expired.

    [JsonPropertyName("step_details")]
    public RunStepDetails StepDetails { get; set; }

    [JsonPropertyName("last_error")]
    public RunError? LastError { get; set; }

    [JsonPropertyName("expired_at")]
    public int? ExpiredAt { get; set; }

    [JsonPropertyName("cancelled_at")]
    public int? CancelledAt { get; set; }

    [JsonPropertyName("failed_at")]
    public int? FailedAt { get; set; }

    [JsonPropertyName("completed_at")]
    public int? CompletedAt { get; set; }
    public ImmutableDictionary<string, string?>? Metadata { get; set; }
}
