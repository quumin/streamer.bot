using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

public class Root
{
    public string access_token { get; set; }

    public string expires_in { get; set; }
}

//------------------------------------------------//
//----- [ POST ] RefreshToken  ---@leblux_tv------//
//------------------------------------------------//
public class CPHInline
{
    public bool Execute()
    {
        //------------------------------------------------------------▼ Call to RefreshToken();
        string json = RefreshToken();
        Root root = JsonConvert.DeserializeObject<Root>(json);
        string accessToken = root.access_token;
        CPH.LogDebug(json);
        CPH.SetGlobalVar("accessToken", accessToken, true);
        return true;
    }

    //------------------------------------------------------------▼ Script for RefreshToken();
    public string RefreshToken()
    {
        string getTokenUrl = "https://accounts.spotify.com/api/token";
        var clientId = CPH.GetGlobalVar<string>("spotifyClientId", true);
        var clientSecret = CPH.GetGlobalVar<string>("spotifyClientSecret", true);
        string refreshToken = CPH.GetGlobalVar<string>("refreshToken", true);
        var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret)));
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(getTokenUrl);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.Accept = "application/json";
        webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);
        var request = ("grant_type=refresh_token&refresh_token=" + refreshToken);
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
            CPH.LogDebug("status code tokenSwap: " + statusCodeResponse);
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