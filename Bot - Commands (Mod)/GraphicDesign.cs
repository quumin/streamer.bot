using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string str_usr, str_msg;
		bool bool_serious;

        //Initializations
        str_usr = args["userName"].ToString();
        str_msg = "Oh no, not again... Ban incoming.";
        bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

		//Send message
        CPH.SendMessage("/me Aware " + str_msg + " YEP ModTime");
		CPH.TwitchTimeOutUser(str_usr, 5, "Mentioned followers/graphic design - you sicken me.", true);

        //If Serious Mode is disabled...
        if (!bool_serious)
		{
			//... play the TTS.
			CPH.TtsSpeak("Brian", str_msg);
		}//if

        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker("Graphic design...");
        }//if

        return true;
	}//Execute()
}//CPHInline