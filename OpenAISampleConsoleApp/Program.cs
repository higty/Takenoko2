using HigLabo.OpenAI;

namespace OpenAISampleConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var apiKey = File.ReadAllText("C:\\Dev\\ChatGPTApiKey.txt");
            var cl = new OpenAIClient(apiKey);

            var imageMessage = new ChatImageMessage(ChatMessageRole.User);
            imageMessage.AddTextContent("この画像について説明してください。");
            imageMessage.AddImageFile(Path.Combine(Environment.CurrentDirectory, "Image", "Castle.jpg"));

            var p = new ChatCompletionsParameter();
            p.Messages.Add(imageMessage);
            p.Model = "gpt-4-vision-preview";
            p.Stream = true;

            await foreach (var text in cl.ChatCompletionsStreamAsync(p))
            {
                Console.Write(text);
            }
        }
        private async ValueTask ChatCompletion()
        {
            var apiKey = File.ReadAllText("C:\\Dev\\ChatGPTApiKey.txt");
            var cl = new OpenAIClient(apiKey);

            var p = new ChatCompletionsParameter();
            p.AddUserMessage("コーヒーの楽しみ方を教えてください。");
            p.Model = "gpt-4-turbo";

            var res = await cl.ChatCompletionsAsync(p);
            foreach (var choice in res.Choices)
            {
                Console.WriteLine(choice.Message.Content);
            }
            Console.WriteLine();
            Console.WriteLine("Total tokens: " + res.Usage.Total_Tokens);
        }
        private async ValueTask ChatCompletionStream()
        {
            var apiKey = File.ReadAllText("C:\\Dev\\ChatGPTApiKey.txt");
            var cl = new OpenAIClient(apiKey);

            var result = new ChatCompletionStreamResult();
            await foreach (var text in cl.ChatCompletionsStreamAsync("コーヒーの楽しみ方を教えてください。", "gpt-4-turbo"
                , result))
            {
                Console.Write(text);
            }
            Console.WriteLine();
            Console.WriteLine("Finish reason: " + result.GetFinishReason());
        }
        private async ValueTask ChatCompletionTool()
        {
            var apiKey = File.ReadAllText("C:\\Dev\\ChatGPTApiKey.txt");
            var cl = new OpenAIClient(apiKey);

            var tool = new ToolObject("function");
            tool.Function = new FunctionObject();
            tool.Function.Name = "getWheather";
            tool.Function.Description = "この関数は指定した場所の天気の情報を取得します。";
            tool.Function.Parameters = new
            {
                type = "object",
                properties = new
                {
                    locationList = new
                    {
                        type = "array",
                        description = "場所の一覧",
                        items = new { type = "string" }
                    }
                }
            };
            var p = new ChatCompletionsParameter();
            p.AddUserMessage("日本のお薦めの観光地を10個あげてください。");
            p.Model = "gpt-3.5-turbo";
            p.Tools = new List<ToolObject>();
            p.Tools.Add(tool);

            var result = new ChatCompletionStreamResult();
            await foreach (var text in cl.ChatCompletionsStreamAsync(p, result, CancellationToken.None))
            {
                Console.Write(text);
            }
            Console.WriteLine();

            foreach (var function in result.GetFunctionCallList())
            {
                Console.WriteLine("Function name: " + function.Name);
                Console.WriteLine("Arguments: " + function.Arguments);
            }
        }
    }
}
