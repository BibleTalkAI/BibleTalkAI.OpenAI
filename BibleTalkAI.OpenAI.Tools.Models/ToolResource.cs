using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Tools.Models;

public struct ToolResource
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FileSearch? FileSearch { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CodeInterpreter? CodeInterpreter { get; set; }
}
