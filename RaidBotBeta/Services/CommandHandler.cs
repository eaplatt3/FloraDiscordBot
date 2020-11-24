using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;

namespace RaidBotBeta.Services
{
    class CommandHandler
    {
        //Setup Field to be set later in the constructor
        private readonly IConfiguration config;
        private readonly CommandService commands;
        private readonly DiscordSocketClient client;
        private readonly IServiceProvider _services;

        public CommandHandler(IServiceProvider services)
        {
            //Juice up the fields with these services
            //Since we passed the servicesin, we can use GetRequiredService
            //To pass them into the fields set earlier

            config = services.GetRequiredService<IConfiguration>();
            commands = services.GetRequiredService<CommandService>();
            client = services.GetRequiredService<DiscordSocketClient>();
            _services = services;

            //Take action when we excute a command
            commands.CommandExecuted += CommandExcutedAsync;

            //Take action when we receive a message (so we can process it, and see if it is a vaild command)
            client.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAysnc()
        {
            //Register modules that are public and inherit ModuleBase<T>
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        //This method is where the magic happens, and takes actions upon receiving messages
        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            //Ensure we dont't process system/other bot messages
            if(!(rawMessage is SocketUserMessage message))
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            //Sets the argument position away from the prefix we set
            var argPos = 0;

            //Gets the prefix from the config file
            char prefix = Char.Parse(config["Prefix"]);

            //Determine if the message has a vaild prefix, and adjust argPos based on prefix
            if(!(message.HasMentionPrefix(client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos)))
            {
                return;
            }
        }


    }
}
