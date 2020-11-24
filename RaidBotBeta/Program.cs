using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


/// <summary>
/// Earl Platt III
/// RaidBotBeta v.0.5
/// Discord Bot to Schedule Raids 
/// </summary>

namespace RaidBotBeta
{
    class Program
    {
        private readonly DiscordSocketClient client;
        private readonly IConfiguration config;

        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            client = new DiscordSocketClient();

            //Hook into log event and write it out to the console
            client.Log += LogAsync;

            //Hook into the client ready event
            client.Ready += ReadyAsync;

            //hook into the message received event, this is how we handle the hello world example
            client.MessageReceived += MessageReceivedAsync;

            //create the configuration
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");
            config = builder.Build();
        }

        public async Task MainAsync()
        {
            //This is where we get the token value from the configuration file
            await client.LoginAsync(TokenType.Bot, config["Token"]);
            await client.StartAsync();

            //Block the Program until its is Closed
            await Task.Delay(-1);
        }

        //Method to Print Log Data to the console
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [{client.CurrentUser}]:)");
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            //This ensures we dont't loop things by responding to Ourselves (as the bot)
            if(message.Author.Id == client.CurrentUser.Id)
            {
                return;
            }

            if(message.Content == ".hello")
            {
                await message.Channel.SendMessageAsync("world!");
            }
        }
    }
}
