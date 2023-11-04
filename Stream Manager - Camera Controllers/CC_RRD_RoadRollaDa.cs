using System;

/*Cam Controller - Road Rolla Da
 * 
 *  Smash me into a pulp no matter which scene I'm in.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_filters, str_cam;
        string str_src, str_path, str_scene, str_ss;
        float f_vol;

        //Initializations
        str_filters = new string[]
        {
            "Apply LUT",
            "Freeze"
        };
        str_cam = new string[]
        {
            "SS_KiyoPro_FancyCam",
            "SS_KP_NF"
        };
        str_src = "Road_Rolla_Da";
        str_path = CPH.GetGlobalVar<string>("qminMediaRoot");
        str_scene = CPH.ObsGetCurrentScene();
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        switch (str_scene)
        {
            //	All Camera Scenes
            case "Game_CC":
            case "StreamRaiders":
            case "ScreenShare":
            case "PC_Game":
                //Start the Clock... in stopped... time?
                CPH.ObsHideFilter(str_scene, str_filters[1]);
                CPH.ObsShowFilter(str_cam[1], str_filters[0]);
                CPH.ObsShowFilter(str_cam[1], str_filters[1]);
                CPH.ObsShowFilter(str_cam[0], str_src);
                CPH.PlaySound(str_path + "Jojo_DioCountdown.mp3", f_vol, true);
                CPH.ObsShowSource(str_cam[0], str_src);
                CPH.Wait(5039);

                //WRYYYYYY
                CPH.ObsHideSource(str_cam[0], str_src);
                CPH.ObsHideSource(str_scene, str_cam[0]);
                CPH.PlaySound(str_path + "Jojo_Wryy.mp3", f_vol, true);

                //Recover
                CPH.ObsHideFilter(str_cam[1], str_filters[1]);
                CPH.ObsShowSource(str_scene, str_cam[0]);
                CPH.ObsShowFilter(str_scene, str_filters[1]);
                CPH.ObsHideFilter(str_cam[1], str_filters[0]);
                CPH.RunAction("Road Roller Da (TimeMoves)");
                break;
            //	Other
            default:
                break;
        }//switch
        return true;
    }//Execute()
}//CPHInline