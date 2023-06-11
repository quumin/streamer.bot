using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CPHInline
{
    public bool Execute()
    {
        bool abortSR = CPH.GetGlobalVar<bool>("abortSR", false);
        if (abortSR)
        {
            return false;
        }
        else
        {
            Spotify_Coming_Next_Queue();
            CPH.LogDebug("Spotify_Coming_Next_Queue();");
            return true;
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
        int spotifyWebsocket = CPH.GetGlobalVar<int>("spotifyWebsocket", true);
        //CPH.LogDebug(json);

        //------------------------------------------------------------▼ Result 
        //CPH.LogDebug(json);
        string spotifyScene = "[NS] SPOTIFY";
        string nextArtistNextSongSource = "[TS] next Artist + Song";

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
        //CPH.SendMessage("Coming Next : " + queuedArtistName + " - " + queuedSongName);
        //	CPH.SendMessage("album image url : " +albumImgUrl);

        string queuedArtistAndSong = "     " + queuedArtistName + "        " + queuedSongName + "     ";
        CPH.ObsSetGdiText(spotifyScene, nextArtistNextSongSource, queuedArtistAndSong, spotifyWebsocket);


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
            //CPH.LogDebug("status code : " + statusCodeResponse);
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
            CPH.LogDebug("status code : " + statusCodeResponseAsInt.ToString() + " " + statusCodeResponse);
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

public class Album
{
    public string album_type { get; set; }

    public List<Artist> artists { get; set; }

    public List<string> available_markets { get; set; }

    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string id { get; set; }

    public List<Image> images { get; set; }

    public string name { get; set; } //---------------------------------------------------string queuedAlbumName = root.queue[0].album.name

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

    public string name { get; set; } //---------------------------------------------------string queuedArtistName = root.queue[0].artists[0].name

    public string type { get; set; }

    public string uri { get; set; }
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

    public string url { get; set; } //-----------------------------------------------------string queuedAlbumImgUrl = root.queue[0].album.images[0].url

    public int width { get; set; }
}

public class Queue
{
    public Album album { get; set; }

    public List<Artist> artists { get; set; }

    public List<string> available_markets { get; set; }

    public int disc_number { get; set; }

    public int duration_ms { get; set; } //------------------------------------------------int queuedDurationMs = root.queue[0].duration_ms

    public bool @explicit { get; set; }

    public ExternalIds external_ids { get; set; }

    public ExternalUrls external_urls { get; set; }

    public string href { get; set; }

    public string id { get; set; }

    public bool is_local { get; set; }

    public string name { get; set; } //----------------------------------------------------string queuedSongName = root.queue[0].name

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
} //----------------------------------------------int queuedItemsCount = root.queue.Count


