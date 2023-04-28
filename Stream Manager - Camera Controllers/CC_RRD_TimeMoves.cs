using System;

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
        str_src = "UnWarudo";
        str_path = "W:\\Streaming\\Media\\Sounds\\";
        str_scene = CPH.ObsGetCurrentScene();
        str_ss = "SS_Alerts";
        f_vol = 0.15f;

        //Check the scene
        switch (str_scene)
		{
            //	All Camera Scenes
            case "Game_CC":
			case "StreamRaiders":
			case "ScreenShare":
			case "PC_Game":
				//End The Madnees, Soshite Toki Ga Ugokidesu
				CPH.ObsHideFilter(str_scene, str_filters[1]);
				CPH.RunAction("Cam Controller");
				CPH.ObsShowSource(str_ss, str_src);
				CPH.PlaySound(str_path + "Jojo_TimeMoves.mp3", f_vol);
				CPH.ObsShowFilter(str_scene, str_filters[0]);
				CPH.Wait(1675);
				CPH.ObsHideFilter(str_scene, str_filters[0]);
				CPH.ObsHideSource(str_ss, str_src);
				break;
            //	Other
            default:
				break;
		}//switch
		return true;
	}//Execute()
}//CPHInline