using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draco.Modules
{
    public class Rate : ModuleBase<SocketCommandContext>
    {
        [Command("rate")]
        public async Task RateAsync([Remainder] string pel)
        {
            Random rnd = new Random();
            var rating = rnd.Next(11);

            await ReplyAsync($"I'd give {pel} a {rating}/10!");

        }
    }
}
