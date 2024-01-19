using System.Net.Http.Json;
using System.Text.Json;

namespace BibleTalkAI.OpenAI.Http.Abstractions;

public abstract class ApiClientBase
    (HttpClient httpClient, JsonSerializerOptions serializerOptions)
{
    public ApiClientBase(
        HttpClient httpClient,
        JsonSerializerOptions serializerOptions,
        Uri baseAddress,
        string apiKey)
        : this(httpClient, serializerOptions)
    {
        httpClient.BaseAddress = baseAddress;

        httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v1");
    }

    protected async ValueTask Delete(Uri endpoint)
    {
        HttpResponseMessage? _ = await httpClient.DeleteAsync(
            endpoint);

        return;
    }

    protected async ValueTask<TResponse?> Get<TResponse>(Uri endpoint)
        => await httpClient.GetFromJsonAsync<TResponse>(
            endpoint,
            serializerOptions);

    protected async ValueTask<TResponse?> Post<TResponse>(
        Uri endpoint)
    {
        HttpResponseMessage? response = await httpClient.PostAsync(endpoint, null);

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

        HttpResponseMessage? response = await httpClient.PostAsync(
            endpoint,
            requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
            return default;
        }

        return await response.Content.ReadFromJsonAsync<TResponse>(
            serializerOptions);
    }
}
