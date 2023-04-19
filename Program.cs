// This console application is working like a REPL, it reads the input from the console and give an answer.
// It simulates being an SQL Server, where you can write SQL queries and get the result, by using Azure OpenAI Service to get the answer.

using Azure.AI.OpenAI;
using Azure;
using Spectre.Console;

// Check if

OpenAIClient client = new OpenAIClient(
    new Uri(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_ENDPOINT")),
    new AzureKeyCredential(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")));


var promptHistory = new List<string>();
promptHistory.Add("Imagine you are a Microsoft sql server. I type commands and you reply with the results, and no other information or description. Just the result.");

// Main REPL loop
while (true)
{
    // Read the input from the console
    AnsiConsole.Write(new Markup("[bold blue]SQL>[/]"));
    string input = Console.ReadLine();
    
    // If the input is empty, then continue
    if (string.IsNullOrEmpty(input))
    {
        continue;
    }
    // If the input is "exit", then break the loop
    if (input.ToLower() == "exit")
    {
        break;
    }
    await AnsiConsole.Status()
    .StartAsync("Executing...", async ctx =>
    {
        // Get the answer from the OpenAI API
        string answer = await GetAnswer(input);
        // Print the answer
        AnsiConsole.WriteLine(answer);
    });
    
}

// Get the answer from the OpenAI API
async Task<string> GetAnswer(string input)
{
    // Add the input to the prompt history
    promptHistory.Add(input);
    // Create the prompt
    string prompt = string.Join(Environment.NewLine, promptHistory);
    // Create the request options
    Response<Completions> completionsResponse = await client.GetCompletionsAsync(
    deploymentOrModelName: "davinci3",
    new CompletionsOptions()
    {
        Prompts = { prompt },
        Temperature = (float)0,
        MaxTokens = 975,
        NucleusSamplingFactor = (float)0.5,
        FrequencyPenalty = (float)0,
        PresencePenalty = (float)0,
        GenerationSampleCount = 1,
    });
    Completions completions = completionsResponse.Value;
    // Get the first completion
    var result = completions.Choices.First().Text.Trim();
    promptHistory.Add(result);
    return result;
}
