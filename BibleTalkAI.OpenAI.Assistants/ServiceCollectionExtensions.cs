using BibleTalkAI.OpenAI.Assistants.Http;
using BibleTalkAI.OpenAI.Http.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BibleTalkAI.OpenAI.Assistants;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAssistantsApiClients(
        this IServiceCollection services,
        string apiKey,
        string baseAddress = Constants.BaseAddress,
        bool removeHttpLogging = true)
        => services.AddApiClients<IAssistantApiClient, AssistantApiClient, IAssistantsApiClient, AssistantsApiClient>(apiKey, baseAddress, removeHttpLogging);
}