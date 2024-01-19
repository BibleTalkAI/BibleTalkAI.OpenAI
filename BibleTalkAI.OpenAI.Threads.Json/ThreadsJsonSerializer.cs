using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Threads.Json;

public static class ThreadsJsonSerializer
{
    public static readonly JsonSerializerContext Context = GeneratedJsonSerializerContext.Default;
    public static readonly JsonSerializerOptions Options = Context.Options;
}
