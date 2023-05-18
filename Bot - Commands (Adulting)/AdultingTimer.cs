using System;

/*Adulting Timer
 * 
 *  Check if streaming and then remind me to take a break.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        bool bool_srs;
        string str_path, str_msg;
        float f_vol;

        //Initializations
        bool_srs = CPH.GetGlobalVar<bool>("seriousMode");
        str_path = CPH.GetGlobalVar<string>("mediaRoot");
        str_msg = "/me marinHey ";
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");

        //Try to get the Global Stored AdultRemind
        if (!string.IsNullOrEmpty(CPH.GetGlobalVar<string>("adultRemind")))
        {
            str_msg += CPH.GetGlobalVar<string>("adultRemind");
        }//if
        else
        {
            str_msg += "Why don't you take a small break and do something you been meaning to?";
        }//else

        //If OBS is streaming...
        if (CPH.ObsIsStreaming())
        {
            //... send the mesage.
            CPH.SendMessage(str_msg + " peepoJoJo");
            //... if Serious Mode is disabled...
            if (!bool_srs)
            {
                //... play the sound.
                CPH.PlaySound(str_path + "Adulting.mp3", f_vol);
            }//if
        }//if

        return true;
    }//Execute()
}//CPHInline