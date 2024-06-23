﻿using System;

/*Deck - Oh No!
 * 
 *  Johnathan Joestar is scared monkaW
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
        markerInfo = "『SOUNDBOARD』 " + "Oh No!";
        msgOut = "/me Menacing NOOOOOOOOOO Menacing";
        mediaOut = "Jojo_OhNo.mp3";

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