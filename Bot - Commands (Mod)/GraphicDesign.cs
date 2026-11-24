using System;
using System.Globalization;

/*Graphic Design
 * 
 *  Timeout bots.
 *  LU: 27-apr-26
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string msgOut;
        bool srs;
        int followAge;

        //Initializations
        CPH.TryGetArg("userName", out string usrName);
        CPH.TryGetArg("followAgeSeconds", out string argFollowAge);
        msgOut = "Oh no, not again... Ban incoming.";
        srs = CPH.GetGlobalVar<bool>("qminSeriousMode");

        try
        {
            //	to get follow age in seconds.
            followAge = int.Parse(argFollowAge, NumberStyles.AllowThousands);
        }//try
        catch (Exception e)
        {
            //	not following/error
            followAge = 0;
        }//catch

        //Send message

        //If Follow age is less than 5mins...
        if (followAge < 600)
        {
            //... send message & timeout.
            CPH.SendMessage($"/me Aware {msgOut} YEP ModTime");
            CPH.TwitchTimeoutUser(usrName, 5, "Mentioned followers/graphic design within first 10 minutes - you sicken me. Go peddle your shit somewhere else.");

            //... if Serious Mode is disabled...
            if (!srs)
            {
                //... play the TTS.
                CPH.TtsSpeak("Brian", msgOut);
            }//if

            //... if OBS is streaming...
            if (CPH.ObsIsStreaming())
            {
                //... create a marker.
                CPH.CreateStreamMarker("Graphic design...");
            }//if
        }//if

        return true;
    }//Execute()
}//CPHInline