using System;

/*Cooler Master
 * 
 *  Handle CoolerMaster Message.
 *  LU: 21-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string filePath, mediaOut;
        bool srs;
        float vol;

        //Initializations
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        mediaOut = "CoolCoolerCoolest.mp3";
        srs = CPH.GetGlobalVar<bool>("qminSeriousMode");
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Send message
        CPH.SendMessage("/me Pog Q is an Coolermaster Affiliate PogU https://t.co/OhdqhTMT1l <- Click the link to get some cool deals TricksterWink");

        //If Serious Mode is disabled...
        if (!srs)
        {
            //... play the sound.
            CPH.PlaySound(filePath + mediaOut, vol, true);
        }//if

        return true;
    }//Execute()
}//CPHInline