using BibleTalkAI.OpenAI.Tools.Models;
using FluentAssertions;
using System.Collections.Immutable;

namespace BibleTalkAI.OpenAI.Tests.Models;

public class ToolModelsTests
{
    [Fact]
    public void Tool_ShouldSetTypeProperty()
    {
        // Act
        var tool = new Tool
        {
            Type = "function"
        };

        // Assert
        tool.Type.Should().Be("function");
        tool.Function.Should().BeNull();
    }

    [Fact]
    public void Tool_ShouldSetFunctionProperty()
    {
        // Arrange
        var function = new Function
        {
            Name = "test_function",
            Description = "A test function"
        };

        // Act
        var tool = new Tool
        {
            Type = "function",
            Function = function
        };

        // Assert
        tool.Type.Should().Be("function");
        tool.Function.Should().NotBeNull();
        tool.Function!.Value.Name.Should().Be("test_function");
        tool.Function.Value.Description.Should().Be("A test function");
    }

    [Fact]
    public void Function_ShouldSetAllProperties()
    {
        // Arrange
        var parameters = ImmutableArray.Create(
            new FunctionParameter 
            { 
                Type = "object", 
                Properties = new Dictionary<string, object>
                {
                    ["param1"] = new { type = "string", description = "First parameter" },
                    ["param2"] = new { type = "integer", required = true }
                }
            }
        );

        // Act
        var function = new Function
        {
            Name = "my_function",
            Description = "This is my function",
            Parameters = parameters
        };

        // Assert
        function.Name.Should().Be("my_function");
        function.Description.Should().Be("This is my function");
        function.Parameters.Should().NotBeNull();
        function.Parameters!.Value.Should().HaveCount(1);
        function.Parameters.Value[0].Type.Should().Be("object");
        function.Parameters.Value[0].Properties.Should().NotBeNull();
        function.Parameters.Value[0].Properties.Should().HaveCount(2);
    }

    [Fact]
    public void Function_ShouldRequireOnlyName()
    {
        // Act
        var function = new Function
        {
            Name = "minimal_function"
        };

        // Assert
        function.Name.Should().Be("minimal_function");
        function.Description.Should().BeNull();
        function.Parameters.Should().BeNull();
    }

    [Fact]
    public void FunctionParameter_ShouldSetProperties()
    {
        // Arrange
        var properties = new Dictionary<string, object>
        {
            ["name"] = new { type = "string", description = "Parameter name" },
            ["value"] = new { type = "number", required = true }
        };

        // Act
        var parameter = new FunctionParameter
        {
            Type = "object",
            Properties = properties
        };

        // Assert
        parameter.Type.Should().Be("object");
        parameter.Properties.Should().NotBeNull();
        parameter.Properties.Should().HaveCount(2);
        parameter.Properties.Should().ContainKey("name");
        parameter.Properties.Should().ContainKey("value");
    }

    [Fact]
    public void ToolResource_ShouldSetFileSearch()
    {
        // Arrange
        var fileSearch = new FileSearch();

        // Act
        var toolResource = new ToolResource
        {
            FileSearch = fileSearch
        };

        // Assert
        toolResource.FileSearch.Should().NotBeNull();
        toolResource.CodeInterpreter.Should().BeNull();
    }

    [Fact]
    public void ToolResource_ShouldSetCodeInterpreter()
    {
        // Arrange
        var codeInterpreter = new CodeInterpreter();

        // Act
        var toolResource = new ToolResource
        {
            CodeInterpreter = codeInterpreter
        };

        // Assert
        toolResource.CodeInterpreter.Should().NotBeNull();
        toolResource.FileSearch.Should().BeNull();
    }

    [Fact]
    public void ToolResource_ShouldAllowBothProperties()
    {
        // Arrange
        var fileSearch = new FileSearch();
        var codeInterpreter = new CodeInterpreter();

        // Act
        var toolResource = new ToolResource
        {
            FileSearch = fileSearch,
            CodeInterpreter = codeInterpreter
        };

        // Assert
        toolResource.FileSearch.Should().NotBeNull();
        toolResource.CodeInterpreter.Should().NotBeNull();
    }
}