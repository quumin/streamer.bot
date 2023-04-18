using System;

public class CPHInline
{
	public bool Execute()
	{
		string scene_Current = CPH.ObsGetCurrentScene();
		switch (scene_Current)
		{
			case "Game_CC":
			case "StreamRaiders":
			case "ScreenShare":
			case "PC_Game":
				string soundPath = "W:\\Streaming\\Media\\Sounds\\";
				string scene_Alerts = "SS_Alerts";
				string so_zaW = "ZaWarudo";
				string so_unW = "UnWarudo";
				string flt_lut = "Apply LUT";
				string flt_frz = "Freeze";
				float vol = 0.2f;

				//Start the Madness, Tokio Tomare
				CPH.PlaySound(soundPath + "Jojo_ZaWarudo.mp3", vol);
				CPH.CreateStreamMarker("EZ Clap");
				CPH.ObsShowSource(scene_Alerts, so_zaW);
				CPH.ObsShowFilter(scene_Current, flt_lut);

				//Wait for End of Za Warudo
				CPH.Wait(1675);
				CPH.ObsHideFilter(scene_Current, flt_lut);
				CPH.ObsShowFilter(scene_Current, flt_frz);
				CPH.SendMessage("EZ Clap I was Here PogU DioGasm");
				CPH.ObsHideSource(scene_Alerts, so_zaW);

				//Run Road Rolla Check
				CPH.RunActionById("e43f39c8-a6ab-4525-805b-58d770f24bb5");

				break;
			default:
				break;
		}
		return true;
	}
}