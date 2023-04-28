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

		CPH.LogInfo("SO Active? :" + soActive);

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

				//If streaming...
				if (CPH.ObsIsStreaming())
				{
					//... make sure that you don't try to run a shoutout until the previous one is done.
					CPH.TwitchSendShoutoutById(usr_id);
					CPH.SetGlobalVar("globalSoActive", true, false);
					CPH.EnableTimer("soTimer");
					CPH.SendMessage("/me DetectedAnomaly2 The Q-mander would like to bring your attention to  lickR @" + usr_target +
						" lickL , follow 'em at ​https://twitch.tv/" + usr_name +
						" and improve your quuminL function.", true);
				}//if

			}//if
			else
			{
				//... otherwise inform the broadcaster to wait.
				CPH.SendMessage("/me DataFingerbang Shoutout is still ongoing! DataFingerbang", true);
			}//else
		}//try
		//... catch if it's not.
		catch (KeyNotFoundException ex)
		{

			CPH.SendMessage("/me DataFingerbang Oops, " + usr_rawIn +
				" not found DataFingerbang", true);
		}//catch

		return true;
	}//Execute()
}//CPHInline