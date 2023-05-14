using System;

/*Deck - Super Mario Death
 * 
 *  Use TTS and inform everyone what they just saw was intentional Kappa
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
        str_marker = "『SOUNDBOARD』 " + "Died Horribly";
        str_msg = "Oh no no no quuminQQ";
        str_media = "SuperMarioDeath.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(str_marker);
        }//if


        CPH.PlaySound(str_path + str_media, f_vol, false);
        CPH.SendMessage("/me " + str_msg);
        CPH.TtsSpeak("Takumi", str_msg, true);

        return true;
    }//Execute
}//CPHInline