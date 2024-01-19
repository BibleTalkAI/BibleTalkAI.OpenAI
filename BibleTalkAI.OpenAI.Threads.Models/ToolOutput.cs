namespace BibleTalkAI.OpenAI.Threads.Models;

public struct ToolOutput
{
    public string ToolCallId { get; set; }
    public string? Output { get; set; }
}
