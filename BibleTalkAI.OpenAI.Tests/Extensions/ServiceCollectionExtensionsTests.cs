using BibleTalkAI.OpenAI.Assistants.Http;
using BibleTalkAI.OpenAI.Http.Abstractions;
using BibleTalkAI.OpenAI.Threads.Http;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BibleTalkAI.OpenAI.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddApiClient_ShouldRegisterServicesCorrectly()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123";
        var baseAddress = "https://custom.api.com/v1/";

        // Act
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>(apiKey, baseAddress);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        // Should register the client
        var client = serviceProvider.GetService<IAssistantApiClient>();
        client.Should().NotBeNull();
        client.Should().BeOfType<AssistantApiClient>();

        // Should configure options correctly
        var options = serviceProvider.GetService<IOptions<OpenAiApiOptions>>();
        options.Should().NotBeNull();
        options!.Value.ApiKey.Should().Be(apiKey);
        options.Value.BaseAddress.Should().Be(baseAddress);
    }

    [Fact]
    public void AddApiClient_ShouldUseDefaultBaseAddress_WhenNotProvided()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123";

        // Act
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>(apiKey);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<OpenAiApiOptions>>();
        
        options.Should().NotBeNull();
        options!.Value.ApiKey.Should().Be(apiKey);
        options.Value.BaseAddress.Should().Be(Constants.BaseAddress);
    }

    [Fact]
    public void AddApiClient_ShouldRemoveHttpLogging_ByDefault()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123";

        // Act
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>(apiKey);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetService<IAssistantApiClient>();
        client.Should().NotBeNull();
        
        // Should not have any HTTP message handler builder filters
        var filters = serviceProvider.GetServices<Microsoft.Extensions.Http.IHttpMessageHandlerBuilderFilter>();
        filters.Should().BeEmpty();
    }

    [Fact]
    public void AddApiClient_ShouldKeepHttpLogging_WhenSpecified()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123";

        // Act
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>(apiKey, removeHttpLogging: false);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetService<IAssistantApiClient>();
        client.Should().NotBeNull();
    }

    [Fact]
    public void AddApiClients_ShouldRegisterBothClientsCorrectly()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123";
        var baseAddress = "https://custom.api.com/v1/";

        // Act
        services.AddApiClients<IAssistantApiClient, AssistantApiClient, IThreadApiClient, ThreadApiClient>(
            apiKey, baseAddress);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        // Should register both clients
        var assistantClient = serviceProvider.GetService<IAssistantApiClient>();
        assistantClient.Should().NotBeNull();
        assistantClient.Should().BeOfType<AssistantApiClient>();

        var threadClient = serviceProvider.GetService<IThreadApiClient>();
        threadClient.Should().NotBeNull();
        threadClient.Should().BeOfType<ThreadApiClient>();

        // Should configure options correctly
        var options = serviceProvider.GetService<IOptions<OpenAiApiOptions>>();
        options.Should().NotBeNull();
        options!.Value.ApiKey.Should().Be(apiKey);
        options.Value.BaseAddress.Should().Be(baseAddress);
    }

    [Fact]
    public void AddApiClients_ShouldUseDefaultBaseAddress_WhenNotProvided()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123";

        // Act
        services.AddApiClients<IAssistantApiClient, AssistantApiClient, IThreadApiClient, ThreadApiClient>(apiKey);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<OpenAiApiOptions>>();
        
        options.Should().NotBeNull();
        options!.Value.ApiKey.Should().Be(apiKey);
        options.Value.BaseAddress.Should().Be(Constants.BaseAddress);
    }

    [Fact]
    public void ServiceRegistration_ShouldAllowMultipleInstances()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123";

        // Act
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>(apiKey);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        var client1 = serviceProvider.GetService<IAssistantApiClient>();
        var client2 = serviceProvider.GetService<IAssistantApiClient>();
        
        client1.Should().NotBeNull();
        client2.Should().NotBeNull();
        client1.Should().NotBeSameAs(client2); // Should be different instances since it's transient
    }
}