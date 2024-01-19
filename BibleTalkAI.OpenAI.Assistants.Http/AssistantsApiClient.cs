using BibleTalkAI.OpenAI.Assistants.Json;
using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Http.Abstractions;
using Microsoft.Extensions.Options;

namespace BibleTalkAI.OpenAI.Assistants.Http;

/// <summary>
/// OpenAI Assistants API client
/// </summary>
public class AssistantsApiClient(
    HttpClient httpClient,
    IOptionsMonitor<OpenAiApiOptions> options)
    : DefaultApiClient(
        httpClient, 
        AssistantsJsonSerializer.Options,
        options, 
        "assistants"), 
    IAssistantsApiClient
{
    /// <summary>post https://api.openai.com/v1/assistants</summary>
    public async ValueTask<Assistant?> CreateAssistant(AssistantCreate request)
        => await Post<AssistantCreate, Assistant?>(request);

    /// <summary>get https://api.openai.com/v1/assistants</summary>
    public async ValueTask<AssistantList> ListAssistants(ListParameters query)
        => await Get<AssistantList>(query.ToUri());
}
