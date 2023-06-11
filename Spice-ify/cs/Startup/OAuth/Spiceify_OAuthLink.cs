using System;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Web;
using System.Threading.Tasks;

public class CPHInline
{
    HttpListener listener = null;
    public bool Execute()
    {
        int websocket = Convert.ToInt32(args["websocket5.x"].ToString());
        CPH.SetGlobalVar("spotifyWebsocket", websocket, true);
        int crossfadeTime = Convert.ToInt32(args["crossfadeTimeInSeconds"].ToString());
        int crossfadeTimeInMs = crossfadeTime * 1000;
        CPH.SetGlobalVar("crossfade", crossfadeTimeInMs, true);
        string oAuthUrl = "https://accounts.spotify.com/authorize?";
        string clientId = args["clientId"].ToString();
        //Scopes
        string readPlayback = "user-read-playback-state";
        string modPlayback = "user-modify-playback-state";
        string plReadPriv = "playlist-read-private";
        string plModPub = "playlist-modify-public";
        string plModPriv = "playlist-modify-private";
        string readPriv = "user-read-private";
        string readMail = "user-read-email";
        string currentPlay = "user-read-currently-playing";
        string modLibrary = "user-library-modify";
        string scopes = readPlayback + " " + modPlayback + " " + plReadPriv + " " + plModPub + " " + plModPriv + " " + readPriv + " " + readMail + " " + currentPlay + " " + modLibrary;
        string redirectUri = "http://127.0.0.1:1300/spotifyOAuthRedirectUri/";
        string UriUrlEncod = HttpUtility.UrlEncode(redirectUri);
        string urlRequest = oAuthUrl + "&response_type=code&client_id=" + clientId + "&scope=" + scopes + "&redirect_uri=" + UriUrlEncod;
        CPH.SendMessage("Please connect to Spotify using the Webpage that opened to obtain your token.");
        System.Diagnostics.Process.Start(urlRequest);
        try
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:1300/spotifyOAuthRedirectUri/");
            listener.Start();
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();
            listener.Close();
        }
        catch (WebException e)
        {
            CPH.LogDebug(e.Status.ToString());
        }

        return true;
    }

    public async Task HandleIncomingConnections()
    {
        bool runServer = true;
        while (runServer)
        {
            CPH.LogDebug("Server Waiting ...");
            HttpListenerContext context = await listener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse resp = context.Response;
            string msg = request.Url.ToString();
            string code = msg.Remove(0, 52);
            CPH.SetGlobalVar("code", code, false);
            //--------------------------------------▼ http://127.0.0.1:1300/spotifyOAuthRedirectUri/ ▼ 
            string pageData = "<!DOCTYPE>" + "<html>" + "<head>" + "<title>SPOTIFY TO STREAMER.BOT</title>" + "</head>" + "<body style=background-position:top;margin-top:30px;background-position-y:50px;background-repeat:no-repeat;background-color:#000000;line-height:100%;backgroundAttachment:fixed; background='https://spottosb.pagesperso-orange.fr/spottosb/images/spot--to-sb2.png';>" + "<font face='Bahnschrift Light'>" + "<h1 style=text-align:center;color:#ffffff;>Spotify Token Acquired</h1>" + "<div style=text-align:center;color:#ffff33;line-height:50%;>&#8659; Premium only &#8659;</div>" + "<h2 style=text-align:center;color:#ffffff;line-height:50%;>!sr artistName , songName </h2>" + "<h3 style=text-align:center;color:#ffffff;line-height:50%;>will add the query to the queue</h3>" + "<h2 style=text-align:center;color:#ffffff;><br></h2>" + "<div><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br></div>" + "<h2 style=text-align:center;color:#ffffff;>Other commands :</h2>" + "<h3 style=text-align:center;color:#ffffff;line-height:50%;>&#9745; !sPrev : Skip to Previous Song</h3>" + "<h3 style=text-align:center;color:#ffffff;line-height:50%;>&#9745; !sNow : Show Now Playing Song</h3>" + "<h3 style=text-align:center;color:#ffffff;line-height:50%;>&#9745; !sPlay : Play / Resume</h3>" + "<h3 style=text-align:center;color:#ffffff;line-height:50%;>&#9745; !sPause : Pause </h3>" + "<h3 style=text-align:center;color:#ffffff;line-height:50%;>&#9745; !sNext : Skip to Next Song</h3>" + "<h3 style=text-align:center;color:#ffffff;line-height:50%;>&#9745; !sQueue : Show Next Song in Queue</h3>" + "<h2 style=text-align:center;color:#ffffff;>You Can Close This Page</h2>" + "</body>" + "</html>";
            //---------------------------------
            byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData));
            resp.ContentType = "text/html";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            await context.Response.OutputStream.WriteAsync(data, 0, data.Length);
            CPH.LogDebug("code : " + code);
            resp.Close();
            runServer = false;
            listener.Close();
        }
    }
}