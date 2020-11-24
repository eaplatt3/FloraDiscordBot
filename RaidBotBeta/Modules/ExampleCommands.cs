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
    //For commands to be available, and have to Contect passed
    //To them, we must inherit ModuleBase
    public class ExampleCommands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            //Initialize empty string builder for reply
            var sb = new StringBuilder();

            //Get User info from the Context
            var user = Context.User;

            //Build out the reply
            sb.AppendLine($"You are -> [{user.Username}]");
            sb.AppendLine("I must now say, World!");

            //Send simple string replay
            await ReplyAsync(sb.ToString());
        }

        [Command("8ball")]
        [Alias("ask")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task AskEightBall([Remainder] string args = null)
        {
            //I like using StringBuilder to build out the reply
            var sb = new StringBuilder();

            //Lets use an embed for this one!
            var embed = new EmbedBuilder();

            //Now to create a List of Possible Replies
            var replies = new List<string>();

            //Add possible replies
            replies.Add("yes");
            replies.Add("no");
            replies.Add("maybe");
            replies.Add("hazzzzy.......");

            //Time to add some option to the embed(Like Color & Title)
            embed.WithColor(new Color(0, 255, 0));
            embed.Title = "Welcome to the 8-Ball!";

            //We can get lots of information from the Context that is passed into the commands
            //Setting up the preface with the user's name and a coma
            sb.AppendLine($"{Context.User.Username},");
            sb.AppendLine();

            //Let's make sure the supplied question isn't null
            if(args == null)
            {
                //If no question is asked (args are null), reply with the below text
                sb.AppendLine("Sorry, can't answer a question you didn't ask!");
            }

            else
            {
                //If we have a question, let's give an answer!
                //Get a random number to index our list with
                //arrays start at zero so we subtract 1 from the count
                var answer = replies[new Random().Next(replies.Count - 1)];

                //Build out our reply with the StringBuilder
                sb.AppendLine($"You asked: **{args}**...");
                sb.AppendLine();
                sb.AppendLine($"...your answer is **{answer}**");

                //Let's switch out the reply & change the color based on it
                switch (answer)
                {
                    case "yes":
                        {
                            embed.WithColor(new Color(0, 255, 0));
                            break;
                        }
                    case "no":
                        {
                            embed.WithColor(new Color(255, 0, 0));
                            break;
                        }
                    case "maybe":
                        {
                            embed.WithColor(new Color(255, 255, 0));
                            break;
                        }
                    case "hazzzzy.......":
                        {
                            embed.WithColor(new Color(255, 0, 255));
                            break;
                        }
                }
            }

            //Now we can assign the description
            //of the embed to the contents
            //of the StringBuilder we created
            embed.Description = sb.ToString();

            //This will reply with the embed
            await ReplyAsync(null, false, embed.Build());
        }
    }
}
