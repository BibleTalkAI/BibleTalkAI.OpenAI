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

    /// <summary>post https://api.openai.com/v1/assistants/{assistant_id}/files</summary>
    public async ValueTask<AssistantFile?> CreateAssistantFile(
        string id,
        AssistantFileCreate request)
        => await Post<AssistantFileCreate, AssistantFile?>(UriFiles(id), request);

    /// <summary>get https://api.openai.com/v1/assistants/{assistant_id}/files/{file_id}</summary>
    public async ValueTask<AssistantFile?> RetrieveAssistantFile(
        string id,
        string file)
        => await Get<AssistantFile>(UriFileId(id, file));

    /// <summary>delete https://api.openai.com/v1/assistants/{assistant_id}/files/{file_id}</summary>
    public async ValueTask DeleteAssistantFile(
        string id,
        string file)
        => await Delete(UriFileId(id, file));

    /// <summary>get https://api.openai.com/v1/assistants/{assistant_id}/files</summary>
    public async ValueTask<AssistantFileList> ListAssistantFiles(
        string id,
        ListParameters query)
        => await Get<AssistantFileList>(query.ToUri(id, "/files"));

    private static Uri UriFiles(string id)
        => new(id + "/files", UriKind.Relative);

    private static Uri UriFileId(string id, string file)
        => new(id + "/files/" + file, UriKind.Relative);
}