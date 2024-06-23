using System;

/*Cam Controller - Road Rolla Da - Time Moves
 * 
 *  Recover from Road Rolla Da.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
	public bool Execute()
	{
        //Declarations
        string[] usedFilters;
        string obSource, filePath, obScene, obSubScene;
        float vol;

        //Initializations
        usedFilters = new string[]
        {
            "Apply LUT",
            "Freeze"
        };
        obSource = "UnWarudo";
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        obScene = CPH.ObsGetCurrentScene();
        obSubScene = "SS_Alerts";
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //Check the scene
        switch (obScene)
		{
            //	All Camera Scenes
            case "Game_CC":
			case "StreamRaiders":
			case "ScreenShare":
			case "PC_Game":
				//End The Madnees, Soshite Toki Ga Ugokidesu
				CPH.ObsHideFilter(obScene, usedFilters[1]);
				CPH.RunAction("Cam Controller");
				CPH.ObsShowSource(obSubScene, obSource);
				CPH.PlaySound(filePath + "Jojo_TimeMoves.mp3", vol);
				CPH.ObsShowFilter(obScene, usedFilters[0]);
				CPH.Wait(1675);
				CPH.ObsHideFilter(obScene, usedFilters[0]);
				CPH.ObsHideSource(obSubScene, obSource);
				break;
            //	Other
            default:
				break;
		}//switch
		return true;
	}//Execute()
}//CPHInline