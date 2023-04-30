using System;

/*Scene Checker
 * 
 *	Check which scene we're currently in and make adjustments.
 * 
 */

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string str_scene, str_ss;
		bool bool_creds;
		int int_wait, int_com;
		
		//Initializations
		str_scene = CPH.ObsGetCurrentScene();
		str_ss = "SS_MidScreen";
		bool_creds = false;
		int_wait = 12000;
		int_com = 180;
		CPH.LogDebug("SCENE: " + str_scene);
		
		switch(str_scene)
		{
            //	OVERLAYS
            case "Starter":
            case "Chat":
                break;
			case "Be Right Back":
                //If I'm live...
                if (CPH.ObsIsStreaming())
                {
                    //... run 3 mins of commercial.
                    CPH.TwitchRunCommercial(int_com);
                }//if
				break;
            case "Ender":
                bool_creds = true;
                break;

            //	CONTENT
            case "StreamRaiders":
				CPH.RunAction("Cam Controller");
                CPH.Wait(int_wait);
                //Check Scene again
				str_scene = CPH.ObsGetCurrentScene();
                //If the Scene is still Stream Raiders...
				if (str_scene == "StreamRaiders")
                {
					//... warn me.
                    CPH.SendMessage("/me monkaW Uhm - Qmander, Streamraiders is still showing... is that intentional? LULdata");
                }//if
				break;
            case "Game_CC":
            case "ScreenShare":
            case "PC_Game":
                CPH.RunAction("Cam Controller");
				break;
			
			// OTHER
			default:
				return true;
        }//switch

		//Handle Credits
		CPH.ObsSetSourceVisibility(str_ss, "Credits", bool_creds);

		//If I'm live...
		if (CPH.ObsIsStreaming())
		{
			//... create a marker.
			CPH.CreateStreamMarker("Scene changed to: " + str_scene);
		}//if

		return true;
	}//Execute()
}//CPHInline