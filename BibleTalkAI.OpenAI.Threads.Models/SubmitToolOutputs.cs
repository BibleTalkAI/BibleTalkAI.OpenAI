using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Threads.Models;

public struct SubmitToolOutputs
{
    public ImmutableArray<ToolOutput> ToolOutputs { get; set; }
}
