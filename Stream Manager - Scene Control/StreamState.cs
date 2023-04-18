using System;

public class CPHInline
{
	public bool Execute()
	{
		string stateObs = args["obsEvent.outputState"].ToString();
		string workOut = "AdultRemind";
		string upTime = "Uptime Watcher";
		string shouts = "Random Shouts";

		switch (stateObs)
		{
			case "OBS_WEBSOCKET_OUTPUT_STARTING":
				CPH.ObsSetScene("Starter");
				CPH.TwitchEmoteOnly(false);

				//Enable all Shoutouts
				CPH.EnableAction("aypoci");
				CPH.EnableAction("bobotucci");
				CPH.EnableAction("cactuarmike");
				CPH.EnableAction("claymorefenrir");
				CPH.EnableAction("earthtothien");
				CPH.EnableAction("galaxy19");
				CPH.EnableAction("gryze_wolf");
				CPH.EnableAction("grzajabarkus");
				CPH.EnableAction("harukunsama");
				CPH.EnableAction("hots_for_kuku");
				CPH.EnableAction("mechamayfly");
				CPH.EnableAction("muhgoop");
				CPH.EnableAction("ogweirdbear");
				CPH.EnableAction("owsgt");
				CPH.EnableAction("sharkiemarki3");
				CPH.EnableAction("thepenguinbean");
				CPH.EnableAction("toothpicksforrobots");
				CPH.EnableAction("whymusticryy");

				//Load the Riddles
				CPH.RunAction("Riddles - Load File");

				//Start Timers
				//CPH.EnableTimer(workOut);
				CPH.EnableTimer(upTime);
				CPH.EnableTimer(shouts);

				//Let me know it went well
				CPH.TwitchAnnounce("All systems go Q-Mander! DataFingerbang", true, "purple");
				CPH.LogInfo("『MARKER』: START_OF_STREAM");
				break;
			case "OBS_WEBSOCKET_OUTPUT_STOPPING":
				CPH.TwitchAnnounce("It was a pleasure to serve you Q-mander, see you next time!", true, "purple");
				//CPH.DisableTimer(workOut);
				CPH.DisableTimer(upTime);
				CPH.DisableTimer(shouts);
				break;
		}
		return true;
	}
}