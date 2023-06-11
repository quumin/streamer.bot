using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

//-----------------------------------------------//
//--- [ GET ] search track ---@leblux_tv---------//
//-----------------------------------------------//
public class CPHInline
{
	public bool Execute()
	{
		//------------------------------------------------------------▼ Call to Spotify_Search_Result()
		string json = Spotify_Search_Result().ToString();
		//------------------------------------------------------------▼ Result From Call 
		CPH.LogDebug("Spotify_Search_Result()");
		Root root = JsonConvert.DeserializeObject<Root>(json);
		string songName = root.tracks.items[0].name.ToString();
		CPH.SetGlobalVar("songName", songName, true);
		string artistName = root.tracks.items[0].artists[0].name.ToString();
		CPH.SetGlobalVar("artistName", artistName, true);
		string albumName = root.tracks.items[0].album.name.ToString();
		CPH.SetGlobalVar("albumName", albumName, true);
		string trackUri = root.tracks.items[0].uri.ToString();
		CPH.SetGlobalVar("trackUri", trackUri, true);
		string albumImgUrl = root.tracks.items[0].album.images[0].url.ToString();
		CPH.SetGlobalVar("albumImgUrl", albumImgUrl, true);

		string songUrl = root.tracks.items[0].external_urls.spotify.ToString();
		var excludedSong = CPH.GetGlobalVar<List<string>>("songExclusion", true);
		string excludedSongInfos = artistName + " " + songName + " " + songUrl;
		if (excludedSong.Contains(excludedSongInfos))
		{
			CPH.SendMessage("❌ BANNED SONG : " + artistName + " - " + songName);
			bool abortSR = true;
			CPH.SetGlobalVar("abortSR", abortSR, false);
			return false;
		}
		else
		{
			CPH.SendMessage("➕ " + artistName + " - " + songName);
			return true;
		}
	}



	//------------------------------------------------------------▼ Script for Spotify_Search_Result()
	public object Spotify_Search_Result()
	{
		int limit = 1;
		string text;
		string token = CPH.GetGlobalVar<string>("accessToken", true);
		string input = args["rawInput"].ToString();
		if (input.Contains(","))
		{
			string[] splittedInput = input.Split(',');
			string artist_name = splittedInput[0];
			string song_name = splittedInput[1];
			text = artist_name + "%20" + song_name;
		}
		else
		{
			text = input;
		}

		string url = "https://api.spotify.com/v1/search";
		string query = url + "?q=" + text + "&type=track" + "&offset=0" + "&limit=" + limit.ToString();
		HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(query);
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
			CPH.LogDebug("status code : " + statusCodeResponseAsInt.ToString() + " " + statusCodeResponse);
			json = "{\"statusCode\":" + statusCodeResponseAsInt + "}";
			if (json.Contains("403"))
			{
				CPH.SendMessage(json + " sorry you must be Premium to use that command");
			}
			else if (json.Contains("401"))
			{
				CPH.RunAction("tiny SPOT TO SB - Refresh Token Swap", true);
				Spotify_Search_Result();
			}

			return json;
		}
	}
}
//------------------------------------------------------------▼ Class
public class Album
{
	public string album_type { get; set; }

	public List<Artist> artists { get; set; }

	public List<string> available_markets { get; set; }

	public ExternalUrls external_urls { get; set; }

	public string href { get; set; }

	public string id { get; set; }

	public List<Image> images { get; set; }

	public string name { get; set; } //------------------------------------albumName = root.tracks.items[0].album.name.ToString();

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

	public string name { get; set; } //------------------------------------artistName = root.tracks.items[0].artists[0].name.ToString();

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

	public string url { get; set; } //------------------------------------albumImgUrl = root.tracks.items[0].album.name.images.url.ToString();

	public int width { get; set; }
}

public class Item
{
	public Album album { get; set; }

	public List<Artist> artists { get; set; }

	public List<string> available_markets { get; set; }

	public int disc_number { get; set; }

	public int duration_ms { get; set; } //------------------------------------durationMs = root.tracks.item[0].duration_ms.ToString();

	public bool @explicit { get; set; }

	public ExternalIds external_ids { get; set; }

	public ExternalUrls external_urls { get; set; } //--------------------------url = root.track.item[0].external_urls.spotify.ToString();

	public string href { get; set; }

	public string id { get; set; }

	public bool is_local { get; set; }

	public string name { get; set; } //-------------------------------------songName = root.tracks.items[0].name.ToString();

	public int popularity { get; set; }

	public object preview_url { get; set; }

	public int track_number { get; set; }

	public string type { get; set; }

	public string uri { get; set; }
} //-------------------------------------trackUri = root.tracks.items[0].uri.ToString();

public class Root
{
	public Tracks tracks { get; set; }
}

public class Tracks
{
	public string href { get; set; }

	public List<Item> items { get; set; }

	public int limit { get; set; }

	public string next { get; set; }

	public int offset { get; set; }

	public object previous { get; set; }

	public int total { get; set; }
}