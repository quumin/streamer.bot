using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

public class CPHInline
{
	private bool isRunning = false;
	private bool runSpot2Sb;
	private static int tokenSwapFailures = 0;
	private static CancellationTokenSource _cts;
	private string cachedJson = "";
	private Dictionary<string, object> _songInfos = new Dictionary<string, object>();
	private Dictionary<string, object> _songState = new Dictionary<string, object>();
	private bool newRequester;
	private string previousDisplayedSongAndArtist = "";
	private DateTime retryAfterTime = DateTime.MinValue;
	private static HttpClient _httpClient;
	private System.Timers.Timer timer;
	private bool watcherInChat = false;
	private bool displayPlayPause = false;
	//private int spotifyWebsocket = 0;
	private string stateNow = "";
	private bool badRequestLogged = false;
	public void Init()
	{
		if (_httpClient == null)
		{
			_httpClient = new HttpClient
			{
				Timeout = TimeSpan.FromSeconds(30) // Adjust the timeout as needed
			};
		}

		_httpClient.DefaultRequestHeaders.Clear(); // Optionally clear headers if needed
												   //CheckAccessToken();
		_cts = new CancellationTokenSource();
		AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
	}

	private void CheckAccessToken()
	{
		string token = CPH.GetGlobalVar<string>("accessToken_sp2sb", true);
		if (string.IsNullOrEmpty(token))
		{
			CPH.SendMessage("please run !spotifyConnect");
		}
		else
		{
			runSpot2Sb = true;
			WatcherState();
		}
	}

	public void Dispose()
	{
		CPH.LogDebug("SPOT2SB -> Dispose");
		_httpClient?.Dispose();
		if (timer != null)
		{
			isRunning = false;
			timer.Dispose();
		}
	}

	public bool Disposing()
	{
		Dispose();
		return true;
	}

	public bool initializing()
	{
		CheckAccessToken();
		CPH.SendMessage("CheckAccessToken");
		return true;
	}

	private void OnProcessExit(object sender, EventArgs e)
	{
		isRunning = false;
		CPH.SetGlobalVar("isSpot2SbWatcherRunning_sp2sb", isRunning, false);
		CPH.LogDebug("SPOT2SB -> Dispose OnProcessExit()");
		Dispose();
	}

	public bool Execute()
	{
		CheckAccessToken();
		return true;
	}

	private void StartTimer()
	{
		_cts = new CancellationTokenSource();
		timer = new System.Timers.Timer(1100);
		timer.Elapsed += OnTimerElapsed;
		timer.AutoReset = true;
		timer.Start();
	}

	private void OnTimerElapsed(object sender, ElapsedEventArgs e)
	{
		if (_cts.Token.IsCancellationRequested)
		{
			timer.Stop();
			return;
		}

		//------------▼
		if (DateTime.Now < retryAfterTime)
			return;
		//------------▲
		Task.Run(async () => await UpdateSpotifyNowPlayingData());
	}

	private void StopTimer()
	{
		CPH.LogDebug("SPOT2SB -> Dispose StopTimer");
		_cts?.Cancel();
		_cts?.Dispose();
	}

	private void WatcherState()
	{
		if (runSpot2Sb)
		{
			if (isRunning)
			{
				return;
			}
			else
			{
				CPH.SendMessage("spot2sb watcher starting");
				isRunning = true;
				CPH.SetGlobalVar("isSpot2SbWatcherRunning_sp2sb", isRunning, false);
				StartTimer();
			}
		}
		else
		{
			CPH.SendMessage("spot2sb watcher stopped");
			isRunning = false;
			CPH.SetGlobalVar("isSpot2SbWatcherRunning_sp2sb", isRunning, false);
			StopTimer();
		}
	}

	private async Task UpdateSpotifyNowPlayingData()
	{
		try
		{
			bool matchingRequestFound = false;
			int spotifyWebsocket = CPH.GetGlobalVar<int>("spotifyWebsocket_sp2sb", true);
			string currentJson = "";
			string currentRequester = "";
			currentJson = await Spotify_NowPlaying_ResultAsync();
			(string currentSongName, string currentArtistName, int currentDuration, string currentAlbumArtUrl, bool currentPlayingState, string currentTrackId, string currentSongLink) = ExtractSongData(currentJson);
			(string cachedSongName, string cachedArtistName, int cachedDuration, string cachedAlbumArtUrl, bool cachedPlayingState, string cachedTrackId, string cachedSongLink) = ExtractSongData(cachedJson);
			//
			currentSongName = Regex.Unescape(currentSongName);
			currentArtistName = Regex.Unescape(currentArtistName);
			currentAlbumArtUrl = Regex.Unescape(currentAlbumArtUrl);
			currentSongLink = Regex.Unescape(currentSongLink);
			//
			cachedSongName = Regex.Unescape(cachedSongName);
			cachedArtistName = Regex.Unescape(cachedArtistName);
			cachedAlbumArtUrl = Regex.Unescape(cachedAlbumArtUrl);
			cachedSongLink = Regex.Unescape(cachedSongLink);
			//-------------------------------------------------------------------------------------------- checking if it comes from a request display it 
			try
			{
				string userNameRequester = "";
				List<string> userRequest = CPH.GetGlobalVar<List<string>>("userRequestList_sp2sb", false);
				string[] wordsInRequestList = userRequest.FirstOrDefault().Split(' ');
				var matchingRequest = userRequest[0];
				if (matchingRequest != null && matchingRequest.Contains(currentArtistName) && matchingRequest.Contains(currentSongName))
				{
					matchingRequestFound = true;
				}
				else
				{
					matchingRequestFound = false;
				}
			}
			catch (Exception aaa)
			{
			}

			//-------------------------------------------------------------------------------------------------------
			//
			string currentSongAndArtist = currentSongName + " " + currentArtistName;
			bool songChanged = currentSongName != cachedSongName;
			bool stateChanged = currentPlayingState != cachedPlayingState;
			if (stateChanged && !currentPlayingState)
			{
				if (stateNow != "pause")
				{
					CPH.LogDebug("SPOT2SB -> pause");
					_songState.Add("songState", "paused");
					CPH.TriggerCodeEvent("statePause", _songState);
					_songState.Clear();
					fromPlayStateToPauseState(spotifyWebsocket);
					stateNow = "pause";
					ProcessPlayPauseToChat(stateNow);
				}
			}

			if (!stateChanged && currentPlayingState)
			{
				if (stateNow != "play")
				{
					CPH.LogDebug("SPOT2SB -> play");
					_songState.Add("songState", "playing");
					CPH.TriggerCodeEvent("statePlay", _songState);
					_songState.Clear();
					fromPauseStateToPlayState(spotifyWebsocket);
					stateNow = "play";
					ProcessPlayPauseToChat(stateNow);
				}
			}

			if (currentSongAndArtist == previousDisplayedSongAndArtist && !matchingRequestFound)
			{
				return;
			}

			if (currentSongName == "spot2sb" && currentArtistName == "spot2sb")
			{
				currentSongName = cachedSongName;
				currentArtistName = cachedArtistName;
				currentAlbumArtUrl = cachedAlbumArtUrl;
				currentPlayingState = cachedPlayingState;
				songChanged = false;
			}

			if (songChanged && !currentPlayingState && !matchingRequestFound) //A
			{
				//CPH.WebsocketBroadcastJson("{'artistName':'" + currentArtistName + "','songName':'" + currentSongName + "','albumArt':'" + currentAlbumArtUrl + "','duration':'" + currentDuration + "'}");
				string jsonPayload = JsonConvert.SerializeObject(new { artistName = currentArtistName, songName = currentSongName, albumArt = currentAlbumArtUrl, duration = currentDuration });
				CPH.WebsocketBroadcastJson(jsonPayload);
				_songInfos.Add("artistName", currentArtistName);
				_songInfos.Add("songName", currentSongName);
				_songInfos.Add("albumArt", currentAlbumArtUrl);
				_songInfos.Add("songDuration", currentDuration);
				_songInfos.Add("songLink", currentSongLink);
				CPH.TriggerCodeEvent("newPlayingSong", _songInfos);
				_songInfos.Clear();
				//
				string queueJson = await Spotify_Queue_ResultAsync();
				(string queuedSongName, string queuedArtistName) = ExtractQueueData(queueJson);
				fromPauseStateToNewSong(spotifyWebsocket, currentArtistName, currentSongName, currentAlbumArtUrl, currentDuration, queuedSongName, queuedArtistName);
				//
				previousDisplayedSongAndArtist = currentSongAndArtist;
				try
				{
					ProcessWatcherToChat(currentArtistName, currentSongName);
				}
				catch (Exception eee)
				{
					CPH.LogError("ProcessWatcherToChat failed [A]: " + eee);
				}
			}

			if (songChanged && currentPlayingState && !matchingRequestFound) //B
			{
				CPH.LogDebug("SPOT2SB -> [New Song] songChanged");
				//CPH.WebsocketBroadcastJson("{'artistName':'" + currentArtistName + "','songName':'" + currentSongName + "','albumArt':'" + currentAlbumArtUrl + "','duration':'" + currentDuration + "'}");
				string jsonPayload = JsonConvert.SerializeObject(new { artistName = currentArtistName, songName = currentSongName, albumArt = currentAlbumArtUrl, duration = currentDuration });
				CPH.WebsocketBroadcastJson(jsonPayload);
				previousDisplayedSongAndArtist = currentSongAndArtist;
				_songInfos.Add("artistName", currentArtistName);
				_songInfos.Add("songName", currentSongName);
				_songInfos.Add("albumArt", currentAlbumArtUrl);
				_songInfos.Add("songDuration", currentDuration);
				_songInfos.Add("songLink", currentSongLink);
				CPH.TriggerCodeEvent("newPlayingSong", _songInfos);
				_songInfos.Clear();
				//
				string queueJson = await Spotify_Queue_ResultAsync();
				(string queuedSongName, string queuedArtistName) = ExtractQueueData(queueJson);
				fromPlayStateToNewSong(spotifyWebsocket, currentArtistName, currentSongName, currentAlbumArtUrl, currentDuration, queuedSongName, queuedArtistName);
				//
				try
				{
					ProcessWatcherToChat(currentArtistName, currentSongName);
				}
				catch (Exception eee)
				{
					CPH.LogError("ProcessWatcherToChat failed [B]: " + eee);
				}
			}

			if (matchingRequestFound) //C
			{
				CPH.LogDebug("SPOT2SB -> [New Song] matchingRequestFound");
				matchingRequestFound = false;
				//CPH.WebsocketBroadcastJson("{'artistName':'" + currentArtistName + "','songName':'" + currentSongName + "','albumArt':'" + currentAlbumArtUrl + "','duration':'" + currentDuration + "'}");
				string jsonPayload = JsonConvert.SerializeObject(new { artistName = currentArtistName, songName = currentSongName, albumArt = currentAlbumArtUrl, duration = currentDuration });
				CPH.WebsocketBroadcastJson(jsonPayload);
				previousDisplayedSongAndArtist = currentSongAndArtist;
				_songInfos.Add("artistName", currentArtistName);
				_songInfos.Add("songName", currentSongName);
				_songInfos.Add("albumArt", currentAlbumArtUrl);
				_songInfos.Add("songDuration", currentDuration);
				_songInfos.Add("songLink", currentSongLink);
				CPH.TriggerCodeEvent("newPlayingSong", _songInfos);
				_songInfos.Clear();
				//
				string queueJson = await Spotify_Queue_ResultAsync();
				(string queuedSongName, string queuedArtistName) = ExtractQueueData(queueJson);
				fromPlayStateToNewSong(spotifyWebsocket, currentArtistName, currentSongName, currentAlbumArtUrl, currentDuration, queuedSongName, queuedArtistName);
				//
				try
				{
					ProcessWatcherToChat(currentArtistName, currentSongName);
				}
				catch (Exception eee)
				{
					CPH.LogError("ProcessWatcherToChat failed [C]: " + eee);
				}

				matchingRequestFound = false;
			}

			cachedJson = currentJson;
		}
		catch (Exception ex)
		{
			CPH.LogError($"SPOT2SB Exception in UpdateSpotifyNowPlayingData: {ex.Message}");
			CPH.LogError($"SPOT2SB StackTrace: {ex.StackTrace}");
			CPH.LogError($"SPOT2SB Exception Type: {ex.GetType().FullName}");
			if (ex.InnerException != null)
			{
				CPH.LogError($"SPOT2SB Inner Exception: {ex.InnerException.Message}");
				CPH.LogError($"SPOT2SB Inner Exception Type: {ex.InnerException.GetType().FullName}");
			}

			CPH.LogError($"SPOT2SB Exception Source: {ex.Source}");
			isRunning = false;
		}
	}

	private string ReplaceSpecialCharacters(string input)
	{
		input = input.Replace("\"", "\\\"");
		input = input.Replace("\'", "\\'");
		return input;
	}

	private void ProcessWatcherToChat(string currentArtistName, string currentSongName)
	{
		bool watcherInChat = CPH.GetGlobalVar<bool>("watcherInChat_sp2sb", true);
		bool matchingRequestFound = false;
		string matchingRequest;
		List<string> userRequest = CPH.GetGlobalVar<List<string>>("userRequestList_sp2sb", false);
		if (userRequest != null && userRequest.Count > 0)
		{
			matchingRequest = userRequest[0];
			if (matchingRequest.Contains(currentArtistName) && matchingRequest.Contains(currentSongName))
			{
				matchingRequestFound = true;
			}
		}

		if (watcherInChat == true)
		{
			string nowPlayingIcon = CPH.GetGlobalVar<string>("nowPlayingEmoji_sp2sb", true);
			bool prependEmojiEmpty = CPH.GetGlobalVar<bool>("prependEmojiEmpty_sp2sb", true);
			if (nowPlayingIcon == null && prependEmojiEmpty == false)
			{
				nowPlayingIcon = "♫ ";
			}
			else if (nowPlayingIcon == null && prependEmojiEmpty)
			{
				nowPlayingIcon = "";
			}

			nowPlayingIcon = nowPlayingIcon + " ";
			string currentArtist = currentArtistName;
			string currentSong = currentSongName;
			if (currentSong == "spot2sb")
			{
				return;
			}

			string nowPlayingInChat = nowPlayingIcon + currentArtist + " - " + currentSong;
			string nowPlayingInChatWithRequester = "";
			//
			if (userRequest == null || userRequest.Count == 0)
			{
				nowPlayingInChat = nowPlayingIcon + " " + currentArtist + " - " + currentSong;
			}
			else
			{
				if (userRequest.Count > 0)
				{
					matchingRequest = userRequest[0];
					if (matchingRequest.Contains(currentArtist) && matchingRequest.Contains(currentSong))
					{
						//CPH.SendMessage("match in request " + matchingRequest);
						nowPlayingInChatWithRequester = nowPlayingIcon + " " + matchingRequest;
						userRequest.RemoveAt(0);
						CPH.SetGlobalVar("userRequestList_sp2sb", userRequest, false);
						matchingRequestFound = true;
					}
				}
			}

			bool showRequester = CPH.GetGlobalVar<bool>("showRequester_sp2sb", true);
			if (showRequester == null)
			{
				showRequester = false;
			}

			if (showRequester && matchingRequestFound)
			{
				CPH.SendMessage(nowPlayingInChatWithRequester);
			}
			else
			{
				CPH.SendMessage(nowPlayingInChat);
			}
		}
		else if (!watcherInChat && matchingRequestFound)
		{
			userRequest.RemoveAt(0);
			CPH.SetGlobalVar("userRequestList_sp2sb", userRequest, false);
		}
	}

	private void ProcessPlayPauseToChat(string stateNow)
	{
		bool displayPlayPause = CPH.GetGlobalVar<bool>("displayPlayPause_sp2sb", true);
		if (displayPlayPause == true)
		{
			if (stateNow == "play")
			{
				CPH.SendMessage("▶️");
			}

			if (stateNow == "pause")
			{
				CPH.SendMessage("⏸");
			}
		}
	}

	public void fromPauseStateToPlayState(int spotifyWebsocket)
	// ⏸️ -> ▶️🎵
	{
		CPH.SetGlobalVar("qminNowPlayingBool", true);
		CPH.RunAction("Cam Controller - Now Playing");
		/*For the sbPlayer, not used by me.
		 * CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterEnabled\":true}", spotifyWebsocket);
		 * CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterEnabled\":true}", spotifyWebsocket);
		 */
	}

	private void fromPauseStateToNewSong(int spotifyWebsocket, string currentArtist, string currentSong, string currentAlbumImgUrl, int durationMSCrossfaded, string queuedSongName, string queuedArtistName)
	// ⏸ -> 🆕🎵
	{
		CPH.SetGlobalVar("qminNowPlayingBool", true);
		CPH.ObsSetGdiText("SS_NowPlaying", "Title", currentSong);
		CPH.ObsSetGdiText("SS_NowPlaying", "Artist", currentArtist);
		CPH.ObsSetBrowserSource("SS_NowPlaying", "AlbumArt_Image", currentAlbumImgUrl);
		CPH.RunAction("Cam Controller - Now Playing");
		//For the sbPlayer, not used by me.
		//	CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterEnabled\":false}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterEnabled\":false}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	//
		//	//
		//	// artist Update
		//	string jsonFormat_artistUpdate = JsonConvert.SerializeObject(new { inputName = "[TS] artistName", inputSettings = new { text = currentArtist }, overlay = true });
		//	// sauvegarde = CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] artistName\",\"inputSettings\":{\"text\":\"" + currentArtist + "\"},\"overlay\":true}", spotifyWebsocket);
		//	CPH.ObsSendRaw("SetInputSettings", jsonFormat_artistUpdate, spotifyWebsocket);
		//	//
		//	//
		//	// Song  update 
		//	string jsonFormat_songUpdate = JsonConvert.SerializeObject(new { inputName = "[TS] songName", inputSettings = new { text = currentSong }, overlay = true });
		//	// sauvegarde = CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] songName\",\"inputSettings\":{\"text\":\"" + currentSong + "\"},\"overlay\":true}", spotifyWebsocket);
		//	CPH.ObsSendRaw("SetInputSettings", jsonFormat_songUpdate, spotifyWebsocket);
		//	//
		//	//
		//	CPH.Wait(10);
		//	// queued artist & Song  update 
		//	string jsonFormat_queueUpdate = JsonConvert.SerializeObject(new { inputName = "[TS] next Artist + Song", inputSettings = new { text = $"{queuedArtistName} - {queuedSongName}" }, overlay = true });
		//	// sauvegarde = CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] next Artist + Song\",\"inputSettings\":{\"text\":\"" + queuedArtistName + " - " + queuedSongName + "\"},\"overlay\":true}", spotifyWebsocket);
		//	CPH.ObsSendRaw("SetInputSettings", jsonFormat_queueUpdate, spotifyWebsocket);
		//	//
		//	CPH.Wait(10);
		//	//
		//	// IMG update
		//	CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[BS] artworkUrl\",\"inputSettings\":{\"height\":300,\"shutdown\":true,\"url\":\"" + currentAlbumImgUrl + "\",\"width\":300},\"overlay\":true}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[BS] artworkUrl - 2\",\"inputSettings\":{\"height\":300,\"shutdown\":true,\"url\":\"" + currentAlbumImgUrl + "\",\"width\":300},\"overlay\":true}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	// Enable 0° filter
		//	CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_0°\",\"filterEnabled\":true}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_0°\",\"filterEnabled\":true}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	// Update Duration for 360° filter
		//	CPH.ObsSendRaw("SetSourceFilterSettings", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterSettings\":{\"duration\":" + durationMSCrossfaded + ",\"easing_match\":0,\"filter\":\"radial_fill_degrees_shader\",\"setting_float\":360,\"setting_name\":\"Fill\",\"value_type\":2},\"overlay\":true}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	CPH.ObsSendRaw("SetSourceFilterSettings", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterSettings\":{\"duration\":" + durationMSCrossfaded + ",\"easing_match\":0,\"filter\":\"radial_fill_degrees_shader\",\"setting_float\":360,\"setting_name\":\"Fill\",\"value_type\":2},\"overlay\":true}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterEnabled\":true}", spotifyWebsocket);
		//	CPH.Wait(10);
		//	CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterEnabled\":true}", spotifyWebsocket);
		//
	}

public void fromPlayStateToPauseState(int spotifyWebsocket)
	// ▶️🎵 -> ⏸️
	{
		CPH.SetGlobalVar("qminNowPlayingBool", false);
		CPH.RunAction("Cam Controller - Now Playing");
		//Not used by me/
		//CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterEnabled\":false}", spotifyWebsocket);
		//CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterEnabled\":false}", spotifyWebsocket);
	}

private void fromPlayStateToNewSong(int spotifyWebsocket, string currentArtist, string currentSong, string currentAlbumImgUrl, int durationMSCrossfaded, string queuedSongName, string queuedArtistName)
// ▶️🎵 -> 🆕🎵
{
		CPH.SetGlobalVar("qminNowPlayingBool", true);
		CPH.ObsSetGdiText("SS_NowPlaying", "Title", currentSong);
		CPH.ObsSetGdiText("SS_NowPlaying", "Artist", currentArtist);
		CPH.ObsSetBrowserSource("SS_NowPlaying", "AlbumArt_Image", currentAlbumImgUrl);
		CPH.RunAction("Cam Controller - Now Playing");
		//CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterEnabled\":false}", spotifyWebsocket);
		//CPH.Wait(10);
		//CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterEnabled\":false}", spotifyWebsocket);
		//CPH.Wait(10);
		//// artist Update
		//string jsonFormat_artistUpdate = JsonConvert.SerializeObject(new { inputName = "[TS] artistName", inputSettings = new { text = currentArtist }, overlay = true });
		//// sauvegarde = CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] artistName\",\"inputSettings\":{\"text\":\"" + currentArtist + "\"},\"overlay\":true}", spotifyWebsocket);
		//CPH.ObsSendRaw("SetInputSettings", jsonFormat_artistUpdate, spotifyWebsocket);
		////
		////
		//// Song  update 
		//string jsonFormat_songUpdate = JsonConvert.SerializeObject(new { inputName = "[TS] songName", inputSettings = new { text = currentSong }, overlay = true });
		//// sauvegarde = CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] songName\",\"inputSettings\":{\"text\":\"" + currentSong + "\"},\"overlay\":true}", spotifyWebsocket);
		//CPH.ObsSendRaw("SetInputSettings", jsonFormat_songUpdate, spotifyWebsocket);
		////
		////
		//CPH.Wait(10);
		//// queued artist & Song  update 
		//string jsonFormat_queueUpdate = JsonConvert.SerializeObject(new { inputName = "[TS] next Artist + Song", inputSettings = new { text = $"{queuedArtistName} - {queuedSongName}" }, overlay = true });
		//// sauvegarde = CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] next Artist + Song\",\"inputSettings\":{\"text\":\"" + queuedArtistName + " - " + queuedSongName + "\"},\"overlay\":true}", spotifyWebsocket);
		//CPH.ObsSendRaw("SetInputSettings", jsonFormat_queueUpdate, spotifyWebsocket);
		///*
		//// artist Update
		//CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] artistName\",\"inputSettings\":{\"text\":\"" + currentArtist + "\"},\"overlay\":true}", spotifyWebsocket);
		//// Song  update 
		//CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] songName\",\"inputSettings\":{\"text\":\"" + currentSong + "\"},\"overlay\":true}", spotifyWebsocket);
		//CPH.Wait(10);
		//// queued artist & Song  update 
		//CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[TS] next Artist + Song\",\"inputSettings\":{\"text\":\"" + queuedArtistName + " - " + queuedSongName + "\"},\"overlay\":true}", spotifyWebsocket);
		//*/
		//		CPH.Wait(10);
		//		// IMG update
		//		CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[BS] artworkUrl\",\"inputSettings\":{\"height\":300,\"shutdown\":true,\"url\":\"" + currentAlbumImgUrl + "\",\"width\":300},\"overlay\":true}", spotifyWebsocket);
		//		CPH.Wait(10);
		//		CPH.ObsSendRaw("SetInputSettings", "{\"inputName\":\"[BS] artworkUrl - 2\",\"inputSettings\":{\"height\":300,\"shutdown\":true,\"url\":\"" + currentAlbumImgUrl + "\",\"width\":300},\"overlay\":true}", spotifyWebsocket);
		//		CPH.Wait(10);
		//		// Enable 0° filter
		//		CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_0°\",\"filterEnabled\":true}", spotifyWebsocket);
		//		CPH.Wait(10);
		//		CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_0°\",\"filterEnabled\":true}", spotifyWebsocket);
		//		CPH.Wait(10);
		//		// Update Duration for 360° filter
		//		CPH.ObsSendRaw("SetSourceFilterSettings", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterSettings\":{\"duration\":" + durationMSCrossfaded + ",\"easing_match\":0,\"filter\":\"radial_fill_degrees_shader\",\"setting_float\":360,\"setting_name\":\"Fill\",\"value_type\":2},\"overlay\":true}", spotifyWebsocket);
		//		CPH.Wait(10);
		//		CPH.ObsSendRaw("SetSourceFilterSettings", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterSettings\":{\"duration\":" + durationMSCrossfaded + ",\"easing_match\":0,\"filter\":\"radial_fill_degrees_shader\",\"setting_float\":360,\"setting_name\":\"Fill\",\"value_type\":2},\"overlay\":true}", spotifyWebsocket);
		//		CPH.Wait(10);
		//		CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"[CS] progressBar_tiny\",\"filterName\":\"fill_360°\",\"filterEnabled\":true}", spotifyWebsocket);
		//		CPH.Wait(10);
		//		CPH.ObsSendRaw("SetSourceFilterEnabled", "{\"sourceName\":\"progressBar-roly\",\"filterName\":\"Fill_360°\",\"filterEnabled\":true}", spotifyWebsocket);
	}

	public async Task<string> Spotify_NowPlaying_ResultAsync()
	{
		int limit = 1;
		string token = CPH.GetGlobalVar<string>("accessToken_sp2sb", true);
		string url = "https://api.spotify.com/v1/me/player";
		_httpClient.DefaultRequestHeaders.Clear();
		_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
		string exceptionJson = @"
{
  ""item"": {
    ""id"": ""spot2sb"",
    ""duration_ms"": 0,
    ""name"": ""spot2sb"",
    ""artists"": [
      {
        ""name"": ""spot2sb""
      }
    ],
    ""album"": {
      ""images"": [
        {
          ""url"": ""https://i.imgur.com/eX4G6my.png""
        }
      ]
    }
  }
}";
		try
		{
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				if (response.Headers.RetryAfter != null)
				{
					var rateLimitRetryAfter = response.Headers.RetryAfter;
					CPH.LogError(" SPOT2SB " + rateLimitRetryAfter.ToString());
				}

				tokenSwapFailures = 0;
				string content = await response.Content.ReadAsStringAsync();
				//CPH.LogInfo($"SPOT2SB - NowPlaying_Result: {content}");
				return content;
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				tokenSwapFailures++;
				CPH.LogError($"SPOT2SB - Attempting token refresh for Spotify API. [{tokenSwapFailures}]");
				CPH.Wait(1000);
				if (tokenSwapFailures >= 5)
				{
					CPH.LogInfo($"SPOT2SB - Stopping watcher due to excessive token swap failures.");
					Dispose();
					return exceptionJson;
				}
				else
				{
					CPH.LogInfo($"SPOT2SB : [" + (int)response.StatusCode + "] The access token expired ");
					// Log the details of the response
					string responseContent = await response.Content.ReadAsStringAsync();
					//CPH.LogInfo($"SPOT2SB :{responseContent} "+(int)response.StatusCode);
					// Run action to refresh token
					CPH.RunAction("SPOT TO SB - Refresh Token Swap", true);
					CPH.Wait(1000);
					return await Spotify_NowPlaying_ResultAsync();
				}
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				if (!badRequestLogged)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					CPH.LogInfo($"SPOT2SB - Bad Request - you are probably configuring your client Id & client Secret ");
					badRequestLogged = true;
				}

				return exceptionJson;
			}
			else if ((int)response.StatusCode == 429)
			{
				if (response.Headers.RetryAfter != null)
				{
					var rateLimitRetryAfter = response.Headers.RetryAfter;
					CPH.LogError("SPOT2SB " + rateLimitRetryAfter.ToString());
					CPH.SendMessage("SPOT2SB Rate Limit Reached " + rateLimitRetryAfter.ToString());
					TimeSpan? retryAfter = response.Headers.RetryAfter.Delta;
					int retryAfterIntSec = 60; // Valeur par défaut en cas de problème avec Retry-After
					if (retryAfter.HasValue)
					{
						retryAfterIntSec = (int)retryAfter.Value.TotalSeconds;
						CPH.LogError("retry after " + retryAfterIntSec.ToString());
					}

					timer.Stop();
					CPH.LogError("SPOT2SB timer.Stop();");
					await Task.Delay(retryAfterIntSec * 1000);
					timer.Start();
					CPH.LogError("SPOT2SB timer.Start();");
					return exceptionJson;
				}
				else
				{
					CPH.LogDebug("SPOT2SB - Retry-After header is null");
					return exceptionJson;
				}
			}

			return $"{{\"statusCode\": {response.StatusCode}}}";
		}
		catch (HttpRequestException ex)
		{
			CPH.LogDebug($"SPOT2SB - Exception status code nowPlayingResult: {ex.Message}");
			return exceptionJson;
		}
	}

	private (string songName, string artistName, int duration, string albumArtUrl, bool isPlaying, string trackId, string songLink) ExtractSongData(string json)
	{
		string songName = "spot2sb";
		string artistName = "spot2sb";
		string albumArtUrl = "https://i.imgur.com/eX4G6my.png";
		string trackId = "noId";
		string songLink = "noLink";
		int duration = 0;
		bool isPlaying = false;
		try
		{
			if (json != null)
			{
				dynamic root = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
				if (root != null && root.item != null)
				{
					trackId = root.item.id.ToString();
					duration = root.item.duration_ms;
					songName = ReplaceSpecialCharacters(root.item.name.ToString());
					artistName = ReplaceSpecialCharacters(root.item.artists[0].name.ToString());
					albumArtUrl = ReplaceSpecialCharacters(root.item.album.images[0].url.ToString());
					songLink = ReplaceSpecialCharacters(root.item.external_urls.spotify.ToString());
				}

				if (root != null && root.is_playing != null)
					isPlaying = (bool)root.is_playing;
			}
		}
		catch (Exception ex)
		{
			CPH.LogError($" ---------------------     SPOT2SB - Error while extracting song data: {ex.Message}");
		}

		return (songName, artistName, duration, albumArtUrl, isPlaying, trackId, songLink);
	}

	public async Task<string> Spotify_Queue_ResultAsync()
	{
		string token = CPH.GetGlobalVar<string>("accessToken_sp2sb", true);
		string url = "https://api.spotify.com/v1/me/player/queue";
		try
		{
			//  using (var client = new HttpClient())
			//   {
			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				//CPH.LogInfo("SPOT2SB - Queue_Result");
				string jsonResponse = await response.Content.ReadAsStringAsync();
				//CPH.LogInfo("Received JSON from Spotify Queue API: " + jsonResponse);
				return await response.Content.ReadAsStringAsync();
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
			{
				CPH.SendMessage($"{{\"statusCode\": {response.StatusCode}, \"message\": \"Sorry, you must be Premium to use that command\"}}");
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				CPH.RunAction("SPOT TO SB - Refresh Token Swap", true);
				CPH.Wait(1000);
				return await Spotify_Queue_ResultAsync();
			}
			else if ((int)response.StatusCode == 429)
			{
				int retryAfterIntSec = 60;
				if (response.Headers.RetryAfter != null)
				{
					TimeSpan? retryAfter = response.Headers.RetryAfter.Delta;
					if (retryAfter.HasValue)
					{
						retryAfterIntSec = (int)retryAfter.Value.TotalSeconds;
					}
				}

				CPH.SetGlobalVar("rateLimitRemainingTime_sp2sb", retryAfterIntSec.ToString(), false);
				await Task.Delay(retryAfterIntSec * 1000);
				return await Spotify_Queue_ResultAsync();
			}

			return $"{{\"statusCode\": {response.StatusCode}}}";
			//     }
		}
		catch (HttpRequestException ex)
		{
			CPH.LogDebug($"Exception status code e: {ex.Message}");
			return $"{{\"statusCode\": 500}}";
		}
	}

	private (string queuedSongName, string queuedArtistName) ExtractQueueData(string json)
	{
		string queuedSongName = "no Queued Song";
		string queuedArtistName = "no Queued Artist";
		try
		{
			if (json != null)
			{
				dynamic root2 = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
				if (root2 != null && root2.queue != null && root2.queue.Count > 0)
				{
					//var queueItem = root2.queue[0];
					if (root2.queue[0] != null && root2.queue[0].name != null)
					{
						queuedSongName = root2.queue[0].name.ToString();
						queuedSongName = ReplaceSpecialCharacters(queuedSongName);
					}

					if (root2.queue[0] != null && root2.queue[0].artists != null && root2.queue[0].artists.Count > 0 && root2.queue[0].artists[0].name != null)
					{
						queuedArtistName = root2.queue[0].artists[0].name.ToString();
						queuedArtistName = ReplaceSpecialCharacters(queuedArtistName);
					}

					queuedSongName = Regex.Unescape(queuedSongName);
					queuedArtistName = Regex.Unescape(queuedArtistName);
				}
			}
		}
		catch (Exception ex)
		{
			CPH.LogError($"Error while extracting queue data: {ex.Message}");
		}

		return (queuedSongName, queuedArtistName);
	}
}