using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BibleTalkAI.OpenAI.Http.Abstractions;

public abstract class ApiClientBase
    (HttpClient httpClient, JsonSerializerOptions serializerOptions)
{
    private static readonly HashSet<HttpStatusCode> _nonRetryableStatusCodes =
    [
        HttpStatusCode.Unauthorized,
        HttpStatusCode.Forbidden,
        HttpStatusCode.BadRequest
    ];

    private static readonly ResiliencePipeline _resilience = new ResiliencePipelineBuilder()
        .AddRetry(new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder().Handle<Exception>((exception) => 
            {
                if (exception is HttpRequestException httpException && httpException.StatusCode != null)
                {
                    // Retry on transient errors (skip non-retryable status codes)
                    return !_nonRetryableStatusCodes.Contains(httpException.StatusCode.Value);
                }

                // Retry on all other exceptions
                return true;
            }),
            Delay = TimeSpan.FromMilliseconds(700),
            MaxRetryAttempts = 3,
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true
        })
        .AddTimeout(TimeSpan.FromSeconds(30))
        .Build();

    public ApiClientBase(
        HttpClient httpClient,
        JsonSerializerOptions serializerOptions,
        Uri baseAddress,
        string apiKey)
        : this(httpClient, serializerOptions)
    {
        httpClient.BaseAddress = baseAddress;

        httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
    }

    protected async ValueTask Delete(Uri endpoint)
    {
        HttpResponseMessage? _ = await _resilience.ExecuteAsync(async cancellationToken 
            => await httpClient.DeleteAsync(endpoint, cancellationToken));

        return;
    }

    protected async ValueTask<TResponse?> Get<TResponse>(Uri endpoint)
    {
        HttpResponseMessage? response = await _resilience.ExecuteAsync(async cancellationToken
            => await httpClient.GetAsync(endpoint, cancellationToken));

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<TResponse>(
            serializerOptions);
    }

    protected async ValueTask<TResponse?> Post<TResponse>(
        Uri endpoint)
    {
        HttpResponseMessage? response = await _resilience.ExecuteAsync(async cancellationToken
            => await httpClient.PostAsync(endpoint, null, cancellationToken));

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<TResponse>(
            serializerOptions);
    }

    protected async ValueTask<TResponse?> Post<TRequest, TResponse>(
        Uri endpoint,
        TRequest request)
    {
        JsonContent requestContent = JsonContent.Create(
            request,
            typeof(TRequest),
            options: serializerOptions);

        HttpResponseMessage? response = await _resilience.ExecuteAsync(async cancellationToken
            => await httpClient.PostAsync(endpoint, requestContent, cancellationToken));

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        return await response.Content.ReadFromJsonAsync<TResponse>(
            serializerOptions);
    }
}
