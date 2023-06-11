using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

//-----------------------------------------------//
//--- [ GET ] Now Playing ---@leblux_tv----------//
//-----------------------------------------------//
public class CPHInline
{

    public bool Execute()
    {
        CPH.SendMessage("Now Playing Watcher ON");

        currentSongCheckerLoop();
        return true;
    }

    public Task currentSongCheckerLoop()
    {
        int spotifyWebsocket = CPH.GetGlobalVar<int>("spotifyWebsocket", true);
        int crossfade = CPH.GetGlobalVar<int>("crossfade", true);
        while (true)
        {
            CPH.Wait(1000);
            try
            {
                string spotifyScene = "[NS] SPOTIFY";
                string artistNameSource = "[TS] artistName";
                string songNameSource = "[TS] songName";
                string artwork = "[BS] artworkUrl";
                string nextArtistNextSongSource = "[TS] next Artist + Song";
                Spotify_NowPlaying_Checker().ToString();
                string savedSong = CPH.GetGlobalVar<string>("savedSong", true);
                string currentSong = CPH.GetGlobalVar<string>("songName_NOW", true);
                string savedArtist = CPH.GetGlobalVar<string>("savedArtist", true);
                string currentArtist = CPH.GetGlobalVar<string>("artistName_NOW", true);
                string savedAlbumImgUrl = CPH.GetGlobalVar<string>("savedAlbumImgUrl", true);
                string currentAlbumImgUrl = CPH.GetGlobalVar<string>("albumImgUrl_NOW", true);
                string savedUrl = CPH.GetGlobalVar<string>("savedUrl", true);
                string currentUrl = CPH.GetGlobalVar<string>("url_Now", true);
                string savedTrackId = CPH.GetGlobalVar<string>("savedTrackId", true);
                string currentTrackId = CPH.GetGlobalVar<string>("trackId_Now", true);
                bool savedState = CPH.GetGlobalVar<bool>("savedState", true);
                bool currentState = CPH.GetGlobalVar<bool>("state_NOW", true);
                int durationMs = CPH.GetGlobalVar<int>("durationMs_NOW", true);
                int savedDurationMs = CPH.GetGlobalVar<int>("savedDurationMs", true);
                int currentDurationMs = CPH.GetGlobalVar<int>("durationMs_NOW", true);
                int crossfadeHalf = crossfade / 2;
                int crossfadeEighth = crossfade / 8;
                int durationHalf = (durationMs / 2) - crossfadeHalf;
                int durationAnEighth = (durationMs / 8) - crossfadeEighth;
                CPH.SetGlobalVar("currentSong", currentSong, true);
                CPH.SetGlobalVar("currentArtist", currentArtist, true);
                CPH.SetGlobalVar("currentUrl", currentUrl, true);
                CPH.SetGlobalVar("currentTrackId", currentTrackId, true);
                CPH.SetGlobalVar("currentState", currentState, true);
                if (savedState != currentState && currentState)
                {
                    CPH.SendMessage("▶️");
                    CPH.SetGlobalVar("savedState", currentState, true);
                    CPH.SetGlobalVar("nowPlayingBool", true, true);
                    CPH.RunAction("Cam Controller - Now Playing");
                }
                else if ((savedState != currentState) && (currentState == false))
                {
                    CPH.SendMessage("⏸");
                    CPH.SetGlobalVar("savedState", currentState, true);
                    CPH.SetGlobalVar("nowPlayingBool", false, true);
                    CPH.RunAction("Cam Controller - Now Playing");
                }

                if (savedSong != currentSong)
                {
                    CPH.Wait(30);
                    CPH.LogDebug("currentSongCheckerLoop()");
                    //---------------------RESET DISPLAY


                    //----------------------NEW SONG DISPLAY                   
                    CPH.Wait(400);
                    CPH.ObsSetBrowserSource("SS_NowPlaying", "AlbumArt_Image", currentAlbumImgUrl);
                    CPH.ObsSetGdiText("SS_NowPlaying", "Title", currentSong);
                    CPH.ObsSetGdiText("SS_NowPlaying", "Artist", currentArtist);
                    //---------------------CHANGE SONG INFOS 
                    CPH.SetGlobalVar("savedArtist", currentArtist, true);
                    CPH.SetGlobalVar("savedSong", currentSong, true);
                    CPH.SetGlobalVar("savedAlbumImgUrl", currentAlbumImgUrl, true);
                    CPH.SetGlobalVar("savedUrl", currentUrl, true);
                    CPH.SetGlobalVar("savedTrackId", currentTrackId, true);
                    CPH.SetGlobalVar("savedDurationMs", currentDurationMs, true);

                    //Websocket
                    CPH.WebsocketBroadcastJson("{'artistName':'" + currentArtist + "','songName':'" + currentSong + "','albumArt':'" + currentAlbumImgUrl + "','duration':'" + currentDurationMs + "'}");
                    bool watcherToChat = Convert.ToBoolean(args["watcherToChat"]);
                    string nowPlaying = args["nowPlayingArg"].ToString();
                    if (watcherToChat)
                    {
                        CPH.SendMessage(nowPlaying + currentArtist + " - " + currentSong);
                    }

                    CPH.ObsToggleFilter(spotifyScene, "Move Source", spotifyWebsocket);
                    CPH.ObsSetGdiText(spotifyScene, artistNameSource, currentArtist, spotifyWebsocket);
                    CPH.ObsSetGdiText(spotifyScene, songNameSource, currentSong, spotifyWebsocket);
                    CPH.ObsSetBrowserSource(spotifyScene, artwork, currentAlbumImgUrl, spotifyWebsocket);
                    Spotify_Coming_Next_Queue();
                    string queuedSong = CPH.GetGlobalVar<string>("queuedSongName", true);
                    string queuedArtist = CPH.GetGlobalVar<string>("queuedArtistName", true);
                    string queuedArtistAndSong = "     " + queuedArtist + "        " + queuedSong + "     ";
                    CPH.ObsSetGdiText(spotifyScene, nextArtistNextSongSource, queuedArtistAndSong, spotifyWebsocket);
                }
            }
            catch (Exception e)
            {
                //CPH.LogDebug(e.ToString());
            }
        }

        return currentSongCheckerLoop();
    }

    public string Spotify_NowPlaying_Checker()
    {
        string json = Spotify_NowPlaying_Result().ToString();
        string songName_NOW = "";
        NowPlaying root = JsonConvert.DeserializeObject<NowPlaying>(json);
        songName_NOW = root.item.name.ToString();
        CPH.SetGlobalVar("songName_NOW", songName_NOW, true);
        string artistName_NOW = root.item.artists[0].name.ToString();
        CPH.SetGlobalVar("artistName_NOW", artistName_NOW, true);
        string albumName_NOW = root.item.album.name.ToString();
        CPH.SetGlobalVar("albumName_NOW", albumName_NOW, true);
        int durationMs_NOW = root.item.duration_ms;
        CPH.SetGlobalVar("durationMs_NOW", durationMs_NOW, true);
        string progressMs_NOW = root.progress_ms.ToString();
        CPH.SetGlobalVar("progressMs_NOW", progressMs_NOW, true);
        string albumImgUrl_NOW = root.item.album.images[0].url.ToString();
        CPH.SetGlobalVar("albumImgUrl_NOW", albumImgUrl_NOW, true);
        string state_NOW = root.is_playing.ToString();
        CPH.SetGlobalVar("state_NOW", state_NOW, true);
        string url_Now = root.item.external_urls.spotify.ToString();
        CPH.SetGlobalVar("url_Now", url_Now, true);
        string trackId_Now = root.item.id.ToString();
        CPH.SetGlobalVar("trackId_Now", trackId_Now, true);

        return json;
    }

    //------------------------------------------------------------▼ Script for Spotify_NowPlaying_Result()
    public string Spotify_NowPlaying_Result()
    {
        int limit = 1;
        string token = CPH.GetGlobalVar<string>("accessToken", true);
        string url = "https://api.spotify.com/v1/me/player";
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Method = "GET";
        webRequest.ContentType = "application/json";
        webRequest.Accept = "application/json";
        webRequest.Headers.Add("Authorization", "Bearer " + token);
        //------------------------------------------------------------▼ Response 
        HttpWebResponse resp = null;
        HttpStatusCode statusCode;
        String json = null;
        try
        {
            resp = (HttpWebResponse)webRequest.GetResponse();
            var statusCodeResponse = resp.StatusCode;
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }

            return json;
        }
        //------------------------------------------------------------▼ Exception
        catch (WebException e)
        {
            resp = (HttpWebResponse)e.Response;
            var statusCodeResponse = resp.StatusCode;
            int statusCodeResponseAsInt = ((int)resp.StatusCode);
            CPH.LogDebug("Exception status code nowPlayingResult  : " + statusCodeResponseAsInt.ToString() + " " + statusCodeResponse);
            json = "{\"statusCode\":" + statusCodeResponseAsInt + "}";
            int retryAfterIntSec = 0;
            if (json.Contains("401"))
            {
                CPH.RunAction("tiny SPOT TO SB - Refresh Token Swap", true);
                CPH.Wait(2000);
                Spotify_NowPlaying_Result();
            }
            else if (json.Contains("429"))
            {
                for (int i = 0; i < resp.Headers.Count; ++i)
                {
                    CPH.LogDebug("Header Name:" + resp.Headers.Keys[i] + ", Value : " + resp.Headers[i]);
                    if (resp.Headers.Keys[i] == "retry-after")
                    {
                        string retryAfter = resp.Headers[i];
                        retryAfterIntSec = Convert.ToInt32(retryAfter);
                        CPH.SendMessage(" retrying after = " + retryAfter.ToString() + " seconds");
                    }
                }

                CPH.SendMessage(" rate limit exceeded ");
            }

            int retryAfterIntMs = retryAfterIntSec * 1000;
            CPH.Wait(retryAfterIntMs);
            return json;
        }
    }

    //=========================================================================================================================================
    //-----------------------------------------------//
    //--- [ GET ] Next In Queue ---@leblux_tv--------//
    //-----------------------------------------------//
    public string Spotify_Coming_Next_Queue()
    {
        //------------------------------------------------------------▼ Call to Spotify_Coming_Next_Queue()
        string json = Spotify_Queue_Result().ToString();
        //------------------------------------------------------------▼ Result 
        JsonQueue root = JsonConvert.DeserializeObject<JsonQueue>(json);
        string queuedItemsCount = root.queue.Count.ToString();
        CPH.SetGlobalVar("queuedItemsCount", queuedItemsCount, true);
        string queuedSongName = root.queue[0].name.ToString();
        CPH.SetGlobalVar("queuedSongName", queuedSongName, true);
        int queuedDurationMs = root.queue[0].duration_ms;
        CPH.SetGlobalVar("queuedDurationMs", queuedDurationMs, true);
        string queuedArtistName = root.queue[0].artists[0].name.ToString();
        CPH.SetGlobalVar("queuedArtistName", queuedArtistName, true);
        string queuedAlbumName = root.queue[0].album.name.ToString();
        CPH.SetGlobalVar("queuedAlbumName", queuedAlbumName, true);
        string queuedAlbumImgUrl = root.queue[0].album.images[0].url.ToString();
        CPH.SetGlobalVar("queuedAlbumImgUrl", queuedAlbumImgUrl, true);
        return json;
    }

    //------------------------------------------------------------▼ Script for Spotify_Queue_Result()
    public object Spotify_Queue_Result()
    {
        string token = CPH.GetGlobalVar<string>("accessToken", true);
        string url = "https://api.spotify.com/v1/me/player/queue";
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Method = "GET";
        webRequest.ContentType = "application/json";
        webRequest.Accept = "application/json";
        webRequest.Headers.Add("Authorization", "Bearer " + token);
        //------------------------------------------------------------▼ Response 
        HttpWebResponse resp = null;
        HttpStatusCode statusCode;
        String json = null;
        try
        {
            resp = (HttpWebResponse)webRequest.GetResponse();
            var statusCodeResponse = resp.StatusCode;
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }

            return json;
        }
        //------------------------------------------------------------▼ Exception
        catch (WebException e)
        {
            resp = (HttpWebResponse)e.Response;
            var statusCodeResponse = resp.StatusCode;
            int statusCodeResponseAsInt = ((int)resp.StatusCode);
            CPH.LogDebug("status code e : " + statusCodeResponseAsInt.ToString() + " " + statusCodeResponse);
            json = "{\"statusCode\":" + statusCodeResponseAsInt + "}";
            if (json.Contains("403"))
            {
                CPH.SendMessage(json + " sorry you must be Premium to use that command");
            }
            else if (json.Contains("401"))
            {
                CPH.RunAction("tiny SPOT TO SB - Refresh Token Swap", true);
                Spotify_Queue_Result();
            }
            else if (json.Contains("429"))
            {
                for (int i = 0; i < resp.Headers.Count; ++i)
                {
                    CPH.LogDebug("Header Name:" + resp.Headers.Keys[i] + ", Value :{1}" + resp.Headers[i]);
                }

                CPH.SendMessage(" rate limit exceeded ");
            }

            return json;
        }
    }
}

public class CurrentlyPlaying
{
    public Album album { get; set; }

    public List<Artist> artists { get; set; }

    public List<string> available_markets { get; set; }

    public int disc_number { get; set; }

    public int duration_ms { get; set; }

    public bool @explicit { get; set; }

    public ExternalIds external_ids { get; set; }

    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string id { get; set; }

    public bool is_local { get; set; }

    public string name { get; set; }

    public int popularity { get; set; }

    public string preview_url { get; set; }

    public int track_number { get; set; }

    public string type { get; set; }

    public string uri { get; set; }
}

public class Queue
{
    public Album album { get; set; }

    public List<Artist> artists { get; set; }

    public List<string> available_markets { get; set; }

    public int disc_number { get; set; }

    public int duration_ms { get; set; }

    public bool @explicit { get; set; }

    public ExternalIds external_ids { get; set; }

    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string id { get; set; }

    public bool is_local { get; set; }

    public string name { get; set; }

    public int popularity { get; set; }

    public string preview_url { get; set; }

    public int track_number { get; set; }

    public string type { get; set; }

    public string uri { get; set; }
}

public class JsonQueue
{
    public CurrentlyPlaying currently_playing { get; set; }

    public List<Queue> queue { get; set; }
}

public class Actions
{
    public Disallows disallows { get; set; }
}

public class Album
{
    public string album_type { get; set; }

    public List<Artist> artists { get; set; }

    public List<string> available_markets { get; set; }

    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string id { get; set; }

    public List<Image> images { get; set; }

    public string name { get; set; }

    public string release_date { get; set; }

    public string release_date_precision { get; set; }

    public int total_tracks { get; set; }

    public string type { get; set; }

    public string uri { get; set; }
}

public class Artist
{
    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string id { get; set; }

    public string name { get; set; }

    public string type { get; set; }

    public string uri { get; set; }
}

public class Context
{
    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string type { get; set; }

    public string uri { get; set; }
}

public class Disallows
{
    public bool resuming { get; set; }
}

public class ExternalIds
{
    public string isrc { get; set; }
}

public class ExternalUrls
{
    public string spotify { get; set; }
}

public class Image
{
    public int height { get; set; }

    public string url { get; set; }

    public int width { get; set; }
}

public class Item
{
    public Album album { get; set; }

    public List<Artist> artists { get; set; }

    public List<string> available_markets { get; set; }

    public int disc_number { get; set; }

    public int duration_ms { get; set; }

    public bool @explicit { get; set; }

    public ExternalIds external_ids { get; set; }

    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string id { get; set; }

    public bool is_local { get; set; }

    public string name { get; set; }

    public int popularity { get; set; }

    public string preview_url { get; set; }

    public int track_number { get; set; }

    public string type { get; set; }

    public string uri { get; set; }
}

public class NowPlaying
{
    public long timestamp { get; set; }

    public Context context { get; set; }

    public int progress_ms { get; set; }

    public Item item { get; set; }

    public string currently_playing_type { get; set; }

    public Actions actions { get; set; }

    public bool is_playing { get; set; }
}