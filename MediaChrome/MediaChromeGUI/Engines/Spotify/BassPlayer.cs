using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using MediaChrome;
using RestSharp;
using System.Web.Script.Serialization;
using MediaChrome.Models;
using System.Drawing;

namespace MediaChrome.Engines.Spotify
{
    public class SpotifySession
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public long issued { get; set; }
        public bool IsValid
        {
            get
            {
                return issued + (expires_in * 1000) > DateTime.Now.Ticks;
            }
        }
    }
    public class SpotifyPlayer : IPlayEngine
    {
        public string Copyright { get => "Buddhalow"; set { } }
        public string Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Company { get => "Buddhalow"; set => throw new NotImplementedException(); }
        public Uri CompanyWebSite { get => new Uri("http://buddhalow.se"); set => throw new NotImplementedException(); }
        public Uri ServiceUri { get => new Uri("spotify:"); set => throw new NotImplementedException(); }
        public SpotifySession Session { get; set; }
        public RestClient CreateClient()
        {
            if (Session == null)
                return null;
            RestClient rc = new RestClient("https://api.spotify.com/v1");
            rc.AddDefaultHeader("Authorization", "Bearer " + Session.access_token);
            return rc;
        }
        public bool LoggedIn
        {
            get
            {
                try
                {
                    string json = (string)Properties.Settings.Default["spotify_session"];
                    JavaScriptSerializer sj = new JavaScriptSerializer();
                    Engines.Spotify.SpotifySession session = sj.Deserialize<Engines.Spotify.SpotifySession>(json);
                    if (session != null && session.IsValid)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
                return false;

            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public bool Streaming => true;

        public bool DownloadStore => false;

        public List<Track> Purchases => new List<Track>();

        public string Image => "";

        public Image Icon => null;

        public bool hasPlaylists => true;

        public string Status { get => "Idle"; set { } }

        public Control MediaControl => new Control();

        public bool Ready { get; set; }

        public double Duration => 0;

        public int FilesCompleted { get => 0; set { } }
        public bool PlaylistsLoaded { get => false; set { } }

        public string Namespace => "spotify";

        public string Title => "Spotify;";

        public int TotalFiles { get => 0; set { } }

        public int Position => throw new NotImplementedException();

        public Form Host { get; set; }

        private List<Playlist> playlists = new List<Playlist>();
        public List<Playlist> Playlists => playlists;

        public string Length => "";

        List<Song> IPlayEngine.Purchases => throw new NotImplementedException();

        public event EventHandler PlaybackFinished;

        public void AddToPlaylist(string playlistID, Track _Song, int pos)
        {
        }

        public void AddToPlaylist(Playlist pls, Track _Song, int pos)
        {
        }

        public Playlist CreatePlaylist(string Name)
        {
            return null;
        }

        public List<Track> Find(string Query)
        {
            return new List<Track>();
        }

        public Album[] FindAlbum(string album)
        {
            throw new NotImplementedException();
        }

        public Artist[] FindArtist(string Query)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbum(Artist artist, string album)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbum(string id)
        {
            RestClient client = CreateClient();
            RestRequest request = new RestRequest("albums/" + id);
            IRestResponse<Album> response = client.Execute<Album>(request);
            Album album = response.Data;
            return album;
        }

        public Artist GetArtist(string ID)
        {
            RestClient client = CreateClient();
            if (client == null) return null;
            RestRequest request = new RestRequest("artists/" + ID);
            IRestResponse<Artist> response = client.Execute<Artist>(request);
            Artist artist = response.Data;
            return artist;
        }

        public List<Track> Import(string RootDir)
        {
            throw new NotImplementedException();
        }

        public void Load(string content)
        {
            RestClient client = CreateClient();
            if (client == null) return;
            if (content.StartsWith("track:"))
            {
                var request = new RestSharp.RestRequest("me/player/play", RestSharp.Method.PUT) { RequestFormat = RestSharp.DataFormat.Json };
                try
                {
                    request.AddJsonBody(new
                    {
                        uris = new string[] { "spotify:" + content }
                    });

                    var response = client.Execute(request);
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        throw new Exception(response.Content);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public SearchResult Search(string query, string type)
        {
            RestClient client = CreateClient();
            RestRequest request = new RestRequest("search");
            request.AddQueryParameter("q", query);
            request.AddQueryParameter("type", type);
            IRestResponse<SearchResult> response = client.Execute<SearchResult>(request);
            SearchResult searchResult = response.Data;
            return searchResult;

        }

        public List<Track> LoadPlaylist(string p, ref Playlist playlist)
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            AuthorizeSpotifyForm authorizeForm = new AuthorizeSpotifyForm();
            authorizeForm.Show();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void MoveSongPlaylist(string playlistID, Track entry, int startLoc, int endLoc)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public bool Purchase(Track song)
        {
            throw new NotImplementedException();
        }

        public Track RawFind(Track _Song)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromPlaylist(string playlistID, int pos)
        {
            throw new NotImplementedException();
        }

        public List<Track> Search()
        {
            throw new NotImplementedException();
        }

        public void Seek(double pos)
        {
            throw new NotImplementedException();
        }

        public void ShowOptions()
        {
            throw new NotImplementedException();
        }

        public void SongImport(Track[] songs)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }

        public Playlist GetPlaylist(string PlsID)
        {
            throw new NotImplementedException();
        }

        public Result<Track> GetTracksInAlbum(string id, double offset = 0, double limit = 28)
        {
            RestClient client = CreateClient();
            RestRequest request = new RestRequest("albums/" + id + "/tracks");
            request.AddQueryParameter("offset", offset.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            IRestResponse<Result<Track>> response = client.Execute<Result<Track>>(request);
            Result<Track> searchResult = response.Data;
            return searchResult;
        }

        public Result<PlaylistTrack> GetTracksInPlaylist(string id, double offset = 0, double limit = 28)
        {
            RestClient client = CreateClient();
            RestRequest request = new RestRequest("playlists/" + id + "/tracks");
            request.AddQueryParameter("offset", offset.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            Result<PlaylistTrack> processedResult = new Result<PlaylistTrack>();

            IRestResponse<Result<PlaylistTrack>> response = client.Execute<Result<PlaylistTrack>>(request);
            Result<PlaylistTrack> searchResult = response.Data;
            Result<PlaylistTrack> result = new Result<PlaylistTrack>();
            foreach (PlaylistTrack pt in searchResult.items)
            {
                PlaylistTrack newTrack = new PlaylistTrack();
                newTrack.added_at = pt.added_at;
                newTrack.added_by = pt.added_by;
                newTrack.album = pt.album;
                newTrack.name = pt.name;
                newTrack.artists = pt.artists;
                newTrack.images = pt.images;
                newTrack.type = pt.type;
                newTrack.Engine = pt.Engine;
                newTrack.url = pt.url;
                result.items.Add(newTrack);

            }

            return searchResult;
        }
        public Result<Models.Album> GetAlbumsByArtist(string id, double offset = 0, double limit = 28)
        {
            RestClient client = CreateClient();
            if (client == null) return new Result<Models.Album>();
            RestRequest request = new RestRequest("artists/" + id + "/albums");
            request.AddQueryParameter("offset", offset.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            IRestResponse<Result<Models.Album>> response = client.Execute<Result<Models.Album>>(request);
            Result<Models.Album> searchResult = response.Data;
            return searchResult;
        }

        public bool Purchase(Song song)
        {
            throw new NotImplementedException();
        }

        List<Song> IPlayEngine.Find(string Query)
        {
            throw new NotImplementedException();
        }

        public void SongImport(Song[] songs)
        {
            throw new NotImplementedException();
        }

        List<Song> IPlayEngine.Import(string RootDir)
        {
            throw new NotImplementedException();
        }

        List<Song> IPlayEngine.Search()
        {
            throw new NotImplementedException();
        }

        public Song RawFind(Song _Song)
        {
            throw new NotImplementedException();
        }

        public Playlist ViewPlaylist(string Name, string PlsID)
        {
            throw new NotImplementedException();
        }

        public void AddToPlaylist(string playlistID, Song _Song, int pos)
        {
            throw new NotImplementedException();
        }

        public void AddToPlaylist(Playlist pls, Song _Song, int pos)
        {
            throw new NotImplementedException();
        }

        public void MoveSongPlaylist(string playlistID, Song entry, int startLoc, int endLoc)
        {
            throw new NotImplementedException();
        }

        List<Song> IPlayEngine.LoadPlaylist(string p, ref Playlist playlist)
        {
            throw new NotImplementedException();
        }
    }
}

