using BibleTalkAI.OpenAI.Http.Abstractions;
using BibleTalkAI.OpenAI.Threads.Json;
using BibleTalkAI.OpenAI.Threads.Models;
using Microsoft.Extensions.Options;
using Thread = BibleTalkAI.OpenAI.Threads.Models.Thread;

namespace BibleTalkAI.OpenAI.Threads.Http;

public class ThreadApiClient(
    HttpClient httpClient,
    IOptionsMonitor<OpenAiApiOptions> options)
    : DefaultApiClient(
        httpClient,
        ThreadsJsonSerializer.Options,
        options,
        "threads/"),
    IThreadApiClient
{
    /// <summary>get https://api.openai.com/v1/threads/{thread_id}</summary>
    public async ValueTask<Thread?> RetrieveThread(string id)
        => await Get<Thread>(id);

    /// <summary>post https://api.openai.com/v1/threads/{thread_id}</summary>
    public async ValueTask<Thread?> ModifyThread(string id, ThreadModify request)
        => await Post<ThreadModify, Thread?>(id, request);

    /// <summary>delete https://api.openai.com/v1/threads/{thread_id}</summary>
    public async ValueTask DeleteThread(string id)
        => await Delete(id);

    /// <summary>post https://api.openai.com/v1/threads/{thread_id}/messages</summary>
    public async ValueTask<Message?> CreateMessage(string id, MessageCreate request)
        => await Post<MessageCreate, Message?>(UriMessages(id), request);

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/messages/{message_id}</summary>
    public async ValueTask<Message?> RetrieveMessage(string id, string messageId)
        => await Get<Message>(UriMessageId(id, messageId));

    /// <summary>post https://api.openai.com/v1/threads/{thread_id}/messages/{message_id}</summary>
    public async ValueTask<Message?> ModifyMessage(string id, string messageId, MessageModify request)
        => await Post<MessageModify, Message?>(UriMessageId(id, messageId), request);

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/messages</summary>
    public async ValueTask<MessageList> ListMessages(
        string id,
        ListParameters query)
        => await Get<MessageList>(query.ToUri(id, "/messages"));

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/messages/{message_id}/files/{file_id}</summary>
    public async ValueTask<MessageFile?> RetrieveMessageFile(string id, string messageId, string fileId)
        => await Get<MessageFile>(UriFileId(id, messageId, fileId));

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/messages</summary>
    public async ValueTask<MessageFileList> ListMessageFiles(
        string id,
        string messageId,
        ListParameters query)
        => await Get<MessageFileList>(query.ToUri(id, "/messages/", messageId, "/files"));

    /// <summary>post https://api.openai.com/v1/threads/{thread_id}/runs</summary>
    public async ValueTask<Run?> CreateRun(string id, RunCreate request)
        => await Post<RunCreate, Run?>(UriRuns(id), request);

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/runs/{run_id}</summary>
    public async ValueTask<Run?> RetrieveRun(string id, string runId)
        => await Get<Run>(UriRunId(id, runId));

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/runs/{run_id}</summary>
    public async ValueTask<Run?> ModifyRun(string id, string runId, RunModify request)
        => await Post<RunModify, Run?>(UriRunId(id, runId), request);

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/runs</summary>
    public async ValueTask<RunList> ListRuns(
        string id,
        ListParameters query)
        => await Get<RunList>(query.ToUri(id, "/runs"));

    /// <summary>post https://api.openai.com/v1/threads/{thread_id}/runs/{run_id}/submit_tool_outputs</summary>
    public async ValueTask<Run?> SubmitToolOutputs(string id, string runId, SubmitToolOutputs request)
        => await Post<SubmitToolOutputs, Run?>(UriRunIdSubmitToolOutputs(id, runId), request);

    /// <summary>post https://api.openai.com/v1/threads/{thread_id}/runs/{run_id}/cancel</summary>
    public async ValueTask<Run?> CancelRun(string id, string runId)
        => await Post<Run?>(UriRunIdCancel(id, runId));

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/runs/{run_id}/steps/{step_id}</summary>
    public async ValueTask<RunStep?> RetrieveRunStep(string id, string runId, string stepId)
        => await Get<RunStep>(UriRunIdStepId(id, runId, stepId));

    /// <summary>get https://api.openai.com/v1/threads/{thread_id}/runs/{run_id}/steps</summary>
    public async ValueTask<RunStepList> ListRunSteps(
        string id,
        string runId,
        ListParameters query)
        => await Get<RunStepList>(query.ToUri(id, "/runs/", runId, "/steps"));

    /// <summary>post https://api.openai.com/v1/threads/runs</summary>
    public async ValueTask<Run?> CreateThreadAndRun(ThreadAndRunCreate request)
        => await Post<ThreadAndRunCreate, Run?>(_uriRuns, request);

    private static readonly Uri _uriRuns = new("runs", UriKind.Relative);

    private static Uri UriMessages(string id)
        => new(id + "/messages", UriKind.Relative);

    private static Uri UriMessageId(string id, string messageId)
        => new(id + "/messages/" + messageId, UriKind.Relative);

    private static Uri UriFileId(string id, string messageId, string fileId)
        => new(id + "/messages/" + messageId + "/files/" + fileId, UriKind.Relative);

    private static Uri UriRuns(string id)
        => new(id + "/runs", UriKind.Relative);

    private static Uri UriRunId(string id, string runId)
        => new(id + "/runs/" + runId, UriKind.Relative);

    private static Uri UriRunIdSubmitToolOutputs(string id, string runId)
        => new(id + "/runs/" + runId + "/submit_tool_outputs", UriKind.Relative);

    private static Uri UriRunIdCancel(string id, string runId)
        => new(id + "/runs/" + runId + "/cancel", UriKind.Relative);

    private static Uri UriRunIdStepId(string id, string runId, string stepId)
        => new(id + "/runs/" + runId + "/steps/" + stepId, UriKind.Relative);
}
