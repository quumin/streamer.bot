using System;

/*Wolf Howl
 * 
 *  Trigger when Roel is here.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_path, str_media;
        bool bool_serious;

        //Initializations
        str_path = "W:\\Streaming\\Media\\Sounds\\";
        str_media = "WolfHowl.mp3";
        bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

        //Send message
        CPH.SendMessage("/me dataHuh Yo whaddup @gryze_wolf! Thanks for including Q in the wolf pack brother-man quuminL");

        //If Serious Mode is disabled...
        if (!bool_serious)
        {
            //... play the sound.
            CPH.PlaySound(str_path + str_media, 0.15f, true);
        }//if

        CPH.DisableAction("gryze_wolf");
        return true;
    }//Execute()
}//CPHInline