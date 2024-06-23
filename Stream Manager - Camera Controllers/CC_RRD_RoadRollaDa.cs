using System;

/*Cam Controller - Road Rolla Da
 * 
 *  Smash me into a pulp no matter which scene I'm in.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] usedFilters, usedCams;
        string obSource, filePath, obScene, obSubScene;
        float vol;

        //Initializations
        usedFilters = new string[]
        {
            "Apply LUT",
            "Freeze"
        };
        usedCams = new string[]
        {
            "SS_KiyoPro_FancyCam",
            "SS_KP_NF"
        };
        obSource = "Road_Rolla_Da";
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        obScene = CPH.ObsGetCurrentScene();
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        switch (obScene)
        {
            //	All Camera Scenes
            case "Game_CC":
            case "StreamRaiders":
            case "ScreenShare":
            case "PC_Game":
                //Start the Clock... in stopped... time?
                CPH.ObsHideFilter(obScene, usedFilters[1]);
                CPH.ObsShowFilter(usedCams[1], usedFilters[0]);
                CPH.ObsShowFilter(usedCams[1], usedFilters[1]);
                CPH.ObsShowFilter(usedCams[0], obSource);
                CPH.PlaySound(filePath + "Jojo_DioCountdown.mp3", vol, true);
                CPH.ObsShowSource(usedCams[0], obSource);
                CPH.Wait(5039);

                //WRYYYYYY
                CPH.ObsHideSource(usedCams[0], obSource);
                CPH.ObsHideSource(obScene, usedCams[0]);
                CPH.PlaySound(filePath + "Jojo_Wryy.mp3", vol, true);

                //Recover
                CPH.ObsHideFilter(usedCams[1], usedFilters[1]);
                CPH.ObsShowSource(obScene, usedCams[0]);
                CPH.ObsShowFilter(obScene, usedFilters[1]);
                CPH.ObsHideFilter(usedCams[1], usedFilters[0]);
                CPH.RunAction("Road Roller Da (TimeMoves)");
                break;
            //	Other
            default:
                break;
        }//switch
        return true;
    }//Execute()
}//CPHInline