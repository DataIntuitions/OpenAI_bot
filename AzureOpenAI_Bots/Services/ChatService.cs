using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using AzureOpenAI_Bots.Models;
using OpenAI.Chat;
using static System.Environment;


namespace AzureOpenAI_Bots.Services
{
    public class ChatService
    {


        public async Task<Message> GetMessage(List<Message> messageChain)
        {
            try
            {
                // Retrieve the OpenAI endpoint from environment variables
                string endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");


                // Use DefaultAzureCredential for Entra ID authentication
                var credential = new DefaultAzureCredential();

                // Initialize the AzureOpenAIClient
                var azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);

                // Initialize the ChatClient with the specified deployment name
                ChatClient chatClient = azureClient.GetChatClient("gpt-35-turbo-16k");

                // Create a list of chat messages
                var messages = new List<ChatMessage>
                {
                    new SystemChatMessage("You are an AI assistant that helps people find information."),

                };
                foreach (var message in messageChain)
                {
                    messages.Add(new UserChatMessage(message.Body));
                }

                // Create chat completion options
                var options = new ChatCompletionOptions
                {
                    Temperature = (float)0.7,
                    MaxOutputTokenCount = 800,

                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                };

                // Create the chat completion request
                ChatCompletion completion = await chatClient.CompleteChatAsync(messages, options);

                int x = 0;
              
                return new Message(false, completion.Content.ToString());
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");
                return new Message(true, $"An error occurred: {ex.Message}");
            }
        }


    }

}
