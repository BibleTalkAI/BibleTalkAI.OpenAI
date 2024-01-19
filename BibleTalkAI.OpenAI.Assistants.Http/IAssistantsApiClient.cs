using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Http.Abstractions;

namespace BibleTalkAI.OpenAI.Assistants.Http;

public interface IAssistantsApiClient
{
    ValueTask<Assistant?> CreateAssistant(AssistantCreate request);
    ValueTask<AssistantList> ListAssistants(ListParameters query);
}
