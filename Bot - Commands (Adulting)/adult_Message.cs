using System;

/*Adulting - Message Out
 * 
 *  Check if streaming and then remind me to take a break.
 *  LU: 20-oct-24
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        bool srs;
        string filePath, msgOut;
        float vol;

        //Initializations
        srs = CPH.GetGlobalVar<bool>("qminSeriousMode");
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        msgOut = "/me marinHey ";
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //If the global has something in it...
        if (!string.IsNullOrEmpty(CPH.GetGlobalVar<string>("qminAdultRemind")))
        {
            //.... then update the message accordingly.
            msgOut += CPH.GetGlobalVar<string>("qminAdultRemind");
        }//if
        else
        {
            //... otherwise go for default text.
            msgOut += "Why don't you take a small break or drink some water?";
        }//else

        //If OBS is streaming...
        if (CPH.ObsIsStreaming())
        {
            //... send the mesage.
            CPH.SendMessage($"{msgOut} peepoJoJo");
            //... if Serious Mode is disabled...
            if (!srs)
            {
                //... play the sound.
                CPH.PlaySound($"{filePath}Adulting.mp3", vol);
            }//if
        }//if

        return true;
    }//Execute()
}//CPHInline