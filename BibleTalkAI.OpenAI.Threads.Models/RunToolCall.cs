namespace BibleTalkAI.OpenAI.Threads.Models;

public struct RunToolCall
{
    public string Id { get; set; }
    public string Type { get; set; }
    public RunToolCallFunction? Function { get; set; }
}
