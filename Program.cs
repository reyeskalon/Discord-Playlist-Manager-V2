using DiscordPlaylistManagerV2;

PlaylistManager playlistManager = new PlaylistManager();
playlistManager.RetrievePlaylists();

PlaylistManagementMenu playlistManagementMenu= new PlaylistManagementMenu(playlistManager);
SongManagementMenu songManagementMenu= new SongManagementMenu(playlistManager);
MainMenu mainMenu = new MainMenu(playlistManager,playlistManagementMenu, songManagementMenu);

mainMenu.RunMenu();