using System;

/*Deck - Wow
 * 
 *  Generic influencer sound #2.
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
		markerInfo = "『SOUNDBOARD』 " + "W O W";
		msgOut = "/me Pog We did it EZ HYPERCLAP !";
		mediaOut = "AnimeWOW.mp3";

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