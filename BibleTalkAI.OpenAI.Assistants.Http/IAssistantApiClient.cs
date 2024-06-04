using BibleTalkAI.OpenAI.Assistants.Models;

namespace BibleTalkAI.OpenAI.Assistants.Http;

public interface IAssistantApiClient
{
    ValueTask DeleteAssistant(string id);
    ValueTask<Assistant?> ModifyAssistant(string id, AssistantModify request);
    ValueTask<Assistant?> RetrieveAssistant(string id);
}
