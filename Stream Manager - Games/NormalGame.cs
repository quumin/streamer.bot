using System;

public class CPHInline
{
	public bool Execute()
	{
		//Disable Serious Mode.
		CPH.SetGlobalVar("seriousMode", false, true);

		//Enable Normal Rewards.
		CPH.TwitchRewardGroupEnable("Standard");
		CPH.TwitchRewardGroupEnable("Standard - Sounds");

		//Disable Game Specific Rewards.
		CPH.TwitchRewardGroupDisable("Game - Specifc");
		CPH.TwitchRewardGroupDisable("GS - PoE");

		//Enable Sound Actions.
		CPH.EnableAction("Best of Both Worlds");
		CPH.EnableAction("BingChilling");
		CPH.EnableAction("Graphic Design");
		CPH.EnableAction("KEKW");
		CPH.EnableAction("Kira");
		CPH.EnableAction("Torture Dance");
		CPH.EnableAction("Unlurk");
		CPH.EnableAction("EZ Clap");
		CPH.EnableAction("Fuck You Data");
		CPH.EnableAction("Oh No!");
		CPH.EnableAction("OMT");
		CPH.EnableAction("Thanks Data");
		CPH.EnableAction("YareYare");

		//Show Normal Visualizer.
		CPH.ObsHideSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Serious");
		CPH.ObsShowSource("SS_KiyoPro_FancyCam", "VM_Visualizer_Normal");
		CPH.ObsShowSource("SS_MidScreen", "soPlayer");
		return true;
	}
}
