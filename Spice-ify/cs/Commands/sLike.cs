using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

//------------------------------------------------//
//------ [ PUT ] SAVE / TRACK ---@leblux_tv------//
//------------------------------------------------//

public class CPHInline
{
    public bool Execute()
    {
        //------------------------------------------------------------▼ Call to saveTrack();
        string json = saveTrack();
        CPH.SendMessage("️💚");
        return true;
    }
    //------------------------------------------------------------▼ Script for saveTrack();
    public string saveTrack()
    {
        string url = "https://api.spotify.com/v1/me/tracks";

        string token = CPH.GetGlobalVar<string>("accessToken", true);

        string trackId = CPH.GetGlobalVar<string>("currentTrackId", true);

        string data = "{\"ids\":[\"" + trackId + "\"]}";
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Method = "PUT";
        webRequest.ContentType = "application/json";
        webRequest.Accept = "application/json";
        webRequest.ContentLength = bytes.Length;
        webRequest.Headers.Add("Authorization: Bearer " + token);

        Stream newStream = webRequest.GetRequestStream();
        newStream.Write(bytes, 0, bytes.Length);
        newStream.Close();
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
            if (json.Contains("401"))
            {
                CPH.RunAction("rolly SPOT TO SB - Refresh Token Swap", true);
                saveTrack();
            }
            else if (json.Contains("404"))
            {
                CPH.RunAction("tiny SPOT TO SB - Transfer Playback", true);
                CPH.Wait(2000);
                saveTrack();
            }
            return json;
        }
        return json;
    }
}