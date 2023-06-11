using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

//------------------------------------------------//
//----- [ POST ] PREVIOUS SONG ---@leblux_tv------//
//------------------------------------------------//
public class CPHInline
{
    public bool Execute()
    {
        //------------------------------------------------------------▼ Call to SkipToPrevious();
        string json = SkipToPrevious();
        return true;
    }

    //------------------------------------------------------------▼ Script for SkipToPrevious();
    public string SkipToPrevious()
    {
        string baseUrl = "https://api.spotify.com/v1/me/player/previous?";
        string deviceId = CPH.GetGlobalVar<string>("deviceId", true);
        string url = baseUrl + "&device_id=" + deviceId;
        string token = CPH.GetGlobalVar<string>("accessToken", true);
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/json";
        webRequest.Accept = "application/json";
        webRequest.ContentLength = 0;
        webRequest.Headers.Add("Authorization: Bearer " + token);
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
            if (json.Contains("403"))
            {
                CPH.RunAction("tiny SPOT TO SB - Transfer Playback", true);
                CPH.Wait(2000);
                SkipToPrevious();
            }
            else if (json.Contains("404"))
            {
                CPH.RunAction("tiny SPOT TO SB - Transfer Playback", true);
                CPH.Wait(2000);
                SkipToPrevious();
            }
            else if (json.Contains("401"))
            {
                CPH.RunAction("tiny SPOT TO SB - Refresh Token Swap", true);
                SkipToPrevious();
            }

            return json;
        }

        return json;
    }
}