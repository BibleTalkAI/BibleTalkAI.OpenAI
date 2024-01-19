namespace BibleTalkAI.OpenAI.Tools.Models;

public struct Tool
{
    public string Type { get; set; }
    public Function? Function { get; set; }
}
