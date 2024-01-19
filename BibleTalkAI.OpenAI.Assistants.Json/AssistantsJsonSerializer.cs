using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Assistants.Json;

public static class AssistantsJsonSerializer
{
    public static readonly JsonSerializerContext Context = GeneratedJsonSerializerContext.Default;
    public static readonly JsonSerializerOptions Options = Context.Options;
}