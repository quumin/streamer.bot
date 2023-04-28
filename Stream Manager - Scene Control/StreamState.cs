using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
        //Declarations
        List<string> list_actions;
        string[] str_timers;
		string str_state, str_msg;

        //Initializations
        list_actions = CPH.GetGlobalVar<List<string>>("autoShouts"); 
		str_timers = new string[]
		{
            "AdultRemind",
            "Uptime Watcher",
            "Random Shouts"
        };
		str_state = args["obsEvent.outputState"].ToString();
		str_msg = "/me ";

		//Check the state of OBS
		switch (str_state)
		{
			//	Output Starting
			case "OBS_WEBSOCKET_OUTPUT_STARTING":
				CPH.ObsSetScene("Starter");
				CPH.TwitchEmoteOnly(false);

                //Enable all Shoutouts
                //... Disable Sound Actions.
                foreach (string s in list_actions)
                {
                    CPH.EnableAction(s);
                }//foreach

                //Load the Riddles
                CPH.RunAction("Riddles - Load File");

				//Start Timers
				for(int i = 0; i < str_timers.Length; i++)
					CPH.EnableTimer(str_timers[i]);

				//Feedback
				str_msg += "All systems go Q-Mander! DataFingerbang";
				CPH.LogInfo("『MARKER』: START_OF_STREAM");
				break;
			//	Output Stopping
			case "OBS_WEBSOCKET_OUTPUT_STOPPING":
				str_msg += "It was a pleasure to serve you Q-mander, see you next time!";
                //Stop Timers
                for (int i = 0; i < str_timers.Length; i++)
                    CPH.DisableTimer(str_timers[i]);
                break;
			//	Other
			default:
				return true;
		}//switch

		//Send message
		CPH.SendMessage(str_msg);

		return true;
	}//Execute()
}//CPHInline