using System;

/*Cooler Master
 * 
 *  Handle CoolerMaster Message.
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
        str_media = "CoolCoolerCoolest.mp3";
        bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

        //Send message
        CPH.SendMessage("/me Pog Q is an Coolermaster Affiliate PogU https://t.co/OhdqhTMT1l <- Click the link to get some cool deals TricksterWink");

        //If Serious Mode is disabled...
        if (!bool_serious)
        {
            //... play the sound.
            CPH.PlaySound(str_path + str_media, 0.15f, true);
        }//if

        return true;
    }//Execute()
}//CPHInline