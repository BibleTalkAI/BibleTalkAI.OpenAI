namespace BibleTalkAI.OpenAI.Http.Abstractions;

public class OpenAiApiOptions
{
    public string? ApiKey { get; set; }
    public string? BaseAddress { get; set; }

    internal string GetBaseAddress() => !string.IsNullOrEmpty(BaseAddress) ? BaseAddress : Constants.BaseAddress;
}
