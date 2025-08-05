using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Tools.Models;
using FluentAssertions;
using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Tests.Models;

public class AssistantModelsTests
{
    [Fact]
    public void Assistant_ShouldSetAllProperties()
    {
        // Arrange
        var tools = ImmutableArray.Create(new Tool { Type = "function" });
        var metadata = ImmutableDictionary.Create<string, string?>()
            .Add("key1", "value1")
            .Add("key2", null);

        // Act
        var assistant = new Assistant
        {
            Id = "asst_123",
            Model = "gpt-4",
            Name = "Test Assistant",
            Description = "A test assistant",
            Instructions = "You are a helpful assistant",
            Tools = tools,
            CreatedAt = 1234567890,
            Metadata = metadata
        };

        // Assert
        assistant.Id.Should().Be("asst_123");
        assistant.Model.Should().Be("gpt-4");
        assistant.Name.Should().Be("Test Assistant");
        assistant.Description.Should().Be("A test assistant");
        assistant.Instructions.Should().Be("You are a helpful assistant");
        assistant.Tools.Should().NotBeNull();
        assistant.Tools!.Value.Should().HaveCount(1);
        assistant.CreatedAt.Should().Be(1234567890);
        assistant.Metadata.Should().NotBeNull();
        assistant.Metadata!.Should().HaveCount(2);
        assistant.Metadata["key1"].Should().Be("value1");
        assistant.Metadata["key2"].Should().BeNull();
    }

    [Fact]
    public void Assistant_ShouldAllowNullableProperties()
    {
        // Act
        var assistant = new Assistant
        {
            Id = "asst_123",
            Model = "gpt-4",
            CreatedAt = 1234567890
        };

        // Assert
        assistant.Id.Should().Be("asst_123");
        assistant.Model.Should().Be("gpt-4");
        assistant.Name.Should().BeNull();
        assistant.Description.Should().BeNull();
        assistant.Instructions.Should().BeNull();
        assistant.Tools.Should().BeNull();
        assistant.ToolResources.Should().BeNull();
        assistant.Metadata.Should().BeNull();
    }

    [Fact]
    public void AssistantCreate_ShouldSetAllProperties()
    {
        // Arrange
        var tools = ImmutableArray.Create(new Tool { Type = "code_interpreter" });
        var metadata = ImmutableDictionary.Create<string, string?>()
            .Add("purpose", "testing");

        // Act
        var assistantCreate = new AssistantCreate
        {
            Model = "gpt-4",
            Name = "New Assistant",
            Description = "A newly created assistant",
            Instructions = "Follow these instructions",
            Tools = tools,
            Metadata = metadata
        };

        // Assert
        assistantCreate.Model.Should().Be("gpt-4");
        assistantCreate.Name.Should().Be("New Assistant");
        assistantCreate.Description.Should().Be("A newly created assistant");
        assistantCreate.Instructions.Should().Be("Follow these instructions");
        assistantCreate.Tools.Should().NotBeNull();
        assistantCreate.Tools!.Value.Should().HaveCount(1);
        assistantCreate.Metadata.Should().NotBeNull();
        assistantCreate.Metadata!.Should().ContainKey("purpose");
    }

    [Fact]
    public void AssistantCreate_ShouldRequireOnlyModel()
    {
        // Act
        var assistantCreate = new AssistantCreate
        {
            Model = "gpt-3.5-turbo"
        };

        // Assert
        assistantCreate.Model.Should().Be("gpt-3.5-turbo");
        assistantCreate.Name.Should().BeNull();
        assistantCreate.Description.Should().BeNull();
        assistantCreate.Instructions.Should().BeNull();
        assistantCreate.Tools.Should().BeNull();
        assistantCreate.ToolResources.Should().BeNull();
        assistantCreate.Metadata.Should().BeNull();
    }

    [Fact]
    public void AssistantModify_ShouldSetAllProperties()
    {
        // Arrange
        var tools = ImmutableArray.Create(new Tool { Type = "file_search" });

        // Act
        var assistantModify = new AssistantModify
        {
            Model = "gpt-4-turbo",
            Name = "Modified Assistant",
            Description = "An updated assistant",
            Instructions = "Updated instructions",
            Tools = tools
        };

        // Assert
        assistantModify.Model.Should().Be("gpt-4-turbo");
        assistantModify.Name.Should().Be("Modified Assistant");
        assistantModify.Description.Should().Be("An updated assistant");
        assistantModify.Instructions.Should().Be("Updated instructions");
        assistantModify.Tools.Should().NotBeNull();
        assistantModify.Tools!.Value.Should().HaveCount(1);
        assistantModify.Tools.Value[0].Type.Should().Be("file_search");
    }

    [Fact]
    public void AssistantList_ShouldSetProperties()
    {
        // Arrange
        var assistants = ImmutableArray.Create(
            new Assistant { Id = "asst_1", Model = "gpt-4", CreatedAt = 1000 },
            new Assistant { Id = "asst_2", Model = "gpt-3.5-turbo", CreatedAt = 2000 }
        );

        // Act
        var assistantList = new AssistantList
        {
            Data = assistants,
            FirstId = "asst_1",
            LastId = "asst_2",
            HasMore = false
        };

        // Assert
        assistantList.Data.Should().HaveCount(2);
        assistantList.FirstId.Should().Be("asst_1");
        assistantList.LastId.Should().Be("asst_2");
        assistantList.HasMore.Should().BeFalse();
    }
}