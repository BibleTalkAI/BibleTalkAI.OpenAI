using BibleTalkAI.OpenAI.Assistants.Json;
using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Http.Abstractions;
using Microsoft.Extensions.Options;

namespace BibleTalkAI.OpenAI.Assistants.Http;

/// <summary>
/// OpenAI Assistants API client
/// </summary>
public class AssistantApiClient(
    HttpClient httpClient,
    IOptionsMonitor<OpenAiApiOptions> options)
    : DefaultApiClient(
        httpClient,
        AssistantsJsonSerializer.Options,
        options,
        "assistants/"),
    IAssistantApiClient
{
    /// <summary>get https://api.openai.com/v1/assistants/{assistantId}</summary>
    public async ValueTask<Assistant?> RetrieveAssistant(string id)
        => await Get<Assistant>(id);

    /// <summary>post https://api.openai.com/v1/assistants/{assistantId}</summary>
    public async ValueTask<Assistant?> ModifyAssistant(string id, AssistantModify request)
        => await Post<AssistantModify, Assistant?>(id, request);

    /// <summary>delete https://api.openai.com/v1/assistants/{assistantId}</summary>
    public async ValueTask DeleteAssistant(string id)
        => await Delete(id);
}