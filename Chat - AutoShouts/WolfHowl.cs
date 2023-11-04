using System;

/*Wolf Howl
 * 
 *  Trigger when Roel is here.
 *  LU: 04-nov-23
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
        str_media = "WolfHowl.mp3";
        bool_serious = CPH.GetGlobalVar<bool>("qminSeriousMode");
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Send message
        CPH.SendMessage("/me dataHuh Yo whaddup @gryze_wolf! Thanks for including Q in the wolf pack brother-man quuminL");

        //If Serious Mode is disabled...
        if (!bool_serious)
        {
            //... play the sound.
            CPH.PlaySound(str_path + str_media, f_vol, true);
        }//if

        CPH.DisableAction("gryze_wolf");
        return true;
    }//Execute()
}//CPHInline