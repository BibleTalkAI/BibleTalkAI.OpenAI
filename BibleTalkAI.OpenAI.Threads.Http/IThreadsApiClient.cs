using BibleTalkAI.OpenAI.Threads.Models;

namespace BibleTalkAI.OpenAI.Threads.Http;

public interface IThreadsApiClient
{
    ValueTask<Models.Thread?> CreateThread(ThreadCreate request);
}