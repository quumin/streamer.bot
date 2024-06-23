using System;

/*Adulting Timer
 * 
 *  Check if streaming and then remind me to take a break.
 *  LU: 21-jun-24
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
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot") + "Adulting.mp3";
        msgOut = "/me marinHey ";
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Try to get the Global Stored AdultRemind
        if (!string.IsNullOrEmpty(CPH.GetGlobalVar<string>("qminAdultRemind")))
        {
            msgOut += CPH.GetGlobalVar<string>("qminAdultRemind");
        }//if
        else
        {
            msgOut += "Why don't you take a small break and do something you been meaning to?";
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
                CPH.PlaySound($"{filePath}", vol);
            }//if
        }//if

        return true;
    }//Execute()
}//CPHInline