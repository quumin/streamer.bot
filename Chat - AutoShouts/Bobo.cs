using System;

/*Bobo
 * 
 *	Trigger when Billy is here.
 *	LU: 04-nov-23
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_path, str_media;
        bool bool_serious;
        float f_vol;

        //Initializations
        str_path = CPH.GetGlobalVar<string>("qminMediaRoot");
        str_media = "Whomp.mp3";
        bool_serious = CPH.GetGlobalVar<bool>("qminSeriousMode");
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Send message
        CPH.SendMessage("/me @bobotucci has been Q's best bud for 17 years. Than-Q for coming by, Q always appreciates it quuminL");

        //If Serious Mode is disabled...
        if (!bool_serious)
        {
            //... play the sound.
            CPH.PlaySound(str_path + str_media, f_vol, true);
        }//if

        CPH.DisableAction("bobotucci");
        return true;
    }//Execute()
}//CPHInline