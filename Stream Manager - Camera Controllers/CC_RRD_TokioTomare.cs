using System;

/*Cam Controller - Road Rolla Da - Stop Time
 * 
 *	Stop Time and rule the world.
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
		obSource = "ZaWarudo";
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
				//Start the Madness, Tokio Tomare
				CPH.PlaySound(filePath + "Jojo_ZaWarudo.mp3", vol);
                if (CPH.ObsIsStreaming())
                {
                    //... create a marker.
                    CPH.CreateStreamMarker("EZ Clap");
                }//if
                CPH.ObsShowSource(obSubScene, obSource);
				CPH.ObsShowFilter(obScene, usedFilters[0]);

				//Wait for End of Za Warudo
				CPH.Wait(1675);
				CPH.ObsHideFilter(obScene, usedFilters[0]);
				CPH.ObsShowFilter(obScene, usedFilters[1]);
				CPH.SendMessage("/me EZ Clap I was Here PogU DioGasm");
				CPH.ObsHideSource(obSubScene, obSource);

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