using System;

/*Deck - Vine Boom
 * 
 *  Play the vine boom sound.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string filePath, markerInfo, mediaOut;
        float vol;

        //Initializations
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");
        markerInfo = "『SOUNDBOARD』 " + "BOOM";
        mediaOut = "VineBoom.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(markerInfo);
        }//if

        CPH.PlaySound(filePath + mediaOut, vol, true);

        return true;
    }//Execute
}//CPHInline