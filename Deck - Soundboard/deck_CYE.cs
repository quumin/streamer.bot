using System;

/*Deck - Curb Your Enthusiasm
 * 
 *  Use the camera to zoom in on your abject failure.
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
        str_marker = "『SOUNDBOARD』 " + "CME";
        str_msg = "Wow, what was that...";
        str_media = "CYE.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(str_marker);
        }//if

        CPH.PlaySound(str_path + str_media, f_vol, false);
        CPH.RunAction("Cam Controller - CYE", true);
        CPH.Wait(8000);
        CPH.SendMessage("/me !showemote PicardCringe");
        CPH.Wait(2000);
        CPH.SendMessage("/me WeirdChamping " + str_msg + " WeirdChamping");
        CPH.TtsSpeak("Brian", str_msg, true);
        CPH.Wait(4263);
        CPH.RunAction("Cam Controller", true);

        return true;
    }//Execute
}//CPHInline