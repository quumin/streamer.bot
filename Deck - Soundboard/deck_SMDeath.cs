using System;

/*Deck - Super Mario Death
 * 
 *  Use TTS and inform everyone what they just saw was intentional Kappa
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
        markerInfo = "『SOUNDBOARD』 " + "Died Horribly";
        msgOut = "Oh no no no quuminQQ";
        mediaOut = "SuperMarioDeath.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(markerInfo);
        }//if


        CPH.PlaySound(filePath + mediaOut, vol, false);
        CPH.SendMessage("/me " + msgOut);
        CPH.TtsSpeak("Takumi", msgOut, true);

        return true;
    }//Execute
}//CPHInline