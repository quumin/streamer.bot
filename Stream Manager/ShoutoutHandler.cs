using System;
using System.Collections.Generic;

/*Shoutout Handler
 * 
 *  Check username and run a shoutouts if the user exists and if there is not currently a shoutout ongoing.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_usr;
        bool bool_so;

        //Initializations
        str_usr = new string[]
        {
            args["targetUserLogin"].ToString(),
            args["targetUserDisplayName"].ToString()
        };
        bool_so = CPH.GetGlobalVar<bool>("globalSoActive", false);

        //Log it
        CPH.LogInfo("『SHOUTOUT』Active: " + bool_so);

        //If a shoutout is not active...
        if (!bool_so)
        {
            //... if streaming...
            if (CPH.ObsIsStreaming())
            {
                //... send shoutout and start warning timer.
                CPH.TwitchSendShoutoutByLogin(str_usr[0]);
                CPH.SetGlobalVar("globalSoActive", true, false);
                CPH.EnableTimer("soTimer");
            }//if

        }//if
        else
        {
            //... otherwise inform the broadcaster to wait.
            CPH.SendMessage("/me DataFingerbang Shoutout is still ongoing! DataFingerbang", true);
        }//else

        CPH.SendMessage($"/me !so {str_usr[1]}");
        CPH.SendMessage($"/me DetectedAnomaly2 The Q-mander would like to bring your attention to  lickR @{str_usr[0]} lickL , follow 'em at ​https://twitch.tv/{str_usr[1]} and improve your quuminL function.");

        return true;
    }//Execute()
}//CPHInline