using System;

/*Deck - Pufferfish Choke
 * 
 *  Play the sound again!
 *  LU: 4-nov-2023
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
        markerInfo = "『SOUNDBOARD』 " + "Cheauxonit";
        msgOut = "/me WorfCUM Hell Yeah quuminGASM";
        mediaOut = "Pufferfish.mp3";

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