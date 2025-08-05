using BibleTalkAI.OpenAI.Assistants.Json;
using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Tools.Models;
using FluentAssertions;
using System.Collections.Immutable;
using System.Text.Json;

namespace BibleTalkAI.OpenAI.Tests.Json;

public class AssistantsJsonSerializationTests
{
    [Fact]
    public void Assistant_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new Assistant
        {
            Id = "asst_123",
            Model = "gpt-4",
            Name = "Test Assistant",
            Description = "A test assistant for unit tests",
            Instructions = "You are a helpful assistant",
            Tools = ImmutableArray.Create(new Tool { Type = "code_interpreter" }),
            CreatedAt = 1234567890,
            Metadata = ImmutableDictionary.Create<string, string?>()
                .Add("test", "value")
                .Add("null_value", null)
        };

        // Act
        var json = JsonSerializer.Serialize(original, AssistantsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<Assistant>(json, AssistantsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        deserialized.Id.Should().Be(original.Id);
        deserialized.Model.Should().Be(original.Model);
        deserialized.Name.Should().Be(original.Name);
        deserialized.Description.Should().Be(original.Description);
        deserialized.Instructions.Should().Be(original.Instructions);
        deserialized.CreatedAt.Should().Be(original.CreatedAt);
        
        deserialized.Tools.Should().NotBeNull();
        deserialized.Tools!.Value.Should().HaveCount(1);
        deserialized.Tools.Value[0].Type.Should().Be("code_interpreter");
        
        deserialized.Metadata.Should().NotBeNull();
        deserialized.Metadata!.Should().HaveCount(2);
        deserialized.Metadata["test"].Should().Be("value");
        deserialized.Metadata.Should().ContainKey("null_value");
    }

    [Fact]
    public void AssistantCreate_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new AssistantCreate
        {
            Model = "gpt-3.5-turbo",
            Name = "New Assistant",
            Description = "Created via API",
            Instructions = "Be helpful",
            Tools = ImmutableArray.Create(
                new Tool { Type = "function", Function = new Function { Name = "test_func" } }
            ),
            Metadata = ImmutableDictionary.Create<string, string?>()
                .Add("created_by", "unit_test")
        };

        // Act
        var json = JsonSerializer.Serialize(original, AssistantsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<AssistantCreate>(json, AssistantsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        deserialized.Model.Should().Be(original.Model);
        deserialized.Name.Should().Be(original.Name);
        deserialized.Description.Should().Be(original.Description);
        deserialized.Instructions.Should().Be(original.Instructions);
        
        deserialized.Tools.Should().NotBeNull();
        deserialized.Tools!.Value.Should().HaveCount(1);
        deserialized.Tools.Value[0].Type.Should().Be("function");
        deserialized.Tools.Value[0].Function.Should().NotBeNull();
        deserialized.Tools.Value[0].Function!.Value.Name.Should().Be("test_func");
        
        deserialized.Metadata.Should().NotBeNull();
        deserialized.Metadata!["created_by"].Should().Be("unit_test");
    }

    [Fact]
    public void AssistantModify_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new AssistantModify
        {
            Model = "gpt-4-turbo",
            Name = "Updated Assistant",
            Description = "Updated description",
            Instructions = "Updated instructions",
            Tools = ImmutableArray.Create(new Tool { Type = "file_search" })
        };

        // Act
        var json = JsonSerializer.Serialize(original, AssistantsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<AssistantModify>(json, AssistantsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        deserialized.Model.Should().Be(original.Model);
        deserialized.Name.Should().Be(original.Name);
        deserialized.Description.Should().Be(original.Description);
        deserialized.Instructions.Should().Be(original.Instructions);
        
        deserialized.Tools.Should().NotBeNull();
        deserialized.Tools!.Value.Should().HaveCount(1);
        deserialized.Tools.Value[0].Type.Should().Be("file_search");
    }

    [Fact]
    public void AssistantList_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var assistants = ImmutableArray.Create(
            new Assistant { Id = "asst_1", Model = "gpt-4", CreatedAt = 1000 },
            new Assistant { Id = "asst_2", Model = "gpt-3.5-turbo", CreatedAt = 2000 }
        );

        var original = new AssistantList
        {
            Data = assistants,
            FirstId = "asst_1",
            LastId = "asst_2",
            HasMore = true
        };

        // Act
        var json = JsonSerializer.Serialize(original, AssistantsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<AssistantList>(json, AssistantsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        deserialized.Data.Should().HaveCount(2);
        deserialized.Data[0].Id.Should().Be("asst_1");
        deserialized.Data[1].Id.Should().Be("asst_2");
        deserialized.FirstId.Should().Be("asst_1");
        deserialized.LastId.Should().Be("asst_2");
        deserialized.HasMore.Should().BeTrue();
    }

    [Fact]
    public void Assistant_WithMinimalData_ShouldSerializeAndDeserialize()
    {
        // Arrange
        var original = new Assistant
        {
            Id = "asst_minimal",
            Model = "gpt-4",
            CreatedAt = 1234567890
        };

        // Act
        var json = JsonSerializer.Serialize(original, AssistantsJsonSerializer.Options);
        var deserialized = JsonSerializer.Deserialize<Assistant>(json, AssistantsJsonSerializer.Options);

        // Assert
        json.Should().NotBeNullOrEmpty();
        deserialized.Id.Should().Be(original.Id);
        deserialized.Model.Should().Be(original.Model);
        deserialized.CreatedAt.Should().Be(original.CreatedAt);
        deserialized.Name.Should().BeNull();
        deserialized.Description.Should().BeNull();
        deserialized.Instructions.Should().BeNull();
        deserialized.Tools.Should().BeNull();
        deserialized.ToolResources.Should().BeNull();
        deserialized.Metadata.Should().BeNull();
    }
}