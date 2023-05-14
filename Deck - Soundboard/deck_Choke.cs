using System;

/*Deck - Pufferfish Choke
 * 
 *  Play the sound again!
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
        str_marker = "『SOUNDBOARD』 " + "Cheauxonit";
        str_msg = "/me WorfCUM Hell Yeah quuminGASM";
        str_media = "Pufferfish.mp3";

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