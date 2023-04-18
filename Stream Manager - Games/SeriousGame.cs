using System;

public class CPHInline
{
	public bool Execute()
	{
		//Enable Serious mode.
		CPH.SetGlobalVar("seriousMode", true, true);

		//Disable Sound Actions.
		CPH.DisableAction("Best of Both Worlds");
		CPH.DisableAction("BingChilling");
		CPH.DisableAction("Kira");
		CPH.DisableAction("KEKW");
		CPH.DisableAction("Torture Dance");
		CPH.DisableAction("Unlurk");
		CPH.DisableAction("EZ Clap");
		CPH.DisableAction("Fuck You Data");
		CPH.DisableAction("Oh No!");
		CPH.DisableAction("OMT");
		CPH.DisableAction("Thanks Data");
		CPH.DisableAction("YareYare");

		//Disable all Rewards.
		CPH.TwitchRewardGroupDisable("Standard");
		CPH.TwitchRewardGroupDisable("Standard - Sounds");
		CPH.TwitchRewardGroupDisable("Game - Specific");
		CPH.TwitchRewardGroupDisable("GS - PoE");

		//Show Serious Visualizer.
		CPH.ObsShowSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Serious");
		CPH.ObsHideSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Normal");
		CPH.ObsHideSource("SS_MidScreen", "soPlayer");
		return true;
	}
}
