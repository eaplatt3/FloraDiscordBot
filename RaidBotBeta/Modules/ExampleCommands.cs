using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace RaidBotBeta.Modules
{
    class ExampleCommands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            //Initialize empty string builder for reply
            var sb = new StringBuilder();

            //Get User info from the Context
            var user = Context.User;

            //Build out the reply
            sb.AppendLine($"You are -> []");
            sb.AppendLine("I must now say, World!");

            //Send simple string replay
            await ReplyAsync(sb.ToString());
        }
    }
}
