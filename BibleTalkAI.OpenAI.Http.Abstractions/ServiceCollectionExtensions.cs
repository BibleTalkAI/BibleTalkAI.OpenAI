using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace BibleTalkAI.OpenAI.Http.Abstractions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClient<TClient, TImplementation>(this IServiceCollection services,
        string apiKey,
        string baseAddress = Constants.BaseAddress,
        bool removeHttpLogging = true)
        where TClient : class
        where TImplementation : class, TClient
    {
        services.Configure<OpenAiApiOptions>(options =>
        {
            options.ApiKey = apiKey;
            options.BaseAddress = baseAddress;
        });

        services.AddHttpClient<TClient, TImplementation>();

        if (removeHttpLogging)
        {
            // Remove logging https://github.com/aspnet/HttpClientFactory/issues/196#issuecomment-432755765 
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
        }

        return services;
    }

    public static IServiceCollection AddApiClients<TClient1, TImplementation1, TClient2, TImplementation2>(this IServiceCollection services,
        string apiKey,
        string baseAddress = Constants.BaseAddress,
        bool removeHttpLogging = true)
        where TClient1 : class
        where TImplementation1 : class, TClient1
        where TClient2 : class
        where TImplementation2 : class, TClient2
    {
        services.AddHttpClient<TClient1, TImplementation1>();
        return services.AddApiClient<TClient2, TImplementation2>(apiKey, baseAddress, removeHttpLogging);
    }
}