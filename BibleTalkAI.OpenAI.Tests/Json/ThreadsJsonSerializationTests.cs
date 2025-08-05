using BibleTalkAI.OpenAI.Threads.Json;
using BibleTalkAI.OpenAI.Threads.Models;
using BibleTalkAI.OpenAI.Tools.Models;
using FluentAssertions;
using System.Collections.Immutable;
using System.Text.Json;
using ThreadModel = BibleTalkAI.OpenAI.Threads.Models.Thread;

namespace BibleTalkAI.OpenAI.Tests.Json;

public class ThreadsJsonSerializationTests
{
    [Fact]
    public void Thread_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new ThreadModel
        {
            Id = "thread_123",
            CreatedAt = 1234567890,
            Tools = ImmutableArray.Create(new Tool { Type = "file_search" }),
            Metadata = ImmutableDictionary.Create<string, string?>()
                .Add("user_id", "user_123")
                .Add("session_type", null)
        };

        // Act
        var json = JsonSerializer.Serialize(original, ThreadsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<ThreadModel>(json, ThreadsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("thread_123");
        json.Should().Contain("created_at");
        
        deserialized.Id.Should().Be(original.Id);
        deserialized.CreatedAt.Should().Be(original.CreatedAt);
        deserialized.Tools.Should().NotBeNull();
        deserialized.Tools!.Value.Should().HaveCount(1);
        deserialized.Tools.Value[0].Type.Should().Be("file_search");
        
        deserialized.Metadata.Should().NotBeNull();
        deserialized.Metadata!.Should().HaveCount(2);
        deserialized.Metadata["user_id"].Should().Be("user_123");
        deserialized.Metadata.Should().ContainKey("session_type");
    }

    [Fact]
    public void ThreadCreate_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var messages = ImmutableArray.Create(new MessageCreate
        {
            Role = "user",
            Content = "Hello from unit test!"
        });

        var original = new ThreadCreate
        {
            Messages = messages,
            Tools = ImmutableArray.Create(new Tool { Type = "code_interpreter" }),
            Metadata = ImmutableDictionary.Create<string, string?>()
                .Add("test_run", "true")
        };

        // Act
        var json = JsonSerializer.Serialize(original, ThreadsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<ThreadCreate>(json, ThreadsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        
        deserialized.Messages.Should().NotBeNull();
        deserialized.Messages!.Value.Should().HaveCount(1);
        deserialized.Messages.Value[0].Role.Should().Be("user");
        deserialized.Messages.Value[0].Content.Should().Be("Hello from unit test!");
        
        deserialized.Tools.Should().NotBeNull();
        deserialized.Tools!.Value.Should().HaveCount(1);
        deserialized.Tools.Value[0].Type.Should().Be("code_interpreter");
        
        deserialized.Metadata.Should().NotBeNull();
        deserialized.Metadata!["test_run"].Should().Be("true");
    }

    [Fact]
    public void ThreadCreate_WithNullProperties_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new ThreadCreate();

        // Act
        var json = JsonSerializer.Serialize(original, ThreadsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<ThreadCreate>(json, ThreadsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        json.Should().Be("{}"); // Should serialize to empty object when all properties are null
        
        deserialized.Messages.Should().BeNull();
        deserialized.Tools.Should().BeNull();
        deserialized.ToolResources.Should().BeNull();
        deserialized.Metadata.Should().BeNull();
    }

    [Fact]
    public void ThreadModify_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new ThreadModify
        {
            Metadata = ImmutableDictionary.Create<string, string?>()
                .Add("modified_at", DateTimeOffset.UtcNow.ToString())
                .Add("version", "2.0")
        };

        // Act
        var json = JsonSerializer.Serialize(original, ThreadsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<ThreadModify>(json, ThreadsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        
        deserialized.Metadata.Should().NotBeNull();
        deserialized.Metadata!.Should().HaveCount(2);
        deserialized.Metadata.Should().ContainKey("modified_at");
        deserialized.Metadata.Should().ContainKey("version");
        deserialized.Metadata["version"].Should().Be("2.0");
    }

    [Fact]
    public void ThreadAndRunCreate_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new ThreadAndRunCreate
        {
            AssistantId = "asst_test_123",
            Thread = new ThreadCreate
            {
                Messages = ImmutableArray.Create(new MessageCreate 
                { 
                    Role = "user", 
                    Content = "Start a new thread and run" 
                })
            },
            Model = "gpt-4",
            Instructions = "Process this thread",
            Metadata = ImmutableDictionary.Create<string, string?>()
                .Add("operation", "thread_and_run")
        };

        // Act
        var json = JsonSerializer.Serialize(original, ThreadsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<ThreadAndRunCreate>(json, ThreadsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        
        deserialized.AssistantId.Should().Be("asst_test_123");
        deserialized.Model.Should().Be("gpt-4");
        deserialized.Instructions.Should().Be("Process this thread");
        
        deserialized.Thread.Should().NotBeNull();
        deserialized.Thread!.Value.Messages.Should().NotBeNull();
        deserialized.Thread.Value.Messages!.Value.Should().HaveCount(1);
        deserialized.Thread.Value.Messages.Value[0].Content.Should().Be("Start a new thread and run");
        
        deserialized.Metadata.Should().NotBeNull();
        deserialized.Metadata!["operation"].Should().Be("thread_and_run");
    }
}