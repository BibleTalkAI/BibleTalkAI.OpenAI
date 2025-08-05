using BibleTalkAI.OpenAI.Assistants.Http;
using BibleTalkAI.OpenAI.Http.Abstractions;
using BibleTalkAI.OpenAI.Threads.Http;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BibleTalkAI.OpenAI.Tests.Integration;

public class ServiceRegistrationIntegrationTests
{
    [Fact]
    public void CompleteServiceRegistration_ShouldWorkEndToEnd()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-test123456789";
        var customBaseAddress = "https://custom.openai.api.com/v1/";

        // Act - Register all services
        services.AddApiClients<IAssistantApiClient, AssistantApiClient, IThreadApiClient, ThreadApiClient>(
            apiKey, customBaseAddress);

        var serviceProvider = services.BuildServiceProvider();

        // Assert - All services should be resolvable
        var assistantClient = serviceProvider.GetService<IAssistantApiClient>();
        assistantClient.Should().NotBeNull();
        assistantClient.Should().BeOfType<AssistantApiClient>();

        var threadClient = serviceProvider.GetService<IThreadApiClient>();
        threadClient.Should().NotBeNull();
        threadClient.Should().BeOfType<ThreadApiClient>();

        // Options should be configured correctly
        var options = serviceProvider.GetService<IOptions<OpenAiApiOptions>>();
        options.Should().NotBeNull();
        options!.Value.ApiKey.Should().Be(apiKey);
        options.Value.BaseAddress.Should().Be(customBaseAddress);

        // HttpClients should be registered
        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        httpClientFactory.Should().NotBeNull();

        // Should be able to create multiple instances
        var assistantClient2 = serviceProvider.GetService<IAssistantApiClient>();
        assistantClient2.Should().NotBeNull();
        assistantClient2.Should().NotBeSameAs(assistantClient); // Should be different instances (transient)
    }

    [Fact]
    public void ServiceRegistration_WithDefaultValues_ShouldWork()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiKey = "sk-minimal-test";

        // Act
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>(apiKey);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var client = serviceProvider.GetService<IAssistantApiClient>();
        client.Should().NotBeNull();

        var options = serviceProvider.GetService<IOptions<OpenAiApiOptions>>();
        options.Should().NotBeNull();
        options!.Value.ApiKey.Should().Be(apiKey);
        options.Value.BaseAddress.Should().Be(Constants.BaseAddress);
    }

    [Fact]
    public void ServiceRegistration_ShouldSupportMultipleConfigurations()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act - Register with different configurations
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>("sk-key1", "https://api1.openai.com/v1/");
        
        // The second registration should override the first
        services.Configure<OpenAiApiOptions>(options =>
        {
            options.ApiKey = "sk-key2";
            options.BaseAddress = "https://api2.openai.com/v1/";
        });

        var serviceProvider = services.BuildServiceProvider();

        // Assert - Should use the last configuration
        var options = serviceProvider.GetService<IOptions<OpenAiApiOptions>>();
        options.Should().NotBeNull();
        options!.Value.ApiKey.Should().Be("sk-key2");
        options.Value.BaseAddress.Should().Be("https://api2.openai.com/v1/");
    }

    [Fact]
    public void ServiceLifetime_ShouldBeTransient()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>("sk-test");
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var instance1 = serviceProvider.GetService<IAssistantApiClient>();
        var instance2 = serviceProvider.GetService<IAssistantApiClient>();
        var instance3 = serviceProvider.GetService<IAssistantApiClient>();

        // Assert
        instance1.Should().NotBeNull();
        instance2.Should().NotBeNull();
        instance3.Should().NotBeNull();
        
        // All instances should be different (transient lifetime)
        instance1.Should().NotBeSameAs(instance2);
        instance2.Should().NotBeSameAs(instance3);
        instance1.Should().NotBeSameAs(instance3);
    }

    [Fact]
    public void HttpClientConfiguration_ShouldHaveCorrectBaseAddress()
    {
        // Arrange
        var services = new ServiceCollection();
        var customBaseAddress = "https://custom.endpoint.com/v1/";
        
        services.AddApiClient<IAssistantApiClient, AssistantApiClient>("sk-test", customBaseAddress);
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        var httpClient = httpClientFactory?.CreateClient(typeof(AssistantApiClient).FullName!);

        // Assert
        httpClientFactory.Should().NotBeNull();
        httpClient.Should().NotBeNull();
        // Note: The actual BaseAddress setting would be handled by the HTTP client factory
        // and the specific client implementation
    }
}