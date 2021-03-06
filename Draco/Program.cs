using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Draco
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private Secret secret = new Secret();
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _service;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _service = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = secret.getToken();

            //bot subscription 
            _client.Log += Log;
            

            await RegisterCommandsSync();
           

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;

        }

        public async Task RegisterCommandsSync()
        {

            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());



        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            
                int argPos = 0;

                if  (message.HasStringPrefix("d/", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
                {
                var context = new SocketCommandContext(_client, message);


                var result = await _commands.ExecuteAsync(context, argPos, _service);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);


                }

                }

                
            


        }
    }
}
