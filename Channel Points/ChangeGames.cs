using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		List<string> games = new List<string>();
		string usr_choice = "";
		string usr = "";
		string usr_redemption = "";
		string usr_reward = "";

		//Assignments
		games = CPH.GetGlobalVar<List<string>>("gamesList");

		if (CPH.ObsIsStreaming())
		{
			usr_redemption = args["redemptionId"].ToString();
			usr_reward = args["rewardId"].ToString();
		}

		usr = args["userName"].ToString();
		usr_choice = args["rawInput"].ToString();

		games = gameLoad();


		if (games.Contains(usr_choice))
		{
			CPH.SendMessage("thinkingJojo so uh... Q, you gonna change to " + usr_choice + " like @" + usr + " asked, or...? Ghost", true);
			CPH.SetChannelGame(usr_choice);
			CPH.EnableTimer("GameChanger");
		}
		else
		{
			CPH.LogDebug("『G A M E S』 " + usr_choice + " not found!");
			CPH.SendMessage("WTFF Sorry @" + usr + " , \"" + usr_choice + "\" was not found! dataHuh Don't worry, your points have been refunded! Saved Please check the list below PointDown and try again.", true);
			if (CPH.ObsIsStreaming())
			{
				CPH.TwitchRedemptionCancel(usr_reward, usr_redemption);
			}
		}

		return true;
	}
	List<string> gameLoad()
	{
		List<string> games = new List<string>();
		try
		{
			using (var reader = new StreamReader(@".\\external_files\\GamesList.csv"))
			{
				//Try to find the file
				//Decalarations


				//Populate the Lists
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					games.Add(line);
				}

				//Store the Globals
				CPH.SetGlobalVar("gamesList", games);
				CPH.LogInfo("『G A M E S』 Loaded Successfully.");
				//Announcement
			}
		}
		catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
		{
			//Catch when the directory and/or file is incorrect.
			CPH.LogWarn("『G A M E S』 Game file failed to load! Is the directory correctly set?");
			CPH.TwitchAnnounce("dataHuh The Games file could not be found, sir.", true,
				"orange");

		}
		return games;
	}
}
