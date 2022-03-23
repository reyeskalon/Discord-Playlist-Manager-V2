using DiscordPlaylistManagerV2;

string filePath = "C:\\Users\\reyes\\Documents\\DiscordPlaylistManagerList.txt";
PlaylistManager playlistManager = new PlaylistManager(filePath);
playlistManager.RetrievePlaylists();
PlaylistManagementMenu playlistManagementMenu= new PlaylistManagementMenu(playlistManager);
SongManagementMenu songManagementMenu= new SongManagementMenu(playlistManager);
MainMenu mainMenu = new MainMenu(playlistManager,playlistManagementMenu, songManagementMenu);
mainMenu.RunMenu();