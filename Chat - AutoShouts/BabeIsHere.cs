using System;

/*Babe is Here
 * 
 *  Trigger when Mrs. Q is here.
 *  LU: 22-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string obScene, mediaOut;
        int waitTime;
        bool srs;

        //Initializations
        obScene = "SS_Alerts";
        mediaOut = "BabesHerePog";
        waitTime = 2300;
        srs = CPH.GetGlobalVar<bool>("qminSeriousMode");

        //Send message
        CPH.SendMessage("/me Hey it's @whymusticryy! Thank-Q for coming by you sexy human quuminL you are Q's thiqq or die quuminL");

        //If Serious Mode is disabled...
        if (!srs)
        {
            //... play video.
            CPH.ObsShowSource(obScene, mediaOut);
            CPH.Wait(waitTime);
            CPH.ObsHideSource(obScene, mediaOut);
        }//if

        CPH.DisableAction("whymusticryy");
        return true;
    }//Execute()
}//CPHInline