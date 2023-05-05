using System;

/*Workout or Break Reminder
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
        string str_path;
        float f_vol;

        //Initializations
        bool_srs = CPH.GetGlobalVar<bool>("seriousMode");
        str_path = "W:\\Streaming\\Media\\Sounds\\";
        f_vol = 0.025f;

        //If OBS is streaming...
        if (CPH.ObsIsStreaming())
        {
            //... send the mesage.
            CPH.SendMessage("/me marinHey Why don't you take a small break and do something you been meaning to? peepoJoJo");
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