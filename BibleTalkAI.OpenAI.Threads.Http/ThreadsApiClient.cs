using BibleTalkAI.OpenAI.Http.Abstractions;
using BibleTalkAI.OpenAI.Threads.Json;
using BibleTalkAI.OpenAI.Threads.Models;
using Microsoft.Extensions.Options;
using Thread = BibleTalkAI.OpenAI.Threads.Models.Thread;

namespace BibleTalkAI.OpenAI.Threads.Http;

public class ThreadsApiClient(
    HttpClient httpClient,
    IOptionsMonitor<OpenAiApiOptions> options)
    : DefaultApiClient(
        httpClient,
        ThreadsJsonSerializer.Options,
        options,
        "threads"),
    IThreadsApiClient
{
    /// <summary>post https://api.openai.com/v1/threads</summary>
    public async ValueTask<Thread?> CreateThread(ThreadCreate request)
        => await Post<ThreadCreate, Thread?>(request);
}
