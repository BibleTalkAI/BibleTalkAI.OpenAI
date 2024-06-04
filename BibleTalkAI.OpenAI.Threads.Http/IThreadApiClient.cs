using BibleTalkAI.OpenAI.Http.Abstractions;
using BibleTalkAI.OpenAI.Threads.Models;

namespace BibleTalkAI.OpenAI.Threads.Http;

public interface IThreadApiClient
{
    ValueTask<Run?> CancelRun(string id, string runId);
    ValueTask<Message?> CreateMessage(string id, MessageCreate request);
    ValueTask<Run?> CreateRun(string id, RunCreate request);
    ValueTask<Run?> CreateThreadAndRun(ThreadAndRunCreate request);
    ValueTask DeleteThread(string id);
    ValueTask<MessageList> ListMessages(string id, ListParameters query);
    ValueTask<RunList> ListRuns(string id, ListParameters query);
    ValueTask<RunStepList> ListRunSteps(string id, string runId, ListParameters query);
    ValueTask<Message?> ModifyMessage(string id, string messageId, MessageModify request);
    ValueTask<Run?> ModifyRun(string id, string runId, RunModify request);
    ValueTask<Models.Thread?> ModifyThread(string id, ThreadModify request);
    ValueTask<Message?> RetrieveMessage(string id, string messageId);
    ValueTask<Run?> RetrieveRun(string id, string runId);
    ValueTask<RunStep?> RetrieveRunStep(string id, string runId, string stepId);
    ValueTask<Models.Thread?> RetrieveThread(string id);
    ValueTask<Run?> SubmitToolOutputs(string id, string runId, SubmitToolOutputs request);
}
