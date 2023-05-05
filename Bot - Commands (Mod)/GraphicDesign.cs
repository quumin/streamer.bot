using System;
using System.Globalization;

/*Graphic Design
 * 
 *  Timeout bots.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_usr, str_msg;
        bool bool_serious;
        int int_fa;

        //Initializations
        str_usr = args["userName"].ToString();
        str_msg = "Oh no, not again... Ban incoming.";
        bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

        try
        {
            //	to get follow age in seconds.
            int_fa = int.Parse(args["followAgeSeconds"].ToString(), NumberStyles.AllowThousands);
        }//try
        catch (Exception e)
        {
            //	not following/error
            int_fa = 0;
        }//catch

        //Send message

        //If Follow age is less than 5mins...
        if (int_fa < 600)
        {
            //... send message & timeout.
            CPH.SendMessage("/me Aware " + str_msg + " YEP ModTime");
            CPH.TwitchTimeoutUser(str_usr, 5, "Mentioned followers/graphic design within first 10 minutes - you sicken me. Go peddle your shit somewhere else.");

            //... if Serious Mode is disabled...
            if (!bool_serious)
            {
                //... play the TTS.
                CPH.TtsSpeak("Brian", str_msg);
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