using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string str_oldG, str_newG, str_stat, str_msg, str_rwd, str_gms, str_srs, str_oldArt, str_newArt, str_scene;
		bool bool_gam, bool_tit, bool_serious;

		//Initializations
		str_newG = args["gameName"].ToString();
		str_stat = args["status"].ToString();
		str_scene = "SS_MidScreen";
		str_msg = "『SYSTEM CHECK』 ";
		str_rwd = "Standard";
		str_gms = "Game-Specific";
		str_srs = "seriousMode";
		bool_gam = Convert.ToBoolean(args["gameUpdate"]);
		bool_tit = Convert.ToBoolean(args["statusUpdate"]);

		try
		{
			bool_serious = CPH.GetGlobalVar<bool>("seriousMode");
		}
		catch (Exception e)
		{
			bool_serious = false;
			CPH.SetGlobalVar("seriousMode", false, true);
		}


		//If the title updates...
		if (bool_tit)
		{
			//... add status to the message.
			str_msg += str_stat;
			//... log stuff.
			CPH.LogInfo("『STATUS UPDATE』: Title update to \'" + str_stat + "\'!");
			CPH.LogInfo("『MARKER』: STATUS_UPDATE");
		}//if (bool_tit)

		//If both update...
		if (bool_tit && bool_gam)
		{
			//... add the separator to the message.
			str_msg += " | ";
		}//if (bool_tit && bool_gam)
		 //... or just the game...
		else if (bool_gam)
		{
			//... and the game is not serious...
			if (!bool_serious)
			{
				//... update message to be only the "game."
				str_msg = "『GAME UPDATE』 ";
			}//if(!bool_serious)	
			else
			{
				//... update message to be "serious game."
				str_msg = "『SERIOUS GAME』 ";
			}//else
		}//else


		//If the game updates...
		if (bool_gam)
		{

			//... and the game is not serious...
			if (!bool_serious)
			{
				//... make sure the Normal Game action runs to enable/disable all alerts.
				CPH.RunAction("Normal Game");
			}//if(!bool_serious)

			//... get old game name.
			str_oldG = args["oldGameName"].ToString();

			//... log stuff.
			CPH.LogInfo("『GAME UPDATE』: Game update from \'" + str_oldG + "\' to \'" + str_newG + "\'!");
			str_msg += str_oldG + " -> " + str_newG;

			//... If OBS is streaming...
			if (CPH.ObsIsStreaming())
			{
				//... create a marker.
				CPH.CreateStreamMarker("CHANGE - " + str_newG);

			}//if (CPH.ObsIsStreaming())

			CPH.LogInfo("『MARKER』: GAME_UPDATE");
		}//if (bool_gam)

		//Feedback in Chat
		CPH.TwitchAnnounce(str_msg,
			true,
			"purple");

		//If the game updates...
		if (bool_gam)
		{
			//... get art.
			str_newArt = args["gameBoxArt"].ToString();
			str_oldArt = args["oldGameBoxArt"].ToString();

			//... show the game box art change.
			CPH.ObsSetBrowserSource(str_scene, "Old GameBox Art", str_oldArt);
			CPH.ObsSetBrowserSource(str_scene, "New GameBox Art", str_newArt);
			CPH.ObsShowSource(str_scene, "Old GameBox Art");
			CPH.Wait(2000);
			CPH.ObsHideSource(str_scene, "Old GameBox Art");
			CPH.ObsShowSource(str_scene, "New GameBox Art");
			CPH.Wait(2000);
			CPH.ObsHideSource(str_scene, "New GameBox Art");
		}//if (bool_gam)

		return true;
	}//public bool Execute()
}//public class CPHInline
