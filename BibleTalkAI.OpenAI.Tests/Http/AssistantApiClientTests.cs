using BibleTalkAI.OpenAI.Assistants.Http;
using BibleTalkAI.OpenAI.Assistants.Json;
using BibleTalkAI.OpenAI.Assistants.Models;
using BibleTalkAI.OpenAI.Http.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BibleTalkAI.OpenAI.Tests.Http;

public class AssistantApiClientTests
{
    private readonly Mock<IOptionsMonitor<OpenAiApiOptions>> _optionsMonitorMock;
    private readonly OpenAiApiOptions _options;

    public AssistantApiClientTests()
    {
        _options = new OpenAiApiOptions
        {
            ApiKey = "sk-test123",
            BaseAddress = "https://api.openai.com/v1/"
        };

        _optionsMonitorMock = new Mock<IOptionsMonitor<OpenAiApiOptions>>();
        _optionsMonitorMock.Setup(x => x.CurrentValue).Returns(_options);
    }

    [Fact]
    public async Task RetrieveAssistant_ShouldReturnAssistant_WhenSuccessful()
    {
        // Arrange
        var assistantId = "asst_123";
        var expectedAssistant = new Assistant
        {
            Id = assistantId,
            Model = "gpt-4",
            Name = "Test Assistant",
            CreatedAt = 1234567890
        };

        var jsonResponse = JsonSerializer.Serialize(expectedAssistant, AssistantsJsonSerializer.Options);
        var httpClientMock = CreateMockHttpClient(HttpStatusCode.OK, jsonResponse);
        var client = new AssistantApiClient(httpClientMock, _optionsMonitorMock.Object);

        // Act
        var result = await client.RetrieveAssistant(assistantId);

        // Assert
        result.Should().NotBeNull();
        result!.Value.Id.Should().Be(assistantId);
        result.Value.Model.Should().Be("gpt-4");
        result.Value.Name.Should().Be("Test Assistant");
    }

    [Fact]
    public async Task RetrieveAssistant_ShouldReturnDefault_WhenNotFound()
    {
        // Arrange
        var assistantId = "asst_nonexistent";
        var httpClientMock = CreateMockHttpClient(HttpStatusCode.NotFound, "");
        var client = new AssistantApiClient(httpClientMock, _optionsMonitorMock.Object);

        // Act
        var result = await client.RetrieveAssistant(assistantId);

        // Assert
        result.Should().NotBeNull(); // Default value for struct is not null
        result!.Value.Id.Should().BeNullOrEmpty(); // Default struct will have default values
    }

    [Fact]
    public async Task ModifyAssistant_ShouldReturnModifiedAssistant_WhenSuccessful()
    {
        // Arrange
        var assistantId = "asst_123";
        var request = new AssistantModify
        {
            Name = "Modified Assistant",
            Instructions = "New instructions"
        };

        var expectedAssistant = new Assistant
        {
            Id = assistantId,
            Model = "gpt-4",
            Name = "Modified Assistant",
            Instructions = "New instructions",
            CreatedAt = 1234567890
        };

        var jsonResponse = JsonSerializer.Serialize(expectedAssistant, AssistantsJsonSerializer.Options);
        var httpClientMock = CreateMockHttpClient(HttpStatusCode.OK, jsonResponse);
        var client = new AssistantApiClient(httpClientMock, _optionsMonitorMock.Object);

        // Act
        var result = await client.ModifyAssistant(assistantId, request);

        // Assert
        result.Should().NotBeNull();
        result!.Value.Id.Should().Be(assistantId);
        result.Value.Name.Should().Be("Modified Assistant");
        result.Value.Instructions.Should().Be("New instructions");
    }

    [Fact]
    public async Task DeleteAssistant_ShouldComplete_WhenSuccessful()
    {
        // Arrange
        var assistantId = "asst_123";
        var httpClientMock = CreateMockHttpClient(HttpStatusCode.OK, "");
        var client = new AssistantApiClient(httpClientMock, _optionsMonitorMock.Object);

        // Act & Assert
        var action = async () => await client.DeleteAssistant(assistantId);
        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async Task DeleteAssistant_ShouldComplete_WhenNotFound()
    {
        // Arrange
        var assistantId = "asst_nonexistent";
        var httpClientMock = CreateMockHttpClient(HttpStatusCode.NotFound, "");
        var client = new AssistantApiClient(httpClientMock, _optionsMonitorMock.Object);

        // Act & Assert
        var action = async () => await client.DeleteAssistant(assistantId);
        await action.Should().NotThrowAsync(); // The base client may not throw for 404
    }

    private static HttpClient CreateMockHttpClient(HttpStatusCode statusCode, string content)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        return new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://api.openai.com/v1/")
        };
    }
}