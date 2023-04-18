using System;

public class CPHInline
{
	public bool Execute()
	{
		string scene_selected = CPH.ObsGetCurrentScene();
		CPH.LogDebug("SCENE: " + scene_selected);
		//CPH.Wait(305);
		//Scenes with Corner Camera
		if (scene_selected == "PC_Game" ||
			scene_selected == "StreamRaiders" ||
			scene_selected == "ScreenShare" ||
			scene_selected == "Game_CC")
		{
			//Adjust the Camera
			CPH.RunAction("Cam Controller");
		}

		//Ender
		if (scene_selected == "Ender")
		{
			CPH.ObsSetSourceVisibility("SS_MidScreen", "Credits", true);
		}
		else
		{
			CPH.ObsSetSourceVisibility("SS_MidScreen", "Credits", false);
		}

		//StreamRaiders
		if (scene_selected == "StreamRaiders")
		{
			CPH.Wait(12000);
			scene_selected = CPH.ObsGetCurrentScene();
			if (scene_selected == "StreamRaiders")
			{
				CPH.SendMessage("monkaW Uhm - Qmander, Streamraiders is still showing... is that intentional? LULdata");
			}
		}

		//If I'm live...
		if (CPH.ObsIsStreaming())
		{
			//... create a marker.
			CPH.CreateStreamMarker("Scene changed to: " + scene_selected);
		}

		return true;
	}
}