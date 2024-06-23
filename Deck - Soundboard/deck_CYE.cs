using System;

/*Deck - Curb Your Enthusiasm
 * 
 *  Use the camera to zoom in on your abject failure.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string filePath, markerInfo, msgOut, mediaOut;
        float vol;

        //Initializations
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");
        markerInfo = "『SOUNDBOARD』 " + "CME";
        msgOut = "Wow, what was that...";
        mediaOut = "CYE.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(markerInfo);
        }//if

        CPH.PlaySound(filePath + mediaOut, vol, false);
        CPH.RunAction("Cam Controller - CYE", true);
        CPH.Wait(8000);
        CPH.SendMessage("/me !showemote PicardCringe");
        CPH.Wait(2000);
        CPH.SendMessage("/me WeirdChamping " + msgOut + " WeirdChamping");
        CPH.TtsSpeak("Brian", msgOut, true);
        CPH.Wait(4263);
        CPH.RunAction("Cam Controller", true);

        return true;
    }//Execute
}//CPHInline