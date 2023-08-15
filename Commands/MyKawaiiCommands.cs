using DisCatSharp.ApplicationCommands;
using DisCatSharp.ApplicationCommands.Attributes;
using DisCatSharp.ApplicationCommands.Context;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using KawaiiAPI.NET;
using KawaiiAPI.NET.Enums;


namespace ExampleBot.Commands;

public class MyKawaiiCommands : ApplicationCommandsModule
{
    private readonly KawaiiClient _kawaiiClient;

    public MyKawaiiCommands(KawaiiClient kawaiiClient)
    {
        _kawaiiClient = kawaiiClient;
    }

    [SlashCommand("hug", "Hugs someone")]
    public async Task HugCommand(InteractionContext ctx, [Option("user", "The user to hug")] DiscordUser user)
    {
        // Get a random hug gif from api and save it to imageUrl
        var imageUrl = await _kawaiiClient.GetRandomGifAsync(KawaiiGifType.Hug);

        // Create a new embed with the image url
        var embed = new DiscordEmbedBuilder()
            .WithImageUrl(imageUrl)
            .WithTitle($"{ctx.Member.DisplayName} hugs {user.Username}!")
            .WithColor(DiscordColor.Blurple)
            .WithFooter("Powered by KawaiiAPI.NET");
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
    }

    [SlashCommand("kiss", "Kisses someone")]
    public async Task KissCommand(InteractionContext ctx, [Option("user", "The user to kiss")] DiscordUser user)
    {
        var imageUrl = await _kawaiiClient.GetRandomGifAsync(KawaiiGifType.Kiss);
        var embed = new DiscordEmbedBuilder()
            .WithImageUrl(imageUrl)
            .WithTitle($"{ctx.Member.DisplayName} kisses {user.Username}!")
            .WithColor(DiscordColor.Blurple)
            .WithFooter("Powered by KawaiiAPI.NET");
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
    }

}