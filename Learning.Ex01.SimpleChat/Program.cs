using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;
var model = "gpt-4o-mini";

var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(model, apiKey)
    .Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

var chatHistory = new ChatHistory();
chatHistory.AddSystemMessage("You are a helpful assistant.");

bool stillChatting = true;
while (true)
{
    Console.Write("User: ");
    string? userInput = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userInput) || userInput.ToLower() == "exit")
        break;

    chatHistory.AddUserMessage(userInput);

    var response = await chatCompletionService.GetChatMessageContentAsync(chatHistory);

    Console.Write("Assistant: ");
    Console.WriteLine(response);

    chatHistory.AddAssistantMessage(response.ToString());
}

Console.WriteLine("Chat ended.");