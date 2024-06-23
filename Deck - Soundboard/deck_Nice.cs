using System;

/*Deck - Jojo Nice
 * 
 *  Play the sound where Joseph is looking at totally not his mom Keepo
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
        markerInfo = "『SOUNDBOARD』 " + "N I C E";
        msgOut = "/me !showemote Nice";
        mediaOut = "Jojo_Nice.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(markerInfo);
        }//if

        CPH.PlaySound(filePath + mediaOut, vol, false);
        CPH.SendMessage(msgOut);

        return true;
    }//Execute
}//CPHInline