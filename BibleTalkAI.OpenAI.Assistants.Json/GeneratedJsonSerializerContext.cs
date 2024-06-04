using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Tools.Models;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace BibleTalkAI.OpenAI.Assistants.Json;

[JsonSourceGenerationOptions(
    WriteIndented = false, 
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
// system types
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(int))]
// collections
[JsonSerializable(typeof(ImmutableDictionary<string, string?>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(ImmutableArray<string>))]
// api types
[JsonSerializable(typeof(Assistant))]
[JsonSerializable(typeof(Assistant?))]
[JsonSerializable(typeof(Tool))]
[JsonSerializable(typeof(Tool?))]
[JsonSerializable(typeof(ToolResource))]
[JsonSerializable(typeof(ToolResource?))]
[JsonSerializable(typeof(FileSearch))]
[JsonSerializable(typeof(FileSearch?))]
[JsonSerializable(typeof(CodeInterpreter))]
[JsonSerializable(typeof(CodeInterpreter?))]
[JsonSerializable(typeof(Function))]
[JsonSerializable(typeof(Function?))]
[JsonSerializable(typeof(FunctionParameter))]
[JsonSerializable(typeof(FunctionParameter?))]
[JsonSerializable(typeof(AssistantCreate))]
[JsonSerializable(typeof(AssistantCreate?))]
[JsonSerializable(typeof(AssistantModify))]
[JsonSerializable(typeof(AssistantModify?))]
[JsonSerializable(typeof(AssistantList))]
[JsonSerializable(typeof(AssistantList?))]
// collections
[JsonSerializable(typeof(ImmutableArray<Tool>))]
[JsonSerializable(typeof(ImmutableArray<FunctionParameter>))]
[JsonSerializable(typeof(ImmutableArray<Assistant>))]
internal partial class GeneratedJsonSerializerContext
    : JsonSerializerContext
{
}
