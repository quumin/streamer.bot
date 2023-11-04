using System;

/*Babe is Here
 * 
 *  Trigger when Mrs. Q is here.
 *  LU: 03-nov-23
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_ss, str_media;
        int int_wait;
        bool bool_srs;

        //Initializations
        str_ss = "SS_Alerts";
        str_media = "BabesHerePog";
        int_wait = 2300;
        bool_srs = CPH.GetGlobalVar<bool>("qminSeriousMode");

        //Send message
        CPH.SendMessage("/me Hey it's @whymusticryy! Thank-Q for coming by you sexy human quuminL you are Q's thiqq or die quuminL");

        //If Serious Mode is disabled...
        if (!bool_srs)
        {
            //... play video.
            CPH.ObsShowSource(str_ss, str_media);
            CPH.Wait(int_wait);
            CPH.ObsHideSource(str_ss, str_media);
        }//if

        CPH.DisableAction("whymusticryy");
        return true;
    }//Execute()
}//CPHInline