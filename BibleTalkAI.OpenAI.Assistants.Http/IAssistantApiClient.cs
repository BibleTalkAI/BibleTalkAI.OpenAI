using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Http.Abstractions;

namespace BibleTalkAI.OpenAI.Assistants.Http;

public interface IAssistantApiClient
{
    ValueTask<AssistantFile?> CreateAssistantFile(string id, AssistantFileCreate request);
    ValueTask DeleteAssistant(string id);
    ValueTask DeleteAssistantFile(string id, string file);
    ValueTask<AssistantFileList> ListAssistantFiles(string id, ListParameters query);
    ValueTask<Assistant?> ModifyAssistant(string id, AssistantModify request);
    ValueTask<Assistant?> RetrieveAssistant(string id);
    ValueTask<AssistantFile?> RetrieveAssistantFile(string id, string file);
}
