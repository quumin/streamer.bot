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
				string scene_Cam = "SS_KiyoPro_FancyCam";
				string scene_JustCam = "SS_KP_NF";
				string rrd = "Road_Rolla_Da";
				string soundPath = "W:\\Streaming\\Media\\Sounds\\";
				string flt_frz = "Freeze";
				string flt_lut = "Apply LUT";
				float vol = 0.2f;

				//Start the Clock... in stopped... time?
				CPH.ObsHideFilter(scene_Current, flt_frz);
				CPH.ObsShowFilter(scene_JustCam, flt_lut);
				CPH.ObsShowFilter(scene_JustCam, flt_frz);
				CPH.ObsShowFilter(scene_Cam, rrd);
				CPH.PlaySound(soundPath + "Jojo_DioCountdown.mp3", vol, true);
				CPH.ObsShowSource(scene_Cam, rrd);
				CPH.Wait(5039);

				//WRYYYYYY
				CPH.ObsHideSource(scene_Cam, rrd);
				CPH.ObsHideSource(scene_Current, scene_Cam);
				CPH.PlaySound(soundPath + "Jojo_Wryy.mp3", vol, true);

				//Recover
				CPH.ObsHideFilter(scene_JustCam, flt_frz);
				CPH.ObsShowSource(scene_Current, scene_Cam);
				CPH.ObsShowFilter(scene_Current, flt_frz);
				CPH.ObsHideFilter(scene_JustCam, flt_lut);
				CPH.RunActionById("672d1336-c416-4950-8a2c-15219b1a6ea1");

				break;
			default:
				break;
		}
		return true;
	}
}