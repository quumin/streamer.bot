using System;

/*Wolf Howl
 * 
 *  Trigger when Roel is here.
 *  LU: 23-jun-2024
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
        mediaOut = "WolfHowl.mp3";
        srs = CPH.GetGlobalVar<bool>("qminSeriousMode");
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Send message
        CPH.SendMessage("/me dataHuh Yo whaddup @gryze_wolf! Thanks for including Q in the wolf pack brother-man quuminL");

        //If Serious Mode is disabled...
        if (!srs)
        {
            //... play the sound.
            CPH.PlaySound(filePath + mediaOut, vol, true);
        }//if

        CPH.DisableAction("gryze_wolf");
        return true;
    }//Execute()
}//CPHInline