using System;
using System.Collections.Generic;

/*Shoutout Handler
 * 
 *  Check username and run a shoutouts if the user exists and if there is not currently a shoutout ongoing.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] userInfo;
        bool shoutoutActive;

        //Initializations
        userInfo = new string[]
        {
            args["targetUserLogin"].ToString(),
            args["targetUserDisplayName"].ToString()
        };
        shoutoutActive = CPH.GetGlobalVar<bool>("qminSoActive", false);

        //Log it
        CPH.LogInfo("『SHOUTOUT』Active: " + shoutoutActive);

        //If a shoutout is not active...
        if (!shoutoutActive)
        {
            //... if streaming...
            if (CPH.ObsIsStreaming())
            {
                //... send shoutout and start warning timer.
                CPH.TwitchSendShoutoutByLogin(userInfo[0]);
                CPH.SetGlobalVar("qminSoActive", true, false);
                CPH.EnableTimer("soTimer");
            }//if

        }//if
        else
        {
            //... otherwise inform the broadcaster to wait.
            CPH.SendMessage("/me DataFingerbang Shoutout is still ongoing! DataFingerbang", true);
        }//else

        CPH.SendMessage($"/me !so {userInfo[1]}");
        CPH.SendMessage($"/me DetectedAnomaly2 The Q-mander would like to bring your attention to  lickR @{userInfo[0]} lickL , follow 'em at ​https://twitch.tv/{userInfo[1]} and improve your quuminL function.");

        return true;
    }//Execute()
}//CPHInline