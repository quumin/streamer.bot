using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

//----------------------------------------------------------------------//
//------ [ GET ] DEVICE ]----------------------@Le_Blux-----------------//
//----------------------------------------------------------------------//
public class Device
{
    public string id { get; set; }

    public bool is_active { get; set; }

    public bool is_private_session { get; set; }

    public bool is_restricted { get; set; }

    public string name { get; set; }

    public string type { get; set; }

    public int volume_percent { get; set; }
}

public class Root
{
    public List<Device> devices { get; set; }
}

public class CPHInline
{
    public bool Execute()
    {
        string token = CPH.GetGlobalVar<string>("accessToken", true);
        string json = Spotify_Get_Devices_Id().ToString();
        string computerName = Dns.GetHostName();
        Root root = JsonConvert.DeserializeObject<Root>(json);
        int devicesNumberFound = root.devices.Count;
        string dNumb = devicesNumberFound.ToString();
        CPH.LogDebug(json.ToString());
        List<string> devicesId = new List<string>();
        CPH.SetGlobalVar("devicesId", devicesId, true);
        var devicesIdList = CPH.GetGlobalVar<List<string>>("devicesId", true);
        for (int i = 0; i <= devicesNumberFound - 1; i++)
        {
            devicesIdList.Add(root.devices[i].id);
            CPH.SetGlobalVar("devicesIdList", devicesIdList, true);
            if (root.devices[i].type == "Computer" && root.devices[i].name == computerName)
            {
                CPH.SetGlobalVar("deviceId", root.devices[i].id, true);
                CPH.SendMessage("default device selected : " + root.devices[i].name + " " + root.devices[i].type);
            }
        }

        if (devicesNumberFound > 1)
        {
            CPH.SendMessage("if you wish to change the default device use !SpotifyDevice");
        }

        return true;
    }

    public object Spotify_Get_Devices_Id()
    {
        string token = CPH.GetGlobalVar<string>("accessToken", true);
        string url = "https://api.spotify.com/v1/me/player/devices";
        string query = url;
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(query);
        webRequest.Method = "GET";
        webRequest.ContentType = "application/json";
        webRequest.Accept = "application/json";
        webRequest.Headers.Add("Authorization", "Bearer " + token);
        String json = null;
        {
            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
        }

        return json;
    }
}