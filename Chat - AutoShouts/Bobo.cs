using System;

/*Bobo
 * 
 *	Trigger when Billy is here.
 *  LU: 22-jun-2024
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
        mediaOut = "Whomp.mp3";
        srs = CPH.GetGlobalVar<bool>("qminSeriousMode");
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Send message
        CPH.SendMessage("/me @bobotucci has been Q's best bud for 17 years. Than-Q for coming by, Q always appreciates it quuminL");

        //If Serious Mode is disabled...
        if (!srs)
        {
            //... play the sound.
            CPH.PlaySound(filePath + mediaOut, vol, true);
        }//if

        CPH.DisableAction("bobotucci");
        return true;
    }//Execute()
}//CPHInline