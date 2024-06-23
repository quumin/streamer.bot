using System;

/*Deck - Nuke
 * 
 *  YAMERO the chat until you say otherwise.
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
        markerInfo = "『SOUNDBOARD』 " + "NUKE";
        msgOut = "/me monkaTOS Time to STOP boys disGUSTING";
        mediaOut = "Jojo_ZaWarudo.mp3";

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker(markerInfo);
        }//if


        CPH.PlaySound(filePath + mediaOut, vol, false);
        CPH.SendMessage(msgOut);
        CPH.TwitchEmoteOnly();

        return true;
    }//Execute
}//CPHInline