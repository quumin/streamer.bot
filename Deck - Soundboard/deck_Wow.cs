using System;

/*Deck - Wow
 * 
 *  Generic influencer sound #2.
 *  LU: 4-nov-2023
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
		str_path = CPH.GetGlobalVar<string>("qminMediaRoot");
		f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");
		str_marker = "『SOUNDBOARD』 " + "W O W";
		str_msg = "/me Pog We did it EZ HYPERCLAP !";
		str_media = "AnimeWOW.mp3";

		//If I'm live...
		if (CPH.ObsIsStreaming())
		{
			//... create a marker.
			CPH.CreateStreamMarker(str_marker);
		}//if

		CPH.PlaySound(str_path + str_media, f_vol, false);
		CPH.SendMessage(str_msg);

		return true;
	}//Execute
}//CPHInline