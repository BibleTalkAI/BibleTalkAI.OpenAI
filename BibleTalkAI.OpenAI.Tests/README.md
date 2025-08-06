# BibleTalkAI.OpenAI Tests

This project contains comprehensive unit tests for the BibleTalkAI.OpenAI library, providing coverage for all major components and ensuring reliability of the OpenAI API integration.

## Test Structure

### Models Tests (`/Models`)
- **AssistantModelsTests.cs**: Tests for Assistant, AssistantCreate, AssistantModify, and AssistantList models
- **ThreadModelsTests.cs**: Tests for Thread, ThreadCreate, ThreadModify, and ThreadAndRunCreate models  
- **ToolModelsTests.cs**: Tests for Tool, Function, FunctionParameter, and ToolResource models

### HTTP Client Tests (`/Http`)
- **HttpAbstractionsTests.cs**: Tests for OpenAiApiOptions and Constants
- **AssistantApiClientTests.cs**: Tests for AssistantApiClient with mocked HTTP responses

### JSON Serialization Tests (`/Json`)
- **AssistantsJsonSerializationTests.cs**: Tests JSON serialization/deserialization for Assistant models
- **ThreadsJsonSerializationTests.cs**: Tests JSON serialization/deserialization for Thread models

### Service Registration Tests (`/Extensions`)
- **ServiceCollectionExtensionsTests.cs**: Tests for dependency injection service registration extensions

### Integration Tests (`/Integration`)
- **ServiceRegistrationIntegrationTests.cs**: End-to-end tests for complete service registration and resolution

## Test Coverage

The test suite covers:

✅ **Models**: All data models with property validation and edge cases  
✅ **HTTP Clients**: API client behavior with mocked HTTP responses  
✅ **JSON Serialization**: Round-trip serialization testing  
✅ **Service Registration**: Dependency injection and configuration  
✅ **Integration**: End-to-end service resolution and configuration  

## Technologies Used

- **xUnit**: Primary testing framework
- **FluentAssertions**: Readable assertions and better error messages
- **Moq**: Mocking framework for HTTP responses
- **Microsoft.Extensions.DependencyInjection**: Testing service registration

## Running Tests

Run all tests:
```bash
dotnet test
```

Run with detailed output:
```bash
dotnet test --verbosity normal
```

Run specific test class:
```bash
dotnet test --filter "ClassName=AssistantModelsTests"
```

## Test Statistics

- **Total Tests**: 51
- **Test Files**: 7
- **Coverage Areas**: Models, HTTP, JSON, Extensions, Integration
- **Status**: All tests passing ✅

## Notes

- Tests use mock HTTP responses to avoid external dependencies
- JSON serialization tests validate round-trip accuracy
- Service registration tests ensure proper dependency injection configuration
- Integration tests verify end-to-end functionality