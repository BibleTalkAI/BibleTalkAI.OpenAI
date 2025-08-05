using BibleTalkAI.OpenAI.Threads.Models;
using BibleTalkAI.OpenAI.Tools.Models;
using FluentAssertions;
using System.Collections.Immutable;
using ThreadModel = BibleTalkAI.OpenAI.Threads.Models.Thread;

namespace BibleTalkAI.OpenAI.Tests.Models;

public class ThreadModelsTests
{
    [Fact]
    public void Thread_ShouldSetAllProperties()
    {
        // Arrange
        var tools = ImmutableArray.Create(new Tool { Type = "code_interpreter" });
        var metadata = ImmutableDictionary.Create<string, string?>()
            .Add("user_id", "user_123")
            .Add("session_id", "session_456");

        // Act
        var thread = new ThreadModel
        {
            Id = "thread_123",
            CreatedAt = 1234567890,
            Tools = tools,
            Metadata = metadata
        };

        // Assert
        thread.Id.Should().Be("thread_123");
        thread.CreatedAt.Should().Be(1234567890);
        thread.Tools.Should().NotBeNull();
        thread.Tools!.Value.Should().HaveCount(1);
        thread.Tools.Value[0].Type.Should().Be("code_interpreter");
        thread.Metadata.Should().NotBeNull();
        thread.Metadata!.Should().HaveCount(2);
        thread.Metadata["user_id"].Should().Be("user_123");
        thread.Metadata["session_id"].Should().Be("session_456");
    }

    [Fact]
    public void Thread_ShouldAllowNullableProperties()
    {
        // Act
        var thread = new ThreadModel
        {
            Id = "thread_123",
            CreatedAt = 1234567890
        };

        // Assert
        thread.Id.Should().Be("thread_123");
        thread.CreatedAt.Should().Be(1234567890);
        thread.Tools.Should().BeNull();
        thread.ToolResources.Should().BeNull();
        thread.Metadata.Should().BeNull();
    }

    [Fact]
    public void ThreadCreate_ShouldSetAllProperties()
    {
        // Arrange
        var messages = ImmutableArray.Create(new MessageCreate
        {
            Role = "user",
            Content = "Hello, world!"
        });
        var tools = ImmutableArray.Create(new Tool { Type = "file_search" });
        var metadata = ImmutableDictionary.Create<string, string?>()
            .Add("purpose", "testing");

        // Act
        var threadCreate = new ThreadCreate
        {
            Messages = messages,
            Tools = tools,
            Metadata = metadata
        };

        // Assert
        threadCreate.Messages.Should().NotBeNull();
        threadCreate.Messages!.Value.Should().HaveCount(1);
        threadCreate.Messages.Value[0].Role.Should().Be("user");
        threadCreate.Messages.Value[0].Content.Should().Be("Hello, world!");
        threadCreate.Tools.Should().NotBeNull();
        threadCreate.Tools!.Value.Should().HaveCount(1);
        threadCreate.Metadata.Should().NotBeNull();
        threadCreate.Metadata!.Should().ContainKey("purpose");
    }

    [Fact]
    public void ThreadCreate_ShouldAllowAllNullableProperties()
    {
        // Act
        var threadCreate = new ThreadCreate();

        // Assert
        threadCreate.Messages.Should().BeNull();
        threadCreate.Tools.Should().BeNull();
        threadCreate.ToolResources.Should().BeNull();
        threadCreate.Metadata.Should().BeNull();
    }

    [Fact]
    public void ThreadModify_ShouldSetProperties()
    {
        // Arrange
        var metadata = ImmutableDictionary.Create<string, string?>()
            .Add("modified", "true");

        // Act
        var threadModify = new ThreadModify
        {
            Metadata = metadata
        };

        // Assert
        threadModify.Metadata.Should().NotBeNull();
        threadModify.Metadata!.Should().ContainKey("modified");
        threadModify.Metadata["modified"].Should().Be("true");
    }

    [Fact]
    public void ThreadAndRunCreate_ShouldSetProperties()
    {
        // Arrange
        var thread = new ThreadCreate();
        var metadata = ImmutableDictionary.Create<string, string?>()
            .Add("run_purpose", "analysis");

        // Act
        var threadAndRunCreate = new ThreadAndRunCreate
        {
            AssistantId = "asst_123",
            Thread = thread,
            Model = "gpt-4",
            Instructions = "Analyze this data",
            Metadata = metadata
        };

        // Assert
        threadAndRunCreate.AssistantId.Should().Be("asst_123");
        threadAndRunCreate.Thread.Should().NotBeNull();
        threadAndRunCreate.Model.Should().Be("gpt-4");
        threadAndRunCreate.Instructions.Should().Be("Analyze this data");
        threadAndRunCreate.Metadata.Should().NotBeNull();
        threadAndRunCreate.Metadata!.Should().ContainKey("run_purpose");
    }
}