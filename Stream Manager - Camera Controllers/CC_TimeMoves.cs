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

				//End The Madnees, Soshite Toki Ga Ugokidesu
				CPH.ObsHideFilter(scene_Current, flt_frz);
				CPH.RunActionById("2181b4aa-3473-409a-aa10-df2f7736db35");
				CPH.ObsShowSource(scene_Alerts, so_unW);
				CPH.PlaySound(soundPath + "Jojo_TimeMoves.mp3", vol);
				CPH.ObsShowFilter(scene_Current, flt_lut);
				CPH.Wait(1675);
				CPH.ObsHideFilter(scene_Current, flt_lut);
				CPH.ObsHideSource(scene_Alerts, so_unW);
				break;
			default:
				break;
		}
		return true;
	}
}