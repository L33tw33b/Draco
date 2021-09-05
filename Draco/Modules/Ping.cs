using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Draco.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {

        [Command("ping")]
        public async Task PingAsync()
        {
            var msg = Context.Message;
            var lat = Context.Client.Latency;

            var msgcreate = DateTime.UtcNow - msg.CreatedAt;

            await ReplyAsync($"Pong: `{Context.Client.Latency }` ms");



        }

    }
}
