using System;

/*Deck - Oh No!
 * 
 *  Johnathan Joestar is scared monkaW
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_path, str_marker, str_msg, str_media;
        float f_vol;

        //Initializations
        str_path = CPH.GetGlobalVar<string>("mediaRoot");
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");
        str_marker = "『SOUNDBOARD』 " + "Oh No!";
        str_msg = "/me Menacing NOOOOOOOOOO Menacing";
        str_media = "Jojo_OhNo.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(str_marker);
        }//if

        CPH.PlaySound(str_path + str_media, f_vol, false);
        CPH.SendMessage(str_msg);

        return true;
    }//Execute
}//CPHInline