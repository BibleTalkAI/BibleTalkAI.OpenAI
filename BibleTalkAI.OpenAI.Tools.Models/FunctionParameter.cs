namespace BibleTalkAI.OpenAI.Tools.Models;

public struct FunctionParameter
{
    public string Type { get; set; }
    public Dictionary<string, object> Properties { get; set; }
}
