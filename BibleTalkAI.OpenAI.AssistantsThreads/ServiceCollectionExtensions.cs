using BibleTalkAI.OpenAI.Assistants;
using BibleTalkAI.OpenAI.Http.Abstractions;
using BibleTalkAI.OpenAI.Threads;
using Microsoft.Extensions.DependencyInjection;

namespace BibleTalkAI.OpenAI.AssistantsThreads;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAssistantsThreadsApiClients(this IServiceCollection services,
        string apiKey,
        string baseAddress = Constants.BaseAddress,
        bool removeHttpLogging = true)
    {
        services.AddAssistantsApiClients(apiKey, baseAddress, removeHttpLogging);
        services.AddThreadsApiClients(apiKey, baseAddress, removeHttpLogging);

        return services;
    }
}