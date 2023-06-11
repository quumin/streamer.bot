using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CPHInline
{
	public bool Execute()
	{
		string accessToken = "";
		string refreshToken = "";
		string json = GetAccessToken();
		JsonTextReader reader = new JsonTextReader(new StringReader(json));
		while (reader.Read())
		{
			string Path = reader.Path.Replace("[", "").Replace("]", "");
			if ((reader.Value != null) && (reader.TokenType.ToString() != "PropertyName") && (Path == "access_token"))
			{
				accessToken = reader.Value.ToString();
			}

			if ((reader.Value != null) && (reader.TokenType.ToString() != "PropertyName") && (Path == "refresh_token"))
			{
				refreshToken = reader.Value.ToString();
			}
		}

		CPH.LogDebug(json.ToString());
		string tokenAcquired = "Access Token Acquired, Now use !sr <artistName> , <songName> to enqueue a song.";
		string color = "green";
		CPH.SendMessage(tokenAcquired);
		CPH.SetGlobalVar("accessToken", accessToken, true);
		CPH.SetGlobalVar("refreshToken", refreshToken, true);
		return true;
	}

	public string GetAccessToken()
	{
		string redirect_uri = "http://127.0.0.1:1300/spotifyOAuthRedirectUri/";
		string getTokenUrl = "https://accounts.spotify.com/api/token";
		string clientId = args["clientId"].ToString();
		CPH.SetGlobalVar("spotifyClientId", clientId, true);
		string clientSecret = args["clientSecret"].ToString();
		CPH.SetGlobalVar("spotifyClientSecret", clientSecret, true);
		string authorization_code = CPH.GetGlobalVar<string>("code", false);
		//request to get the access token
		var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret)));
		HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(getTokenUrl);
		webRequest.Method = "POST";
		webRequest.ContentType = "application/x-www-form-urlencoded";
		webRequest.Accept = "application/json";
		webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);
		var request = ("grant_type=authorization_code&code=" + authorization_code + "&redirect_uri=" + redirect_uri);
		byte[] req_bytes = Encoding.ASCII.GetBytes(request);
		webRequest.ContentLength = req_bytes.Length;
		Stream strm = webRequest.GetRequestStream();
		strm.Write(req_bytes, 0, req_bytes.Length);
		strm.Close();
		//------------------------------------------------------------▼ Response 
		HttpWebResponse resp = null;
		HttpStatusCode statusCode;
		String json = null;
		try
		{
			resp = (HttpWebResponse)webRequest.GetResponse();
			var statusCodeResponse = resp.StatusCode;
			CPH.LogDebug("status code : " + statusCodeResponse);
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
			return json;
		}
		return json;
	}
}