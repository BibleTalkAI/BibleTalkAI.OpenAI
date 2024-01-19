using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Assistants.Models;

public struct AssistantFileList
{
    public ImmutableArray<AssistantFile> Data { get; set; }
    public string FirstId { get; set; }
    public string LastId { get; set; }
    public bool HasMore { get; set; }
}
