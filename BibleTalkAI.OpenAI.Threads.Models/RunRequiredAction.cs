using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct RunRequiredAction
{
    public string Type { get; set; }

    [JsonPropertyName("submit_tool_outputs")]
    public RunSubmitToolOutputs? SubmitToolOutputs { get; set; }
}
