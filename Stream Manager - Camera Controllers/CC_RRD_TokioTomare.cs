using System;

/*Cam Controller - Road Rolla Da - Stop Time
 * 
 *	Stop Time and rule the world.
 * 
 */

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string[] str_filters;
		string str_src, str_path, str_scene, str_ss;
		float f_vol;

		//Initializations
		str_filters = new string[]
		{
            "Apply LUT",
            "Freeze"
        };
		str_src = "ZaWarudo";
        str_path = CPH.GetGlobalVar<string>("mediaRoot");
        str_scene = CPH.ObsGetCurrentScene();
        str_ss = "SS_Alerts";
        f_vol = CPH.GetGlobalVar<float>("mediaVolume");

        //Check the scene
        switch (str_scene)
		{
			//	All Camera Scenes
			case "Game_CC":
			case "StreamRaiders":
			case "ScreenShare":
			case "PC_Game":
				//Start the Madness, Tokio Tomare
				CPH.PlaySound(str_path + "Jojo_ZaWarudo.mp3", f_vol);
                if (CPH.ObsIsStreaming())
                {
                    //... create a marker.
                    CPH.CreateStreamMarker("EZ Clap");
                }//if
                CPH.ObsShowSource(str_ss, str_src);
				CPH.ObsShowFilter(str_scene, str_filters[0]);

				//Wait for End of Za Warudo
				CPH.Wait(1675);
				CPH.ObsHideFilter(str_scene, str_filters[0]);
				CPH.ObsShowFilter(str_scene, str_filters[1]);
				CPH.SendMessage("/me EZ Clap I was Here PogU DioGasm");
				CPH.ObsHideSource(str_ss, str_src);

				//Run Road Rolla Check
				CPH.RunAction("Road Roller Da (Check)");

				break;
			//	Other
			default:
				break;
		}//switch
		return true;
	}//Execute()
}//CPHInline