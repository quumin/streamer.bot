using System;
using System.Collections.Generic;


public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string usr_rawIn, usr_target, usr_name, usr_id;
		bool soActive;

		//Initializations
		usr_target = usr_name = usr_id = "";
		usr_rawIn = args["rawInput"].ToString();
		soActive = CPH.GetGlobalVar<bool>("globalSoActive", false);

		CPH.LogVerbose("SO Active? :" + soActive);

		//Check if the rawInput is a valid username...
		try
		{
			//If a shoutout is not active...
			if (!soActive)
			{
				//... run a shoutout.
				usr_target = args["targetUser"].ToString();
				usr_name = args["targetUserName"].ToString();
				usr_id = args["targetUserId"].ToString();

				//If you're streaming...
				if (CPH.ObsIsStreaming())
				{
					//... make sure that you don't try to run a shoutout until the previous one is done.
					CPH.TwitchSendShoutoutById(usr_id);
					CPH.SetGlobalVar("globalSoActive", true, false);
					CPH.EnableTimer("soTimer");
					CPH.SendMessage("DetectedAnomaly2 The Q-mander would like to bring your attention to  lickR @" + usr_target +
						" lickL , follow 'em at ​https://twitch.tv/" + usr_name +
						" and improve your quuminL function.", true);
				}

			}
			else
			{
				//... otherwise inform the broadcaster to wait.
				CPH.SendMessage("DataFingerbang Shoutout is still ongoing! DataFingerbang", true);
			}
		}
		//... catch if it's not.
		catch (KeyNotFoundException ex)
		{

			CPH.SendMessage("DataFingerbang Oops, " + usr_rawIn +
				" not found DataFingerbang", true);
		}

		return true;
	}
}
