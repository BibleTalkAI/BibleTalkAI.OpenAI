using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BibleTalkAI.OpenAI.Http.Abstractions;

public abstract class DefaultApiClient(
    HttpClient httpClient,
    JsonSerializerOptions serializerOptions,
    IOptionsMonitor<OpenAiApiOptions> options,
    string? baseAddressSuffix = null)
    : ApiClientBase(
        httpClient,
        serializerOptions,
        new(options.CurrentValue.GetBaseAddress() + baseAddressSuffix),
        options.CurrentValue.ApiKey!)
{
    protected async ValueTask Delete(string id)
        => await Delete(Uris.Id(id));

    protected async ValueTask<TResponse?> Get<TResponse>(string id)
        => await Get<TResponse>(Uris.Id(id));

    protected async ValueTask<TResponse?> Post<TRequest, TResponse>(
        TRequest request)
        => await Post<TRequest, TResponse>(Uris.Empty, request);

    protected async ValueTask<TResponse?> Post<TRequest, TResponse>(
        string id,
        TRequest request)
        => await Post<TRequest, TResponse>(Uris.Id(id), request);
}
