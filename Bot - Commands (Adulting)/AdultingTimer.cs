using System;

/*Adulting Timer
 * 
 *  Check if streaming and then remind me to take a break.
 *  LU: 04-nov-23
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
        bool_srs = CPH.GetGlobalVar<bool>("qminSeriousMode");
        str_path = CPH.GetGlobalVar<string>("qminMediaRoot") + "Adulting.mp3";
        str_msg = "/me marinHey ";
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Try to get the Global Stored AdultRemind
        if (!string.IsNullOrEmpty(CPH.GetGlobalVar<string>("qminAdultRemind")))
        {
            str_msg += CPH.GetGlobalVar<string>("qminAdultRemind");
        }//if
        else
        {
            str_msg += "Why don't you take a small break and do something you been meaning to?";
        }//else

        //If OBS is streaming...
        if (CPH.ObsIsStreaming())
        {
            //... send the mesage.
            CPH.SendMessage($"{str_msg} peepoJoJo");
            //... if Serious Mode is disabled...
            if (!bool_srs)
            {
                //... play the sound.
                CPH.PlaySound($"{str_path}", f_vol);
            }//if
        }//if

        return true;
    }//Execute()
}//CPHInline