using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public class Program
{
    private CommandService commands;
    private DiscordSocketClient client;
    private IServiceProvider services;
    public Program()
    {
        mainDir = "C:/srab";
        roleFiles = Path.Combine(mainDir, "usersroles");
        userFiles = Path.Combine(mainDir, "users");
    }

    static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

    public async Task Start()
    {
        client = new DiscordSocketClient();
        commands = new CommandService();
        string token = ""; //bot token here
        client.UserJoined += AnnounceJoinedUser; //Check if userjoined

        services = new ServiceCollection()
                .BuildServiceProvider();

 
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        await Task.Delay(-1);
    }

    public async Task AnnounceJoinedUser(SocketGuildUser user) //welcomes New Users
    {
        SocketGuild guid = client.GetGuild(/*guild id*/);
        SocketTextChannel channel = guid.GetChannel(/*channel id*/) as SocketTextChannel;
        await channel.SendMessageAsync("Welcome " + user.Mention);
    }
}
