# Discord PingPong (300 points)

> Our bot was able to infiltrate the server of our nemesis, but we don't know how to get the flag.

This challenge didn't come with a website link, but instead an executable file `discordbot`. When running this file, we get the following output:

```
Successfully connected to Discord API
Last message in #general is: Welcome to out secret server!
Bot is now running. Press CTRL+C to exit.
```

The program at this point will output nothing else and will stay like this until we do as it says and exit it using `CTRL+C`.

Given the program's output and the name of the challenge, we are clearly dealing with a Discord bot that interacts with Discord's API.

Now let us ask ourselves, how is this program able to grab the last message from a channel?

## Grabbing credentials

Every Discord bot that interacts with the API has to send its API key. There are a few ways of getting the key for this challenge, but I did a static analysis of the binary file in order to get this API key.

### Figuring out the language and library

Before doing anything that could be considered overkill, let's first take a good look around the strings in the binary. By running `strings discordbot | grep discord`, we can take a look at what strings inside could be referring to Discord. At the time, I wasn't exactly hoping to find anything using this command, but it did give out something very interesting.

```
...
mod     github.com/dragonsecsi/dctf-chall-discordbot    (devel)
dep     github.com/bwmarrin/discordgo   v0.23.2 h1:BzrtTktixGHIu9Tt7dEE6diysEF9HWnXeHuoJEt2fH4=
github.com/bwmarrin/discordgo.New
github.com/bwmarrin/discordgo.NewState
github.com/bwmarrin/discordgo.NewRatelimiter
github.com/bwmarrin/discordgo.interfaceEventHandler.Type
github.com/bwmarrin/discordgo.interfaceEventHandler.Handle
github.com/bwmarrin/discordgo.registerInterfaceProvider
github.com/bwmarrin/discordgo.(*Session).addEventHandler
github.com/bwmarrin/discordgo.(*Session).AddHandler
...
```

From these strings, we can tell that:

  1. This Discord bot has a repo at github.com/dragonsecsi/dctf-chall-discordbot (sadly this repo is private during and after the competition, so this isn't the way)

  2. The bot is written in Go
  
  3. The bot uses the library [discordgo](https://github.com/bwmarrin/discordgo) to communicate with Discord.

Now that we know what our bot is written in and what it's using to communicating with Discord, we can read up on its documentation and learn how it works.

### RTFM

In one of their examples in the repository for the library, we can see an example bot called "[pingpong](https://github.com/bwmarrin/discordgo/blob/master/examples/pingpong/main.go)". For authentication, we should refer to the following lines in `main.go`.

```go
// Create a new Discord session using the provided bot token.
dg, err := discordgo.New("Bot " + Token)
```

Here we can see that for us to be able to create a new session, the bot's token has to be prepended with the string "`Bot `". If the bot works, then the variable `err` will be nil and the variable `dg` will be a discordgo object. This is exactly what we're looking for in this challenge.

Now all we have to do is look through the strings in the binary, and pull out the API key!

### The API Key

So now that we know which pattern we should go for, we can run the command `strings discordbot | grep "Bot "`. From this, we should be able to pull the flag, right?

```
...Bot CMYKChamDATADashDateEtagFromGOGCGoneGrayHostIENDJulyJuneLEAFLisuMiaoModiNameNewaPINGPOSTRGBAThaiType\u00
...
Bot is now running.  Press CTRL-C to exit.MapIter.Value called on exhausted iteratorPRIORITY frame payload size was %d; want 5PrintableString contains invalid characterTime.MarshalBinary: unexpected zone offsetacquireSudog: found s.elem != nil in cache
...
RetryRequestDiscordBot (https://github.com/bwmarrin/discordgo, v0.23.0)RoundTripper returned a response & error; ignoring responsebufio.Scanner: SplitFunc returns advance count beyond inputhttp2: Transport received Server's graceful shutdown
...
Bot ODM4ODY3MDA4MTI3ODkzNTQ1.YJBVyA.zTQk10ZIQpMfT_Evi00sAkt5KIAcryptobyte: BuilderContinuation reallocated a fixed-size bufferhaven't gotten a heartbeat ACK in %v, triggering a reconnectionhttp2:
...
HTTP request was unauthorized. This could be because the provided token was not a bot token. Please add "Bot " to the start of your token. https://discord.com/developers/docs/reference#authentication-example-bot-token-authorization-header0w
```

While I have made it a bit easier here for us to find the API key, the actual strings dump for this is actually a lot more brutal. This is because in compilation Go squishes all of its strings into giant string arrays. So not only do we see strings that we see in the main program, we also see strings that are in the `discordgo` library and all the other dependencies that the program has.

Either way, the API key we are looking for is somewhere in there, and if you look a bit closely (or if you're a bit smart, you figured out the regex for a bot token) you'll see the following: `Bot ODM4ODY3MDA4MTI3ODkzNTQ1.YJBVyA.zTQk10ZIQpMfT_Evi00sAkt5KIA`

This is our API key, and we shall use it to finally get the flag!

## Exfiltrating the flag

Now that we have the API key that the bot uses to communicate with Discord, we can now spoof ourselves as the bot. We can write our own bot that reads every message from every channel to get the flag, but in this case I got pretty lazy and decided to use [DiscordChatExporter](https://github.com/Tyrrrz/DiscordChatExporter) to simply pull the messages out of every channel that the bot can see.

Upon exporting the messages for the server "`Totally not a server with a flag in it`" in the text channel "`#there-is-no-flag-in-here-move-along`", we can finally read the flag:

```
[04-May-21 04:34 AM] DCTF-ChallUser#4374
dctf{7h3_R0b0t_Upr151ng_15_NEAR!}
```
