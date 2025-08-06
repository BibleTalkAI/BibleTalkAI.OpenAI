using BibleTalkAI.OpenAI.Http.Abstractions;
using FluentAssertions;

namespace BibleTalkAI.OpenAI.Tests.Http;

public class OpenAiApiOptionsTests
{
    [Fact]
    public void OpenAiApiOptions_ShouldSetApiKey()
    {
        // Act
        var options = new OpenAiApiOptions
        {
            ApiKey = "sk-test123"
        };

        // Assert
        options.ApiKey.Should().Be("sk-test123");
    }

    [Fact]
    public void OpenAiApiOptions_ShouldSetCustomBaseAddress()
    {
        // Act
        var options = new OpenAiApiOptions
        {
            BaseAddress = "https://custom.openai.com/v1/"
        };

        // Assert
        options.BaseAddress.Should().Be("https://custom.openai.com/v1/");
    }

    [Fact]
    public void OpenAiApiOptions_ShouldAllowNullProperties()
    {
        // Act
        var options = new OpenAiApiOptions();

        // Assert
        options.ApiKey.Should().BeNull();
        options.BaseAddress.Should().BeNull();
    }
}

public class ConstantsTests
{
    [Fact]
    public void Constants_BaseAddress_ShouldBeCorrectValue()
    {
        // Assert
        Constants.BaseAddress.Should().Be("https://api.openai.com/v1/");
    }
}