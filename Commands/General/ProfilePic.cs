namespace CoonBot.Commands {
    public class ProfilePic : CommandBase {
        [Command("avatar"), Aliases("pfp")]
        [Description("See any user's profile picture.")]
        public async Task AuthorPic(CommandContext msg) {
            if(msg.Member is null) {
				
                return;
			}
            await MemberPic(msg, msg.Member);
        }

        public async Task MemberPic(CommandContext msg, [Description("The username or mention of the user to get the profile picture of.")] DiscordMember member) {
            DiscordEmbedBuilder embed = new();
            embed.ImageUrl = member.AvatarUrl;
            embed.Title = member.Username;
            await msg.Channel.SendMessageAsync(embed.Build());
        }
    }
}
