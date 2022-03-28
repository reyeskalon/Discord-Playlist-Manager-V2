# Discord-Playlist-Manager-V2
Console application for managing playlists that are converted to Auto Hot Key scripts to be used with discord bots

IMPORTANT! The output AHK files will not work without having AutoHotKey installed on your machine https://www.autohotkey.com/
To use the AHK scripts navigate to the directory Discord-Playlist-Manager-V2 is in then go into these files "bin > Debug > net6.0" in there should be the AHK files.
You just need to double click the file and then put your cursor into the text entry field of a discord server and hit your "1" key and it should type and enter all the 
needed text to queue up your playlist.

I created this application to use when wanting to add songs to a discord bots play queue specifically this one https://mee6.xyz/ but it should work with other bots
that play music in a similar fashion. If your bots play command is something other than "/play" you can go into the PlaylistManager class and change the PlayCommand
property to whatever your bot uses. I also have it set to break the AHK scripts into 50 songs per because that is the maximum number of songs the mee6 bot allows to be
queued at a time. This can also be changed by going into the same class file and changing the SongsPerFile property.

This application was created to get around the annoyance of queueing up a bunch of songs manually when wanting to listen to music with friends and also to avoid
queueing up the same few songs over and over again because they're the only ones that come to mind when it's time to enter a song. This application can manage multiple
playlists each with their own unique list of songs.
