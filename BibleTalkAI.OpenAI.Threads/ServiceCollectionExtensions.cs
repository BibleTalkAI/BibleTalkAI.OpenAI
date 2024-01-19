using BibleTalkAI.OpenAI.Http.Abstractions;
using BibleTalkAI.OpenAI.Threads.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BibleTalkAI.OpenAI.Threads;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddThreadsApiClients(this IServiceCollection services,
        string apiKey,
        string baseAddress = Constants.BaseAddress,
        bool removeHttpLogging = true)
        => services.AddApiClients<IThreadApiClient, ThreadApiClient, IThreadsApiClient, ThreadsApiClient>(apiKey, baseAddress, removeHttpLogging);
}