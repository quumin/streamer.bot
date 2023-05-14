using System;

/*Deck - Scared
 * 
 *  Jumpscare in 3... 2... 1...
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
		str_marker = "『SOUNDBOARD』 " + "monkaW";
		str_msg = "/me !showemote monkaChrist";
		str_media = "Inception.mp3";

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